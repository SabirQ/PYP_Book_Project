﻿using Newtonsoft.Json;

namespace WebApi.Models
{
    public class ExceptionModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
