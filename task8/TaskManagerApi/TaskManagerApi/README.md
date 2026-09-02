# Task Manager API — both assignments

Complete source for ASP.NET Core 10, SQL Server, EF Core, repository/service interfaces, AutoMapper, DTOs and Swagger UI. Both bonuses are included.

## .NET 10 update

Targets net10.0 and uses EF Core/SQL Server/dotnet-ef 10.0.11. Your reported .NET 10.0.11 runtime matches this target; .NET 8 is not required. Building requires a .NET 10 SDK (check `dotnet --list-sdks`), and running this web app requires Microsoft.AspNetCore.App 10.0.x (check `dotnet --list-runtimes`). The SDK includes both runtimes.

Extract into a fresh folder so old bin/obj files are not reused. Keep any custom SQL Server connection string you previously configured. The database is created by the migration command, not by merely building the app. In SSMS connect to the SAME server as the connection string, then refresh Databases and expand TaskManagerDb → Tables → dbo.Tasks.

AutoMapper is upgraded to 15.1.1, a patched version for GHSA-rvv3-g6hj-g44x. Its DI registration is updated too. AutoMapper 15 requires a license; obtain the applicable license from https://automapper.io and supply the key through the `AutoMapper__LicenseKey` environment variable. No license key is bundled. License warnings must not be confused with database or runtime errors.

The existing migration IDs and table schema are preserved. If both migrations were already applied, they should not be reapplied. To check model/snapshot consistency on your machine, run `dotnet ef migrations has-pending-model-changes` after restoring and building. Runtime and migration verification remain pending because the preparation environment has no .NET SDK or SQL Server.

## Run it

1. Install the .NET 10 SDK and SQL Server (Developer or Express is suitable). SSMS is a database client; you also need the SQL Server engine running.
2. Extract the ZIP and open `TaskManagerApi.csproj` in Visual Studio, or open a terminal inside the `TaskManagerApi` folder.
3. Edit `appsettings.json` → `ConnectionStrings:Default`. The supplied `Server=.` targets the local default SQL Server instance using your Windows login. For Express, use `Server=.\SQLEXPRESS` (escape the backslash as `\\` inside JSON). For LocalDB, use `Server=(localdb)\MSSQLLocalDB`, likewise escaped in JSON. `TrustServerCertificate=True` is intended for your local development database.
4. Run these commands from the folder containing the project:

```powershell
Unblock-File -LiteralPath ".\.config\dotnet-tools.json"
dotnet restore
dotnet tool restore
dotnet build
dotnet ef database update
dotnet run --launch-profile http
```

5. Open http://localhost:5080/swagger. Choose an endpoint → Try it out → Execute.

The two migrations are already included. Do not add another `InitialCreate`. `dotnet ef database update` applies both migrations to your SQL Server database. There is no `EnsureCreated` or in-memory storage.

## How the layers work

The controller receives HTTP requests and calls `ITaskService`. `TaskService` uses `IMapper` to convert request/response DTOs and calls `ITaskRepository`. `TaskRepository` uses `AppDbContext` to query and save SQL Server records.

The second assignment supersedes the first assignment's direct service-to-context dependency. Consequently, `TaskService` has no `AppDbContext` reference in this final implementation. Both repository and service are registered as scoped.

The screenshots do not specify the entire original entity. This implementation assumes Id, Title, Description, IsCompleted, CreatedAt, plus the bonus UpdatedAt. A new task always starts incomplete. PUT changes completion status.

## Five endpoints

| Method | URL | Success |
| --- | --- | --- |
| GET | /api/tasks | 200, paginated TaskSummaryDto items |
| GET | /api/tasks/{id} | 200, TaskItemDto |
| POST | /api/tasks | 201, TaskItemDto and Location header |
| PUT | /api/tasks/{id} | 200, updated TaskItemDto |
| DELETE | /api/tasks/{id} | 204, no body |

Unknown IDs return 404. Invalid bodies or query parameters return 400 automatically through `[ApiController]` validation. Title is required, cannot be whitespace-only, and is limited to 200 characters; Description is limited to 2,000. Page starts at 1, pageSize is 1–100. Page is capped at 1,000,000 to keep the SQL offset calculation bounded.

