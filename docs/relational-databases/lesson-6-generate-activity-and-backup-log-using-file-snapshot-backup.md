---
title: "Lesson 6: Generate activity and backup log using file-snapshot backup | Microsoft Docs"
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
ms.assetid: 26aa534a-afe7-4a14-b99f-a9184fc699bd
caps.latest.revision: 15
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Lesson 6: Generate activity and backup log using file-snapshot backup
In this lesson, you will generate activity in the AdventureWorks2014 database and periodically create transaction log backups using file-snapshot backups. For more information on using file snapshot backups, see [File-Snapshot Backups for Database Files in Azure](../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md).  
  
To generate activity in the AdventureWorks2014 database and periodically create transaction log backups using file-snapshot backups, follow these steps:  
  
1.  Connect to SQL Server Management Studio.  
  
2.  Open two new query windows and connect each to the SQL Server 2016 instance of the database engine in your Azure virtual machine.  
  
3.  Copy, paste and execute the following Transact-SQL script into one of the query windows. Notice that the Production.Location table has 14 rows before we add new rows in step 4.  
  
    ```  
  
    -- Verify row count at start  
    SELECT COUNT (*) from AdventureWorks2014.Production.Location;  
  
    ```  
  
4.  Copy and paste the following two Transact-SQL scripts into the two separate query windows. Modify the URL appropriately for your storage account name and the container that you specified in Lesson 1 and then execute these scripts simultaneously in separate query windows. These scripts will take about seven minutes to complete.  
  
    ```  
    -- Insert 30,000 new rows into the Production.Location table in the AdventureWorks2014 database in batches of 75  
    DECLARE @count INT=1, @inner INT;  
    WHILE @count < 400  
       BEGIN  
          BEGIN TRAN;  
             SET @inner =1;  
                WHILE @inner <= 75  
                   BEGIN;  
                      INSERT INTO AdventureWorks2014.Production.Location    
                         (Name, CostRate, Availability, ModifiedDate)   
                            VALUES (NEWID(), .5, 5.2, GETDATE());  
                      SET @inner = @inner + 1;  
                   END;  
          COMMIT;  
       WAITFOR DELAY '00:00:01';   
       SET @count = @count + 1;  
       END;  
    SELECT COUNT (*) from AdventureWorks2014.Production.Location;  
  
    ```  
  
    ```  
    --take 7 transaction log backups with FILE_SNAPSHOT, one per minute, and include the row count and the execution time in the backup file name   
    DECLARE @count INT=1, @device NVARCHAR(120), @numrows INT;  
    WHILE @count <= 7  
       BEGIN  
             SET @numrows = (SELECT COUNT (*) FROM AdventureWorks2014.Production.Location);  
             SET @device = 'https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>/tutorial-' + CONVERT (varchar(10),@numrows) + '-' + FORMAT(GETDATE(), 'yyyyMMddHHmmss') + '.bak';  
             BACKUP LOG AdventureWorks2014 TO URL = @device WITH FILE_SNAPSHOT;  
             SELECT * from sys.fn_db_backup_file_snapshots ('AdventureWorks2014');  
          WAITFOR DELAY '00:1:00';   
             SET @count = @count + 1;  
       END;  
    ```  
  
5.  Review the output of the first script and notice that final row count is now 29,939.  
  
    ![Row count of 29,939 is displayed](../relational-databases/media/5e2f4229-1970-49c9-89b3-e96b6f7fde83.JPG "Row count of 29,939 is displayed")  
  
6.  Review the output of the second script and notice that each time the BACKUP LOG statement is executed that   two new file snapshots are created, one file snapshot of the log file and one file snapshot of the data file - for a total of two file snapshots for each database file. After the second script completes, notice that there are now a total of 16 file snapshots, 8 for each database file - one from the BACKUP DATABASE statement and one for each execution of the BACKUP LOG statement.  
  
    ![results pane showing file snapshots of both data and log file when log backup is taken](../relational-databases/media/acd213b8-895e-425c-bd72-2bf10e65a5ba.JPG "results pane showing file snapshots of both data and log file when log backup is taken")  
  
    ![four file snapshots are displayed](../relational-databases/media/e7eff77d-85b9-4e52-abd8-e49952c8118a.JPG "four file snapshots are displayed")  
  
    ![results pane showing a total of 16 file snapshots](../relational-databases/media/c3ddff17-a83c-4bf0-a670-a38834f9c922.JPG "results pane showing a total of 16 file snapshots")  
  
7.  In Object Explorer, connect to Azure storage.  
  
8.  Expand Containers,  expand the container that your created in Lesson 1 and verify that 7 new backup files appear, along with the blobs from the previous lessons (refresh the node as needed).  
  
    ![Azure container showing 7 log backup blobs](../relational-databases/media/cfa5a326-87a2-4202-9a04-38bf577d2d0b.JPG "Azure container showing 7 log backup blobs")  
  
**Next Lesson:**  
  
[Lesson 7: Restore a database to a point in time](../relational-databases/lesson-7-restore-a-database-to-a-point-in-time.md)  
  
  
  
