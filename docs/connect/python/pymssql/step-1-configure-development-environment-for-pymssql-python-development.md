---
title: Step 1 - Configure pymssql environment
description: Step 1 of this getting started guide involves installing Python, the Microsoft ODBC Driver for SQL Server, and pymssql into your development environment.
author: David-Engel
ms.author: davidengel
ms.date: 08/22/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
# CustomerIntent: As a developer, I want to install the pymssql package so that I can connect to SQL with Python code.
---

# Step 1: Configure development environment for pymssql Python development

You need to configure your development environment with the prerequisites in order to develop an application using the pymssql Python driver for SQL Server.

> [!NOTE]
> This driver uses the TDS protocol, which is enabled by default in SQL Server and Azure SQL Database.  No extra configuration is required.

## Prerequisites

- Python 3
  - If you don't already have Python, install the **Python runtime** and **Python Package Index (PyPI) package manager** from [python.org](https://www.python.org/downloads/).  
  - Prefer to not use your own environment? Open as a devcontainer using [GitHub Codespaces](https://github.com/features/codespaces).
    - [![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/github/codespaces-blank?quickstart=1).

## Install the pymssql package

Get the [`pymssql` package](https://pypi.org/project/pymssql/) from PyPI.

1. Open a command prompt in an empty directory.

1. Install the [`pymssql` package](https://pypi.org/project/pymssql/).

    ```bash
    pip install pymssql
    ```

## Check installed packages

You can use the PyPI command-line tool to verify that your intended packages are installed.

1. Check the list of installed packages with `pip list`.

    ```bash
    pip list
    ```

## Next steps

> [!div class="nextstepaction"]
> [Step 2: Create an SQL database for pymssql Python development](step-2-create-a-sql-database-for-pymssql-python-development.md)
