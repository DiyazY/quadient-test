using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace quadient_test
{
    public static class MappingExtension
    {
        public static T CopyFromJson<T>(this T source, T template)
        {
            var json = JsonConvert.SerializeObject(template);
            source = JsonConvert.DeserializeObject<T>(json);
            return source;
        }


        public static T CopyFromFlitBitImplementation<T>(this T source, object template)
        {
            typeof(FlitBit.Copy.Extensions).GetMethod("CopyFrom", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(source.GetType(), template.GetType())
                .Invoke(null, new[] { source, template });
            return source;
        }


        public static T CopyFromSerializable<T>(this T source, T template)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, template);
                ms.Position = 0;
                source = (T)formatter.Deserialize(ms);
                return source;
            }
        }

        public static T CopyFromJObject<T>(this T source, T template)
        {
            var obj = JObject.FromObject(template);
            source = obj.ToObject<T>();
            return source;
        }

        public static T CopyFromReflection<T>(this T source, T template)
        {
            source = (T)Process(template);
            return source;
        }

        private static object Process(object obj)
        {
            if (obj == null)
                return null;
            Type type = obj.GetType();
            if (type.IsValueType || type == typeof(string))
            {
                return obj;
            }
            else if (type.IsArray)
            {
                Type elementType = Type.GetType(
                     type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(Process(array.GetValue(i)), i);
                }
                return Convert.ChangeType(copied, obj.GetType());
            }
            else if (type.IsClass)
            {
                object toret = Activator.CreateInstance(obj.GetType());
                FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                            BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(obj);
                    if (fieldValue == null)
                        continue;
                    field.SetValue(toret, Process(fieldValue));
                }
                return toret;
            }
            else
                throw new ArgumentException("Unknown type");
        }
    }
}
