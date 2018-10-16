---
title: "Lesson 3: Write a Full Database Backup to the Windows Azure Blob Storage Service | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 454c8296-64e9-46ed-b141-5ebfbc8a4fe2
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 3: Write a Full Database Backup to the Windows Azure Blob Storage Service
  This lesson demonstrates the use of tsql statement to perform a full database backup to Windows Azure Blob storage service.  
  
## Perform a Full Database Backup to the Windows Azure Blob Storage Service  
 To create a full database backup, use the following steps:  
  
1.  Connect to [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
2.  In the **Object Explorer**, connect to the instance of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
3.  On the Standard menu bar, click **New Query**.  
  
4.  Copy and paste the following example into the query window, modify as needed, and click **Execute**.  
  
    ```  
    BACKUP DATABASE[AdventureWorks2012]   
    TO URL = 'https://mystorageaccount.blob.core.windows.net/privatecontainertest/AdventureWorks2012.bak'   
    /* URL includes the endpoint for the BLOB service, followed by the container name, and the name of the backup file*/   
    WITH CREDENTIAL = 'mycredential';  
    /* name of the credential you created in the previous step */   
    GO  
  
    ```  
  
5.  In the object explorer, connect to Azure Storage. Browse to find the container and the newly created backup files.  
  
## Next Lesson  
 [Lesson 4: Perform a Restore From a Full Database Backup](../../2014/tutorials/lesson-4-perform-a-restore-from-a-full-database-backup.md).  
  
  
