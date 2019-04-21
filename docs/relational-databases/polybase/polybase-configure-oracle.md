---
title: "Configure PolyBase to access external data in Oracle | Microsoft Docs"
description: This article demonstrates how to use PolyBase to create an external data source to access Oracle data.
ms.date: 04/10/2019
ms.prod: sql
ms.technology: polybase
ms.topic: conceptual
author: Abiola
ms.author: aboke
ms.reviewer: jroth
manager: craigg
ms.custom: ""
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Configure PolyBase to access external data in Oracle

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

The article explains how to use PolyBase on a SQL Server instance to query external data in Oracle.

## Prerequisites

- [Install PolyBase](polybase-installation.md).

- A master key on the database. For more information, see [CREATE MASTER KEY](../../t-sql/statements/create-master-key-transact-sql.md).

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  
   ```

## Create external data source

To query the data from an Oracle data source, you must first create an external data source to Oracle with the following steps.

1. Create a database scoped credential for the external data source with the [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md) command.

   ```sql
   /*  
   * Specify credentials to external data source
   * IDENTITY: user name for external source.
   * SECRET: password for external source.
   */
   CREATE DATABASE SCOPED CREDENTIAL credential_name
   WITH IDENTITY = 'username', Secret = 'password';
   ```

1. Create an external data source with the [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md) command. Specify external data source location and credentials for the Oracle data source.

   ```sql
   /*
   * LOCATION: Location string should be of format '<vendor>://<server>[:<port>]'.
   * PUSHDOWN: Specify whether computation should be pushed down to the source. ON by default.
   * CREDENTIAL: the database scoped credential, created above.
   */  
   CREATE EXTERNAL DATA SOURCE external_data_source_name
   WITH (
     LOCATION = 'oracle://<server address>[:<port>]',
     -- PUSHDOWN = ON | OFF,
     CREDENTIAL = credential_name)
   ```

## Next steps

After creating the data source, you can create an external table by specifying the column names and compatible SQL Server types for each column.

For more information and examples, see the following articles:

- [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).
- [Overview of SQL Server PolyBase](polybase-guide.md).
