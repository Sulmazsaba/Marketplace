using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Contracts;
using Microsoft.AspNetCore.Mvc;
using static Marketplace.Contracts.ClassifiedAds;
namespace Marketplace.Api
{
    [Route("/ad")]
    public class ClassifiedAdsCommandsApi : Controller
    {
        private readonly ClassifiedAdsApplicationService _applicationService;
        private readonly IHandleCommand<V1.Create> _createAdCommandHandler;

        public ClassifiedAdsCommandsApi(ClassifiedAdsApplicationService applicationService, IHandleCommand<V1.Create> createAdCommandHandler)
        {
            _applicationService = applicationService;
            _createAdCommandHandler = createAdCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Post(V1.Create request)
        {
            //return _createAdCommandHandler.Handle(request);
            await _applicationService.Handle(request);
            return Ok();
        }
    }
}
