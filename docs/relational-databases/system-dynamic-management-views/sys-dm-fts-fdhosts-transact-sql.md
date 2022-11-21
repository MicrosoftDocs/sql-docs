---
title: "sys.dm_fts_fdhosts (Transact-SQL)"
description: sys.dm_fts_fdhosts (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/29/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_fts_fdhosts"
  - "dm_fts_fdhosts_TSQL"
  - "sys.dm_fts_fdhosts"
  - "sys.dm_fts_fdhosts_TSQL"
helpviewer_keywords:
  - "sys.dm_fts_fdhosts dynamic management view"
  - "troubleshooting [SQL Server], full-text search"
dev_langs:
  - "TSQL"
ms.assetid: d42a6334-4362-4361-83da-f8324fe55ec7
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_fts_fdhosts (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns information on the current activity of the filter daemon host or hosts on the server instance.  
  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**fdhost_id**|**int**|ID of the filter daemon host.|  
|**fdhost_name**|**nvarchar(120)**|Name of filter daemon host.|  
|**fdhost_process_id**|**int**|Windows process ID of the filter daemon host.|  
|**fdhost_type**|**nvarchar(120)**|Type of document being processed by the filter daemon host, one of:<br /><br /> Single thread<br /><br /> Multi-thread<br /><br /> Huge document|  
|**max_thread**|**int**|Maximum number of threads in the filter daemon host.|  
|**batch_count**|**int**|Number of batches that are being processed in the filter daemon host.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Examples  
 The following example returns the name of the filter daemon host and the maximum number of threads in it. It also monitors how many batches are currently being processed in the filter daemon. This information can be used to diagnose performance.  
  
```  
SELECT fdhost_name, batch_count, max_thread FROM sys.dm_fts_fdhosts;  
GO  
```  
  
## See Also  
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
