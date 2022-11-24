---
description: "sys.syscacheobjects (Transact-SQL)"
title: "sys.syscacheobjects (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.syscacheobjects_TSQL"
  - "sys.syscacheobjects"
  - "syscacheobjects"
  - "syscacheobjects_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syscacheobjects system table"
  - "sys.syscacheobjects compatibility view"
ms.assetid: 9b14f37c-b7f5-4f71-b070-cce89a83f69e
author: rwestMSFT
ms.author: randolphwest
---
# sys.syscacheobjects (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains information about how the cache is used.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**bucketid**|**int**|Bucket ID. Value indicates a range from 0 through (directory size - 1). Directory size is the size of the hash table.|  
|**cacheobjtype**|**nvarchar(17)**|Type of object in the cache:<br /><br /> Compiled plan<br /><br /> Executable plan<br /><br /> Parse tree<br /><br /> Cursor<br /><br /> Extended stored procedure|  
|**objtype**|**nvarchar(8)**|Type of object:<br /><br /> Stored procedure<br /><br /> Prepared statement<br /><br /> Ad hoc query ([!INCLUDE[tsql](../../includes/tsql-md.md)] submitted as language events from the **sqlcmd** or **osql** utilities, instead of remote procedure calls)<br /><br /> ReplProc (replication procedure)<br /><br /> Trigger<br /><br /> View<br /><br /> Default<br /><br /> User table<br /><br /> System table<br /><br /> Check<br /><br /> Rule|  
|**objid**|**int**|One of the main keys used for looking up an object in the cache. This is the object ID stored in **sysobjects** for database objects (procedures, views, triggers, and so on). For cache objects such as ad hoc or prepared SQL, **objid** is an internally generated value.|  
|**dbid**|**smallint**|Database ID in which the cache object was compiled.|  
|**dbidexec**|**smallint**|Database ID from which the query is executed.<br /><br /> For most objects, **dbidexec** has the same value as **dbid**.<br /><br /> For system views, **dbidexec** is the database ID from which the query is executed.<br /><br /> For ad hoc queries, **dbidexec** is 0. This means **dbidexec** has the same value as **dbid**.|  
|**uid**|**smallint**|Indicates the creator of the plan for ad hoc query plans and prepared plans.<br /><br /> -2 = The batch submitted does not depend on implicit name resolution and can be shared among different users. This is the preferred method. Any other value represents the user ID of the user submitting the query in the database.<br /><br /> Overflows or returns NULL if the number of users and roles exceeds 32,767.|  
|**refcounts**|**int**|Number of other cache objects referencing this cache object. A count of 1 is the base.|  
|**usecounts**|**int**|Number of times this cache object has been used since inception.|  
|**pagesused**|**int**|Number of pages consumed by the cache object.|  
|**setopts**|**int**|SET option settings that affect a compiled plan. These settings are part of the cache key. Changes to values in this column indicate users have modified SET options. These options include the following:<br /><br /> **ANSI_PADDING**<br /><br /> **FORCEPLAN**<br /><br /> **CONCAT_NULL_YIELDS_NULL**<br /><br /> **ANSI_WARNINGS**<br /><br /> **ANSI_NULLS**<br /><br /> **QUOTED_IDENTIFIER**<br /><br /> **ANSI_NULL_DFLT_ON**<br /><br /> **ANSI_NULL_DFLT_OFF**|  
|**langid**|**smallint**|Language ID. ID of the language of the connection that created the cache object.|  
|**dateformat**|**smallint**|Date format of the connection that created the cache object.|  
|**status**|**int**|Indicates whether the cache object is a cursor plan. Currently, only the least significant bit is used.|  
|**lasttime**|**bigint**|For backward compatibility only. Always returns 0.|  
|**maxexectime**|**bigint**|For backward compatibility only. Always returns 0.|  
|**avgexectime**|**bigint**|For backward compatibility only. Always returns 0.|  
|**lastreads**|**bigint**|For backward compatibility only. Always returns 0.|  
|**lastwrites**|**bigint**|For backward compatibility only. Always returns 0.|  
|**sqlbytes**|**int**|Length in bytes of the procedure definition or batch submitted.|  
|**sql**|**nvarchar(3900)**|Module definition or the first 3900 characters of the batch submitted.|  
  
## See Also  
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  

