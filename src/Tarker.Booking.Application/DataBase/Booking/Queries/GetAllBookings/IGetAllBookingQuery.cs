namespace Tarker.Booking.Application.DataBase.Booking.Queries.GetAllBookings
{
    public interface IGetAllBookingQuery
    {
        Task<List<GetAllBookingModel>> Execute();
    }
}
