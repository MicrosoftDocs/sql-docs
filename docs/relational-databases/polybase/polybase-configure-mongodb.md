---
title: "Configure PolyBase to access external data in MongoDB | Microsoft Docs"
ms.custom: ""
ms.date: 09/24/2018
ms.prod: sql
ms.reviewer: ""
ms.suite: "sql"
ms.technology: polybase
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: Abiola
ms.author: aboke
manager: craigg
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Configure PolyBase to access external data in MongoDB

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The article explains how to use PolyBase on a SQL Server instance to query external data in MongoDB.

## Prerequisites

If you haven't installed PolyBase, see [PolyBase installation](polybase-installation.md). The installation article explains the prerequisites.

## Configure an External Table

To query the data from an MongoDB data source, you must create external tables to reference the external data. This section provides sample code to create these external tables. 
 
We recommend creating statistics on external table columns, especially the ones used for joins, filters and aggregates, for optimal query performance.

These objects will create in this section:

- CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL) 
- CREATE EXTERNAL DATA SOURCE (Transact-SQL) 
- CREATE EXTERNAL FILE FORMAT (Transact-SQL) 
- CREATE EXTERNAL TABLE (Transact-SQL) 
- CREATE STATISTICS (Transact-SQL)


1.    Create a master key on the database. This is required to encrypt the credential secret.

      ```sql
      CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0me!nfo';  
      ```

1.   Create a database scoped credential.

     ```sql
     /*  specify credentials to external data source
     *  IDENTITY: user name for external source.  
     *  SECRET: password for external source.
     */
     CREATE DATABASE SCOPED CREDENTIAL MongoDBCredentials 
     WITH IDENTITY = 'username', Secret = 'password';
     ```

1.  Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md). Specify external data source location and credentials for the MongoDB data source.

     ```sql
     /*  LOCATION: Server DNS name or IP address.
     *  PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
     *  CREDENTIAL: the database scoped credential, created above.
     */  
     CREATE EXTERNAL DATA SOURCE MongoDBInstance
     WITH ( 
      LOCATION = '<vendor>://<server>[:<port>]',
     -- PUSHDOWN = ON | OFF,
    , CREDENTIAL = MongoDBCredentials
     );
     ```

1. Create schemas for external data

     ```sql
     CREATE SCHEMA MongoDB;
     GO
     ```

1.  Create external tables that represents data stored in external MongoDB system [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).
 
     ```sql
     /*  LOCATION: MongoDB table/view in '<database_name>.<schema_name>.<object_name>' format
     *  DATA_SOURCE: the external data source, created above.
     */
     CREATE EXTERNAL TABLE MongoDB.orders(
     [O_ORDERKEY] DECIMAL(38) NOT NULL,
     [O_CUSTKEY] DECIMAL(38) NOT NULL,
     [O_ORDERSTATUS] CHAR COLLATE Latin1_General_BIN NOT NULL,
     [O_TOTALPRICE] DECIMAL(15,2) NOT NULL,
     [O_ORDERDATE] DATETIME2(0) NOT NULL,
     [O_COMMENT] VARCHAR(79) COLLATE Latin1_General_BIN NOT NULL
     )
     WITH (
     LOCATION='TPCH..ORDERS',
     DATA_SOURCE= MongoDBInstance
     );
     ```

1. Create statistics on an external table for optimized performance.

     ```sql
      CREATE STATISTICS OrdersOrderKeyStatistics ON MongoDB.orders(O_ORDERKEY) WITH FULLSCAN;
     ```

##Flattening

 
## Next steps

To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md).