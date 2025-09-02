# 📚 Book Catalog App

A complete **Book Catalog System** with a **.NET 9 backend** and **React + Vite frontend**, fully containerized with Docker and ready to run locally or in production.

This project allows you to **search, filter, and list books** using a RESTful API, with a modern single-page application frontend.

---

## 🏗 Project Architecture

The project is divided into two main modules:

### Backend (BookCatalog API)

- **Framework:** .NET 9 / ASP.NET Core
- **Architecture:** Layered
  - **Domain:** Contains business rules, entities, and service logic.
  - **Infrastructure:** Handles persistence using EF Core with PostgreSQL.
  - **API Layer:** Exposes RESTful endpoints via Controllers.
- **Database:** PostgreSQL 16
- **Documentation:** Swagger / OpenAPI for API exploration and testing.
- **CORS:** Configured to allow requests from the frontend.

**Database and Migrations**

- EF Core is used to handle database schema migrations.
- The project includes a **migration container** that automatically applies all pending migrations on startup.
- Optional **seed data** can be configured in the `BookCatalog.Infrastructure` project to pre-populate the database with initial books.

### Frontend (BookCatalog Web)

- **Framework:** React.js + Vite.js
- **Styling:** Tailwind CSS (optional)
- **Communication with Backend:** Fetch API with the environment variable `VITE_API_URL`
- **Build:** Production-ready build via Vite and served using Nginx in a container

---

## 🛠 Technology Stack

| Layer              | Technology                                 |
| ------------------ | ------------------------------------------ |
| Backend            | .NET 9, C#, ASP.NET Core, EF Core, Swagger |
| Database           | PostgreSQL 16                              |
| Frontend           | React.js, Vite.js, Tailwind CSS (optional) |
| Containerization   | Docker, Docker Compose                     |
| Service Networking | Docker bridge network                      |

![.NET](https://img.shields.io/badge/.NET-9-blue?logo=dotnet)
![React](https://img.shields.io/badge/React-18-blue?logo=react)
![Vite](https://img.shields.io/badge/Vite-4-lightgrey?logo=vite)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue?logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Compose-blue?logo=docker)

---

## 🚀 Running the Project

### Prerequisites

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Step 1: Clone the Repository

```bash
git clone https://github.com/alissonb13/book-catalog-app
cd book-catalog-app
```

### Step 2: Start the Full Application

```bash
docker-compose up --build
```

### This command does the following:

1. **Database (book-catalog-db)**

   - Runs PostgreSQL 16 with persistent storage.
   - Healthcheck ensures the database is ready before other services start.

2. **Migration (book-catalog-migration)**

   - Applies all EF Core migrations to create the schema.
   - Optional: can insert seed data to pre-populate the database.

3. **Backend (book-catalog-api)**

   - Connects to PostgreSQL using the configured connection string.
   - Exposes RESTful endpoints on http://localhost:5296
   - Swagger UI available for testing API endpoints.

4. **Frontend (book-catalog-web)**
   - Built using Vite and served via Nginx.
   - Access the web application at http://localhost:8080
   - Automatically configured to point to the backend API via Docker network.

## 🌐 Access

- Frontend UI: http://localhost:8080

- Swagger: http://localhost:5296/swagger

## ⚡ Environment Variables

### Backend

```
ConnectionStrings__BookCatalogConnectionString → PostgreSQL connection string
ASPNETCORE_ENVIRONMENT → Development or Production
```

### Frontend

```
VITE_API_URL → Backend URL inside Docker (e.g., http://book-catalog-api:5296)
```

## 📂 Project Structure

```bash
book-catalog-app/
│
├─ book-catalog-backend/    # Backend (.NET 9)
│  ├─ src/
│  ├─ Dockerfile
│  └─ BookCatalog.sln
│
├─ book-catalog-frontend/   # Frontend (React + Vite)
│  ├─ src/
│  ├─ package.json
│  └─ Dockerfile
│
└─ docker-compose.yml
```

## 📝 Notes & Tips

- The frontend communicates with the backend via Docker service name, ensuring proper networking (`book-catalog-api`).

- Database migrations are applied automatically by the `book-catalog-migration` container.

- Seed data can be added to the database inside the infrastructure project and will run during migration if configured.

- To rebuild and refresh the frontend: `docker-compose up --build book-catalog-web`

- To rebuild the backend and apply migrations: `docker-compose up --build book-catalog-api book-catalog-migration`
