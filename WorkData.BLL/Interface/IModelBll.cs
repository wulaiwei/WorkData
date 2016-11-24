// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：IModelBll.cs
// 创建标识：吴来伟 2016-11-10
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;
using WorkData.Dto.Entity;
using WorkData.Util.Entity;

namespace WorkData.BLL.Interface
{
    public interface IModelBll
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModelDto> Page(PageEntity pageEntity);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModelDto> GetList();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        ModelDto HttpGetSave(SaveState saveState);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="modelDto"></param>
        /// <param name="saveState"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        void HttpPostSave(ModelDto modelDto, SaveState saveState, int[] array);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        ModelDto Query(SaveState saveState);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ModelDto Query(object key);

        /// <summary>
        /// 验证 名称是否唯一
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ValidateEntity Validate(string param);
    }
}