---
title: "Create an Availability Group (SQL Server PowerShell) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], creating"
ms.assetid: bc69a7df-20fa-41e1-9301-11317c5270d2
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Create an Availability Group (SQL Server PowerShell)
  This topic describes how to use PowerShell cmdlets to create and configure an AlwaysOn availability group by using PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. An *availability group* defines a set of user databases that will fail over as a single unit and a set of failover partners, known as *availability replicas*, which support failover.  
  
> [!NOTE]  
>  For an introduction to availability groups, see [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md).  
  

  
> [!NOTE]  
>  As an alternative to using PowerShell cmdlets, you can use the Create Availability Group wizard or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. For more information, see [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md) or [Create an Availability Group &#40;Transact-SQL&#41;](create-an-availability-group-transact-sql.md).  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 We strongly recommend that you read this section before attempting to create your first availability group.  
  
###  <a name="PrerequisitesRestrictions"></a> Prerequisites, Restrictions, and Recommendations  
  
-   Before creating an availability group, verify that the host instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] each resides on a different Windows Server Failover Clustering (WSFC) node of a single WSFC failover cluster. Also, verify that your server instances met the other server-instance prerequisites and that all of the other [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] requirements are meet and that you are aware of the recommendations. For more information, we strongly recommend that you read [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-restrictions-recommendations-always-on-availability.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
###  <a name="SummaryPSStatements"></a> Summary of Tasks and Corresponding PowerShell Cmdlets  
 The following table lists the basic tasks involved in configuring an availability group and indicates those that are supported by PowerShell cmdlets. The [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] tasks must be performed in the sequence in which they are presented in the table.  
  
|Task|PowerShell Cmdlets (if Available) or Transact-SQL Statement|Where to Perform Task**<sup>*</sup>**|  
|----------|--------------------------------------------------------------------|-------------------------------------------|  
|Create database mirroring endpoint (once per [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance)|`New-SqlHadrEndPoint`|Execute on each server instance that lacks database mirroring endpoint.<br /><br /> Note: To alter an existing database mirroring endpoint, use `Set-SqlHadrEndpoint`.|  
|Create availability group|First, use the `New-SqlAvailabilityReplica` cmdlet with the `-AsTemplate` parameter to create an in-memory availability-replica object for each of the two availability replicas that you plan to include in the availability group.<br /><br /> Then, create the availability group by using the `New-SqlAvailabilityGroup` cmdlet and referencing your availability-replica objects.|Execute on the server instance that is to host the initial primary replica.|  
|Join secondary replica to availability group|`Join-SqlAvailabilityGroup`|Execute on each server instance that is hosts a secondary replica.|  
|Prepare the secondary database|`Backup-SqlDatabase` and `Restore-SqlDatabase`|Create backups on the server instance that hosts the primary replica.<br /><br /> Restore backups on each server instance that hosts a secondary replica, using the `NoRecovery` restore parameter. If the file paths differ between the computers that host the primary replica and the target secondary replica, also use the `RelocateFile` restore parameter.|  
|Start data synchronization by joining each secondary database to availability group|`Add-SqlAvailabilityDatabase`|Execute on each server instance that hosts a secondary replica.|  
  
 **<sup>*</sup>**  To perform a given task, change directory (`cd`) to the indicated server instance or instances.  
  
###  <a name="PsProviderLinks"></a> To Set Up and Use the SQL Server PowerShell Provider  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
-   [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md)  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell to Create and Configure an Availability Group  
  
> [!NOTE]  
>  To view the syntax and an example of a given cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
1.  Change directory (`cd`) to the server instance that is to host the primary replica.  
  
2.  Create an in-memory availability-replica object for the primary replica.  
  
3.  Create an in-memory availability-replica object for each of the secondary replicas.  
  
4.  Create the availability group.  
  
    > [!NOTE]  
    >  The maximum length for an availability group name is 128 characters.  
  
5.  Join the new secondary replica to the availability group. For more information, see [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](join-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
6.  For each database in the availability group, create a secondary database by restoring recent backups of the primary database, using RESTORE WITH NORECOVERY.  
  
7.  Join every new secondary database to the availability group. For more information, see [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](join-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
8.  Optionally, use the Windows `dir` command to verify the contents of the new availability group.  
  
> [!NOTE]  
>  If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service accounts of the server instances run under different domain user accounts, on each server instance, create a login for the other server instance and grant this login CONNECT permission to the local database mirroring endpoint.  
  
##  <a name="ExampleConfigureGroup"></a> Example: Using PowerShell to Create an Availability Group  
 The following PowerShell example creates and configures a simple availability group named `MyAG` with two availability replicas and one availability database. The example:  
  
1.  Backs up `MyDatabase` and its transaction log.  
  
2.  Restores `MyDatabase` and its transaction log, using the `-NoRecovery` option.  
  
3.  Creates an in-memory representation of the primary replica, which will be hosted by the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (named `PrimaryComputer\Instance`).  
  
4.  Creates an in-memory representation of the secondary replica, which will be hosted by an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (named `SecondaryComputer\Instance`).  
  
5.  Creates an availability group named `MyAG`.  
  
6.  Joins the secondary replica to the availability group.  
  
7.  Joins the secondary database to the availability group.  
  
```  
# Backup my database and its log on the primary  
Backup-SqlDatabase `  
    -Database "MyDatabase" `  
    -BackupFile "\\share\backups\MyDatabase.bak" `  
    -ServerInstance "PrimaryComputer\Instance"  
  
Backup-SqlDatabase `  
    -Database "MyDatabase" `  
    -BackupFile "\\share\backups\MyDatabase.log" `  
    -ServerInstance "PrimaryComputer\Instance" `  
    -BackupAction Log   
  
# Restore the database and log on the secondary (using NO RECOVERY)  
Restore-SqlDatabase `  
    -Database "MyDatabase" `  
    -BackupFile "\\share\backups\MyDatabase.bak" `  
    -ServerInstance "SecondaryComputer\Instance" `  
    -NoRecovery  
  
Restore-SqlDatabase `  
    -Database "MyDatabase" `  
    -BackupFile "\\share\backups\MyDatabase.log" `  
    -ServerInstance "SecondaryComputer\Instance" `  
    -RestoreAction Log `  
    -NoRecovery  
  
# Create an in-memory representation of the primary replica.  
$primaryReplica = New-SqlAvailabilityReplica `  
    -Name "PrimaryComputer\Instance" `  
    -EndpointURL "TCP://PrimaryComputer.domain.com:5022" `  
    -AvailabilityMode "SynchronousCommit" `  
    -FailoverMode "Automatic" `  
    -Version 12 `  
    -AsTemplate  
  
# Create an in-memory representation of the secondary replica.  
$secondaryReplica = New-SqlAvailabilityReplica `  
    -Name "SecondaryComputer\Instance" `  
    -EndpointURL "TCP://SecondaryComputer.domain.com:5022" `  
    -AvailabilityMode "SynchronousCommit" `  
    -FailoverMode "Automatic" `  
    -Version 12 `  
    -AsTemplate  
  
# Create the availability group  
New-SqlAvailabilityGroup `  
    -Name "MyAG" `  
    -Path "SQLSERVER:\SQL\PrimaryComputer\Instance" `  
    -AvailabilityReplica @($primaryReplica,$secondaryReplica) `  
    -Database "MyDatabase"  
  
# Join the secondary replica to the availability group.  
Join-SqlAvailabilityGroup -Path "SQLSERVER:\SQL\SecondaryComputer\Instance" -Name "MyAG"  
  
# Join the secondary database to the availability group.  
Add-SqlAvailabilityDatabase -Path "SQLSERVER:\SQL\SecondaryComputer\Instance\AvailabilityGroups\MyAG" -Database "MyDatabase"  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To configure a server instance for AlwaysOn Availability Groups**  
  
-   [Enable and Disable AlwaysOn Availability Groups &#40;SQL Server&#41;](enable-and-disable-always-on-availability-groups-sql-server.md)  
  
-   [Create a Database Mirroring Endpoint for AlwaysOn Availability Groups &#40;SQL Server PowerShell&#41;](database-mirroring-always-on-availability-groups-powershell.md)  
  
 **To configure availability group and replica properties**  
  
-   [Change the Availability Mode of an Availability Replica &#40;SQL Server&#41;](change-the-availability-mode-of-an-availability-replica-sql-server.md)  
  
-   [Change the Failover Mode of an Availability Replica &#40;SQL Server&#41;](change-the-failover-mode-of-an-availability-replica-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [Configure the Flexible Failover Policy to Control Conditions for Automatic Failover (AlwaysOn Availability Groups)](configure-flexible-automatic-failover-policy.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
-   [Configure Backup on Availability Replicas &#40;SQL Server&#41;](configure-backup-on-availability-replicas-sql-server.md)  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
-   [Change the Session-Timeout Period for an Availability Replica &#40;SQL Server&#41;](change-the-session-timeout-period-for-an-availability-replica-sql-server.md)  
  
 **To complete availability group configuration**  
  
-   [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md)  
  
 **Alternative ways to create an availability group**  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Create an Availability Group &#40;Transact-SQL&#41;](create-an-availability-group-transact-sql.md)  
  
 **To troubleshoot AlwaysOn Availability Groups configuration**  
  
-   [Troubleshoot AlwaysOn Availability Groups Configuration (SQL Server)deleted](troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
-   [Troubleshoot a Failed Add-File Operation &#40;AlwaysOn Availability Groups&#41;](troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [AlwaysON - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](https://blogs.msdn.com/b/psssql/archive/2012/05/17/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases.aspx)  
  
     [Configuring AlwaysOn with SQL Server PowerShell](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/03/configuring-alwayson-with-sql-server-powershell.aspx)  
  
     [SQL Server AlwaysOn Team Blogs: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Videos:**  
  
     [Microsoft SQL Server Code-Named "Denali" AlwaysOn Series,Part 1: Introducing the Next Generation High Availability Solution](http://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI302)  
  
     [Microsoft SQL Server Code-Named "Denali" AlwaysOn Series,Part 2: Building a Mission-Critical High Availability Solution Using AlwaysOn](http://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI404)  
  
-   **Whitepapers:**  
  
     [Microsoft SQL Server AlwaysOn Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](http://sqlcat.com/)  
  
## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)  
  
  
