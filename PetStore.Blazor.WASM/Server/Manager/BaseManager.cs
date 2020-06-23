using AutoMapper;
using System;

namespace PetStore.Blazor.WASM.Server.Manager
{
    public class BaseManager
    {
        protected readonly IMapper _mapper;

        protected BaseManager(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
