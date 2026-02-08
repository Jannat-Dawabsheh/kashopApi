namespace kashop.pl.Middleware
{
    public class CustomMidleware
    {
        private readonly RequestDelegate _next;

        public CustomMidleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("proccessing request");
            await _next(context);
            Console.WriteLine("proccessing response");
        }
    }
}
