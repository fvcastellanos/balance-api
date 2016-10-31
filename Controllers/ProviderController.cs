
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
                return internalServerError(result.getException());
            }
        }
    }
}