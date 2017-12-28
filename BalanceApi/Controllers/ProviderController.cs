using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BalanceApi.Model.Domain;
using BalanceApi.Model.Views.Request;
using BalanceApi.Model.Views.Response;
using BalanceApi.Services;
using BalanceApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BalanceApi.Controllers.Routes;

namespace BalanceApi.Controllers {

    [Route(Providers)]
    public class ProviderController: BaseController {

        private readonly ILogger _logger;
        private readonly ProviderService _service;

        public ProviderController(ILogger<ProviderController> logger, ProviderService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            if (result.IsSuccess())
            {
                var responseView = BuildResponseList(result.GetPayload());
                return Ok(responseView);
            }

            _logger.LogError("Unable to get the providers due: {0}", result.GetFailure());
            return ForFailure(result.GetFailure());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _service.GetById(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var provider = result.GetPayload();
            if (provider == null) return NotFound();
            
            var responseView = BuildResponse(provider);
            return Ok(responseView);
        }

        [HttpGet("country/{country}")]
        public IActionResult GetByCountry(string country)
        {
            var result = _service.GetByCountry(country);

            if (result.HasErrors()) ForFailure(result.GetFailure());

            var providers = BuildResponseList(result.GetPayload());
            return Ok(providers);
        }

        [HttpPost]
        public IActionResult New([FromBody] ProviderRequest providerRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var provider = new Provider() { Name = providerRequest.Name, Country = providerRequest.Country };
            var result = _service.New(provider);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var p = result.GetPayload();
            var responseView = BuildResponse(p);
            var uri = BuildResourceUri(Providers, p.Id);
            
            return Created(uri, responseView);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ProviderRequest providerRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var provider = new Provider()
            {
                Id = id,
                Name = providerRequest.Name,
                Country = providerRequest.Country
            };
            
            var result = _service.Update(provider);

            return result.HasErrors() ? ForFailure(result.GetFailure()) : Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _service.Delete(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var rows = result.GetPayload();
            if (rows > 0) return Accepted(rows);

            return NotFound();
        }

        private ICollection<ProviderResponse> BuildResponseList(IEnumerable<Provider> providers)
        {
            var responseList = (from provider in providers
                select new ProviderResponse(provider.Id, provider.Name, provider.Country)).ToList();

            return responseList;
        }

        private ProviderResponse BuildResponse(Provider provider)
        {
            return new ProviderResponse(provider.Id, provider.Name, provider.Country);
        }
    }
}