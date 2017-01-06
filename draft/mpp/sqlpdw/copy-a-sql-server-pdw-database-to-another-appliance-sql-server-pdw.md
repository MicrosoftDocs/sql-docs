---
title: "Copy a SQL Server PDW Database to Another Appliance (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 574c0e7b-b94f-4dc9-8e6f-6a4317cf6ec0
caps.latest.revision: 8
author: BarbKess
---
# Copy a SQL Server PDW Database to Another Appliance (SQL Server PDW)
Use this process to copy a SQL Server PDW database from a source appliance to a destination appliance.  
  
## Before You Begin  
  
### Prerequisites  
Verify that the destination appliance has the same number or more Compute nodes than the source appliance.  
  
## <a name="HowToCopyDatabase"></a>Perform the Copy  
  
#### To copy a SQL Server PDW database to another appliance  
  
1.  Use the [BACKUP DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/backup-database-sql-server-pdw.md) statement to perform a backup of the database you want to copy. The backup must contain a full backup. A differential backup in addition to the full backup is optional.  
  
2.  Locate the full and differential backup files. They are each stored in separate directories on the Backup server under G:\Backups. The name of the directories is determined by the parameters you enter in the Backup statement.  
  
3.  Copy the backup directory or directories from G:\Backups on the Backup server of the source appliance to G:\Backups on the Backup server of the destination appliance.  
  
    -   To perform the copy, use the Windows file system to copy the data across your corporate network. Additionally, you can install your own software on the Backup server to assist with copying data across the corporate network.  
  
    -   You will get an error if the destination directories already exist.  
  
    -   You can change the name of each backup directory when you copy it to the destination appliance. For example, you could copy G:\Backups\MyFullBackup on the source appliance to G:\Backups\MyFullBackupToRestore on the destination appliance.  
  
4.  Use the [RESTORE DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/restore-database-sql-server-pdw.md) statement to restore the database to the destination appliance.  
  
    -   You can specify the name for the restored database when you run the RESTORE DATABASE statement.  
  
    -   The database to restore cannot already exist on the destination appliance.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
