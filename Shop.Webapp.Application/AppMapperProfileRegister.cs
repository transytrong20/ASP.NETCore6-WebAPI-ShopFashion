﻿using AutoMapper;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Domain;

namespace Shop.Webapp.Application
{
    public class AppMapperProfileRegister : Profile
    {
        public AppMapperProfileRegister()
        {
            ModelToEntity();
            EntityToDto();
        }

        private void ModelToEntity()
        {
            CreateMap<AddRoleModel, Role>();
        }

        private void EntityToDto()
        {
            CreateMap<Role, RoleDto>();
        }
    }
}