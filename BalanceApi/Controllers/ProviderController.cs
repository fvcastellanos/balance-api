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

namespace BalanceApi.Controllers {

    [Route("/api/provider")]
    public class ProviderController: BaseController {

        private readonly ILogger _logger;
        private readonly ProviderService _service;

        private readonly IModelValidator<Provider> _validator;

        public ProviderController(ILogger<ProviderController> logger, ProviderService service,
            IModelValidator<Provider> validator)
        {
            _service = service;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            if (result.IsSuccess())
            {
                var responseView = _buildResponseList(result.GetPayload());
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
            
            var responseView = _buildResponse(provider);
            return Ok(responseView);
        }

        [HttpGet("country/{country}")]
        public IActionResult GetByCountry(string country)
        {
            var result = _service.GetByCountry(country);

            if (result.HasErrors()) ForFailure(result.GetFailure());

            var providers = _buildResponseList(result.GetPayload());
            return Ok(providers);
        }

        [HttpPost]
        public IActionResult New([FromBody] AddProvider newProvider)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var provider = new Provider() { Name = newProvider.Name, Country = newProvider.Country };
            var result = _service.New(provider);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var p = result.GetPayload();
            if (p == null) return BadRequest();

            var responseView = _buildResponse(p);
            return Created("New provider", responseView);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateProvider updateProvider)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var provider = new Provider()
            {
                Id = updateProvider.Id,
                Name = updateProvider.Name,
                Country = updateProvider.Country
            };
            
            var r = _service.Update(provider);

            if (r.HasErrors()) return ForFailure(r.GetFailure());

            var p = r.GetPayload();
            if (p == null) return NotFound();

            var responseView = _buildResponse(p);
            
            return Accepted(responseView);
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

        private ICollection<ProviderResponse> _buildResponseList(IEnumerable<Provider> providers)
        {
            var responseList = (from provider in providers
                select new ProviderResponse(provider.Id, provider.Name, provider.Country)).ToList();

            return responseList;
        }

        private ProviderResponse _buildResponse(Provider provider)
        {
            return new ProviderResponse(provider.Id, provider.Name, provider.Country);
        }
    }
}