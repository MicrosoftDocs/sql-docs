---
title: "Lesson 3: Create a SQL Server Credential | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 29e57ebd-828f-4dff-b473-c10ab0b1c597
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Lesson 3: Create a SQL Server Credential
  In this lesson, you will create a credential to store security information used to access the Windows Azure storage account.  
  
 A SQL Server credential is an object that is used to store authentication information required to connect to a resource outside of SQL Server. The credential stores the URI path of the storage container and the shared access signature key values. For each storage container used by a data or log file, you must create a SQL Server Credential whose name matches the container path.  
  
 For general information about credentials, see [Credentials &#40;Database Engine&#41;](security/authentication-access/credentials-database-engine.md).  
  
> [!IMPORTANT]  
>  The requirements for creating a SQL Server credential described below are specific to the [SQL Server Data Files in Windows Azure](databases/sql-server-data-files-in-microsoft-azure.md) feature. For information on creating credentials for backup processes in Azure storage, see [Lesson 2: Create a SQL Server Credential](../tutorials/lesson-2-create-a-sql-server-credential.md).  
  
 To create a SQL Server Credential, follow these steps:  
  
1.  Connect to SQL Server Management Studio.  
  
2.  In Object Explorer, connect to the instance of Database Engine installed.  
  
3.  On the Standard tool bar, click New Query.  
  
4.  Copy and paste the following example into the query window, modify as needed. The following statement will create a SQL Server Credential to store your storage container's Shared Access Certificate.  
  
    ```sql  
  
    USE master  
    CREATE CREDENTIAL credentialname - this name should match the container path and it must start with https.   
       WITH IDENTITY='SHARED ACCESS SIGNATURE', -- this is a mandatory string and do not change it.   
       SECRET = 'sharedaccesssignature' -- this is the shared access signature key that you obtained in Lesson 2.   
    GO  
  
    ```  
  
     For detailed information, see [CREATE CREDENTIAL &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-credential-transact-sql) in SQL Server Books Online.  
  
5.  To see all available credentials, you can run the following statement in the query window:  
  
    ```sql  
    SELECT * from sys.credentials  
    ```  
  
     For more information on sys.credentials, see [sys.credentials &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-credentials-transact-sql) in SQL Server Books Online.  
  
 **Next Lesson:**  
  
 [Lesson 4: Create a database in Windows Azure Storage](lesson-3-database-backup-to-url.md)  
  
  
