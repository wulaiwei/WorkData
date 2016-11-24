// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：CategoryBll.cs
// 创建标识：吴来伟 2016-11-15
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
    public class CategoryBll: ICategoryBll
    {
        private readonly ICategoryService _categoryService;
        private readonly IResourceService _resourceService;

        public CategoryBll(ICategoryService categoryService, IResourceService resourceService)
        {
            _categoryService = categoryService;
            _resourceService = resourceService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetList()
        {
            return _categoryService.GetList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public CategoryDto Query(SaveState saveState)
        {
            var categoryDto = new CategoryDto();
            return saveState.OperationState == OperationState.Add ?
                categoryDto :
                _categoryService.Query(saveState.Key, "Model");
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isLasy"></param>
        /// <returns></returns>
        public CategoryDto Query(object key,bool isLasy=true)
        {
            return isLasy ? _categoryService.Query(key, "Model") 
                : _categoryService.Query(key);
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public CategoryDto HttpGetSave(SaveState saveState)
        {
            var categoryDto = new CategoryDto();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    break;
                case OperationState.Update:
                    categoryDto = _categoryService.Query(saveState.Key, "Model");
                    break;
                case OperationState.Remove:
                    _categoryService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }
            return categoryDto;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <param name="saveState"></param>
        /// <param name="modelId"></param>
        public void HttpPostSave(CategoryDto categoryDto, SaveState saveState, int modelId)
        {
            var parentCategory = _categoryService.Query(categoryDto.ParentId);

            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    if (parentCategory != null) categoryDto.Layer = parentCategory.Layer + 1;
                    _categoryService.Add(categoryDto, modelId);
                    break;
                case OperationState.Update:
                    _categoryService.Update(categoryDto, modelId);
                    break;
                default:
                    break;
            }
            //同步数据
            DataSynchronization(categoryDto, parentCategory, saveState);
        }

        /// <summary>
        /// Ajax保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="formTemplate"></param>
        /// <param name="formJson"></param>
        /// <param name="listTemplate"></param>
        /// <param name="listHead"></param>
        /// <param name="listJson"></param>
        public void AjaxSave(object key, string formTemplate, string formJson, string listTemplate, string listHead, string listJson)
        {
            var info = _categoryService.Query(key);
            if (formTemplate != null) info.FormTemplate = formTemplate;
            if (formJson != null) info.FormJson = formJson;
            if (listTemplate != null) info.ListTempalte = listTemplate;
            if (listHead != null) info.ListHead = listHead;
            if (listJson != null) info.ListJson = listJson;

            _categoryService.Update(info, 0);
        }

        /// <summary>
        /// 验证代码唯一性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ValidateEntity Validate(string param)
        {
            
            var validateEntity = new ValidateEntity();
            if (string.IsNullOrEmpty(param))
            {
                validateEntity.Info = "栏目代码不可为空";
                validateEntity.Status = "n";
                return validateEntity;
            }

            var modelDto = _categoryService.Query(param);
            if (modelDto == null)
            {
                validateEntity.Info = "该栏目代码可使用！";
                validateEntity.Status = "y";
            }
            else
            {
                validateEntity.Info = "该栏目代码已被占用，请更换！";
                validateEntity.Status = "n";
            }
            return validateEntity;
        }

        /// <summary>
        /// 数据同步 (Category与Resource资源同步)
        /// </summary>
        private void DataSynchronization(CategoryDto categoryDto, CategoryDto parentCategoryDto, SaveState saveState)
        {
            var code = parentCategoryDto == null ? "zhandianguanli" : parentCategoryDto.Code;
            var parentResourceDto = _resourceService.Query(code);
            var resourceDto = new ResourceDto
            {
                ParentId = parentResourceDto.ResourceId,
                ResourceName = categoryDto.Name,
                IsLock = !categoryDto.Status,
                Layer= parentResourceDto.Layer+1,
                Code= categoryDto.Code
            };

            if (!categoryDto.HasLevel)
            {
                resourceDto.ResourceUrl = $"/Admin/Content/Index?Key={categoryDto.CategoryId}";
            }

            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    _resourceService.Add(resourceDto,null);
                    break;
                case OperationState.Update:
                    _resourceService.Update(resourceDto, null);
                    break;
                default:
                    break;
            }

            parentResourceDto.HasLevel = true;
            _resourceService.Update(parentResourceDto, null);
        }

    }
}