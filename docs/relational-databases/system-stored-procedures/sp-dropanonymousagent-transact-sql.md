---
title: "sp_dropanonymousagent (Transact-SQL)"
description: "sp_dropanonymousagent (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropanonymousagent"
  - "sp_dropanonymousagent_TSQL"
helpviewer_keywords:
  - "sp_dropanonymousagent"
dev_langs:
  - "TSQL"
---
# sp_dropanonymousagent (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops an anonymous agent for replication monitoring at the distributor from the Publisher. This stored procedure is executed at the Publisher on any database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_dropanonymousagent [ @subid= ] sub_id    , [ @type= ] type  
```  
  
## Arguments  
`[ @subid = ] sub_id`
 Is the global identifier for an anonymous subscription. *sub_id* is **uniqueidentifier**, with no default. This identifier can be retrieved at the Subscriber using **sp_helppullsubscription**. The value in the **subid** field of the returned result set is this global identifier.  
  
`[ @type = ] type`
 Is the type of subscription. *type* is **int**, with no default. Valid values are **1** or **2**. Specify **1**, if snapshot replication or transactional replication using the Distribution Agent. Specify **2**, if merge replication using the Merge Agent.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropanonymousagent** is used in all types of replication.  
  
 This stored procedure is used to drop anonymous subscription agents only and cannot be used to drop well-known subscriptions.  
  
## Permissions  
 Only members of the **db_owner** fixed database role in the distribution database can execute **sp_dropanonymousagent**.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
