---
title: "Add, remove nodes Failover Cluster Instance"
description: This article shows you how to add or remove nodes in an existing SQL Server Always On failover cluster instance.
ms.custom: "seo-lt-2019"
ms.date: "12/13/2019"
ms.reviewer: ""
ms.service: sql
ms.subservice: failover-cluster-instance
ms.topic: how-to
helpviewer_keywords: 
  - "adding nodes"
  - "nodes [Faillover Clustering], removing"
  - "nodes [Faillover Clustering], adding"
  - "failover clustering [SQL Server], nodes"
  - "deleting nodes"
  - "cluster maintenance [SQL Server]"
  - "removing nodes"
ms.assetid: fe20dca9-a4c1-4d32-813d-42f1782dfdd3
author: MashaMSFT
ms.author: mathoma
---

# Add or remove nodes in a failover cluster instance (Setup)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

 Use this procedure to manage nodes to an existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance.  
  
 To update or remove a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI, you must be a local administrator with permission to log in as a service on all nodes of the underlying Windows Server failover cluster (WSFC). For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.  
  
 To add a node to an existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI, you must run [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup on the node that is to be added to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance. Do not run Setup on the active node.  
  
 To remove a node from an existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI, you must run [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup on the node that is to be removed from the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance.  
  
 To view procedural steps to add or remove nodes, select one of the following operations:  
  
-   [Add a node to an existing Always On failover cluster instance](#Add)  
  
-   [Remove a node from an existing Always On failover cluster instance](#Remove)  
  
> [!IMPORTANT]  
>  The operating system drive letter for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] install locations must match on all the nodes added to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance.  
  
##  <a name="Add"></a> Add Node  
  
#### To add a node to an existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance  
  
1.  Insert the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation media, and from the root folder, double-click Setup.exe. To install from a network share, navigate to the root folder on the share, and then double-click Setup.exe.  
  
2.  The Installation Wizard will launch the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Installation Center. To add a node to an existing failover cluster instance, click **Installation** in the left-hand pane. Then, select **Add node to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster**.  
  
3.  The System Configuration Checker will run a discovery operation on your computer. To continue, select **OK**.
  
4.  On the Language Selection page, you can specify the language for your instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] if you are installing on a localized operating system and the installation media includes language packs for both English and the language corresponding to the operating system. For more information about cross-language support and installation considerations, see [Local Language Versions in SQL Server](../../../sql-server/install/local-language-versions-in-sql-server.md).  
  
     To continue, click **Next**.  
  
5.  On the Product key page, specify the PID key for a production version of the product. Note that the product key you enter for this installation must be for the same [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] edition as that which is installed on the active node.  
  
6.  On the License Terms page, read the license agreement, and then select the check box to accept the licensing terms and conditions. To help improve [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE[msCoName](../../../includes/msconame-md.md)]. To continue, click **Next**. To end Setup, click **Cancel**.  
  
7.  The System Configuration Checker will verify the system state of your computer before Setup continues. After the check is complete, click **Next** to continue.  
  
8.  On the Cluster Node Configuration page, use the drop-down box to specify the name of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance that will be modified during this Setup operation.  
  
9. On the Server Configuration - Service Accounts page, specify login accounts for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] services. The actual services that are configured on this page depend on the features you selected to install. For failover cluster instance installations, account name and startup type information will be pre-populated on this page based on settings provided for the active node. You must provide passwords for each account. For more information, see [Server Configuration - Service Accounts](../../../database-engine/install-windows/install-sql-server.md) and [Configure Windows Service Accounts and Permissions](../../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
     **Security Note** [!INCLUDE[ssNoteStrongPass](../../../includes/ssnotestrongpass-md.md)]  
  
     When you are finished specifying login information for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] services, click **Next**.  
  
10. On the Reporting page, specify the information you would like to send to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] to improve [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. By default, option for error reporting is enabled.  
  
11. The System Configuration Checker will run one more set of rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features you have specified.  
  
12. The Ready to Add Node page displays a tree view of installation options that were specified during Setup.  
  
13. Add Node Progress page provides status so you can monitor installation progress as Setup proceeds.  
  
14. After installation, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
15. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you are done with Setup. For more information about Setup log files, see [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  
  
##  <a name="Remove"></a> Remove Node  
  
#### To remove a node from an existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance  
  
1.  Insert the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click setup.exe. To install from a network share, navigate to the root folder on the share, and then double-click Setup.exe.  
  
2.  The Installation Wizard launches the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Installation Center. To remove a node to an existing failover cluster instance, click **Maintenance** in the left-hand pane, and then select **Remove node from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster**.  
  
3.  The System Configuration Checker will run a discovery operation on your computer. To continue, select **OK**.
  
4.  After you click install on the Setup Support Files page, the System Configuration Checker verifies the system state of your computer before Setup continues. After the check is complete, click **Next** to continue.  
  
5.  On the Cluster Node Configuration page, use the drop-down box to specify the name of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance to be modified during this Setup operation. The node to be removed is listed in the **Name of this node** field.  
  
6.  The Ready to Remove Node page displays a tree view of options that were specified during Setup. To continue, click **Remove**.  
  
7.  During the remove operation, the Remove Node Progress page provides status.  
  
8.  The Complete page provides a link to the summary log file for the remove node operation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] remove node, click **Close**. For more information about Setup log files, see [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  
  
## See Also  
 [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)  
  
