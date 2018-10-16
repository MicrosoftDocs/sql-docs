---
title: "DBCC PROCCACHE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DBCC PROCCACHE"
  - "DBCC_PROCCACHE_TSQL"
  - "PROCCACHE_TSQL"
  - "PROCCACHE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "procedure cache [SQL Server]"
  - "displaying procedure cache information"
  - "DBCC PROCCACHE statement"
ms.assetid: 7a4f9f8a-13ff-4bf2-ba29-c17012a23659
author: uc-msft
ms.author: umajay
manager: craigg
---
# DBCC PROCCACHE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Displays information in a table format about the procedure cache.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DBCC PROCCACHE [ WITH NO_INFOMSGS ]  
```  
  
## Arguments  
 WITH  
 Allows for options to be specified.  
  
 NO_INFOMSGS  
 Suppresses all informational messages that have severity levels 0 through 10.  
  
## Remarks  
The procedure cache is used to cache the compiled and executable plans to speed up the execution of batches. The entries in a procedure cache are at a batch level. The procedure cache includes the following entries:
-   Compiled plans  
-   Execution plans  
-   Algebrizer tree  
-   Extended procedures  
  
## Result Sets  
The following table describes the columns of the result set.
  
|Column name|Description|  
|-----------------|-----------------|  
|**num proc buffs**|Total number of pages used by all entries in the procedure cache.|  
|**num proc buffs used**|Total number of pages used by all entries that are currently being used.|  
|**num proc buffs active**|For backward compatibility only. Total number of pages used by all entries that are currently being used.|  
|**proc cache size**|Total number of entries in the procedure cache.|  
|**proc cache used**|Total number of entries that are currently being used.|  
|**proc cache active**|For backward compatibility only. Total number of entries that are currently being used.|  
  
## Permissions  
Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)
  
  
