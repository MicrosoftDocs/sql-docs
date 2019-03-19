---
title: "Upgrade a SQL Server Failover Cluster Instance (Setup) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading clusters"
  - "clusters [SQL Server], upgrading"
  - "failover clustering [SQL Server], creating clusters"
  - "clusters [SQL Server], creating"
  - "failover clustering [SQL Server], upgrading"
ms.assetid: ea8b7d66-e5a1-402f-9928-8f7310e84f5c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Upgrade a SQL Server Failover Cluster Instance (Setup)
  You can upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster to a [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] failover cluster by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Installation Wizard or a command prompt.  
  
 During the failover cluster upgrade, downtime is limited to failover time and the time that is required for upgrade scripts to run. If you follow the failover cluster rolling upgrade process, your downtime is minimal. Depending on whether you have all the prerequisites on the failover cluster nodes, you might incur additional downtime while you install these prerequisites. For more information about how to minimize the downtime during upgrade, see the [Best Practices Before Upgrading Failover Cluster](#BestPractices) section on this page.  
  
 For more information about how to upgrade, see [Supported Version and Edition Upgrades](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md) and [Upgrade to SQL Server 2014](../../../database-engine/install-windows/upgrade-sql-server.md).  
  
 For more information about sample syntax for command prompt usage, see [Install SQL Server 2014 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
## Prerequisites  
 Before you begin, review the following important information:  
  
-   [Before Installing Failover Clustering](../install/before-installing-failover-clustering.md)  
  
-   [Use Upgrade Advisor to Prepare for Upgrades](../../install/use-upgrade-advisor-to-prepare-for-upgrades.md).  
  
-   [Upgrade Database Engine](../../../database-engine/install-windows/upgrade-database-engine.md)  
  
-   Setup installs .NET Framework 4.0 on a clustered operating system. To minimize any possible downtime, consider installing .NET Framework 4.0 before you run Setup.  
  
-   To make sure that the Visual Studio component can be installed correctly, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] requires you to install an update. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup checks for the presence of this update and then requires you to download and install the update before you can continue with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation. To avoid the interruption during [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup, you can download and install the update before running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup as described below (or install all the updates for .NET 3.5 SP1 available on Windows Update):  
  
     If you install [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] on a computer with the Windows Server 2008 SP2 operating system, you can get the required update from [here](https://go.microsoft.com/fwlink/?LinkId=198093)  
  
     If you install [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] on a computer with the [!INCLUDE[win7](../../../includes/win7-md.md)] SP1 or [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)] SP1 operating system, this update in included.  
  
-   .NET Framework 3.5 SP1 is no longer installed by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup, but may be required while installing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on [!INCLUDE[firstref_longhorn](../../../includes/firstref-longhorn-md.md)]. For more information, see [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)][Release Notes](https://go.microsoft.com/fwlink/?LinkId=296445).  
  
-   For local installations, you must run [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read permissions on the remote share.  
  
-   To upgrade an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to a [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] failover cluster, the instance being upgraded must be a failover cluster.  
  
     To move a stand-alone instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to a [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] failover cluster, install a new [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] failover cluster, and then migrate user databases from the stand-alone instance by using the Copy Database Wizard. For more information, see [Use the Copy Database Wizard](../../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Rolling Upgrades  
 To upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster to [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)], you must run setup with upgrade action on each failover cluster node, one at a time, starting with the passive nodes. As you upgrade each node, it is left out of the possible owners of the failover cluster. If there is an unexpected failover, the upgraded nodes do not participate in the failover until cluster resource group ownership is moved to an upgraded node by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup.  
  
 By default, Setup automatically determines when to fail over to an upgraded node. This depends on the total number of nodes in the failover cluster instance and the number of nodes that have already been upgraded. When half of the nodes or more have already been upgraded, Setup causes a failover to an upgraded node when you perform upgrade on the next node. Upon failover to an upgraded node, the cluster group is moved to an upgraded node. All the upgraded nodes are put in the possible owners list and all the nodes that are not yet upgraded are removed from the possible owners list. As you upgrade each remaining node, it is added to the possible owners of the failover cluster.  
  
 This process results in downtime limited to one failover time and database upgrade script execution time during the whole failover cluster upgrade.  
  
 To control the failover behavior of cluster nodes during the upgrade process, run the upgrade operation at the command prompt and use the /FAILOVERCLUSTERROLLOWNERSHIP parameter. For more information, see [Install SQL Server 2014 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
 **Note** If there is a single-node failover cluster, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup takes the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource group offline.  
  
## Considerations when Upgrading from [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]  
 If you specified domain groups for the cluster security policy, you cannot specify service SID on [!INCLUDE[nextref_longhorn](../../../includes/nextref-longhorn-md.md)]. If you want to use the service SID, you need to perform a side by side upgrade.  
  
 When you select [!INCLUDE[ssDE](../../../includes/ssde-md.md)] for upgrade, full-text search is included in the setup regardless of whether it was installed in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
 If full-text search was enabled in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], Setup rebuilds the full-text search catalog regardless of the options available to you.  
  
## Upgrading to a [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] Multi-Subnet Failover Cluster  
 There are two possible scenarios for upgrades:  
  
1.  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster is currently configured on a single subnet: You must first upgrade the existing cluster to [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] by launching Setup and following the upgrade process. After completing the upgrade of the existing failover cluster, add a node that is on a different subnet using the AddNode functionality. Confirm the IP address resource dependency change to OR in the cluster network configuration page. You now have a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover cluster.  
  
2.  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster is currently configured on multiple subnets using the stretch V-LAN technology: You must first upgrade the existing cluster to [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)]. Because stretch V-LAN technology configures a single subnet, the network configuration must to be changed to multiple subnets, and change the IP address resource dependency using the Windows Failover Cluster administration tool and change the IP dependency to OR.  
  
###  <a name="BestPractices"></a> Best Practices Before Upgrading a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster  
 To eliminate unexpected downtime caused by a restart, preinstall the no-reboot package for .NET Framework 4.0 on all the failover cluster nodes before you run the upgrade on the cluster nodes. We recommend the following steps to preinstall the prerequisites:  
  
-   Install the no-reboot package for .NET Framework 4.0, and upgrade only the shared components starting with the passive nodes. This installs [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] 4.0, Windows Installer 4.5, and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] support files.  
  
-   Restart one or more times, as required.  
  
-   Fail over to an upgraded node.  
  
-   Upgrade the shared components on the last remaining node.  
  
 After all the shared components are upgraded and prerequisites are installed, start the failover cluster upgrade process. You have to run upgrade on each failover cluster node, starting with the passive nodes first and making your way toward the node that owns the cluster resource group.  
  
-   You cannot add features to an existing failover cluster.  
  
-   Changing the edition of the failover cluster is limited to certain scenarios. For more information, see [Supported Version and Edition Upgrades](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md).  
  
##  <a name="UpgradeSteps"></a> To Upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster  
  
#### To upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster  
  
1.  Insert the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation media, and from the root folder, double-click Setup.exe. To install from a network share, move to the root folder on the share, and then double-click Setup.exe. You may be asked to install the prerequisites, if they are not previously installed.  
  
2.  > [!IMPORTANT]  
    >  For more information about steps 3 and 4, see the [Best Practices Before Upgrading Failover Cluster](#BestPractices) section.  
  
3.  After prerequisites are installed, the Installation Wizard starts the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Installation Center. To upgrade an existing instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], click **Upgrade from [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)], or [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)].**  
  
