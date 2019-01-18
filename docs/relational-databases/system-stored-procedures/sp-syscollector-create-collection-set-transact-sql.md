---
title: "sp_syscollector_create_collection_set (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_create_collection_set_TSQL"
  - "sp_syscollector_create_collection_set"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_create_collection_set"
ms.assetid: 69e9ff0f-c409-43fc-89f6-40c3974e972c
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_create_collection_set (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a new collection set. You can use this stored procedure to create a custom collection set for data collection.  
  
> [!WARNING]  
>  In cases where the Windows account configured as a proxy is a non-interactive or interactive user that has not yet logged in, the profile directory will not exist, and the creation of the staging directory will fail. Therefore, if you are using a proxy account on a domain controller, you must specify an interactive account that has been used at least once in order to assure that the profile directory has been created.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_create_collection_set   
      [ @name = ] 'name'  
    , [ [ @target = ] 'target' ]  
    , [ [ @collection_mode = ] collection_mode ]  
    , [ [ @days_until_expiration = ] days_until_expiration ]  
    , [ [ @proxy_id = ] proxy_id ]  
    , [ [ @proxy_name = ] 'proxy_name' ]  
    , [ [ @schedule_uid = ] 'schedule_uid' ]  
    , [ [ @schedule_name = ] 'schedule_name' ]  
    , [ [ @logging_level = ] logging_level ]  
    , [ [ @description = ] 'description' ]  
    , [ @collection_set_id = ] collection_set_id OUTPUT   
    , [ [ @collection_set_uid = ] 'collection_set_uid' OUTPUT ]  
```  
  
## Arguments  
 [ **@name =** ] '*name*'  
 Is the name of the collection set. *name* is **sysname** and cannot be an empty string or NULL.  
  
 *name* must be unique. For a list of current collection set names, query the syscollector_collection_sets system view.  
  
 [ **@target =** ] '*target*'  
 Reserved for future use. *name* is **nvarchar(128)** with a default value of NULL.  
  
 [ **@collection_mode =** ] *collection_mode*  
 Specifies the manner in which the data is collected and stored. *collection_mode* is **smallint** and can have one of the following values:  
  
 0 - Cached mode. Data collection and upload are on separate schedules. Specify cached mode for continuous collection.  
  
 1 - Non-cached mode. Data collection and upload is on the same schedule. Specify non-cached mode for ad hoc collection or snapshot collection.  
  
 The default value for *collection_mode* is 0. When *collection_mode* is 0, *schedule_uid* or *schedule_name* must be specified.  
  
 [ **@days_until_expiration =** ] *days_until_expiration*  
 Is the number of days that the collected data is saved in the management data warehouse. *days_until_expiration* is **smallint** with a default value of 730 (two years). *days_until_expiration* must be 0 or a positive integer.  
  
 [ **@proxy_id =** ] *proxy_id*  
 Is the unique identifier for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account. *proxy_id* is **int** with a default value of NULL. If specified, *proxy_name* must be NULL. To obtain *proxy_id*, query the sysproxies system table. The dc_admin fixed database role must have permission to access the proxy. For more information, see [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md).  
  
 [ **@proxy_name =** ] '*proxy_name*'  
 Is the name of the proxy account. *proxy_name* is **sysname** with a default value of NULL. If specified, *proxy_id* must be NULL. To obtain *proxy_name*, query the sysproxies system table.  
  
 [ **@schedule_uid =** ] '*schedule_uid*'  
 Is the GUID that points to a schedule. *schedule_uid* is **uniqueidentifier** with a default value of NULL. If specified, *schedule_name* must be NULL. To obtain *schedule_uid*, query the sysschedules system table.  
  
 When *collection_mode* is set to 0, *schedule_uid* or *schedule_name* must be specified. When *collection_mode* is set to 1, *schedule_uid* or *schedule_name* is ignored if specified.  
  
 [ **@schedule_name =** ] '*schedule_name*'  
 Is the name of the schedule. *schedule_name* is **sysname** with a default value of NULL. If specified, *schedule_uid* must be NULL. To obtain *schedule_name*, query the sysschedules system table.  
  
 [ **@logging_level =** ] *logging_level*  
 Is the logging level. *logging_level* is **smallint** with one of the following values:  
  
 0 - log execution information and [!INCLUDE[ssIS](../../includes/ssis-md.md)] events that track:  
  
-   Starting/stopping collection sets  
  
-   Starting/stopping packages  
  
-   Error information  
  
 1 - level-0 logging and:  
  
-   Execution statistics  
  
-   Continuously running collection progress  
  
-   Warning events from [!INCLUDE[ssIS](../../includes/ssis-md.md)]  
  
 2 - level-1 logging and detailed event information from [!INCLUDE[ssIS](../../includes/ssis-md.md)]  
  
 The default value for *logging_level* is 1.  
  
 [ **@description =** ] '*description*'  
 Is the description of the collection set. *description* is **nvarchar(4000)** with a default value of NULL.  
  
 [ **@collection_set_id =** ] *collection_set_id*  
 Is the unique local identifier for the collection set. *collection_set_id* is **int** with OUTPUT and is required.  
  
 [ **@collection_set_uid =** ] '*collection_set_uid*'  
 Is the GUID for the collection set. *collection_set_uid* is **uniqueidentifier** with OUTPUT with a default value of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_syscollector_create_collection_set must be run in the context of the msdb system database.  
  
## Permissions  
 Requires membership in the dc_admin (with EXECUTE permission) fixed database role to execute this procedure.  
  
## Examples  
  
### A. Creating a collection set by using default values  
 The following example creates a collection set by specifying only the required parameters. `@collection_mode` is not required, but the default collection mode (cached) requires specifying either a schedule ID or schedule name.  
  
```  
USE msdb;  
GO  
DECLARE @collection_set_id int;  
EXECUTE dbo.sp_syscollector_create_collection_set  
    @name = N'Simple collection set test 1',  
    @description = N'This is a test collection set that runs in non-cached mode.',  
    @collection_mode = 1,  
    @collection_set_id = @collection_set_id OUTPUT;  
GO  
```  
  
### B. Creating a collection set by using specified values  
 The following example creates a collection set by specifying values for many of the parameters.  
  
```  
USE msdb;  
GO  
DECLARE @collection_set_id int;  
DECLARE @collection_set_uid uniqueidentifier;  
SET @collection_set_uid = NEWID();  
EXEC dbo.sp_syscollector_create_collection_set  
    @name = N'Simple collection set test 2',  
    @collection_mode = 0,  
    @days_until_expiration = 365,  
    @description = N'This is a test collection set that runs in cached mode.',  
    @logging_level = 2,  
    @schedule_name = N'CollectorSchedule_Every_30min',  
    @collection_set_id = @collection_set_id OUTPUT,  
    @collection_set_uid = @collection_set_uid OUTPUT;  
```  
  
## See Also  
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [Create a Custom Collection Set That Uses the Generic T-SQL Query Collector Type &#40;Transact-SQL&#41;](../../relational-databases/data-collection/create-custom-collection-set-generic-t-sql-query-collector-type.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [syscollector_collection_sets &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-collection-sets-transact-sql.md)  
  
  
