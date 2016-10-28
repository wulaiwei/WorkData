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
    public class OperationService: IOperationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Operation> _operationRepository;

        public OperationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _operationRepository = unitOfWork.Repository<Operation>();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<OperationDto> GetList()
        {
            var infoList = _operationRepository.Query();

            return AutoMapperHelper.Queryable<Operation,OperationDto>(infoList);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public IQueryable<OperationDto> GetList(int[] array)
        {
            Expression<Func<Operation, bool>> where = w => array.Contains(w.OperationId);
            var infoList = _operationRepository.Query(where);

            return AutoMapperHelper.Queryable<Operation, OperationDto>(infoList);
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
                _operationRepository.Add(info);

                _unitOfWork.Commit();

            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(OperationDto entity)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<OperationDto, Operation>(entity);
                _operationRepository.Delete(info);

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
                _operationRepository.Update(info);

                _unitOfWork.Commit();
            }
        }
    }
}