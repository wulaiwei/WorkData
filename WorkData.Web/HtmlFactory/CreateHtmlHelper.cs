
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using WorkData.Dto.Entity;
using WorkData.Util;
using WorkData.Util.Entity;

namespace WorkData.Web.HtmlFactory
{
    public class CreateHtmlHelper
    {
        #region 按钮组
        /// <summary>
        /// 列表按钮组
        /// </summary>
        /// <param name="operationDto"></param>
        /// <returns></returns>
        public static string CreateOperationIndexList(List<OperationDto> operationDto)
        {
            var sw = new StringWriter();
            var writer = new HtmlTextWriter(sw);

            foreach (var item in operationDto)
            {
                writer.AddAttribute("id", item.Id);
                if (!string.IsNullOrEmpty(item.OnClick))
                {
                    writer.AddAttribute("onclick", item.OnClick);
                    writer.AddAttribute("href", "javascript:;");
                }

                writer.AddAttribute("class", item.Class);
                writer.AddAttribute("style", item.Style);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(item.Name);
                writer.RenderEndTag();
            }

            return writer.InnerWriter.ToString();
        }

        /// <summary>
        /// 顶部按钮组
        /// </summary>
        /// <param name="operationDto"></param>
        /// <returns></returns>
        public static string CreateOperationTopList(List<OperationDto> operationDto)
        {
            var sw = new StringWriter();
            var writer = new HtmlTextWriter(sw);

            foreach (var item in operationDto)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("id", item.Id);
                if (!string.IsNullOrEmpty( item.OnClick))
                {
                    writer.AddAttribute("onclick", item.OnClick);
                    writer.AddAttribute("href", "javascript:;");
                }

                writer.AddAttribute("class", item.Class);
                writer.AddAttribute("style", item.Style);
                writer.RenderBeginTag(HtmlTextWriterTag.A);

                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(item.Name);
                writer.RenderEndTag();

                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            return writer.InnerWriter.ToString();
        } 
        #endregion

        #region 列表
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="modelFieldDto"></param>
        /// <returns></returns>
        public static string CreateTrList(List<ModelFieldDto> modelFieldDto)
        {
            var sw = new StringWriter();
            var writer = new HtmlTextWriter(sw);

            foreach (var item in modelFieldDto)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("{{{{content.ContentValue.{0}}}}}", item.Code);
                writer.RenderEndTag();
            }

            return writer.InnerWriter.ToString();
        }  
        #endregion


        #region 表头
        public static string CreateListHead(List<ModelFieldDto> modelFieldDto)
        {
            var sw = new StringWriter();
            var writer = new HtmlTextWriter(sw);
            //tr
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            //构建表头
            writer.AddAttribute("width", "6%");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("选择");
            writer.RenderEndTag();

            foreach (var item in modelFieldDto)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(item.Name);
                writer.RenderEndTag();
            }
            writer.AddAttribute("width", "30%");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("操作");
            writer.RenderEndTag();

            writer.RenderEndTag();

