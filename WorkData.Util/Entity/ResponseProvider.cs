namespace WorkData.Util.Entity
{
    /// <summary>
    /// 响应消息工厂
    /// </summary>
    public static class ResponseProvider
    {
        /// <summary>
        ///     响应消息封装类
        /// </summary>
        /// <param name="errmsg">消息内容</param>
        /// <returns></returns>
        public static ServerResponse Success(string errmsg = null)
        {
            var result = new ServerResponse {ErrorCode = 0, ErrorMsg = errmsg};

            return result;
        }

        /// <summary>
        ///     响应消息封装类
        /// </summary>
        /// <param name="errcode">状态:0 -成功 其它 失败</param>
        /// <param name="errMsg">消息内容</param>
        /// <returns></returns>
        public static ServerResponse Error(string errMsg, int errcode = 0)
        {
            var result = new ServerResponse {ErrorCode = errcode, ErrorMsg = errMsg};
            return result;
        }

        /// <summary>
        ///     响应消息封装类
        /// </summary>
        /// <param name="data">业务数据</param>
        /// <param name="errmsg">消息内容</param>
        /// <returns></returns>
        public static ServerResponse<T> Success<T>(T data, string errmsg = null) where T : class,new()
        {
            var result = new ServerResponse<T> {Data = data, ErrorCode = 0, ErrorMsg = errmsg};

            return result;
        }

        /// <summary>
        ///     Http 响应消息封装类
        /// </summary>
        /// <param name="errmsg">消息内容</param>
        /// <returns></returns>
        public static ServerResponse<T> Error<T>(string errmsg = null) where T : class, new()
        {
            var result = new ServerResponse<T> {Data = null, ErrorCode = -1, ErrorMsg = errmsg};

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="errmsg"></param>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public static ServerResponse<T> Error<T>(int errcode, string errmsg) where T : class, new()
        {
            var result = new ServerResponse<T> {Data = null, ErrorCode = errcode, ErrorMsg = errmsg};

            return result;
        }
    }
}