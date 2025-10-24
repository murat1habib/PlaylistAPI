# 🎵 Playlist API

## DELIVERABLES

  - This repository includes:
    - A link to the project source code (see below)
    - Notes about design, assumptions, and issues encountered

| Deliverable | Link |
|--------------|------|
| **Code** | [https://github.com/murat1habib/PlaylistAPI](https://github.com/murat1habib/PlaylistAPI) |
| **Deployment** | [https://addressbookapi-murat-cbadc3gcbeaqb8dy.francecentral-01.azurewebsites.net/index.html](https://addressbookapi-murat-cbadc3gcbeaqb8dy.francecentral-01.azurewebsites.net/index.html) |

---

## 📘 Design & Assumptions
- The project is built using **ASP.NET Core 8 Web API**.
- Data is stored temporarily using an **in-memory repository** (no database).
- The API follows standard **RESTful** structure with simple CRUD operations.
- **Swagger UI** is integrated for endpoint testing and documentation.
- The application is deployed to **Azure App Service**.

---

## ⚙️ Issues Encountered
- Swagger initially did not display in production — fixed by enabling it globally.
- Azure redirection caused 404 at root — resolved by setting `RoutePrefix = string.Empty` for Swagger.

---
