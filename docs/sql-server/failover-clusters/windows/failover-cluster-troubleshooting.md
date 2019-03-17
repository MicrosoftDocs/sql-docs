---
title: "Failover Cluster Troubleshooting | Microsoft Docs"
ms.custom: ""
ms.date: "10/21/2015"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "troublshooting, failover clustering"
  - "failover clustering, troubleshooting"
  - "cluster troubleshooting"
ms.assetid: 84012320-5a7b-45b0-8feb-325bf0e21324
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Failover Cluster Troubleshooting
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic provides information about the following issues:  
  
-   Basic troubleshooting steps.  
  
-   Recovering from a failover cluster failure.  
  
-   Resolving the most common failover clustering problems.  
  
-   Using extended stored procedures and COM objects.  
  
## Basic Troubleshooting Steps  
 The first diagnostic step is to run a fresh cluster validation check. For details on validation, see [Failover Cluster Step-by-Step Guide: Validating Hardware for a Failover Cluster](https://technet.microsoft.com/library/cc732035.aspx).  This can be completed without any interruption of service as it does not affect any online cluster resources. Validation can be run at any time once the Failover Clustering feature has been installed, including before the cluster has been deployed, during cluster creation and while the cluster is running. In fact, additional tests are executed once the cluster is in use, which check that best practices are being followed for highly-available workloads. Across these dozens of tests, only a few of them will impact running cluster workloads and these are all within the storage category, so skipping this entire category is an easy way to avoid disruptive tests.  
Failover Clustering comes with a built-in safeguard to prevent accidental downtime when running the storage tests during validation. If the cluster has any online groups when validation is initiated, and the storage tests remain selected, it will prompt the user for confirmation whether they want to run all the tests (and cause downtime), or to skip testing the disks of any online groups to avoid downtime. If the entire storage category was excluded from being tested, then this prompt is not displayed. This will enable cluster validation with no downtime.  
  
#### How to revalidate your cluster  
  
1.  In the Failover Cluster snap-in, in the console tree, make sure **Failover Cluster Management** is selected and then, under **Management**, click **Validate a Configuration**.  
  
2.  Follow the instructions in the wizard to specify the servers and the tests, and run the tests. The **Summary** page appears after the tests run.  
  
3.  While still on the **Summary** page, click **View Report** to view the test results.  
  
     To view the results of the tests after you close the wizard, see **%SystemRoot%\Cluster\Reports\Validation Report date and time.html** where **%SystemRoot%** is the folder in which the operating system is installed (for example, **C:\Windows**).  
  
4.  To view help topics that will help you interpret the results, click **More about cluster validation tests**.  
  
 To view help topics about cluster validation after you close the wizard, in the Failover Cluster snap-in, click **Help**, click **Help Topics**, click the **Contents** tab, expand the contents for the failover cluster help, and click **Validating a Failover Cluster Configuration**.  After the validation wizard has completed, the **Summary Report** will display the results. All tests must pass with either a green check mark or in some cases a yellow triangle (warning). When looking for problem areas (red Xs or yellow question marks), in the part of the report that summarizes the test results, click an individual test to review the details. Any red X issues will need to be resolved prior to troubleshooting [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] issues.  
  
 **Install Updates**  
  
 Installing updates is an important part of avoiding problems with your system. Useful links:  
  
-   [Recommended hotfixes and updates for Windows Server 2012 R2-based failover clusters](https://support.microsoft.com/kb/2920151)  
  
-   [Recommended hotfixes and updates for Windows Server 2012-based failover clusters](https://support.microsoft.com/kb/2784261)  
  
-   [Recommended hotfixes and updates for Windows Server 2008 R2-based failover clusters](https://support.microsoft.com/kb/980054)  
  
-   [Recommended hotfixes and updates for Windows Server 2008-based failover clusters](https://support.microsoft.com/kb/957311)  
  
## Recovering from Failover Cluster Failure  
 Usually, failover cluster failure is to the result of one of two causes:  
  
-   Hardware failure in one node of a two-node cluster. This hardware failure could be caused by a failure in the SCSI card or in the operating system.  
  
     To recover from this failure, remove the failed node from the failover cluster using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program, address the hardware failure with the computer offline, bring the machine back up, and then add the repaired node back to the failover cluster instance.  
  
     For more information, see [Create a New SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md) and [Recover from Failover Cluster Instance Failure](../../../sql-server/failover-clusters/windows/recover-from-failover-cluster-instance-failure.md).  
  
-   Operating system failure. In this case, the node is offline, but is not irretrievably broken.  
  
     To recover from an operating system failure, recover the node and test failover. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance does not fail over properly, you must use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program to remove [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from the failover cluster, make necessary repairs, bring the computer back up, and then add the repaired node back to the failover cluster instance.  
  
     Recovering from operating system failure this way can take time. If the operating system failure can be recovered easily, avoid using this technique.  
  
     For more information, see [Create a New SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md) and [How to: Recover from Failover Cluster Failure in Scenario 2](recover-from-failover-cluster-instance-failure.md).  
  
## Resolving Common Problems  
 The following list describes common usage issues and explains how to resolve them.  
  
### Problem: Incorrect use of command-prompt syntax to install SQL Server  
 **Issue 1:** It is difficult to diagnose Setup issues when using the **/qn** switch from the command prompt, as the **/qn** switch suppresses all Setup dialog boxes and error messages. If the **/qn** switch is specified, all Setup messages, including error messages, are written to Setup log files. For more information about log files, see [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  
  
 **Resolution 1**: Use the **/qb** switch instead of the **/qn** switch. If you use the **/qb** switch, the basic UI in each step will be displayed, including error messages.  
  
### Problem: SQL Server cannot log on to the network after it migrates to another node  
 **Issue 1:** [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service accounts are unable to contact a domain controller.  
  
 **Resolution 1**: Check your event logs for signs of networking issues such as adapter failures or DNS problems. Verify that you can ping your domain controller.  
  
 **Issue 2:** [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account passwords are not identical on all cluster nodes, or the node does not restart a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service that has migrated from a failed node.  
  
 **Resolution 2:** Change the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account passwords using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager. If you do not, and you change the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account passwords on one node, you must also change the passwords on all other nodes. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager does this automatically.  
  
### Problem: SQL Server cannot access the cluster disks  
 **Issue 1:** Firmware or drivers are not updated on all nodes.  
  
 **Resolution 1:** Verify that all nodes are using correct firmware versions and same driver versions.  
  
 **Issue 2:** A node cannot recover cluster disks that have migrated from a failed node on a shared cluster disk with a different drive letter.  
  
 **Resolution 2:** Disk drive letters for the cluster disks must be the same on both servers. If they are not, review your original installation of the operating system and [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Cluster Service (MSCS).  
  
### Problem: Failure of a SQL Server service causes failover  
 **Resolution:** To prevent the failure of specific services from causing the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] group to fail over, configure those services using Cluster Administrator in Windows, as follows:  
  
-   Clear the **Affect the Group** check box on the **Advanced** tab of the **Full Text Properties** dialog box. However, if [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] causes a failover, the full-text search service restarts.  
  
### Problem: SQL Server does not start automatically  
 **Resolution:** Use Cluster Administrator in MSCS to automatically start a failover cluster. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service should be set to start manually; the Cluster Administrator should be configured in MSCS to start the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service. For more information, see [Managing Services](https://msdn.microsoft.com/library/ms178096\(v=sql.105\).aspx).  
  
### Problem: The Network Name is offline and you cannot connect to SQL Server using TCP/IP  
 **Issue 1:** DNS is failing with cluster resource set to require DNS.  
  
 **Resolution 1:** Correct the DNS problems.  
  
 **Issue 2:** A duplicate name is on the network.  
  
 **Resolution 2:** Use NBTSTAT to find the duplicate name and then correct the issue.  
  
 **Issue 3:** [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is not connecting using Named Pipes.  
  
 **Resolution 3:** To connect using Named Pipes, create an alias using the SQL Server Configuration Manager to connect to the appropriate computer. For example, if you have a cluster with two nodes (**Node A** and **Node B**), and a failover cluster instance (**Virtsql**) with a default instance, you can connect to the server that has the Network Name resource offline using the following steps:  
  
1.  Determine on which node the group containing the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running by using the Cluster Administrator. For this example, it is **Node A**.  
  
2.  Start the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service on that computer using **net start**. For more information about using **net start**, see [Starting SQL Server Manually](https://msdn.microsoft.com/library/ms191193\(v=sql.105\).aspx).  
  
3.  Start the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] SQL Server Configuration Manager on **Node A**. View the pipe name on which the server is listening. It should be similar to \\\\.\\$$\VIRTSQL\pipe\sql\query.  
  
4.  On the client computer, start the SQL Server Configuration Manager.  
  
5.  Create an alias SQLTEST1 to connect through Named Pipes to this pipe name. To do this, enter **Node A** as the server name and edit the pipe name to be \\\\.\pipe\\$$\VIRTSQL\sql\query.  
  
6.  Connect to this instance using the alias SQLTEST1 as the server name.  
  
### Problem: SQL Server Setup fails on a cluster with error 11001  
 **Issue:** An orphan registry key in [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL.X\Cluster]  
  
 **Resolution:** Make sure the MSSQL.X registry hive is not currently in use, and then delete the cluster key.  
  
### Problem: Cluster Setup Error: "The installer has insufficient privileges to access this directory: \<drive>\Microsoft SQL Server. The installation cannot continue. Log on as an administrator or contact your system administrator"  
 **Issue:** This error is caused by a SCSI shared drive that is not partitioned properly.  
  
 **Resolution:** Re-create a single partition on the shared disk using the following steps:  
  
1.  Delete the disk resource from the cluster.  
  
2.  Delete all partitions on the disk.  
  
3.  Verify in the disk properties that the disk is a basic disk.  
  
4.  Create one partition on the shared disk, format the disk, and assign a drive letter to the disk.  
  
5.  Add the disk to the cluster using Cluster Administrator (cluadmin).  
  
6.  Run [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup.  
  
### Problem: Applications fail to enlist SQL Server resources in a distributed transaction  
 **Issue:** Because the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) is not completely configured in Windows, applications may fail to enlist [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resources in a distributed transaction. This problem can affect linked servers, distributed queries, and remote stored procedures that use distributed transactions. For more information about how to configure MS DTC, see [Before Installing Failover Clustering](../../../sql-server/failover-clusters/install/before-installing-failover-clustering.md).  
  
 **Resolution:** To prevent such problems, you must fully enable MS DTC services on the servers where [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is installed and MS DTC is configured.  
  
 To fully enable MS DTC, use the following steps:  
  
1.  In Control Panel, open **Administrative Tools**, and then open **Computer Management**.  
  
2.  In the left pane of Computer Management, expand **Services and Applications**, and then click **Services**.  
  
3.  In the right pane of Computer Management, right-click **Distributed Transaction Coordinator**, and select **Properties**.  
  
4.  In the **Distributed Transaction Coordinator** window, click the **General** tab, and then click **Stop** to stop the service.  
  
5.  In the **Distributed Transaction Coordinator** window, click the **Logon** tab, and set the logon account NT AUTHORITY\NetworkService.  
  
6.  Click **Apply** and **OK** to close the **Distributed Transaction Coordinator** window. Close the **Computer Management** window. Close the **Administrative Tools** window.  
  
## Using Extended Stored Procedures and COM Objects  
 When you use extended stored procedures with a failover clustering configuration, all extended stored procedures must be installed on a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-dependent cluster disk. Doing so ensures that when a node fails over, the extended stored procedures can still be used.  
  
 If the extended stored procedures use COM components, the administrator must register the COM components on each node of the cluster. The information for loading and executing COM components must be in the registry of the active node in order for the components to be created. Otherwise, the information remains in the registry of the computer on which the COM components were first registered.  
  
## See Also  
 [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)   
 [How Extended Stored Procedures Work](../../../relational-databases/extended-stored-procedures-programming/how-extended-stored-procedures-work.md)   
 [Execution Characteristics of Extended Stored Procedures](../../../relational-databases/extended-stored-procedures-programming/execution-characteristics-of-extended-stored-procedures.md)  
  
  
