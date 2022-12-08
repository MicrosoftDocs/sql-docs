---
description: "sp_helpmergealternatepublisher (Transact-SQL)"
title: "sp_helpmergealternatepublisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_helpmergealternatepublisher_TSQL"
  - "sp_helpmergealternatepublisher"
helpviewer_keywords: 
  - "sp_helpmergealternatepublisher"
ms.assetid: a96e365f-5967-4580-9d79-5bacf2d12211
author: markingmyname
ms.author: maghan
---
# sp_helpmergealternatepublisher (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a list of all servers enabled as alternate Publishers for merge publications. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpmergealternatepublisher [ @publisher = ] 'publisher', [ @publisher_db = ] 'publisher_db', [ @publication = ] 'publication'  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the alternate Publisher.*publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publication database.*publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication.*publication* is **sysname**, with no default.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**alternate_publisher**|**sysname**|Name of the alternate Publisher.|  
|**alternate_publisher_db**|**sysname**|Name of the publication database.|  
|**alternate_publication**|**sysname**|Name of the publication.|  
|**alternate_distributor**|**sysname**|Name of the distributor.|  
|**friendly_name**|**nvarchar(255)**|Description of the alternate Publisher.|  
|**enabled**|**bit**|Specifies if the server is an alternate Publisher. **1** specifies that the Publisher is enabled as an alternate Publisher. **0** specifies that it is not enabled.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpmergealternatepublisher** is used in merge replication.  
  
 During every merge session, the system queries both the Publisher and Subscriber for each one's list of alternate publishers. The merge process adds or drops entries in the list of alternate publishers, the result of which is that the list of alternate publishers at both the Subscriber and Publisher match.  
  
## Permissions  
 Only members of the publication access list for the publication can execute **sp_helpmergealternatepublisher**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
