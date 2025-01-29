using System;
using _net.DTOs;
using _net.Models;
using AutoMapper;

namespace _net.Profiles;

public class MyProfiles : Profile
{
    public MyProfiles() {
        CreateMap<UserModel, RegisterDTO>().ReverseMap();
        CreateMap<UserModel, LoginDTO>().ReverseMap();
        CreateMap<UserModel, AuthenticatedDTO>().ReverseMap();
        CreateMap<LoginDTO, RegisterDTO>().ReverseMap();

        CreateMap<ProductModel, ProductDTO>().ReverseMap();

        CreateMap<CreateAddressDTO, AddressModel>().ReverseMap();

        CreateMap<AddressDTO, AddressModel>().ReverseMap();
    }
}
