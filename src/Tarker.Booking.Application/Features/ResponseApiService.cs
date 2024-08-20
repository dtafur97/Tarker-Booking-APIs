﻿using Tarker.Booking.Domain.Models;

namespace Tarker.Booking.Application.Features
{
    public static class ResponseApiService
    {
        public static BaseResponseModel Response(int statusCode, string message = null, Object Data = null) 
        {
            bool success = false;

            if (statusCode >= 200 && statusCode <= 299)
            {
                success = true;
            }

            var result = new BaseResponseModel
            {
                StatusCode = statusCode,
                Success = success,
                Message = message,
                Data = Data
            };

            return result;
        }
    }
}
