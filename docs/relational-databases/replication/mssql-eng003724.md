---
description: "MSSQL_ENG003724"
title: "MSSQL_ENG003724 | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG003724 error"
ms.assetid: 10cb119d-92df-4124-b85d-cd2f2666c99c
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG003724
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3724|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Cannot %S_MSG the %S_MSG '%.*ls' because it is being used for replication.|  
  
## Explanation  
 When objects in a database are replicated, they are marked as replicated in the system table **sysarticles** (for snapshot and transactional publications) or **sysmergearticles** (for merge publications). If you attempt drop a replicated object, this error is raised.  
  
## User Action  
 Ensure the database object is not replicated before attempting to drop it. For example:  
  
-   If the error occurs in the publication database, drop the article from the publication before dropping the object. For more information, see [Add Articles to and Drop Articles from Existing Publications](../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).  
  
-   If the error occurs in the subscription database, drop the subscription before dropping the object. For more information, see [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md). For subscriptions to transactional publications, it is possible to drop the subscription to an individual article rather than the entire publication. For more information, see [sp_dropsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql.md).  
  
 If this error occurs in a database that is not replicated, execute [sp_removedbreplication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to ensure objects in the database are not marked as replicated.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
