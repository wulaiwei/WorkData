// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：IModelFieldBll.cs
// 创建标识：吴来伟 2016-11-14
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
    public interface IModelFieldBll
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModelFieldDto> Page(PageEntity pageEntity);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        ModelFieldDto HttpGetSave(SaveState saveState);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="modelFieldDto"></param>
        /// <param name="saveState"></param>
        /// <returns></returns>
        void HttpPostSave(ModelFieldDto modelFieldDto, SaveState saveState);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        ModelFieldDto Query(SaveState saveState);

        /// <summary>
        /// 验证 名称是否唯一
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ValidateEntity Validate(string param);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModelFieldDto> GetList();
    }
}