4.  If Setup support files are required, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup installs them. If you are instructed to restart your computer, restart before you continue.  
  
5.  The System Configuration Checker runs a discovery operation on your computer. To continue, [!INCLUDE[clickOK](../../../includes/clickok-md.md)].  
  
6.  On the Product Key page, enter the PID key for the new version edition that matches the edition of the old product version. For example, to upgrade an Enterprise failover cluster, you must supply a PID key for [!INCLUDE[ssEnterprise](../../../includes/ssenterprise-md.md)]. Click **Next** to continue. Be aware that the PID key that you use for a failover cluster upgrade must be consistent across all failover cluster nodes in the same [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. For more information, see [Editions and Components of SQL Server 2014](../../editions-and-components-of-sql-server-2016.md) and [Supported Version and Edition Upgrades](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md).  
  
7.  On the License Terms page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE[msCoName](../../../includes/msconame-md.md)]. **Click Next to continue**. To end Setup, click **Cancel**.  
  
8.  On the Select Instance page, specify the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance to upgrade to [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)]. **Click Next to continue**.  
  
9. On the Feature Selection page, the features to upgrade are preselected. A description for each component group appears in the right pane after you select the feature name. Be aware that you cannot change the features to be upgraded, and you cannot add features during the upgrade operation. To add features to an upgraded instance of [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] after the upgrade operation is complete, see [Add Features to an Instance of SQL Server 2014 &#40;Setup&#41;](../../../database-engine/install-windows/add-features-to-an-instance-of-sql-server-setup.md).  
  
     The prerequisites for the selected features are displayed on the right-hand pane. SQL Server Setup will install the prerequisite that are not already installed during the installation step described later in this procedure.  
  
10. On the Instance Configuration page, fields are automatically populated from the old instance. You can choose to specify the new InstanceID value.  
  
     **Instance ID** - By default, the instance name is used as the Instance ID. This is used to identify installation directories and registry keys for your instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. For a default instance, the instance name and instance ID would be MSSQLSERVER. To use a nondefault instance ID, select the **Instance ID** check box and provide a value. If you override the default value, you must specify the same Instance ID for the instance being upgraded on all the failover cluster nodes. The Instance ID for the upgraded instance must match across the nodes.  
  
     **Detected instances and features** - The grid shows instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that are on the computer where Setup is running. **Click Next to continue**.  
  
11. The Disk Space Requirements page calculates the required disk space for the features that you specify, and compares requirements to the available disk space on the computer where Setup is running.  
  
12. On the Full-Text Search Upgrade page, specify the upgrade options for the databases being upgraded. For more information, see [Full-Text Search Upgrade Options](../../install/full-text-search-upgrade-options.md).  
  
13. On the **Error Reporting** page, specify the information that you want to send to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] that will help improve [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. By default, options for error reporting is enabled.  
  
14. The System Configuration Checker runs one more set of rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features that you have specified, before the upgrade operation begins.  
  
15. The Cluster Upgrade Report page displays the list of nodes in the failover cluster instance and the instance version information for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] components on each node. It displays the database script status and replication script status. In addition, it also displays informational messages on what will occur when you click **Next**. Depending on the number of failover cluster nodes that have already been upgraded and total number of nodes, Setup displays the failover behavior that happens when you click **Next**. It also warns about potential unnecessary downtime if you have not installed the prerequisites already.  
  
