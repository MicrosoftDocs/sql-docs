---
title: "Check Integrity of Database with Suspect Pages | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 3b1ec9fe-f6c5-46f7-aa63-6e671be1572d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Check Integrity of Database with Suspect Pages
  This rule checks for user databases that have the database status set to suspect. When the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] reads a database page that contains an 824 error, the page is considered suspect, its page ID is recorded in the suspect_pages table in msdb, and the database that contains the page is set to suspect.  
  
 Error 824 indicates that a logical consistency error was detected during a read operation. This error frequently indicates data corruption caused by a faulty I/O subsystem component. This is a severe error condition that threatens database integrity and must be corrected immediately.  
  
## Best Practices Recommendations  
  
-   Review the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log for the details of the 824 error for this database.  
  
-   Complete a full database consistency check ([DBCC CHECKDB](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql)).  
  
-   Implement the user actions that are defined in [MSSQLSERVER_824](https://go.microsoft.com/fwlink/?LinkId=81397).  
  
## For More Information  
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
  
