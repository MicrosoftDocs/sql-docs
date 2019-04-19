---
title: "Create an availability group using Transact-SQL (T-SQL)"
description: "Steps to creating an Always On availability group using Transact-SQL (T-SQL). "
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], creating"
ms.assetid: 8b0a6301-8b79-4415-b608-b40876f30066
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Create an Always On availability group using Transact-SQL (T-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to use [!INCLUDE[tsql](../../../includes/tsql-md.md)] to create and configure an availability group on instances of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] on which the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature is enabled. An *availability group* defines a set of user databases that will fail over as a single unit and a set of failover partners, known as *availability replicas*, that support failover.  
  
> [!NOTE]  
>  For an introduction to availability groups, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
> [!NOTE]  
>  As an alternative to using [!INCLUDE[tsql](../../../includes/tsql-md.md)], you can use the Create Availability Group wizard or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell cmdlets. For more information, see [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md), [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md), or [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-sql-server-powershell.md).  

  
## <a name="PrerequisitesRestrictions"></a> Prerequisites, Restrictions, and Recommendations  
  
-   Before creating an availability group, verify that the instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that host availability replicas reside on different Windows Server Failover Clustering (WSFC) node within the same WSFC failover cluster. Also, verify that each of the server instance meets all other [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] prerequisites. For more information, we strongly recommend that you read [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
  
##  <a name="Permissions"></a> Permissions  
 Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL to Create and Configure an Availability Group 

###  <a name="SummaryTsqlStatements"></a> Summary of Tasks and Corresponding Transact-SQL Statements  
 The following table lists the basic tasks involved in creating and configuring an availability group and indicates which [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements to use for these tasks. The [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] tasks must be performed in the sequence in which they are presented in the table.  
  
|Task|Transact-SQL Statement(s)|Where to Perform Task**&#42;**|  
|----------|----------------------------------|---------------------------------|  
|Create database mirroring endpoint (once per [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance)|[CREATE ENDPOINT](../../../t-sql/statements/create-endpoint-transact-sql.md) *endpointName* ... FOR DATABASE_MIRRORING|Execute on each server instance that lacks database mirroring endpoint.|  
|Create availability group|[CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md)|Execute on the server instance that is to host the initial primary replica.|  
|Join secondary replica to availability group|[ALTER AVAILABILITY GROUP](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md) *group_name* JOIN|Execute on each server instance that hosts a secondary replica.|  
|Prepare the secondary database|[BACKUP](../../../t-sql/statements/backup-transact-sql.md) and [RESTORE](../../../t-sql/statements/restore-statements-transact-sql.md).|Create backups on the server instance that hosts the primary replica.<br /><br /> Restore backups on each server instance that hosts a secondary replica, using RESTORE WITH NORECOVERY.|  
|Start data synchronization by joining each secondary database to availability group|[ALTER DATABASE](../../../t-sql/statements/alter-database-transact-sql-set-hadr.md) *database_name* SET HADR AVAILABILITY GROUP = *group_name*|Execute on each server instance that hosts a secondary replica.|  
  
 *To perform a given task, connect to the indicated server instance or instances.   
 
### Using Transact-SQL 
> [!NOTE]  
>  For a sample configuration procedure containing code examples of each these [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements, see [Example: Configuring an Availability Group that Uses Windows Authentication](#ExampleConfigAGWinAuth).  
  
1.  Connect to the server instance that is to host the primary replica.  
  
2.  Create the availability group by using the [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement.  
  
3.  Join the new secondary replica to the availability group. For more information, see [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
4.  For each database in the availability group, create a secondary database by restoring recent backups of the primary database, using RESTORE WITH NORECOVERY. For more information, see [Example: Setting Up an Availability Group Using Windows Authentication (Transact-SQL)](../../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md), starting with the step that restores the database backup.  
  
5.  Join every new secondary database to the availability group. For more information, see [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
##  <a name="ExampleConfigAGWinAuth"></a> Example: Configuring an Availability Group that Uses Windows Authentication  
 This example creates a sample [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] configuration procedure that uses [!INCLUDE[tsql](../../../includes/tsql-md.md)] to set up database mirroring endpoints that use Windows Authentication and to create and configure an availability group and its secondary databases.  
  
 This example contains the following sections:  
  
-   [Prerequisites for Using the Sample Configuration Procedure](#PrerequisitesForExample)  
  
-   [Sample Configuration Procedure](#SampleProcedure)  
  
-   [Complete Code Example for Sample Configuration Procedure](#CompleteCodeExample)  
  
###  <a name="PrerequisitesForExample"></a> Prerequisites for Using the Sample Configuration Procedure  
 This sample procedure has the following requirements:  
  
-   The server instances must support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. For more information, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
-   Two sample databases, *MyDb1* and *MyDb2*, must exist on the server instance that will host the primary replica. The following code examples create and configure these two databases and create a full backup of each. Execute these code examples on the server instance on which you intend to create the sample availability group. This server instance will host the initial primary replica of the sample availability group.  
  
    1.  The following [!INCLUDE[tsql](../../../includes/tsql-md.md)] example creates these databases and alters them to use the full recovery model:  
  
        ```  
        -- Create sample databases:  
        CREATE DATABASE MyDb1;  
        GO  
        ALTER DATABASE MyDb1 SET RECOVERY FULL;  
        GO  
  
        CREATE DATABASE MyDb2;  
        GO  
        ALTER DATABASE MyDb2 SET RECOVERY FULL;  
        GO  
        ```  
  
    2.  The following code example creates a full database backup of *MyDb1* and *MyDb2*. This code example uses a fictional backup share, \\\\*FILESERVER*\\*SQLbackups*.  
  
        ```  
        -- Backup sample databases:  
        BACKUP DATABASE MyDb1   
        TO DISK = N'\\FILESERVER\SQLbackups\MyDb1.bak'   
            WITH FORMAT  
        GO  
  
        BACKUP DATABASE MyDb2   
        TO DISK = N'\\FILESERVER\SQLbackups\MyDb2.bak'   
            WITH FORMAT  
        GO  
  
        ```  
  
 [&#91;TopOfExample&#93;](#ExampleConfigAGWinAuth)  
  
###  <a name="SampleProcedure"></a> Sample Configuration Procedure  
 In this sample configuration, the availability replica will be created on two stand-alone server instances whose service accounts run under different, but trusted, domains (`DOMAIN1` and `DOMAIN2`).  
  
 The following table summarizes the values used in this sample configuration.  
  
|Initial role|System|Host [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Instance|  
|------------------|------------|---------------------------------------------|  
|Primary|`COMPUTER01`|`AgHostInstance`|  
|Secondary|`COMPUTER02`|Default instance.|  
  
1.  Create a database mirroring endpoint named *dbm_endpoint* on the server instance on which you plan to create the availability group (this is an instance named `AgHostInstance` on `COMPUTER01`). This endpoint uses port 7022. Note that the server instance on which you create the availability group will host the primary replica.  
  
    ```  
    -- Create endpoint on server instance that hosts the primary replica:  
    CREATE ENDPOINT dbm_endpoint  
        STATE=STARTED   
        AS TCP (LISTENER_PORT=7022)   
        FOR DATABASE_MIRRORING (ROLE=ALL)  
    GO  
  
    ```  
  
2.  Create an endpoint *dbm_endpoint* on the server instance that will host the secondary replica (this is the default server instance on `COMPUTER02`). This endpoint uses port 5022.  
  
    ```  
    -- Create endpoint on server instance that hosts the secondary replica:   
    CREATE ENDPOINT dbm_endpoint  
        STATE=STARTED   
        AS TCP (LISTENER_PORT=5022)   
        FOR DATABASE_MIRRORING (ROLE=ALL)  
    GO  
  
    ```  
  
3.  > [!NOTE]  
    >  If the service accounts of the server instances that are to host your availability replicas run under the same domain account this step is unnecessary. Skip it and go directly to the next step.  
  
     If the service accounts of the server instances run under different domain users, on each server instance, create a login for the other server instance and grant this login permission to access the local database mirroring endpoint.  
  
     The following code example shows the [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements for creating a login and granting it permission on an endpoint. The domain account of the remote server instance is represented here as *domain_name*\\*user_name*.  
  
    ```  
    -- If necessary, create a login for the service account, domain_name\user_name  
    -- of the server instance that will host the other replica:  
    USE master;  
    GO  
    CREATE LOGIN [domain_name\user_name] FROM WINDOWS;  
    GO  
    -- And Grant this login connect permissions on the endpoint:  
    GRANT CONNECT ON ENDPOINT::dbm_endpoint   
       TO [domain_name\user_name];  
    GO  
    ```  
  
4.  On the server instance where the user databases reside, create the availability group.  
  
     The following code example creates an availability group named *MyAG* on the server instance on which the sample databases, *MyDb1* and *MyDb2*, were created. The local server instance, `AgHostInstance`, on *COMPUTER01* is specified first. This instance will host the initial primary replica. A remote server instance, the default server instance on *COMPUTER02*, is specified to host a secondary replica. Both availability replica are configured to use asynchronous-commit mode with manual failover (for asynchronous-commit replicas manual failover means  forced failover with possible data loss).  
  
    ```  
  
              -- Create the availability group, MyAG:   
    CREATE AVAILABILITY GROUP MyAG   
       FOR   
          DATABASE MyDB1, MyDB2   
       REPLICA ON   
          'COMPUTER01\AgHostInstance' WITH   
             (  
             ENDPOINT_URL = 'TCP://COMPUTER01.Adventure-Works.com:7022',   
             AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,  
             FAILOVER_MODE = MANUAL  
             ),  
          'COMPUTER02' WITH   
             (  
             ENDPOINT_URL = 'TCP://COMPUTER02.Adventure-Works.com:5022',  
             AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,  
             FAILOVER_MODE = MANUAL  
             );   
    GO  
    ```  
  
     For additional [!INCLUDE[tsql](../../../includes/tsql-md.md)] code examples of creating an availability group, see [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../../t-sql/statements/create-availability-group-transact-sql.md).  
  
5.  On the server instance that hosts the secondary replica, join the secondary replica to the availability group.  
  
     The following code example joins the secondary replica on `COMPUTER02` to the `MyAG` availability group.  
  
    ```  
    -- On the server instance that hosts the secondary replica,   
    -- join the secondary replica to the availability group:  
    ALTER AVAILABILITY GROUP MyAG JOIN;  
    GO  
    ```  
  
6.  On the server instance that hosts the secondary replica, create the secondary databases.  
  
     The following code example creates the *MyDb1* and *MyDb2* secondary databases by restoring database backups using RESTORE WITH NORECOVERY.  
  
    ```  
    -- On the server instance that hosts the secondary replica,   
    -- Restore database backups using the WITH NORECOVERY option:  
    RESTORE DATABASE MyDb1   
        FROM DISK = N'\\FILESERVER\SQLbackups\MyDb1.bak'   
        WITH NORECOVERY  
    GO  
  
    RESTORE DATABASE MyDb2   
        FROM DISK = N'\\FILESERVER\SQLbackups\MyDb2.bak'   
        WITH NORECOVERY  
    GO  
  
    ```  
  
7.  On the server instance that hosts the primary replica, back up the transaction log on each of the primary databases.  
  
    > [!IMPORTANT]  
    >  When you are configuring a real availability group, we recommend that, before taking this log backup, you suspend log backup tasks for your primary databases until you have joined the corresponding secondary databases to the availability group.  
  
     The following code example creates a transaction log backup on MyDb1 and on MyDb2.  
  
    ```  
    -- On the server instance that hosts the primary replica,   
    -- Backup the transaction log on each primary database:  
    BACKUP LOG MyDb1   
    TO DISK = N'\\FILESERVER\SQLbackups\MyDb1.bak'   
        WITH NOFORMAT  
    GO  
  
    BACKUP LOG MyDb2   
    TO DISK = N'\\FILESERVER\SQLbackups\MyDb2.bak'   
        WITHNOFORMAT  
    GO  
  
    ```  
  
    > [!TIP]  
    >  Typically, a log backup must be taken on each primary database and then restored on the corresponding secondary database (using WITH NORECOVERY). However, this log backup might be unnecessary if the database has just been created and no log backup has been taken yet or the recovery model has just been changed from SIMPLE to FULL.  
  
8.  On the server instance that hosts the secondary replica, apply log backups to the secondary databases.  
  
     The following code example applies backups to *MyDb1* and *MyDb2* secondary databases by restoring database backups using RESTORE WITH NORECOVERY.  
  
    > [!IMPORTANT]  
    >  When you are preparing a real secondary database, you need to apply every log backup taken since the database backup from which you created the secondary database, starting with the earliest and always using RESTORE WITH NORECOVERY. Of course, if you restore both full and differential database backups, you would only need to apply the log backups taken after the differential backup.  
  
    ```  
    -- Restore the transaction log on each secondary database,  
    -- using the WITH NORECOVERY option:  
    RESTORE LOG MyDb1   
        FROM DISK = N'\\FILESERVER\SQLbackups\MyDb1.bak'   
        WITH FILE=1, NORECOVERY  
    GO  
    RESTORE LOG MyDb2   
        FROM DISK = N'\\FILESERVER\SQLbackups\MyDb2.bak'   
        WITH FILE=1, NORECOVERY  
    GO  
    ```  
  
9. On the server instance that hosts the secondary replica, join the new secondary databases to the availability group.  
  
     The following code example, joins the *MyDb1* secondary database and then the *MyDb2* secondary databases to the *MyAG* availability group.  
  
    ```  
    -- On the server instance that hosts the secondary replica,   
    -- join each secondary database to the availability group:  
    ALTER DATABASE MyDb1 SET HADR AVAILABILITY GROUP = MyAG;  
    GO  
  
    ALTER DATABASE MyDb2 SET HADR AVAILABILITY GROUP = MyAG;  
    GO  
  
    ```  
  
###  <a name="CompleteCodeExample"></a> Complete Code Example for Sample Configuration Procedure  
 The following example merges the code examples from all the steps of the sample configuration procedure. The following table summarized the placeholder values used in this code example. For more information about the steps in this code example, see [Prerequisites for Using the Sample Configuration Procedure](#PrerequisitesForExample) and [Sample Configuration Procedure](#SampleProcedure), earlier in this topic.  
  
|Placeholder|Description|  
|-----------------|-----------------|  
|\\\\*FILESERVER*\\*SQLbackups*|Fictional backup share.|  
|\\\\*FILESERVER*\\*SQLbackups\MyDb1.bak*|Backup file for MyDb1.|  
|\\\\*FILESERVER*\\*SQLbackups\MyDb2.bak*|Backup file for MyDb2.|  
|*7022*|Port number assigned to each database mirroring endpoint.|  
|*COMPUTER01\AgHostInstance*|Server instance that hosts the initial primary replica.|  
|*COMPUTER02*|Server instance that hosts the initial secondary replica. This is the default server instance on `COMPUTER02`.|  
|*dbm_endpoint*|Name specified for each database mirroring endpoint.|  
|*MyAG*|Name of sample availability group.|  
|*MyDb1*|Name of first sample database.|  
|*MyDb2*|Name of second sample database.|  
|*DOMAIN1\user1*|Service account of the server instance that is to host the initial primary replica.|  
|*DOMAIN2\user2*|Service account of the server instance that is to host the initial secondary replica.|  
|TCP://*COMPUTER01.Adventure-Works.com*:*7022*|Endpoint URL of the AgHostInstance instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on COMPUTER01.|  
|TCP://*COMPUTER02.Adventure-Works.com*:*5022*|Endpoint URL of the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on COMPUTER02.|  
  
> [!NOTE]  
>  For additional [!INCLUDE[tsql](../../../includes/tsql-md.md)] code examples of creating an availability group, see [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../../t-sql/statements/create-availability-group-transact-sql.md).  
  
```  
-- on the server instance that will host the primary replica,   
-- create sample databases:  
CREATE DATABASE MyDb1;  
GO  
ALTER DATABASE MyDb1 SET RECOVERY FULL;  
GO  
  
CREATE DATABASE MyDb2;  
GO  
ALTER DATABASE MyDb2 SET RECOVERY FULL;  
GO  
  
-- Backup sample databases:  
BACKUP DATABASE MyDb1   
TO DISK = N'\\FILESERVER\SQLbackups\MyDb1.bak'   
    WITH FORMAT  
GO  
  
BACKUP DATABASE MyDb2   
TO DISK = N'\\FILESERVER\SQLbackups\MyDb2.bak'   
    WITH FORMAT  
GO  
  
-- Create the endpoint on the server instance that will host the primary replica:  
CREATE ENDPOINT dbm_endpoint  
    STATE=STARTED   
    AS TCP (LISTENER_PORT=7022)   
    FOR DATABASE_MIRRORING (ROLE=ALL)  
GO  
  
-- Create the endpoint on the server instance that will host the secondary replica:   
CREATE ENDPOINT dbm_endpoint  
    STATE=STARTED   
    AS TCP (LISTENER_PORT=7022)   
    FOR DATABASE_MIRRORING (ROLE=ALL)  
GO  
  
-- If both service accounts run under the same domain account, skip this step. Otherwise,   
-- On the server instance that will host the primary replica,   
-- create a login for the service account   
-- of the server instance that will host the secondary replica, DOMAIN2\user2,   
-- and grant this login connect permissions on the endpoint:  
USE master;  
GO  
CREATE LOGIN [DOMAIN2\user2] FROM WINDOWS;  
GO  
GRANT CONNECT ON ENDPOINT::dbm_endpoint   
   TO [DOMAIN2\user2];  
GO  
  
-- If both service accounts run under the same domain account, skip this step. Otherwise,   
-- On the server instance that will host the secondary replica,  
-- create a login for the service account   
-- of the server instance that will host the primary replica, DOMAIN1\user1,   
-- and grant this login connect permissions on the endpoint:  
USE master;  
GO  
  
CREATE LOGIN [DOMAIN1\user1] FROM WINDOWS;  
GO  
GRANT CONNECT ON ENDPOINT::dbm_endpoint   
   TO [DOMAIN1\user1];  
GO  
  
-- On the server instance that will host the primary replica,   
-- create the availability group, MyAG:  
CREATE AVAILABILITY GROUP MyAG   
   FOR   
      DATABASE MyDB1, MyDB2   
   REPLICA ON   
      'COMPUTER01\AgHostInstance' WITH   
         (  
         ENDPOINT_URL = 'TCP://COMPUTER01.Adventure-Works.com:7022',  
         AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,  
         FAILOVER_MODE = AUTOMATIC  
         ),  
      'COMPUTER02' WITH   
         (  
         ENDPOINT_URL = 'TCP://COMPUTER02.Adventure-Works.com:7022',  
         AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,  
         FAILOVER_MODE = AUTOMATIC  
         );   
GO  
  
-- On the server instance that hosts the secondary replica,   
-- join the secondary replica to the availability group:  
ALTER AVAILABILITY GROUP MyAG JOIN;  
GO  
  
-- Restore database backups onto this server instance, using RESTORE WITH NORECOVERY:  
RESTORE DATABASE MyDb1   
    FROM DISK = N'\\FILESERVER\SQLbackups\MyDb1.bak'   
    WITH NORECOVERY  
GO  
  
RESTORE DATABASE MyDb2   
    FROM DISK = N'\\FILESERVER\SQLbackups\MyDb2.bak'   
    WITH NORECOVERY  
GO  
  
-- Back up the transaction log on each primary database:  
BACKUP LOG MyDb1   
TO DISK = N'\\FILESERVER\SQLbackups\MyDb1.bak'   
    WITH NOFORMAT  
GO  
  
BACKUP LOG MyDb2   
TO DISK = N'\\FILESERVER\SQLbackups\MyDb2.bak'   
    WITHNOFORMAT  
GO  
  
-- Restore the transaction log on each secondary database,  
-- using the WITH NORECOVERY option:  
RESTORE LOG MyDb1   
    FROM DISK = N'\\FILESERVER\SQLbackups\MyDb1.bak'   
    WITH FILE=1, NORECOVERY  
GO  
RESTORE LOG MyDb2   
    FROM DISK = N'\\FILESERVER\SQLbackups\MyDb2.bak'   
    WITH FILE=1, NORECOVERY  
GO  
  
-- On the server instance that hosts the secondary replica,   
-- join each secondary database to the availability group:  
ALTER DATABASE MyDb1 SET HADR AVAILABILITY GROUP = MyAG;  
GO  
  
ALTER DATABASE MyDb2 SET HADR AVAILABILITY GROUP = MyAG;  
GO  
  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To configure availability group and replica properties**  
  
-   [Change the Availability Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-availability-mode-of-an-availability-replica-sql-server.md)  
  
-   [Change the Failover Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-failover-mode-of-an-availability-replica-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [Configure the Flexible Failover Policy to Control Conditions for Automatic Failover &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
-   [Configure Backup on Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md)  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
-   [Change the Session-Timeout Period for an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-session-timeout-period-for-an-availability-replica-sql-server.md)  
  
 **To complete availability group configuration**  
  
-   [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
 **Alternative ways to create an availability group**  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-sql-server-powershell.md)  
  
 **To enable Always On Availability Groups**  
  
-   [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)  
  
 **To configure a database mirroring endpoint**  
  
-   [Create a Database Mirroring Endpoint for Always On Availability Groups &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md)  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
 **To troubleshoot Always On Availability Groups configuration**  
  
-   [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
-   [Troubleshoot a Failed Add-File Operation &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](https://blogs.msdn.com/b/psssql/archive/2012/05/17/Always%20On-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases.aspx)  
  
     [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Videos:**  
  
     [Microsoft SQL Server Code-Named "Denali" Always On Series,Part 1: Introducing the Next Generation High Availability Solution](https://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI302)  
  
     [Microsoft SQL Server Code-Named "Denali" Always On Series,Part 2: Building a Mission-Critical High Availability Solution Using Always On](https://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI404)  
  
-   **Whitepapers:**  
  
     [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)  
  
## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)  
  
  
