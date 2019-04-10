---
title: "Configure PolyBase to access external data in Oracle | Microsoft Docs"
ms.custom: ""
ms.date: 04/10/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
author: Abiola
ms.author: aboke
manager: craigg
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Configure PolyBase to access external data in Oracle

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The article explains how to use PolyBase on a SQL Server instance to query external data in Oracle.

## Prerequisites

If you haven't installed PolyBase, see [PolyBase installation](polybase-installation.md).

## Configure an External Table

To query the data from an Oracle data source, you must create external tables to reference the external data. This section provides sample code to create these external tables. 
 
The following Transact-SQL commands are used in this section:

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md) 
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md) 
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)

1. Create a master key on the database if one does not already exist. This is required to encrypt the credential secret.

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  
   ```

   > [!NOTE]
   > `PASSWORD` is the password that is used to encrypt the master key in the database. It must meet the Windows password policy requirements of the computer that is hosting the instance of SQL Server.

1. Create a database scoped credential.

   ```sql
   /*  
   * Specify credentials to external data source
   * IDENTITY: user name for external source.  
   * SECRET: password for external source.
   */
   CREATE DATABASE SCOPED CREDENTIAL credential_name
   WITH IDENTITY = 'username', Secret = 'password';
   ```

1. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md). Specify external data source location and credentials for the Oracle data source.

   ```sql
   /* 
   * LOCATION: Location string should be of format '<vendor>://<server>[:<port>]'.
   * PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
   * CONNECTION_OPTIONS: Specify driver location
   * CREDENTIAL: the database scoped credential, created above.
   */  
   CREATE EXTERNAL DATA SOURCE external_data_source_name
   WITH ( 
     LOCATION = 'oracle://<server address>[:<port>]',
     -- PUSHDOWN = ON | OFF,
     CREDENTIAL = credential_name)
   ```

1. Use [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) to create external tables that represents data stored in external Oracle system.

   ```sql
   /*
   * LOCATION: Oracle table/view in '<database_name>.<schema_name>.<object_name>' format
   * DATA_SOURCE: the external data source, created above.
   */
   CREATE EXTERNAL TABLE customers(
   [O_ORDERKEY] DECIMAL(38) NOT NULL,
   [O_CUSTKEY] DECIMAL(38) NOT NULL,
   [O_ORDERSTATUS] CHAR COLLATE Latin1_General_BIN NOT NULL,
   [O_TOTALPRICE] DECIMAL(15,2) NOT NULL,
   [O_ORDERDATE] DATETIME2(0) NOT NULL,
   [O_ORDERPRIORITY] CHAR(15) COLLATE Latin1_General_BIN NOT NULL,
   [O_CLERK] CHAR(15) COLLATE Latin1_General_BIN NOT NULL,
   [O_SHIPPRIORITY] DECIMAL(38) NOT NULL,
   [O_COMMENT] VARCHAR(79) COLLATE Latin1_General_BIN NOT NULL
   )
   WITH (
    LOCATION='customer',
    DATA_SOURCE=  external_data_source_name
   );
   ```

1. **Optional:** Create statistics on the external table with the following command.

   ```sql
   CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
   ```

   > [!TIP]
   > It is recommended to create statistics on external table columns, especially the ones used for joins, filters, and aggregates. This results in optimal query performance.

## Next steps

To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md).
