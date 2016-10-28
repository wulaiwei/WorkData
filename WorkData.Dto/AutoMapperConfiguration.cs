using AutoMapper;
using WorkData.Dto.Profiles;

namespace WorkData.Dto
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<RoleDtoProfile>();
                cfg.AddProfile<UserDtoProfile>();
                cfg.AddProfile<OperationDtoProfile>();
                cfg.AddProfile<PrivilegeDtoProfile>();
                cfg.AddProfile<ResourceDtoProfile>();
            });
        }
    }
}