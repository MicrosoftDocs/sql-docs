---
title: "sp_syscollector_update_collection_set (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_update_collection_set_TSQL"
  - "sp_syscollector_update_collection_set"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syscollector_update_collection_set"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 2dccc3cd-0e93-4e3e-a4e5-8fe89b31bd63
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_update_collection_set (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Used to modify the properties of a user-defined collection set or to rename a user-defined collection set.  
  
> [!WARNING]  
>  In cases where the Windows account configured as a proxy is a non-interactive or interactive user that has not yet logged in, the profile directory will not exist, and the creation of the staging directory will fail. Therefore, if you are using a proxy account on a domain controller, you must specify an interactive account that has been used at least once in order to assure that the profile directory has been created.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_update_collection_set   
    [ [ @collection_set_id = ] collection_set_id ]  
    , [ [ @name = ] 'name' ]  
    , [ [ @new_name = ] 'new_name' ]  
    , [ [ @target = ] 'target' ]  
    , [ [ @collection_mode = ] collection_mode ]  
    , [ [ @days_until_expiration = ] days_until_expiration ]  
    , [ [ @proxy_id = ] proxy_id ]  
    , [ [ @proxy_name = ] 'proxy_name' ]  
    ,[ [ @schedule_uid = ] 'schedule_uid' ]  
    ,[ [ @schedule_name = ] 'schedule_uid' ]  
    , [ [ @logging_level = ] logging_level ]  
    , [ [ @description = ] 'description' ]  
```  
  
## Arguments  
 [ **@collection_set_id =** ] *collection_set_id*  
 Is the unique local identifier for the collection set. *collection_set_id* is **int** and must have a value if *name* is NULL.  
  
 [ **@name =** ] '*name*'  
 Is the name of the collection set. *name* is **sysname** and must have a value if *collection_set_id* is NULL.  
  
 [ **@new_name =** ] '*new_name*'  
 Is the new name for the collection set. *new_name* is **sysname**, and if used, cannot be an empty string. *new_name* must be unique. For a list of current collection set names, query the syscollector_collection_sets system view.  
  
 [ **@target =** ] '*target*'  
 Reserved for future use.  
  
 [ **@collection_mode =** ] *collection_mode*  
 Is the type of data collection to use. *collection_mode* is **smallint** and can have one of the following values:  
  
 0 - Cached mode. Data collection and upload are on separate schedules. Specify cached mode for continuous collection.  
  
 1 - Non-cached mode. Data collection and upload is on the same schedule. Specify non-cached mode for ad hoc collection or snapshot collection.  
  
 If changing from non-cached mode to cached mode (0), you must also specify either *schedule_uid* or *schedule_name*.  
  
 [ **@days_until_expiration=** ] *days_until_expiration*  
 Is the number of days that the collected data is saved in the management data warehouse. *days_until_expiration* is **smallint**. *days_until_expiration* must be 0 or a positive integer.  
  
 [ **@proxy_id =** ] *proxy_id*  
 Is the unique identifier for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account. *proxy_id* is **int**.  
  
 [ **@proxy_name =** ] '*proxy_name*'  
 Is the name of the proxy. *proxy_name* is **sysname** and is nullable.  
  
 [ **@schedule_uid** = ] '*schedule_uid*'  
 Is the GUID that points to a schedule. *schedule_uid* is **uniqueidentifier**.  
  
 To obtain *schedule_uid*, query the sysschedules system table.  
  
 When *collection_mode* is set to 0, *schedule_uid* or *schedule_name* must be specified. When *collection_mode* is set to 1, *schedule_uid* or *schedule_name* is ignored if specified.  
  
 [ **@schedule_name =** ] '*schedule_name*'  
 Is the name of the schedule. *schedule_name* is **sysname** and is nullable. If specified, *schedule_uid* must be NULL. To obtain *schedule_name*, query the sysschedules system table.  
  
 [ **@logging_level =** ] *logging_level*  
 Is the logging level. *logging_level* is **smallint** with one of the following values:  
  
 0 - Log execution information and [!INCLUDE[ssIS](../../includes/ssis-md.md)] events that track:  
  
-   Starting/stopping collection sets  
  
-   Starting/stopping packages  
  
-   Error information  
  
 1 - Level-0 logging and:  
  
-   Execution statistics  
  
-   Continuously running collection progress  
  
-   Warning events from [!INCLUDE[ssIS](../../includes/ssis-md.md)]  
  
 2 - Level-1 logging and detailed event information from [!INCLUDE[ssIS](../../includes/ssis-md.md)].  
  
 The default value for *logging_level* is 1.  
  
 [ **@description =** ] '*description*'  
 Is the description of the collection set. *description* is **nvarchar(4000)**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_syscollector_update_collection_set must be run in the context of the msdb system database.  
  
 Either *collection_set_id* or *name* must have a value, both cannot be NULL. To obtain these values, query the syscollector_collection_sets system view.  
  
 If the collection set is running, you can only update *schedule_uid* and *description*. To stop the collection set, use [sp_syscollector_stop_collection_set](../../relational-databases/system-stored-procedures/sp-syscollector-stop-collection-set-transact-sql.md).  
  
## Permissions  
 Requires membership in the dc_admin or the dc_operator (with EXECUTE permission) fixed database role to execute this procedure. Although dc_operator can run this stored procedure, members of this role are limited in the properties that they can change. The following properties can only be changed by dc_admin:  
  
-   @new_name  
  
-   @target  
  
-   @proxy_id  
  
-   @description  
  
-   @collection_mode  
  
-   @days_until_expiration  
  
## Examples  
  
### A. Renaming a collection set  
 The following example renames a user-defined collection set.  
  
```  
USE msdb;  
GO  
EXECUTE dbo.sp_syscollector_update_collection_set  
@name = N'Simple collection set test 1',  
@new_name = N'Collection set test 1 in cached mode';  
GO  
```  
  
### B. Changing the collection mode from non-cached to cached  
 The following example changes the collection mode from non-cached mode to cached mode. This change requires that you specify a schedule ID or schedule name.  
  
```  
USE msdb;  
GO  
EXECUTE dbo.sp_syscollector_update_collection_set  
@name = N'Collection set test 1 in cached mode',  
@collection_mode = 0,  
@schedule_uid = 'C7022AF3-51B8-4011-B159-64C47C88FF70';  
-- alternatively, use @schedule_name.  
-- @schedule_name = N'CollectorSchedule_Every_15min;  
GO  
```  
  
### C. Changing other collection set parameters  
 The following example updates various properties of the collection set named "Simple collection set test 2'.  
  
```  
USE msdb;  
GO  
EXEC dbo.sp_syscollector_update_collection_set  
@name = N'Simple collection set test 2',  
@collection_mode = 1,  
@days_until_expiration = 5,  
@description = N'This is a test collection set that runs in noncached mode.',  
@logging_level = 0;  
GO  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [syscollector_collection_sets &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-collection-sets-transact-sql.md)   
 [dbo.sysschedules &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysschedules-transact-sql.md)  
  
  
