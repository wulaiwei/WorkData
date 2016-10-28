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
using EFModel.Entity;
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
        private readonly IRepository<Resource> _resourceRepository;

        public ResourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _resourceRepository = unitOfWork.Repository<Resource>();
        }

        /// <summary>
        /// 递归拉取树结构
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<ResourceDto> GetSourceTree(int parentId = 0)
        {
            using (_unitOfWork)
            {
                var resourceList = new List<Resource>();

                resourceList.AddRange(GetResourceList(parentId));
                return AutoMapperHelper.List<Resource, ResourceDto>(resourceList);
            }

        }

        /// <summary>
        /// 获取Resource列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private IEnumerable<Resource> GetResourceList(int parentId)
        {
            var resourceList = new List<Resource>();

            Expression<Func<Resource, bool>> where = w => w.ParentId == parentId;

            var infoList = _resourceRepository.Query(where);

            foreach (var info in infoList)
            {
                resourceList.Add(info);
                if (info.HasLevel)
                {
                    resourceList.AddRange(GetResourceList(info.ResourceId));
                }
            }

            return resourceList;
        }

        /// <summary>
        /// 查询所有列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<ResourceDto> GetList()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 条件查询列表
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public IQueryable<ResourceDto> GetList(int[] array)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(ResourceDto entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(ResourceDto entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(ResourceDto entity)
        {
            throw new System.NotImplementedException();
        }
    }
}