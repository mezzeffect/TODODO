using AutoMapper;
using TodoSampleBackend.Business;
using TodoSampleBackend.DataObjects;
using TodoSampleBackend.Models;

namespace TodoSampleBackend
{
    public static class AutoMapperConfigurator
    {
        public static void ConfigureAll()
        {
            MapBusinessObjectsToDataObjects();
            MapServiceModelsToBusinessObjectModel();
        }

        private static void MapBusinessObjectsToDataObjects()
        {
            Mapper.CreateMap<User, UserBusinessObject>();
        }

        private static void MapServiceModelsToBusinessObjectModel()
        {
            Mapper.CreateMap<LoginRequest, LoginBusinessObject>();
            Mapper.CreateMap<UserBusinessObject, UserModel>();
        }
    }
}