﻿namespace Tarker.Booking.Application.DataBase.Booking.Queries.GetAllBookings
{
    public class GetAllBookingModel
    {
        public int BookingId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerDocumentNumber { get; set; }

    }
}
