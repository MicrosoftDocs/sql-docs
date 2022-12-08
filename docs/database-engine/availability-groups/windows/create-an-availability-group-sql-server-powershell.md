---
title: "Create an availability group using PowerShell"
description: "Learn how to use PowerShell cmdlets to create and configure an Always On availability group by using PowerShell in SQL Server 2019 (15.x)."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seodec18
helpviewer_keywords:
  - "Availability Groups [SQL Server], creating"
---
# Create an Always On availability group using PowerShell
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to use PowerShell cmdlets to create and configure an Always On availability group by using PowerShell in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)]. An *availability group* defines a set of user databases that will fail over as a single unit and a set of failover partners, known as *availability replicas*, which support failover.  
  
> [!NOTE]  
> For an introduction to availability groups, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
> [!NOTE]  
> As an alternative to using PowerShell cmdlets, you can use the Create Availability Group wizard or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. For more information, see [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md) or [Create an Availability Group &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md).  

## Before You Begin
### <a name="PrerequisitesRestrictions"></a> Prerequisites, Restrictions, and Recommendations  

- Before creating an availability group, verify that the host instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] each resides on a different Windows Server Failover Clustering (WSFC) node of a single WSFC failover cluster. Also, verify that your server instances met the other server-instance prerequisites and that all of the other [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] requirements are meet and that you are aware of the recommendations. For more information, we strongly recommend that you read [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  

### <a name="Permissions"></a> Permissions  
 Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  

## <a name="PowerShellProcedure"></a> Using PowerShell to Create and Configure an Availability Group  
 
The following table lists the basic tasks involved in configuring an availability group and indicates those that are supported by PowerShell cmdlets. The [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] tasks must be performed in the sequence in which they are presented in the table.  
  
|Task|PowerShell Cmdlets (if Available) or Transact-SQL Statement|Where to Perform Task|  
|----------|--------------------------------------------------------------------|---------------------------------|  
|Create database mirroring endpoint (once per [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance)|**New-SqlHadrEndPoint**|Execute on each server instance that lacks database mirroring endpoint.<br /><br />To alter an existing database mirroring endpoint, use **Set-SqlHadrEndpoint**.|  
|Create availability group|First, use the **New-SqlAvailabilityReplica** cmdlet with the **-AsTemplate** parameter to create an in-memory availability-replica object for each of the two availability replicas that you plan to include in the availability group.<br /><br /> Then, create the availability group by using the **New-SqlAvailabilityGroup** cmdlet and referencing your availability-replica objects.|Execute on the server instance that is to host the initial primary replica.|  
|Join secondary replica to availability group|**Join-SqlAvailabilityGroup**|Execute on each server instance that is hosts a secondary replica.|  
|Prepare the secondary database|**Backup-SqlDatabase** and **Restore-SqlDatabase**|Create backups on the server instance that hosts the primary replica.<br /><br /> Restore backups on each server instance that hosts a secondary replica, using the **NoRecovery** restore parameter. If the file paths differ between the computers that host the primary replica and the target secondary replica, also use the **RelocateFile** restore parameter.|  
|Start data synchronization by joining each secondary database to availability group|**Add-SqlAvailabilityDatabase**|Execute on each server instance that hosts a secondary replica.|  
  
> [!NOTE]
> To perform the given tasks, change directory (**cd**) to the indicated server instance or instances.  

## Using PowerShell

Set up and use the [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md). 

> [!NOTE]  
> To view the syntax and an example of a given cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  

1. Change directory (**cd**) to the server instance that is to host the primary replica.  
  
1. Create an in-memory availability-replica object for the primary replica.  
  
1. Create an in-memory availability-replica object for each of the secondary replicas.  
  
1. Create the availability group.  
  
    > [!NOTE]  
    > The maximum length for an availability group name is 128 characters.  

1. Join the new secondary replica to the availability group, see [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
1. For each database in the availability group, create a secondary database by restoring recent backups of the primary database, using RESTORE WITH NORECOVERY.  
  
1. Join every new secondary database to the availability group, see [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
1. (optional) Use the Windows **dir** command to verify the contents of the new availability group.  
  
> [!NOTE]  
> If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service accounts of the server instances run under different domain user accounts, on each server instance, create a login for the other server instance and grant this login CONNECT permission to the local database mirroring endpoint.  

### <a name="ExampleConfigureGroup"></a> Example
The following PowerShell example creates and configures a simple availability group named `<myAvailabilityGroup>` with two availability replicas and one availability database. The example:  

1. Backs up `<myDatabase>` and its transaction log.  

1. Restores `<myDatabase>` and its transaction log, using the **-NoRecovery** option.  

1. Creates an in-memory representation of the primary replica, which will be hosted by the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (named `PrimaryComputer\Instance`).  

1. Creates an in-memory representation of the secondary replica, which will be hosted by an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (named `SecondaryComputer\Instance`).  

1. Creates an availability group named `<myAvailabilityGroup>`.  

1. Joins the secondary replica to the availability group.  

1. Joins the secondary database to the availability group.  

```powershell
# Backup my database and its log on the primary  
Backup-SqlDatabase `  
    -Database "<myDatabase>" `  
    -BackupFile "\\share\backups\<myDatabase>.bak" `  
    -ServerInstance "PrimaryComputer\Instance"  
  
Backup-SqlDatabase `  
    -Database "<myDatabase>" `  
    -BackupFile "\\share\backups\<myDatabase>.log" `  
    -ServerInstance "PrimaryComputer\Instance" `  
    -BackupAction Log   
  
# Restore the database and log on the secondary (using NO RECOVERY)  
Restore-SqlDatabase `  
    -Database "<myDatabase>" `  
    -BackupFile "\\share\backups\<myDatabase>.bak" `  
    -ServerInstance "SecondaryComputer\Instance" `  
    -NoRecovery  
  
Restore-SqlDatabase `  
    -Database "<myDatabase>" `  
    -BackupFile "\\share\backups\<myDatabase>.log" `  
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
    -Name "<myAvailabilityGroup>" `  
    -Path "SQLSERVER:\SQL\PrimaryComputer\Instance" `  
    -AvailabilityReplica @($primaryReplica,$secondaryReplica) `  
    -Database "<myDatabase>"  
  
# Join the secondary replica to the availability group.  
Join-SqlAvailabilityGroup -Path "SQLSERVER:\SQL\SecondaryComputer\Instance" -Name "<myAvailabilityGroup>"  
  
# Join the secondary database to the availability group.  
Add-SqlAvailabilityDatabase -Path "SQLSERVER:\SQL\SecondaryComputer\Instance\AvailabilityGroups\<myAvailabilityGroup>" -Database "<myDatabase>"  
```  
  
## <a name="RelatedTasks"></a> Related Tasks  
 **To configure a server instance for Always On Availability Groups**  
  
- [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)  
  
- [Create a Database Mirroring Endpoint for Always On Availability Groups &#40;SQL Server PowerShell&#41;](~/database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md)  
  
 **To configure availability group and replica properties**  
  
- [Change the Availability Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-availability-mode-of-an-availability-replica-sql-server.md)  
  
- [Change the Failover Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-failover-mode-of-an-availability-replica-sql-server.md)  
  
- [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
- [Configure the Flexible Failover Policy to Control Conditions for Automatic Failover &#40;Always On Availability Groups&#41;](~/database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md)  
  
- [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
- [Configure Backup on Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md)  
  
- [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
- [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
- [Change the Session-Timeout Period for an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-session-timeout-period-for-an-availability-replica-sql-server.md)  
  
 **To complete availability group configuration**  
  
- [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
- [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
- [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
- [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
 **Alternative ways to create an availability group**  
  
- [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md)  
  
- [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
- [Create an Availability Group &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md)  
  
 **To troubleshoot Always On Availability Groups configuration**  
  
- [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
- [Troubleshoot a Failed Add-File Operation &#40;Always On Availability Groups&#41;](~/database-engine/availability-groups/windows/troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
  
## <a name="RelatedContent"></a> Related Content  
  
- **Blogs:**  
  
     [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](/archive/blogs/psssql/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases)  
  
     [Configuring Always On with SQL Server PowerShell](/archive/blogs/sqlalwayson/configuring-alwayson-with-sql-server-powershell)  
  
     [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](/archive/blogs/psssql/)  
  
- **Whitepapers:**  
  
     [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))  
  
     [Microsoft White Papers for SQL Server 2012](https://social.technet.microsoft.com/wiki/contents/articles/13146.white-paper-gallery-for-sql-server.aspx#[Category]SQLServer2012)  
  
     [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)  
  
## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)