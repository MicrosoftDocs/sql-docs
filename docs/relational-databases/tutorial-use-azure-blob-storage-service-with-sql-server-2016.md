---
title: "Tutorial: Use Azure Blob storage service with SQL Server 2016 | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "01/07/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: e69be67d-da1c-41ae-8c9a-6b12c8c2fb61
caps.latest.revision: 23
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Tutorial: Use Azure Blob storage service with SQL Server 2016
Welcome to the  Working with SQL Server 2016 in Microsoft Azure Blob Storage service tutorial. This tutorial helps you understand how to use the Microsoft Azure Blob storage service for SQL Server data files and SQL Server backups.  
  
SQL Server integration support for the Microsoft Azure Blob storage service began as a SQL Server 2012 Service Pack 1 CU2 enhancement, and has been enhanced further with SQL Server 2014 and SQL Server 2016. For an overview of the functionality and benefits of using this functionality, see [SQL Server Data Files in Microsoft Azure](../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md). For a live demo, see [Demo of Point in Time Restore](https://channel9.msdn.com/Blogs/Windows-Azure/File-Snapshot-Backups-Demo).  
  
  
**Download**<br /><br />**>>**  To download [!INCLUDE[ssSQL15](../includes/sssql15-md.md)], go to  **[Evaluation Center](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016)**.<br /><br />**>>**  Have an Azure account?  Then go **[Here](https://azure.microsoft.com/en-us/services/virtual-machines/sql-server/)** to spin up a Virtual Machine with [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] already installed.  
  
## What you will learn  
This tutorial shows you how to work with  SQL Server Data Files in Microsoft Azure Blob storage service in multiple lessons. Each lesson is focused on a specific task and the lessons should be completed in sequence. First, you will learn how to create a new container in Blob storage with a stored access policy and a shared access signature. Then, you will learn how to create a SQL Server credential to integrate SQL Server with Azure blob storage. Next, you will back up a database to Blob storage and restore it to an Azure virtual machine. You will then use SQL Server 2016 file-snapshot transaction log backup to restore to a point in time and to a new database. Finally, the tutorial will demonstrate the use of meta data system stored procedures and functions to help you understand and work with file-snapshot backups.  
  
This article assumes the following:  
  
-   You have an on-premises instance of SQL Server 2016 with a copy of AdventureWorks2014 installed.  
  
-   You have an Azure storage account.  
  
-   You have at least one Azure virtual machines with SQL Server 2016 installed and provisioned this virtual machine in accordance with [Provisioning a SQL Server Virtual Machine on Azure](https://azure.microsoft.com/en-us/documentation/articles/virtual-machines-provision-sql-server/). As an option, a second virtual machine can be used for the scenario in [Lesson 8. Restore as new database from log backup](../relational-databases/lesson-8-restore-as-new-database-from-log-backup.md)).  
  
This tutorial is divided into nine lessons, which you must complete in order:  
  
**[Lesson 1: Create a stored access policy and a shared access signature  on an Azure container](../relational-databases/lesson-1-create-stored-access-policy-and-shared-access-signature.md)**  
In this lesson, you create a policy on a new blob container and also generate a shared access signature for use in creating a SQL Server credential.  
  
**[Lesson 2: Create a SQL Server credential using a shared access signature](../relational-databases/lesson-2-create-a-sql-server-credential-using-a-shared-access-signature.md)**  
In this lesson, you create a credential using a SAS key to store security information used to access the Microsoft Azure storage account.  
  
**[Lesson 3: Database backup to URL](../relational-databases/lesson-3-database-backup-to-url.md)**  
In this lesson, you backup an on-premises database to Microsoft Azure Blob storage.  
  
**[Lesson 4: Restore database to virtual machine from URL](../relational-databases/lesson-4-restore-database-to-virtual-machine-from-url.md)**  
In this lesson, you restore a database to an Azure virtual machine from Windows Azure Blob storage.  
  
**[Lesson 5: Backup database using file-snapshot backup](../relational-databases/lesson-5-backup-database-using-file-snapshot-backup.md)**  
In this lesson, you backup a database using file-snapshot backup and view-file snapshot metadata.  
  
**[Lesson 6: Generate activity and backup log using file-snapshot backup](../relational-databases/lesson-6-generate-activity-and-backup-log-using-file-snapshot-backup.md)**  
In this lesson, you generate activity in the database and perform several log backups using file-snapshot backup, and view file-snapshot metadata.  
  
**[Lesson 7: Restore a database to a point in time](../relational-databases/lesson-7-restore-a-database-to-a-point-in-time.md)**  
In this lesson, you restore a database to a point in time using two file-snapshot log backups.  
  
**[Lesson 8. Restore as new database from log backup](../relational-databases/lesson-8-restore-as-new-database-from-log-backup.md)**  
In this lesson, you restore from a file-snapshot log backup to a new database on a different virtual machine.  
  
**[Lesson 9: Manage backup sets and file-snapshot backups](../relational-databases/lesson-9-manage-backup-sets-and-file-snapshot-backups.md)**  
In this lesson, you delete unneeded backup sets and learn how to delete orphaned file snapshot backups (when necessary).  
  
## Did this Article Help You? We’re Listening  
What information are you looking for, and did you find it? We’re listening to your feedback to improve the content. Please submit your comments to [sqlfeedback@microsoft.com](mailto:sqlfeedback@microsoft.com?subject=Your%20feedback%20about%20the%20Tutorial:%20Using%20the%20Microsoft%20Azure%20Blob%20storage%20service%20with%20SQL%20Server%202016%20databases%20page)  
  
## See Also  
[SQL Server Data Files in Microsoft Azure](../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md)  
[File-Snapshot Backups for Database Files in Azure](../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md)  
[SQL Server Backup to URL](../relational-databases/backup-restore/sql-server-backup-to-url.md)  
  
  
  

