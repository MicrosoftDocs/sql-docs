---
title: "Database Files and Filegroups | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "databases [SQL Server], files"
  - "filegroups [SQL Server]"
  - "transaction logs [SQL Server], about"
  - "transaction logs [SQL Server], files"
  - ".mdf files"
  - "data files [SQL Server]"
  - "default filegroups"
  - "files [SQL Server], about files and filegroups"
  - "secondary files [SQL Server]"
  - "log files [SQL Server]"
  - ".ndf files"
  - "files [SQL Server]"
  - ".ldf files"
  - "database files [SQL Server]"
  - "databases [SQL Server], filegroups"
  - "filegroups [SQL Server], types"
  - "primary filegroups [SQL Server]"
  - "user-defined filegroups [SQL Server]"
  - "filegroups [SQL Server], about filegroups"
  - "primary files [SQL Server]"
  - "file types [SQL Server]"
ms.assetid: 9ca11918-480d-4838-9198-cec221ef6ad0
author: stevestein
ms.author: sstein
manager: craigg
---
# Database Files and Filegroups
  At a minimum, every [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database has two operating system files: a data file and a log file. Data files contain data and objects such as tables, indexes, stored procedures, and views. Log files contain the information that is required to recover all transactions in the database. Data files can be grouped together in filegroups for allocation and administration purposes.  
  
## Database Files  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases have three types of files, as shown in the following table.  
  
|File|Description|  
|----------|-----------------|  
|Primary|The primary data file contains the startup information for the database and points to the other files in the database. User data and objects can be stored in this file or in secondary data files. Every database has one primary data file. The recommended file name extension for primary data files is .mdf.|  
|Secondary|Secondary data files are optional, are user-defined, and store user data. Secondary files can be used to spread data across multiple disks by putting each file on a different disk drive. Additionally, if a database exceeds the maximum size for a single Windows file, you can use secondary data files so the database can continue to grow.<br /><br /> The recommended file name extension for secondary data files is .ndf.|  
|Transaction Log|The transaction log files hold the log information that is used to recover the database. There must be at least one log file for each database. The recommended file name extension for transaction logs is .ldf.|  
  
 For example, a simple database named **Sales** can be created that includes one primary file that contains all data and objects and a log file that contains the transaction log information. Alternatively, a more complex database named **Orders** can be created that includes one primary file and five secondary files. The data and objects within the database spread across all six files, and the four log files contain the transaction log information.  
  
 By default, the data and transaction logs are put on the same drive and path. This is done to handle single-disk systems. However, this may not be optimal for production environments. We recommend that you put data and log files on separate disks.  
  
## Filegroups  
 Every database has a primary filegroup. This filegroup contains the primary data file and any secondary files that are not put into other filegroups. User-defined filegroups can be created to group data files together for administrative, data allocation, and placement purposes.  
  
 For example, three files, Data1.ndf, Data2.ndf, and Data3.ndf, can be created on three disk drives, respectively, and assigned to the filegroup **fgroup1**. A table can then be created specifically on the filegroup **fgroup1**. Queries for data from the table will be spread across the three disks; this will improve performance. The same performance improvement can be accomplished by using a single file created on a RAID (redundant array of independent disks) stripe set. However, files and filegroups let you easily add new files to new disks.  
  
 All data files are stored in the filegroups listed in the following table.  
  
|Filegroup|Description|  
|---------------|-----------------|  
|Primary|The filegroup that contains the primary file. All system tables are allocated to the primary filegroup.|  
|User-defined|Any filegroup that is specifically created by the user when the user first creates or later modifies the database.|  
  
### Default Filegroup  
 When objects are created in the database without specifying which filegroup they belong to, they are assigned to the default filegroup. At any time, exactly one filegroup is designated as the default filegroup. The files in the default filegroup must be large enough to hold any new objects not allocated to other filegroups.  
  
 The PRIMARY filegroup is the default filegroup unless it is changed by using the ALTER DATABASE statement. Allocation for the system objects and tables remains within the PRIMARY filegroup, not the new default filegroup.  
  
## Related Content  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql)  
  
 [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-file-and-filegroup-options)  
  
 [Database Detach and Attach &#40;SQL Server&#41;](database-detach-and-attach-sql-server.md)  
  
  
