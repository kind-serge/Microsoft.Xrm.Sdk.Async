using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.Xrm.Sdk.Async
{
    internal static class ReflectionUtils
    {
        public static Func<object, T> CreatePropertyGetter<T>(Type type, string propertyName)
        {
            var pi = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return CreatePropertyGetter<T>(pi);
        }

        public static Func<object, T> CreatePropertyGetter<T>(PropertyInfo propertyInfo)
        {
            var target = Expression.Parameter(typeof(object), "target");
            var instance = Expression.ConvertChecked(target, propertyInfo.DeclaringType);
            var access = Expression.MakeMemberAccess(instance, propertyInfo);
            var lambda = Expression.Lambda(access, target);
            var func = lambda.Compile();
            return (Func<object, T>)func;
        }

        public static Func<object, T, bool> CreatePropertySetter<T>(Type type, string propertyName)
        {
            var pi = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return CreatePropertySetter<T>(pi);
        }

        public static Func<object, T, bool> CreatePropertySetter<T>(PropertyInfo propertyInfo)
        {
            var target = Expression.Parameter(typeof(object), "target");
            var value = Expression.Parameter(typeof(T), "value");
            var instance = Expression.ConvertChecked(target, propertyInfo.DeclaringType);
            var access = Expression.MakeMemberAccess(instance, propertyInfo);
            var assignment = Expression.Assign(access, value);
            var body = Expression.Block(assignment, Expression.Constant(true, typeof(bool)));
            var lambda = Expression.Lambda(body, target, value);
            var func = lambda.Compile();
            return (Func<object, T, bool>)func;
        }

        public static PropertyAccessor<T> CreatePropertyAccessor<T>(Type type, string propertyName)
        {
            var pi = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (pi == null)
                throw new InvalidOperationException($"The type '{type}' does not have property '{propertyName}'");

            Func<object, T> getter;
            Func<object, T, bool> setter;

            if (pi.GetMethod == null)
                getter = (target) => { throw new InvalidOperationException($"The property '{propertyName}' of type '{type}' does not have a getter"); };
            else
                getter = CreatePropertyGetter<T>(pi);

            if (pi.SetMethod == null)
                setter = (target, value) => { throw new InvalidOperationException($"The property '{propertyName}' of type '{type}' does not have a setter"); };
            else
                setter = CreatePropertySetter<T>(pi);

            return new PropertyAccessor<T>(getter, setter);
        }

        public static EventAccessor<T> CreateEventAccessor<T>(Type type, string eventName)
        {
            var ei = type.GetEvent(eventName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (ei == null)
                throw new InvalidOperationException($"The type '{type}' does not have event '{eventName}'");

            Func<object, T, bool> add;
            Func<object, T, bool> remove;

            if (ei.AddMethod == null)
                add = (target, d) => { throw new InvalidOperationException($"The event '{eventName}' of type '{type}' does not have the 'add' method"); };
            else
                add = (Func<object, T, bool>)CreateMethodAccessor(ei.AddMethod, typeof(bool));

            if (ei.RemoveMethod == null)
                remove = (target, d) => { throw new InvalidOperationException($"The event '{eventName}' of type '{type}' does not have the 'remove' method"); };
            else
                remove = (Func<object, T, bool>)CreateMethodAccessor(ei.RemoveMethod, typeof(bool));

            return new EventAccessor<T>(add, remove);
        }

        public static Func<object, TResult> CreateMethodAccessor<TResult>(Type type, string methodName)
            => (Func<object, TResult>)CreateMethodAccessor(type, methodName, typeof(TResult));

        public static Func<object, TArg1, TResult> CreateMethodAccessor<TArg1, TResult>(Type type, string methodName)
            => (Func<object, TArg1, TResult>)CreateMethodAccessor(type, methodName, typeof(TResult), typeof(TArg1));

        public static Func<object, TArg1, TArg2, TResult> CreateMethodAccessor<TArg1, TArg2, TResult>(Type type, string methodName)
            => (Func<object, TArg1, TArg2, TResult>)CreateMethodAccessor(type, methodName, typeof(TResult), typeof(TArg1), typeof(TArg2));

        public static Func<object, TArg1, TArg2, TArg3, TResult> CreateMethodAccessor<TArg1, TArg2, TArg3, TResult>(Type type, string methodName)
            => (Func<object, TArg1, TArg2, TArg3, TResult>)CreateMethodAccessor(type, methodName, typeof(TResult), typeof(TArg1), typeof(TArg2), typeof(TArg3));

        public static Func<object, TArg1, TArg2, TArg3, TArg4, TResult> CreateMethodAccessor<TArg1, TArg2, TArg3, TArg4, TResult>(Type type, string methodName)
            => (Func<object, TArg1, TArg2, TArg3, TArg4, TResult>)CreateMethodAccessor(type, methodName, typeof(TResult), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4));

        private static Delegate CreateMethodAccessor(Type type, string methodName, Type returnType, params Type[] argTypes)
        {
            var mi = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, argTypes, null);
            if (mi == null)
                throw new InvalidOperationException($"The type '{type}' does not have method '{methodName}' with input argument types specified");
            return CreateMethodAccessor(mi, returnType);
        }

        private static Delegate CreateMethodAccessor(MethodInfo methodInfo, Type returnType)
        {
            if (returnType != null && methodInfo.ReturnType != typeof(void) && methodInfo.ReturnType != returnType)
                throw new InvalidOperationException($"Return type mismatch for method '{methodInfo.Name}': '{returnType}' vs. '{methodInfo.ReturnType}'");

            var target = Expression.Parameter(typeof(object), "target");
            var parameters = methodInfo.GetParameters().Select(pi => Expression.Parameter(pi.ParameterType, pi.Name)).ToList();
            var instance = Expression.ConvertChecked(target, methodInfo.DeclaringType);
            var call = Expression.Call(instance, methodInfo, parameters);
            var body = (returnType != null && returnType != methodInfo.ReturnType) ? Expression.Block(call, Expression.Default(returnType)) : (Expression)call;
            parameters.Insert(0, target);
            var lambda = Expression.Lambda(body, parameters);
            var func = lambda.Compile();
            return func;
        }
    }

    internal struct PropertyAccessor<T>
    {
        private Func<object, T> _getter;
        private Func<object, T, bool> _setter;

        public PropertyAccessor(Func<object, T> getter, Func<object, T, bool> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public T Get(object target)
        {
            return _getter(target);
        }

        public void Set(object target, T value)
        {
            _setter(target, value);
        }
    }

    internal struct EventAccessor<T>
    {
        private Func<object, T, bool> _add;
        private Func<object, T, bool> _remove;

        public EventAccessor(Func<object, T, bool> add, Func<object, T, bool> remove)
        {
            _add = add;
            _remove = remove;
        }

        public void Add(object target, T @delegate)
        {
            _add(target, @delegate);
        }

        public void Remove(object target, T @delegate)
        {
            _remove(target, @delegate);
        }
    }
}
