---
title: "Disable SQL Server Managed Backup to Microsoft Azure | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: 3e02187f-363f-4e69-a82f-583953592544
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Disable SQL Server Managed Backup to Microsoft Azure
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to disable or pause [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] at both the database and instance levels.  
  
##  <a name="DatabaseDisable"></a> Disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for a database  
 You can disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] settings by using the system stored procedure, [managed_backup.sp_backup_config_basic (Transact-SQL)](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-basic-transact-sql.md). The *@enable_backup* parameter is used to enable and disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configurations for a specific database, where 1 enables and 0 disables the configuration settings.  
  
#### To Disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for a specific database:  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
```  
EXEC msdb.managed_backup.sp_backup_config_basic  
                @database_name = 'TestDB'   
                ,@enable_backup = 0;  
GO  
  
```  
  
##  <a name="DatabaseAllDisable"></a> Disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for all the databases on the Instance  
 The following procedure is for when you want to disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration settings from all the databases that currently have [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] enabled on the instance.  The configuration settings like the storage URL, retention, and the SQL Credential will remain in the metadata and can be used if [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is enabled for the database at a later time. If you want to just pause [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] services temporarily, you can use the master switch explained in the later sections of this topic.  
  
#### To disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for all the databases:  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The following example identifies if [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is configured at the instance level and all the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] enabled databases on the instance, and executes the system stored procedure **sp_backup_config_basic** to disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
  
```  
-- Create a working table to store the database names  
Declare @DBNames TABLE  
  
       (  
             RowID int IDENTITY PRIMARY KEY  
             ,DBName varchar(500)  
  
       )  
-- Define the variables  
DECLARE @rowid int  
DECLARE @dbname varchar(500)  
DECLARE @SQL varchar(2000)  
-- Get the database names from the system function  
  
INSERT INTO @DBNames (DBName)  
  
SELECT db_name  
       FROM   
  
       msdb.managed_backup.fn_backup_db_config (NULL)  
       WHERE is_managed_backup_enabled = 1 
       AND is_dropped = 0
  
       --Select DBName from @DBNames  
  
       select @rowid = min(RowID)  
       FROM @DBNames  
  
       WHILE @rowID IS NOT NULL  
       Begin  
  
             Set @dbname = (Select DBName From @DBNames Where RowID = @rowid)  
             Begin  
             Set @SQL = 'EXEC msdb.managed_backup.sp_backup_config_basic    
                @database_name= '''+'' + @dbname+ ''+''',   
                @enable_backup=0'  
  
            EXECUTE (@SQL)  
  
             END  
             Select @rowid = min(RowID)  
             From @DBNames Where RowID > @rowid  
  
       END  
```  
  
 To review the configuration settings for all the databases on the instance, use the following query:  
  
```  
Use msdb;  
GO  
SELECT * FROM managed_backup.fn_backup_db_config (NULL);  
GO  
  
```  
  
##  <a name="InstanceDisable"></a> Disable Default [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] settings for the Instance  
 Default settings at the instance level apply to all new databases created on that instance.  If you no longer need or require default settings, you can disable this configuration by using the **managed_backup.sp_backup_config_basic** system stored procedure with the *@database_name* parameter set to NULL. Disabling does not remove the other configuration settings like the storage URL, retention setting, or the SQL Credential name. These settings will be used if [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is enabled for the instance at a later time.  
  
#### To disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] default configuration settings:  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    EXEC msdb.managed_backup.sp_backup_config_basic  
                    @enable_backup = 0;  
    GO  
  
    ```  
  
##  <a name="InstancePause"></a> Pause [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] at the Instance Level  
 There might be times when you need to temporarily pause the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] services for a short period time.  The **managed_backup.sp_backup_master_switch** system stored procedure allows you to disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] service at the instance level.  The same stored procedure is used to resume [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. The @state parameter is used to define whether [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] should be turned off or on.  
  
#### To Pause [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] Services Using Transact-SQL:  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and then click **Execute**  
  
```  
Use msdb;  
GO  
EXEC managed_backup.sp_backup_master_switch @new_state=0;  
Go  
  
```  
  
#### To resume [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] Using Transact-SQL  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and then click **Execute**.  
  
```  
Use msdb;  
Go  
EXEC managed_backup.sp_backup_master_switch @new_state=1;  
GO  
  
```  
  
## See Also  
 [Enable SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/enable-sql-server-managed-backup-to-microsoft-azure.md)  
  
  
