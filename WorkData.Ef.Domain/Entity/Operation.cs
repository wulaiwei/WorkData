using System.Collections.Generic;

namespace WorkData.EF.Domain.Entity
{
    public sealed class Operation
    {
        public Operation()
        {
            Privileges = new List<Privilege>();
        }

        /// <summary>
        ///操作ID
        /// </summary>

        public int OperationId { get; set; }

        /// <summary>
        ///操作名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        ///操作代码
        /// </summary>

        public string Code { get; set; }

        /// <summary>
        ///状态
        /// </summary>

        public bool Status { get; set; }

        #region 外键

        public ICollection<Privilege> Privileges { get; set; }

        #endregion 外键
    }
}