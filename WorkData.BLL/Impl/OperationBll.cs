// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：OperationBll.cs
// 创建标识：吴来伟 2016-10-31
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Service.Interface;
using WorkData.Util;
using WorkData.Util.Entity;
using WorkData.Util.Enum;

namespace WorkData.BLL.Impl
{
    public class OperationBll:IOperationBll
    {
        private readonly IOperationService _operationService;
        public OperationBll(IOperationService operationService)
        {
            _operationService = operationService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OperationDto> GetList()
        {
            return _operationService.GetList(true);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OperationDto> Page(PageEntity pageEntity)
        {
            return _operationService.Page(pageEntity);
        }

        /// <summary>
        /// Get请求处理
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public OperationDto HttpGetSave(SaveState saveState)
        {
            var operationDto = new OperationDto();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    break;
                case OperationState.Update:
                    operationDto = _operationService.Query(saveState.Key);
                    break;
                case OperationState.Remove:
                    _operationService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }
            return operationDto;
        }

        /// <summary>
        /// Post请求处理
        /// </summary>
        /// <param name="operationDto"></param>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public void HttpPostSave(OperationDto operationDto, SaveState saveState)
        {
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    _operationService.Add(operationDto);
                    break;
                case OperationState.Update:
                    _operationService.Update(operationDto);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public OperationDto Query(SaveState saveState)
        {
            var operationDto = new OperationDto();
            return saveState.OperationState == OperationState.Add ? operationDto : _operationService.Query(saveState.Key);
        }

        /// <summary>
        /// 验证名称是否唯一
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ValidateEntity Validate(string param)
        {
            var validateEntity = new ValidateEntity();
            if (string.IsNullOrEmpty(param))
            {
                validateEntity.Info = "代码不可为空";
                validateEntity.Status = "n";
                return validateEntity;
            }

            var operationDto= _operationService.Query(param);
            if (operationDto == null)
            {
                validateEntity.Info = "该代码可使用！";
                validateEntity.Status = "y";
            }
            else
            {
                validateEntity.Info = "该代码已被占用，请更换！";
                validateEntity.Status = "n";
            }
            return validateEntity;
        }
    }
}
