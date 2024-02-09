using AutoMapper;
using Resource.Repository.Model;
using Resource.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Business.Profiles
{
    public sealed class AssemblyMarker
    {
        public AssemblyMarker() { }
    }

    public class ResourceProfiles : Profile
    {
        public ResourceProfiles()
        {
            CreateMap<ResourceDb, ResourceDTO>();
            CreateMap<ResourceDTO, ResourceDb>();
        }   
    }
}
