using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace template__.Helpers
{
    public static class MapHelper
    {
        /// <summary>
        /// Create new object
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T2 Map<T1, T2>(T1 obj)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>().ReverseMap());
            var autoMapper = mapperConfig.CreateMapper();
            return autoMapper.Map<T1, T2>(obj);
        }

        public static List<T2> MapList<T1, T2>(List<T1> obj)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>().ReverseMap());
            var autoMapper = mapperConfig.CreateMapper();
            return autoMapper.Map<List<T1>, List<T2>>(obj);
        }

        /// <summary>
        /// Update to target object
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="obj"></param>
        /// <param name="desObj"></param>
        public static void Map<T1, T2>(T1 obj, T2 desObj)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>().ReverseMap());
            var autoMapper = mapperConfig.CreateMapper();
            autoMapper.Map<T1, T2>(obj, desObj);
        }

        public static T DictionaryToObject<T>(this IDictionary<string, object> source) where T : class, new()
        {
            var resultObject = new T();
            var resultObjectType = resultObject.GetType();

            foreach (var item in source)
            {
                resultObjectType.GetProperty(item.Key).SetValue(resultObject, item.Value, null);
            }

            return resultObject;
        }

        public static IDictionary<string, object> ObjectToDictionary(this object source, BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