16. The Ready to Upgrade page displays a tree view of installation options that were specified during Setup. To continue, click **Upgrade**. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup will first install the required prerequisites for the selected features followed by the feature installation.  
  
17. During upgrade, the Progress page provides status so that you can monitor the upgrade progress on the current node as Setup continues.  
  
18. After the upgrade of the current node, the Cluster Upgrade Report page displays an upgrade status information for all the failover cluster nodes, features on each failover cluster node, and their version information. Confirm the version information that is displayed and continue with the upgrade of the remaining nodes. If the failover to upgraded nodes occurred, this is also apparent on the status page. You can also check in the Windows Cluster administrator tool to confirm.  
  
19. After upgrade, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
20. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information about Setup log files, see [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  
  
21. To complete the upgrade process, repeat steps 1 to 21 on all the other nodes on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster.  
  
## To upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Multi-Subnet Failover Cluster  
  
#### To upgrade to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover Cluster (Existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cluster is a non multi-subnet cluster).  
  
1.  Follow the steps 1 through 24 described in the [To upgrade a SQL Server failover cluster](#UpgradeSteps) section above to upgrade your cluster to [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)].  
  
2.  Add a node on a different subnet using the AddNode Setup action and confirm the IP address resource dependency to OR on the **Cluster Network Configuration** page. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).  
  
#### To upgrade a multi-subnet cluster currently using Stretch V-Lan.  
  
1.  Follow the steps 1 through 24 described in the [To upgrade a SQL Server failover cluster](#UpgradeSteps) section above to upgrade your cluster to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
2.  Change the network settings to move the remote node to a different subnet.  
  
3.  Using the Windows Failover Cluster management tool, add a new IP address for the new subnet set the IP address resource dependency to OR.  
  
## Next Steps  
 After you upgrade to [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)], complete the following tasks:  
  
-   Register your servers  
  
     Upgrade removes registry settings for the previous [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. After you upgrade, you must reregister your servers.  
  
-   Update statistics  
  
     To help optimize query performance, we recommend that you update statistics on all databases following upgrade. Use the **sp_updatestats** stored procedure to update statistics in user-defined tables in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases.  
  
-   Configure your new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation  
  
     To reduce the attackable surface area of a system, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] selectively installs and enables key services and features. For more information about surface area configuration, see the readme file for this release.  
  
## See Also  
 [Install SQL Server 2014 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)   
 [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)  
  
  
