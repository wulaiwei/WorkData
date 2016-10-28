// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：IOperationService.cs
// 创建标识：吴来伟 2016-10-27
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using WorkData.Dto.Entity;

namespace WorkData.Service.Interface
{
    public interface IOperationService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IQueryable<OperationDto> GetList();

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        IQueryable<OperationDto> GetList(int[] array);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        void Add(OperationDto entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        void Remove(OperationDto entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(OperationDto entity);
    }
}