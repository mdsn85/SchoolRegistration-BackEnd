using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SchoolRegistration.Controllers.Resources;
using SchoolRegistration.Models;

namespace SchoolRegistration.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserResource>().ReverseMap()
                .ForMember(v=>v.UserStatus, opt =>opt.MapFrom(vr =>vr.UserStatus.ToString()));
       


        }
    }
}
