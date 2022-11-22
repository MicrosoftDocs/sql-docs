---
description: "The sys.fn_xe_file_target_read_file system function reads files that are created by the Extended Events asynchronous file target."
title: "sys.fn_xe_file_target_read_file (Transact-SQL)"
ms.custom: ""
ms.date: "03/21/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
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
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_xe_file_target_read_file (Transact-SQL)
[!INCLUDE [SQL Server SQL Database SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Reads files that are created by the Extended Events asynchronous file target. One event, in XML format, is returned per row. 

  The Extended Events `event_file` target stores the data it receives in a binary format that is not human readable. Read the contents of the `.xel` file with the `sys.fn_xe_file_target_read_file` function. These files can also be read from [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For a walkthrough, see [Quickstart: Extended events in SQL Server](../extended-events/quick-start-extended-events-in-sql-server.md).
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
sys.fn_xe_file_target_read_file ( path, mdpath, initial_file_name, initial_offset )  
```  
  
## Arguments  

#### *path*  
 The path to the files to read. *path* can contain wildcards and include the name of a file. *path* is **nvarchar(260)**. There is no default. In the context of Azure SQL Database, this value is an HTTP URL to a file in Azure Storage.
  
#### *mdpath*  
 The path to the metadata file that corresponds to the file or files specified by the *path* argument. *mdpath* is **nvarchar(260)**. There is no default. [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] does not require the *mdpath* parameter. However, it is maintained for backward compatibility for log files generated in previous versions of SQL Server. Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]
, this parameter can be given as null as `.xem` files are no longer used.
  
#### *initial_file_name*  
 The first file to read from *path*. *initial_file_name* is **nvarchar(260)**. There is no default. If **null** is specified as the argument all the files found in *path* are read.  
  
> [!NOTE]  
>  *initial_file_name* and *initial_offset* are paired arguments. If you specify a value for either argument you must specify a value for the other argument.  
  
#### *initial_offset*  
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
|timestamp_utc|**datetime2(7)**|**Applies to**: [!INCLUDE[sssql17](../../includes/sssql17-md.md)] and later and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br />The date and time (UTC timezone) of the event. Is not nullable.|  

  
## Remarks  
 Reading large result sets by executing `sys.fn_xe_file_target_read_file` in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] may result in an error. Use the **Results to File** mode (in SSMS, **Ctrl+Shift+F**) to export large result sets to a human-readable file, to  read the file with another tool instead.  

 [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] accept trace results generated in XEL and XEM format. [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Extended Events only support trace results in XEL format. We recommend that you use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to read trace results in XEL format.    
  
### Azure SQL

 In [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] or [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], store .xel files in Azure Blob Storage. You can use `sys.fn_xe_file_target_read_file` to read from extended event sessions you create yourself and store in Azure Blob Storage. For example walkthrough, review [Event File target code for extended events in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file).

If you specify wildcard and/or a path for a local file system, you will receive an error message similar to:

```
Msg 40538, Level 16, State 3, Line 15
A valid URL beginning with 'https://' is required as value for any filepath specified.
```

## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Examples  
  
### A. Retrieving data from file targets
 
 For [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and previous versions, the following example gets all the rows from all the files, including both the `.xel` and `.xem` file. In this example, the file targets and metafiles are located in the trace folder in the `C:\traces\` folder.
  
```sql  
SELECT * FROM sys.fn_xe_file_target_read_file('C:\traces\*.xel', 'C:\traces\metafile.xem', null, null);  
```  
 
 In [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)] and later, the following example retrieves events inside all `.xel` files in the default folder. The default location is `\MSSQL\Log` within the installation folder of the instance.

```sql
SELECT * FROM sys.fn_xe_file_target_read_file('*.xel', null, null, null)
```
 
 In [!INCLUDE[sssql17](../../includes/sssql17-md.md)] or later, the following example retrieves only data from the last day, from the built-in system_health session. The system_health session is an Extended Events session that is included by default with SQL Server. For more information, see [Use the system_health session](../extended-events/use-the-system-health-session.md).

```sql
SELECT * FROM sys.fn_xe_file_target_read_file('system_health*.xel', null, null, null)
WHERE cast(timestamp_utc as datetime2(7)) > dateadd(day, -1, GETUTCDATE())
```


## See also

- [Extended Events Dynamic Management Views](../../relational-databases/system-dynamic-management-views/extended-events-dynamic-management-views.md)   
- [Extended Events Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-events-catalog-views-transact-sql.md)   
- [Extended Events](../../relational-databases/extended-events/extended-events.md)  

  
## Next steps

- [Targets for Extended Events in SQL Server](../extended-events/targets-for-extended-events-in-sql-server.md)
- [Advanced Viewing of Target Data from Extended Events in SQL Server](../extended-events/advanced-viewing-of-target-data-from-extended-events-in-sql-server.md)
- [Convert an Existing SQL Trace Script to an Extended Events Session](../extended-events/convert-an-existing-sql-trace-script-to-an-extended-events-session.md)
- [Use the system_health session](../extended-events/use-the-system-health-session.md)