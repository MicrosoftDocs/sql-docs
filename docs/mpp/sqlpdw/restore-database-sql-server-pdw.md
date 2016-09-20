---
title: "RESTORE DATABASE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1de4a58c-3472-4506-8ed5-5528249a2859
caps.latest.revision: 66
author: BarbKess
---
# RESTORE DATABASE (SQL Server PDW)
Restores a SQL Server PDW user database from a database backup to a SQL Server PDW appliance. The database is restored from a backup that was previously created by the SQL Server PDW[BACKUP DATABASE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/backup-database-sql-server-pdw.md) command. Use the backup and restore operations to build a disaster recovery plan, or to move databases from one appliance to another.  
  
> [!NOTE]  
> Restoring master includes restoring appliance login information. To restore master, use the [Restore the Master Database &#40;Analytics Platform System&#41;](../../mpp/management/restore-the-master-database-analytics-platform-system.md) page in the **Configuration Manager** tool. An administrator with access to the Control node can perform this operation.  
  
For more information about SQL Server PDW database backups, see [Backup and Restore &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/backup-and-restore-sql-server-pdw.md).  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
Restore the master database  
-- Use the Configuration Manager tool.  
  
Restore a full user database backup.  
RESTORE DATABASE database_name   
    FROM DISK = '\\UNC_path\full_backup_directory'  
[;]  
  
Restore a full user database backup and then a differential backup.  
RESTORE DATABASE database_name  
    FROM DISK = '\\UNC_path\differential_backup_directory'   
    WITH [ ( ] BASE = '\\UNC_path\full_backup_directory' [ ) ]   
[;]  
  
Restore header information for a full or differential user database backup.  
RESTORE HEADERONLY   
    FROM DISK = '\\UNC_path\backup_directory'  
[;]  
```  
  
## Arguments  
RESTORE DATABASE *database_name*  
Specifies to restore a user database to a database called *database_name*. The restored database can have a different name than the source database that was backed up. *database_name* cannot already exist as a database on the destination appliance. For more details on permitted database names, see [Object Naming Rules &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-naming-rules-sql-server-pdw.md).  
  
Restoring a user database restores a full database backup and then optionally restores a differential backup to the appliance. A restore of a user database includes restoring database users, and database roles.  
  
FROM DISK = '\\\\*UNC_path*\\*backup_directory*'  
The network path and directory from which SQL Server PDW will restore the backup files. For example, FROM DISK = '\\\xxx.xxx.xxx.xxx\backups\2012\Monthly\08.2012.Mybackup'.  
  
*backup_directory*  
Specifies the name of a directory that contains the full or differential backup. For example, you can perform a RESTORE HEADERONLY operation on a full or differential backup.  
  
*full_backup_directory*  
Specifies the name of a directory that contains the full backup.  
  
*differential_backup_directory*  
Specifies the name of the directory that contains the differential backup.  
  
-   The path and backup directory must already exist and must be specified as a fully qualified universal naming convention (UNC) path.  
  
-   The path to the backup directory cannot be a local path and it cannot be a location on any of the SQL Server PDW appliance nodes.  
  
-   The maximum length of the UNC path and backup directory name is 200 characters.  
  
-   The server or host must be specified as an IP address.  
  
RESTORE HEADERONLY  
Specifies to return only the header information for one user database backup. Among other fields, the header includes the text description of the backup, and the backup name. The backup name does not need to be the same as the name of the directory that stores the backup files.  
  
RESTORE HEADERONLY results are patterned after the SQL Server RESTORE HEADERONLY results. The result has over 50 columns, which are not all used by SQL Server PDW. For a description of the columns in the SQL Server RESTORE HEADERONLY results, see [RESTORE HEADERONLY (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms178536(v=sql.110)) on MSDN.  
  
## Permissions  
Requires the **CREATE ANY DATABASE** permission.  
  
Requires a Windows account that has permission to access and read from the backup directory. You must also store the Windows account name and password in SQL Server PDW.  
  
1.  To verify the credentials are already there, use [sys.dm_pdw_network_credentials &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-network-credentials-sql-server-pdw.md)  
  
2.  To add or update the credentials, use [sp_pdw_add_network_credentials &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pdw-add-network-credentials-sql-server-pdw.md).  
  
3.  To remove credentials from SQL Server PDW, use [sp_pdw_remove_network_credentials &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pdw-remove-network-credentials-sql-server-pdw.md).  
  
## Error Handling  
The RESTORE DATABASE command results in errors under the following conditions:  
  
-   The name of the database to restore already exists on the target appliance. To avoid this, choose a unique database name, or drop the existing database before running the restore.  
  
-   There is an invalid set of backup files in the backup directory.  
  
-   The login permissions are not sufficient to restore a database.  
  
-   SQL Server PDW does not have the correct permissions to the network location where the backup files are located.  
  
-   The network location for the backup directory does not exist, or is not available.  
  
-   There is insufficient disk space on the Compute nodes or Control node. SQL Server PDW does not confirm that sufficient disk space exists on the appliance before initiating the restore. Therefore, it is possible to generate an out-of-disk-space error while running the RESTORE DATABASE statement. When insufficient disk space occurs, SQL Server PDW rolls back the restore.  
  
-   The target appliance to which the database is being restored has fewer Compute nodes than the source appliance from which the database was backed up.  
  
-   The database restore is attempted from within a transaction.  
  
## General Remarks  
SQL Server PDW tracks the success of database restores. Before restoring a differential database backup, SQL Server PDW verifies the full database restore finished successfully.  
  
After a restore, the user database will have database compatibility level 120. This is true for all databases regardless of their original compatibility level.  
  
**Restoring to an Appliance With a Larger Number of Compute Nodes**  
  
Restoring a backup to an appliance with a larger number of Compute nodes grows the allocated database size in proportion to the number of Compute nodes.  
  
For example, when restoring a 60 GB database from a 2-node appliance (30 GB per node) to a 6-node appliance, SQL Server PDW creates a 180 GB database (6 nodes with 30 GB per node) on the 6-node appliance. SQL Server PDW initially restores the database to 2 nodes to match the source configuration, and then redistributes the data to all 6 nodes.  
  
After the redistribution each Compute node will contain less actual data and more free space than each Compute node on the smaller source appliance. Use the additional space to add more data to the database. If the restored database size is larger than you need, you can use [ALTER DATABASE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-database-sql-server-pdw.md) to shrink the database file sizes.  
  
## Limitations and Restrictions  
For these limitations and restrictions, the source appliance is the appliance from which the database backup was created, and the target appliance is the appliance to which the database will be restored.  
  
Restoring a database does not automatically rebuild statistics.  
  
Only one RESTORE DATABASE or BACKUP DATABASE statement can be running on the appliance at any given time. If multiple backup and restore statements are submitted concurrently, the appliance will put them into a queue and process them one at a time.  
  
You can only restore a database backup to a SQL Server PDW target appliance that has the same number or more Compute nodes than the source appliance. The target appliance cannot have fewer Compute nodes than the source appliance.  
  
You cannot restore a backup that was created on an appliance that has SQL Server 2012 PDW hardware to an appliance that has SQL Server 2008 R2 hardware. This holds true even if the appliance was originally purchased with the SQL Server 2008 R2 PDW hardware and is now running SQL Server 2012 PDW software.  
  
## Locking  
Takes an exclusive lock on the DATABASE object.  
  
## Examples  
  
### A. Simple RESTORE examples  
The following example restores a full backup to the `SalesInvoices2013` database. The backup files are stored in the \\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full directory. The SalesInvoices2013 database cannot already exist on the target appliance or this command will fail with an error.  
  
```  
RESTORE DATABASE SalesInvoices2013  
FROM DISK = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full';  
```  
  
### B. Restore a full and differential backup  
The following example restores a full, and then a differential backup to the SalesInvoices2013 database  
  
The full backup of the database is restored from the full backup which is stored in the '\\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full' directory. If the restore completes successfully, the differential backup is restored to the SalesInvoices2013 database.  The differential backup is stored in the '\\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Diff' directory.  
  
```  
RESTORE DATABASE SalesInvoices2013  
    FROM DISK = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Diff'  
    WITH BASE = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full'  
[;]  
```  
  
### C. Restoring the backup header  
This example restores the header information for database backup '\\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full' . The command results in one row of information for the Invoices2013Full backup.  
  
```  
RESTORE HEADERONLY  
    FROM DISK = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full'  
[;]  
```  
  
You can use the header information to check the contents of a backup, or to make sure the target restoration appliance is compatible with the source backup appliance before attempting to restore the backup.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[BACKUP DATABASE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/backup-database-sql-server-pdw.md)  
  
