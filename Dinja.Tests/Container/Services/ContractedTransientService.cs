using Dinja.ServiceTypes;
using Dinja.Tests.Container.Services.Contracts;

namespace Dinja.Tests.Container.Services;

[Transient(typeof(IContractedTransientService))]
public class ContractedTransientService : IContractedTransientService
{
}