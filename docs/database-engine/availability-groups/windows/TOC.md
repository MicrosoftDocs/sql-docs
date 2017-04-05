# [Overview](overview-of-always-on-availability-groups-sql-server.md)  
## [Technology and capabilities](always-on-availability-groups-sql-server.md)  
## [Interoperability](always-on-availability-groups-interoperability-sql-server.md)  

# Quickstart
## [Getting started](getting-started-with-always-on-availability-groups-sql-server.md)  
## [Prerequisites, restrictions, and recommendations](prereqs-restrictions-recommendations-always-on-availability.md)  
## [Creation and configuration](creation-and-configuration-of-availability-groups-sql-server.md)  
### [Wizard](use-the-availability-group-wizard-sql-server-management-studio.md)  
### [Dialog](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
### [Transact-SQL](create-an-availability-group-transact-sql.md)  
### [PowerShell](create-an-availability-group-sql-server-powershell.md)  
### [Azure virtual machines>](http://docs.microsoft.com/azure/virtual-machines/windows/sql/virtual-machines-windows-portal-sql-availability-group-overview)

## Manage
### SSMS
### [PowerShell Cmdlets](overview-of-powershell-cmdlets-for-always-on-availability-groups-sql-server.md)  
### [Transact-SQL](transact-sql-statements-for-always-on-availability-groups.md)  

## Connect
### To primary replica
### To secondary replica

## [Listener](create-or-configure-an-availability-group-listener-sql-server.md)  
### SSMS
### PowerShell
### Azure virtual machines

# Samples
## SSMS
## Transact-SQL
## PowerShell

# Tutorials

# Concepts
## [Failover clustering and availability groups](failover-clustering-and-always-on-availability-groups-sql-server.md)  
## [Client connectivity](always-on-client-connectivity-sql-server.md)  
## [Basic availability groups](basic-availability-groups-always-on-availability-groups.md)  
## [Distributed availability groups](distributed-availability-groups-always-on-availability-groups.md)  
## [Availability modes](availability-modes-always-on-availability-groups.md)  
## [Failover and failover modes](failover-and-failover-modes-always-on-availability-groups.md)  
## [Flexible failover policy for automatic failover](flexible-automatic-failover-policy-availability-group.md)  
## [Possible failures during sessions between replicas](possible-failures-during-sessions-between-availability-replicas-sql-server.md)  
## [Database health detection](sql-server-always-on-database-health-detection-failover-option.md)  
## [Backup on secondary](active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md)  
## [Readable secondary](active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)  
## [Client connection](about-client-connection-access-to-availability-replicas-sql-server.md)  
## [Listeners, clients, and failover](listeners-client-connectivity-application-failover.md)  
## [Logins and jobs](logins-and-jobs-for-availability-group-databases.md)  
## [Automatically seeding](automatically-initialize-always-on-availability-group.md)  
## [Availability group policies](always-on-policies-for-operational-issues-always-on-availability.md)  
## Interoperability with other features
### [Contained databases](contained-databases-with-always-on-availability-groups-sql-server.md)  
### [Distributed transactions](transactions-always-on-availability-and-database-mirroring.md)  
#### [Cluster DTC](cluster-dtc-for-sql-server-2016-availability-groups.md)  
### [Database snapshots](database-snapshots-with-always-on-availability-groups-sql-server.md)  
### [Encrypted databases](encrypted-databases-with-always-on-availability-groups-sql-server.md)  
### [FILESTREAM and FileTable](filestream-and-filetable-with-always-on-availability-groups-sql-server.md)  
### [Migrating from log shipping](prereqs-migrating-log-shipping-to-always-on-availability-groups.md)  
### [Remote blob store (RBS)](remote-blob-store-rbs-and-always-on-availability-groups-sql-server.md)  
### [Configure replication](configure-replication-for-always-on-availability-groups-sql-server.md)  
#### [Publication database](maintaining-an-always-on-publication-database-sql-server.md)  
#### [Subscribers](replication-subscribers-and-always-on-availability-groups-sql-server.md)  
### [Change tracking and change data capture](replicate-track-change-data-capture-always-on-availability.md)  
### [Failover cluster instances](analysis-services-with-always-on-availability-groups.md)  
### [Reporting Services](reporting-services-with-always-on-availability-groups-sql-server.md)  
### [Service Broker](service-broker-with-always-on-availability-groups-sql-server.md)  

