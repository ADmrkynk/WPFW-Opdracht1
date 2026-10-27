# WPFW - Portfolio Backend (Opdracht 3)

Backend Web API voor het persoonlijke portfolio, gebouwd met **ASP.NET Core Web API**, **Entity Framework Core (EF Core)** en **SQL Server**.

---

## 🚀 Technologieën & Versies

* **.NET Versie:** .NET 10.0 (C# 13)
* **Framework:** ASP.NET Core Web API (Minimal API)
* **ORM:** Entity Framework Core 10 (`Microsoft.EntityFrameworkCore.SqlServer`)
* **Documentatie & Testing:** Swagger UI (`Swashbuckle.AspNetCore`)
* **Database:** Microsoft SQL Server

---

## 🏛️ Gekozen API-Stijl & Codestructuur

Voor deze opdracht is gekozen voor de **Minimal API** stijl, gecombineerd met een duidelijke **gelaagde architectuur (Separation of Concerns)**.

### Structuur van het project (`/Backend`)

```text
Backend/
├── Data/
│   └── PortfolioDb.cs            # EF Core DbContext met DbSets voor Projects en BlogPosts
├── DTOs/
│   ├── ProjectDtos.cs            # Invoer- (Create/Update) en Uitvoer-DTO's voor Projects
│   └── BlogPostDtos.cs           # Invoer- (Create/Update) en Uitvoer-DTO's voor BlogPosts
├── Endpoints/
│   ├── ProjectEndpoints.cs       # Minimal API route definitions & HTTP-afhandeling (/api/projects)
│   └── BlogPostEndpoints.cs      # Minimal API route definitions & HTTP-afhandeling (/api/blogposts)
├── Models/
│   ├── Project.cs                # EF Core database entiteit voor Projecten
│   └── BlogPost.cs               # EF Core database entiteit voor Blogberichten
├── Services/
│   ├── IProjectService.cs & ProjectService.cs       # Bedrijfslogica en DTO-mapping
│   └── IBlogPostService.cs & BlogPostService.cs    # Bedrijfslogica en DTO-mapping
├── appsettings.json              # Configuratie inclusief ConnectionString
└── Program.cs                    # Dependency Injection, Middleware en startup
```

### Verantwoordelijkheden per laag
1. **Endpoints (`Endpoints/`):** Behandelt inkomende HTTP-verzoeken, routeert naar de service, en retourneert passende statuscodes (`TypedResults.Ok`, `CreatedAtRoute`, `NotFound`, `NoContent`). Bevat géén databasecode.
2. **Services (`Services/`):** Bevat de applicatielogica en de omzetting tussen entiteiten en DTO's.
3. **Data (`Data/` & `Models/`):** Beheert de communicatie met SQL Server via `PortfolioDb`. Entiteiten worden nooit rechtstreeks aan de client getoond.
4. **DTO's (`DTOs/`):** Garandeert dat clients bij POST geen eigen Id kunnen forceren en dat interne velden afgeschermd blijven.

---

## 🗄️ SQL Server Configuratie & Database Initialisatie

### 1. Connection String
De connection string staat geconfigureerd in `Backend/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PortfolioDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

* **Windows (LocalDB):** De bovenstaande standaard connection string werkt direct met SQL Server LocalDB.
* **macOS / Linux (via Docker):** Start een SQL Server container:
  ```bash
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
  ```
  En pas `DefaultConnection` aan naar:
  ```json
  "DefaultConnection": "Server=localhost,1433;Database=PortfolioDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True"
  ```

### 2. Database Aanmaken (`EnsureCreated`)
Bij het opstarten van de backend in `Program.cs` wordt de database automatisch aangemaakt via EF Core's `Database.EnsureCreated()`:
```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDb>();
    db.Database.EnsureCreated();
}
```
Hierdoor hoeven er geen handmatige migraties te worden uitgevoerd; de tabellen `Projects` en `BlogPosts` worden automatisch aangemaakt zodra een geldige verbinding met SQL Server beschikbaar is.

---

## ▶️ Applicatie Starten

1. Navigeer naar de `Backend` map:
   ```bash
   cd Backend
   ```
2. Start de Web API:
   ```bash
   dotnet run
   ```
3. Open je browser en ga naar de interactieve Swagger UI:
   ```text
   http://localhost:5009/swagger
   ```
   *(of https://localhost:7107/swagger)*

---

## 📡 Beschikbare API Endpoints

### Projecten (`/api/projects`)
* `GET /api/projects` - Haal alle projecten op (retourneert `IReadOnlyList<ProjectDto>`)
* `GET /api/projects/{id}` - Haal één project op via ID (retourneert `200 OK` of `404 Not Found`)
* `POST /api/projects` - Maak een nieuw project aan (ontvangt `CreateProjectDto`, retourneert `201 Created`)
* `PUT /api/projects/{id}` - Wijzig een bestaand project (ontvangt `UpdateProjectDto`, retourneert `204 NoContent` of `404 Not Found`)
* `DELETE /api/projects/{id}` - Verwijder een project (retourneert `204 NoContent` of `404 Not Found`)

### Blogposts (`/api/blogposts`)
* `GET /api/blogposts` - Haal alle blogposts op (retourneert `IReadOnlyList<BlogPostDto>`)
* `GET /api/blogposts/{id}` - Haal één blogpost op via ID (retourneert `200 OK` of `404 Not Found`)
* `POST /api/blogposts` - Maak een nieuw blogbericht aan (ontvangt `CreateBlogPostDto`, retourneert `201 Created`)
* `PUT /api/blogposts/{id}` - Wijzig een blogbericht (ontvangt `UpdateBlogPostDto`, retourneert `204 NoContent` of `404 Not Found`)
* `DELETE /api/blogposts/{id}` - Verwijder een blogbericht (retourneert `204 NoContent` of `404 Not Found`)