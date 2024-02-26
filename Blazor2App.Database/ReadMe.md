--DbContext
Set default project to Blazor2App.Database
Add-Migration InitialMigration -Context DataContext

update-database -context DataContext

--Outbox
Set default project to Blazor2App.Database
Add-Migration InitialMigration -Context OutboxDbContext

Set default project back to Blazor2App.Server
update-database -context OutboxDbContext