// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：ModelBll.cs
// 创建标识：吴来伟 2016-11-10
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
    public class ModelBll: IModelBll
    {
        private readonly IModelService _modelService;
        public ModelBll(IModelService modelService)
        {
            _modelService = modelService;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public IEnumerable<ModelDto> Page(PageEntity pageEntity)
        {
            return _modelService.Page(pageEntity);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModelDto> GetList()
        {
            return _modelService.GetList();
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public ModelDto HttpGetSave(SaveState saveState)
        {
            var modelDto = new ModelDto();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    break;
                case OperationState.Update:
                    modelDto = _modelService.Query(saveState.Key, "ModelFields");
                    break;
                case OperationState.Remove:
                    _modelService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }
            return modelDto;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="modelDto"></param>
        /// <param name="saveState"></param>
        /// <param name="array"></param>
        public void HttpPostSave(ModelDto modelDto, SaveState saveState, int[] array)
        {
            modelDto.Description = modelDto.Description.Trim();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    _modelService.Add(modelDto,array);
                    break;
                case OperationState.Update:
                    _modelService.Update(modelDto, array);
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
        public ModelDto Query(SaveState saveState)
        {
            var modelDto = new ModelDto();
            return saveState.OperationState == OperationState.Add ? modelDto : 
                _modelService.Query(saveState.Key);
        }

        /// <summary>
        /// 加载指定内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ModelDto Query(object key)
        {
            return   _modelService.Query(key, "ModelFields");
        }

        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ValidateEntity Validate(string param)
        {
            var validateEntity = new ValidateEntity();
            if (string.IsNullOrEmpty(param))
            {
                validateEntity.Info = "模型代码不可为空";
                validateEntity.Status = "n";
                return validateEntity;
            }

            var modelDto = _modelService.Query(param);
            if (modelDto == null)
            {
                validateEntity.Info = "该模型代码可使用！";
                validateEntity.Status = "y";
            }
            else
            {
                validateEntity.Info = "该模型代码已被占用，请更换！";
                validateEntity.Status = "n";
            }
            return validateEntity;
        }
    }
}