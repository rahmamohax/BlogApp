# BlogApi

ASP.NET Core **Blog API** built with a layered architecture (Domain → Persistence → Services → Web API).

## Features

### Authentication / Identity (JWT)
- **Register / Login** with ASP.NET Core Identity
- **JWT bearer tokens** returned by auth endpoints
- Roles are seeded (`Admin`, `User`) and newly registered users are assigned the `User` role
- Some endpoints are protected with `[Authorize]` (e.g. `GET /api/categories`, `GET /api/authentication/CurrentUser`)

### Posts
- **Create post** (starts as `Draft`)
  - Category must exist
  - Persists via EF Core repository
- **Update post**
  - Post must exist
  - Archived posts cannot be updated (read-only)
  - Updates mutable fields (title/content/category)
- **Hard delete post**
  - Post must exist
- **Get posts**
  - `GET /api/posts` returns a list of `PostDto`
  - `GET /api/posts/{id}` returns `PostDetailsDto` including comments
- **Publish / Archive**
  - Cannot publish an archived post
  - Cannot publish an already published post
  - Cannot archive an already archived post

### Categories
- **Create category**
  - Enforces unique name (service checks existing names)
- **Get categories**
  - Returns DTOs
- **Delete category**
  - Only deletes when the category has **no posts** (checked via repository `HasPostsAsync`)

### Comments
- **Add comment to a post**
  - Post must exist
  - Cannot add comments to an archived post
- **Get comments for a post**
  - Returns DTOs
- **Delete comment**
  - Comment must exist

## Backend Implementation Details

### Solution layout
- **`ApplicationLayer/`** (`Blog.Domain`)
  - Entities: `Post`, `Category`, `Comment`, base `BaseEntity`
  - Identity entity: `ApplicationUser`
  - Contracts (repository + seed interfaces): `IPostRepository`, `ICategoryRepository`, `ICommentRepository`, `IDataSeeder`, `IIdentityDataSeeder`
- **`Blog.Persistence/`**
  - EF Core `BlogDbContext` (includes Identity tables via `IdentityDbContext<ApplicationUser>`)
  - Entity configurations (`Data/Configurations/*Config.cs`)
  - Repositories (`Repositories/*Repository.cs`)
  - Migrations (`Data/Migrations`)
  - Data seeding (`Data/DataSeed`)
- **`Blog.Service/`** + **`Blog.Service.Abstraction/`**
  - Business logic & rules (services)
  - DTO mapping
  - JWT token creation in `AuthenticationService`
- **`BlogApi/`** (`BlogApi.Web`)
  - Controllers and DI wiring
  - Swagger enabled in Development

### DTOs
Located in `Blog.Shared`:
- `PostDto` (summary/list)
- `PostDetailsDto` (extends `PostDto`, includes `Comments`)
- `CreateOrUpdatePostDto`
- `CategoryDto`, `CreateCategoryDto`
- `CommentDto`, `CreateCommentDto`

### Data access (EF Core)
- `PostRepository`
  - Uses `Include` to load `Category` and `Comments` for `GetByIdAsync`
- `CategoryRepository`
  - `HasPostsAsync(categoryId)` uses an efficient `AnyAsync` query against `Posts`

## API Endpoints

### Authentication (`/api/authentication`)
- `POST /api/authentication/register` — register user (returns JWT)
- `POST /api/authentication/login` — login (returns JWT)
- `GET /api/authentication/emailExists?email=...` — check if email is registered
- `GET /api/authentication/CurrentUser` — get current user (requires `Authorization: Bearer <token>`)

### Posts (`/api/posts`)
- `POST /api/posts` — create post
- `PUT /api/posts/{postId}` — update post
- `GET /api/posts` — list posts
- `GET /api/posts/{id}` — post details (includes comments)
- `PUT /api/posts/{id}/publish` — publish
- `PUT /api/posts/{id}/archive` — archive
- `DELETE /api/posts/{id}` — hard delete

### Categories (`/api/categories`)
- `GET /api/categories` — list categories
- `POST /api/categories` — create category
- `DELETE /api/categories/{id}` — delete (blocked when category has posts)

### Comments
- `GET /api/posts/{postId}/comments` — list comments for a post
- `POST /api/posts/{postId}/comments` — add comment
- `DELETE /api/posts/comment/{id}` — delete comment

## Data Seeding

On startup, the app:
- applies migrations (`MigrateDatabase`)
- seeds blog data from JSON (`IDataSeeder.InitializeAsync()`)
- seeds Identity roles/users (`IIdentityDataSeeder.InitializerAsync()`)

Seed JSON files are in:
- `Blog.Persistence/Data/DataSeed/JSONFiles/`

## Running locally

### Prerequisites
- .NET 8 SDK
- SQL Server (example connection string is in `BlogApi/appsettings.json`)

### Run

```bash
dotnet restore
dotnet run --project BlogApi/BlogApi.Web.csproj
```

Swagger UI launches at `https://localhost:7200/swagger` (see `BlogApi/Properties/launchSettings.json`).

### Auth in Postman / clients
- Set header: `Authorization: Bearer <token>`
- JWT settings live in `BlogApi/appsettings.json` under `JWTOptions` (SecretKey/Issuer/Audience).

