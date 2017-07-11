---
title: "Lesson 4: Restore database to virtual machine from URL | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-backup-restore"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: ba793c8f-665a-4c46-b68d-f558a37906b2
caps.latest.revision: 23
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Lesson 4: Restore database to virtual machine from URL
In this lesson, you will restore the AdventureWorks2014 database to your SQL Server 2016 instance in your Azure virtual machine the AdventureWorks2014 database.  
  
> [!NOTE]  
> For the purposes of simplicity in this tutorial, we are using the same container for the data and log files that we used for the database backup. In a production environment, you would likely use multiple containers, and frequently multiple data files as well. With SQL Server 2016, you could also consider striping your backup across multiple blobs to increase backup performance when backing up a large database.  
  
To restore the SQL Server 2014 database from Azure blob storage to your SQL Server 2016 instance in your Azure virtual machine, follow these steps:  
  
1.  Connect to SQL Server Management Studio.  
  
2.  Open a new query window and connect to the SQL Server 2016 instance of the database engine in your Azure virtual machine.  
  
3.  Copy and paste the following Transact-SQL script into the query window. Modify the URL appropriately for your storage account name and the container that you specified in Lesson 1 and then execute this script.  
  
    ```  
  
    -- Restore AdventureWorks2014 from URL to SQL Server instance using Azure blob storage for database files  
    RESTORE DATABASE AdventureWorks2014   
       FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>/AdventureWorks2014_onprem.bak'   
       WITH  
          MOVE 'AdventureWorks2014_data' to 'https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>/AdventureWorks2014_Data.mdf'  
         ,MOVE 'AdventureWorks2014_log' to 'https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>/AdventureWorks2014_Log.ldf'  
    --, REPLACE  
  
    ```  
  
4.  Open Object Explorer and connect to your Azure SQL Server 2016 instance.  
  
5.  In Object Explorer, expand the Databases node and verify that the AdventureWorks2014 database has been restored (refresh the node as necessary).  
  
    ![Adventure Works 2014 database restored to SQL Server 2016 in virtual machine](../relational-databases/media/311f69a6-8443-4df5-8f30-3103c2472300.JPG "Adventure Works 2014 database restored to SQL Server 2016 in virtual machine")  
  
6.  In Object Explorer, right-click AdventureWorks2014, and click Properties (click Cancel when done).  
  
7.  Click Files and verify that the path for the two database files are URLs pointing to blobs in your Azure blog container.  
  
    ![database properties showing file path of logical data files as URL](../relational-databases/media/cfeee576-6319-460e-9fa2-f0922e02ee23.JPG "database properties showing file path of logical data files as URL")  
  
8.  In Object Explorer, connect to Azure storage.  
  
9. Expand Containers,  expand the container that your created in Lesson 1 and verify that the AdventureWorks2014_Data.mdf and AdventureWorks2014_Log.ldf  from step 3 above appears in this container, along with the backup file from Lesson 3 (refresh the node as necessary).  
  
    ![Adventure Works 2014 data and log file appear as blobs in Azure container](../relational-databases/media/156c7d73-44be-4754-9653-04cccb6c3066.JPG "Adventure Works 2014 data and log file appear as blobs in Azure container")  
  
**Next Lesson:**  
  
[Lesson 5: Backup database using file-snapshot backup](../relational-databases/lesson-5-backup-database-using-file-snapshot-backup.md)  
  
  
  
