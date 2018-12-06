---
title: "Upgrade a SQL Server Failover Cluster Instance (Setup) | Microsoft Docs"
ms.custom: ""
ms.date: "01/22/2016"
ms.prod: sql
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
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster to a [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] failover cluster by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup UI or from a command prompt.  
  
 For local installations, you must run [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read permissions on the remote share.  
  
 Before upgrading, see [Upgrade a SQL Server Failover Cluster Instance](../../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md).  
  
##  <a name="UpgradeSteps"></a> To Upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster  
  
#### To upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster  
  
1.  From the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation media for the edition that matches the edition you are upgrading, double-click setup.exe in the root folder. You may be asked to install the prerequisites, if they are not previously installed.  
  
2.  After prerequisites are installed, the Installation Wizard starts the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Installation Center. To upgrade an existing instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], select your instance.  
  
3.  If [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup support files are required, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup installs them. If you are instructed to restart your computer, restart before you continue.  
  
4.  The System Configuration Checker runs a discovery operation on your computer. To continue, [!INCLUDE[clickOK](../../../includes/clickok-md.md)].  
  
5.  On the Product Key page, enter the PID key for the new version edition that matches the edition of the old product version. For example, to upgrade an Enterprise failover cluster, you must supply a PID key for [!INCLUDE[ssEnterprise](../../../includes/ssenterprise-md.md)]. Click **Next** to continue. Be aware that the PID key that you use for a failover cluster upgrade must be consistent across all failover cluster nodes in the same [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance.  
  
6.  On the License Terms page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE[msCoName](../../../includes/msconame-md.md)]. **Click Next to continue**. To end Setup, click **Cancel**.  
  
7.  On the Select Instance page, specify the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance to upgrade to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. **Click Next to continue**.  
  
8.  On the Feature Selection page, the features to upgrade are preselected. A description for each component group appears in the right pane after you select the feature name. Be aware that you cannot change the features to be upgraded, and you cannot add features during the upgrade operation. To add features to an upgraded instance of [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] after the upgrade operation is complete, see [Add Features to an Instance of SQL Server 2016 &#40;Setup&#41;](../../../database-engine/install-windows/add-features-to-an-instance-of-sql-server-2016-setup.md).  
  
     The prerequisites for the selected features are displayed on the right-hand pane. SQL Server Setup will install the prerequisite that are not already installed during the installation step described later in this procedure. To save time, you should pre-install these prerequisites on each node.  
  
9. On the Instance Configuration page, fields are automatically populated from the old instance. You can choose to specify the new InstanceID value.  
  
     **Instance ID** - By default, the instance name is used as the Instance ID. This is used to identify installation directories and registry keys for your instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. For a default instance, the instance name and instance ID would be MSSQLSERVER. To use a nondefault instance ID, select the **Instance ID** check box and provide a value. If you override the default value, you must specify the same Instance ID for the instance being upgraded on all the failover cluster nodes. The Instance ID for the upgraded instance must match across the nodes.  
  
     **Detected instances and features** - The grid shows instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that are on the computer where setup is running. **Click Next to continue**.  
  
10. The Disk Space Requirements page calculates the required disk space for the features that you specify, and compares requirements to the available disk space on the computer where Setup is running.  
  
11. On the Full-Text Search Upgrade page, specify the upgrade options for the databases being upgraded. For more information, see [Full-Text Search Upgrade Options](https://msdn.microsoft.com/library/16c9376b-5fbb-4495-a429-06a2493849c9).  
  
12. On the **Error Reporting** page, specify the information that you want to send to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] that will help improve [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. By default, options for error reporting is enabled.  
  
13. The System Configuration Checker runs one more set of rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features that you have specified, before the upgrade operation begins.  
  
14. The Cluster Upgrade Report page displays the list of nodes in the failover cluster instance and the instance version information for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] components on each node. It displays the database script status and replication script status. In addition, it also displays informational messages on what will occur when you click **Next**. Depending on the number of failover cluster nodes that have already been upgraded and total number of nodes, setup displays the failover behavior that happens when you click **Next**. It also warns about potential unnecessary downtime if you have not installed the prerequisites already.  
  
15. The Ready to Upgrade page displays a tree view of installation options that were specified during Setup. To continue, click **Upgrade**. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup will first install the required prerequisites for the selected features followed by the feature installation.  
  
16. During upgrade, the Progress page provides status so that you can monitor the upgrade progress on the current node as Setup continues.  
  
17. After the upgrade of the current node, the Cluster Upgrade Report page displays an upgrade status information for all the failover cluster nodes, features on each failover cluster node, and their version information. Confirm the version information that is displayed and continue with the upgrade of the remaining nodes. If the failover to upgraded nodes occurred, this is also apparent on the status page. You can also check in the Windows Cluster administrator tool to confirm.  
  
18. After upgrade, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
19. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information about Setup log files, see [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  
  
20. To complete the upgrade process, repeat these steps on all the other nodes on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster.  
  
## To upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Multi-Subnet Failover Cluster  
  
#### To upgrade to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover Cluster (Existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cluster is a non multi-subnet cluster).  
  
1.  Follow the steps above to upgrade your cluster to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
2.  Add a node on a different subnet using the AddNode Setup action and confirm the IP address resource dependency to OR on the **Cluster Network Configuration** page. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).  
  
#### To upgrade a multi-subnet cluster currently using Stretch V-Lan.  
  
1.  Follow the steps above to upgrade your cluster to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
2.  Change the network settings to move the remote node to a different subnet.  
  
3.  Using the Windows Failover Cluster management tool, add a new IP address for the new subnet set the IP address resource dependency to OR.  
  
## Next Steps  
 After you upgrade to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], complete the following tasks:  
  
-   [Complete the Database Engine Upgrade](../../../database-engine/install-windows/complete-the-database-engine-upgrade.md)  
  
-   [Change the Database Compatibility Mode and Use the Query Store](../../../database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store.md)  
  
-   [Take Advantage of New SQL Server 2016 Features](https://msdn.microsoft.com/library/d8879659-8efa-4442-bcbb-91272647ae16)  
  
## See Also  
 [Upgrade a SQL Server Failover Cluster Instance](../../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md)   
 [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)   
 [Add Features to an Instance of SQL Server 2016 &#40;Setup&#41;](../../../database-engine/install-windows/add-features-to-an-instance-of-sql-server-2016-setup.md)  
  
  
