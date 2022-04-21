using CryptoBoard.Application.DTOs;
using CryptoBoard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoBoard.API.Controllers
{
    [Route("api/[controller]")]
    public class BinanceController : Controller
    {
        private readonly IBinanceService _binanceService;

        public BinanceController(IBinanceService binanceService)
        {
            _binanceService = binanceService;
        }

        [HttpGet("loadlist")]
        public async Task<IActionResult> Binance()
        {
            await _binanceService.LoadCoinList();

            return Ok();
        }

        [HttpGet("listCryptocurrencies/{skip:int}/{take:int}")]
        public async Task<IActionResult> ListCryptocurrencies([FromRoute] int skip = 0, [FromRoute] int take = 100)
        {
            return Ok(await _binanceService.CryptocurrenciesData(skip, take));
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {   
            return Ok(await _binanceService.GetSymbolId(id));
        }
    }
}
