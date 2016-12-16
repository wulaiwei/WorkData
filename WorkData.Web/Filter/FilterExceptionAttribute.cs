// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：FilterExceptionAttribute.cs
// 创建标识：吴来伟 2016-12-01
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Web.Mvc;

namespace WorkData.Web.Filter
{
    public class FilterExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            throw new System.NotImplementedException();
        }
    }
}