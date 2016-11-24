// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：ICategoryBll.cs
// 创建标识：吴来伟 2016-11-15
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
    public interface ICategoryBll
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<CategoryDto> GetList();

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        CategoryDto Query(SaveState saveState);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isLasy"></param>
        /// <returns></returns>
        CategoryDto Query(object key,bool isLasy= true);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        CategoryDto HttpGetSave(SaveState saveState);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="saveState"></param>
        /// <param name="modelId"></param>
        /// <returns></returns>
        void HttpPostSave(CategoryDto userDto, SaveState saveState, int modelId);


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="formTemplate"></param>
        /// <param name="formJson"></param>
        /// <param name="listTemplate"></param>
        /// <param name="listHead"></param>
        /// <param name="listJson"></param>
        /// <returns></returns>
        void AjaxSave(object key,string formTemplate,string formJson,string listTemplate,string listHead, string listJson);

        /// <summary>
        /// 验证 代码是否唯一
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ValidateEntity Validate(string param);
    }
}