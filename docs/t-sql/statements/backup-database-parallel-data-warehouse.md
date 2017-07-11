---
title: "BACKUP DATABASE (Parallel Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 73c8d465-b36b-4727-b9f3-368e98677c64
caps.latest.revision: 11
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# BACKUP DATABASE (Parallel Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw_md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Creates a backup of a [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database and stores the backup off the appliance in a user-specified network location. Use this statement with [RESTORE DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/restore-database-parallel-data-warehouse.md) for disaster recovery, or to copy a database from one appliance to another.  
  
 **Before you begin**, see "Acquire and Configure a Backup Server" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
 There are two types of backups in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]. A *full database backup* is a backup of an entire [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database. A *differential database backup* only includes changes made since the last full backup. A backup of a user database includes database users, and database roles. A backup of the master database includes logins.  
  
 For more information about [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database backups, see "Backup and Restore" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
      Create a full backup of a user database or the master database.  
BACKUP DATABASE database_name  
    TO DISK = '\\UNC_path\backup_directory'  
    [ WITH [ ( ]  <with_options> [ ,...n ]  [ ) ] ]  
[;]  
  
Create a differential backup of a user database.  
BACKUP DATABASE database_name  
    TO DISK = '\\UNC_path\backup_directory'  
    WITH [ ( ] DIFFERENTIAL   
    [ , <with_options> [ ,...n ] [ ) ]  
[;]  
  
<with_options> ::=  
    DESCRIPTION = 'text'  
    | NAME = 'backup_name'  
  
```  
  
## Arguments  
 *database_name*  
 The name of the database on which to create a backup. The database can be the master database or a user database.  
  
 TO DISK = '\\\\*UNC_path*\\*backup_directory*'  
 The network path and directory to which [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will write the backup files. For example, '\\\xxx.xxx.xxx.xxx\backups\2012\Monthly\08.2012.Mybackup'.  
  
-   The path to the backup directory name must already exist and must be specified as a fully qualified universal naming convention (UNC) path.  
  
-   The backup directory, *backup_directory*, must not exist before running the backup command. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will create the backup directory.  
  
-   The path to the backup directory cannot be a local path and it cannot be a location on any of the [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] appliance nodes.  
  
-   The maximum length of the UNC path and backup directory name is 200 characters.  
  
-   The server or host must be specified as an IP address.  You cannot specify it as the host or server name.  
  
 DESCRIPTION = **'***text***'**  
 Specifies a textual description of the backup. The maximum length of the text is 255 characters.  
  
 The description is stored in the metadata, and will be displayed when the backup header is restored with RESTORE HEADERONLY.  
  
 NAME = **'***backup _name***'**  
 Specifies the name of the backup. The backup name can be different from the database name.  
  
-   Names can have a maximum of 128 characters.  
  
-   Cannot include a path.  
  
-   Must begin with a letter or number character or an underscore (_). Special characters permitted are the underscore (\_), hyphen (-), or space ( ). Backup names cannot end with a space character.  
  
-   The statement will fail if *backup_name* already exists in the specified location.  
  
 This name is stored in the metadata, and will be displayed when the backup header is restored with RESTORE HEADERONLY.  
  
 DIFFERENTIAL  
 Specifies to perform a differential backup of a user database. If omitted, the default is a full database backup. The name of the differential backup does not need to match the name of the full backup. For keeping track of the differential and its corresponding full backup, consider using the same name with 'full' or 'diff' appended.  
  
 For example:  
  
 `BACKUP DATABASE Customer TO DISK = '\\xxx.xxx.xxx.xxx\backups\CustomerFull';`  
  
 `BACKUP DATABASE Customer TO DISK = '\\xxx.xxx.xxx.xxx\backups\CustomerDiff' WITH DIFFERENTIAL;`  
  
## Permissions  
 Requires the **BACKUP DATABASE** permission or membership in the **db_backupoperator** fixed database role. The master database cannot be backed up but by a regular user that was added to the **db_backupoperator** fixed database role. The master database can only be backed up by **sa**, the fabric administrator, or members of the **sysadmin** fixed server role.  
  
 Requires a Windows account that has permission to access, create, and write to the backup directory. You must also store the Windows account name and password in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]. To add these network credentials to [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the [sp_pdw_add_network_credentials &#40;SQL Data Warehouse&#41;](../../relational-databases/system-stored-procedures/sp-pdw-add-network-credentials-sql-data-warehouse.md) stored procedure.  
  
 For more information about managing credentials in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], see the [Security](#Security) section.  
  
## Error Handling  
 BACKUP DATABASE errors under the following conditions:  
  
-   User permissions are not sufficient to perform a backup.  
  
-   [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] does not have the correct permissions to the network location where the backup will be stored.  
  
-   The database does not exist.  
  
-   The target directory already exists on the network share.  
  
-   The target network share is not available.  
  
-   The target network share does not have enough space for the backup. The BACKUP DATABASE command does not confirm that sufficient disk space exists prior to initiating the backup, making it possible to generate an out-of-disk-space error while running BACKUP DATABASE. When insufficient disk space occurs, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] rolls back the BACKUP DATABASE command. To decrease the size of your database, run [DBCC SHRINKLOG (Azure SQL Data Warehouse)](../../t-sql/database-console-commands/dbcc-shrinklog-azure-sql-data-warehouse.md)  
  
-   Attempt to start a backup within a transaction.  
  
## General Remarks  
 Before you perform a database backup, use [DBCC SHRINKLOG (Azure SQL Data Warehouse)](../../t-sql/database-console-commands/dbcc-shrinklog-azure-sql-data-warehouse.md) to decrease the size of your database. 
 
 A [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] backup is stored as a set of multiple files within the same directory.  
  
 A differential backup usually takes less time than a full backup and can be performed more frequently. When multiple differential backups are based on the same full backup, each differential includes all of the changes in the previous differential backup.  
  
 If you cancel a BACKUP command, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will remove the target directory and any files created for the backup. If [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] loses network connectivity to the share, the rollback cannot complete.  
  
 Full backups and differential backups are stored in separate directories. Naming conventions are not enforced for specifying that a full backup and differential backup belong together. You can track this through your own naming conventions. Alternatively, you can track this by using the WITH DESCRIPTION option to add a description, and then by using the RESTORE HEADERONLY statement to retrieve the description.  
  
## Limitations and Restrictions  
 You cannot perform a differential backup of the master database. Only full backups of the master database are supported.  
  
 The backup files are stored in a format suitable only for restoring the backup to a [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] appliance by using the [RESTORE DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/restore-database-parallel-data-warehouse.md) statement.  
  
 The backup with the BACKUP DATABASE statement cannot be used to transfer data or user information to SMP [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases. For that functionality, you can use the remote table copy feature. For more information, see "Remote Table Copy" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
 [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup technology to backup and restore databases. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup options are preconfigured to use backup compression. You cannot set backup options such as compression, checksum, block size, and buffer count.  
  
 Only one database backup or restore can run on the appliance at any given time. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will queue backup or restore commands until the current backup or restore command has completed.  
  
 The target appliance for restoring the backup must have at least as many Compute nodes as the source appliance. The target can have more Compute nodes than the source appliance, but cannot have fewer Compute nodes.  
  
 [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] does not track the location and names of backups since the backups are stored off the appliance.  
  
 [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] does track the success or failure of database backups.  
  
 A differential backup is only allowed if the last full backup completed successfully. For example, suppose that on Monday you create a full backup of the Sales database and the backup finishes successfully. Then on Tuesday you create a full backup of the Sales database and it fails. After this failure, you cannot then create a differential backup based on Monday’s full backup. You must first create a successful full backup before creating a differential backup.  
  
## Metadata  
 These dynamic management views contain information about all backup, restore, and load operations. The information persists across system restarts.  
  
-   [sys.pdw_loader_backup_runs &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-loader-backup-runs-transact-sql.md)  
  
-   [sys.pdw_loader_backup_run_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-loader-backup-run-details-transact-sql.md)  
  
-   [sys.pdw_loader_run_stages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-loader-run-stages-transact-sql.md)  
  
## Performance  
 To perform a backup, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] first backs up the metadata, and then it performs a parallel backup of the database data stored on the Compute nodes. Data is copied directly from each Compute nodes to the backup directory. To achieve the best performance for moving data from the Compute nodes to the backup directory, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] controls the number of Compute nodes that are copying data concurrently.  
  
## Locking  
 Takes an ExclusiveUpdate lock on the DATABASE object.  
  
##  <a name="Security"></a> Security  
 [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] backups are not stored on the appliance. Therefore, your IT team is responsible for managing all aspects of the backup security. For example, this includes managing the security of the backup data, the security of the server used to store backups, and the security of the networking infrastructure that connects the backup server to the [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] appliance.  
  
 **Manage Network Credentials**  
  
 Network access to the backup directory is based on standard Windows file sharing security. Before performing a backup, you need to create or designate a Windows account that will be used for authenticating [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] to the backup directory. This windows account must have permission to access, create, and write to the backup directory.  
  
> [!IMPORTANT]  
>  To reduce security risks with your data, we advise that you designate one Windows account solely for the purpose of performing backup and restore operations. Allow this account to have permissions to the backup location and nowhere else.  
  
 You need to store the user name and password in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] by running the [sp_pdw_add_network_credentials &#40;SQL Data Warehouse&#41;](../../relational-databases/system-stored-procedures/sp-pdw-add-network-credentials-sql-data-warehouse.md) stored procedure. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] uses Windows Credential Manager to store and encrypt user names and passwords on the Control node and Compute nodes. The credentials are not backed up with the BACKUP DATABASE command.  
  
 To remove network credentials from [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], see [sp_pdw_remove_network_credentials &#40;SQL Data Warehouse&#41;](../../relational-databases/system-stored-procedures/sp-pdw-remove-network-credentials-sql-data-warehouse.md).  
  
 To list all of the network credentials stored in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the [sys.dm_pdw_network_credentials &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-network-credentials-transact-sql.md) dynamic management view.  
  
## Examples  
  
### A. Add network credentials for the backup location  
 To create a backup, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] must have read/write permission to the backup directory. The following example shows how to add the credentials for a user. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will store these credentials and use them to for backup and restore operations.  
  
> [!IMPORTANT]  
>  For security reasons, we recommend creating one domain account solely for the purpose of performing backups.  
  
```  
EXEC sp_pdw_add_network_credentials 'xxx.xxx.xxx.xxx', 'domain1\backupuser', '*****';  
```  
  
### B. Remove network credentials for the backup location  
 The following example shows how to remove the credentials for a domain user from [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
  
```  
EXEC sp_pdw_remove_network_credentials 'xxx.xxx.xxx.xxx';  
```  
  
### C. Create a full backup of a user database  
 The following example creates a full backup of the Invoices user database. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will create the Invoices2013 directory and will save the backup files to the \\\10.192.63.147\backups\yearly\Invoices2013Full directory.  
  
```  
BACKUP DATABASE Invoices TO DISK = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full';  
```  
  
### D. Create a differential backup of a user database  
 The following example creates a differential backup, which includes all changes made since the last full backup of the Invoices database. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will create the \\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Diff directory to which it will store the files. The description 'Invoices 2013 differential backup' will be stored with the header information for the backup.  
  
 The differential backup will only run successfully if the last full backup of Invoices completed successfully.  
  
```  
BACKUP DATABASE Invoices TO DISK = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Diff'  
    WITH DIFFERENTIAL,   
    DESCRIPTION = 'Invoices 2013 differential backup';  
```  
  
### E. Create a full backup of the master database  
 The following example creates a full backup of the master database and stores it in the directory '\\\10.192.63.147\backups\2013\daily\20130722\master'.  
  
```  
BACKUP DATABASE master TO DISK = '\\xxx.xxx.xxx.xxx\backups\2013\daily\20130722\master';  
```  
  
### F. Create a backup of appliance login information.  
 The master database stores the appliance login information. To backup the appliance login information you need to backup master.  
  
 The following example creates a full backup of the master database.  
  
```  
BACKUP DATABASE master TO DISK = '\\xxx.xxx.xxx.xxx\backups\2013\daily\20130722\master'  
WITH (   
    DESCRIPTION = 'Master Backup 20130722',  
    NAME = 'login-backup'  
)  
;  
```  
  
## See Also  
 [RESTORE DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/restore-database-parallel-data-warehouse.md)  
  
  