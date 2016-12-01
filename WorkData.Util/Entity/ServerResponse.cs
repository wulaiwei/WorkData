#region 版权信息

// ------------------------------------------------------------------------------
// Copyright © 成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Util 
// 文件名：ServerResponse.cs
// 创建标识：吴来伟  2016-06-21 16:21
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#endregion

#region 命名空间

using Newtonsoft.Json;

#endregion

namespace WorkData.Util.Entity
{
    /// <summary>
    ///     接口返回值
    /// </summary>
    public class ServerResponse
    {
        /// <summary>
        ///     返回码[0=success,其它代码都是异常]
        /// </summary>
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        ///     返回码描述
        /// </summary>
        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; } = "操作成功";
    }

    /// <summary>
    ///     响应消息类
    /// </summary>
    public class ServerResponse<T> : ServerResponse
        where T : class,new()
    {
        /// <summary>
        ///     业务数据对象
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}