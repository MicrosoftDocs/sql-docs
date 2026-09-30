---
title: Troubleshoot Installation and Connection Issues with mssql-python
description: Diagnose and resolve mssql-python installation, connection, container, and continuous integration issues.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, sumitsar
ms.date: 09/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: troubleshooting
ai-usage: ai-assisted
---

# Troubleshoot installation and connection issues with mssql-python

Use this article to diagnose installation, connection, container, and continuous integration (CI) issues with the `mssql-python` driver.

## Install issues

### pip install fails or builds from source

**Symptoms:**

```text
error: Microsoft Visual C++ 14.0 or greater is required
ERROR: Failed building wheel for mssql-python
```

**Possible causes and solutions:**

- **No prebuilt wheel for your platform**
  - Check that you run a supported Python version (3.10 and later versions) and platform. See [Support lifecycle](support-lifecycle.md) for the compatibility matrix.
  - Upgrade pip before installation with `pip install --upgrade pip`.
  - For repeatable team environments, use the locked workflow in [Repeatable deployments](python-sql-driver-mssql-python-repeatable-deployments-quickstart.md) or the container patterns in [Container and local development](container-local-development.md) to reduce local machine drift.

- **Virtual environment not activated**
  - Activate your virtual environment first. Installation into the system Python can cause permission errors or conflicts.

  # [Windows](#tab/windows)

  ```console
  python -m venv .venv
  .venv\Scripts\activate
  pip install mssql-python
  ```

  # [Linux/macOS](#tab/linux-macos)

  ```console
  python -m venv .venv
  source .venv/bin/activate
  pip install mssql-python
  ```

  ---

- **Missing Linux system libraries**
  - The driver requires several system libraries on Linux. See [Platform-specific dependencies](container-local-development.md#platform-specific-dependencies) for the packages to install.

### Conflicting driver installations

**Symptoms:**

You encounter import errors or unexpected behavior after you install `mssql-python` and `pyodbc` in the same environment.

**Solution:**

`mssql-python` and `pyodbc` can coexist. If you encounter conflicts, create a clean virtual environment.

# [Windows](#tab/windows)

```console
python -m venv .venv --clear
.venv\Scripts\activate
pip install mssql-python
```

# [Linux/macOS](#tab/linux-macos)

```console
python -m venv .venv --clear
source .venv/bin/activate
pip install mssql-python
```

---

## Connection issues

### Unable to connect to server

**Symptoms:**

```text
OperationalError: [08001] (0) Client unable to establish connection
```

**Possible causes and solutions:**

- **Server not reachable**
  - Verify that the server name and port are correct.
  - Check network connectivity with `ping <server>` or `telnet <server> 1433`.
  - Ensure that the firewall allows outbound connections on port 1433.

- **SQL Server not running**
  - Verify that the SQL Server service is started.
  - For named instances, verify that the SQL Server Browser service is running.

- **Azure SQL firewall rules**
  - Add your client IP address to the Azure SQL firewall rules in the Azure portal.
  - For Azure SQL Managed Instance, ensure that you connect from an allowed network.

Test basic TCP connectivity:

```python
import socket

try:
    sock = socket.create_connection(("<server>.database.windows.net", 1433), timeout=5)
    print("TCP connection successful")
    sock.close()
except Exception as e:
    print(f"Cannot reach server: {e}")
```

### Login failed

**Symptoms:**

```text
OperationalError: [28000] (18456) Login failed for user '<user_id>'.
```

**Possible causes and solutions:**

- **Authentication mode mismatch**
  - For Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric, prefer a Microsoft Entra mode such as `Authentication=ActiveDirectoryDefault`.
  - If you use SQL authentication intentionally, verify that the server allows it and that you use the correct login format for that endpoint.

- **Incorrect SQL authentication credentials**
  - Verify the user ID and password.
  - For Azure SQL, include the full user ID: `<user_id>@<server>`.

- **User doesn't exist in the database**
  - Verify that the user has access to the specified database.
  - Check whether the sign-in is mapped to a database user.

- **Authentication not configured**
  - Use Microsoft Entra authentication (recommended): `Authentication=ActiveDirectoryDefault`.
  - If you troubleshoot a local SQL Server instance that should accept SQL authentication, verify that SQL Server uses mixed mode authentication.

### Connection timeout

**Symptoms:**

```text
OperationalError: [HYT00] (0) Timeout expired
OperationalError: [HYT01] (0) Connection timeout expired
```

**Possible causes and solutions:**

- **Server is slow to respond**
  - Increase the connection timeout.

  ```python
  conn = mssql_python.connect(connection_string, timeout=60)
  ```

- **Network latency**
  - Check the network path to the server.
  - Consider a shorter network path or virtual private network (VPN).

- **Server under heavy load**
  - Try to connect during off-peak hours.
  - Contact your database administrator.

### SSL certificate errors

**Symptoms:**

```text
OperationalError: [08001] SSL Provider: The certificate chain was issued by an authority that is not trusted
```

**Solutions:**

Prefer a trusted certificate or the local development patterns in [Container and local development](container-local-development.md). Use `TrustServerCertificate=yes` only for local development against a server that you control.

For development and testing with a self-signed certificate:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
    "TrustServerCertificate=yes;"  # Don't use in production
)
```

> [!CAUTION]
> `TrustServerCertificate=yes` is a local-only fallback. Don't carry it into shared development containers, CI pipelines, or production deployments. For more information, see [Encryption and certificates](encryption-certificates.md).

For production, install the appropriate certificates and use:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
    "HostnameInCertificate=<server>.domain.com;"
)
```

## Container and CI issues

### Missing system libraries on Linux

**Symptoms:**

```text
ImportError: libltdl.so.7: cannot open shared object file: No such file or directory
ImportError: libkrb5.so.3: cannot open shared object file
```

**Solution:**

Install the required system packages for your distribution:

| Distribution | Install command |
| --- | --- |
| Ubuntu or Debian | `sudo apt-get install libltdl7 libkrb5-3 libgssapi-krb5-2` |
| Red Hat or Fedora | `sudo dnf install libtool-ltdl krb5-libs` |
| Alpine | `apk add libltdl krb5-libs` |

For Dockerfile examples, see [Container and local development](container-local-development.md).

### macOS SSL errors after install

**Symptoms:**

You encounter SSL-related errors when you connect from macOS, especially on Apple silicon.

**Solution:**

Install OpenSSL with Homebrew, and set the linker flags:

```bash
brew install openssl
export LDFLAGS="-L/opt/homebrew/opt/openssl/lib"
export CPPFLAGS="-I/opt/homebrew/opt/openssl/include"
```

## Related content

- [Install the mssql-python driver](installation.md)
- [Connection strings for mssql-python](connection-strings.md)
- [Manage connections with mssql-python](connection-management.md)
- [Encryption and certificate validation](encryption-certificates.md)
- [Container and local development](container-local-development.md)
- [Troubleshoot mssql-python](troubleshooting.md)
