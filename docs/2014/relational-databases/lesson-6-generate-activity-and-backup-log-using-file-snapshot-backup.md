---
title: "Lesson 7: Move your data files to Windows Azure Storage | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 26aa534a-afe7-4a14-b99f-a9184fc699bd
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Lesson 7: Move your data files to Windows Azure Storage
  In this lesson, you will learn how to move your data files to Windows Azure Storage (but not your SQL Server instance). To follow this lesson, you do not need to complete Lesson 4, 5, and 6.  
  
 To move your data files to Windows Azure Storage, you can use the `ALTER DATABASE` statement as it helps to change the location of the data files.  
  
 This lesson assumes that you already completed the following steps:  
  
-   You have a Windows Azure Storage account.  
  
-   You have created a container under your Windows Azure Storage account.  
  
-   You have created a policy on a container with read, write, and list rights. You also generated a SAS key.  
  
-   You have created a SQL Server credential on the source machine.  
  
 Next, use the following steps to move your data files to Windows Azure Storage:  
  
1.  First, create a test database in the source machine and add some data to it.  
  
    ```tsql  
  
    USE master;   
    CREATE DATABASE TestDB1Alter;   
    GO   
    USE TestDB1Alter;   
    GO   
    CREATE TABLE Table1 (Col1 int primary key, Col2 varchar(20));   
    GO   
    INSERT INTO Table1 (Col1, Col2) VALUES (1, 'string1'), (2, 'string2');   
    GO  
  
    ```  
  
2.  Run the following code:  
  
    ```tsql  
  
    -- In the following statement, modify the path specified in FILENAME to   
    -- the new location of the file in Windows Azure Storage container.   
    ALTER DATABASE TestDB1Alter    
        MODIFY FILE ( NAME = TestDB1Alter,    
                    FILENAME = 'https://teststorageaccnt.blob.core.windows.net/testcontaineralter/TestDB1AlterData.mdf');   
    GO  
  
    ```  
  
3.  When you run this, you will see this message: "The file "TestDB1Alter" has been modified in the system catalog. The new path will be used the next time the database is started."  
  
4.  Then, set the database offline.  
  
    ```tsql  
  
    ALTER DATABASE TestDB1Alter SET OFFLINE;   
    GO  
  
    ```  
  
5.  Now, you need to copy the data files to Windows Azure Storage by using one of the following methods: [AzCopy Tool](https://blogs.msdn.com/b/windowsazurestorage/archive/2012/12/03/azcopy-uploading-downloading-files-for-windows-azure-blobs.aspx), [Put Page](https://msdn.microsoft.com/library/azure/ee691975.aspx), [Storage Client Library Reference](https://msdn.microsoft.com/library/azure/dn261237.aspx), or a third-party storage explorer tool.  
  
     **Important:** When using this new enhancement, always make sure that you create a page blob not a block blob.  
  
6.  Then, set the database online.  
  
    ```tsql  
  
    ALTER DATABASE TestDB1Alter SET ONLINE;   
    GO  
  
    ```  
  
 **Next Lesson:**  
  
 [Lesson 8. Restore a database to Windows Azure Storage](lesson-7-restore-a-database-to-a-point-in-time.md)  
  
  
