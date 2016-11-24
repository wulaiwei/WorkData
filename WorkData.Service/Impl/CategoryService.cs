// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：CategoryService.cs
// 创建标识：吴来伟 2016-11-15
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
using WorkData.Util.Entity;

namespace WorkData.Service.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CategoryDto Query(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Category>();
                var model = repository.Get(Convert.ToInt32(key));

                return AutoMapperHelper.Signle<Category, CategoryDto>(model);
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetList()
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Category>();
                var categoryList = new List<Category>();

                categoryList.AddRange(GetCategoryList(repository,15));

                return AutoMapperHelper.List<Category, CategoryDto>(categoryList);
            }
        }

        /// <summary>
        /// 查询 + 延迟加载
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        public CategoryDto Query(object key, string includeName)
        {
            using (_unitOfWork)
            {
                var infoKey = Convert.ToInt32(key);
                var repository = _unitOfWork.Repository<Category>();

                Expression<Func<Category, bool>> where = w => w.CategoryId == infoKey;
                var user = repository.Get(where, includeName);

                return AutoMapperHelper.Signle<Category, CategoryDto>(user);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="modelId"></param>
        public void Add(CategoryDto entity, int modelId)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<CategoryDto, Category>(entity);

                var repository = _unitOfWork.Repository<Category>();
                var modelRepository = _unitOfWork.Repository<Model>();

                if (modelId > 0)
                {
                    var model = new Model
                    {
                        ModelId = modelId
                    };

                    modelRepository.Attach(model);
                    info.Model = model;
                }
                repository.Add(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="modelId"></param>
        public void Update(CategoryDto entity, int modelId)
        {
            var info = AutoMapperHelper.Signle<CategoryDto, Category>(entity);
            using (_unitOfWork)
            {
                //Expression<Func<Category, bool>> where = w => w.CategoryId == info.CategoryId;

                var repository = _unitOfWork.Repository<Category>();
                var modelRepository = _unitOfWork.Repository<Model>();

                //var category = repository.Get(where, "Model");
                //repository.CurrentValue(category, info);


                if (modelId>0)
                {
                    //var model= modelRepository.Get(modelId);
                    var model = new Model
                    {
                        ModelId = modelId
                    };

                    modelRepository.Attach(model);
                    info.Model = model;
                }
            

                repository.Update(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Category>();

                var info = repository.Get(Convert.ToInt32(key));
                info.Status =false;
                repository.Update(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 代码查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CategoryDto Query(string param)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Category>();

                Expression<Func<Category, bool>> where = w => w.Code == param;
                var category = repository.Get(where);

                return AutoMapperHelper.Signle<Category, CategoryDto>(category);
            }
        }

        #region 私有
        /// <summary>
        /// 获取Resource列表
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private IEnumerable<Category> GetCategoryList(IRepository<Category> repository, int parentId)
        {
            var categoryList = new List<Category>();
            var @where = (Expression<Func<Category, bool>>)(w => w.ParentId == parentId);

            var infoList = repository.Query(@where).OrderBy(x => x.CategoryId).ToList();

            foreach (var info in infoList)
            {
                categoryList.Add(info);
                if (info.HasLevel)
                {
                    categoryList.AddRange(GetCategoryList(repository, info.CategoryId));
                }
            }

            return categoryList;

        } 
        #endregion
    }
}