---
title: "Database Properties (Filegroups Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.databaseproperties.filegroups.f1"
ms.assetid: 8d06e859-73dd-4019-b6e8-99c5c5297697
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Database Properties (Filegroups Page)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use this page to view the filegroups or add a new filegroup to the selected database. Filegroup types are separated into *row* filegroups, FILESTREAM data, and memory-optimized filegroups.  
  
 Row filegroups contain regular data and log files. FILESTREAM data filegroups contain FILESTREAM data files. These data files store information about how binary large object (BLOB) data is stored on the file system when you are using FILESTREAM storage. The options are the same for both types of filegroups.  
  
 If FILESTREAM is not enabled, the **Filestream** section will not be available. You can enable FILESTREAM storage by using [Server Properties (Advanced Page)](../../database-engine/configure-windows/server-properties-advanced-page.md).  
  
 For information about how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses row filegroups, see [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md). For more information about FILESTREAM data and filegroups, see [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md).  
  
 Memory-optimized file groups are required for a database to contain one or more memory-optimized tables.  
  
## Row and FILESTREAM Data Filegroup Options  
 **Name**  
 Enter the name of the filegroup.  
  
 **Files**  
 Displays the count of files in the filegroup.  
  
 **Read-only**  
 Select to set the filegroup to a read-only status.  
  
 **Default**  
 Select to make this filegroup the default filegroup. You can have one default filegroup for rows and one default filegroup for FILESTREAM data.  
  
 **Add**  
 Adds a new blank row to the grid listing filegroups for the database.  
  
 **Remove**  
 Removes the selected filegroup row from the grid.  
  
## Memory-Optimized Data Filegroup Options  
 **Name**  
 Enter the name of the memory-optimized filegroup.  
  
 **Filestream Files**  
 Displays the number of files (containers) in the memory-optimized data filegroup. You can add containers in the **Files** page.  
  
 **Add**  
 Adds a new blank row to the grid listing filegroups for the database.  
  
 **Remove**  
 Removes the selected filegroup row from the grid.  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
  
