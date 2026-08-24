---
title: Failover Cluster Troubleshooting
description: Learn about troubleshooting failover clusters, including recovering from a failure, resolving common problems, and using extended stored procedures/COM objects.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 10/20/2025
ms.service: sql
ms.subservice: failover-cluster-instance
ms.topic: how-to
helpviewer_keywords:
  - "troubleshooting, failover clustering"
  - "failover clustering, troubleshooting"
  - "cluster troubleshooting"
---
# Failover cluster troubleshooting

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article provides information about the following issues:

- [Basic troubleshooting steps](#basic-troubleshooting-steps)
- [Recover from a failover cluster failure](#recover-from-failover-cluster-failure)
- [Resolve the most common failover clustering problems](#resolve-common-problems)
- [Use extended stored procedures and COM objects](#use-extended-stored-procedures-and-com-objects)

## Basic troubleshooting steps

The first diagnostic step is to run a fresh cluster validation check. For details on validation, see [Create a Failover Cluster: Validate the Configuration](/windows-server/failover-clustering/create-failover-cluster#validate-the-configuration). This can be completed without any interruption of service as it doesn't affect any online cluster resources.

Validation can be run at any time once the Failover Clustering feature has been installed, including before the cluster has been deployed, during cluster creation and while the cluster is running. In fact, additional tests are executed once the cluster is in use, which check that best practices are being followed for highly available workloads. Across these dozens of tests, only a few of them affect running cluster workloads and these are all within the storage category, so skipping this entire category is an easy way to avoid disruptive tests.

Failover clustering comes with a built-in safeguard to prevent accidental downtime when running the storage tests during validation. If the cluster has any online groups when validation is initiated, and the storage tests remain selected, it prompts the user for confirmation whether they want to run all the tests (and cause downtime), or to skip testing the disks of any online groups to avoid downtime. If the entire storage category was excluded from being tested, then this prompt isn't displayed. This enables cluster validation with no downtime.

### How to revalidate your cluster

1. In the Failover Cluster snap-in, in the console tree, make sure **Failover Cluster Management** is selected and then, under **Management**, select **Validate a Configuration**.

1. Follow the instructions in the wizard to specify the servers and the tests, and run the tests. The **Summary** page appears after the tests run.

1. While still on the **Summary** page, select **View Report** to view the test results.

   To view the results of the tests after you close the wizard, see `%SystemRoot%\Cluster\Reports\Validation Report date and time.html` where `%SystemRoot%` is the folder in which the operating system is installed (for example, `C:\Windows`).

1. To view help articles that help you interpret the results, select **More about cluster validation tests**.

To view help articles about cluster validation after you close the wizard, in the Failover Cluster snap-in, select **Help**, select **Help Topics**, select the **Contents** tab, expand the contents for the failover cluster help, and select **Validating a Failover Cluster Configuration**. After the validation wizard has completed, the **Summary Report** will display the results. All tests must pass with either a green check mark or in some cases a yellow triangle (warning). When looking for problem areas (red Xs or yellow question marks), in the part of the report that summarizes the test results, select an individual test to review the details. Any red X issues need to be resolved before troubleshooting [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] issues.

### Install updates

Installing updates is an important part of avoiding problems with your system. Useful links:

- [Recommended hotfixes and updates for Windows Server 2012 R2-based failover clusters](/previous-versions/troubleshoot/windows-server/updates-for-windows-server-2012-r2-failover-cluster)
- [Recommended hotfixes and updates for Windows Server 2012-based failover clusters](/previous-versions/troubleshoot/windows-server/updates-for-windows-server-2012-failover-cluster)
- [Recommended hotfixes and updates for Windows Server 2008 R2-based failover clusters](/previous-versions/troubleshoot/windows-server/hotfixes-for-windows-server-2008-r2-server-cluster)
- [Recommended hotfixes and updates for Windows Server 2008-based failover clusters](https://support.microsoft.com/help/957311)

## Recover from failover cluster failure

Usually, failover cluster failure is to the result of one of two causes:

- Hardware failure in one node of a two-node cluster. This hardware failure could be caused by a failure in the SCSI card or in the operating system.

  To recover from this failure, remove the failed node from the failover cluster using the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program, address the hardware failure with the computer offline, bring the machine back up, and then add the repaired node back to the failover cluster instance.

  For more information, see [Create a New Always On Failover Cluster Instance (Setup)](../install/create-a-new-sql-server-failover-cluster-setup.md) and [Recover from failover cluster instance failure](recover-from-failover-cluster-instance-failure.md).

- Operating system failure. In this case, the node is offline, but isn't irretrievably broken.

  To recover from an operating system failure, recover the node and test failover. If the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance doesn't fail over properly, you must use the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program to remove [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] from the failover cluster, make necessary repairs, bring the computer back up, and then add the repaired node back to the failover cluster instance.

  Recovering from operating system failure this way can take time. If the operating system failure can be recovered easily, avoid using this technique.

  For more information, see [Create a New Always On Failover Cluster Instance (Setup)](../install/create-a-new-sql-server-failover-cluster-setup.md) and [Recover from failover cluster instance failure](recover-from-failover-cluster-instance-failure.md).

## Resolve common problems

The following list describes common usage issues and explains how to resolve them.

### Problem: Incorrect use of command-prompt syntax to install SQL Server

**Issue 1:** It's difficult to diagnose Setup issues when using the `/qn` switch from the command prompt, as the `/qn` switch suppresses all Setup dialog boxes and error messages. If the `/qn` switch is specified, all Setup messages, including error messages, are written to Setup log files. For more information about log files, see [View and read SQL Server Setup log files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).

**Resolution 1**: Use the `/qb` switch instead of the `/qn` switch. If you use the `/qb` switch, the basic UI in each step will be displayed, including error messages.

### Problem: SQL Server can't connect to the network after it migrates to another node

**Issue 1:** [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service accounts are unable to contact a domain controller.

**Resolution 1**: Check your event logs for signs of networking issues such as adapter failures or DNS problems. Verify that you can ping your domain controller.

**Issue 2:** [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service account passwords aren't identical on all cluster nodes, or the node doesn't restart a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service that has migrated from a failed node.

**Resolution 2:** Change the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service account passwords using [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager. If you don't, and you change the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service account passwords on one node, you must also change the passwords on all other nodes. [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager does this automatically.

### Problem: SQL Server can't access the cluster disks

**Issue 1:** Firmware or drivers aren't updated on all nodes.

**Resolution 1:** Verify that all nodes are using correct firmware versions and same driver versions.

**Issue 2:** A node can't recover cluster disks that have migrated from a failed node on a shared cluster disk with a different drive letter.

**Resolution 2:** Disk drive letters for the cluster disks must be the same on both servers. If they aren't, review your original installation of the operating system and [!INCLUDE [msCoName](../../../includes/msconame-md.md)] Cluster Service (MSCS).

### Problem: Failure of a SQL Server service causes failover

**Resolution:** To prevent the failure of specific services from causing the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] group to fail over, configure those services using Cluster Administrator in Windows, as follows:

- Clear the **Affect the Group** check box on the **Advanced** tab of the **Full Text Properties** dialog box. However, if [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] causes a failover, the full-text search service restarts.

### Problem: SQL Server doesn't start automatically

**Resolution:** Use Cluster Administrator in MSCS to automatically start a failover cluster. The [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service should be set to start manually; the Cluster Administrator should be configured in MSCS to start the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service. For more information, see [Managing Services](/previous-versions/sql/sql-server-2008-r2/ms178096(v=sql.105)).

### Problem: The Network Name is offline and you can't connect to SQL Server using TCP/IP

**Issue 1:** DNS is failing with cluster resource set to require DNS.

**Resolution 1:** Correct the DNS problems.

**Issue 2:** A duplicate name is on the network.

**Resolution 2:** Use **nbtstat** to find the duplicate name and then correct the issue.

**Issue 3:** [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] isn't connecting using Named Pipes.

**Resolution 3:** To connect using Named Pipes, create an alias using the SQL Server Configuration Manager to connect to the appropriate computer. For example, if you have a cluster with two nodes (**Node A** and **Node B**), and a failover cluster instance (**Virtsql**) with a default instance, you can connect to the server that has the Network Name resource offline using the following steps:

1. Determine on which node the group containing the instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is running by using the Cluster Administrator. For this example, it's **Node A**.

1. Start the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service on that computer using **net start**. For more information about using **net start**, see [Starting SQL Server Manually](/previous-versions/sql/sql-server-2008-r2/ms191193(v=sql.105)).

1. Start the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] SQL Server Configuration Manager on **Node A**. View the pipe name on which the server is listening. It should be similar to `\\.\$$\VIRTSQL\pipe\sql\query`.

1. On the client computer, start the SQL Server Configuration Manager.

1. Create an alias `SQLTEST1` to connect through Named Pipes to this pipe name. To do this, enter **Node A** as the server name and edit the pipe name to be `\\.\pipe\$$\VIRTSQL\sql\query`.

1. Connect to this instance using the alias `SQLTEST1` as the server name.

### Problem: SQL Server Setup fails on a cluster with error 11001

**Issue:** An orphan registry key in `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL.X\Cluster`.

**Resolution:** Make sure the `MSSQL.X` registry hive isn't currently in use, and then delete the cluster key.

### Problem: Cluster Setup Error: "The installer has insufficient privileges to access this directory: \<drive>\Microsoft SQL Server. The installation cannot continue. Log on as an administrator or contact your system administrator"

**Issue:** This error is caused by a SCSI shared drive that isn't partitioned properly.

**Resolution:** Recreate a single partition on the shared disk using the following steps:

1. Delete the disk resource from the cluster.
1. Delete all partitions on the disk.
1. Verify in the disk properties that the disk is a basic disk.
1. Create one partition on the shared disk, format the disk, and assign a drive letter to the disk.
1. Add the disk to the cluster using Cluster Administrator (cluadmin).
1. Run [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Setup.

### Problem: Applications fail to enlist SQL Server resources in a distributed transaction

**Issue:** Because the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) isn't completely configured in Windows, applications might fail to enlist [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] resources in a distributed transaction. This problem can affect linked servers, distributed queries, and remote stored procedures that use distributed transactions. For more information about how to configure MS DTC, see [Before Installing Failover Clustering](../install/before-installing-failover-clustering.md).

**Resolution:** To prevent such problems, you must fully enable MS DTC services on the servers where [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is installed and MS DTC is configured.

To fully enable MS DTC, use the following steps:

1. In Control Panel, open **Administrative Tools**, and then open **Computer Management**.

1. In the left pane of Computer Management, expand **Services and Applications**, and then select **Services**.

1. In the right pane of Computer Management, right-click **Distributed Transaction Coordinator**, and select **Properties**.

1. In the **Distributed Transaction Coordinator** window, select the **General** tab, and then select **Stop** to stop the service.

1. In the **Distributed Transaction Coordinator** window, select the **Logon** tab, and set the logon account `NT AUTHORITY\NetworkService`.

1. Select **Apply** and **OK** to close the **Distributed Transaction Coordinator** window. Close the **Computer Management** window. Close the **Administrative Tools** window.

## Use extended stored procedures and COM objects

When you use extended stored procedures with a failover clustering configuration, all extended stored procedures must be installed on a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]-dependent cluster disk. Doing so ensures that when a node fails over, the extended stored procedures can still be used.

If the extended stored procedures use COM components, the administrator must register the COM components on each node of the cluster. The information for loading and executing COM components must be in the registry of the active node in order for the components to be created. Otherwise, the information remains in the registry of the computer on which the COM components were first registered.

## Related content

- [View and read SQL Server Setup log files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)
- [Programming Database Engine extended stored procedures](../../../relational-databases/extended-stored-procedures-programming/database-engine-extended-stored-procedures-programming.md)
