---
title: "Tutorial: SQL Server Data Files in Azure Storage service | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: e69be67d-da1c-41ae-8c9a-6b12c8c2fb61
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Tutorial: SQL Server Data Files in Azure Storage service
  Welcome to the  SQL Server Data Files in Azure Storage Service tutorial. This tutorial helps you understand how to store SQL Server data files in the Azure Blob storage service directly.  
  
 SQL Server integration support for the Azure Blob storage service is a SQL Server 2014 enhancement. For an overview of the functionality and benefits of using this functionality, see [SQL Server Data Files in Azure](databases/sql-server-data-files-in-microsoft-azure.md).  
  
## What you will learn  
 This tutorial shows you how to store SQL Server Data Files in Azure Storage service in multiple lessons. Each lesson is focused on a specific task. First, you will learn how to create a storage account and a container in Azure. Then, you will learn how to create a SQL Server credential to be able to integrate SQL Server with Azure Storage. Then, you will create a database in Azure Storage directly. In addition, the tutorial will demonstrate encryption, migration, and backup and restore scenarios.  
  
 This tutorial is divided into nine lessons:  
  
 **[Lesson 1: Create Azure Storage Account and Container](../tutorials/lesson-1-create-windows-azure-storage-account-and-container.md)**  
 In this lesson, you create an Azure Storage account and a container.  
  
 **[Lesson 2. Create a policy on container and generate a Shared Access Signature &#40;SAS&#41; key](lesson-1-create-stored-access-policy-and-shared-access-signature.md)**  
 In this lesson, you create a policy on the Blob container and also generate a shared access signature.  
  
 **[Lesson 3: Create a SQL Server Credential](lesson-2-create-a-sql-server-credential-using-a-shared-access-signature.md)**  
 In this lesson, you create a Credential to store security information used to access the Azure storage account.  
  
 **[Lesson 4: Create a database in Azure Storage](../relational-databases/lesson-3-database-backup-to-url.md)**  
 In this lesson, you create a database in Azure Storage using CREATE Database FILENAME option.  
  
 **[Lesson 5. &#40;Optional&#41; Encrypt your database using TDE](../relational-databases/lesson-4-restore-database-to-virtual-machine-from-url.md)**  
 In this lesson, you encrypt your database by using a transparent data encryption (TDE) and a server certificate.  
  
 **[Lesson 6: Migrate a database from a source machine on-premises to a destination machine in Azure](lesson-5-backup-database-using-file-snapshot-backup.md)**  
 In this lesson, you migrate a database from on-premises to a virtual machine in Azure using CREATE DATABASE FOR ATTACH option.  
  
 **[Lesson 7: Move your data files to Azure Storage](../relational-databases/lesson-6-generate-activity-and-backup-log-using-file-snapshot-backup.md)**  
 In this lesson, you move your data files to Azure Storage using ALTER DATABASE statement.  
  
 **[Lesson 8. Restore a database to Azure Storage](../relational-databases/lesson-7-restore-a-database-to-a-point-in-time.md)**  
 In this lesson, you restore a database to Azure Storage using RESTORE Database MOVE option.  
  
 **[Lesson 9. Restore a database from Azure Storage](lesson-8-restore-as-new-database-from-log-backup.md)**  
 In this lesson, you restore a database from Azure Storage using RESTORE Database MOVE option.  
  
## See Also  
 [SQL Server Data Files in Azure](databases/sql-server-data-files-in-microsoft-azure.md)  
  
  
