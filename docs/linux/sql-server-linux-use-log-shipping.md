---
title: Configure log shipping for SQL Server on Linux
description: This tutorial shows a basic example of how to replicate a SQL Server instance on Linux to a secondary instance using log shipping.
author: VanMSFT
ms.author: vanto
ms.date: 07/01/2020
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.custom:
  - intro-get-started
---
# Get started with Log Shipping on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

SQL Server Log shipping is a HA configuration where a database from a primary server is replicated onto one or more secondary servers. In a nutshell, a backup of the source database is restored onto the secondary server. Then the primary server creates transaction log backups periodically, and the secondary servers restore them, updating the secondary copy of the database. 

  ![Diagram showing the log shipping workflow.](https://preview.ibb.co/hr5Ri5/logshipping.png)

As described in the this picture, a log shipping session involves the following steps:

- Backing up the transaction log file on the primary SQL Server instance
- Copying the transaction log backup file across the network to one or more secondary SQL Server instances
- Restoring the transaction log backup file on the secondary SQL Server instances

## Prerequisites
- [Install SQL Server Agent on Linux](./sql-server-linux-setup-sql-agent.md)

## Setup a network share for Log Shipping using CIFS 

> [!NOTE] 
> This tutorial uses CIFS + Samba to setup the network share. If you want to use NFS, leave a comment and we will add it to the doc.       

### Configure Primary Server
-   Run the following to install Samba

    ```bash
    sudo apt-get install samba #For Ubuntu
    sudo yum -y install samba #For RHEL/CentOS
    ```
-   Create a directory to store the logs for Log Shipping and give mssql the required permissions

    ```bash
    mkdir /var/opt/mssql/tlogs
    chown mssql:mssql /var/opt/mssql/tlogs
    chmod 0700 /var/opt/mssql/tlogs
    ```

-   Edit the /etc/samba/smb.conf file (you need root permissions for that) and add the following section:

    ```bash
    [tlogs]
    path=/var/opt/mssql/tlogs
    available=yes
    read only=yes
    browsable=yes
    public=yes
    writable=no
    ```

-   Create a mssql user for Samba

    ```bash
    sudo smbpasswd -a mssql
    ```

-   Restart the Samba services
    ```bash
    sudo systemctl restart smbd.service nmbd.service
    ```
 
### Configure Secondary Server

-   Run the following to install the CIFS client
    ```bash   
    sudo apt-get install cifs-utils #For Ubuntu
    sudo yum -y install cifs-utils #For RHEL/CentOS
    ```

-   Create a file to store your credentials. Use the password you recently set for your mssql Samba account 

    ```console
        vim /var/opt/mssql/.tlogcreds
        #Paste the following in .tlogcreds
        username=mssql
        domain=<domain>
        password=<password>
    ```

-   Run the following commands to create an empty directory for mounting and set permission and ownership correctly
    ```bash   
    mkdir /var/opt/mssql/tlogs
    sudo chown root:root /var/opt/mssql/tlogs
    sudo chmod 0550 /var/opt/mssql/tlogs
    sudo chown root:root /var/opt/mssql/.tlogcreds
    sudo chmod 0660 /var/opt/mssql/.tlogcreds
    ```

-   Add the line to etc/fstab to persist the share 

    ```console
        //<ip_address_of_primary_server>/tlogs /var/opt/mssql/tlogs cifs credentials=/var/opt/mssql/.tlogcreds,ro,uid=mssql,gid=mssql 0 0
    ```

-   Mount the shares
    ```bash   
    sudo mount -a
    ```
       
## Setup Log Shipping via T-SQL

- Run this script from your primary server

    ```sql
    BACKUP DATABASE SampleDB
    TO DISK = '/var/opt/mssql/tlogs/SampleDB.bak'
    GO
    ```

    ```sql
    DECLARE @LS_BackupJobId	AS uniqueidentifier 
    DECLARE @LS_PrimaryId	AS uniqueidentifier 
    DECLARE @SP_Add_RetCode	As int 
    EXECUTE @SP_Add_RetCode = master.dbo.sp_add_log_shipping_primary_database 
             @database = N'SampleDB' 
            ,@backup_directory = N'/var/opt/mssql/tlogs' 
            ,@backup_share = N'/var/opt/mssql/tlogs' 
            ,@backup_job_name = N'LSBackup_SampleDB' 
            ,@backup_retention_period = 4320
            ,@backup_compression = 2
            ,@backup_threshold = 60 
            ,@threshold_alert_enabled = 1
            ,@history_retention_period = 5760 
            ,@backup_job_id = @LS_BackupJobId OUTPUT 
            ,@primary_id = @LS_PrimaryId OUTPUT 
            ,@overwrite = 1 

    IF (@@ERROR = 0 AND @SP_Add_RetCode = 0) 
    BEGIN 

    DECLARE @LS_BackUpScheduleUID	As uniqueidentifier 
    DECLARE @LS_BackUpScheduleID	AS int 

    EXECUTE msdb.dbo.sp_add_schedule 
            @schedule_name =N'LSBackupSchedule' 
            ,@enabled = 1 
            ,@freq_type = 4 
            ,@freq_interval = 1 
            ,@freq_subday_type = 4 
            ,@freq_subday_interval = 15 
            ,@freq_recurrence_factor = 0 
            ,@active_start_date = 20170418 
            ,@active_end_date = 99991231 
            ,@active_start_time = 0 
            ,@active_end_time = 235900 
            ,@schedule_uid = @LS_BackUpScheduleUID OUTPUT 
            ,@schedule_id = @LS_BackUpScheduleID OUTPUT 

    EXECUTE msdb.dbo.sp_attach_schedule 
            @job_id = @LS_BackupJobId 
            ,@schedule_id = @LS_BackUpScheduleID  

    EXECUTE msdb.dbo.sp_update_job 
            @job_id = @LS_BackupJobId 
            ,@enabled = 1 
            
    END 

    EXECUTE master.dbo.sp_add_log_shipping_alert_job 

    EXECUTE master.dbo.sp_add_log_shipping_primary_secondary 
            @primary_database = N'SampleDB' 
            ,@secondary_server = N'<ip_address_of_secondary_server>' 
            ,@secondary_database = N'SampleDB' 
            ,@overwrite = 1 
    ```


- Run this script from your secondary server

    ```sql
    RESTORE DATABASE SampleDB FROM DISK = '/var/opt/mssql/tlogs/SampleDB.bak'
    WITH NORECOVERY;
    ```
    
    ```sql
    DECLARE @LS_Secondary__CopyJobId	AS uniqueidentifier 
    DECLARE @LS_Secondary__RestoreJobId	AS uniqueidentifier 
    DECLARE @LS_Secondary__SecondaryId	AS uniqueidentifier 
    DECLARE @LS_Add_RetCode	As int 

    EXECUTE @LS_Add_RetCode = master.dbo.sp_add_log_shipping_secondary_primary 
            @primary_server = N'<ip_address_of_primary_server>' 
            ,@primary_database = N'SampleDB' 
            ,@backup_source_directory = N'/var/opt/mssql/tlogs/' 
            ,@backup_destination_directory = N'/var/opt/mssql/tlogs/' 
            ,@copy_job_name = N'LSCopy_SampleDB' 
            ,@restore_job_name = N'LSRestore_SampleDB' 
            ,@file_retention_period = 4320 
            ,@overwrite = 1 
            ,@copy_job_id = @LS_Secondary__CopyJobId OUTPUT 
            ,@restore_job_id = @LS_Secondary__RestoreJobId OUTPUT 
            ,@secondary_id = @LS_Secondary__SecondaryId OUTPUT 

    IF (@@ERROR = 0 AND @LS_Add_RetCode = 0) 
    BEGIN 

    DECLARE @LS_SecondaryCopyJobScheduleUID	As uniqueidentifier 
    DECLARE @LS_SecondaryCopyJobScheduleID	AS int 

    EXECUTE msdb.dbo.sp_add_schedule 
            @schedule_name =N'DefaultCopyJobSchedule' 
            ,@enabled = 1 
            ,@freq_type = 4 
            ,@freq_interval = 1 
            ,@freq_subday_type = 4 
            ,@freq_subday_interval = 15 
            ,@freq_recurrence_factor = 0 
            ,@active_start_date = 20170418 
            ,@active_end_date = 99991231 
            ,@active_start_time = 0 
            ,@active_end_time = 235900 
            ,@schedule_uid = @LS_SecondaryCopyJobScheduleUID OUTPUT 
            ,@schedule_id = @LS_SecondaryCopyJobScheduleID OUTPUT 

    EXECUTE msdb.dbo.sp_attach_schedule 
            @job_id = @LS_Secondary__CopyJobId 
            ,@schedule_id = @LS_SecondaryCopyJobScheduleID  

    DECLARE @LS_SecondaryRestoreJobScheduleUID	As uniqueidentifier 
    DECLARE @LS_SecondaryRestoreJobScheduleID	AS int 

    EXECUTE msdb.dbo.sp_add_schedule 
            @schedule_name =N'DefaultRestoreJobSchedule' 
            ,@enabled = 1 
            ,@freq_type = 4 
            ,@freq_interval = 1 
            ,@freq_subday_type = 4 
            ,@freq_subday_interval = 15 
            ,@freq_recurrence_factor = 0 
            ,@active_start_date = 20170418 
            ,@active_end_date = 99991231 
            ,@active_start_time = 0 
            ,@active_end_time = 235900 
            ,@schedule_uid = @LS_SecondaryRestoreJobScheduleUID OUTPUT 
            ,@schedule_id = @LS_SecondaryRestoreJobScheduleID OUTPUT 

    EXECUTE msdb.dbo.sp_attach_schedule 
            @job_id = @LS_Secondary__RestoreJobId 
            ,@schedule_id = @LS_SecondaryRestoreJobScheduleID  
            
    END 
    DECLARE @LS_Add_RetCode2	As int 
    IF (@@ERROR = 0 AND @LS_Add_RetCode = 0) 
    BEGIN 

    EXECUTE @LS_Add_RetCode2 = master.dbo.sp_add_log_shipping_secondary_database 
            @secondary_database = N'SampleDB' 
            ,@primary_server = N'<ip_address_of_primary_server>' 
            ,@primary_database = N'SampleDB' 
            ,@restore_delay = 0 
            ,@restore_mode = 0 
            ,@disconnect_users	= 0 
            ,@restore_threshold = 45   
            ,@threshold_alert_enabled = 1 
            ,@history_retention_period	= 5760 
            ,@overwrite = 1 

    END 

    IF (@@error = 0 AND @LS_Add_RetCode = 0) 
    BEGIN 

    EXECUTE msdb.dbo.sp_update_job 
            @job_id = @LS_Secondary__CopyJobId 
            ,@enabled = 1 

    EXECUTE msdb.dbo.sp_update_job 
            @job_id = @LS_Secondary__RestoreJobId 
            ,@enabled = 1 

    END 
    ```

## Verify Log Shipping works

- Verify that Log Shipping works by starting the following job on the primary server

    ```sql
    USE msdb ;  
    GO  

    EXECUTE dbo.sp_start_job N'LSBackup_SampleDB' ;  
    GO  
    ```

- Verify that Log Shipping works by starting the following job on the secondary server
 
    ```sql
    USE msdb ;  
    GO  

    EXECUTE dbo.sp_start_job N'LSCopy_SampleDB' ;  
    GO  
    EXECUTE dbo.sp_start_job N'LSRestore_SampleDB' ;  
    GO  
    ```
 - Verify that Log Shipping failover works by executing the following command
 
    > [!WARNING]
    > This command will bring the secondary database online and break the Log Shipping configuration. You will need to reconfigure Log    Shipping after running this command.
 
    ```sql
    RESTORE DATABASE SampleDB WITH RECOVERY;
    ```
