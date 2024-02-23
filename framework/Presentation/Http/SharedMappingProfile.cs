using AutoMapper;
using Octopus.Core.Contract.Queries;

namespace Octopus.Presentation.Http;

public class SharedMappingProfile : Profile
{
    public SharedMappingProfile()
    {
        CreateMap(typeof(Pagination<>), typeof(PaginationViewModel<>));
    }
}