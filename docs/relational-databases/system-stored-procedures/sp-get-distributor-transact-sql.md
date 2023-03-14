---
title: "sp_get_distributor (Transact-SQL)"
description: "sp_get_distributor (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_get_distributor"
  - "sp_get_distributor_TSQL"
helpviewer_keywords:
  - "sp_get_distributor"
dev_langs:
  - "TSQL"
---
# sp_get_distributor (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Determines whether a Distributor is installed on a server. This stored procedure is executed at the computer where the Distributor is being looked for, on any database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_get_distributor   
```  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**installed**|**int**|**0** = No; **1** = Yes|  
|**distribution server**|**sysname**|Name of the Distributor server|  
|**distribution db installed**|**int**|**0** = No; **1** = Yes|  
|**is distribution publisher**|**int**|**0** = No; **1** = Yes|  
|**has remote distribution publisher**|**int**|**0** = No; **1** = Yes|  
  
## Remarks  
 **sp_get_distributor** is used primarily by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] in snapshot, transactional, and merge replication.  
  
## Permissions  
 Any user can execute **sp_get_distributor**. A non-NULL result set is returned when this stored procedure is executed by members of the **db_owner** or **replmonitor** fixed database roles on the distribution database, or members of the **db_owner** fixed database role on at least one published database. A non-NULL result set is also returned when this stored procedure is executed by users in the publication access list (PAL) of at least one published database, or in the PAL of the distribution database for a non-SQL Server Publisher, can also execute **sp_get_distributor**.  
  
## See Also  
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [Distributor and Publisher Information Script](../../relational-databases/replication/administration/distributor-and-publisher-information-script.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
