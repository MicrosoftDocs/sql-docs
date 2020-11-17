---
description: "sp_data_source_table_columns (Transact-SQL)"
title: "sp_data_source_table_columns | Microsoft Docs"
ms.custom: ""
ms.date: "11/10/2020"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sp_data_source_table_columns"
helpviewer_keywords: 
  - "PolyBase"
author: MikeRayMSFT
ms.author: mikeray
---
# sp_data_source_table_columns (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

List columns in external data source.
  
The SQL Server instance must have the  [PolyBase](../../relational-databases/polybase/polybase-guide.md) feature installed.  PolyBase enables the integration of non-SQL Server data sources, such as Hadoop and Azure blob storage. See also [sp_polybase_leave_group &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-leave-group.md).  

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sqlsyntax
sp_data_source_table_columns
         [ @data_source = ] 'data_source'
       , [ @table_location = ] 'table_location'
[ ; ]
```  

## Arguments

`[ @data_source = ] 'data_source'`
   The name of the external data source to get the metadata from. Type is `sysname`.

`[ @table_location = ] 'table_location'`
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
|IS_NULLABLE|bit|Is this column nullable.
|SOURCE_TYPE_NAME|nvarchar(max)|Backend-specific type name. Mostly used for debugging. For ODBC sources this will correspond to the TYPE_NAME result column for SQLColumns().
|REMARKS|nvarchar(max)|General comments or description of column. Currently always NULL.|

## Permissions  

Requires ALTER ANY EXTERNAL DATA SOURCE permission.
  
## Remarks  

The results will be a SQL Server representation of the backend schema as interpreted by the PolyBase connector for the backend. The distinction here is that instead of merely passing along the results of the ODBC call to the backend, the results would be based on the outcome of the PolyBase type mapping code. This is necessary because just giving the user the results of the ODBC call wouldn’t be enough to figure out how they should define their External Table.  

> [!NOTE]
> This article describes objects introduced in SQL Server 2019 CU5. Both [`sp_data_source_objects`](sp-data-source-objects.md) and `sp_data_source_columns` were added in [SQL 2019 CU5](../../big-data-cluster/release-notes-big-data-cluster.md#cu5).
  
Use [`sp_data_source_objects`](sp-data-source-objects.md) and `sp_data_source_table_columns` to discover external objects. These system stored procedures return the schema of tables that are available to be virtualized. Azure Data Studio uses these two stored procedures to support [data virtualization](../../azure-data-studio/extensions/data-virtualization-extension.md). Use `sp_data_source_table_columns` to discover external table schemas represented in SQL Server data types.
  
## Example  

The following example returns the table columns for an external table in a SQL Server named `server`, belonging to a schema named `schema`.
  
```sql
declare @data_source SYSNAME = N'ServerName';
declare @table_location NVARCHAR(400) = '[server].[schema].[table]';
EXEC sp_data_source_table_columns @data_source, @table_location
```  
  
## See also

- [Get started with PolyBase](../polybase/polybase-guide.md)
- [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)