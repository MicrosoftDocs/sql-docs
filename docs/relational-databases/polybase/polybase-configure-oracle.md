---
title: "Access external data: Oracle - PolyBase"
description: This article demonstrates how to use PolyBase to create an external data source to access Oracle data.
ms.date: 12/13/2019
ms.custom: seo-lt-2019
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mikeray
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-ver15"
---
# Configure PolyBase to access external data in Oracle

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The article explains how to use PolyBase on a SQL Server instance to query external data in Oracle.

## Prerequisites

If you haven't installed PolyBase, see [PolyBase installation](polybase-installation.md).

  Before creating a database scoped credential, a [Master Key](../../t-sql/statements/create-master-key-transact-sql.md) must be created. 

## Configure an Oracle external data source

To query the data from an Oracle data source, you must create external tables to reference the external data. This section provides sample code to create these external tables.

The following Transact-SQL commands are used in this section:

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)


1. Create a database scoped credential for accessing the Oracle source.

    ```sql
    /*  specify credentials to external data source
    *  IDENTITY: user name for external source. 
    *  SECRET: password for external source.
    */
    CREATE DATABASE SCOPED CREDENTIAL credential_name WITH IDENTITY = 'username', Secret = 'password';
    ```
    
   > [!IMPORTANT] 
   > The Oracle ODBC Connector for PolyBase supports only basic authentication, not Kerberos authentication. 

1. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).

    ```sql
    /* 
    * LOCATION: Location string should be of format '<vendor>://<server>[:<port>]'.
    * PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
    * CONNECTION_OPTIONS: Specify driver location
    * CREDENTIAL: the database scoped credential, created above.
    */  
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH ( LOCATION = 'oracle://<server address>[:<port>]',
    -- PUSHDOWN = ON | OFF,
    CREDENTIAL = credential_name)
    ```

1. Create an external table with [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

    ```sql
    /*
    * LOCATION: Three-part identifier indicating database & domain or only database, schema, and table name.
    * DATA_SOURCE: The data source created above.
    */
    CREATE EXTERNAL TABLE [T1] (
      [KEY] DECIMAL(38) NOT NULL,
      [RANDOM_INT] DECIMAL(38),
      [RANDOM_FLOAT] FLOAT(53))
    WITH (
      LOCATION = '[ORCLCDB.localdomain].SYS.T1',
      DATA_SOURCE = external_data_source_name)
    ```

1. **Optional:** Create statistics on an external table.

    We recommend creating statistics on external table columns especially the ones used for joins, filters and aggregates,for optimal query performance.

    ```sql
    CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
    ```

>[!IMPORTANT] 
>Once you have created an external data source, you can use the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) command to create a queryable table over that source. 

## Next steps

For more tutorials on creating external data sources and external tables to a variety of data sources, see [PolyBase Transact-SQL reference](polybase-t-sql-objects.md).

For more information and examples, see the following articles:

- [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).
- [Overview of SQL Server PolyBase](polybase-guide.md).
