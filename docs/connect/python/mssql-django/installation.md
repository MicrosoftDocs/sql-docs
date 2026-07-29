---
title: Install mssql-django
description: Learn how to install the mssql-django Django database backend for SQL Server on Windows, Linux, and macOS.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 07/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Install mssql-django

The `mssql-django` package is the official Microsoft-supported Django database backend for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. This article explains how to install the package and its dependencies.

## Prerequisites

- **Python 3.8 or later**. Django 6.0 requires Python 3.12 and later versions.
- **pip** package manager (included with Python 3.4 and later versions)
- **Microsoft ODBC Driver 17 or 18 for SQL Server**. See [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md).

> [!IMPORTANT]  
> Unlike the `mssql-python` driver, `mssql-django` requires an external ODBC driver. Install the Microsoft ODBC Driver for SQL Server before configuring your Django project.

## Install from PyPI

Install the package using pip. This command also installs Django, pyodbc, and pytz automatically:

```bash
pip install mssql-django
```

To upgrade an existing installation:

```bash
pip install --upgrade mssql-django
```

To install a specific version:

```bash
pip install mssql-django==1.7.4
```

## Dependency and version compatibility

For `mssql-django` 1.7.4, the package metadata includes these dependency constraints:

| Component | Version guidance |
| --- | --- |
| Python | 3.8 or later versions |
| Django | `>=3.2` and `<6.1` |
| pyodbc | `>=3.0` |
| pytz | Installed as a dependency |

> [!TIP]
> Let `pip` resolve compatible versions unless you have a tested lock file. Pinning an older `pyodbc` version can cause runtime issues even when installation succeeds.

## Verify the installation

After installation, verify the package is installed correctly:

```bash
pip show mssql-django
```

Expected output:

```output
Name: mssql-django
Version: 1.7.4
Summary: Django backend for Microsoft SQL Server
```

Verify installation from Python by testing the ODBC layer:

```python
import pyodbc

print(f"pyodbc version: {pyodbc.version}")
print(f"Available ODBC drivers: {pyodbc.drivers()}")
```

> [!NOTE]  
> The `mssql-django` backend is automatically configured in Django's database routing system. Don't import it directly in application code. Instead, set the `ENGINE` to `mssql` in your `DATABASES` configuration.

## Use a virtual environment

Use a Python virtual environment to isolate project dependencies:

```bash
python -m venv .venv
```

Activate the virtual environment:

### [Windows](#tab/windows)

```console
.venv\Scripts\activate
```

### [Linux/macOS](#tab/linux)

```bash
source .venv/bin/activate
```

---

Then install `mssql-django` inside the virtual environment:

```bash
pip install mssql-django
```

## Platform-specific notes

The ODBC driver installation steps vary by operating system.

### Windows

Install the Microsoft ODBC Driver 18 for SQL Server using the `.msi` installer from [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md).

### Linux

Install the ODBC driver using your distribution's package manager. See [Install the Microsoft ODBC driver for SQL Server (Linux)](../../odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md) for platform-specific instructions.

### macOS

Install the ODBC driver with Homebrew:

```bash
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
HOMEBREW_ACCEPT_EULA=Y brew install msodbcsql18
```

## Dependencies

The `mssql-django` package automatically installs the following dependencies:

| Package | Purpose |
| --- | --- |
| `Django` | Web framework |
| `pyodbc` | ODBC database driver for Python |
| `pytz` | Time zone support |

The backend imports `pytz` internally to convert timezone-aware `datetime` values against SQL Server, so `pytz` remains a required runtime dependency even if your Django project uses `USE_TZ=True` with the standard library `zoneinfo` module.

## Related content

- [Quickstart: Connect Django to SQL Server](quickstart.md)
- [mssql-django configuration reference](configuration-reference.md)
- [Container and local development with mssql-django](container-local-development.md)
- [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md)
- [Django installation guide](https://docs.djangoproject.com/en/stable/topics/install)
