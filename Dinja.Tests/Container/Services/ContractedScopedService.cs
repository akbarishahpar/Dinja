using Dinja.ServiceTypes;
using Dinja.Tests.Container.Services.Contracts;

namespace Dinja.Tests.Container.Services;

[Scoped(typeof(IContractedScopedService))]
public class ContractedScopedService
{
}