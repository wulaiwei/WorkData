// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Util 
// 文件名：PageEntity.cs
// 创建标识：吴来伟 2016-10-31
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace WorkData.Util.Entity
{
    public class PageEntity
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Records { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Total
        {
            get
            {
                if (Records > 0)
                {
                    return Records % this.PageSize == 0 ? Records / this.PageSize : Records / this.PageSize + 1;
                }

                return 0;
            }
        }


        /// <summary>
        /// 排序列
        /// </summary>
        public string Sidx { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string Sord { get; set; }

    }
}