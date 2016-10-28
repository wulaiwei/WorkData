// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：Auto.Fac.Repository
// 文件名：IAutoFacTransaction.cs
// 创建标识：  2016-08-23 13:53
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;

namespace WorkData.Infrastructure.ITransactions
{
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// 提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚
        /// </summary>
        void RollBack();
    }
}