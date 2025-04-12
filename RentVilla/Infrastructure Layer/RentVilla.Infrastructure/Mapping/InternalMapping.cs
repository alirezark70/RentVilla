using RentVilla.Application.Common.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Infrastructure.Mapping
{
    public class InternalMapper : IMapper, IMapperConfiguration
    {
        private readonly Dictionary<Type, Dictionary<Type, Func<object, object, object>>> _mappings =
            new Dictionary<Type, Dictionary<Type, Func<object, object, object>>>();

        private readonly Dictionary<Type, Dictionary<Type, Dictionary<string, Func<object, object>>>> _memberMappings =
            new Dictionary<Type, Dictionary<Type, Dictionary<string, Func<object, object>>>>();

        private static readonly Dictionary<Type, PropertyInfo[]> _propertyCache =
            new Dictionary<Type, PropertyInfo[]>();

        public IMapperConfiguration CreateMap<TSource, TDestination>()
        {
            var sourceType = typeof(TSource);
            var destType = typeof(TDestination);

            if (!_mappings.ContainsKey(sourceType))
            {
                _mappings[sourceType] = new Dictionary<Type, Func<object, object, object>>();
            }

            _mappings[sourceType][destType] = (source, destination) =>
            {
                return MapInternal<TSource, TDestination>((TSource)source, destination != null ? (TDestination)destination : default);
            };

            if (!_memberMappings.ContainsKey(sourceType))
            {
                _memberMappings[sourceType] = new Dictionary<Type, Dictionary<string, Func<object, object>>>();
            }

            if (!_memberMappings[sourceType].ContainsKey(destType))
            {
                _memberMappings[sourceType][destType] = new Dictionary<string, Func<object, object>>();
            }

            return this;
        }

        public IMapperConfiguration ForMember<TSource, TDestination>(
            Expression<Func<TDestination, object>> destinationMember,
            Func<TSource, object> memberValue)
        {
            var sourceType = typeof(TSource);
            var destType = typeof(TDestination);

            string propertyName = GetPropertyName(destinationMember);

            if (_memberMappings.ContainsKey(sourceType) &&
                _memberMappings[sourceType].ContainsKey(destType))
            {
                _memberMappings[sourceType][destType][propertyName] = source => memberValue((TSource)source);
            }

            return this;
        }

        private string GetPropertyName<TDestination>(Expression<Func<TDestination, object>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            if (expression.Body is UnaryExpression unaryExpression &&
                unaryExpression.Operand is MemberExpression memberExpr)
            {
                return memberExpr.Member.Name;
            }

            throw new ArgumentException("The expression is not a valid property expression.", nameof(expression));
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
                return default;

            var destType = typeof(TDestination);
            TDestination destination;

            if (!destType.IsValueType && destType.GetConstructor(Type.EmptyTypes) != null)
            {
                destination = Activator.CreateInstance<TDestination>();
            }
            else
            {
                destination = default;
            }

            return Map(source, destination);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
                return destination;

            var sourceType = typeof(TSource);
            var destType = typeof(TDestination);

            if (_mappings.ContainsKey(sourceType) && _mappings[sourceType].ContainsKey(destType))
            {
                return (TDestination)_mappings[sourceType][destType](source, destination);
            }

            return MapInternal<TSource, TDestination>(source, destination);
        }

        private TDestination MapInternal<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
                return destination;

            var sourceType = typeof(TSource);
            var destType = typeof(TDestination);

            if (destination == null && !destType.IsValueType && destType.GetConstructor(Type.EmptyTypes) != null)
            {
                destination = Activator.CreateInstance<TDestination>();
            }

            if (destination == null)
                return default;

            if (!_propertyCache.TryGetValue(sourceType, out var sourceProperties))
            {
                sourceProperties = sourceType.GetProperties().Where(p => p.CanRead).ToArray();
                _propertyCache[sourceType] = sourceProperties;
            }

            if (!_propertyCache.TryGetValue(destType, out var destProperties))
            {
                destProperties = destType.GetProperties().Where(p => p.CanWrite).ToArray();
                _propertyCache[destType] = destProperties;
            }

            var destPropsDict = destProperties.ToDictionary(p => p.Name);

            bool hasMemberMappings = _memberMappings.ContainsKey(sourceType) &&
                                     _memberMappings[sourceType].ContainsKey(destType);

            foreach (var sourceProp in sourceProperties)
            {
                if (hasMemberMappings && _memberMappings[sourceType][destType].TryGetValue(sourceProp.Name, out var customMapping))
                {
                    var value = customMapping(source);
                    destPropsDict[sourceProp.Name].SetValue(destination, value);
                    continue;
                }

                if (destPropsDict.TryGetValue(sourceProp.Name, out var destProp))
                {
                    var sourceValue = sourceProp.GetValue(source);

                    if (sourceValue == null)
                        continue;

                    if (destProp.PropertyType == sourceProp.PropertyType)
                    {
                        destProp.SetValue(destination, sourceValue);
                    }
                    else if (IsCollectionType(sourceProp.PropertyType) && IsCollectionType(destProp.PropertyType))
                    {
                        MapCollection(sourceValue, destination, destProp);
                    }
                    else if (!sourceProp.PropertyType.IsValueType && !destProp.PropertyType.IsValueType)
                    {
                        var destValue = destProp.GetValue(destination);
                        var mapMethod = typeof(InternalMapper).GetMethod("Map", new[] { sourceProp.PropertyType, destProp.PropertyType });

                        if (mapMethod != null)
                        {
                            var result = mapMethod.Invoke(this, new[] { sourceValue, destValue });
                            destProp.SetValue(destination, result);
                        }
                    }
                    else if (destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        destProp.SetValue(destination, sourceValue);
                    }
                }
            }

            return destination;
        }

        private bool IsCollectionType(Type type)
        {
            // بررسی اینکه آیا نوع، یک کالکشن است (مثل لیست یا آرایه)  
            return type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
        }

        private void MapCollection(object sourceCollection, object destination, PropertyInfo destProp)
        {
            // پیاده‌سازی مپینگ کالکشن‌ها  
            // این متد می‌تواند برای مپینگ آرایه‌ها و لیست‌ها استفاده شود  

            // به دلیل پیچیدگی، فقط اصول کلی را اینجا بیان می‌کنیم  
            // در پیاده‌سازی واقعی باید انواع عناصر را بررسی کرد و  
            // از متد Map برای مپینگ هر عنصر استفاده کرد  
        }
    }
}
