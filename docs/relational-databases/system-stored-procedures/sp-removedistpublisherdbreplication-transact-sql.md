---
description: "sp_removedistpublisherdbreplication (Transact-SQL)"
title: "sp_removedistpublisherdbreplication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_removedistpublisherdbreplication_TSQL"
  - "sp_removedistpublisherdbreplication"
helpviewer_keywords: 
  - "sp_removedistpublisherdbreplication"
ms.assetid: 9bfe002a-25b5-4226-bcfb-feb2060d6b4a
author: markingmyname
ms.author: maghan
---
# sp_removedistpublisherdbreplication (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Removes publishing metadata belonging to a specific publication at the Distributor. This stored procedure is executed at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_removedistpublisherdbreplication [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher server. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publication database. *publisher_db* is **sysname** with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_removedistpublisherdbreplication** is used by transactional and snapshot replication.  
  
 **sp_removedistpublisherdbreplication** is used when a published database must be recreated without also dropping the distribution database. The following metadata is removed:  
  
-   All publication metadata.  
  
-   Metadata for all articles belong to the publication.  
  
-   Metadata for all subscriptions to the publication.  
  
-   Metadata for all replication agent jobs that belong to the publication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Distributor or members of the **db_owner** fixed database role in the distribution database can execute **sp_removedistpublisherdbreplication**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
