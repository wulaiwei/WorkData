// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：UnitWork.Repository
// 文件名：EfTransaction.cs
// 创建标识：laiwei wu  2016-08-29 16:44
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using WorkData.Infrastructure.ITransactions;

namespace WorkData.Respository.Transactions
{
    public class EfTransaction : ITransaction
    {
        private readonly DbContextTransaction _transaction;

        public EfTransaction(DbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _transaction.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}