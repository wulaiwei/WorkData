// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：ContentService.cs
// 创建标识：吴来伟 2016-11-17
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WorkData.Code.AutoMapper;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;
using WorkData.Util.Entity;
using System.Linq;

namespace WorkData.Service.Impl
{
    public class ContentService : IContentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ContentDto Query(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Content>();
                var content = repository.Get(Convert.ToInt32(key));
                var modelRepository = _unitOfWork.Repository<Model>();

                Expression<Func<Model, bool>> where = w => w.ModelId == content.ModelId;
                var model = modelRepository.Get(where, "ModelFields");

                var contentDto = AutoMapperHelper.Signle<Content, ContentDto>(content);
                //获取字典集
                var dictionary = BuildDictionary(_unitOfWork, model, Convert.ToInt32(key));

                dynamic obj = new ContentValue(dictionary);
                contentDto.ContentValue = obj;
                return contentDto;
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="categoryId"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public IEnumerable<ContentDto> Page(PageEntity pageEntity, int categoryId, dynamic[] arr)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Content>();
                Expression<Func<Content, bool>> where = w => w.CategoryId == categoryId;
                var infoList = repository.Page(pageEntity, where).ToList();
                var data = AutoMapperHelper.Enumerable<Content, ContentDto>(infoList);

                var contentDtos = data as ContentDto[] ?? data.ToArray();
                var firstOrDefault = contentDtos.FirstOrDefault();
                if (firstOrDefault == null) return contentDtos;
                {
                    foreach (var item in contentDtos)
                    {
                        //获取字典集
                        var dictionary = BuildDictionary(_unitOfWork,item.ContentId,arr);
                        dynamic obj = new ContentValue(dictionary);
                        item.ContentValue = obj;
                    }
                 }
                return contentDtos;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dictionary"></param>
        public void Add(ContentDto entity, Dictionary<string, object> dictionary)
        {
            var content = AutoMapperHelper.Signle<ContentDto, Content>(entity);
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Content>();
                var modelRepository = _unitOfWork.Repository<Model>();

                Expression<Func<Model, bool>> where = w => w.ModelId == entity.ModelId;
                var model = modelRepository.Get(where, "ModelFields");

                BuildContentField(content, model, dictionary);

                repository.Add(content);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dictionary"></param>
        public void Update(ContentDto entity, Dictionary<string, object> dictionary)
        {
            var content = AutoMapperHelper.Signle<ContentDto, Content>(entity);
            using (_unitOfWork)
            {
                var modelRepository = _unitOfWork.Repository<Model>();
                Expression<Func<Model, bool>> where = w => w.ModelId == entity.ModelId;
                var model = modelRepository.Get(where, "ModelFields");

                UpdateContentField(_unitOfWork, content, model, dictionary);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Content>();

                var content = repository.Get(Convert.ToInt32(key));

                repository.Delete(content);

                _unitOfWork.Commit();
            }
        }

        #region 私有

        /// <summary>
        /// 构建字典
        /// </summary>
        /// <param name="unitwork"></param>
        /// <param name="key"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        private Dictionary<string, object> BuildDictionary(IUnitOfWork unitwork, int key, dynamic[] arr)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var item in arr)
            {
                string code = item.Value;
                var repository = unitwork.Repository<ModelField>();
                Expression<Func<ModelField, bool>> where = w => w.Code == code;
                var modelField = repository.Query(where).FirstOrDefault();
                if (modelField == null) continue;

                switch (modelField.FieldType)
                {
                    case 0:
                        var intRepository = unitwork.Repository<ContentIntField>();
                        Expression<Func<ContentIntField, bool>> intWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var intInfo = intRepository.Query(intWhere).FirstOrDefault();
                        if (intInfo != null)
                        {
                            var info = intInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 1:
                        var doubleRepository = unitwork.Repository<ContentDoubleField>();
                        Expression<Func<ContentDoubleField, bool>> doubleWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var doubleInfo = doubleRepository.Query(doubleWhere).FirstOrDefault();
                        if (doubleInfo != null)
                        {
                            var info = doubleInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 2:
                        var stringRepository = unitwork.Repository<ContentStringField>();
                        Expression<Func<ContentStringField, bool>> stringWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var stringInfo = stringRepository.Query(stringWhere).FirstOrDefault();
                        if (stringInfo != null)
                        {
                            var info = stringInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 3:
                        var descRepository = unitwork.Repository<ContentDescriptionField>();
                        Expression<Func<ContentDescriptionField, bool>> descWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var descInfo = descRepository.Query(descWhere).FirstOrDefault();
                        if (descInfo != null)
                        {
                            var info = descInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 4:
                        var textRepository = unitwork.Repository<ContentTextField>();
                        Expression<Func<ContentTextField, bool>> textWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var textInfo = textRepository.Query(textWhere).FirstOrDefault();
                        if (textInfo != null)
                        {
                            var info = textInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 5:
                        var timeRepository = unitwork.Repository<ContentTimeField>();
                        Expression<Func<ContentTimeField, bool>> timeWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var timeInfo = timeRepository.Query(timeWhere).FirstOrDefault();
                        if (timeInfo != null)
                        {
                            var info = timeInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                }

            }
            return dictionary;
        }

        #region 公用方法暂注释
        ///// <summary>
        ///// 公用方法提取
        ///// </summary>
        ///// <param name="unitwork"></param>
        ///// <param name="modelField"></param>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //private Dictionary<string, object> BuildDictionary(IUnitOfWork unitwork, ModelField modelField, int key)
        //{
        //    var dictionary = new Dictionary<string, object>();
        //    switch (modelField.FieldType)
        //    {
        //        case 0:
        //            var intRepository = unitwork.Repository<ContentIntField>();
        //            Expression<Func<ContentIntField, bool>> intWhere = w => w.ContentId == key
        //            && w.FieldCode == modelField.Code;
        //            var intInfo = intRepository.Query(intWhere).FirstOrDefault();
        //            if (intInfo != null)
        //            {
        //                var info = intInfo.FieldValue;
        //                dictionary.Add(modelField.Code, info);
        //            }
        //            break;
        //        case 1:
        //            var doubleRepository = unitwork.Repository<ContentDoubleField>();
        //            Expression<Func<ContentDoubleField, bool>> doubleWhere = w => w.ContentId == key
        //            && w.FieldCode == modelField.Code;
        //            var doubleInfo = doubleRepository.Query(doubleWhere).FirstOrDefault();
        //            if (doubleInfo != null)
        //            {
        //                var info = doubleInfo.FieldValue;
        //                dictionary.Add(modelField.Code, info);
        //            }
        //            break;
        //        case 2:
        //            var stringRepository = unitwork.Repository<ContentStringField>();
        //            Expression<Func<ContentStringField, bool>> stringWhere = w => w.ContentId == key
        //            && w.FieldCode == modelField.Code;
        //            var stringInfo = stringRepository.Query(stringWhere).FirstOrDefault();
        //            if (stringInfo != null)
        //            {
        //                var info = stringInfo.FieldValue;
        //                dictionary.Add(modelField.Code, info);
        //            }
        //            break;
        //        case 3:
        //            var descRepository = unitwork.Repository<ContentDescriptionField>();
        //            Expression<Func<ContentDescriptionField, bool>> descWhere = w => w.ContentId == key
        //            && w.FieldCode == modelField.Code;
        //            var descInfo = descRepository.Query(descWhere).FirstOrDefault();
        //            if (descInfo != null)
        //            {
        //                var info = descInfo.FieldValue;
        //                dictionary.Add(modelField.Code, info);
        //            }
        //            break;
        //        case 4:
        //            var textRepository = unitwork.Repository<ContentTextField>();
        //            Expression<Func<ContentTextField, bool>> textWhere = w => w.ContentId == key
        //            && w.FieldCode == modelField.Code;
        //            var textInfo = textRepository.Query(textWhere).FirstOrDefault();
        //            if (textInfo != null)
        //            {
        //                var info = textInfo.FieldValue;
        //                dictionary.Add(modelField.Code, info);
        //            }
        //            break;
        //        case 5:
        //            var timeRepository = unitwork.Repository<ContentTimeField>();
        //            Expression<Func<ContentTimeField, bool>> timeWhere = w => w.ContentId == key
        //            && w.FieldCode == modelField.Code;
        //            var timeInfo = timeRepository.Query(timeWhere).FirstOrDefault();
        //            if (timeInfo != null)
        //            {
        //                var info = timeInfo.FieldValue;
        //                dictionary.Add(modelField.Code, info);
        //            }
        //            break;
        //    }
        //    return dictionary;
        //} 
        #endregion



        /// <summary>
        /// 构建字典
        /// </summary>
        /// <param name="unitwork"></param>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Dictionary<string, object> BuildDictionary(IUnitOfWork unitwork, Model model, int key)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var modelField in model.ModelFields)
            {
                switch (modelField.FieldType)
                {
                    case 0:
                        var intRepository = unitwork.Repository<ContentIntField>();
                        Expression<Func<ContentIntField, bool>> intWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var intInfo = intRepository.Query(intWhere).FirstOrDefault();
                        if (intInfo != null)
                        {
                            var info = intInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 1:
                        var doubleRepository = unitwork.Repository<ContentDoubleField>();
                        Expression<Func<ContentDoubleField, bool>> doubleWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var doubleInfo = doubleRepository.Query(doubleWhere).FirstOrDefault();
                        if (doubleInfo != null)
                        {
                            var info = doubleInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 2:
                        var stringRepository = unitwork.Repository<ContentStringField>();
                        Expression<Func<ContentStringField, bool>> stringWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var stringInfo = stringRepository.Query(stringWhere).FirstOrDefault();
                        if (stringInfo != null)
                        {
                            var info = stringInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 3:
                        var descRepository = unitwork.Repository<ContentDescriptionField>();
                        Expression<Func<ContentDescriptionField, bool>> descWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var descInfo = descRepository.Query(descWhere).FirstOrDefault();
                        if (descInfo != null)
                        {
                            var info = descInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 4:
                        var textRepository = unitwork.Repository<ContentTextField>();
                        Expression<Func<ContentTextField, bool>> textWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var textInfo = textRepository.Query(textWhere).FirstOrDefault();
                        if (textInfo != null)
                        {
                            var info = textInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                    case 5:
                        var timeRepository = unitwork.Repository<ContentTimeField>();
                        Expression<Func<ContentTimeField, bool>> timeWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var timeInfo = timeRepository.Query(timeWhere).FirstOrDefault();
                        if (timeInfo != null)
                        {
                            var info = timeInfo.FieldValue;
                            dictionary.Add(modelField.Code, info);
                        }
                        break;
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="unitwork"></param>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="dictionary"></param>
        private void UpdateContentField(IUnitOfWork unitwork, Content content, Model model, Dictionary<string, object> dictionary)
        {
            var key = content.ContentId;
            foreach (var modelField in model.ModelFields)
            {
                var keyValue = dictionary[modelField.Code];
                switch (modelField.FieldType)
                {
                    case 0:
                        var intRepository = unitwork.Repository<ContentIntField>();
                        Expression<Func<ContentIntField, bool>> intWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var intInfo = intRepository.Query(intWhere).FirstOrDefault();
                        if (intInfo != null)
                        {
                            intInfo.FieldValue = Convert.ToInt32(keyValue);
                            intRepository.Update(intInfo);
                        }

                        break;
                    case 1:
                        var doubleRepository = unitwork.Repository<ContentDoubleField>();
                        Expression<Func<ContentDoubleField, bool>> doubleWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var doubleInfo = doubleRepository.Query(doubleWhere).FirstOrDefault();
                        if (doubleInfo != null)
                        {
                            doubleInfo.FieldValue = Convert.ToDouble(keyValue);
                            doubleRepository.Update(doubleInfo);
                        }
                        break;
                    case 2:
                        var stringRepository = unitwork.Repository<ContentStringField>();
                        Expression<Func<ContentStringField, bool>> stringWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var stringInfo = stringRepository.Query(stringWhere).FirstOrDefault();
                        if (stringInfo != null)
                        {
                            stringInfo.FieldValue = Convert.ToString(keyValue);
                            stringRepository.Update(stringInfo);
                        }
                        break;
                    case 3:
                        var descRepository = unitwork.Repository<ContentDescriptionField>();
                        Expression<Func<ContentDescriptionField, bool>> descWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var descInfo = descRepository.Query(descWhere).FirstOrDefault();
                        if (descInfo != null)
                        {
                            descInfo.FieldValue = Convert.ToString(keyValue);
                            descRepository.Update(descInfo);
                        }
                        break;
                    case 4:
                        var textRepository = unitwork.Repository<ContentTextField>();
                        Expression<Func<ContentTextField, bool>> textWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var textInfo = textRepository.Query(textWhere).FirstOrDefault();
                        if (textInfo != null)
                        {
                            textInfo.FieldValue = Convert.ToString(keyValue);
                            textRepository.Update(textInfo);
                        }
                        break;
                    case 5:
                        var timeRepository = unitwork.Repository<ContentTimeField>();
                        Expression<Func<ContentTimeField, bool>> timeWhere = w => w.ContentId == key
                        && w.FieldCode == modelField.Code;
                        var timeInfo = timeRepository.Query(timeWhere).FirstOrDefault();
                        if (timeInfo != null)
                        {
                            timeInfo.FieldValue = Convert.ToDateTime(keyValue);
                            timeRepository.Update(timeInfo);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 构建内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="dictionary"></param>
        private void BuildContentField(Content content, Model model, Dictionary<string, object> dictionary)
        {
            foreach (var modelField in model.ModelFields)
            {
                var keyValue = dictionary[modelField.Code];
                switch (modelField.FieldType)
                {
                    case 0:
                        var contentIntField = new ContentIntField
                        {
                            Content = content,
                            FieldCode = modelField.Code,
                            FieldValue = Convert.ToInt32(keyValue)
                        };
                        content.ContentIntFields.Add(contentIntField);
                        break;
                    case 1:
                        var contentDoubleField = new ContentDoubleField
                        {
                            Content = content,
                            FieldCode = modelField.Code,
                            FieldValue = Convert.ToDouble(keyValue)
                        };
                        content.ContentDoubleFields.Add(contentDoubleField);
                        break;
                    case 2:
                        var contentStringField = new ContentStringField
                        {
                            Content = content,
                            FieldCode = modelField.Code,
                            FieldValue = Convert.ToString(keyValue)
                        };
                        content.ContentStringFields.Add(contentStringField);
                        break;
                    case 3:
                        var contentDescriptionField = new ContentDescriptionField
                        {
                            Content = content,
                            FieldCode = modelField.Code,
                            FieldValue = Convert.ToString(keyValue)
                        };
                        content.ContentDescriptionFields.Add(contentDescriptionField);
                        break;
                    case 4:
                        var contentTextField = new ContentTextField
                        {
                            Content = content,
                            FieldCode = modelField.Code,
                            FieldValue = Convert.ToString(keyValue)
                        };
                        content.ContentTextFields.Add(contentTextField);
                        break;
                    case 5:
                        var contentTimeField = new ContentTimeField
                        {
                            Content = content,
                            FieldCode = modelField.Code,
                            FieldValue = Convert.ToDateTime(keyValue)
                        };
                        content.ContentTimeFields.Add(contentTimeField);
                        break;
                }
            }
        }
        #endregion
    }
}