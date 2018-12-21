using AutoMapper;
using System;

namespace Lxsh.Project.AutoMapper.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{ DTOClassInfo().ClassName}   Hello World!");
            Console.WriteLine($"{ DTOUser().Name}   Hello World!");
         
           // Console.WriteLine($"{ DTOClassInfo().ClassName}Hello World!");
        }
        static DTOUserInfo DTOUser()
        {      
            EntityUser user = new EntityUser() {
                Name = "lxsh",
                Age = 18,
                ClassID = 1,
                Sex = 1    
            };
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EntityUser, DTOUserInfo>());
            var mapper = config.CreateMapper();
            return mapper.Map<DTOUserInfo>(user);
        }
     

        static DTOClass DTOClassInfo()
        {
            EntityClass entityClass = new EntityClass()
            {
                ClassID = 1,
                Name="一班"
            };
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EntityClass, DTOClass>().ForMember(d=>d.ClassName,opt=>opt.MapFrom(s=>s.Name)));
            var mapper = config.CreateMapper();
            return mapper.Map<DTOClass>(entityClass);
        }
    }
}
