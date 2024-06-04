using AutoMapper;
using NetBanking.Core.Dtos;
using NetBanking.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Infrastructure.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<User, User>();
            CreateMap<Check, Check>()
                .ForMember(des => des.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(des => des.ReceiverName, opt => opt.MapFrom(src => src.ReceiverName))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<CreditCard, CreditCard>()
                .ForMember(des => des.CreditLimit, opt => opt.MapFrom(src => src.CreditLimit))
                .ForMember(des => des.Balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(des => des.ProductStatus, opt => opt.MapFrom(src => src.ProductStatus))
                .ForMember(des => des.DailyWithdrawalLimit, opt => opt.MapFrom(src => src.DailyWithdrawalLimit))
                .ForAllMembers(opt => opt.Ignore());

		}
	}
}
