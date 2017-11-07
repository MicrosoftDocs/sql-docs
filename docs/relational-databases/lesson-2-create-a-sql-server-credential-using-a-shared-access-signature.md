---
title: "Lesson 2: Create a SQL Server credential using a shared access signature | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "02/25/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-backup-restore"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 29e57ebd-828f-4dff-b473-c10ab0b1c597
caps.latest.revision: 17
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Lesson 2: Create a SQL Server credential using a shared access signature
In this lesson, you will create a credential to store the security information that will be used by SQL Server to write to and read from the Azure container that you created in [Lesson 1: Create a stored access policy and a shared access signature  on an Azure container](../relational-databases/lesson-1-create-stored-access-policy-and-shared-access-signature.md).  
  
A SQL Server credential is an object that is used to store authentication information required to connect to a resource outside of SQL Server. The credential stores the URI path of the storage container and the shared access signature for this container.  
  
> [!NOTE]  
> If you wish to backup a SQL Server 2012 SP1 CU2 or later database or a SQL Server 2014 database to this Azure container, you can use the [deprecated syntax](https://technet.microsoft.com/en-US/library/dn435916(v=sql.120).aspx) documented here to create a SQL Server credential based on your storage account key.  
  
## Create SQL Server credential  
To create a SQL Server credential, follow these steps:  
  
1.  Connect to SQL Server Management Studio.  
  
2.  Open a new query windows and connect to the SQL Server 2016 instance of the database engine in your on-premises environment.  
  
3.  In the new query window, paste the CREATE CREDENTIAL statement with the shared access signature from Lesson 1 and execute that script.  
  
    The script will look like the following code.  
  
    ```Transact-SQL  
  
    USE master  
    CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>] – this name must match the container path, start with https and must not contain a forward slash.  
       WITH IDENTITY='SHARED ACCESS SIGNATURE' -- this is a mandatory string and do not change it.   
       , SECRET = 'sharedaccesssignature' –- this is the shared access signature key that you obtained in Lesson 1.   
    GO  
  
    ```  
  
4.  To see all available credentials, you can run the following statement in a query window connected to your instance:  
  
    ```Transact-SQL  
    SELECT * from sys.credentials  
    ```  
  
5.  Open a new query windows and connect to the SQL Server 2016 instance of the database engine in your Azure virtual machine.  
  
6.  In the new query window, paste the CREATE CREDENTIAL statement with the shared access signature from Lesson 1 and execute that script.  
  
7.  Repeat steps 5 and 6 for any additional SQL Server 2016 instances that you wish to have access to the Azure container.  
  
**Next Lesson:**  
  
[Lesson 3: Database backup to URL](../relational-databases/lesson-3-database-backup-to-url.md)  
  
## See Also  
[Credentials &#40;Database Engine&#41;](../relational-databases/security/authentication-access/credentials-database-engine.md)  
[CREATE CREDENTIAL &#40;Transact-SQL&#41;](../t-sql/statements/create-credential-transact-sql.md)  
[sys.credentials &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)  
  

