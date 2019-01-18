---
title: "sp_adjustpublisheridentityrange (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_adjustpublisheridentityrange_TSQL"
  - "sp_adjustpublisheridentityrange"
helpviewer_keywords: 
  - "sp_adjustpublisheridentityrange"
ms.assetid: 64f111fd-fb7d-4459-93f7-65f0f8dd7efe
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_adjustpublisheridentityrange (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adjusts the identity range on a publication and reallocates new ranges based on the threshold value on the publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_adjustpublisheridentityrange [ [ @publication = ] 'publication' ]  
    [ , [ @table_name = ] 'table_name' ]  
    [ , [ @table_owner= ] 'table_owner' ]  
```  
  
## Arguments  
 [ **@publication=**] **'***publication***'**  
 Is the name of the publication in which new identity ranges are reallocated. *publication* is **sysname**, with a default of NULL.  
  
 [ **@table_name=**] **'***table_name***'**  
 Is the name of the table in which new identity ranges are reallocated. *table_name* is **sysname**, with a default of NULL.  
  
 [ **@table_owner=**] **'***table_owner***'**  
 Is the owner of the table at the Publisher. *table_owner* is **sysname**, with a default of NULL. If *table_owner* is not specified, the name of the current user is used.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_adjustpublisheridentityrange** is used in all types of replication.  
  
 For a publication which has the auto identity range enabled, the Distribution Agent or Merge Agent is responsible for automatically adjusting the identity range in a publication based on its threshold value. However, if for some reason the Distribution Agent or Merge Agent has not been run for a period of time, and identity range resource have been consumed heavily to the point of threshold, users can call **sp_adjustpublisheridentityrange** to allocate a new range of values for a Publisher.  
  
 When executing **sp_adjustpublisheridentityrange**, either *publication* or *table_name* must be specified. If both or neither are specified an error is returned.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_adjustpublisheridentityrange**.  
  
## See Also  
 [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
