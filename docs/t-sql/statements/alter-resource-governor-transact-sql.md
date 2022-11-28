---
title: "ALTER RESOURCE GOVERNOR (Transact-SQL)"
description: ALTER RESOURCE GOVERNOR (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "05/01/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER RESOURCE GOVERNOR"
  - "ALTER_RESOURCE_GOVERNOR_TSQL"
  - "ALTER RESOURCE GOVERNOR RECONFIGURE"
  - "ALTER_RESOURCE_GOVERNOR_RECONFIGURE_TSQL"
helpviewer_keywords:
  - "ALTER RESOURCE GOVERNOR"
  - "RECONFIGURE, ALTER RESOURCE GOVERNOR"
dev_langs:
  - "TSQL"
---
# ALTER RESOURCE GOVERNOR (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This statement is used to perform the following Resource Governor actions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   Apply the configuration changes specified when the CREATE|ALTER|DROP WORKLOAD GROUP or CREATE|ALTER|DROP RESOURCE POOL or CREATE|ALTER|DROP EXTERNAL RESOURCE POOL statements are issued.  
  
-   Enable or disable Resource Governor.  
  
-   Configure classification for incoming requests.  
  
-   Reset workload group and resource pool statistics.  
  
-   Sets the maximum I/O operations per disk volume.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER RESOURCE GOVERNOR   
      { DISABLE | RECONFIGURE }  
    | WITH ( CLASSIFIER_FUNCTION = { schema_name.function_name | NULL } )  
    | RESET STATISTICS  
    | WITH ( MAX_OUTSTANDING_IO_PER_VOLUME = value )   
[ ; ]  
```  
  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 DISABLE  
 Disables Resource Governor. Disabling Resource Governor has the following results:  
  
-   The classifier function is not executed.  
  
-   All new connections are automatically classified into the default group.  
  
-   System-initiated requests are classified into the internal workload group.  
  
-   All existing workload group and resource pool settings are reset to their default values. In this case, no events are fired when limits are reached.  
  
-   Normal system monitoring is not affected.  
  
-   Configuration changes can be made, but the changes do not take effect until Resource Governor is enabled.  
  
-   Upon restarting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the Resource Governor will not load its configuration, but instead will have only the default and internal groups and pools.  
  
 RECONFIGURE  
 When the Resource Governor is not enabled, RECONFIGURE enables the Resource Governor. Enabling Resource Governor has the following results:  
  
-   The classifier function is executed for new connections so that their workload can be assigned to workload groups.  
  
-   The resource limits that are specified in the Resource Governor configuration are honored and enforced.  
  
-   Requests that existed before enabling Resource Governor are affected by any configuration changes that were made when Resource Governor was disabled.  
  
 When Resource Governor is running, RECONFIGURE applies any configuration changes requested when the CREATE|ALTER|DROP WORKLOAD GROUP or CREATE|ALTER|DROP RESOURCE POOL or CREATE|ALTER|DROP EXTERNAL RESOURCE POOL statements are executed.  
  
> [!IMPORTANT]  
>  ALTER RESOURCE GOVERNOR RECONFIGURE must be issued in order for any configuration changes to take effect.  
  
 CLASSIFIER_FUNCTION = { _schema_name_**.**_function_name_ | NULL }  
 Registers the classification function specified by *schema_name.function_name*. This function classifies every new session and assigns the session requests and queries to a workload group. When NULL is used, new sessions are automatically assigned to the default workload group.  
  
 RESET STATISTICS  
 Resets statistics on all workload groups and resource pools. For more information, see [sys.dm_resource_governor_workload_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md) and [sys.dm_resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md).  
  
 MAX_OUTSTANDING_IO_PER_VOLUME = *value*  
 **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.  
  
 Sets the maximum queued I/O operations per disk volume. These I/O operations can be reads or writes of any size.  The maximum value for MAX_OUTSTANDING_IO_PER_VOLUME is 100. It is not a percent. This setting is designed to tune IO resource governance to the IO characteristics of a disk volume. We recommend that you experiment with different values and consider using a calibration tool such as IOMeter, [DiskSpd](https://gallery.technet.microsoft.com/DiskSpd-a-robust-storage-6cd2f223),     or SQLIO (deprecated) to identify the max value for your storage subsystem. This setting provides a system-level safety check that allows SQL Server to meet the minimum IOPS for resource pools even if other pools have the MAX_IOPS_PER_VOLUME set to unlimited. For more information about MAX_IOPS_PER_VOLUME, see [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md).  
  
## Remarks  
 ALTER RESOURCE GOVERNOR DISABLE, ALTER RESOURCE GOVERNOR RECONFIGURE, and ALTER RESOURCE GOVERNOR RESET STATISTICS cannot be used inside a user transaction.  
  
 The RECONFIGURE parameter is part of the Resource Governor syntax and should not be confused with [RECONFIGURE](../../t-sql/language-elements/reconfigure-transact-sql.md), which is a separate DDL statement.  
  
 We recommend being familiar with Resource Governor states before you execute DDL statements. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).  
  
## Permissions  
 Requires CONTROL SERVER permission.  
  
## Examples  
  
### A. Starting the Resource Governor  
 When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is first installed Resource Governor is disabled. The following example starts Resource Governor. After the statement executes, Resource Governor is running and can use the predefined workload groups and resource pools.  
  
```sql  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
```  
  
### B. Assigning new sessions to the default group  
 The following example assigns all new sessions to the default workload group by removing any existing classifier function from the Resource Governor configuration. When no function is designated as a classifier function, all new sessions are assigned to the default workload group. This change applies to new sessions only. Existing sessions are not affected.  
  
```sql  
ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = NULL);  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
```  
  
### C. Creating and registering a classifier function  
 The following example creates a classifier function named `dbo.rgclassifier_v1`. The function classifies every new session based on either the user name or application name and assigns the session requests and queries to a specific workload group. Sessions that do not map to the specified user or application names are assigned to the default workload group. The classifier function is then registered and the configuration change is applied.  
  
```sql  
-- Store the classifier function in the master database.  
USE master;  
GO  
SET ANSI_NULLS ON;  
GO  
SET QUOTED_IDENTIFIER ON;  
GO  
CREATE FUNCTION dbo.rgclassifier_v1() RETURNS sysname   
WITH SCHEMABINDING  
AS  
BEGIN  
-- Declare the variable to hold the value returned in sysname.  
    DECLARE @grp_name AS sysname  
