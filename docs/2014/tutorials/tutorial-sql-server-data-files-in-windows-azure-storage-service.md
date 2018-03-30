---
title: "Tutorial: SQL Server Data Files in Windows Azure Storage service | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: e69be67d-da1c-41ae-8c9a-6b12c8c2fb61
caps.latest.revision: 7
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Tutorial: SQL Server Data Files in Windows Azure Storage service
  Welcome to the  SQL Server Data Files in Windows Azure Storage Service tutorial. This tutorial helps you understand how to store SQL Server data files in the Windows Azure Blob storage service directly.  
  
 SQL Server integration support for the Windows Azure Blob storage service is a SQL Server 2014 enhancement. For an overview of the functionality and benefits of using this functionality, see [SQL Server Data Files in Windows Azure](../../2014/database-engine/sql-server-data-files-in-windows-azure.md).  
  
## What you will learn  
 This tutorial shows you how to store SQL Server Data Files in Windows Azure Storage service in multiple lessons. Each lesson is focused on a specific task. First, you will learn how to create a storage account and a container in Windows Azure. Then, you will learn how to create a SQL Server credential to be able to integrate SQL Server with Windows Azure Storage. Then, you will create a database in Windows Azure Storage directly. In addition, the tutorial will demonstrate encryption, migration, and backup and restore scenarios.  
  
 This tutorial is divided into nine lessons:  
  
 **[Lesson 1: Create Windows Azure Storage Account and Container](../../2014/tutorials/lesson-1-create-windows-azure-storage-account-and-container.md)**  
 In this lesson, you create a Windows Azure Storage account and a container.  
  
 **[Lesson 2. Create a policy on container and generate a Shared Access Signature &#40;SAS&#41; key](../../2014/tutorials/lesson-1-create-stored-access-policy-and-shared-access-signature.md)**  
 In this lesson, you create a policy on the Blob container and also generate a shared access signature.  
  
 **[Lesson 3: Create a SQL Server Credential](../../2014/tutorials/lesson-3-create-a-sql-server-credential.md)**  
 In this lesson, you create a Credential to store security information used to access the Windows Azure storage account.  
  
 **[Lesson 4: Create a database in Windows Azure Storage](../../2014/tutorials/lesson-4-create-a-database-in-windows-azure-storage.md)**  
 In this lesson, you create a database in Windows Azure Storage using CREATE Database FILENAME option.  
  
 **[Lesson 5. &#40;Optional&#41; Encrypt your database using TDE](../../2014/tutorials/lesson-5-optional-encrypt-your-database-using-tde.md)**  
 In this lesson, you encrypt your database by using a transparent data encryption (TDE) and a server certificate.  
  
 **[Lesson 6: Migrate a database from a source machine on-premises to a destination machine in Windows Azure](../../2014/tutorials/lesson-5-backup-database-using-file-snapshot-backup.md)**  
 In this lesson, you migrate a database from on-premises to a virtual machine in Windows Azure using CREATE DATABASE FOR ATTACH option.  
  
 **[Lesson 7: Move your data files to Windows Azure Storage](../../2014/tutorials/lesson-7-move-your-data-files-to-windows-azure-storage.md)**  
 In this lesson, you move your data files to Windows Azure Storage using ALTER DATABASE statement.  
  
 **[Lesson 8. Restore a database to Windows Azure Storage](../../2014/tutorials/lesson-8-restore-a-database-to-windows-azure-storage.md)**  
 In this lesson, you restore a database to Windows Azure Storage using RESTORE Database MOVE option.  
  
 **[Lesson 9. Restore a database from Windows Azure Storage](../../2014/tutorials/lesson-9-restore-a-database-from-windows-azure-storage.md)**  
 In this lesson, you restore a database from Windows Azure Storage using RESTORE Database MOVE option.  
  
## See Also  
 [SQL Server Data Files in Windows Azure](../../2014/database-engine/sql-server-data-files-in-windows-azure.md)  
  
  