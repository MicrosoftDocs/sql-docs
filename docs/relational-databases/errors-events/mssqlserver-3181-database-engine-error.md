---
title: "MSSQLSERVER_3181 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "3181 (Database Engine error)"
ms.assetid: e6d2e716-5263-477c-ad0e-7bcbfef4c1f3
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_3181
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|3181|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LDDB_STORAGE_VERIFY|  
|Message Text|Attempting to restore this backup may encounter storage space problems. Subsequent messages will provide details.|  
  
## Explanation  
The RESTORE VERIFYONLY statement checks the available storage space on the disk to which the database is to be restored.  
  
### Possible Causes  
The available disk space may be insufficient to restore the backup being verified.  
  
## User Action  
Restore the backup to a location with sufficient disk space, or provide more space on the disk.  
  
## See Also  
[Restore a Database to a New Location &#40;SQL Server&#41;](../Topic/Restore%20a%20Database%20to%20a%20New%20Location%20(SQL%20Server).md)  
[Restore Files to a New Location &#40;SQL Server&#41;](../Topic/Restore%20Files%20to%20a%20New%20Location%20(SQL%20Server).md)  
[RESTORE &#40;Transact-SQL&#41;](../Topic/RESTORE%20(Transact-SQL).md)  
[RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../Topic/RESTORE%20VERIFYONLY%20(Transact-SQL).md)  
  
