using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LightCommands.Implementation
{
    internal static class CommonExtensions
    {
        /// <summary>
        /// Return value if exist; otherwise return defaultValue
        /// </summary>
        [DebuggerStepThrough]
        public static TValue GetValueOrDefault<TKey, TValue>( this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue) )
        {
            TValue res;
            return dict.TryGetValue( key, out res ) ? res : defaultValue;
        }

        
        /// <summary>
        /// Return value if exist; otherwise create and add to dict
        /// </summary>
        [DebuggerStepThrough]
        public static TValue GetValueOrCreate<TKey, TValue>( this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> creator )
        {
            TValue res;
            if ( !dict.TryGetValue( key, out res ) )
            {
                res = creator(key);
                dict.Add( key, res );
            }

            return res;
        }

        /// <summary>
        /// Get DescriptionAttribute value if exist; otherwise return ToString()
        /// </summary>
        [DebuggerStepThrough]
        public static string GetDescription(this Enum en)
        {
            return en.GetAttribute<DescriptionAttribute>()?.Description ?? en.ToString();
        }

        /// <summary>
        /// Get custom attribute value
        /// </summary>
        [DebuggerStepThrough]
        public static T GetAttribute<T>(this Enum en) where T : Attribute
        {
            MemberInfo[] mi = en.GetType().GetMember(en.ToString());
            if (mi.Length <= 0)
                return null;

            return Attribute.GetCustomAttributes(mi[0], typeof(T), false)
                            .Cast<T>()
                            .FirstOrDefault();
        }
    }
}
