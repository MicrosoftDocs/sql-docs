---
title: "sp_restoremergeidentityrange (Transact-SQL)"
description: "sp_restoremergeidentityrange (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_restoremergeidentityrange_TSQL"
  - "sp_restoremergeidentityrange"
helpviewer_keywords:
  - "sp_restoremergeidentityrange"
dev_langs:
  - "TSQL"
---
# sp_restoremergeidentityrange (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This stored procedure is used to update identity range assignments. It ensures that automatic identity range management functions properly after a Publisher has been restored from a backup. This stored procedure is executed at the Publisher on the publication database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_restoremergeidentityrange [ [ @publication = ] 'publication' ]  
    [ , [ @article = ] 'article' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with default value of **all**. When specified, only identity ranges for that publication are restored.  
  
`[ @article = ] 'article'`
 Is the name of the article. *article* is **sysname**, with a default value of **all**. When specified, only identity ranges for that article are restored.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_restoremergeidentityrange** is used with merge replication.  
  
 **sp_restoremergeidentityrange** gets maximum identity range allocation information from the Distributor and updates values in the **max_used** column of [MSmerge_identity_range_allocations &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msmerge-identity-range-allocations-transact-sql.md) for the articles which use automatic identity range management.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_restoremergeidentityrange**.  
  
## See Also  
 [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md)   
 [sp_changemergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md)   
 [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md)  
  
  