POST example:

```json
{"title":"Team meeting","description":"Discuss the project"}
```

PUT example:

```json
{"title":"Team meeting","description":"Meeting finished","isCompleted":true}
```

Filter example:

```text
GET /api/tasks?search=meeting&isCompleted=false&page=1&pageSize=5
```

Example list shape (actual IDs/counts vary):

```json
{
  "items": [{"id":1,"title":"Team meeting","isCompleted":false}],
  "totalCount":1,
  "page":1,
  "pageSize":5,
  "totalPages":1
}
```

Search and completion filters combine with AND. Blank search is ignored. Title has an explicit SQL Server case-insensitive collation. The repository runs CountAsync on the filtered query, followed by an ordered Skip/Take query; it does not load all rows into memory. A page beyond the last page returns empty items with the matching totalCount. The two queries do not provide a point-in-time snapshot during concurrent writes.

All mappings between requests, entities and response DTOs are in MappingProfile. CreatedAt is assigned using ForMember during creation. PUT preserves Id and CreatedAt; the service sets UpdatedAt to UTC. Unknown JSON properties such as id and createdAt are ignored because those fields are absent from the request DTO. The list endpoint uses the summary DTO bonus without adding a duplicate GET route.

## Acceptance checks in Swagger

1. POST a task named `Team meeting`, then GET it by the returned ID. Confirm 201, Location, server-generated ID and CreatedAt.
2. POST with extra `"id":999999,"createdAt":"2000-01-01T00:00:00Z"`. Confirm the server supplies its own ID and timestamp.
3. POST an unrelated task and another meeting. Query `search=MEETING&page=1&pageSize=1`; only meeting tasks should match, and totalCount should count all matching tasks across pages. Try page 2.
4. PUT one meeting with isCompleted=true. Confirm CreatedAt stays unchanged and UpdatedAt becomes populated. Filter by isCompleted=true and false.
5. Stop the app with Ctrl+C, restart it, and GET a previously created task. It must still exist.
6. DELETE a task, then GET that ID. Expect 204 followed by 404. PUT and DELETE for that missing ID should also return 404.
7. Submit an empty/whitespace title, page=0, or pageSize=101. Expect 400.
8. In SSMS, inspect Tasks and __EFMigrationsHistory; both migrations should appear.

## Verify the second migration on existing data

Use a NEW, disposable database name such as `TaskManagerMigrationDemo` in the connection string. Do not downgrade your populated working database. Before running the application:

```powershell
dotnet ef database update InitialCreate
```

In SSMS, select that demo database and run:

```sql
INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)
VALUES (N'Keep this task', N'Created before UpdatedAt', 0, SYSUTCDATETIME());
SELECT * FROM Tasks;
```

Then run:

```powershell
dotnet ef database update
```

Repeat the SELECT. The same row and ID should remain, and UpdatedAt should be NULL. Start the API, PUT that ID, and confirm UpdatedAt is populated. The migration adds a nullable column and does not recreate or clear the table. Only run the final application after BOTH migrations are applied, because the final entity expects UpdatedAt.

## If your instructor asks you to generate migrations yourself

The included migration classes and snapshots were prepared as source, not generated by a running SDK here. In a separate fresh project/database, first omit UpdatedAt from the entity, DTO and mappings/service as appropriate, run `dotnet ef migrations add InitialCreate`, and apply it. Then restore UpdatedAt, run `dotnet ef migrations add AddUpdatedAt`, and apply it. Do not delete applied migrations from your working project.

## Validation status

Source structure, JSON files and package references were checked. AutoMapper configuration is also validated at application startup. This build environment has no .NET SDK or SQL Server, so compilation, Swagger execution, database persistence and migration application have NOT been verified here. Follow the checks above on your machine.

## References

- SQL Server EF provider: https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/10.0.11
- EF pagination and ordering: https://learn.microsoft.com/en-us/ef/core/querying/pagination
- AutoMapper 15 registration: https://docs.automapper.io/en/stable/15.0-Upgrade-Guide.html
- Swagger package: https://www.nuget.org/packages/Swashbuckle.AspNetCore/10.0.1
