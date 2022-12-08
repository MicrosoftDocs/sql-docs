---
description: "sp_addmergealternatepublisher (Transact-SQL)"
title: "sp_addmergealternatepublisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_addmergealternatepublisher_TSQL"
  - "sp_addmergealternatepublisher"
helpviewer_keywords: 
  - "sp_addmergealternatepublisher"
ms.assetid: de46e0b1-d946-4021-bff6-2d8e3187656d
author: markingmyname
ms.author: maghan
---
# sp_addmergealternatepublisher (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Adds the ability for a Subscriber to use an alternate synchronization partner. The publication properties must specify that Subscribers can synchronize with other Publishers. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addmergealternatepublisher [ @publisher= ] 'publisher'  
          , [ @publisher_db= ] 'publisher_db'  
          , [ @publication= ] 'publication'  
          , [ @alternate_publisher= ] 'alternate_synchronization_partner'  
          , [ @alternate_publisher_db= ] 'alternate_publisher_db'  
          , [ @alternate_publication= ] 'alternate_synchronization_partner'  
     , [ @alternate_distributor= ] 'alternate_distributor'   
    [ , [ @friendly_name= ] 'friendly_name' ]   
    [ , [ @reserved= ] 'reserved' ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publication database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @alternate_publisher = ] 'alternate_synchronization_partner'`
 Is the name of the alternate Publisher. *alternate_synchronization_partner* is **sysname**, with no default.  
  
`[ @alternate_publisher_db = ] 'alternate_publisher_db'`
 Is the name of the publication database on the alternate publisher. *alternate_publisher_db* is **sysname**, with no default.  
  
`[ @alternate_publication = ] 'alternate_synchronization_partner'`
 Is the name of the publication on the alternate synchronization partner. *alternate_synchronization_partner* is **sysname**, with no default.  
  
`[ @alternate_distributor = ] 'alternate_distributor'`
 Is the name of the Distributor for the alternate synchronization partner. *alternate_distributor* is **sysname**, with no default.  
  
`[ @friendly_name = ] 'friendly_name'`
 Is a display name by which the association of Publisher, publication, and Distributor that makes up an alternate synchronization partner can be identified. *friendly_name* is **nvarchar(255)**, with a default of NULL.  
  
`[ @reserved = ] 'reserved'`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addmergealternatepublisher** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addmergealternatepublisher**.  
  
## See Also  
 [sp_dropmergealternatepublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergealternatepublisher-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
