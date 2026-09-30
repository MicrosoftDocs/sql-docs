---
title: Install mssql-django
description: Learn how to install the mssql-django Django database backend for SQL Server on Windows, Linux, and macOS.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, sharmag, sumitsar
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Install mssql-django

The `mssql-django` package is the official Microsoft-supported Django database backend for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. This article explains how to install the package and its dependencies.

## Prerequisites

- **Python 3.10 through 3.14**. Django 6.0 and later versions require at least Python 3.12.
- **pip** package manager (included with Python 3.4 and later versions)
- **Microsoft ODBC Driver 17 or 18 for SQL Server**, for database aliases that use the default `pyodbc` driver. See [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md).

> [!IMPORTANT]
> `mssql-django` 2.0 requires `mssql-python` 1.15.0 or later, even when every database alias uses `pyodbc`. The package installs only on platforms that have a compatible `mssql-python` distribution. Projects on other platforms stay on version 1.8.0. For the platform list, see [mssql-django support and lifecycle](support-lifecycle.md#operating-system-compatibility).

## Install from PyPI

Install the package by using pip. This command also installs Django, `pyodbc`, `mssql-python`, and `tzdata` automatically:

```bash
pip install mssql-django
```

To upgrade an existing installation:

```bash
pip install --upgrade mssql-django
```

To install a specific version:

```bash
pip install mssql-django==2.0.0
```

## Dependency and version compatibility

For `mssql-django` 2.0, the package metadata includes these dependency constraints:

| Component | Version guidance |
| --- | --- |
| Python | 3.10 through 3.14 |
| Django | `>=5.2` and `<6.2` |
| `pyodbc` | `>=3.0` |
| `mssql-python` | `>=1.15.0` |
| `tzdata` | Installed as a dependency |

> [!TIP]
> Let `pip` resolve compatible versions unless you have a tested lock file. Pinning an older `pyodbc` version can cause runtime issues even when installation succeeds.

## Choose a database driver

`pyodbc` is the default driver and needs no configuration. To run a database alias on `mssql-python` instead, add `"python_driver": "mssql_python"` to that alias's `OPTIONS` dictionary. Aliases can use different drivers in the same project. For the behavior differences and the options each driver ignores, see [Select the database driver for mssql-django](select-database-driver.md).

## Verify the installation

After installation, verify the package is installed correctly:

```bash
pip show mssql-django
```

Expected output:

```output
Name: mssql-django
Version: 2.0.0
Summary: Django backend for Microsoft SQL Server
```

Verify the ODBC layer that the default `pyodbc` path uses:

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

Database aliases that use the default `pyodbc` driver need a separately installed ODBC driver, and the installation steps vary by operating system. Aliases that use `mssql-python` don't need these steps. Instead, install the [platform prerequisites for mssql-python](https://github.com/microsoft/mssql-python#installation), including OpenSSL on macOS and the required libraries on Linux.

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
| `pyodbc` | Default ODBC database driver for Python |
| `mssql-python` | Opt-in Python database driver, installed unconditionally |
| `tzdata` | IANA time zone database for the standard library `zoneinfo` module |

The backend converts time zone-aware `datetime` values by using the standard library `zoneinfo` module. `zoneinfo` reads the operating system's IANA time zone database when one exists and falls back to the `tzdata` package otherwise, which covers Windows and minimal container images. `pytz` is no longer a dependency.

## Related content

- [Quickstart: Connect Django to SQL Server](quickstart.md)
- [Select the database driver for mssql-django](select-database-driver.md)
- [mssql-django configuration reference](configuration-reference.md)
- [Container and local development with mssql-django](container-local-development.md)
- [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md)
- [Django installation guide](https://docs.djangoproject.com/en/stable/topics/install)
