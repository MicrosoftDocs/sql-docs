---
title: "sp_delete_schedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_schedule"
  - "sp_delete_schedule_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_schedule"
ms.assetid: 18b2c985-47b8-49c8-82d1-8a4af3d7d33a
caps.latest.revision: 33
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sp_delete_schedule (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes a schedule.  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_schedule { [ @schedule_id = ] schedule_id | [ @schedule_name = ] 'schedule_name' } ,  
     [ @force_delete = ] force_delete  
```  
  
## Arguments  
 [ **@schedule_id=** ] *schedule_id*  
 The schedule identification number of the schedule to delete. *schedule_id* is **int**, with a default of NULL.  
  
> **NOTE:** Either *schedule_id* or *schedule_name* must be specified, but both cannot be specified.  
  
 [ **@schedule_name=** ] **'***schedule_name***'**  
 The name of the schedule to delete. *schedule_name* is **sysname**, with a default of NULL.  
  
> **NOTE:** Either *schedule_id* or *schedule_name* must be specified, but both cannot be specified.  
  
 [ **@force_delete** = ] *force_delete*  
 Specifies whether the procedure should fail if the schedule is attached to a job. *Force_delete* is bit, with a default of **0**. When *force_delete* is **0**, the stored procedure fails if the schedule is attached to a job. When *force_delete* is **1**, the schedule is deleted regardless of whether the schedule is attached to a job.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 By default, a schedule cannot be deleted if the schedule is attached to a job. To delete a schedule that is attached to a job, specify a value of **1** for *force_delete*. Deleting a schedule does not stop jobs that are currently running.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 Note that the job owner can attach a job to a schedule and detach a job from a schedule without also having to be the schedule owner. However, a schedule cannot be deleted if the detach would leave it with no jobs, unless the caller is the schedule owner.  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](http://msdn.microsoft.com/library/719ce56b-d6b2-414a-88a8-f43b725ebc79).  
  
 Only members of the **sysadmin** role can delete a job schedule that is owned by another user.  
  
## Examples  
  
### A. Deleting a schedule  
 The following example deletes the schedule `NightlyJobs`. If the schedule is attached to any job, the example does not delete the schedule.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_delete_schedule  
    @schedule_name = N'NightlyJobs' ;  
GO  
```  
  
### B. Deleting a schedule attached to a job  
 The following example deletes the schedule `RunOnce`, regardless of whether the schedule is attached to a job.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_delete_schedule  
    @schedule_name = 'RunOnce',  
    @force_delete = 1;  
GO  
```  
  
## See Also  
 [Implement Jobs](http://msdn.microsoft.com/library/69e06724-25c7-4fb3-8a5b-3d4596f21756)   
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)  
  
  
