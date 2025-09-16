ASP.NET Core Web API .NET 9.08 – Developer Documentation
1. Project Setup & Installation
Video: Create Project + Install
Install .NET SDK 9.0.8
Install Visual Studio 2022 or later

Create a new Web API project:
bash
dotnet new webapi -n MyWebApi
cd MyWebApi
Run the project and verify Swagger UI at https://localhost:5001/swagger

2. Define Models
Video: Models
Create model classes (e.g., Stock.cs) in the Models folder
Use data annotations for validation:

csharp
public class Stock {
    public int Id { get; set; }
    [Required]
    public string Symbol { get; set; }
}

3. Set Up Entity Framework Core
Video: Entity Framework
Install EF Core packages:

bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
Create AppDbContext and configure in Program.cs:

csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
4. Create Controllers
Video: Controllers
Add a new controller (e.g., StockController.cs) in Controllers
Inject AppDbContext via constructor
Implement basic HttpGet endpoint:

csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<Stock>>> GetStocks() {
    return await _context.Stocks.ToListAsync();
}

5. Use DTOs
Video: DTOs
Create DTO classes to shape API responses
Map models to DTOs manually or with AutoMapper

Example:

csharp
public class StockDto {
    public string Symbol { get; set; }
    public decimal Price { get; set; }
}

6. Implement POST (Create)
Video: POST (Create)
Add HttpPost method to controller
Accept DTO as input, validate, and save to DB
Return CreatedAtAction with new resource

7. Implement PUT (Update)
Video: PUT (Update)
Add HttpPut method
Check if entity exists, update fields, and save changes
Return NoContent on success

8. Implement DELETE
Video: DELETE
Add HttpDelete method
Find entity by ID, remove from DB, and return Ok

9. Use Async/Await
Video: Async/Await
Make all DB calls asynchronous using await and ToListAsync, FindAsync, etc.
Improves scalability and responsiveness

10–11. Repository Pattern + Refactor
Videos: Repository Pattern + DI, Refactor to Repository
Create repository interfaces and classes
Inject repositories into controllers
Decouple data access logic from controllers

12–16. Comment System (One-to-Many)
Videos: Comment System, GET + Include(), CREATE, UPDATE, DELETE
Create Comment model with foreign key to Stock
Use Include() to fetch related comments
Implement CRUD for comments

17. Data Validation
Video: Data Validation
Use [Required], [StringLength], [Range] attributes
Validate DTOs before saving
Return BadRequest with validation errors

18–20. Filtering, Sorting, Pagination
Videos: Filtering, Sorting, Pagination
Add query parameters to GET endpoints
Use LINQ to filter, sort, and paginate results

Example:

csharp
var stocks = _context.Stocks
    .Where(s => s.Symbol.Contains(query))
    .OrderBy(s => s.Price)
    .Skip((page - 1) * pageSize)
    .Take(pageSize);
    
21–24. Identity & JWT Authentication
Videos: Install Identity, Register, Token Service, Login
Add ASP.NET Core Identity
Configure JWT authentication
Create endpoints for user registration and login
Secure endpoints with [Authorize]

25. Many-to-Many Relationships
Video: Many-To-Many
Create join entity (e.g., UserStock)
Configure relationships in DbContext
Use Include() and ThenInclude() to fetch related data

26–28. Portfolio CRUD
Videos: Portfolio GET, CREATE, DELETE
Create Portfolio model linked to User
Implement endpoints to manage user portfolios

29. One-to-One Relationships
Video: One-To-One
Define one-to-one relationship (e.g., UserProfile)
Configure with HasOne().WithOne() in DbContext

30. User-Generated Content
Video: User Generated Content
Allow authenticated users to create and manage their own content
Use User.Identity.Name to associate data with users
![Output](https://github.com/user-attachments/assets/6f0eca69-b676-42b1-8688-568bb278c931)
![Output](https://github.com/user-attachments/assets/bacd6090-2aae-41c4-9973-e294e82803a4)
