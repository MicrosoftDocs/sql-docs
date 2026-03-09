---
title: "Upgrade a Failover Cluster Instance"
description: "Steps to upgrade a SQL Server Always On failover cluster instance using the installation media. Learn about rolling upgrades and upgrading a multi-subnet cluster."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: failover-cluster-instance
ms.topic: how-to
helpviewer_keywords:
  - "upgrading failover cluster instances"
  - "clusters [SQL Server], upgrading"
  - "failover clustering [SQL Server], upgrading"
---

# Upgrade a failover cluster instance

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] supports upgrading a failover cluster to a new version of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], to a new [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service pack or cumulative update, or when installing to a new Windows service pack or cumulative update separately on all failover cluster nodes, with downtime limited to a single manual failover (or two manual failovers if failing back to the original primary).

Upgrading the Windows Server operating system of a node containing a failover cluster instance isn't supported for operating systems before [!INCLUDE [winserver2012r2-md](../../../includes/winserver2012r2-md.md)]. To upgrade a Windows Server failover cluster node running on [!INCLUDE [winserver2012r2-md](../../../includes/winserver2012r2-md.md)] or later versions, see [Perform a rolling upgrade or update](#perform-a-rolling-upgrade-or-update).

Support details are as follows:

- [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] upgrade is supported both through the user interface and from the command prompt. You can run upgrade from the command prompt on each failover cluster node, or by using the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] setup UI to upgrade each cluster node. For more information, see:

- Install a new [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance
- [Install and configure SQL Server on Windows from the command prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)

- The following scenarios aren't supported as part of a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] upgrade:

  - You can't upgrade from a stand-alone instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to a failover cluster instance.

  - You can't add features to a failover cluster instance. For example, you can't add the [!INCLUDE [ssDE](../../../includes/ssde-md.md)] to an existing [!INCLUDE [ssASnoversion](../../../includes/ssasnoversion-md.md)]-only failover cluster instance.

  - You can't downgrade a failover cluster instance to a stand-alone instance on any node of the Windows Server failover cluster.

  - Changing the edition of the failover cluster instance is limited to certain scenarios. For more information, see [Supported version & edition upgrades (SQL Server 2016)](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md).

- During the failover cluster instance upgrade, downtime is limited to failover time and the time that is required for upgrade scripts to run. If you follow this failover cluster instance rolling upgrade process, and meet all prerequisites on all nodes before you begin the upgrade process, your downtime is minimal. Upgrading [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] when memory-optimized tables are in use takes some extra time. For more information, see [Plan and test the Database Engine upgrade plan](../../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md).

## Prerequisites

Before you begin, review the following important information:

- [Supported version & edition upgrades (SQL Server 2016)](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md): Verify that you can upgrade to your desired version of [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] from your version of the Windows operating system and version of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. For example, you can't upgrade directly from a SQL Server 2005 failover clustering instance to [!INCLUDE [sssql14-md](../../../includes/sssql14-md.md)] or upgrade a failover cluster instance running on [!INCLUDE [winserver2003-md](../../../includes/winserver2003-md.md)].

- [Choose a Database Engine upgrade method](../../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md): Select the appropriate upgrade method and steps based on your review of supported version and edition upgrades, and also based on other components installed in your environment, to upgrade components in the correct order.

- [Plan and test the Database Engine upgrade plan](../../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md): Review the release notes and known upgrade issues, the pre-upgrade checklist, and develop and test the upgrade plan.

- [SQL Server 2016 and 2017: Hardware and software requirements](../../install/hardware-and-software-requirements-for-installing-sql-server.md): Review the software requirements for installing [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)]. If extra software is required, install it on each node before you begin the upgrade process to minimize any downtime.

## Perform a rolling upgrade or update

To upgrade a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance, use [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] setup to upgrade each node participating in the failover cluster instance, one at a time, starting with the passive nodes. As you upgrade each node, that node is left out of the possible owners of the failover cluster instance. If there's an unexpected failover, the upgraded nodes don't participate in the failover until Windows Server failover cluster role ownership is moved to an upgraded node by setup.

By default, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] setup automatically determines when to fail over to an upgraded node. This depends on the total number of nodes in the failover cluster instance and the number of nodes that are already upgraded. When half of the nodes or more are already upgraded, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] setup causes a failover to an upgraded node when you perform upgrade on the next node. Upon failover to an upgraded node, the cluster group is moved to an upgraded node. All the upgraded nodes are put in the possible owners list and all the nodes that aren't yet upgraded are removed from the possible owners list. As you upgrade each remaining node, it's added to the possible owners of the failover cluster instance.

This process results in downtime limited to one failover time and database upgrade script execution time during the whole failover cluster upgrade.

To control the failover behavior of cluster nodes during the upgrade process, run the upgrade operation at the command prompt and use the /FAILOVERCLUSTERROLLOWNERSHIP parameter. For more information, see [Install and configure SQL Server on Windows from the command prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).

For more information about upgrading a Windows Server cluster, see [Cluster OS Rolling Upgrade](/windows-server/failover-clustering/cluster-operating-system-rolling-upgrade).

## Upgrade with installation media

1. From the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] installation media for the edition that matches the edition you're upgrading, double-click setup.exe in the root folder. You might be asked to install the prerequisites, if they aren't previously installed.

1. After prerequisites are installed, the Installation Wizard starts the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Installation Center. 

