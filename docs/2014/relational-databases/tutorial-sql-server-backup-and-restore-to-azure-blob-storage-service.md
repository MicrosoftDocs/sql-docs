---
title: "Tutorial: SQL Server Backup and Restore to Windows Azure Blob Storage Service | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: 9e1d94ce-2c93-45d1-ae2a-2a7d1fa094c4
author: rothja
ms.author: jroth
manager: craigg
---
# Tutorial: SQL Server Backup and Restore to Windows Azure Blob Storage Service
  Welcome to the Getting Started with SQL Server Backup and Restore with Windows Azure Blob Storage Service tutorial. This tutorial helps you understand how to write backups to and restore from the Windows Azure Blob storage service.  
  
## What You Will Learn  
 This tutorial shows you how to create a Windows Storage account, a blob container, creating credentials to access the storage account, writing a backup to the blob service, and performing a simple restore. This tutorial is divided into four lessons:  
  
 [Lesson 1: Create Windows Azure Storage Objects](../tutorials/lesson-1-create-windows-azure-storage-objects.md)  
 In this lesson, you create a Windows Azure storage account and a blob container.  
  
 [Lesson 2: Create a SQL Server Credential](../tutorials/lesson-2-create-a-sql-server-credential.md)  
 In this lesson, you create a Credential to store security information used to access the Windows Azure storage account.  
  
 [Lesson 3: Write a Full Database Backup to the Windows Azure Blob Storage Service](../tutorials/lesson-3-write-a-full-database-backup-to-the-windows-azure-blob-storage-service.md)  
 In this lesson, you issue a T-SQL statement to write a backup of the AdventureWorks2012 database to the Windows Azure Blob storage service.  
  
 [Lesson 4: Perform a Restore From a Full Database Backup](../tutorials/lesson-4-perform-a-restore-from-a-full-database-backup.md)  
 In this lesson, you issue a T-SQL statement to restore from the database backup you created in the previous lesson.  
  
### Requirements  
 To complete this tutorial, you must be familiar with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] backup and restore concepts and T-SQL syntax. To use this tutorial, your system must meet the following requirements:  
  
-   An instance of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], and AdventureWorks2012 database installed.  
  
     The SQL Server instance can be on-premises or in a Windows Azure Virtual Machine.  
  
     You can use a user database in place of AdventureWorks2012, and modify the tsql syntax accordingly.  
  
-   The user account that is used to issue BACKUP or RESTORE commands should be in the **db_backup operator** database role with **Alter any credential** permissions.  
  
### Additional Reading  
 Following are some recommended reading to understand the concepts, best practices when using Windows Azure Blob storage service for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] backups.  
  
1.  [SQL Server Backup and Restore with Windows Azure Blob Storage Service](backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)  
  
2.  [SQL Server Backup to URL Best Practices and Troubleshooting](backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md)  
  
  
