// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Util 
// 文件名：PageListHepler.cs
// 创建标识：吴来伟 2016-11-02
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using WorkData.Util.Entity;
using System.Collections;
using System.Collections.Generic;
using Webdiyer.WebControls.Mvc;

namespace WorkData.Util
{
    public class PageListHepler
    {
        /// <summary>
        /// 构建分页类
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <returns></returns>
        public static PageEntity BuildPageEntity(int pageIndex,int pageSize,string sidx,string sord)
        {
            var pageEntity = new PageEntity
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Sidx = sidx,
                Sord = sord
            };
            return pageEntity;
        }

        /// <summary>
        /// 构建分页实体(PagedList)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public static PagedList<T> BuildPagedList<T>(IEnumerable<T> data,PageEntity pageEntity)
        {
            var page = new PagedList<T>(data, pageEntity.PageIndex, pageEntity.PageSize,pageEntity.Total);
            return page;
        }
    }
}