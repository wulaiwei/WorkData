// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：ResourceService.cs
// 创建标识：吴来伟 2016-10-27
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WorkData.Code.AutoMapper;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;

namespace WorkData.Service.Impl
{
    public class ResourceService : IResourceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 递归拉取树结构
        /// </summary>
        /// <param name="isAll"></param>
        /// <param name="includeName"></param>
        /// <param name="parentId"></param>
        /// <param name="isLock"></param>
        /// <returns></returns>
        public IList<ResourceDto> GetSourceTree(bool isAll, string includeName, int parentId = 0, bool isLock = false)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Resource>();
                var resourceList = new List<Resource>();

                resourceList.AddRange(GetResourceList(repository, includeName, parentId, isLock, isAll));
                return AutoMapperHelper.List<Resource, ResourceDto>(resourceList);
            }
        }


        /// <summary>
        /// 查询所有列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ResourceDto> GetList()
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Resource>();
                var resourceList = repository.Query();

                return AutoMapperHelper.Enumerable<Resource, ResourceDto>(resourceList);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        public ResourceDto Query(object key, string includeName)
        {
            using (_unitOfWork)
            {
                var resourceId = Convert.ToInt32(key);
                var repository = _unitOfWork.Repository<Resource>();

                Expression<Func<Resource, bool>> where = w => w.ResourceId == resourceId;
                var resource = repository.Get(where, includeName);

                return AutoMapperHelper.Signle<Resource, ResourceDto>(resource);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ResourceDto Query(object key)
        {
            using (_unitOfWork)
            {
                var keyInfo = Convert.ToInt32(key);
                Expression<Func<Resource, bool>> where = w => w.ResourceId == keyInfo;

                var repository = _unitOfWork.Repository<Resource>();
                var resource = repository.Get(where);

                return AutoMapperHelper.Signle<Resource, ResourceDto>(resource);
            }
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        public void Add(ResourceDto entity, int[] array)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<ResourceDto, Resource>(entity);

                var repository = _unitOfWork.Repository<Resource>();

                foreach (var item in array)
                {
                    var privilege = new Privilege
                    {
                        ResourceId = info.ResourceId,
                        OperationId = item
                    };
                    info.Privileges.Add(privilege);
                }

                repository.Add(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Resource>();
                var info = repository.Get(Convert.ToInt32(key));
                info.IsLock = true;
                repository.Update(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        public void Update(ResourceDto entity, int[] array)
        {
            var info = AutoMapperHelper.Signle<ResourceDto, Resource>(entity);
            var resourceId = info.ResourceId;
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Resource>();
                var privilegeRepository = _unitOfWork.Repository<Privilege>();

                Expression<Func<Resource, bool>> where = w => w.ResourceId == resourceId;
                var resource = repository.Get(where, "Privileges");
                repository.CurrentValue(resource, info);

                var list = resource.Privileges.Where(r => !(array.Contains(r.OperationId)
                     && r.ResourceId == info.ResourceId)).ToList();
                if (array != null)
                {
                    //2.0  求差集
                    var operationIdArray = resource.Privileges.Select(x => x.OperationId).ToArray();
                    var expectedList = array.Except(operationIdArray);

                    foreach (var privilege in expectedList.Select(expected =>
                        new Privilege
                        {
                            ResourceId = resourceId,
                            OperationId = expected
                        }))
                    {
                        privilegeRepository.Add(privilege);
                    }
                    list.ForEach(t => privilegeRepository.Delete(t));
                }



                repository.Update(resource);

                _unitOfWork.Commit();
            }
        }

        #region 私有方法

        /// <summary>
        /// 获取Resource列表
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="parentId"></param>
        /// <param name="isLock"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        private IEnumerable<Resource> GetResourceList(
            IRepository<Resource> repository, string includeName, int parentId, bool isLock, bool isAll)
        {

            var resourceList = new List<Resource>();
            var @where = !isAll
                ? (Expression<Func<Resource, bool>>)(w => w.ParentId == parentId && w.IsLock == isLock)
                : (w => w.ParentId == parentId);

            var infoList = string.IsNullOrEmpty(includeName) ?
                repository.Query(@where).OrderBy(x => x.Sort).ToList() :
                repository.Query(@where, includeName).OrderBy(x => x.Sort).ToList();


            foreach (var info in infoList)
            {
                resourceList.Add(info);
                if (info.HasLevel)
                {
                    resourceList.AddRange(GetResourceList(repository, includeName, info.ResourceId, isLock, isAll));
                }
            }

            return resourceList;

        }
        #endregion
    }
}