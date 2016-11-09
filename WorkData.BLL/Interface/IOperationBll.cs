// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：IOperationBll.cs
// 创建标识：吴来伟 2016-10-31
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.Util.Entity;

namespace WorkData.BLL.Interface
{
    public interface IOperationBll
    {

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<OperationDto> GetList();

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<OperationDto> Page(PageEntity pageEntity);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        OperationDto HttpGetSave(SaveState saveState);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="operationDto"></param>
        /// <param name="saveState"></param>
        /// <returns></returns>
        void HttpPostSave(OperationDto operationDto, SaveState saveState);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        OperationDto Query(SaveState saveState);

        /// <summary>
        /// 验证 名称是否唯一
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ValidateEntity Validate(string param);
    }
}