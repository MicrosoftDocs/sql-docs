---
title: "sp_addmergealternatepublisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addmergealternatepublisher_TSQL"
  - "sp_addmergealternatepublisher"
helpviewer_keywords: 
  - "sp_addmergealternatepublisher"
ms.assetid: de46e0b1-d946-4021-bff6-2d8e3187656d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addmergealternatepublisher (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
 [ **@publisher=**] **'**_publisher_**'**  
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
 [ **@publisher_db=**] **'**_publisher_db_**'**  
 Is the name of the publication database. *publisher_db* is **sysname**, with no default.  
  
 [ **@publication=**] **'**_publication_**'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@alternate_publisher=**] **'**_alternate_synchronization_partner_**'**  
 Is the name of the alternate Publisher. *alternate_synchronization_partner* is **sysname**, with no default.  
  
 [ **@alternate_publisher_db=**] **'**_alternate_publisher_db_**'**  
 Is the name of the publication database on the alternate publisher. *alternate_publisher_db* is **sysname**, with no default.  
  
 [ **@alternate_publication=**] **'**_alternate_synchronization_partner_**'**  
 Is the name of the publication on the alternate synchronization partner. *alternate_synchronization_partner* is **sysname**, with no default.  
  
 [ **@alternate_distributor=**] **'**_alternate_distributor_**'**  
 Is the name of the Distributor for the alternate synchronization partner. *alternate_distributor* is **sysname**, with no default.  
  
 [ **@friendly_name=**] **'**_friendly_name_**'**  
 Is a display name by which the association of Publisher, publication, and Distributor that makes up an alternate synchronization partner can be identified. *friendly_name* is **nvarchar(255)**, with a default of NULL.  
  
 [ **@reserved=**] **'**_reserved_**'**  
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
  
  
