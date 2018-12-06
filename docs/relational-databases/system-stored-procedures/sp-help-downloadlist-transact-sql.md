---
title: "sp_help_downloadlist (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_downloadlist_TSQL"
  - "sp_help_downloadlist"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_downloadlist"
ms.assetid: 745b265b-86e8-4399-b928-c6969ca1a2c8
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_downloadlist (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists all rows in the **sysdownloadlist** system table for the supplied job, or all rows if no job is specified.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_downloadlist { [ @job_id = ] job_id | [ @job_name = ] 'job_name' }   
     [ , [ @operation = ] 'operation' ]   
     [ , [ @object_type = ] 'object_type' ]   
     [ , [ @object_name = ] 'object_name' ]   
     [ , [ @target_server = ] 'target_server' ]   
     [ , [ @has_error = ] has_error ]   
     [ , [ @status = ] status ]   
     [ , [ @date_posted = ] date_posted ]  
```  
  
## Arguments  
 [ **@job_id=** ] *job_id*  
 The job identification number for which to return information. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
 [ **@job_name=** ] **'***job_name***'**  
 The name of the job. *job_name* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
 [ **@operation=** ] **'***operation***'**  
 The valid operation for the specified job. *operation* is **varchar(64)**, with a default of NULL, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**DEFECT**|Server operation that requests the target server to defect from the Master **SQLServerAgent** service.|  
|**DELETE**|Job operation that removes an entire job.|  
|**INSERT**|Job operation that inserts an entire job or refreshes an existing job. This operation includes all job steps and schedules, if applicable.|  
|**RE-ENLIST**|Server operation that causes the target server to resend its enlistment information, including the polling interval and time zone to the multiserver domain. The target server also redownloads the **MSXOperator** details.|  
|**SET-POLL**|Server operation that sets the interval, in seconds, for target servers to poll the multiserver domain. If specified, *value* is interpreted as the required interval value, and can be a value from **10** to **28,800**.|  
|**START**|Job operation that requests the start of job execution.|  
|**STOP**|Job operation that requests the stop of job execution.|  
|**SYNC-TIME**|Server operation that causes the target server to synchronize its system clock with the multiserver domain. Because this is a costly operation, perform this operation on a limited, infrequent basis.|  
|**UPDATE**|Job operation that updates only the **sysjobs** information for a job, not the job steps or schedules. Is automatically called by **sp_update_job**.|  
  
 [ **@object_type=** ] **'***object_type***'**  
 The type of object for the specified job. *object_type* is **varchar(64)**, with a default of NULL. *object_type* can be either JOB or SERVER. For more information about valid *object_type*values, see [sp_add_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-category-transact-sql.md).  
  
 [ **@object_name=** ] **'***object_name***'**  
 The name of the object. *object_name* is **sysname**, with a default of NULL. If *object_type* is JOB, *object_name*is the job name. If *object_type*is SERVER, *object_name*is the server name.  
  
 [ **@target_server=** ] **'***target_server***'**  
 The name of the target server. *target_server* is **nvarchar(128)**, with a default of NULL.  
  
 [ **@has_error=** ] *has_error*  
 Is whether the job should acknowledge errors. *has_error* is **tinyint**, with a default of NULL, which indicates no errors should be acknowledged. **1** indicates that all errors should be acknowledged.  
  
 [ **@status=** ] *status*  
 The status for the job. *status* is **tinyint**, with a default value of NULL.  
  
 [ **@date_posted=** ] *date_posted*  
 The date and time for which all entries made on or after the specified date and time should be included in the result set. *date_posted* is **datetime**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**instance_id**|**int**|Unique integer identification number of the instruction.|  
|**source_server**|**nvarchar(30)**|Computer name of the server the instruction came from. In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0, this is always the computer name of the master (MSX) server.|  
|**operation_code**|**nvarchar(4000)**|Operation code for the instruction.|  
|**object_name**|**sysname**|Object affected by the instruction.|  
|**object_id**|**uniqueidentifier**|Identification number of the object affected by the instruction (**job_id** for a job object, or 0x00 for a server object) or a data value specific to the **operation_code**.|  
|**target_server**|**nvarchar(30)**|Target server that this instruction is to be downloaded by.|  
|**error_message**|**nvarchar(1024)**|Error message (if any) from the target server if it encountered a problem while processing this instruction.<br /><br /> Note: Any error message blocks all further downloads by the target server.|  
|**date_posted**|**datetime**|Date the instruction was posted to the table.|  
|**date_downloaded**|**datetime**|Date the instruction was downloaded by the target server.|  
|**status**|**tinyint**|Status of the job:<br /><br /> **0** = Not yet downloaded<br /><br /> **1** = Successfully downloaded.|  
  
## Permissions  
 Permissions to execute this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example lists rows in the `sysdownloadlist` for the `NightlyBackups` job.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_downloadlist  
    @job_name = N'NightlyBackups',  
    @operation = N'UPDATE',   
    @object_type = N'JOB',   
    @object_name = N'NightlyBackups',  
    @target_server = N'SEATTLE2',   
    @has_error = 1,   
    @status = NULL,   
    @date_posted = NULL ;  
GO  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
