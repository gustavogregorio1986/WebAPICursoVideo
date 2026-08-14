using AutoMapper;
using WebAPICursoVideo.Dto;
using WebAPICursoVideo.Models;

namespace WebAPICursoVideo.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<UsuarioCriacaoDto, UsuarioModel>().ReverseMap();
            CreateMap<UsuarioEdicaoDto, UsuarioModel>().ReverseMap();
        }
    }
}
