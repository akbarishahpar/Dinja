using Dinja.ServiceTypes;
using Dinja.Tests.Container.Services.Contracts;

namespace Dinja.Tests.Container.Services;

[Singleton(typeof(IContractedSingletonService))]
public class ContractedSingletonService : IContractedSingletonService
{
}