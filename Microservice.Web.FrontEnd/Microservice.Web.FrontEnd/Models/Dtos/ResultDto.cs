﻿using Microsoft.CodeAnalysis.Options;

namespace Microservice.Web.FrontEnd.Models.Dtos
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string   Message { get; set; }
    }
    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T  Data { get; set; }
    }
}
