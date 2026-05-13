using Mozo.Model.Interfaces;

using System.Reflection;

namespace Mozo.ApiSeguridad.Helper;



public static class ClaimsExtensions
{
    public static T SetClaims<T>(this T model, UserClaims user, bool flEmpresa = true) where T : IBaseEntityModel
    {
        model.CoUsuCre = user.CoPersona;
        model.CoUsuMod = user.CoPersona;
        model.CoUsuEli = user.CoPersona;
        if (flEmpresa)
            model.CoEmpresa = user.CoEmpresa;
        return model;
    }



    private static void SetIfExists<T>(T model, string propertyName, object? value)
    {
        PropertyInfo? prop = typeof(T).GetProperty(propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (prop != null && value != null)
            prop.SetValue(model, value);
    }

    public static T ToModel<T>(this object source, UserClaims user, bool flEmpresa = true)
    {
        T target = Activator.CreateInstance<T>();


        int? coEmpresa = user.CoEmpresa;
        int? coPersona = user.CoPersona;

        // Mapear propiedades comunes
        SetIfExists(target, "CoUsuCre", coPersona);
        SetIfExists(target, "CoUsuMod", coPersona);
        SetIfExists(target, "CoUsuEli", coPersona);
        if (flEmpresa)
            SetIfExists(target, "CoEmpresa", coEmpresa);


        Type? tSource = source.GetType();
        Type? tTarget = target.GetType();
        //string[] PropertyCol =
        //{
        //    "CoEmpresa", // Property to enterprice
        //    "PageSize", "PageIndex", // Property to pagination
        //    "FeInicio", "FeFinal","NoInputSearch", // Property to Search
        //    "CoEstReg"// Property to Status
        //};

        foreach (PropertyInfo propertySource in tSource.GetProperties())
        {
            //var PropertyTarget = tTarget.GetProperty(NoProperty);
            //var PropertySource = tSource.GetProperty(NoProperty);

            PropertyInfo? propertyTarget = tTarget.GetProperty(propertySource.Name);

            if (propertyTarget != null)
            {
                object? value = propertySource.GetValue(source, null);

                //if (PropertyTarget.Name == "FeInicio" || PropertyTarget.Name == "FeFinal")
                //{
                //    Value = Value.Text().FechaBd();
                //}

                Type? propertyType = propertySource.PropertyType;
                Type? targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
                if (value != null) value = Convert.ChangeType(value, targetType);
                propertyTarget.SetValue(target, value, null);
            }
        }
        return target;
    }

    private static bool IsNullableType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));


}

//public static class ModelSettingsExtensions
//{
//    public static T? SetClaims<T>(this T m, UserClaims user, bool flEmpresa = true) where T : class
//    {
//        int? CoPersona = user.CoPersona;
//        int? CoEmpresa = user.CoEmpresa;
//        Type model = m.GetType();
//        // Mapear propiedades comunes
//        model.GetProperty("CoUsuCre")?.SetValue(m, CoPersona);
//        model.GetProperty("CoUsuMod")?.SetValue(m, CoPersona);
//        model.GetProperty("CoUsuEli")?.SetValue(m, CoPersona);
//        if (flEmpresa)
//            model.GetProperty("CoEmpresa")?.SetValue(m, CoEmpresa);

//        return m;
//    }


//}



//public class T> : BaseFilter<T>
//    where T : new()
//{


//    public static async ValueTask<T>> BindAsync(HttpContext context)
//    {
//        T> filter = new();

//        // aquí igual que antes: mapear query, route y body a las propiedades del filtro
//        foreach (KeyValuePair<string, StringValues> kv in context.Request.Query)
//        {
//            PropertyInfo? prop = typeof(T).GetProperty(kv.Key,
//                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

//            if (prop != null)
//            {
//                object? converted = Convert.ChangeType(kv.Value.ToString(), prop.PropertyType);
//                prop.SetValue(filter, converted);
//            }
//        }

//        return filter;
//    }
//}


