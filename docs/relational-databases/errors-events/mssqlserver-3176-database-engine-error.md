---
title: "MSSQLSERVER_3176_deleted | Microsoft Docs"
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
  - "3176 (Database Engine error)"
ms.assetid: 4be24c64-2d52-4cb4-b4d7-36efbe4555b6
caps.latest.revision: 12
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_3176_deleted
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|3176|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LDDB_FILE_CLAIM|  
|Message Text|File '%ls' is claimed by '%ls'(%d) and '%ls'(%d). The WITH MOVE clause can be used to relocate one or more files.|  
  
## Explanation  
Attempting to use a file for more than one purpose.  
  
### Possible Causes  
Another database is already using the file name.  
  
## User Action  
Restore the database files to a different location. In a RESTORE statement, use a WITH MOVE clause to move each file. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], change the file locations in the **Restore the database files as** grid of the **Restore Database Options** dialog box.  
  
## See Also  
[Restore a Database to a New Location &#40;SQL Server&#41;](../Topic/Restore%20a%20Database%20to%20a%20New%20Location%20(SQL%20Server).md)  
[Restore Files to a New Location &#40;SQL Server&#41;](../Topic/Restore%20Files%20to%20a%20New%20Location%20(SQL%20Server).md)  
[RESTORE &#40;Transact-SQL&#41;](../Topic/RESTORE%20(Transact-SQL).md)  
  
