// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Test 
// 文件名：Text.cs
// 创建标识：吴来伟 2016-11-22
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace WorkData.Test
{
    public class Text: IDictionary<string, object>
    {
        IDictionary<string, object> data;


        #region IDictionary<string,object> Members

        void IDictionary<string, object>.Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        bool IDictionary<string, object>.ContainsKey(string key)
        {
            return data.ContainsKey(key);
        }

        ICollection<string> IDictionary<string, object>.Keys
        {
            get { return data.Keys; }
        }

        bool IDictionary<string, object>.Remove(string key)
        {
            throw new NotImplementedException();
        }

        bool IDictionary<string, object>.TryGetValue(string key, out object value)
        {
            return data.TryGetValue(key, out value);
        }

        ICollection<object> IDictionary<string, object>.Values
        {
            get { return data.Values; }
        }

        object IDictionary<string, object>.this[string key]
        {
            get
            {
                return data[key];
            }
            set
            {
                if (!data.ContainsKey(key))
                {
                    throw new NotImplementedException();
                }
                data[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<string,object>> Members

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return data.Contains(item);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        int ICollection<KeyValuePair<string, object>>.Count
        {
            get { return data.Count; }
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get { return true; }
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion
    }
}