---
title: "Configure PolyBase to access external data in Teradata | Microsoft Docs"
ms.custom: ""
ms.date: 04/23/2019
ms.prod: sql
ms.technology: polybase
ms.topic: conceptual
author: Abiola
ms.author: aboke
ms.reviewer: jroth
manager: craigg
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"
---
# Configure PolyBase to access external data in Teradata

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article explains how to use PolyBase on a SQL Server instance to query external data in Teradata.

## Prerequisites

If you haven't installed PolyBase, see [PolyBase installation](polybase-installation.md). The installation article explains the prerequisites.

Before creating a database scoped credential a [Master Key](../../t-sql/statements/create-master-key-transact-sql.md) must be created. 

To use PolyBase on Teradata, VC++ redistributable is needed.
 
## Configure a Teradata external data source

To query the data from a Teradata data source, you must create external tables to reference the external data. This section provides sample code to create these external tables. 



The following Transact-SQL commands are used in this section:

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md) 
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)

1. Create a database scoped credential for accessing the MongoDB source.

    ```sql
    /*  specify credentials to external data source
    *  IDENTITY: user name for external source. 
    *  SECRET: password for external source.
    */
    CREATE DATABASE SCOPED CREDENTIAL credential_name WITH IDENTITY = 'username', Secret = 'password';
    ```

1. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).

    ```sql
    /*  LOCATION: Location string should be of format '<vendor>://<server>[:<port>]'.
    *  PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
    * CONNECTION_OPTIONS: Specify driver location
    *  CREDENTIAL: the database scoped credential, created above.
    */  
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH (LOCATION = teradata://<server address>[:<port>],
    -- PUSHDOWN = ON | OFF,
    CREDENTIAL =credential_name);
    ```

1. **Optional:** Create statistics on an external table.

    We recommend creating statistics on external table columns, especially the ones used for joins, filters and aggregates, for optimal query performance.

    ```sql
    CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
    ```

>[!IMPORTANT] 
>Once you have created an external data source, you can use the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) command to create a queryable table over that source.

## Next steps

To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md).
