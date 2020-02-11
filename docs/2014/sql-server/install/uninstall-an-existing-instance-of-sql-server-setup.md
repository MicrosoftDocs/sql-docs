---
title: "Uninstall an Existing Instance of SQL Server (Setup) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "removing instances of SQL Server"
  - "uninstalling instances of SQL Server"
  - "removing SQL Server"
  - "instances of SQL Server, uninstalling"
  - "uninstalling SQL Server"
ms.assetid: 3c64b29d-61d7-4b86-961c-0de62261c6a1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Uninstall an Existing Instance of SQL Server (Setup)
  This article describes how to uninstall a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By following the steps in this topic, you also prepare the system so that you can reinstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  To uninstall an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must be a local administrator with permission to log on as a service.  
  
> [!NOTE]  
>  To uninstall a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, use the Remove Node functionality provided by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup to remove each node individually. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md)  
  
 Consider the following important information before you uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   Before you remove [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components from a computer that has the minimum required amount of physical memory, make sure that the page file size is sufficient. The page file size must be equal to two times the amount of physical memory. Insufficient virtual memory can cause an incomplete removal of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   If you have multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser uninstalls automatically when the last instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is uninstalled.  
  
     If you want to uninstall all components of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you must uninstall the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser component manually from **Programs and Features** in **Control Panel**.  
  
### Before You Uninstall  
  
1.  **Back up your data.** Although this is not a required step, you might have databases that you want to save in their present state. You might also want to save changes that were made to the system databases. If either situation is true, make sure that back up the data before you uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Alternatively, save a copy of all the data and log files in a folder other than the MSSQL folder. The MSSQL folder is deleted during uninstallation.  
  
     The files that you must save include the following database files:  
  
    -   Master.mdf  
  
    -   Mastlog.ldf  
  
    -   Model.mdf  
  
    -   Modellog.ldf  
  
    -   Msdbdata.mdf  
  
    -   Msdblog.ldf  
  
    -   Mssqlsystemresource.mdf  
  
    -   Mssqlsustemresource.ldf  
  
    -   Tempdb.mdf  
  
    -   Templog.ldf  
  
    -   `ReportServer[$InstanceName]` (Thisis the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] default database.)  
  
    -   ReportServer[$InstanceName]TempDB (This is the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] default temporary database.)  
  
2.  **Delete the local security groups.** Before you uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], delete the local security groups for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components.  
  
3.  **Stop all**  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **services.** We recommend that you stop all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services before you uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components. Active connections can prevent successful uninstallation.  
  
4.  **Use an account that has the appropriate permissions.** Log on to the server by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account or by using an account that has equivalent permissions. For example, you can log on to the server by using an account that is a member of the local Administrators group.  
  
### To Uninstall an Instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
1.  To begin the uninstall process, go to **Control Panel** and then **Programs and Features**.  
  
2.  Right click **[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]** and select **Uninstall**. Then click **Remove**. This starts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard.  
  
     Setup Support Rules runs to verify your computer configuration. To continue, click **Next**.  
  
3.  On the Select Instance page, use the drop-down box to specify an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to remove, or specify the option to remove only the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shared features and management tools. To continue, click **Next**.  
  
4.  On the Select Features page, specify the features to remove from the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     Removal rules runs to verify that the operation can complete successfully.  
  
5.  On the **Ready to Remove** page, review the list of components and features that will be uninstalled. Click **Remove** to begin uninstalling  
  
6.  Immediately after you uninstall the last [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, the other programs associated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will still be visible in the list of programs in **Programs and Features**. However, if you close **Programs and Features**, the next time you open **Programs and Features**, it will refresh the list of programs, to show only the ones that are actually still installed.  
  
### If the Uninstallation Fails  
  
1.  If the uninstallation process does not complete successfully, attempt to fix the problem that caused the uninstallation to fail. The following articles can help you understand the cause of the failed uninstallation:  
  
    -   [How to identify SQL Server 2008 setup issues in the setup log files](https://support.microsoft.com/kb/955396/en-us)  
  
    -   [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)  
  
2.  If you are unable to fix the cause of the uninstallation failure, you can contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Support. In some cases, such as unintentional deletion of important files, reinstalling the operating system may be required before reinstalling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the computer.  
  
## See Also  
 [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)  
  
  