1. On the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Installation Center, select **Installation** and then select the **Upgrade from a previous version of SQL Server** option to start the upgrade process.

1. If [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] setup support files are required, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] setup installs them. If you're instructed to restart your computer, restart before you continue.

1. The System Configuration Checker runs a discovery operation on your computer. To continue, select **OK**.

1. On the Product Key page, enter the PID key for the new version edition that matches the edition of the old product version. For example, to upgrade an Enterprise failover cluster, you must supply a PID key for [!INCLUDE [ssEnterprise](../../../includes/ssenterprise-md.md)]. Select **Next** to continue. The PID key that you use for a failover cluster upgrade must be consistent across all failover cluster nodes in the same [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance.

1. On the License Terms page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE [msCoName](../../../includes/msconame-md.md)]. **Click Next to continue**. To end Setup, select **Cancel**.

1. On the Select Instance page, specify the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance to upgrade. Select **Next** to continue.

1. On the Feature Selection page, the features to upgrade are preselected. A description for each component group appears in the right pane after you select the feature name. You can't change the features to be upgraded, and you can't add features during the upgrade operation. To add features to an upgraded instance of [!INCLUDE [ssSQL14](../../../includes/sssql14-md.md)] after the upgrade operation is complete, see [Add Features to an Instance of SQL Server (Setup)](../../../database-engine/install-windows/add-features-to-an-instance-of-sql-server-setup.md).

     The prerequisites for the selected features are displayed on the right-hand pane. SQL Server Setup installs the prerequisites that aren't already installed during the installation step described later in this procedure. To save time, you should preinstall these prerequisites on each node.

1. On the Instance Configuration page, fields are automatically populated from the old instance. You can choose to specify the new InstanceID value.

     **Instance ID** - By default, the instance name is used as the Instance ID. This is used to identify installation directories and registry keys for your instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. For a default instance, the instance name and instance ID would be MSSQLSERVER. To use a nondefault instance ID, select the **Instance ID** check box and provide a value. If you override the default value, you must specify the same Instance ID for the instance being upgraded on all the failover cluster nodes. The Instance ID for the upgraded instance must match across the nodes.

     **Detected instances and features** - The grid shows instances of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that are on the computer where setup is running. **Click Next to continue**.

1. The Disk Space Requirements page calculates the required disk space for the features that you specify, and compares requirements to the available disk space on the computer where Setup is running.

1. On the Full-Text Search Upgrade page, specify the upgrade options for the databases being upgraded. For more information, see [Upgrade Full-Text Search](../../../relational-databases/search/upgrade-full-text-search.md).

1. On the **Error Reporting** page, specify the information that you want to send to [!INCLUDE [msCoName](../../../includes/msconame-md.md)] that help improve [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. By default, the option for error reporting is enabled.

1. The System Configuration Checker runs one more set of rules to validate your computer configuration with the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] features that you specified, before the upgrade operation begins.

1. The Cluster Upgrade Report page displays the list of nodes in the failover cluster instance and the instance version information for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] components on each node. It displays the database script status and replication script status. In addition, it also displays informational messages on what will occur when you select **Next**. Depending on the number of failover cluster nodes that are already upgraded and total number of nodes, setup displays the failover behavior that happens when you select **Next**. It also warns about potential unnecessary downtime if you haven't installed the prerequisites already.

1. The Ready to Upgrade page displays a tree view of installation options that were specified during Setup. To continue, select **Upgrade**. [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Setup first installs the required prerequisites for the selected features followed by the feature installation.

1. During upgrade, the Progress page provides status so that you can monitor the upgrade progress on the current node as Setup continues.

1. After the upgrade of the current node, the Cluster Upgrade Report page displays an upgrade status information for all the failover cluster nodes, features on each failover cluster node, and their version information. Confirm the version information that is displayed and continue with the upgrade of the remaining nodes. If the failover to upgraded nodes occurred, this is also apparent on the status page. You can also check in the Windows Cluster administrator tool to confirm.

1. After upgrade, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] installation process, select **Close**.

1. If you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you finish with Setup. For more information about Setup log files, see [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).

1. To complete the upgrade process, repeat these steps on all the other nodes of the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance.

## Upgrade a multi-subnet failover cluster instance

Follow these steps to upgrade your Always On failover cluster instance in a multi-subnet environment.

### Upgrade to a SQL Server multi-subnet failover cluster instance (existing SQL Server cluster is a non-multi-subnet cluster)

1. Follow the previous steps to upgrade your failover cluster instance.

1. To add a new node on a different subnet using the AddNode Setup action and confirm the IP address resource dependency to OR on the **Cluster Network Configuration** page. For more information, see [Add or remove nodes in a failover cluster instance (Setup)](../install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).

### Upgrade a multi-subnet failover cluster instance currently using Stretch VLAN to use multi-subnet

1. Follow the previous steps to upgrade your cluster.

1. Change the network settings to move the remote node to a different subnet.

1. Using Failover Cluster Manager or PowerShell, add a new IP address for the new subnet to set the IP address resource dependency to OR.

## Related content

- [Complete the Database Engine Upgrade](../../../database-engine/install-windows/complete-the-database-engine-upgrade.md)
- [Change the database compatibility level and use the Query Store](../../../database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store.md)
- [What's new in SQL Server 2017](../../what-s-new-in-sql-server-2017.md)
