// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：OperationService.cs
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
using WorkData.Util.Entity;

namespace WorkData.Service.Impl
{
    public class OperationService: IOperationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OperationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IList<OperationDto> GetList()
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Operation>();
                var infoList = repository.Query(); ;

                return AutoMapperHelper.List<Operation, OperationDto>(infoList.ToList());
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<OperationDto> GetList(bool status)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Operation>();
                Expression<Func<Operation, bool>> where = w => w.Status == status;

                var infoList = repository.Query(where);

                return AutoMapperHelper.List<Operation, OperationDto>(infoList.ToList());
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public IEnumerable<OperationDto> Page(PageEntity pageEntity)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Operation>();
                Expression<Func<Operation, bool>> where = w => w.Status;

                var infoList = repository.Page(pageEntity, where);

                return AutoMapperHelper.Enumerable<Operation, OperationDto>(infoList);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(OperationDto entity)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<OperationDto,Operation>(entity);
                var repository = _unitOfWork.Repository<Operation>();
                repository.Add(info);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 删除（逻辑删除）
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Operation>();
                var info = repository.Get(Convert.ToInt32(key));

                info.Status = false;
                repository.Update(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(OperationDto entity)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<OperationDto, Operation>(entity);

                var repository = _unitOfWork.Repository<Operation>();
                repository.Update(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public OperationDto Query(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Operation>();
                var operation = repository.Get(Convert.ToInt32(key));

                return AutoMapperHelper.Signle<Operation, OperationDto>(operation);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public OperationDto Query(string param)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Operation>();

                Expression<Func<Operation, bool>> where = w => w.Code == param;
                var operation = repository.Get(where);

                return AutoMapperHelper.Signle<Operation, OperationDto>(operation);
            }
        }
    }
}