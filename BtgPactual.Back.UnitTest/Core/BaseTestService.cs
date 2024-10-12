using AutoMapper;
using BtgPactual.Back.Api.Mappings;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace BtgPactual.Back.UnitTest.Core
{
    public class BaseTestService<T> where T : class
    {
        protected IMapper mapper;
        protected readonly ILogger<T> logger = new NullLogger<T>();
        public BaseTestService()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ConstructServicesUsing(type =>
                {
                    return Activator.CreateInstance(type);
                });
                cfg.AddProfile<BtgPactualMappingProfile>();

            });
            mapper = config.CreateMapper();
        }
    }
}
