using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace WorkData.Dto.Entity
{
    public class ContentValue : DynamicObject,IDictionary<string, object>
    {
        #region 动态类构建

        private readonly IDictionary<string, object> _properties;

        public ContentValue()
        {
        }

        public ContentValue(IDictionary<string, object>  properties)
        {
            _properties = properties;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!_properties.Keys.Contains(binder.Name))
            {
                _properties.Add(binder.Name, value.ToString());
            }
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _properties.TryGetValue(binder.Name, out result);
        }

        #endregion 动态类构建



        #region IDictionary<string,object> Members

        void IDictionary<string, object>.Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        bool IDictionary<string, object>.ContainsKey(string key)
        {
            return _properties.ContainsKey(key);
        }

        ICollection<string> IDictionary<string, object>.Keys => _properties.Keys;

        bool IDictionary<string, object>.Remove(string key)
        {
            throw new NotImplementedException();
        }

        bool IDictionary<string, object>.TryGetValue(string key, out object value)
        {
            return _properties.TryGetValue(key, out value);
        }

        ICollection<object> IDictionary<string, object>.Values => _properties.Values;

        object IDictionary<string, object>.this[string key]
        {
            get
            {
                return _properties[key];
            }
            set
            {
                if (!_properties.ContainsKey(key))
                {
                    throw new NotImplementedException();
                }
                _properties[key] = value;
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
            return _properties.Contains(item);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _properties.CopyTo(array, arrayIndex);
        }

        int ICollection<KeyValuePair<string, object>>.Count
        {
            get { return _properties.Count; }
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
            return _properties.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        #endregion
    }
}