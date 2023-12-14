Set default project to Blazor2App.Database
Add-Migration InitialMigration -Context DataContext

dotnet ef database update --project .\Server