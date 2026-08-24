---
title: "Access External Data: ODBC Generic Types - PolyBase"
description: PolyBase in SQL Server allows you to connect to compatible data sources through the ODBC connector. Install the ODBC driver and create external tables.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/11/2025
ms.service: sql
ms.subservice: polybase
ms.topic: how-to
ms.custom:
  - build-2025
monikerRange: ">=sql-server-linux-ver15 || >=sql-server-ver15"
---
# Configure PolyBase to access external data with ODBC generic types

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

PolyBase starting in SQL Server 2019 allows you to connect to ODBC-compatible data sources using the ODBC connector. Beginning with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], this feature is available on Linux.

This article demonstrates how to create configuring connectivity using an ODBC data source. The guidance provided uses one specific ODBC driver as an example. Check with your ODBC provider for specific examples. Reference the ODBC driver documentation for your data source to determine the appropriate connection string options. The examples in this article might not apply to any specific ODBC driver.

## Prerequisites

> [!NOTE]  
> In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and previous versions, this feature requires SQL Server on Windows.

- PolyBase must be installed and enabled for your SQL Server instance [PolyBase installation](polybase-installation.md).

- Before creating a database scoped credential, you must create a [master key](../../t-sql/statements/create-master-key-transact-sql.md).

## Install the ODBC driver

Follow the installation instructions for your operating system.

::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] defaults to Microsoft ODBC Driver version 18 for SQL Server, for PolyBase `sqlserver` data sources. This driver supports TDS 8.0 and includes various updates, features, and some breaking changes. To use TDS 8.0, you must use a new encryption option, and install a trusted certificate on your server.

For more information about Microsoft ODBC Driver version 18 for SQL Server, see:

