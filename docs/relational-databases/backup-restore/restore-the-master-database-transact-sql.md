---
title: "Restore the master database (Transact-SQL)"
description: This article shows you how to restore the master database in SQL Server from a full database backup by using Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.date: "12/07/2022"
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "master database [SQL Server], restoring"
---
# Restore the master database (Transact-SQL)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This article explains how to restore the `master` database from a full database backup.

> [!WARNING]
> In the event of disaster recovery, the instance where the `master` database is being restored to should be as close to an exact match to the original as possible. At a minimum, this recovery instance should be the same version, edition, and patch level, and it should have the same selection of features and the same external configuration (hostname, cluster membership, and so on) as the original instance. Doing otherwise might result in undefined SQL Server instance behavior, with inconsistent feature support, and is not guaranteed to be viable.
  
### To restore the `master` database
  
1. Start the server instance in single-user mode.  
  
   You can start SQL Server by either using the `-m` or `-f` startup parameters. For more information about startup parameters, see [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md).
   
   From a command prompt, run the following commands, and make sure you replace `MSSQLXX.instance` with the appropriate folder name:
  
   ```console
   cd C:\Program Files\Microsoft SQL Server\MSSQLXX.instance\MSSQL\Binn
   sqlservr -c -f -s <instance> -mSQLCMD
   ```

   - The `-mSQLCMD` parameter ensures that only **sqlcmd** can connect to SQL Server.
   - For a default instance name, use `-s MSSQLSERVER`
   - `-c` starts SQL Server as an application to bypass Service Control Manager to shorten startup time
  
   If the SQL Server instance can't start due to a damaged `master` database, you must rebuild the system databases first. For more information, see [Rebuild system databases](../databases/rebuild-system-databases.md).

1. Connect to SQL Server using SQLCMD from another Command Prompt window

   ```console
   SQLCMD -S <instance> -E -d master
   ```

1. To restore a full database backup of **master**, use the following [RESTORE DATABASE](../../t-sql/statements/restore-statements-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
    ```sql
    RESTORE DATABASE master FROM  <backup_device>  WITH REPLACE
    ```
  
     The REPLACE option instructs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to restore the specified database even when a database of the same name already exists. The existing database, if any, is deleted. In single-user mode, we recommend that you enter the RESTORE DATABASE statement in the [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md). For more information, see [Use the sqlcmd Utility](../../tools/sqlcmd/sqlcmd-use-utility.md).  
  
    > [!IMPORTANT]  
    >  After **master** is restored, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shuts down and terminates the **sqlcmd** process. Before you restart the server instance, remove the single-user startup parameter. For more information, see [Configure Server Startup Options &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md).  


1. Restart the server instance normally as a service, without using any startup parameters. 
1. Continue other recovery steps such as restoring other databases, attaching databases, and correcting user mismatches.  
  
## Example

The following example restores the `master` database on the default server instance. The example assumes that the server instance is already running in single-user mode. The example starts `sqlcmd` and executes a `RESTORE DATABASE` statement that restores a full database backup of `master` from a disk device: `Z:\SQLServerBackups\master.bak`.  
  
 > [!NOTE]  
 > For a named instance, the **sqlcmd** command must specify the **-S**_\<ComputerName>_\\*\<InstanceName>* option.

```console
C:\> sqlcmd  
1> RESTORE DATABASE master FROM DISK = 'Z:\SQLServerBackups\master.bak' WITH REPLACE;  
2> GO  
```  
  
## See Also

 [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)   
 [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md)   
 [Troubleshoot Orphaned Users &#40;SQL Server&#41;](../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md)   
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)   
 [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md)   
 [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md)   
 [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)   
 [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
- [Start SQL Server in Single-User Mode](../../database-engine/configure-windows/start-sql-server-in-single-user-mode.md)