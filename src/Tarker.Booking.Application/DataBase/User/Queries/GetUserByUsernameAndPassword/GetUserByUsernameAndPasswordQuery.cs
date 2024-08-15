using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarker.Booking.Application.DataBase.User.Queries.GetUserByUsernameAndPassword
{
    public class GetUserByUsernameAndPasswordQuery: IGetUserByUsernameAndPasswordQuery
    {
        private readonly IDataBaseService _DatabaseService;
        private readonly IMapper _mapper;

        public GetUserByUsernameAndPasswordQuery(IDataBaseService dataBaseService, IMapper mapper)
        {
            _DatabaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<GetUserByUsernameAndPasswordModel> Execute(string userName, string password)
        {
            var entity = await _DatabaseService.User.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
            
            return _mapper.Map<GetUserByUsernameAndPasswordModel>(entity);
        }
    }
}
