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
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WorkData.Code.AutoMapper;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;
using WorkData.Util;

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
        /// 递归拉取树结构
        /// </summary>
        /// <param name="array"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<ResourceDto> GetSourceTree(IEnumerable<int> array,int parentId = 0)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Resource>();
                var resourceList = new List<Resource>();

                resourceList.AddRange(GetResourceList(repository, parentId, array));

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
        /// <param name="sourcePropertyName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResourceDto Query(string sourcePropertyName, object param)
        {
            using (_unitOfWork)
            {
                var where = ExpressionHelper.GenerateCondition<Resource>(sourcePropertyName, param);

                var repository = _unitOfWork.Repository<Resource>();
                var resource = repository.Query(where).Include("Resources.Operations").FirstOrDefault();

                return AutoMapperHelper.Signle<Resource, ResourceDto>(resource);
            }
        }

        /// <summary>
        /// 查询(特例)
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        public ResourceDto Query(string controllerName, string resourceUrl)
        {
            using (_unitOfWork)
            {
                Expression<Func<Resource, bool>> where = w => w.ControllerName == controllerName && w.ResourceUrl.Contains(resourceUrl);

                var repository = _unitOfWork.Repository<Resource>();
                var resource = repository.Query(where).Include("Operations").FirstOrDefault();

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

                if (array != null)
                {
                    foreach (var item in array)
                    {
                        var operation = new Operation
                        {
                            OperationId = item
                        };
                        info.Operations.Add(operation);
                    }
                }
                repository.Add(info);

                _unitOfWork.Commit();

                entity.ResourceId = info.ResourceId;
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
        public void Update(ResourceDto entity)
        {
            var info = AutoMapperHelper.Signle<ResourceDto, Resource>(entity);
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Resource>();
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
                var operationRepository = _unitOfWork.Repository<Operation>();

                Expression<Func<Resource, bool>> where = w => w.ResourceId == resourceId || w.Code == entity.Code;
                var resource = repository.Get(where, "Operations");
                if (info.ResourceId <= 0)
                {
                    info.ResourceId = resource.ResourceId;
                }
                repository.CurrentValue(resource, info);

                var list = resource.Operations.Where(r => array != null
                    && !array.Contains(r.OperationId)).ToList();
                list.ForEach(c => resource.Operations.Remove(c));
                if (array != null)
                {
                    //2.0  求差集
                    var operationIdArray = resource.Operations.Select(x => x.OperationId).ToArray();
                    var expectedList = array.Except(operationIdArray);

                    foreach (var operation in expectedList.Select(expected =>
                    new Operation
                    {
                        OperationId = expected
                    }))
                    {
                        operationRepository.Attach(operation);
                        resource.Operations.Add(operation);
                    }

                }

                repository.Update(resource);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 代码查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResourceDto Query(string param)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Resource>();

                Expression<Func<Resource, bool>> where = w => w.Code == param;
                var resource = repository.Get(where);

                return AutoMapperHelper.Signle<Resource, ResourceDto>(resource);
            }
        }

        #region 私有方法

        /// <summary>
        /// 获取Resource列表
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="includeName"></param>
        /// <param name="parentId"></param>
        /// <param name="isLock"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        private IEnumerable<Resource> GetResourceList(
            IRepository<Resource> repository, string includeName, int parentId, bool isLock, bool isAll)
        {
            var resourceList = new List<Resource>();
        
            var where = !isAll
                ? (Expression<Func<Resource, bool>>)(w => w.ParentId == parentId && w.IsLock == isLock
                )
                : (w => w.ParentId == parentId);

            var infoList = string.IsNullOrEmpty(includeName) ?
                repository.Query(where).OrderBy(x => x.Sort).ToList() :
                repository.Query(where, includeName).OrderBy(x => x.Sort).ToList();


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

        /// <summary>
        /// 获取Resource列表
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="array"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private IEnumerable<Resource> GetResourceList(
            IRepository<Resource> repository,int parentId,IEnumerable<int> array)
        {
            var resourceList = new List<Resource>();
            Expression<Func<Resource, bool>> where = w => w.ParentId == parentId && w.IsLock == false
                  && w.Roles.Any(i => array.Any(r => i.RoleId == r));

            var infoList = repository.Query(where).OrderBy(x => x.Sort).ToList();

            foreach (var info in infoList)
            {
                resourceList.Add(info);
                if (info.HasLevel)
                {

                    resourceList.AddRange(GetResourceList(repository,info.ResourceId, array));
                }
            }

            return resourceList;

        }
        #endregion
    }
}