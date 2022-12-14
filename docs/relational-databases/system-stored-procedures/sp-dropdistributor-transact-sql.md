---
description: "sp_dropdistributor (Transact-SQL)"
title: "sp_dropdistributor (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_dropdistributor"
  - "sp_dropdistributor_TSQL"
helpviewer_keywords: 
  - "sp_dropdistributor"
ms.assetid: 0644032f-5ff0-4718-8dde-321bc9967a03
author: markingmyname
ms.author: maghan
---
# sp_dropdistributor (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Uninstalls the Distributor. This stored procedure is executed at the Distributor on any database except the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropdistributor [ [ @no_checks= ] no_checks ]   
    [ , [ @ignore_distributor= ] ignore_distributor ]  
```  
  
## Arguments  
`[ @no_checks = ] no_checks`
 Indicates whether to check for dependent objects before dropping the Distributor. *no_checks* is **bit**, with a default of 0.  
  
 If **0**, **sp_dropdistributor** checks to make sure that all publishing and distribution objects in addition to the Distributor have been dropped.  
  
 If **1**, **sp_dropdistributor** drops all the publishing and distribution objects prior to uninstalling the distributor.  
  
`[ @ignore_distributor = ] ignore_distributor`
 Indicates whether this stored procedure is executed without connecting to the Distributor. *ignore_distributor* is **bit**, with a default of **0**.  
  
 If **0**, **sp_dropdistributor** connects to the Distributor and removes all replication objects. If **sp_dropdistributor** is unable to connect to the Distributor, the stored procedure fails.  
  
 If **1**, no connection is made to the Distributor and the replication objects are not removed. This is used if the Distributor is being uninstalled or is permanently offline. The objects for this Publisher at the Distributor are not removed until the Distributor is reinstalled at some future time.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropdistributor** is used in all types of replication.  
  
 If other Publisher or distribution objects exist on the server, **sp_dropdistributor** fails unless **\@no_checks** is set to **1**.  
  
 This stored procedure must be executed after dropping the distribution database by executing **sp_dropdistributiondb**.  
  
## Example  
 [!code-sql[HowTo#sp_DropDistPub](../../relational-databases/replication/codesnippet/tsql/sp-dropdistributor-trans_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_dropdistributor**.  
  
## See Also  
 [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md)   
 [sp_adddistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributor-transact-sql.md)   
 [sp_changedistributor_property &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedistributor-property-transact-sql.md)   
 [sp_helpdistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributor-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
