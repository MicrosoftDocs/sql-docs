---
title: "sp_copysnapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_copysnapshot"
  - "sp_copysnapshot_TSQL"
helpviewer_keywords: 
  - "sp_copysnapshot"
ms.assetid: a012a32f-6f26-45bf-8046-b51cd7fec455
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_copysnapshot (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Copies the snapshot folder of the specified publication to the folder listed in the **@destination_folder**. This stored procedure is executed at the Publisher on the publication database. This stored procedure is useful for copying a snapshot to removable media, such as CD-ROM.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_copysnapshot [ @publication = ] 'publication', [ @destination_folder = ] 'destination_folder' ]  
    [ , [ @subscriber = ] 'subscriber' ]  
    [ , [ @subscriber_db = ] 'subscriber_db' ]  
```  
  
## Arguments  
 [ **@publication=**] **'**_publication_**'**  
 Is the name of the publication whose snapshot contents are to be copied. *publication* is **sysname**, with no default.  
  
 [ **@destination_folder=**] **'**_destination_folder_**'**  
 Is the name of the folder where the contents of the publication snapshot are to be copied. *destination_folder*is **nvarchar(255)**, with no default. The *destination_folder* can be an alternate location such as on another server, on a network drive, or on removable media (such as CD-ROMs or removable disks).  
  
 [ **@subscriber=**] **'**_subscriber_**'**  
 Is the name of the Subscriber. *subscriber* is sysname, with a default of NULL.  
  
 [ **@subscriber_db=**] **'**_subscriber_db_**'**  
 Is the name of the subscription database. *subscriber_db* is sysname, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_copysnapshot** is used in all types of replication. Subscribers running [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 and earlier cannot use the alternate snapshot location.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_copysnapshot**.  
  
## See Also  
 [Alternate Snapshot Folder Locations](../../relational-databases/replication/snapshot-options.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
