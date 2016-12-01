// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：BusinessHelper.cs
// 创建标识：吴来伟 2016-10-28
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Web;
using WorkData.Util.Entity;
using WorkData.Util.Enum;
using static System.String;

namespace WorkData.Util
{
    public class BusinessHelper
    {

        /// <summary>
        /// 构建业务操作状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static SaveState BuildSaveState(HttpRequestBase request)
        {
            var action = request["Action"];
            var key = request["Key"];
            if (IsNullOrEmpty(action))
            {
                return request["SaveState"].ToObject<SaveState>();
            }
            var saveState = new SaveState
            {
                Key = key,
                OperationState = action == "Add" ? OperationState.Add :
                    action == "Edit" ?
                    OperationState.Update : OperationState.Remove
            };
            return saveState;
        }

        /// <summary>
        /// 拆分数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int[] BreakUpStr(string str,char key)
        {
            var strArray= str.Split(new[] { key }, StringSplitOptions.RemoveEmptyEntries);
            return Array.ConvertAll(strArray, int.Parse);
        }


        /// <summary>
        /// 拆分数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] BreakUpOptions(string str, char key)
        {
            var strArray = str.Split(new[] { key }, StringSplitOptions.RemoveEmptyEntries);
            return strArray;
        }

    }
}