# [How-to guides](administration-of-an-availability-group-sql-server.md)  
## [Configuration of a Server Instance for Always On Availability Groups](configuration-of-a-server-instance-for-always-on-availability-groups-sql-server.md)  
### [Enable and Disable Always On Availability Groups](enable-and-disable-always-on-availability-groups-sql-server.md)  
### [Create a Database Mirroring Endpoint for Always On Availability Groups (SQL Server PowerShell)](database-mirroring-always-on-availability-groups-powershell.md)  
### [Create Clustered DTC for an Always On Availability Group](create-clustered-dtc-for-an-always-on-availability-group.md)  
## Configure Availability Group 
### [Change the Availability Mode of an Availability Replica](change-the-availability-mode-of-an-availability-replica-sql-server.md)  
### [Change the Failover Mode of an Availability Replica](change-the-failover-mode-of-an-availability-replica-sql-server.md)  
### [Configure the Flexible Failover Policy to Control Conditions for Automatic Failover](configure-flexible-automatic-failover-policy.md)  
### [Configure Backup on Availability Replicas](configure-backup-on-availability-replicas-sql-server.md)  
### [Configure Read-Only Access on an Availability Replica](configure-read-only-access-on-an-availability-replica-sql-server.md)  
### [Configure Read-Only Routing for an Availability Group](configure-read-only-routing-for-an-availability-group-sql-server.md)  
### [Remove Availability Group Listener](remove-an-availability-group-listener-sql-server.md)  
### [Join a Secondary Database to an Availability Group](join-a-secondary-database-to-an-availability-group-sql-server.md)  
### [Start Data Movement on an Always On Secondary Database](start-data-movement-on-an-always-on-secondary-database-sql-server.md)  
### [Manually Prepare a Secondary Database for an Availability Group](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
### [Specify the Endpoint URL When Adding or Modifying an Availability Replica](specify-endpoint-url-adding-or-modifying-availability-replica.md)  
### [Join a Secondary Replica to an Availability Group](join-a-secondary-replica-to-an-availability-group-sql-server.md)  
### [Tune compression for availability group](tune-compression-for-availability-group.md)  
## [Troubleshoot Always On Availability Groups Configuration](troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
## Operation
### [Planned Manual Failover](perform-a-planned-manual-failover-of-an-availability-group-sql-server.md)  
### [Forced Manual Failover](perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)  
### [Fail Over - Wizard](use-the-fail-over-availability-group-wizard-sql-server-management-studio.md)  
### Add Database
#### [Dialog](availability-group-add-a-database.md)  
#### [Wizard](availability-group-add-database-to-group-wizard.md)  
### [Suspend an Availability Database](suspend-an-availability-database-sql-server.md)  
### [Resume an Availability Database](resume-an-availability-database-sql-server.md)  
### [Remove a Secondary Database from an Availability Group (SQL Server)](remove-a-secondary-database-from-an-availability-group-sql-server.md)  
### [Remove a Primary Database from an Availability Group](remove-a-primary-database-from-an-availability-group-sql-server.md)  
### [Add a Secondary Replica to an Availability Group](add-a-secondary-replica-to-an-availability-group-sql-server.md)  
### [Use the Add Replica to Availability Group Wizard](use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)  
### [Use the Add Azure Replica Wizard](use-the-add-azure-replica-wizard-sql-server.md)  
### [Remove a Secondary Replica from an Availability Group](remove-a-secondary-replica-from-an-availability-group-sql-server.md)  
### [Change the Session-Timeout Period for an Availability Replica](change-the-session-timeout-period-for-an-availability-replica-sql-server.md)  
### [Remove an Availability Group](remove-an-availability-group-sql-server.md)  
### [Take an Availability Group Offline](take-an-availability-group-offline-sql-server.md)  
### [Change the HADR Cluster Context of Server Instance](change-the-hadr-cluster-context-of-server-instance-sql-server.md)  
### [Troubleshoot a Failed Add-File Operation (Always On Availability Groups)](troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
### [Use Always On Policies to View the Health of an Availability Group](use-always-on-policies-to-view-the-health-of-an-availability-group-sql-server.md)  
### [Upgrading Always On Availability Group Replica Instances](upgrading-always-on-availability-group-replica-instances.md)  
## [Monitoring of Availability Groups](monitoring-of-availability-groups-sql-server.md)  
### [Monitor Availability Groups (Transact-SQL)](monitor-availability-groups-transact-sql.md)  
### [Use the Object Explorer Details to Monitor Availability Groups (SQL Server Management Studio)](use-object-explorer-details-to-monitor-availability-groups.md)  
### [View Availability Group Properties](view-availability-group-properties-sql-server.md)  
#### [Availability Group Properties: New Availability Group (General Page)](availability-group-properties-new-availability-group-general-page.md)  
#### [Availability Group Properties: New Availability Group (Backup Preferences Page)](availability-group-properties-new-availability-group-backup-preferences-page.md)  
### [View Availability Replica Properties](view-availability-replica-properties-sql-server.md)  
### [View Availability Group Listener Properties](view-availability-group-listener-properties-sql-server.md)  


