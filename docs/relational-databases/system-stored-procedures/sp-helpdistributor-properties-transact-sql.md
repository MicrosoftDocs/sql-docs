---
title: "sp_helpdistributor_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpdistributor_properties_TSQL"
  - "sp_helpdistributor_properties"
helpviewer_keywords: 
  - "sp_helpdistributor_properties"
ms.assetid: ee267724-3244-49eb-84c9-f38dbefdd639
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpdistributor_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns Distributor properties. This stored procedure is executed at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpdistributor_properties   
```  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**heartbeat_interval**|**int**|The maximum number of minutes that an agent can go without logging a progress message.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpdistributor_properties** is used with all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, members of the **db_owner** or **replmonitor** fixed database role on the distribution database, and users in the publication access list (PAL) for a publication that uses this Distributor can execute **sp_helpdistributor_properties**.  
  
## See Also  
 [sp_changedistributor_property &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedistributor-property-transact-sql.md)  
  
  
