---
title: Use ODBC Data Source with SQL Server on Linux
description: Learn how to connect to ODBC data sources with PolyBase on SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: hudequei, amitkh, atsingh
ms.date: 01/02/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - build-2025
  - linux-related-content
# customer intent: As a database professional, connect to ODBC datasources from SQL Server deployments on Linux.
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
---

# Connect to ODBC data sources with PolyBase on SQL Server on Linux

[!INCLUDE [sqlserver2025](../includes/applies-to-version/sqlserver2025.md)]

This article describes how you can use PolyBase services with SQL Server on Linux.

Beginning with [!INCLUDE [sssql25-md](../includes/sssql25-md.md)], deployments on Linux can use ODBC data sources for PolyBase. This allows you to bring your own driver (BYOD). On Linux, this feature works similarly to how it works on Windows. For more information, see [Configure PolyBase to access external data with ODBC generic types](../relational-databases/polybase/polybase-configure-odbc-generic.md).

> [!CAUTION]  
> The bring-your-own-driver (BYOD) model involves risks that are the responsibility of the customer and the driver provider. Microsoft isn't responsible for any issues the third-party driver could cause.

## Examples

### Install on Linux

The following example demonstrates the SQL ODBC driver on Ubuntu.

1. Add the Microsoft repository:

   1. Import the Microsoft GPG key

      ```bash
      curl https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
      ```

   1. Add the Microsoft repository to your system

      ```bash
      curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
      ```

1. Update the package list:

   ```bash
   sudo apt update
   ```

1. Install the ODBC Driver

   Install the latest version of the ODBC driver. The following example installs version 18.

   ```bash
   sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18
   ```

Installation creates the following files:

| File | Description |
| --- | --- |
| `/etc/odbcinst.ini` | Driver name, description, and version information. |
| `/etc/odbc.ini` | DNS name, encryption, and other specifications. |

You need to create the `odbc.ini` file based on the driver's properties and specifications. Multiple drivers share the same `odbc.ini` and `odbcinst.ini` files, with multiple entries.

## Example files

### Example odbc.ini

In this example, `driver_name` must match the name from `odbcinst.ini`.

```ini
[MyDSN]
Driver = driver_name
Server = your_server_name
Database = your_database_name
Trusted_Connection = yes
```

#### Sybase example odbcinst.ini

```ini
[ODBC Drivers]
Devart ODBC Driver for ASE=installed
[Devart ODBC Driver for ASE]
Driver=/usr/share/devart/odbcase/libdevartodbcase.3.5.0.so
```

#### Sybase example odbc.ini

```ini
[ODBC Data Sources]
DEVART_ASE=Devart ODBC Driver for ASE
[DEVART_ASE]
Driver=Devart ODBC Driver for ASE
Data Source=database_server_ip
Port=5000
Database=master
QuotedIdentifier=1
```

For the full list of supported parameters check the driver's provider documentation.

## Example queries

Once the driver setup is complete, you can use database scoped credential, external data source, and other PolyBase.

For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL dsc_Sybase
    WITH IDENTITY = '<user>', SECRET = '<password>';
GO

CREATE EXTERNAL DATA SOURCE EDS_Sybase
WITH (
    LOCATION = 'odbc://<servername>:<port>',
    PUSHDOWN = ON, --- optional
    CONNECTION_OPTIONS = 'DSN=DEVART_ASE;DRIVER=Devart ODBC Driver for ASE',
    CREDENTIAL = dsc_Sybase
);
GO

CREATE EXTERNAL TABLE T_EXT
(
    C1 INT
)
WITH (
    DATA_SOURCE = [EDS_SYBASE],
    LOCATION = N'TEST.DBO.T'
);
GO

SELECT * FROM T_EXT;
GO
```

## Limitations

PolyBase for SQL Server on Linux uses an external service to safely isolate and load the drivers. This service is started by default when the PolyBase package (`mssql-server-polybase`) is installed.

The service uses the default port number `25100`. If this port is in use, it fails with the following message:

```output
Failed to bind port "127.0.0.1:25100"
```

You can find this message in PolyBase's log file, located at: `/var/opt/mssql-polybase-ees/log/`. In [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] and later versions, the location has moved to `/var/opt/mssql/log/polybase-ees-log`.

## Related content

- [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md)
- [PolyBase EES encryption (SQL Server on Linux)](sql-server-linux-polybase-ees-encryption.md)
