// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：ModelFieldService.cs
// 创建标识：吴来伟 2016-11-14
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WorkData.Code.AutoMapper;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;
using WorkData.Util.Entity;

namespace WorkData.Service.Impl
{
    public class ModelFieldService : IModelFieldService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelFieldService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ModelFieldDto Query(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<ModelField>();
                var model = repository.Get(Convert.ToInt32(key));

                return AutoMapperHelper.Signle<ModelField, ModelFieldDto>(model);
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModelFieldDto> GetList()
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<ModelField>();

                Expression<Func<ModelField, bool>> where = w => w.Status;

                var infoList = repository.Query(where);

                return AutoMapperHelper.Enumerable<ModelField, ModelFieldDto>(infoList);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public IEnumerable<ModelFieldDto> Page(PageEntity pageEntity)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<ModelField>();

                Expression<Func<ModelField, bool>> where = w => w.ModelFieldId != 0;

                var infoList = repository.Page(pageEntity, where);

                return AutoMapperHelper.Enumerable<ModelField, ModelFieldDto>(infoList);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(ModelFieldDto entity)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<ModelFieldDto, ModelField>(entity);
                var repository = _unitOfWork.Repository<ModelField>();
                repository.Add(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(ModelFieldDto entity)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<ModelFieldDto, ModelField>(entity);
                var repository = _unitOfWork.Repository<ModelField>();
                repository.Update(info);

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
                var repository = _unitOfWork.Repository<ModelField>();
                var info = repository.Get(Convert.ToInt32(key));

                info.Status = false;
                repository.Update(info);

                _unitOfWork.Commit();
            }
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ModelFieldDto Query(string param)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<ModelField>();

                Expression<Func<ModelField, bool>> where = w => w.Code == param;
                var operation = repository.Get(where);

                return AutoMapperHelper.Signle<ModelField, ModelFieldDto>(operation);
            }
        }
    }
}