using CryptoBoard.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace CryptoBoard.API.Controllers
{
    [Route("api/[controller]")]
    public class BinanceController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetSymbols()
        {
            var url = "https://api.binance.com/api/v3/exchangeInfo";

            var result = await url.GetJsonAsync<CoinsList>();

            return Ok(result);
        }
    }
}
