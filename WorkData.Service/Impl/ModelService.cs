// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：ModelService.cs
// 创建标识：吴来伟 2016-11-10
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
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;
using WorkData.Util.Entity;

namespace WorkData.Service.Impl
{
    public class ModelService: IModelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ModelDto Query(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Model>();
                var model = repository.Get(Convert.ToInt32(key));

                return AutoMapperHelper.Signle<Model, ModelDto>(model);
            }
        }

        /// <summary>
        /// 查询 +延迟加载
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        public ModelDto Query(object key, string includeName)
        {
            using (_unitOfWork)
            {
                var modelId = Convert.ToInt32(key);
                var repository = _unitOfWork.Repository<Model>();

                Expression<Func<Model, bool>> where = w => w.ModelId == modelId;
                var model = repository.Get(where, includeName);

                return AutoMapperHelper.Signle<Model, ModelDto>(model);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public IEnumerable<ModelDto> Page(PageEntity pageEntity)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Model>();
                Expression<Func<Model, bool>> where = w => w.ModelId!=0;

                var infoList = repository.Page(pageEntity, where);

                return AutoMapperHelper.Enumerable<Model, ModelDto>(infoList);
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModelDto> GetList()
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Model>();
                Expression<Func<Model, bool>> where = w => w.ModelId != 0;

                var infoList = repository.Query( where);

                return AutoMapperHelper.Enumerable<Model, ModelDto>(infoList);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        public void Add(ModelDto entity, int[] array)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<ModelDto, Model>(entity);
                var repository = _unitOfWork.Repository<Model>();
                var modelFieldRepository = _unitOfWork.Repository<ModelField>();

                foreach (var item in array)
                {
                    var role = new ModelField
                    {
                        ModelFieldId = item
                    };
                    modelFieldRepository.Attach(role);

                    info.ModelFields.Add(role);
                }


                repository.Add(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        public void Update(ModelDto entity, int[] array)
        {
            var info = AutoMapperHelper.Signle<ModelDto, Model>(entity);
            using (_unitOfWork)
            {
                Expression<Func<Model, bool>> where = w => w.ModelId == info.ModelId;
                var repository = _unitOfWork.Repository<Model>();
                var modelFieldRepository = _unitOfWork.Repository<ModelField>();

                var model = repository.Get(where, "ModelFields");
                repository.CurrentValue(model, info);

                var list = model.ModelFields.Where(modelField => !array.Contains(modelField.ModelFieldId)).ToList();
                list.ForEach(c => model.ModelFields.Remove(c));
                if (array != null)
                {
                    //2.0  求差集
                    var modelFieldIdArray = model.ModelFields.Select(x => x.ModelFieldId).ToArray();
                    var expectedList = array.Except(modelFieldIdArray);

                    foreach (var modelField in expectedList.Select(expected =>
                    new ModelField
                    {
                        ModelFieldId = expected
                    }))
                    {
                        modelFieldRepository.Attach(modelField);
                        model.ModelFields.Add(modelField);
                    }

                }

                repository.Update(model);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Model>();
                var info = repository.Get(Convert.ToInt32(key));

                info.Status = false;
                repository.Update(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 关键字查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ModelDto Query(string param)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Model>();

                Expression<Func<Model, bool>> where = w => w.Code == param;
                var model = repository.Get(where);

                return AutoMapperHelper.Signle<Model, ModelDto>(model);
            }
        }
    }
}