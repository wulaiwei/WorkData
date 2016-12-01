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
                //RBAC
                cfg.AddProfile<RoleDtoProfile>();
                cfg.AddProfile<UserDtoProfile>();
                cfg.AddProfile<OperationDtoProfile>();
                //cfg.AddProfile<PrivilegeDtoProfile>();
                cfg.AddProfile<ResourceDtoProfile>();

                //栏目  连接RBAC和  DynamicForm
                cfg.AddProfile<CategoryDtoProfile>();

                //DynamicForm
                cfg.AddProfile<ModelDtoProfile>();
                cfg.AddProfile<ModelFieldDtoProfile>();
                cfg.AddProfile<ContentDtoProfile>();

                cfg.AddProfile<ContentDescriptionFieldDtoProfile>();
                cfg.AddProfile<ContentDoubleFieldDtoProfile>();
                cfg.AddProfile<ContentIntFieldDtoProfile>();
                cfg.AddProfile<ContentStringFieldDtoProfile>();
                cfg.AddProfile<ContentTextFieldDtoProfile>();
                cfg.AddProfile<ContentTimeFieldDtoProfile>();


            });
        }
    }
}