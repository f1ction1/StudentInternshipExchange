#  Online Student Internship Exchange (Engineering Thesis)

This repository contains an engineering thesis project — a custom **online student internship exchange platform**.  
The main goal of the project was **learning and practicing modern backend and frontend architectures**.

All rights to the original implementation belong to the author.

---

##  Project purpose

The project was created to:
- learn **Modular Monolith architecture** in practice,
- apply **Clean Architecture** and **DDD-inspired modeling**,
- implement **CQRS** with clear separation of commands and queries,
- implement **Event-driven communication** to maintain loose coupling between modules,
- build a non-trivial system beyond basic CRUD.

Because of its educational nature:
- code quality may vary between modules,
- some solutions could be simplified or refactored,
- not all modules follow the same maturity level.

---

##  Architecture overview

- **Modular Monolith** — a single deployable application, internally divided into independent modules with clear boundaries. This structure can serve as a foundation for gradually extracting selected modules into microservices in the future.
- Each module (except Recommendations) follows **Clean Architecture**
- Modules communicate via:
  - **Module APIs (contracts)**
  - **Integration Events**
- Shared abstractions placed in `Modules.Common`

### Modules:
- **Auth** — authentication and authorization
- **Users** — student and employer profiles
- **Internships** — internship offers
- **Applications** ⭐ *(most mature module)* - applications handling
- **Recommendations** — interaction-based recommendations

---

##  Frontend overview

The frontend is a **Single Page Application (SPA)** built with React.  
Navigation is handled by **React Router**, which enables:
- deep-linking to views (e.g., `/internships?search=...`),
- route-based page structure (public vs. authenticated areas),
- clean UX without full page reloads.

Data fetching and caching are handled with **TanStack Query**.

---

##  Code quality note

The **Applications module** is the **most refined part of the system**.

It was implemented last, after gaining experience from earlier modules, and therefore:
- follows Clean Architecture most consistently,
- has the clearest domain model,
- contains the best examples of CQRS and business rules,
- shows how the final architectural approach was intended to look.

Earlier modules may contain:
- less strict layering,
- simpler abstractions,
- solutions that would be redesigned with current knowledge.

This evolution reflects the **learning process**, which is intentional and documented.

---

##  Technologies used

### Backend
- ASP.NET Core (.NET 9)
- Modular Monolith architecture
- Clean Architecture
- CQRS + MediatR
- MassTransit (for events)
- Entity Framework Core
- Dapper (recommendation queries)
- MS SQL Server

### Frontend
- React
- React Router (SPA routing)
- TanStack Query (client-side caching & server state management)
- Axios
- Bootstrap
- REST API communication

---

##  Features

- internship offer publishing and searching
- paginated internship offer listings
- application workflow with clear statuses
- employer-side application handling
- interaction tracking (viewed / liked / applied)
- simple recommendation mechanism based on user interactions
- client-side caching of dictionary data (e.g., countries, cities, industries)


