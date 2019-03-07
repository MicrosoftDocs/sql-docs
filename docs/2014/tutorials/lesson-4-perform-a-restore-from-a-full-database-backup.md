---
title: "Lesson 4: Perform a Restore From a Full Database Backup | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
ms.assetid: 580f76e6-9802-4abc-9043-db6de592c733
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Lesson 4: Perform a Restore From a Full Database Backup
  This lesson demonstrates the use of a tsql statement to perform a restore from a full database backup created in the previous lesson.  
  
## Perform a Restore of a Database Backup  
 To restore a full database backup, use the following steps:  
  
1.  Connect to [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
2.  In the **Object Explorer**, connect to the instance of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
3.  On the Standard menu bar, click **New Query**.  
  
4.  Copy and paste the following example into the query window, modify as needed.  
  
    ```  
    RESTORE DATABASE AdventureWorks2012   
    FROM URL = 'https://mystorageaccount.blob.core.windows.net/privatecontainertest/AdventureWorks2012.bak'   
    WITH CREDENTIAL = 'mycredential';  
    , STATS = 5 - use this to see monitor the progress  
    GO  
  
    ```  
  
5.  Verify the T-SQL statement and click **Execute**  
  
### Return to Tutorials Portal  
 [Tutorial: SQL Server Backup and Restore to Windows Azure Blob Storage Service](../relational-databases/tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md).  
  
  
