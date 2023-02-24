---
title: "Access external data: SQL Server - PolyBase"
description: Learn how to use PolyBase on a SQL Server instance to query external data in another SQL Server instance. Create external tables to reference external data.
ms.date: 10/18/2022
ms.custom: seo-lt-2019
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mikeray
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-ver15"
---

# Configure PolyBase to access external data in SQL Server

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article explains how to use PolyBase on a SQL Server instance to query external data in another SQL Server instance.

## Prerequisites

If you haven't installed PolyBase, see [PolyBase installation](polybase-installation.md). The installation article explains the prerequisites. Once installed, also be sure to [enable PolyBase](polybase-installation.md#enable).

The SQL Server external data source uses SQL Authentication.

Before creating a database scoped credential a [Master Key](../../t-sql/statements/create-master-key-transact-sql.md) must be created. 

## Configure a SQL Server external data source

To query the data from a SQL Server data source, you must create external tables to reference the external data. This section provides sample code to create these external tables.
 
For optimal query performance, create statistics on external table columns, especially for the ones used for joins, filters, and aggregates.

The following Transact-SQL commands are used in this section:

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)

1. Create a database scoped credential for accessing the SQL Server source. The following example creates a credential to the external data source with `IDENTITY = 'username'` and `SECRET = 'password'`.

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL SqlServerCredentials
    WITH IDENTITY = 'username', SECRET = 'password';
    ```
   >[!IMPORTANT]
   >The SQL ODBC Connector for PolyBase only supports basic authentication, not Kerberos authentication.

1. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md). The following example:

   - Creates an external data source named `SQLServerInstance`.
   - Identifies the external data source (`LOCATION = '<vendor>://<server>[:<port>]'`). In the example it points to a default instance of SQL Server.
   - Identifies whether computation should be pushed to the source (`PUSHDOWN`). `PUSHDOWN` is `ON` by default.

   Finally, the example uses the credential created previously.

    ```sql
    CREATE EXTERNAL DATA SOURCE SQLServerInstance
        WITH ( LOCATION = 'sqlserver://SqlServer',
        PUSHDOWN = ON,
        CREDENTIAL = SQLServerCredentials);
    ```

1. Create the external table with [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) The statement requires collation and the location requires three-part notation (`<database>.<schema>.<table>`).

    ```sql
    CREATE EXTERNAL TABLE DatabasesExternal (
        name VARCHAR(128) COLLATE SQL_Latin1_General_CP1_CI_AS)
      WITH (LOCATION = 'master.sys.databases',
      DATA_SOURCE = SQLServerInstance);
    ```

1. Optionally, create statistics on an external table.

  For optimal query performance, create statistics on external table columns, especially the ones used for joins filters and aggregates.

  ```sql
    CREATE STATISTICS statistics_name ON customer (C_CUSTKEY)
    WITH FULLSCAN;
  ```

>[!IMPORTANT]
>Once you have created an external data source, you can use the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) command to create a queryable table over that source.

## SQL Server connector compatible types

You can also utilize the SQL Server (sqlserver://) connector to access Azure SQL Database. To accomplish this task, follow the same steps listed previously. Make sure the database scoped credential, server address, port, and location string correlate to that of the Azure SQL Database data source you want to connect to.


## Next steps

For more tutorials on creating external data sources and external tables to a variety of data sources, see [PolyBase Transact-SQL reference](polybase-t-sql-objects.md).

To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md).
