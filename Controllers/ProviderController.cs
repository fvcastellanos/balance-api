
using Microsoft.AspNetCore.Mvc;
using BalanceApi.Services;
using Microsoft.Extensions.Logging;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;
using System.Collections.Generic;

namespace BalanceApi.Controllers {

    [Route("/api/provider")]
    public class ProviderController: BaseController {

        private ILogger logger;
        private ProviderService Service;

        public ProviderController(ILogger<ProviderController> logger, ProviderService Service) {
            this.Service = Service;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll() {
            Result result = Service.GetAll();
            if(result.isSuccess()) {
                return Ok(result.getObject<List<Provider>>());
            } else {
                logger.LogError("Unable to get the providers due: {0}", result.getException());
                return ForException(result.getException());
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            Result result = Service.GetById(id);
            if(result.isSuccess()) {
                Provider provider = result.getObject<Provider>();
                if(provider != null) {
                    return Ok(provider);
                } else {
                    return NotFound();
                }
            } else {
                return ForException(result.getException());
            }
        }

        [HttpGet("/country/{country}")]
        public IActionResult GetByCountry(string country) {
            Result result = Service.GetByCountry(country);
            if(result.isSuccess()) {
                return Ok(result.getObject<List<Provider>>());
            } else {
                return ForException(result.getException());
            }
        }

        [HttpPost]
        public IActionResult New([FromBody] Provider provider) {
            Result result = Service.New(provider);
            if(result.isSuccess()) {
                Provider p = result.getObject<Provider>();
                if(p != null ) {
                    return Created("New provider", p);
                } else {
                    return BadRequest();
                }
            } else {
                return ForException(result.getException());
            }
        }
    }
}