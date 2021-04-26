using AutoMapper;
using Cms.Services.Contract.AuthenticationService;
using Cms.Services.Contract.StockManagementService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Dto = Cms.StockManagementApi.Models;

namespace Cms.StockManagementApi.Controllers
{
    [Authorize, Route("api/v1"), ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStockQueryService _stockQueryService;
        private readonly IStockUpdateService _stockUpdateService;
        private readonly IAuthenticationService _authenticationService;

        public StockController(IStockQueryService stockQueryService,
                               IStockUpdateService stockUpdateService,
                               IAuthenticationService authenticationService,
                               IMapper mapper)
        {
            _stockQueryService = stockQueryService;
            _stockUpdateService = stockUpdateService;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [AllowAnonymous, HttpPost, Route("login"), Consumes(MediaTypeNames.Application.Json), ProducesResponseType(200)]
        public ActionResult<Dto.LoginResult> Login(Dto.LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid request");
            }

            var loginResult = _authenticationService.Authenticate(loginRequest.Username, loginRequest.Password);
            if (loginResult.AuthenticationSuccess)
            {
                var loginResultDto = _mapper.Map<Dto.LoginResult>(loginResult);
                return Ok(loginResultDto);
            }

            return Unauthorized();
        }

        [HttpGet, Route("list"), ProducesResponseType(200)]
        public async Task<ActionResult<List<Dto.StockItem>>> GetStockItems([FromQuery] Dto.PaginationQuery pagination)
        {
            var paginationFilter = _mapper.Map<Types.PaginationFilter>(pagination);
            var results = await _stockQueryService.GetStockItemsAsync(paginationFilter);

            return Ok(new Dto.PagedResponse<Dto.StockItem>(_mapper.Map<List<Dto.StockItem>>(results), paginationFilter.PageNumber, paginationFilter.PageSize));
        }

        [HttpPost, Route("search"), ProducesResponseType(200)]
        public async Task<ActionResult<List<Dto.StockItem>>> SearchStockItems(Dto.StockItem searchItemDto)
        {
            var searchItem = _mapper.Map<Types.StockItem>(searchItemDto);
            var results = await _stockQueryService.SearchStockAsync(searchItem);

            return Ok(new Dto.Response<List<Dto.StockItem>>(_mapper.Map<List<Dto.StockItem>>(results)));
        }

        [HttpPost, Consumes(MediaTypeNames.Application.Json), ProducesResponseType(201)]
        public async Task<IActionResult> AddStockItem(Dto.StockItem stockItemDto)
        {
            var stockItem = _mapper.Map<Types.StockItem>(stockItemDto);
            await _stockUpdateService.AddAsync(stockItem);

            return Ok();
        }

        [HttpPost, Route("accessory"), Consumes(MediaTypeNames.Application.Json), ProducesResponseType(201)]
        public async Task<IActionResult> AddStockItemAccessory(Dto.StockAccessory stockAccessoryDto)
        {
            var stockItemAccessory = _mapper.Map<Types.StockAccessory>(stockAccessoryDto);
            await _stockUpdateService.AddAsync(stockItemAccessory);

            return Ok();
        }

        [HttpPost, Route("image"), Consumes(MediaTypeNames.Application.Json), ProducesResponseType(201)]
        public async Task<IActionResult> AddStockItemImage(Dto.Image imageDto)
        {
            var stockItemImage = _mapper.Map<Types.Image>(imageDto);
            await _stockUpdateService.AddAsync(stockItemImage);

            return Ok();
        }

        [HttpDelete, Route("{id}"), ProducesResponseType(200)]
        public async Task<IActionResult> DeleteStockItem(int id)
        {
            await _stockUpdateService.DeleteAsync(id);

            return Ok();
        }

        [HttpDelete, Route("accessory/{id}"), ProducesResponseType(200)]
        public async Task<IActionResult> DeleteStockItemAccessory(int id)
        {
            await _stockUpdateService.DeleteAccessoryAsync(id);

            return Ok();
        }

        [HttpDelete, Route("image/{id}"), ProducesResponseType(200)]
        public async Task<IActionResult> DeleteStockItemImage(int id)
        {
            await _stockUpdateService.DeleteImageAsync(id);

            return Ok();
        }

        [HttpPut, Consumes(MediaTypeNames.Application.Json), ProducesResponseType(200)]
        public async Task<IActionResult> UpdateStockItem(Dto.StockItem stockItemDto)
        {
            var stockItem = _mapper.Map<Types.StockItem>(stockItemDto);
            await _stockUpdateService.UpdateAsync(stockItem);

            return Ok();
        }
    }
}