            return writer.InnerWriter.ToString();
        }
        #endregion

            #region 表单
        public static string CreateForm(List<ModelFieldDto> modelFieldDto)
        {
            var sw = new StringWriter();
            var writer = new HtmlTextWriter(sw);
            //构建form
            //writer.AddAttribute("method", "post");
            //writer.AddAttribute("action", "~/Admin/Content/Save");
            //writer.AddAttribute("id", "form1");
            //writer.RenderBeginTag(HtmlTextWriterTag.Form);

            //构建内容DIV
            writer.AddAttribute("class", "tab-content");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //构建字段列表
            foreach (var item in modelFieldDto)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Dl);
                //dt
                writer.RenderBeginTag(HtmlTextWriterTag.Dt);
                writer.Write(item.Name);
                writer.RenderEndTag();

                //dd
                writer.RenderBeginTag(HtmlTextWriterTag.Dd);
                writer.Write(item.HtmlTemplate);
                writer.RenderEndTag();

                writer.RenderEndTag();
            }

            writer.RenderEndTag();
            //writer.RenderEndTag();

            return writer.InnerWriter.ToString();
        }
        #endregion

        #region 标签
        public static HtmlMessage ProcessCreateHtmlFactory(ModelFieldDto model)
        {
            var message = new HtmlMessage();
            var controlType = model.ControlType;
            message.FieldType = CreateDataType(controlType);
            switch (model.ControlType)
            {
                case "single-text":
                case "images":
                case "video":
                case "password":
                case "number":
                case "datetime":
                    message.Html = CreateInputText(model);
                    break;

                case "multi-text":
                case "editor":
                    message.Html = CreateTextArea(model);
                    break;

                case "multi-radio":
                case "multi-checkbox":
                    message.Html = CreateButtonList(model);
                    break;

                default:
                    break;
            }

            if (!string.IsNullOrEmpty(model.ValidTipMsg))
            {
                message.Html = message.Html + CreateTip(model);
            }

            return message;
        }

        /// <summary>
        /// 创建标签说明
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string CreateTip(ModelFieldDto model)
        {
            var builder = new TagBuilder("span");
            builder.AddCssClass("Validform_checktip");

            builder.InnerHtml = string.IsNullOrEmpty(model.ValidTipMsg)
                ? $"*填写说明：{model.Name}"
                : $"*填写说明：{model.ValidTipMsg}";

            return builder.ToString(TagRenderMode.Normal);
        }

        #region 创建标签
        /// <summary>
        /// 创建文本框
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static string CreateInputText(ModelFieldDto model)
        {
            var builder = new TagBuilder("input");
            builder.MergeAttribute("id", "txt" + model.Code);
            builder.MergeAttribute("name", model.Code);
            builder.MergeAttribute("ng-model", "content.ContentValue." + model.Code);

            if (!string.IsNullOrEmpty(model.ValidPattern))
            {
                builder.MergeAttribute("datatype", model.ValidPattern);
                builder.MergeAttribute("errormsg", model.ValidErrorMsg);
            }

            switch (model.ControlType)
            {
                case "single-text":
                    builder.MergeAttribute("class", "input normal");
                    builder.MergeAttribute("type", "text");
                    break;

                case "images":
                case "video":
                    builder.MergeAttribute("class", "input normal upload-path");
                    builder.MergeAttribute("type", "text");
                    break;

                case "password":
                    builder.MergeAttribute("class", "input normal");
                    builder.MergeAttribute("type", "password");
                    break;

                case "datetime":
                    builder.MergeAttribute("class", "input rule-date-input time");
                    break;

                case "number":
                case "int":
                    builder.MergeAttribute("class", "input small");
                    builder.MergeAttribute("type", "number");
                    break;

                default:
                    break;
            }

            switch (model.ControlType)
            {
                case "images":
                    //TagRenderMode.SelfClosing生成自闭和标签
                    return builder.ToString(TagRenderMode.SelfClosing) + CreateUploadDiv("upload-box upload-img");
                case "video":
                    return builder.ToString(TagRenderMode.SelfClosing) + CreateUploadDiv("upload-box upload-video");
                default:
                    return builder.ToString(TagRenderMode.SelfClosing);
            }
        }

        /// <summary>
        /// 创建文本域
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static string CreateTextArea(ModelFieldDto model)
        {
            var builder = new TagBuilder("textarea");
            builder.MergeAttribute("id", "txt" + model.Code);
            builder.MergeAttribute("name", model.Code);
            builder.InnerHtml = "{{content.ContentValue." + model.Code+"}}";
            //builder.InnerHtml = "@Model.ContentValue" + model.Code;

            if (!string.IsNullOrEmpty(model.ValidPattern))
            {
                builder.MergeAttribute("datatype", model.ValidPattern);
                builder.MergeAttribute("errormsg", model.ValidErrorMsg);
            }

            switch (model.ControlType)
            {
                case "editor":
                    builder.MergeAttribute("class", "editor");
                    break;

                case "multi-text":
                    builder.MergeAttribute("rows", "3");
                    builder.MergeAttribute("cols", "25");
                    builder.MergeAttribute("class", "input");
                    break;

                default:
                    break;
            }
            return builder.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// 创建复选框
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static string CreateButtonList(ModelFieldDto model)
        {
            var sw = new StringWriter();
            var writer = new HtmlTextWriter(sw);

            writer.AddAttribute("class", model.ControlType == "multi-radio" ?
                "rule-multi-radio" : "rule-multi-checkbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute("class", "multi-check");
            writer.AddAttribute("value", "{{content.ContentValue." + model.Code + "}}");
            writer.AddAttribute("type", "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.AddAttribute("id", "txt" + model.Code);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            var array = BusinessHelper.BreakUpOptions(model.ItemOption, '|');
            for (var i = 0; i < array.Length; i++)
            {
                var obj = array[i].Split(',');

                #region 构建input
                writer.AddAttribute("id", "txt" + model.Code + "_" + i);
                writer.AddAttribute("name", model.Code);

                writer.AddAttribute("type", model.ControlType == "multi-radio" ? "radio" : "checkbox");
                writer.AddAttribute("value", obj[0]);

                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();
                #endregion

                #region 构建label
                writer.AddAttribute("for", "txt" + model.Code + "_" + i);

                writer.RenderBeginTag(HtmlTextWriterTag.Label);
                writer.Write(obj[1]);
                writer.RenderEndTag();
                #endregion
            }
            writer.WriteLine();

            writer.RenderEndTag();

            writer.RenderEndTag();
            return writer.InnerWriter.ToString();
        }


        /// <summary>
        /// 构建上传控件
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        private static string CreateUploadDiv(string className)
        {
            var builder = new TagBuilder("div");
            builder.MergeAttribute("class", className);
            return builder.ToString();
        }
        #endregion

        #region 创建数据库类型 + CreateDataType
        /// <summary>
        /// 创建数据库类型
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns></returns>
        private static FieldType CreateDataType(string controlType)
        {
            switch (controlType)
            {
                case "single-text":
                case "images":
                case "video":
                case "password":
                    return FieldType.StringdField;

                case "multi-radio":
                case "multi-checkbox":
                case "multi-text":
                    return FieldType.DescriptionField;

                case "editor":
                    return FieldType.TextField;

                case "number":
                    return FieldType.DoubleField;

                case "int":
                    return FieldType.IntField;

                case "datetime":
                    return FieldType.TimeField;
                default:
                    return FieldType.StringdField;
            }
        }
        #endregion 
        #endregion
    }
}