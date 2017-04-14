---
# required metadata

title: Configure log shipping for SQL Server on Linux | Microsoft Docs
description: This tutorial shows a basic example of how to replicate a SQL Server instance on Linux to a secondary instance using log shipping.
author: meet-bhagdev 
ms.author: meetb 
manager: jhubbard
ms.date: 04/19/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---


#What is SQL Server log shipping?

SQL Server Log shipping is a HA-like configuration where a database from a primary server is replicated onto one or more secondary servers The process of transferring the transaction log files and restoring is automated across the SQL Servers. As the process result there are two copies of the data on two separate locations. In a nutshell, a backup of the source database is restored onto the secondary server. Then the primary server creates transaction log backups periodically, and the secondary servers restore them, updating the secondary copy of the database. 

  ![Logshipping](https://preview.ibb.co/hr5Ri5/logshipping.png)


As described in the picture above, a log shipping session involves the following steps:

- Backing up the transaction log file on the primary SQL Server instance
- Copying the transaction log backup file across the network to one or more secondary SQL Server instances
- Restoring the transaction log backup file on the secondary SQL Server instances
  

# Setup a network share for Log Shipping

- Mount a network share
  //Work with Val for Instructions

# Setup Log Shipping via T-SQL

- Run this script from your primary server

      DECLARE @LS_BackupJobId    AS uniqueidentifier 
      DECLARE @LS_PrimaryId      AS uniqueidentifier 
      DECLARE @SP_Add_RetCode    As int 
      EXEC @SP_Add_RetCode = master.dbo.sp_add_log_shipping_primary_database 
                    @database = N'SampleDB' 
                    ,@backup_directory = N'/mnt/azmnt/azlship1/' 
                    ,@backup_share = N'/mnt/azmnt/azlship1/' 
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

      DECLARE @LS_BackUpScheduleUID     As uniqueidentifier 
      DECLARE @LS_BackUpScheduleID      AS int 


      EXEC msdb.dbo.sp_add_schedule 
                    @schedule_name =N'LSBackupSchedule_40.86.189.2341' 
                    ,@enabled = 1 
                    ,@freq_type = 4 
                    ,@freq_interval = 1 
                    ,@freq_subday_type = 4 
                    ,@freq_subday_interval = 5 
                    ,@freq_recurrence_factor = 0 
                    ,@active_start_date = 20170409 
                    ,@active_end_date = 99991231 
                    ,@active_start_time = 0 
                    ,@active_end_time = 235900 
                    ,@schedule_uid = @LS_BackUpScheduleUID OUTPUT 
                    ,@schedule_id = @LS_BackUpScheduleID OUTPUT 

      EXEC msdb.dbo.sp_attach_schedule 
                    @job_id = @LS_BackupJobId 
                    ,@schedule_id = @LS_BackUpScheduleID  

      EXEC msdb.dbo.sp_update_job 
                    @job_id = @LS_BackupJobId 
                    ,@enabled = 1 


      END 


      EXEC master.dbo.sp_add_log_shipping_alert_job 

      EXEC master.dbo.sp_add_log_shipping_primary_secondary 
                    @primary_database = N'SampleDB' 
                    ,@secondary_server = N'<id_address_of_your_secondary_server>' 
                    ,@secondary_database = N'SampleDB' 
                    ,@overwrite = 1 


- Run this script from your secondary server

      DECLARE @LS_Secondary__CopyJobId  AS uniqueidentifier 
      DECLARE @LS_Secondary__RestoreJobId      AS uniqueidentifier 
      DECLARE @LS_Secondary__SecondaryId AS uniqueidentifier 
      DECLARE @LS_Add_RetCode    As int 


      EXEC @LS_Add_RetCode = master.dbo.sp_add_log_shipping_secondary_primary 
                    @primary_server = N'<id_address_of_your_primary_server>' 
                    ,@primary_database = N'SampleDB1' 
                    ,@backup_source_directory = N'/mnt/azmnt/azlship1/' 
                    ,@backup_destination_directory = N'/mnt/azmnt/azlship2/' 
                    ,@copy_job_name = N'LSCopy_SampleDB1' 
                    ,@restore_job_name = N'LSRestore_SampleDB1' 
                    ,@file_retention_period = 4320 
                    ,@overwrite = 1 
                    ,@copy_job_id = @LS_Secondary__CopyJobId OUTPUT 
                    ,@restore_job_id = @LS_Secondary__RestoreJobId OUTPUT 
                    ,@secondary_id = @LS_Secondary__SecondaryId OUTPUT 

      IF (@@ERROR = 0 AND @LS_Add_RetCode = 0) 
      BEGIN 

      DECLARE @LS_SecondaryCopyJobScheduleUID  As uniqueidentifier 
      DECLARE @LS_SecondaryCopyJobScheduleID   AS int 


      EXEC msdb.dbo.sp_add_schedule 
                    @schedule_name =N'DefaultCopyJobSchedule' 
                    ,@enabled = 1 
                    ,@freq_type = 4 
                    ,@freq_interval = 1 
                    ,@freq_subday_type = 4 
                    ,@freq_subday_interval = 6 
                    ,@freq_recurrence_factor = 0 
                    ,@active_start_date = 20170409 
                    ,@active_end_date = 99991231 
                    ,@active_start_time = 0 
                    ,@active_end_time = 235900 
                    ,@schedule_uid = @LS_SecondaryCopyJobScheduleUID OUTPUT 
                    ,@schedule_id = @LS_SecondaryCopyJobScheduleID OUTPUT 

      EXEC msdb.dbo.sp_attach_schedule 
                    @job_id = @LS_Secondary__CopyJobId 
                    ,@schedule_id = @LS_SecondaryCopyJobScheduleID  

      DECLARE @LS_SecondaryRestoreJobScheduleUID      As uniqueidentifier 
      DECLARE @LS_SecondaryRestoreJobScheduleID AS int 


      EXEC msdb.dbo.sp_add_schedule 
                    @schedule_name =N'DefaultRestoreJobSchedule' 
                    ,@enabled = 1 
                    ,@freq_type = 4 
                    ,@freq_interval = 1 
                    ,@freq_subday_type = 4 
                    ,@freq_subday_interval = 8 
                    ,@freq_recurrence_factor = 0 
                    ,@active_start_date = 20170409 
                    ,@active_end_date = 99991231 
                    ,@active_start_time = 0 
                    ,@active_end_time = 235900 
                    ,@schedule_uid = @LS_SecondaryRestoreJobScheduleUID OUTPUT 
                    ,@schedule_id = @LS_SecondaryRestoreJobScheduleID OUTPUT 

      EXEC msdb.dbo.sp_attach_schedule 
                    @job_id = @LS_Secondary__RestoreJobId 
                    ,@schedule_id = @LS_SecondaryRestoreJobScheduleID  


      END 


      DECLARE @LS_Add_RetCode2   As int 


      IF (@@ERROR = 0 AND @LS_Add_RetCode = 0) 
      BEGIN 

      EXEC @LS_Add_RetCode2 = master.dbo.sp_add_log_shipping_secondary_database 
                    @secondary_database = N'SampleDB' 
                    ,@primary_server = N'<id_address_of_your_primary_server>' 
                    ,@primary_database = N'SampleDB' 
                    ,@restore_delay = 0 
                    ,@restore_mode = 0 
                    ,@disconnect_users   = 0 
                    ,@restore_threshold = 45   
                    ,@threshold_alert_enabled = 1 
                    ,@history_retention_period = 5760 
                    ,@overwrite = 1 

      END 


      IF (@@error = 0 AND @LS_Add_RetCode = 0) 
      BEGIN 

      EXEC msdb.dbo.sp_update_job 
                    @job_id = @LS_Secondary__CopyJobId 
                    ,@enabled = 1 

      EXEC msdb.dbo.sp_update_job 
                    @job_id = @LS_Secondary__RestoreJobId 
                    ,@enabled = 1 

      END 

## Next Steps
TBD
