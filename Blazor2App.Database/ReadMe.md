--DbContext
Set default project to Blazor2App.Database
Add-Migration InitialMigration -Context DataContext

dotnet ef database update --project .\Blazor2App.Server

--Outbox
Set default project to Blazor2App.Database
Add-Migration InitialMigration -Context OutboxDbContext

Set default project back to Blazor2App.Server
update-database -context OutboxDbContext