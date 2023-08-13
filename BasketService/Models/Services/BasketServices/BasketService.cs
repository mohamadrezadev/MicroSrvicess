using AutoMapper;
using BasketService.Infrastructure.Contexts;
using BasketService.MessagingBus;
using BasketService.Models.Dtos;
using BasketService.Models.Entites;
using BasketService.Models.Services.BasketServices.MessageDto;
using BasketService.Models.Services.DiscountServices;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BasketService.Models.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly BasketDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBus messageBus;
        private readonly string queueName_checkoutBasket;
        public BasketService(BasketDatabaseContext context, IMapper mapper,IMessageBus messageBus,
            IOptions<RabbitMqConfiguration>reabbitmqOptions
         )
        {
            _context = context;
            _mapper = mapper;
            this.messageBus = messageBus;
            queueName_checkoutBasket = reabbitmqOptions.Value.QueueName_BaksetChekout;
        }

        public void AddItemToBasket(AddItemToBasketDto Item)
        {
            var basket = _context.Baskets.FirstOrDefault(p => p.id == Item.basketId);
            if (basket == null)
                throw new Exception("Basket not founds .....!");
            var basketitem = _mapper.Map<BasketItem>(Item);
            var productdto = _mapper.Map<Productdto>(Item);
            CreateProduct(productdto);
            basket.Items.Add(basketitem);
            _context.SaveChanges();

        }
        private Productdto Getproduct(Guid ProductId)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (existProduct != null)
            {
                var producdto = _mapper.Map<Productdto>(existProduct);
                return producdto;
            }
            return null;
        }
        private Productdto CreateProduct(Productdto producdto)
        {
            var exitproduct = Getproduct(producdto.ProductId);
            if (exitproduct != null)
            {
                return exitproduct;
            }
            else
            {
                var newproduct = _mapper.Map<Product>(producdto);
                _context.Products.Add(newproduct);
                _context.SaveChanges();
                return _mapper.Map<Productdto>(newproduct);

            }
        }

        public void ApplyDiscountTobasket(Guid BasketId, Guid DiscountId)
        {
            var basket = _context.Baskets.FirstOrDefault(b => b.id == BasketId);
            basket.DiscountId = DiscountId;
            _context.SaveChanges();

        }

        public BasketDto GetBasket(string userid)
        {
            var basket = _context.Baskets.Include(p => p.Items).ThenInclude(p => p.Product).FirstOrDefault(p => p.UserId == userid);
            if (basket == null) return new BasketDto();

            return new BasketDto
            {
                Id = basket.id,
                UserId = basket.UserId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    id = item.id,
                    ImageUrl = item.Product.ImageUrl,
                    Productid = item.Product.ProductId,
                    ProductName = item.Product.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.UnitPrice
                }).ToList()

            };
        }

        public BasketDto GetOrCreateBasketForUser(string userId)
        {
            var basket = _context.Baskets.Include(p => p.Items).ThenInclude(p => p.Product).FirstOrDefault(p => p.UserId == userId);
            if (basket == null)
            {
                return CreateBasketForUser(userId);
            }

            return new BasketDto
            {
                Id = basket.id,
                UserId = basket.UserId,
                DiscountId = basket.DiscountId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    id = item.id,

                    ImageUrl = item.Product.ImageUrl,
                    Productid = item.Product.ProductId,
                    ProductName = item.Product.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.UnitPrice
                })
                    .ToList()
            };
        }

        public void RemoveItemFromBasket(Guid ItemId)
        {
            var item = _context.BasketItems.FirstOrDefault(p => p.id == ItemId);
            if (item == null)
                throw new Exception("Basket not founds .....!");
            _context.BasketItems.Remove(item);
            _context.SaveChanges();
        }

        public void SetQuantities(Guid itemid, int quantity)
        {
            var item = _context.BasketItems.FirstOrDefault(p => p.id == itemid);
            item.SetQuantity(quantity);
            _context.SaveChanges();

        }

        public void TransferBasket(string anonymousId, string userId)
        {
            var anonymousBasket = _context.Baskets
                .Include(p => p.Items).ThenInclude(p => p.Product)
                .FirstOrDefault(p => p.UserId == anonymousId);
            if (anonymousBasket == null) return;
            var userbasket = _context.Baskets.SingleOrDefault(p => p.UserId == anonymousId);
            if (userbasket == null)
            {
                userbasket = new Basket(userId);
                _context.Baskets.Add(userbasket);

            }
            foreach (var item in anonymousBasket.Items)
            {
                userbasket.Items.Add(new BasketItem
                {

                    ProductId = item.ProductId,
                    Quantity = item.Quantity,

                });
            }
            _context.Baskets.Remove(anonymousBasket);
            _context.SaveChanges();
        }

        private BasketDto CreateBasketForUser(string userid)
        {
            Basket basket = new Basket(userid);
            _context.Baskets.Add(basket);
            _context.SaveChanges();
            return new BasketDto
            {
                UserId = basket.UserId,
                Id = basket.id
            };
        }

        public ResultDto CheckoutBasket(CheckoutBasketDto checkoutBasket, IDiscountService discountService)
        {
            //get basket
            var basket = _context.Baskets.Include(p => p.Items).ThenInclude(p => p.Product)
                .SingleOrDefault(p => p.id == checkoutBasket.BasketId);
            if(basket == null)
            {
                return new ResultDto()
                {
                    IsSuccess=false,
                    Message=$"{nameof(basket)} not Found"
                };
            }
     

            //send message with rabbitmq
            BasketCheckoutMessage Message = _mapper.Map<BasketCheckoutMessage>(checkoutBasket);
            double TotalPrice = 0;
            basket.Items.ForEach(item =>
            {
                var basketItem = new BasketItemMessage()
                {
                    BasketItemId=item.BasketId,
                    ProductId=item.Product.ProductId,
                    NameProduct=item.Product.ProductName,
                    Price=item.Product.UnitPrice,
                    Quantity=item.Quantity
                };
                TotalPrice += item.Product.UnitPrice * item.Quantity;
                Message.basketItem.Add(basketItem);
                

            });
            // get discount
            DiscountDto discountDto = null;
            if (basket.DiscountId.HasValue)
                discountDto = discountService.GetDicountById(basket.DiscountId.Value);
            
            if (discountDto!=null)
            {
                Message.TotalPrice = TotalPrice - discountDto.Amount;
            }
            else
            {
                Message.TotalPrice = TotalPrice;
            }
            messageBus.SendMessage(Message, queueName_checkoutBasket);
           
            //delete basket
            _context.Baskets.Remove(basket);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "سفارش شما با موفقیت ثبت گردید ",
            };
        }
    }
}
