---
title: "sys.fn_xe_file_target_read_file (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/22/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "fn_xe_file_target_read_file_TSQL"
  - "fn_xe_file_target_read_file"
  - "sys.fn_xe_file_target_read_file_TSQL"
  - "sys.fn_xe_file_target_read_file"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "extended events [SQL Server], functions"
  - "fn_xe_file_target_read_file function"
  - "sys.fn_xe_file_target_read_file function"
ms.assetid: cc0351ae-4882-4b67-b0d8-bd235d20c901
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_xe_file_target_read_file (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Reads files that are created by the Extended Events asynchronous file target. One event, in XML format, is returned per row.  
  
> [!WARNING]  
>  [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] accept trace results generated in XEL and XEM format. [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Extended Events only support trace results in XEL format. We recommend that you use SQL Server Management Studio to read trace results in XEL format.    
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_xe_file_target_read_file ( path, mdpath, initial_file_name, initial_offset )  
```  
  
## Arguments  
 *path*  
 The path to the files to read. *path* can contain wildcards and include the name of a file. *path* is **nvarchar(260)**. There is no default. In the context of Azure SQL Database, this value is an HTTP URL to a file in Azure Storage.
  
 *mdpath*  
 The path to the metadata file that corresponds to the file or files specified by the *path* argument. *mdpath* is **nvarchar(260)**. There is no default. Starting with SQL Server 2016, this parameter can be given as null.
  
> [!NOTE]  
>  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] does not require the *mdpath* parameter. However, it is maintained for backward compatibility for log files generated in previous versions of SQL Server.  
  
 *initial_file_name*  
 The first file to read from *path*. *initial_file_name* is **nvarchar(260)**. There is no default. If **null** is specified as the argument all the files found in *path* are read.  
  
> [!NOTE]  
>  *initial_file_name* and *initial_offset* are paired arguments. If you specify a value for either argument you must specify a value for the other argument.  
  
 *initial_offset*  
 Used to specify last offset read previously and skips all events up to the offset (inclusive). Event enumeration starts after the offset specified. *initial_offset* is **bigint**. If **null** is specified as the argument the entire file will be read.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|module_guid|**uniqueidentifier**|The event module GUID. Is not nullable.|  
|package_guid|**uniqueidentifier**|The event package GUID. Is not nullable.|  
|object_name|**nvarchar(256)**|The name of the event. Is not nullable.|  
|event_data|**nvarchar(max)**|The event contents, in XML format. Is not nullable.|  
|file_name|**nvarchar(260)**|The name of the file that contains the event. Is not nullable.|  
|file_offset|**bigint**|The offset of the block in the file that contains the event. Is not nullable.|  
|timestamp_utc|**datetime2**|**Applies to**: [!INCLUDE[ssSQLv14](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br />The date and time (UTC timezone) of the event. Is not nullable.|  

  
## Remarks  
 Reading large result sets by executing **sys.fn_xe_file_target_read_file** in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] may result in an error. Use the **Results to File** mode (**Ctrl+Shift+F**) to export large result sets to a file and read the file with another tool instead.  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Examples  
  
### A. Retrieving data from file targets  
 The following example gets all the rows from all the files. In this example the file targets and metafiles are located in the trace folder on the C:\ drive.  
  
```  
SELECT * FROM sys.fn_xe_file_target_read_file('C:\traces\*.xel', 'C:\traces\metafile.xem', null, null);  
```  
  
## See Also  
 [Extended Events Dynamic Management Views](../../relational-databases/system-dynamic-management-views/extended-events-dynamic-management-views.md)   
 [Extended Events Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-events-catalog-views-transact-sql.md)   
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
