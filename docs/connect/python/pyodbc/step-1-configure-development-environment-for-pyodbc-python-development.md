---
title: Step 1 - Configure pyodbc environment
description: Step 1 of this getting started guide involves installing Python, the Microsoft ODBC Driver for SQL Server, and pyODBC into your development environment.
author: David-Engel
ms.author: davidengel
ms.reviewer: mikeray
ms.date: 09/18/2024
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
# CustomerIntent: As a developer, I want to install the pymssql package so that I can connect to SQL with Python code.
---

# Step 1: Configure development environment for pyodbc Python development

You need to configure your development environment with the prerequisites in order to develop an application using the pyodbc Python driver for SQL Server.

## Prerequisites

- Python 3
  - If you don't already have Python, install the **Python runtime** and **Python Package Index (PyPI) package manager** from [python.org](https://www.python.org/downloads/).
  - Prefer to not use your own environment? Open as a devcontainer using [GitHub Codespaces](https://github.com/features/codespaces).
    - [![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/github/codespaces-blank?quickstart=1).

## Install the ODBC driver

This driver requires the host operating system to have the appropriate ODBC driver already installed.

### [Windows](#tab/windows)

1. Obtain and install the Microsoft ODBC driver for SQL Server on Windows:

   - [Microsoft ODBC Driver for SQL Server on Windows](../../odbc/windows/system-requirements-installation-and-driver-files.md#installing-microsoft-odbc-driver-for-sql-server)

1. Verify that you have installed the driver.

### [Linux](#tab/linux)

1. Obtain and install the Microsoft ODBC driver for SQL Server on Linux:

   - [Microsoft ODBC driver for SQL Server (Linux)](../../odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md)

1. Verify that you have installed the driver.

### [macOS](#tab/macos)

1. Obtain and install the Microsoft ODBC driver for SQL Server on macOS:

   - [Microsoft ODBC driver for SQL Server (macOS)](../../odbc/linux-mac/install-microsoft-odbc-driver-sql-server-macos.md)

1. Verify that you have installed the driver.

---

## Install the pyodbc package

Get the [`pyodbc` package](https://pypi.org/project/pyodbc/) from PyPI.

1. Open a command prompt in an empty directory.

1. Install the [`pyodbc` package](https://pypi.org/project/pyodbc/).

    ```bash
    pip install pyodbc
    ```

## Check installed packages

You can use the PyPI command-line tool to verify that your intended packages are installed.

1. Check the list of installed packages with `pip list`.

    ```bash
    pip list
    ```

## Related content

- [Step 2: Create an SQL database for pyodbc Python development](step-2-create-a-sql-database-for-pyodbc-python-development.md)
