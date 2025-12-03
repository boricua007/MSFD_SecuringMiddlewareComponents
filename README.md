# MSFD Securing Middleware Components

.NET 9.0 Web Application demonstrating custom middleware design, security implementation, and pipeline optimization for the Microsoft Full Stack Developer certification.

## Quick Start

```bash
dotnet run
```

## Features

✅ **Custom Security Middleware Pipeline** - Sequential middleware components with comprehensive security validation  
✅ **HTTPS Enforcement Simulation** - Query parameter-based security validation  
✅ **Authentication Middleware** - Simulated user authentication with bypass capabilities  
✅ **Authorization Controls** - Path-based access control and route protection  
✅ **Input Validation & Sanitization** - XSS prevention and malicious input detection  
✅ **Async Request Processing** - Non-blocking request handling with performance monitoring  
✅ **Security Event Logging** - Comprehensive request tracking and security monitoring  

**Tech Stack:** .NET 9.0 • C# • ASP.NET Core • Kestrel • Security Middleware Pipeline

## Middleware Pipeline

| Component | Purpose | Validation | Security Feature |
|-----------|---------|------------|------------------|
| **HTTPS Enforcement** | Security validation | Query parameter check | Enforces secure connections |
| **Authentication** | User verification | Credential validation | User identity confirmation |
| **Authorization** | Access control | Path-based blocking | Route protection |
| **Input Validation** | Sanitization | Malicious input detection | XSS prevention |
| **Async Processing** | Request handling | async/await | Performance optimization |
| **Security Logging** | Monitoring | Event tracking | Audit trail |

## Usage Examples

### Testing the Middleware Pipeline

```bash
# Valid request (all middleware passes)
curl "http://localhost:5000/api/data?secure=true&authenticated=true&input=validtext"

# Missing secure parameter (fails HTTPS middleware)
curl "http://localhost:5000/api/data?authenticated=true&input=validtext"

# Missing authentication (fails authentication middleware)
curl "http://localhost:5000/api/secure?secure=true&input=validtext"

# Invalid input (fails validation middleware)
curl "http://localhost:5000/api/data?secure=true&authenticated=true&input=<script>alert('xss')</script>"

# Unauthorized route (fails authorization middleware)
curl "http://localhost:5000/unauthorized?secure=true&authenticated=true&input=validtext"
```

### Middleware Configuration

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware executes in order of registration
app.Use(async (context, next) => { /* HTTPS Enforcement */ });
app.Use(async (context, next) => { /* Authentication Checks */ });
app.Use(async (context, next) => { /* Authorization Control */ });
app.Use(async (context, next) => { /* Input Validation */ });
app.Use(async (context, next) => { /* Async Processing */ });
app.Use(async (context, next) => { /* Security Logging */ });

app.Run();
```

### Input Validation Logic

```csharp
bool IsValidInput(string input)
{
    return string.IsNullOrEmpty(input) || 
           (!input.Contains("<script>"));
}
```

## Middleware Components

**HTTPS Enforcement** - Validates secure connections via query parameter  
**Authentication** - Simulates user authentication with bypass for demo paths  
**Authorization** - Blocks access to restricted routes like `/unauthorized`  
**Input Validation** - Prevents XSS attacks by blocking `<script>` tags  
**Async Processing** - Simulates async I/O with 100ms delay  
**Security Logging** - Logs request paths and IP addresses  

## Learning Objectives

• **Middleware Design:** Custom ASP.NET Core middleware components
• **Security Implementation:** Authentication, authorization, input validation
• **Pipeline Optimization:** Execution order and short-circuiting
• **Async Programming:** Non-blocking request handling
• **MSFD Certification:** Security and middleware optimization competencies  

## Project Structure

```
MSFD_SecuringMiddlewareComponents/
├── Program.cs                                    # Main application with security middleware pipeline
├── MSFD_SecuringMiddlewareComponents.csproj     # Project configuration
├── MSFD_SecuringMiddlewareComponents.sln        # Solution file
├── request.http                                 # HTTP test requests with security scenarios
├── appsettings.json                            # Application settings
├── appsettings.Development.json                # Development settings
├── Properties/
│     └── launchSettings.json                     # Launch configuration
├── bin/                                        # Compiled binaries
├── obj/                                        # Build artifacts
└── README.md                                   # This file
```

## Development Workflow

1. **Start the Application:** `dotnet run`
2. **Test Security Middleware Components:**

   • Send valid requests with proper security parameters
   • Test HTTPS enforcement by omitting `secure=true`
   • Test authentication by omitting `authenticated=true`
   • Attempt XSS injection to verify input validation
   • Access `/unauthorized` route to test authorization

3. **Study Security Pipeline:**

   • Examine `Program.cs` for security pipeline configuration
   • Understand short-circuiting behavior in security contexts
   • Test middleware execution sequence with security validations

## Getting Started

1. Clone or download the project
2. Navigate to project directory: `cd MSFD_SecuringMiddlewareComponents`
3. Restore dependencies: `dotnet restore`
4. Build the application: `dotnet build`
5. Run the application: `dotnet run`
6. Test the security middleware pipeline:

   • Use the `request.http` file in VS Code with REST Client extension
   • Or use `curl` commands to test different security scenarios
   • Monitor console output for security middleware execution

## Key Concepts Demonstrated

• **Security-First Middleware Pipeline:** Sequential execution with comprehensive security validation
• **Defense in Depth:** Multi-layer security checks before processing
• **Security Event Handling:** Appropriate HTTP status codes for different security failures
• **Async Security Operations:** Non-blocking security validation for better performance
• **Security Monitoring:** Comprehensive logging and audit trail for security events  

## Test Scenarios

| Test Case | Endpoint | Expected Result | Status |
|-----------|----------|----------------|--------|
| Valid Request | `/api/data?secure=true&authenticated=true&input=valid` | All security checks pass | 200 |
| Missing HTTPS | `/api/data?authenticated=true&input=valid` | HTTPS required | 403 |
| Missing Auth | `/api/secure?secure=true&input=valid` | Authentication required | 401 |
| Invalid Input | `/api/data?secure=true&authenticated=true&input=<script>` | Invalid Input | 400 |
| Unauthorized Route | `/unauthorized?secure=true&authenticated=true&input=valid` | Unauthorized Access | 401 |

---

.NET 9.0 Web Application built for the Microsoft Back-End Development course as part of the Full-Stack Certification track. This app demonstrates custom security middleware design, comprehensive security patterns, and secure pipeline optimization - essential skills for back-end developers building secure and performant web applications.