---
title: Install mssql-python
description: Learn how to install the mssql-python driver on Windows, Linux, and macOS. No external ODBC driver is required.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Install mssql-python

The `mssql-python` driver is Microsoft's official Python driver for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. Install the driver on Windows, Linux, or macOS by using pip.

## Prerequisites

- The driver requires **Python 3.10 or later** and doesn't support earlier Python versions.
- **pip** package manager (included with Python 3.4+).

> [!IMPORTANT]
> The mssql-python driver uses **DDBC** (Direct Database Connectivity), which bundles and manages the ODBC layer internally. You don't need to install the Microsoft ODBC Driver for SQL Server separately.

## Install from PyPI

Install the driver using pip:

```bash
pip install mssql-python
```

To upgrade an existing installation:

```bash
pip install --upgrade mssql-python
```

To install a specific version:

```bash
pip install mssql-python==1.12.0
```

## Verify the installation

After installation, verify the driver is working:

```python
import mssql_python

print(f"mssql-python version: {mssql_python.__version__}")
print(f"DB-API level: {mssql_python.apilevel}")
print(f"Thread safety: {mssql_python.threadsafety}")
print(f"Parameter style: {mssql_python.paramstyle}")
```

Expected output:

```output
mssql-python version: 1.12.0
DB-API level: 2.0
Thread safety: 1
Parameter style: pyformat
```

## Platform-specific notes

### Windows

The driver ships as a prebuilt wheel that includes all necessary components.

### Linux

The driver requires certain system libraries for Kerberos authentication and dynamic loading. Install them using your distribution's package manager:

**Ubuntu/Debian:**

```bash
sudo apt-get update
sudo apt-get install libltdl7 libkrb5-3 libgssapi-krb5-2
```

**Red Hat/CentOS/Fedora:**

```bash
sudo dnf install libtool-ltdl krb5-libs
```

> [!NOTE]
> On RHEL 8 and other glibc 2.28 compatible distributions (AlmaLinux 8, Rocky Linux 8), the driver installs from the standard `manylinux_2_28` wheel published to PyPI.

**Alpine Linux:**

```bash
apk add libltdl krb5-libs
```

### macOS

The driver ships as a `universal2` fat binary that runs natively on both Apple Silicon (arm64) and Intel (x86_64) Macs. You don't need a separate unixODBC install.

The driver requires OpenSSL. Install it by using Homebrew:

```bash
brew install openssl
```

If you encounter SSL-related errors, ensure OpenSSL is properly linked:

```bash
export LDFLAGS="-L/opt/homebrew/opt/openssl/lib"
export CPPFLAGS="-I/opt/homebrew/opt/openssl/include"
```

## Virtual environments

To avoid conflicts, install Python packages in a virtual environment:

```bash
# Create a virtual environment
python -m venv .venv

# Activate it (Windows)
.venv\Scripts\activate

# Activate it (Linux/macOS)
source .venv/bin/activate

# Install the driver
pip install mssql-python
```

## Development installation

To install the driver with development dependencies for contributing to the driver:

```bash
git clone https://github.com/microsoft/mssql-python.git
cd mssql-python
pip install -e ".[dev]"
```

## Troubleshoot installation issues

### pip install fails with "no matching distribution"

Ensure you're using Python 3.10 or later:

```bash
python --version
```

If you're using an older Python version, upgrade Python or use pyenv to manage multiple versions.

### Import error after installation

If you see `ModuleNotFoundError: No module named 'mssql_python'`, verify you're using the same Python environment where you installed the package.

```bash
pip show mssql-python
```

### Permission errors on Linux/macOS

If you encounter permission errors, avoid using `sudo pip`. Instead, use a virtual environment or the `--user` flag.

```bash
pip install --user mssql-python
```

## Related content

- [Quickstart: Connect and query](python-sql-driver-mssql-python-quickstart.md)
- [Connection strings reference](connection-strings.md)
- [mssql-python GitHub repository](https://github.com/microsoft/mssql-python)
