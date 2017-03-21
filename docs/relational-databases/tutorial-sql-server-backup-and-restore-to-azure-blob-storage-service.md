---
title: "Tutorial: SQL Server Backup and Restore to Azure Blob Storage Service | Microsoft Docs"
ms.custom: ""
ms.date: "02/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-query-tuning"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: 9e1d94ce-2c93-45d1-ae2a-2a7d1fa094c4
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Tutorial: SQL Server Backup and Restore to Azure Blob Storage Service
This tutorial helps you understand how to write backups to and restore from the Azure Blob storage service.  
  
## What you will learn  
This tutorial shows you how to create a Storage account, a blob container, creating credentials to access the storage account, writing a backup to the blob service, and performing a simple restore. This tutorial is divided into four lessons:  
  
[Lesson 1: Create Azure Storage Objects](http://msdn.microsoft.com/library/74edd1fd-ab00-46f7-9e29-7ba3f1a446c5)  
In this lesson, you create an Azure storage account and a blob container.  
  
[Lesson 2: Create a SQL Server Credential](http://msdn.microsoft.com/library/64f8805c-1ddc-4c96-a47c-22917d12e1ab)  
In this lesson, you create a Credential to store security information used to access the Azure storage account.  
  
[Lesson 3: Write a Full Database Backup to the Azure Blob Storage Service](http://msdn.microsoft.com/library/454c8296-64e9-46ed-b141-5ebfbc8a4fe2)  
In this lesson, you issue a T-SQL statement to write a backup of the AdventureWorks2012 database to the Azure Blob storage service.  
  
[Lesson 4: Perform a Restore From a Full Database Backup](http://msdn.microsoft.com/library/580f76e6-9802-4abc-9043-db6de592c733)  
In this lesson, you issue a T-SQL statement to restore from the database backup you created in the previous lesson.  
  
### Requirements  
To complete this tutorial, you must be familiar with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] backup and restore concepts and T-SQL syntax. To use this tutorial, your system must meet the following requirements:  
  
-   An instance of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], and AdventureWorks2012 database installed.  
  
    The SQL Server instance can be on-premises or in an Azure Virtual Machine.  
  
    You can use a user database in place of AdventureWorks2012, and modify the tsql syntax accordingly.  
  
-   The user account used to issue BACKUP or RESTORE commands should be in the **db_backup operator** database role with **Alter any credential** permissions.  
  
### Additional reading  
Following are some recommended reading to understand the concepts, best practices when using Azure Blob storage service for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] backups.  
  
1.  [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)  
  
2.  [SQL Server Backup to URL Best Practices and Troubleshooting](../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md)  
  
## See also  
[SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)

