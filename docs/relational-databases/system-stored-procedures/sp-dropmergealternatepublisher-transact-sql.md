---
description: "sp_dropmergealternatepublisher (Transact-SQL)"
title: "sp_dropmergealternatepublisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_dropmergealternatepublisher"
  - "sp_dropmergealternatepublisher_TSQL"
helpviewer_keywords: 
  - "sp_dropmergealternatepublisher"
ms.assetid: a7dee4e2-2a60-41da-9d1d-6f991d7e2c5e
author: markingmyname
ms.author: maghan
---
# sp_dropmergealternatepublisher (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes an alternate Publisher from a merge publication. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropmergealaternatepublisher [ @publisher = ] 'publisher'    , [ @publisher_db = ] 'publisher_db'    , [ @publication = ] 'publication'    , [ @alternate_publisher = ] 'alternate_publisher'    , [ @alternate_publisher_db = ] 'alternate_publisher_db'    , [ @alternate_publication = ] 'alternate_publication'  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the current Publisher. *publisher*is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the current publication database. *publisher_db*is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the current publication. *publication* is **sysname**, with no default.  
  
`[ @alternate_publisher = ] 'alternate_publisher'`
 Is the name of the alternate Publisher to drop as the alternate synchronization partner. *alternate_publisher*is **sysname**, with no default.  
  
`[ @alternate_publisher_db = ] 'alternate_publisher_db'`
 Is the name of the publication database to drop as the alternate synchronization partner publication database. *alternate_publisher_db*is **sysname**, with no default.  
  
`[ @alternate_publication = ] 'alternate_publication'`
 Is the name of the publication to drop as the alternate synchronization partner publication. *alternate_publication*is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropmergealternatepublisher** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_dropmergelternatepublisher**.  
  
## See Also  
 [sp_addmergealternatepublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergealternatepublisher-transact-sql.md)  
  
  
