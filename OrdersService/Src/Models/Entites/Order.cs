namespace OrdersService.Models.Entites
{
    public class Order
    {
        public Order(string UserId,
            string FirstName,string LastName,
            string Address,string PhoneNumber,
            double TotalPrice,
            List<OrderLine> orderLines)
        {
            this.UserId = UserId;
            OrderPlaced = DateTime.Now;
            OrderPaid = false;
            OrderLines = orderLines;
            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.TotalPrice=TotalPrice;
            this.PaymentStatus = PaymentStatus.unPaid;
        }
        public Order()
        {
            OrderLines= new List<OrderLine>();
        }
        public Guid Id { get; set; }
        public string  UserId { get;  set; }
        public DateTime  OrderPlaced { get; private set; }
        public bool OrderPaid { get; private set; }
        public string  FirstName { get; private set ; }
        public string  LastName { get; private set ; }
        public string  Address { get; private set ; }
        public string  PhoneNumber { get;  private set; }
        public double TotalPrice { get; set; }
        public ICollection<OrderLine> OrderLines { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }

        public void RequestPayment()
        {
            PaymentStatus = PaymentStatus.RequestPayment;
        }
        public void PaymentIsDone()
        {
            OrderPaid= true;
            PaymentStatus = PaymentStatus.isPaid;
        }
    }
    public enum PaymentStatus
    {
        /// <summary>
        /// پرداخت نشده
        /// </summary>
        unPaid=0,
        /// <summary>
        /// درخواست پرداخت شده 
        /// </summary>
        RequestPayment=1,
        /// <summary>
        /// پرداخت شده است
        /// </summary>
        isPaid=3


    }
}
