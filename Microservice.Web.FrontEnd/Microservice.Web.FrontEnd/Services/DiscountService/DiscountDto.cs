﻿namespace Microservice.Web.FrontEnd.Services.DiscountService
{
    public class DiscountDto
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string Code { get; set; }
        public bool Used { get; set; }
    }
}
