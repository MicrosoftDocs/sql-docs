---
title: sp_data_source_table_columns
description: sp_data_source_table_columns (Transact-SQL)
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords: 
  - sp_data_source_table_columns_TSQL
  - sys.sp_data_source_table_columns_TSQL
  - sp_data_source_table_columns
  - sys.sp_data_source_table_columns
dev_langs:
  - TSQL
helpviewer_keywords: 
  - "PolyBase"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: 
ms.custom: ""
ms.date: "1/7/2022"
---

# sp_data_source_table_columns (Transact-SQL)

[!INCLUDE [sqlserver2019](../../includes/applies-to-version/sqlserver2019.md)]

Returns list of columns in external data source table.
  
> [!NOTE]
> This procedure is introduced in [SQL 2019 CU5](../../big-data-cluster/release-notes-cumulative-updates-history.md#cu5).

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
sp_data_source_table_columns
         [ @data_source = ] 'data_source'
       , [ @table_location = ] 'table_location'
[ ; ]
```  

## Arguments

#### `[ @data_source = ] 'data_source'`   
The name of the external data source to get the metadata from. Type is `sysname`.

#### `[ @table_location = ] 'table_location'`   
The table location string that identifies the table. `table_location` type is `nvarchar(max)`.

## Returns

The stored procedure returns the following information:

|Column Name |Data Type |Description|
|---|---|---|
|NAME|nvarchar(max)|The name of the column.
|TYPE|nvarchar(200)|SQL Server type name
|LENGTH|int|Length of column
|PRECISION|int|Precision of column
|SCALE|int|Scale of column
|COLLATION|nvarchar(200)|SQL Server collation of column
|IS_NULLABLE|bit|1 = nullable, 0 = not nullable
|SOURCE_TYPE_NAME|nvarchar(max)|Backend-specific type name. Mostly used for debugging. For ODBC sources, `SOURCE_TYPE_NAME` will correspond to the `TYPE_NAME` result column for SQLColumns().
|REMARKS|nvarchar(max)|General comments or description of column. Currently always `NULL`.|

## Permissions  

Requires ALTER ANY EXTERNAL DATA SOURCE permission.
  
## Remarks  

The SQL Server instance must have the [PolyBase](../../relational-databases/polybase/polybase-guide.md) feature installed.

This stored procedure supports connectors for:

- SQL Server
- Oracle
- Teradata
- MongoDB
- CosmosDB

The stored procedure does not support generic ODBC data source or Hadoop connectors.

The notion of empty vs. non-empty relates to the behavior of the ODBC driver and the [`SQLTables` function](../native-client-odbc-api/sqltables.md). Non-empty indicates an object contains tables, not rows. For example, an empty schema contains no tables in SQL Server. An empty database contains with no tables inside Teradata.The results are a SQL Server representation of the backend schema as interpreted by the PolyBase connector for the backend. The distinction here is that instead of merely passing along the results of the ODBC call to the backend, the results are based on the outcome of the PolyBase type-mapping code.

Use [`sp_data_source_objects`](sp-data-source-objects.md) and `sp_data_source_table_columns` to discover external objects. These system stored procedures return the schema of tables that are available to be virtualized. Azure Data Studio uses these two stored procedures to support [data virtualization](../../azure-data-studio/extensions/data-virtualization-extension.md). Use `sp_data_source_table_columns` to discover external table schemas represented in SQL Server data types.

Due to differences between collations in Hadoop source data and supported collations in SQL Server 2019, the recommended data type lengths for varchar data type columns in external tables may be much larger than expected. This is by design.

Oracle synonyms are not supported for usage with PolyBase.

## Example  

The following example returns the table columns for an external table in a SQL Server named `server`, belonging to a schema named `schema`.
  
```sql
DECLARE @data_source SYSNAME = N'ExternalDataSourceName';
DECLARE @table_location NVARCHAR(400) = N'[database].[schema].[table]';
EXEC sp_data_source_table_columns @data_source, @table_location
```  
  
## See also

- [Get started with PolyBase](../polybase/polybase-guide.md)
- [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
- [CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)](../../t-sql/statements/create-external-table-as-select-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
