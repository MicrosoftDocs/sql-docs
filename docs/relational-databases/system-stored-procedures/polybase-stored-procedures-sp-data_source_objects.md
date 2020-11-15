---
description: "sp_data_source_objects (Transact-SQL)"
title: "sp_data_source_objects | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2020"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sp_data_source_objects"
helpviewer_keywords: 
  - "PolyBase"
ms.assetid: 48066431-fed2-4a8a-85af-ac704689e183
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# sp_data_source_objects (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Allows users to find out what the table objects are available to be virtualized. 
  
  Both sp_data_source_objects and [sp_data_source_columns](/polybase-stored-procedures-sp-data_source_columns.md) can be used by customers for schema discovery of external objects. These system stored procedures allow the user via T-SQL to see the schema of tables that are available to be virtualized. The two stored procedures will also be used in the implementation of the Data Virtualization Wizard extension for Azure Data Studio. Use [sp_data_source_columns](/polybase-stored-procedures-sp-data_source_columns.md) to discover external table schemas represented in SQL Server data types.

> [!NOTE]
> Both sp_data_source_objects and [sp_data_source_columns](/polybase-stored-procedures-sp-data_source_columns.md) were added in [SQL 2019 CU5](../../big-data-cluster/release-notes-big-data-cluster?view=sql-server-ver15#cu5).

 The SQL Server instance must have the [PolyBase](../../relational-databases/polybase/polybase-guide.md) feature installed. PolyBase enables the integration of non-SQL Server data sources, such as Hadoop and Azure blob storage. See also [sp_polybase_leave_group &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-leave-group.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_data_source_objects  
         [ @data_source = ] 'data_source'   
     [ , [ @object_root_name = ] 'object_root_name' ]
     [ , [ @max_search_depth = ] max_search_depth ]
     [ , [ @search_options = ] 'search_options' ]
[ ; ]
```  
  
## Arguments  
 [ @data_source = ] 'data_source' 

The name of the External Data Source to get the metadata from. data_source is sysname.  

[ @object_root_name = ] 'object_root_name' 

This is the root of the name of the object(s) that we are going to search for. object_root_name is nvarchar(max), with a default of NULL. 

The results of this call will only look at objects that begin with this. The syntax is according to our table location string syntax. This means that for a backend like Oracle, object_root_name must begin with the SID to yield any results. 

For other backends, a value of NULL indicates that there is no filter. 

The object_root_name must be made up of complete "parts". In the case of a data source that connects to an RDBMS that uses 3-part names, the string cannot contain a partial database name, for example. 

[ @max_search_depth = ] max_search_depth 

This value specifies the maximum depth (in parts) past the object_root_name that we wish to search. max_search_depth is an int with a default of 1. 

For example, a max_search_depth of 1, with an object_root_name that is the name of a SQL Server database would return schemata contained inside the database. 

A max_search_depth of NULL will just return information about object_root_name if it exists and is non-empty in the case of catalog or schema. 

[ @search_options = ] 'search_options' 

Contains a set of options that can configure the search behavior beyond what is available in the other parameters. search_options is nvarchar(max) with a default of NULL. 

This parameter currently isn't used but could be useful for future backends that have special requirements. 
 
## Result Sets
 

| Column Name | Data Type | Description |
|--|--|--|
| OBJECT_TYPE | nvarchar(200) | The type of the object (e.g. TABLE or DATABASE). |
| OBJECT_NAME | nvarchar(max) | The fully qualified name of the object. Escaped using backend-specific quote character. |
| OBJECT_LEAF_NAME | nvarchar(max) | The unqualified object name. |
| TABLE_LOCATION | nvarchar(max) | A valid table location string that could be used for a CREATE EXTERNAL TABLE statement. Will be NULL if it isn't applicable. |
  
## Permissions  
 Requires ALTER ANY EXTERNAL DATA SOURCE permission.  
  
## Remarks  
 
The notion of empty vs. non-empty relates to the behavior of the ODBC driver and the SQLTables function. Non-empty indicates an object contains tables, not rows. For example, an empty schema contains no tables in SQL Server. An empty database contains with no tables inside Teradata. 

Further, "table" objects are determined by the ODBC driver, so any external data source determines what qualifies as a "table". This can include things like functions in Teradata, or synonyms in Oracle. Some ODBC objects are not able to be pointed to in an external table definition and will therefore not have a value in the TABLE_LOCATION column. Despite that, the presence of one of these objects can make a database or schema non-empty.

Teradata's system views don't use row-level security (RLS), and so users can see the existence of tables that they cannot query. 

Some earlier versions of MongoDB restrict the ability to list all databases to admin-like users. Users without this permission may get auth errors trying to execute this procedure with a null object_root_name.
  
## Examples  

### A. Get all databases, schemata, and tables/views 

```
declare @data_source SYSNAME = N'SqlServerEDS'; 
declare @object_root_name NVARCHAR(MAX) = NULL; 
declare @max_search_depth INT = 3; 
EXEC sp_data_source_objects @data_source, @object_root_name, @max_search_depth;
```

| OBJECT_TYPE | OBJECT_NAME | OBJECT_LEAF_NAME | TABLE_LOCATION |
|--|--|--|--|
| DATABASE | "tpch0_01g" | tpch0_01g | NULL |
| SCHEMA | "tpch0_01g"."dbo" | dbo | NULL |
| TABLE | "tpch0_01g"."dbo"."customer" | customer | [tpch0_01g].[dbo].[customer] |
| TABLE | "tpch0_01g"."dbo"."lineitem" | lineitem | [tpch0_01g].[dbo].[lineitem] |
| TABLE | "tpch0_01g"."dbo"."nation" | nation | [tpch0_01g].[dbo].[nation] |

### B. Get all databases 

```
declare @data_source SYSNAME = N'SqlServerEDS'; 
declare @object_root_name NVARCHAR(MAX) = NULL; 
EXEC sp_data_source_objects @data_source, @object_root_name;
```

| OBJECT_TYPE | OBJECT_NAME | OBJECT_LEAF_NAME | TABLE_LOCATION |
|--|--|--|--|
| DATABASE | "DWQueue" | DWQueue | NULL |
| DATABASE | "master" | master | NULL |
| DATABASE | "msdb" | msdb | NULL |
| DATABASE | "tempdb" | tempdb | NULL |
| DATABASE | "tpch0_01g" | tpch0_01g | NULL |

### C. Get all schemata in a database 

```
declare @data_source SYSNAME = N'SqlServerEDS'; 
declare @object_root_name NVARCHAR(MAX) = N'[tpch0_01g]'; 
EXEC sp_data_source_objects @data_source, @object_root_name;
```

| OBJECT_TYPE | OBJECT_NAME | OBJECT_LEAF_NAME | TABLE_LOCATION |
|--|--|--|--|
| SCHEMA | "tpch0_01g"."dbo" | dbo | NULL |
| SCHEMA | "tpch0_01g"."INFORMATION_SCHEMA" | INFORMATION_SCHEMA | NULL |
| SCHEMA | "tpch0_01g"."sys" | sys | NULL |

### D. Get all tables in schema 

```
declare @data_source SYSNAME = N'SqlServerEDS'; 
declare @object_root_name NVARCHAR(MAX) = N'[tpch0_01g].[dbo]'; 
EXEC sp_data_source_objects @data_source, @object_root_name 
```

| OBJECT_TYPE | OBJECT_NAME | OBJECT_LEAF_NAME | TABLE_LOCATION |
|--|--|--|--|
| TABLE | "tpch0_01g"."dbo"."customer" | customer | [tpch0_01g].[dbo].[customer] |
| TABLE | "tpch0_01g"."dbo"."lineitem" | lineitem | [tpch0_01g].[dbo].[lineitem] |
| TABLE | "tpch0_01g"."dbo"."nation" | nation | [tpch0_01g].[dbo].[nation] |
| TABLE | "tpch0_01g"."dbo"."orders" | orders | [tpch0_01g].[dbo].[orders] |
| TABLE | "tpch0_01g"."dbo"."part" | part | [tpch0_01g].[dbo].[part] |


## Oracle

### A. Get all schemata and tables/views/synonyms/etc. 

```
declare @data_source SYSNAME = N'OracleEDS'; 
declare @object_root_name NVARCHAR(MAX) = N'[XE]'; 
declare @max_search_depth INT = 2; 
EXEC sp_data_source_objects @data_source, @object_root_name, @max_search_depth;
```

| OBJECT_TYPE | OBJECT_NAME | OBJECT_LEAF_NAME | TABLE_LOCATION |
|--|--|--|--|
| VIEW | "SYS"."ALL_SQLSET_STATEMENTS" | ALL_SQLSET_STATEMENTS | [XE].[SYS].[ALL_SQLSET_STATEMENTS] |
| SYSTEM TABLE | "SYS"."BOOTSTRAP$" | BOOTSTRAP$ | [XE].[SYS].[BOOTSTRAP$] |
| SYNONYM | "PUBLIC"."ALL_ALL_TABLES" | ALL_ALL_TABLES | NULL |
| SCHEMA | "tpch0_01g" | tpch0_01g | NULL |
| TABLE | "tpch0_01g"."customer" | customer | [XE].[tpch0_01g].[customer] |

## Teradata 

### A. Get all databases and tables/functions/views/etc. 

```
declare @data_source SYSNAME = N'TeradataEDS'; 
declare @object_root_name NVARCHAR(MAX) = NULL; 
declare @max_search_depth INT = 2; 
EXEC sp_data_source_objects @data_source, @object_root_name, @max_search_depth;
```

| OBJECT_TYPE | OBJECT_NAME | OBJECT_LEAF_NAME | TABLE_LOCATION |  |
|--|--|--|--|
| FUNCTION | "SYSLIB"."ExtractRoles" | ExtractRoles | NULL |  |
| SYSTEM TABLE | "DBC"."UDTCast" | UDTCast | [DBC].[UDTCast] |  |
| TYPE | "SYSUDTLIB"."XML" | XML | NULL |  |
| DATABASE | "tpch0_01g" | tpch0_01g | NULL |
| TABLE | "tpch0_01g"."customer" | customer | [tpch0_01g].[customer] |  |


## Mongo DB 

### A. Get all databases and tables 

```
declare @data_source SYSNAME = N'MongoDbEDS'; |
declare @object_root_name NVARCHAR(MAX) = NULL; 
declare @max_search_depth INT = 2; 
EXEC sp_data_source_objects @data_source, @object_root_name, @max_search_depth;
```

| OBJECT_TYPE | OBJECT_NAME | OBJECT_LEAF_NAME | TABLE_LOCATION |
|--|--|--|--|
| DATABASE | "tpch0_01g" | tpch0_01g | NULL |
| TABLE | "tpch0_01g"."customer" | customer | [tpch0_01g].[customer] |
| TABLE | "tpch0_01g"."lineitem" | lineitem | [tpch0_01g].[lineitem] |
| TABLE | "tpch0_01g"."nation" | nation | [tpch0_01g].[nation] |
| TABLE | "tpch0_01g"."orders" | orders | [tpch0_01g].[orders] |

  
## See Also  
 [Get started with PolyBase](../polybase/polybase-guide.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
