---
description: "MSSQLSERVER_1203"
title: "MSSQLSERVER_1203 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1203 (Database Engine error)"
ms.assetid: 33a35f00-98c8-46c6-b432-544b326b6117
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_1203
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1203|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LK_NOT|  
|Message Text|Process ID %d attempted to unlock a resource it does not own: %.*ls. Retry the transaction, because this error may be caused by a timing condition. If the problem persists, contact the database administrator.|  
  
## Explanation  
This error occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is engaged in some activity other than ordinary post-processing cleanup and it finds that a particular page that it is trying to unlock is already unlocked.  
  
### Possible Causes  
The underlying cause of this error may be related to structural problems within the affected database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] manages the acquisition and release of pages to maintain concurrency control in the multiuser environment. This mechanism is maintained by using various internal lock structures that identify the page and the type of lock present. Locks are acquired for processing of affected pages and released when the processing is finished.  
  
## User Action  
Execute DBCC CHECKDB against the database in which the object belongs. If DBCC CHECKDB reports no errors, try to reestablish the connection and execute the command.  
  
> [!IMPORTANT]  
> If you are executing DBCC CHECKDB with one of the REPAIR clauses does not correct the index problem, or if you are not sure what effect DBCC CHECKDB with a REPAIR clause has on your data, contact your primary support provider.  
  
