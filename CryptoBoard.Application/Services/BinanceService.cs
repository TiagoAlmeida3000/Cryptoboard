using AutoMapper;
using CryptoBoard.Application.DTOs;
using CryptoBoard.Application.Interfaces;
using CryptoBoard.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Application.Services
{
    public class BinanceService : IBinanceService
    {
        private IBinanceRepository _binanceRepository;
        private readonly IMapper _mapper;

        public BinanceService(IBinanceRepository binanceRepository, IMapper mapper)
        {
            _binanceRepository = binanceRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SymbolsDTO>> GetListSymbols()
        {
            var symbolsEntities = await _binanceRepository.GetListSymbols();
            return _mapper.Map<IEnumerable<SymbolsDTO>>(symbolsEntities);
        }

        public async Task<SymbolsDTO> GetSymbolId(int? id)
        {
            var symbolsEntities = await _binanceRepository.GetSymbolId(id);
            return _mapper.Map<SymbolsDTO>(symbolsEntities);
        }

        public async Task LoadCoinList()
        {
            await _binanceRepository.LoadCoinList();
        }
    }
}
