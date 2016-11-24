// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：ModelFieldBll.cs
// 创建标识：吴来伟 2016-11-14
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Service.Interface;
using WorkData.Util.Entity;
using WorkData.Util.Enum;

namespace WorkData.BLL.Impl
{
    public class ModelFieldBll: IModelFieldBll
    {
        private readonly IModelFieldService _modelFieldService;
        public ModelFieldBll(IModelFieldService modelFieldService)
        {
            _modelFieldService = modelFieldService;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public IEnumerable<ModelFieldDto> Page(PageEntity pageEntity)
        {
            return _modelFieldService.Page(pageEntity);
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public ModelFieldDto HttpGetSave(SaveState saveState)
        {
            var modelFieldDto = new ModelFieldDto();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    break;
                case OperationState.Update:
                    modelFieldDto = _modelFieldService.Query(saveState.Key);
                    break;
                case OperationState.Remove:
                    _modelFieldService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }
            return modelFieldDto;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="modelFieldDto"></param>
        /// <param name="saveState"></param>
        public void HttpPostSave(ModelFieldDto modelFieldDto, SaveState saveState)
        {
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    _modelFieldService.Add(modelFieldDto);
                    break;
                case OperationState.Update:
                    _modelFieldService.Update(modelFieldDto);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public ModelFieldDto Query(SaveState saveState)
        {
            var modelFieldDto = new ModelFieldDto();
            return saveState.OperationState == OperationState.Add ? modelFieldDto :
                _modelFieldService.Query(saveState.Key);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ValidateEntity Validate(string param)
        {
            var validateEntity = new ValidateEntity();
            if (string.IsNullOrEmpty(param))
            {
                validateEntity.Info = "字段代码不可为空";
                validateEntity.Status = "n";
                return validateEntity;
            }

            var modelFieldDto = _modelFieldService.Query(param);
            if (modelFieldDto == null)
            {
                validateEntity.Info = "该字段代码可使用！";
                validateEntity.Status = "y";
            }
            else
            {
                validateEntity.Info = "该字段代码已被占用，请更换！";
                validateEntity.Status = "n";
            }
            return validateEntity;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModelFieldDto> GetList()
        {
            return _modelFieldService.GetList();
        }
    }
}