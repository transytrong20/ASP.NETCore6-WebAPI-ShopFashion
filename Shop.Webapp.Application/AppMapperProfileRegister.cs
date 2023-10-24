using AutoMapper;
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
            
        }

        private void EntityToDto()
        {

        }
    }
}
