// Objective:Design, implement, and secure middleware components within an ASP.NET Core Web API, ensuring they meet performance and security requirements.
// Write the middleware examples using app.Use inline delegates


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Simulated HTTPS Enforcement
app.Use(async (context, next) =>
{
    if (context.Request.Query["secure"] != "true")
    {
        Console.WriteLine($"[SECURITY] Blocked insecure request: {context.Request.Path}");
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("Simulated HTTPS required.\n\n// Simulated HTTPS Enforcement (FAILED)");
        return; // short-circuit
    }
    await next();
});

// Authentication Checks (simulated with query parameter for demo)
app.Use(async (context, next) =>
{
    // Simulate authentication for demo purposes
    bool isAuthenticated = context.Request.Query["authenticated"] == "true";
    
    // Skip authentication for specific demo paths
    if (context.Request.Path == "/" || context.Request.Path.StartsWithSegments("/test"))
    {
        await next();
        return;
    }
    
    if (!isAuthenticated)
    {
        Console.WriteLine($"[SECURITY] Authentication failed: {context.Request.Path}");
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Authentication required.\n\n// Simulated HTTPS Enforcement\n// Authentication Checks (FAILED)");
        return;
    }
    await next();
});

// Short-Circuit Unauthorized Access
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/unauthorized"))
    {
        Console.WriteLine($"[SECURITY] Unauthorized access attempt: {context.Request.Path}");
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Unauthorized Access.\n\n// Simulated HTTPS Enforcement\n// Authentication Checks\n// Short-Circuit Unauthorized Access (FAILED)");
        return;
    }
    await next();
});

// Input Validation
app.Use(async (context, next) =>
{
    var input = context.Request.Query["input"];
    // Only block if input contains malicious scripts, allow empty or valid input
    if (!string.IsNullOrEmpty(input) && input.Contains("<script>"))
    {
        Console.WriteLine("[SECURITY] Validation failed: unsafe input");
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync("Invalid Input.\n\n// Simulated HTTPS Enforcement\n// Authentication Checks\n// Short-Circuit Unauthorized Access\n// Input Validation (FAILED)");
        return;
    }
    await next();
});

// Asynchronous Processing
app.Use(async (context, next) =>
{
    await Task.Delay(100); // simulate async I/O
    Console.WriteLine("[PERFORMANCE] Request processed asynchronously.");
    await next();
});

// Security Event Logging (example usage inside pipeline)
app.Use(async (context, next) =>
{
    Console.WriteLine($"[SECURITY LOG] Path: {context.Request.Path}, IP: {context.Connection.RemoteIpAddress}");
    await next();
});

// Add a simple endpoint to test the middleware
app.MapGet("/", () => "Middleware Security Demo - All checks passed!\n\n// Short-Circuit Unauthorized Access\n// Input Validation\n// Asynchronous Processing\n// Security Event Logging");
app.MapGet("/test", () => "Test endpoint reached successfully!\n\n// Simulated HTTPS Enforcement\n// Short-Circuit Unauthorized Access\n// Input Validation\n// Asynchronous Processing\n// Security Event Logging");
app.MapGet("/api/data", () => "API Data endpoint - All security checks passed!\n\n// Simulated HTTPS Enforcement\n// Authentication Checks\n// Short-Circuit Unauthorized Access\n// Input Validation\n// Asynchronous Processing\n// Security Event Logging");
app.MapGet("/api/secure", () => "Secure API endpoint - Authentication successful!\n\n// Simulated HTTPS Enforcement\n// Authentication Checks\n// Short-Circuit Unauthorized Access\n// Input Validation\n// Asynchronous Processing\n// Security Event Logging");

app.Run();
