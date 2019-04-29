---
title: "Use Resource Governor to Limit CPU Usage by Backup Compression (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "backup compression [SQL Server], Resource Governor"
  - "backup compression [SQL Server], CPU usage"
  - "compression [SQL Server], backup compression"
  - "backups [SQL Server], compression"
  - "Resource Governor, backup compression"
ms.assetid: 01796551-578d-4425-9b9e-d87210f7ba72
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Use Resource Governor to Limit CPU Usage by Backup Compression (Transact-SQL)
  By default, backing up using compression significantly increases CPU usage, and the additional CPU consumed by the compression process can adversely impact concurrent operations. Therefore, you might want to create a low-priority compressed backup in a session whose CPU usage is limited by[Resource Governor](../resource-governor/resource-governor.md) when CPU contention occurs. This topic presents a scenario that classifies the sessions of a particular [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user by mapping them to a Resource Governor workload group that limits CPU usage in such cases.  
  
> [!IMPORTANT]  
>  In a given Resource Governor scenario, session classification might be based on a user name, an application name, or anything else that can differentiate a connection. For more information, see [Resource Governor Classifier Function](../resource-governor/resource-governor-classifier-function.md) and [Resource Governor Workload Group](../resource-governor/resource-governor-workload-group.md).  
  
##  <a name="Top"></a> This topic contains the following set of scenarios, which are presented in sequence:  
  
1.  [Setting Up a Login and User for Low-Priority Operations](#setup_login_and_user)  
  
2.  [Configuring Resource Governor to Limit CPU Usage](#configure_RG)  
  
3.  [Verifying the Classification of the Current Session (Transact-SQL)](#verifying)  
  
4.  [Compressing Backups Using a Session with Limited CPU](#creating_compressed_backup)  
  
##  <a name="setup_login_and_user"></a> Setting Up a Login and User for Low-Priority Operations  
 The scenario in this topic requires a low-priority [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and user. The user name will be used to classify sessions running in the login and route them to a Resource Governor workload group that limits CPU usage.  
  
 The following procedure describes the steps for setting up a login and user for this purpose, followed by a [!INCLUDE[tsql](../../includes/tsql-md.md)] example, "Example A: Setting Up a Login and User (Transact-SQL)."  
  
### To set up a login and database user for classifying sessions  
  
1.  Create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for creating low-priority compressed backups.  
  
     **To create a login**  
  
    -   [Create a Login](../security/authentication-access/create-a-login.md)  
  
    -   [CREATE LOGIN &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-login-transact-sql)  
  
2.  Optionally, grant VIEW SERVER STATE to this login.  
  
    -   [GRANT System Object Permissions &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-system-object-permissions-transact-sql)  
  
     For more information, see [GRANT Database Principal Permissions &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-database-principal-permissions-transact-sql).  
  
3.  Create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user for this login.  
  
     **To create a user**  
  
    -   [Create a Database User](../security/authentication-access/create-a-database-user.md)  
  
    -   [CREATE USER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-user-transact-sql)  
  
4.  To enable sessions of this login and user to back up a given database, add the user to the db_backupoperator database role of that database. Do this for each database that this user will back up. Optionally, add the user to other fixed database roles.  
  
     **To add a user to a fixed database role**  
  
    -   [sp_addrolemember &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addrolemember-transact-sql)  
  
     For more information, see [GRANT Database Principal Permissions &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-database-principal-permissions-transact-sql).  
  
### Example A: Setting Up a Login and User (Transact-SQL)  
 The following example is relevant only if you choose to create a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and user for low-priority backups. Alternatively, you can use an existing login and user, if an appropriate one exists.  
  
> [!IMPORTANT]  
>  The following example uses a sample login and user name, *domain_name*`\MAX_CPU`. Replace these with the names of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and user that you plan to use when creating your low-priority compressed backups.  
  
 This example creates a login for the *domain_name*`\MAX_CPU` Windows account and then grants VIEW SERVER STATE permission to the login. This permission enables you to verify the Resource Governor classification of sessions of the login. The example then creates a user for *domain_name*`\MAX_CPU` and adds it to the db_backupoperator fixed database role for the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] sample database. This user name will be used by the Resource Governor classifier function.  
  
```sql  
-- Create a SQL Server login for low-priority operations  
USE master;  
CREATE LOGIN [domain_name\MAX_CPU] FROM WINDOWS;  
GRANT VIEW SERVER STATE TO [domain_name\MAX_CPU];  
GO  
-- Create a SQL Server user in AdventureWorks2012 for this login  
USE AdventureWorks2012;  
CREATE USER [domain_name\MAX_CPU] FOR LOGIN [domain_name\MAX_CPU];  
EXEC sp_addrolemember 'db_backupoperator', 'domain_name\MAX_CPU';  
GO  
  
```  
  
 [&#91;Top&#93;](#Top)  
  
##  <a name="configure_RG"></a> Configuring Resource Governor to Limit CPU Usage  
  
> [!NOTE]  
>  Ensure that Resource Governor is enabled. For more information, see [Enable Resource Governor](../resource-governor/enable-resource-governor.md).  
  
 In this Resource Governor scenario, configuration comprises the following basic steps:  
  
1.  Create and configure a Resource Governor resource pool that limits the maximum average CPU bandwidth that will be given to requests in the resource pool when CPU contention occurs.  
  
2.  Create and configure a Resource Governor workload group that uses this pool.  
  
3.  Create a *classifier function*, which is a user-defined function (UDF) whose return values are used by Resource Governor for classifying sessions so that they are routed to the appropriate workload group.  
  
4.  Register the classifier function with Resource Governor.  
  
5.  Apply the changes to the Resource Governor in-memory configuration.  
  
> [!NOTE]  
>  For information about Resource Governor resource pools, workload groups, and classification, see [Resource Governor](../resource-governor/resource-governor.md).  
  
 The [!INCLUDE[tsql](../../includes/tsql-md.md)] statements for these steps are described in the procedure, "To configure Resource Governor for limiting CPU usage," which is followed by a [!INCLUDE[tsql](../../includes/tsql-md.md)] example of the procedure.  
  
 **To configure Resource Governor (SQL Server Management Studio)**  
  
-   [Configure Resource Governor Using a Template](../resource-governor/configure-resource-governor-using-a-template.md)  
  
-   [Create a Resource Pool](../resource-governor/create-a-resource-pool.md)  
  
-   [Create a Workload Group](../resource-governor/create-a-workload-group.md)  
  
### To configure Resource Governor for limiting CPU usage (Transact-SQL)  
  
1.  Issue a [CREATE RESOURCE POOL](/sql/t-sql/statements/create-resource-pool-transact-sql) statement to create a resource pool. The example for this procedure uses the following syntax:  
  
     *CREATE RESOURCE POOL pool_name* WITH ( MAX_CPU_PERCENT = *value* );  
  
     *Value* is an integer from 1 to 100 that indicates the percentage of maximum average CPU bandwidth. The appropriate value depends on your environment. For the purpose of illustration, the example in this topic uses 20%  percent (MAX_CPU_PERCENT = 20.)  
  
2.  Issue a [CREATE WORKLOAD GROUP](/sql/t-sql/statements/create-workload-group-transact-sql) statement to create a workload group for low-priority operations whose CPU usage you want to govern. The example for this procedure uses the following syntax:  
  
     CREATE WORKLOAD GROUP *group_name* USING *pool_name*;  
  
3.  Issue a [CREATE FUNCTION](/sql/t-sql/statements/create-function-transact-sql) statement to create a classifier function that maps the workload group created in the preceding step to the user of the low-priority login. The example for this procedure uses the following syntax:  
  
     CREATE FUNCTION [*schema_name*.]*function_name*() RETURNS sysname  
  
     WITH SCHEMABINDING  
  
     AS  
  
     BEGIN  
  
     DECLARE @workload_group_name AS *sysname*  
  
     IF (SUSER_NAME() = '*user_of_low_priority_login*')  
  
     SET @workload_group_name = '*workload_group_name*'  
  
     RETURN @workload_group_name  
  
     END  
  
     For information about the components of this CREATE FUNCTION statement, see:  
  
    -   [DECLARE @local_variable &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/declare-local-variable-transact-sql)  
  
    -   [SUSER_SNAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/suser-sname-transact-sql)  
  
        > [!IMPORTANT]  
        >  SUSER_NAME is just one of several system functions that can be used in a classifier function. For more information, see [Create and Test a Classifier User-Defined Function](../resource-governor/create-and-test-a-classifier-user-defined-function.md).  
  
    -   [SET @local_variable &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/set-local-variable-transact-sql).  
  
4.  Issue an [ALTER RESOURCE GOVERNOR](/sql/t-sql/statements/alter-resource-governor-transact-sql) statement to register the classifier function with Resource Governor. The example for this procedure uses the following syntax:  
  
     ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = *schema_name*.*function_name*);  
  
5.  Issue a second ALTER RESOURCE GOVERNOR statement to apply the changes to the Resource Governor in-memory configuration, as follows:  
  
    ```  
    ALTER RESOURCE GOVERNOR RECONFIGURE;  
    ```  
  
### Example B: Configuring Resource Governor (Transact-SQL)  
 The following example performs the following steps within a single transaction:  
  
1.  Creates the `pMAX_CPU_PERCENT_20` resource pool.  
  
2.  Creates the `gMAX_CPU_PERCENT_20` workload group.  
  
3.  Creates the `rgclassifier_MAX_CPU()` classifier function, which uses the user name created in the preceding example.  
  
4.  Registers the classifier function with Resource Governor.  
  
 After committing the transaction, the example applies the configuration changes requested in the ALTER WORKLOAD GROUP or ALTER RESOURCE POOL statements.  
  
> [!IMPORTANT]  
>  The following example uses the user name of the sample [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user created in "Example A: Setting Up a Login and User (Transact-SQL)," *domain_name*`\MAX_CPU`. Replace this with the name of the user of the login that you plan to use for creating low-priority compressed backups.  
  
```sql  
-- Configure Resource Governor.  
BEGIN TRAN  
USE master;  
-- Create a resource pool that sets the MAX_CPU_PERCENT to 20%.   
CREATE RESOURCE POOL pMAX_CPU_PERCENT_20  
   WITH  
      (MAX_CPU_PERCENT = 20);  
GO  
-- Create a workload group to use this pool.   
CREATE WORKLOAD GROUP gMAX_CPU_PERCENT_20  
USING pMAX_CPU_PERCENT_20;  
GO  
-- Create a classification function.  
-- Note that any request that does not get classified goes into   
-- the 'Default' group.  
CREATE FUNCTION dbo.rgclassifier_MAX_CPU() RETURNS sysname   
WITH SCHEMABINDING  
AS  
BEGIN  
    DECLARE @workload_group_name AS sysname  
      IF (SUSER_NAME() = 'domain_name\MAX_CPU')  
          SET @workload_group_name = 'gMAX_CPU_PERCENT_20'  
    RETURN @workload_group_name  
END;  
GO  
  
-- Register the classifier function with Resource Governor.  
ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION= dbo.rgclassifier_MAX_CPU);  
COMMIT TRAN;  
GO  
-- Start Resource Governor  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
  
```  
  
 [&#91;Top&#93;](#Top)  
  
##  <a name="verifying"></a> Verifying the Classification of the Current Session (Transact-SQL)  
 Optionally, log in as the user that you specified in your classifier function, and verify the session classification by issuing the following [SELECT](/sql/t-sql/queries/select-transact-sql) statement in Object Explorer:  
  
```sql  
USE master;  
SELECT sess.session_id, sess.login_name, sess.group_id, grps.name   
FROM sys.dm_exec_sessions AS sess   
JOIN sys.dm_resource_governor_workload_groups AS grps   
    ON sess.group_id = grps.group_id  
WHERE session_id > 50;  
GO  
```  
  
 In the results pane, the **name** column should list one or more sessions for the workload-group name that you specified in your classifier function.  
  
> [!NOTE]  
>  For information about the dynamic management views called by this SELECT statement, see [sys.dm_exec_sessions &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql) and [sys.dm_resource_governor_workload_groups &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql).  
  
 [&#91;Top&#93;](#Top)  
  
##  <a name="creating_compressed_backup"></a> Compressing Backups Using a Session with Limited CPU  
 To create a compressed backup in a session with a limited maximum CPU, log in as the user specified in your classifier function. In your backup command, either specify WITH COMPRESSION ([!INCLUDE[tsql](../../includes/tsql-md.md)]) or select **Compress backup** ([!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]). To create a compressed database backup, see [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md).  
  
### Example C: Creating a Compressed Backup (Transact-SQL)  
 The following [BACKUP](/sql/t-sql/statements/backup-transact-sql) example creates a compressed full backup of the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] database in a newly formatted backup file, `Z:\SQLServerBackups\AdvWorksData.bak`.  
  
```sql  
--Run backup statement in the gBackup session.  
BACKUP DATABASE AdventureWorks2012 TO DISK='Z:\SQLServerBackups\AdvWorksData.bak'   
WITH   
   FORMAT,   
   MEDIADESCRIPTION='AdventureWorks2012 Compressed Data Backups'  
   DESCRIPTION='First database backup on AdventureWorks2012 Compressed Data Backups media set'  
   COMPRESSION;  
GO  
```  
  
 [&#91;Top&#93;](#Top)  
  
## See Also  
 [Create and Test a Classifier User-Defined Function](../resource-governor/create-and-test-a-classifier-user-defined-function.md)   
 [Resource Governor](../resource-governor/resource-governor.md)  
  
  