# References

# Resources
## Dialogs and Wizards
### [Add IP Address Dialog Box](add-ip-address-dialog-box-sql-server-management-studio.md)  
### [Specify Availability Group Name Page (New Availability Group Wizard/Add Database Wizard)](specify-availability-group-name-page.md)  
### [Select Databases Page (New Availability Group Wizard and Add Database Wizard)](select-databases-page-new-availability-group-wizard-and-add-database-wizard.md)  
### [Specify Replicas Page (New Availability Group Wizard: Add Replica Wizard)](specify-replicas-page-new-availability-group-wizard-add-replica-wizard.md)  
### [Enter Passwords Page (Add Replica Wizard)](enter-passwords-page-add-replica-wizard.md)  
### [Select Initial Data Synchronization Page (Always On Availability Group Wizards)](select-initial-data-synchronization-page-always-on-availability-group-wizards.md)  
### [Validation Page (Always On Availability Group Wizards)](validation-page-always-on-availability-group-wizards.md)  
### [Summary Page (Always On Availability Group Wizards)](summary-page-always-on-availability-group-wizards.md)  
### [Progress Page (Always On Availability Group Wizards)](progress-page-always-on-availability-group-wizards.md)  
### [Results Page (Always On Availability Group Wizards)](results-page-always-on-availability-group-wizards.md)  
### [Availability Group - Connect Existing Secondary Replicas Page](connect-to-existing-secondary-replicas-page.md)  
## [Use the Always On Dashboard (SQL Server Management Studio)](use-the-always-on-dashboard-sql-server-management-studio.md)  
### [Options (SQL Server Always On, Dashboard Page)](options-sql-server-always-on-dashboard-page.md)  
### [Policy Evaluation Result (Always On)](policy-evaluation-result-always-on.md)  
### [Availability Replica Properties (General Page)](availability-replica-properties-general-page.md)  
## Policies
### [WSFC cluster service is offline](wsfc-cluster-service-is-offline.md)  
### [Availability group is offline](availability-group-is-offline.md)  
### [Availability group is not ready for automatic failover](availability-group-is-not-ready-for-automatic-failover.md)  
### [Some availability replicas are not synchronizing data](some-availability-replicas-are-not-synchronizing-data.md)  
### [Some synchronous replicas are not synchronized](some-synchronous-replicas-are-not-synchronized.md)  
### [Some availability replicas do not have a healthy role](some-availability-replicas-do-not-have-a-healthy-role.md)  
### [Some availability replicas are disconnected](some-availability-replicas-are-disconnected.md)  
### [Availability replica does not have a healthy role](availability-replica-does-not-have-a-healthy-role.md)  
### [Availability replica is disconnected](availability-replica-is-disconnected.md)  
### [Data synchronization state of availability database is not healthy](data-synchronization-state-of-availability-database-is-not-healthy.md)  
### [Availability replica is not joined](availability-replica-is-not-joined.md)  
### [Availability database is suspended](availability-database-is-suspended.md)  
### [Secondary database is not joined](secondary-database-is-not-joined.md)  
### [Data synchronization state of some availability database is not healthy](data-synchronization-state-of-some-availability-database-is-not-healthy.md)  
