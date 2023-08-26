using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyPhillReminder_APIIssak_melany.Attributes
{
    //esta clase ayuda a limitar la forma en que se puede consumir un recurso de controlador (un rnd point)
    //basicamente vamos a crear una decoracion personalizada que incluya cierta funcionalidad
    //ya sea a todo un controller o a un end point en particular

    [AttributeUsage(validOn:AttributeTargets.All)]
    public sealed class ApiKeyAttribute: Attribute,IAsyncActionFilter
    {
        //espicificamos cual es la clave : valor dentro del appsettings que queremos usar como ApiKey
        private readonly string _apiKey = "Progra6ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            //aca validamos que en el body (en tipo json) del request valla la info del ApiKey
            //si no va la info presentamos un mensaje de error indicando que falta ApiKey
            //y que no se puede consumir el recurso
            
            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey,out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Llamada no contiene informacion de seguridad..."
                };
                return;

                //si no hay info sale de la funcion y muestra este mensaje 
            }

            //si viene info de seguridad falta validar que sea la correcta para esto 
            //lo primero es extraer el valor de Progra6ApiKey dentro de appsettings.json
            //para poder comparar contra lo que viene en el request
            var AppSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = AppSettings.GetValue<string>(_apiKey);

            //queda comparar que las apikey sean iguales
            if (!ApiKeyValue.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey Invalida..."
                };
                return;
            }
            await next();


        }
    }
}