- [Download ODBC Driver for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md)
- [ODBC Driver 18.0 for SQL Server Released | Microsoft Community Hub](https://techcommunity.microsoft.com/blog/sqlserver/odbc-driver-18-0-for-sql-server-released/3169228)

For more information about SQL Server TDS 8.0 support, see [TDS 8.0](../security/networking/tds-8.md).

::: moniker-end

### [Windows](#tab/windows)

Download and install the ODBC driver of the data source you want to connect to on each of the PolyBase nodes. Once the driver is properly installed, you can view and test the driver from the **ODBC Data Source Administrator**.

:::image type="content" source="media/polybase-odbc-admin.png" alt-text="Screenshot of PolyBase scale-out groups.":::

In the previous example, the name of the driver is circled in red. Use this name when you create the external data source.

> [!IMPORTANT]  
> In order to improve query performance, enable connection pooling. This can be accomplished from the **ODBC Data Source Administrator**.

### [Linux](#tab/linux)

The following example demonstrates the SQL ODBC driver on Ubuntu.

> [!TIP]  
> If you haven't already, [install PolyBase on Linux](polybase-linux-setup.md).

1. Add the Microsoft repository:

   1. Import the Microsoft GPG key.

      ```bash
      curl https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
      ```

   1. Add the Microsoft repository to your system.

      ```bash
      curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
      ```

1. Update the package list.

   ```bash
   sudo apt update
   ```

1. Install the ODBC driver.

   Install the latest version of the ODBC driver. The following example installs version 18.

   ```bash
   sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18
   ```

Installation creates the following files:

| File | Description |
| --- | --- |
| `/etc/odbcinst.ini` | Driver name, description, and version information. |
| `/etc/odbc.ini` | DSN name, encryption, and other specifications. |

You need to create the `odbc.ini` file based on the driver's properties and specifications. Multiple drivers share the same `odbc.ini` and `odbcinst.ini` files, with multiple entries.

### Example files

#### Example odbc.ini

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

For the full list of supported parameters check the driver provider's documentation.

### Limitations

PolyBase for SQL Server on Linux uses an external service to safely isolate and load the drivers. This service is started by default when the PolyBase package (`mssql-server-polybase`) is installed.

The service uses the default port number `25100`. If this port is in use, it fails with the following message:

```output
Failed to bind port "127.0.0.1:25100"
```

You can find this message in PolyBase's log file, located at: `/var/opt/mssql-polybase-ees/log/`. In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, the location has moved to `/var/opt/mssql/log/polybase-ees-log`.

To fix, customize the service to use an available port and restart.

1. Find and edit the `/var/opt/mssql/binn/PolyBase/DMs.exe.config` file. Locate the key entry `EESPort`, and assign the new port.

1. Find and edit the `/var/opt/mssql/binn/PolyBase/DWEngineService.exe.config` file. Locate the key entry `EESPort`, and assign the new port.

1. Run the following command to restart the service informing the new port:

   ```bash
   sudo /opt/mssql/lib/dotnet6/dotnet/opt/mssql/lib/ExternalExecutionService.dll -port <newportnumber>
   ```

   In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, use the following command instead:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set polybaseEES eesport <newportnumber>
   ```

1. You're prompted to restart the PolyBase service.

   ```bash
   systemctl restart mssql-ees.service
   ```

---

## Create dependent objects in SQL Server

To use the ODBC data source, you must first create a few objects to complete the configuration.

The following Transact-SQL commands are used in this section:

- [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md)

1. Create a database scoped credential for accessing the ODBC source.

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL [<credential_name>]
        WITH IDENTITY = '<username>', SECRET = '<password>';
    ```

    For example, the following example creates a credential named `credential_name`, with an identity of `username`. Replace `<password>` with a complex password.

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL credential_name
        WITH IDENTITY = 'username', SECRET = '<password>';
    ```

1. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).

    ```sql
    CREATE EXTERNAL DATA SOURCE [<external_data_source_name>]
    WITH (
        LOCATION = 'odbc://<ODBC server address>[:<port>]',
        CONNECTION_OPTIONS = 'Driver={<Name of installed driver>};
            ServerNode = <name of server  address>:<Port>',
        -- PUSHDOWN = [ON] | OFF,
        CREDENTIAL = [<credential_name>]
    );
    ```

    The following example creates an external data source:

    - Named `external_data_source_name`
    - Located at the ODBC `SERVERNAME` and port `4444`
    - Connecting with `CData ODBC Driver For SAP 2015` - This is the driver created under [Install the ODBC driver](#install-the-odbc-driver)
    - On `ServerNode` `sap_server_node` port `5555`
    - Configured for processing pushed down to the server (`PUSHDOWN = ON`)
    - Using the `credential_name` credential

    ```sql
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH (
        LOCATION = 'odbc://SERVERNAME:4444',
        PUSHDOWN = ON,
        CONNECTION_OPTIONS = 'Driver={CData ODBC Driver For SAP 2015};
            ServerNode = sap_server_node:5555',
        CREDENTIAL = credential_name
    );
    ```

## Create an external table

Once you have created the dependent objects, you can create an external table using T-SQL.

The following Transact-SQL commands are used in this section:

- [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md)
- [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md)

1. Create one or more external tables.

   Create an external table. You need to reference the external data source created previously using the `DATA_SOURCE` argument and specify the source table as the `LOCATION`. You don't need to reference all columns but you need to ensure that the types are correctly mapped.

   ```sql
   CREATE EXTERNAL TABLE [<your_table_name>]
   (
       [<col1_name>] DECIMAL (38) NOT NULL,
       [<col2_name>] DECIMAL (38) NOT NULL,
       [<col3_name>] CHAR COLLATE Latin1_General_BIN NOT NULL
   )
   WITH (
       DATA_SOURCE = [<external_data_source_name>],
       LOCATION = '<sap_table_name>'
   );
   ```

   > [!NOTE]  
   > Note you can reuse the dependent objects for all external tables using this external data source.

1. **Optional:** Create statistics on an external table.

    For optimal query performance, we recommend creating statistics on external table columns especially the ones used for joins, filters, and aggregates.

    ```sql
    CREATE STATISTICS statistics_name ON contact(FirstName) WITH FULLSCAN;
    ```

## Related content

- [PolyBase overview](overview.md)
- [PolyBase Transact-SQL reference](polybase-t-sql-objects.md)
