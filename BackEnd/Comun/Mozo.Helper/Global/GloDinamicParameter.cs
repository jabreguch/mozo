using Dapper;

using Microsoft.AspNetCore.Http;

using System.Collections.Concurrent;
using System.Data;
using System.Reflection;
using System.Security.Claims;

namespace Mozo.Helper.Global;

public static partial class BuildDinamicParameter
{
    public static T? ClaimsCredential<T>(this T model, HttpContext ctx)
    {

        List<string> values = new() { "NoUsuario", "CoEmpresa", "CoPersona", "CoPermiso", "CoIngreso" };

        if (ctx.User != null)
        {
            IEnumerable<Claim> claims = ctx.User.Claims;
            if (claims != null)
            {
                PropertyInfo? propertyCredential = model!.GetType().GetProperty("Credencial");
                object credencial = Activator.CreateInstance(propertyCredential!.PropertyType)!;
                propertyCredential.SetValue(model, credencial, null);
                foreach (Claim claim in claims)
                {
                    string? key = values.Find(x => x.Equals(claim.Type));
                    if (key != null)
                    {
                        PropertyInfo? property = credencial!.GetType().GetProperty(key);
                        if (property != null)
                        {
                            object? value = claim.Value;
                            var propertyType = property.PropertyType;
                            var targetType = Glo.IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
                            if (value != null) value = Convert.ChangeType(value, targetType!);
                            property.SetValue(credencial, value, null);

                        }
                    }
                }
            }
        }

        return model;
    }



    private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> _propCache = new();

    private static Dictionary<string, PropertyInfo> GetProps(Type type)
        => _propCache.GetOrAdd(type, modelType =>
            modelType.GetProperties().ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase));

    public static (DynamicParameters Parameters, string SqlArgs) Build<T>(T model, int? coEmpresa, int? coPersona, params string[] propertyNames)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        if (propertyNames == null || propertyNames.Length == 0)
            throw new ArgumentException("Debe indicar al menos un parámetro.", nameof(propertyNames));

        bool requiresUserContext = propertyNames.Any(name =>
            string.Equals(name, "CoEmpresa", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(name, "CoPersona", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(name, "CoUsuCre", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(name, "CoUsuMod", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(name, "CoUsuEli", StringComparison.OrdinalIgnoreCase));

        if (requiresUserContext && (!coEmpresa.HasValue || !coPersona.HasValue || coEmpresa <= 0 || coPersona <= 0))
            throw new UnauthorizedAccessException("Contexto de usuario requerido: valores de CoEmpresa y CoPersona deben ser mayores a 0");

        DynamicParameters parameters = new();
        List<string> sqlArgs = new();
        Dictionary<string, PropertyInfo> props = GetProps(typeof(T));

        foreach (string name in propertyNames)
        {
            if (string.IsNullOrWhiteSpace(name))
                continue;

            string parameterName = NormalizeParameterName(name);

            if (name == "CoEmpresa" && coEmpresa != null)
            {
                Add2(parameters, parameterName, coEmpresa, DbType.Int32);
                sqlArgs.Add(parameterName);
            }
            else if ((name == "CoUsuCre" || name == "CoUsuMod" || name == "CoUsuEli") && coPersona != null)
            {
                Add2(parameters, parameterName, coPersona, DbType.Int32);
                sqlArgs.Add(parameterName);
            }
            else
            {
                if (!props.TryGetValue(name, out PropertyInfo? prop)) continue;

                object? value = prop.GetValue(model);
                DbType dbType = ToDbType(prop.PropertyType);
                Add2(parameters, parameterName, value, dbType);
                sqlArgs.Add(parameterName);
            }
        }

        return (parameters, string.Join(",", sqlArgs));
    }

    public static void Add2(this DynamicParameters parameters, string name, object? value, DbType dbType)
    {
        if (parameters == null)
            throw new ArgumentNullException(nameof(parameters));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del parámetro es obligatorio.", nameof(name));

        string parameterName = NormalizeParameterName(name);
        parameters.Add(parameterName, value ?? DBNull.Value, dbType);
    }


    private static DbType ToDbType(Type propertyType)
    {
        Type type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
        return type switch
        {
            Type t when t == typeof(string) => DbType.String,
            Type t when t == typeof(int) => DbType.Int32,
            Type t when t == typeof(decimal) => DbType.Decimal,
            Type t when t == typeof(bool) => DbType.Boolean,
            Type t when t == typeof(DateTime) => DbType.DateTime,
            Type t when t == typeof(long) => DbType.Int64,
            Type t when t == typeof(short) => DbType.Int16,
            Type t when t == typeof(double) => DbType.Double,
            Type t when t == typeof(float) => DbType.Single,
            Type t when t == typeof(Guid) => DbType.Guid,
            _ => DbType.Object
        };
    }

    private static string NormalizeParameterName(string name)
    {
        return name.StartsWith("@", StringComparison.Ordinal) ? name : $"@{name}";
    }

    public static T Mapper<T>(this object obj) //where T : new()
    {
        if (obj == null)
            return default!;
        //Assembly assem = typeof(T).Assembly;
        ////T instance = null System.DBNull.Value;
        ////T instance;

        //return obj.GetType().MakeByRefType();

        Type t = obj.GetType();


        //return Activator.CreateInstance<T>();


        //return (T)new object { };


        IEnumerable<PropertyInfo> InfoList = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //.Where(p => p.GetCustomAttributes(typeof(BasicAttrAttribute), false).Count() == 1);

        T NewObjec = Activator.CreateInstance<T>();
        //T NewObjec = new  default(T); // obj.GetType();

        Type t2 = NewObjec!.GetType();

        foreach (var Info in InfoList)
        {
            var Value = Info.GetValue(obj, null);

            var propertyType = Info.PropertyType;
            var targetType = Glo.IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
            if (Value != null) Value = Convert.ChangeType(Value, targetType);

            PropertyInfo PropertySource = t2.GetProperty(Info.Name)!;

            PropertySource!.SetValue(NewObjec, Value, null);
        }

        //object ret = prop != null ? prop.GetValue(obj, null) : null;

        return NewObjec;
    }

}
