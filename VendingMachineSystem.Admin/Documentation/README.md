DB Connection

```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet ef dbcontext scaffold "server=.;database=VendingMachine;user id=sa;password=sa@123;TrustServerCertificate=true;MultipleActiveResultSets=True;", Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c ApplicationDbContext -f

```