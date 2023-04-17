using FluentNHibernate.Automapping;
using library_domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace library_Api;


public class MemberProfil : Profile
{
    public MemberProfil()
    {
        CreateMap<MemberDto, Student>();
        CreateMap<MemberDto, Resident>();
    }
}


