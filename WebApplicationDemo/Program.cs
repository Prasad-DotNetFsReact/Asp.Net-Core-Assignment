namespace WebApp { 

    public class Program {

        public static void Main()
        {

            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("hello"))
                {
                    await context.Response.WriteAsync("hello1 ");
                    await next.Invoke();
                }
                else
                {
                    await next.Invoke();
                }
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("hello"))
                {
                    await context.Response.WriteAsync("hello2 ");
                    await next.Invoke();
                }
                else
                {
                    await next.Invoke();
                }
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("end"))
                {
                    await context.Response.WriteAsync("Terminating chain...");
                    return;
                }
                else
                {
                    await next.Invoke();
                }
            });

            app.Run(async (context) =>
            {
                if (!context.Request.Path.Value.Contains("hello") && !context.Request.Path.Value.Contains("end"))
                {
                    await context.Response.WriteAsync("Default response if not matched.");
                }
            });

            app.Run();

        }
    }

}




