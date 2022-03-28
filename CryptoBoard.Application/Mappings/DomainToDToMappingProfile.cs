using AutoMapper;
using CryptoBoard.Application.DTOs;
using CryptoBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Application.Mappings
{
    public class DomainToDToMappingProfile : Profile
    {
        public DomainToDToMappingProfile()
        {
            CreateMap<CoinList, CoinListDTO>().ReverseMap();
            CreateMap<Symbols, SymbolsDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
