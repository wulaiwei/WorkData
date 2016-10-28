#region Apache License Version 2.0

// ---------------------------------------------------------------------------
//  Copyright 2016  The LightWork Project
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// ---------------------------------------------------------------------------

#endregion Apache License Version 2.0

#region Import namespace

using System;
using System.Security.Cryptography;

#endregion Import namespace

namespace WorkData.Code.Helpers
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    internal sealed class RandomGenerator : RandomNumberGenerator
    {
        private static RandomNumberGenerator _rng;

        public RandomGenerator()
        {
            _rng = Create();
        }

        public override void GetBytes(byte[] data)
        {
            _rng.GetBytes(data);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            _rng.GetNonZeroBytes(data);
        }

        /// <summary>
        /// 返回一个在 0.0-1.0之间的随机数
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            var bytes = new byte[4];
            _rng.GetBytes(bytes);
            return (double)BitConverter.ToUInt32(bytes, 0) / uint.MaxValue;
        }

        /// <summary>
        /// 返回一个指定范围内的随机数
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public int Next(int minValue, int maxValue)
        {
            return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
        }

        /// <summary>
        /// 返回一个小于maxValue的非负随机数
        /// </summary>
        /// <param name="maxValue">maxValue 必须大于或等于 0</param>
        /// <returns></returns>
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        /// <summary>
        /// 返回一个非负的随机数
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            return Next(0, int.MaxValue);
        }
    }
}