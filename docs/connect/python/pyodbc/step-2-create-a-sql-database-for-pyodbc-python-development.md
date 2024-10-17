---
title: Step 2 - Create a SQL database for pyodbc
description: Step 2 of this getting started guide involves creating a database in SQL Server or Azure SQL Database for this pyodbc sample.
author: David-Engel
ms.author: davidengel
ms.date: 08/22/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
# CustomerIntent: As a developer, I want to create a SQL database so that I can connect to it with Python code.
---

# Step 2: Create a SQL database for pyodbc Python development

Create a SQL database using either Azure SQL or SQL Server.

## Prerequisites

- Python 3
  - If you don't already have Python, install the **Python runtime** and **Python Package Index (PyPI) package manager** from [python.org](https://www.python.org/downloads/).
  - Prefer to not use your own environment? Open as a devcontainer using [GitHub Codespaces](https://github.com/features/codespaces).
    - [![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/github/codespaces-blank?quickstart=1).
- `pyodbc` package from PyPI.

## Create a SQL database

The samples in this next section only work with the AdventureWorks schema, on either Microsoft SQL Server or Azure SQL Database.

### [Azure SQL Database](#tab/azure-sql)

[Create a SQL database in minutes using the Azure portal](/azure/azure-sql/database/single-database-create-quickstart)

### [Microsoft SQL Server](#tab/sql-server)

[Microsoft SQL Server Samples on GitHub](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks)

---

> [!IMPORTANT]
> Don't forget to get the credentials for the database you create.

## Next steps

> [!div class="nextstepaction"]
> [Step 3: Proof of concept connecting to SQL using pyodbc](step-3-proof-of-concept-connecting-to-sql-using-pyodbc.md)