-- If the user login is 'sa', map the connection to the groupAdmin  
-- workload group.   
    IF (SUSER_NAME() = 'sa')  
        SET @grp_name = 'groupAdmin'  
-- Use application information to map the connection to the groupAdhoc  
-- workload group.  
    ELSE IF (APP_NAME() LIKE '%MANAGEMENT STUDIO%')  
        OR (APP_NAME() LIKE '%QUERY ANALYZER%')  
            SET @grp_name = 'groupAdhoc'  
-- If the application is for reporting, map the connection to  
-- the groupReports workload group.  
    ELSE IF (APP_NAME() LIKE '%REPORT SERVER%')  
        SET @grp_name = 'groupReports'  
-- If the connection does not map to any of the previous groups,  
-- put the connection into the default workload group.  
    ELSE  
        SET @grp_name = 'default'  
    RETURN @grp_name  
END;  
GO  
-- Register the classifier user-defined function and update the   
-- the in-memory configuration.  
ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION=dbo.rgclassifier_v1);  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
### D. Resetting Statistics  
 The following example resets all workload group and resource pool statistics.  
  
```sql 
ALTER RESOURCE GOVERNOR RESET STATISTICS;  
```  
  
### E. Setting the MAX_OUTSTANDING_IO_PER_VOLUME option  
 The following example set the MAX_OUTSTANDING_IO_PER_VOLUME option to 20.  
  
```sql  
ALTER RESOURCE GOVERNOR  
WITH (MAX_OUTSTANDING_IO_PER_VOLUME = 20);   
```  
  
## See Also  
 [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-resource-pool-transact-sql.md)   
 [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-pool-transact-sql.md)   
 [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-resource-pool-transact-sql.md)   
 [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md)   
 [DROP EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-external-resource-pool-transact-sql.md)   
 [ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)   
 [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)   
 [ALTER WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-workload-group-transact-sql.md)   
 [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/drop-workload-group-transact-sql.md)   
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [sys.dm_resource_governor_workload_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md)   
 [sys.dm_resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md)  
  
  
