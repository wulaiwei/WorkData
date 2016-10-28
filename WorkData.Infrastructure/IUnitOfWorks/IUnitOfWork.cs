// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：UnitWork.Repository
// 文件名：IUnitWork.cs
// 创建标识：laiwei wu  2016-08-29 16:39
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Data;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.ITransactions;

namespace WorkData.Infrastructure.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;

        void Commit();

        ITransaction BeginTransaction(IsolationLevel level);
    }
}