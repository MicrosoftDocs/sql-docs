---
title: Add Features to an Instance of SQL Server (Setup)
description: This article provides a step-by-step procedure for adding instance-aware features to an instance of SQL Server  2019.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/05/2021
ms.service: sql
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords:
  - "feature adding [SQL Server]"
  - " SQL Server, features"
  - "adding features to  SQL Server"
monikerRange: ">=sql-server-2016"
---

# Add Features to an Instance of SQL Server (Setup)

[!INCLUDE [ SQL Server - Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article provides a step-by-step procedure for adding features to an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Some SQL Server components or services are specific to an instance of SQL Server. These are also known as instance-aware. They share the same version as the instance that hosts them, and are used exclusively for that instance. You can add the instance-aware components to an instance SQL Server, along with the shared components of if they are not already installed. For a list of features that are supported by the different editions of SQL Server, see [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md) or [SQL Server 2019](../../sql-server/editions-and-components-of-sql-server-2019.md).

To add features to an instance of SQL Server from the command prompt, see [Install SQL Server from the Command Prompt](./install-sql-server-from-the-command-prompt.md).

> [!CAUTION]
> Adding features to an existing installation of SQL Server will install the features at the version level of the installation media, which may be behind the version level other features of SQL Server. This may result in unexpected behavior or errors. Always follow the success of SQL Server Setup by bringing the new feature up to the same version level. Install service packs (SPs), cumulative updates (CUs), and/or general distribution releases (GDRs) as needed. To determine the version of features added to an installation of SQL Server, see [Determine the version, edition, and update level of SQL Server and its components](/troubleshoot/sql/general/determine-version-edition-update-level).

## Prerequisites

Before you continue, review articles in [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md). Also be aware:

- For local installations, you must run Setup as an administrator. If you install SQL Server from a remote share, you must use a domain account that has read permissions on the remote share.  
- When you add features to an instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], the existing usage report settings are applied to the newly added features. To change these settings, use the **SQL Server Error and Usage Reporting** tool on the SQL Server **Configuration Tools** menu.
- You cannot add features to a failover cluster instance. For example, you cannot add the PolyBase feature to an existing failover cluster instance. Similarly, removing features from a failover cluster instance is also not supported.

## Procedures

#### To add features to an instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]

1. Insert the SQL Server installation media. From the root folder, double-click setup.exe. To install from a network share, navigate to the root folder on the share, and then double-click setup.exe. If the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Setup dialog box appears, select **OK** to install the prerequisites, then select **Cancel** to quit [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] installation.  

2. The Installation Wizard will launch the SQL Server Installation Center. To add a new feature to an existing instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], select **Installation** in the left-hand navigation area, and then select **New SQL Server stand-alone installation or add features to an existing installation**.

3. The System Configuration Checker will run a discovery operation on your computer. To view the verification details, select **View Details**. To continue, select **OK**.

4. On the Product Updates page, the latest available SQL Server product updates are displayed. If you don't want to include the updates, clear the **Include SQL Server product updates** check box. If no product updates are discovered SQL Server Setup does not display this page and auto advances to the **Install Setup Files** page.

5. On the Install Setup files page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for SQL Server Setup is found, and is specified to be included, that update will also be installed. Select **Install** to install Setup Support files.  

6. The System Configuration Checker will verify the system state of your computer before Setup continues.  

7. On the Installation Type page, select the option **Add features to an existing instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]**, and select the instance you would like to update.

8. On the Feature Selection page, select the components for your installation. A description for each component group appears in the right-hand pane after you select the feature name. You can select any combination of check boxes. For more information, see [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md) and [SQL Server  2019](../../sql-server/editions-and-components-of-sql-server-2019.md). Each component can be installed only once on a given instance of SQL Server. To install multiple components, you must install an additional instance of SQL Server.

    The prerequisites for the selected features are displayed on the right-hand pane. SQL Server Setup will install the prerequisites that are not already installed during the installation step described later in this procedure.

    The System Configuration Checker will verify the system state of your computer before Setup continues. Select **Next** to continue.

9. The Disk Space Requirements page calculates the required disk space for the features you specify, and compares requirements to the available disk space on the computer where Setup is running.

10. Work flow for the remainder of this article depends on the features you have specified for your installation. You might not see all of the pages, depending on your selections.

11. On the Server Configuration - Service Accounts page, specify login accounts for SQL Server services. The actual services that are configured on this page depend on the features you selected to install.

    You can assign the same login account to all SQL Server services, or you can configure each service account individually. You can also specify whether services start automatically, are started manually, or are disabled. Microsoft recommends that you configure service accounts individually to provide least privileges for each service, where SQL Server services are granted the minimum permissions they need to complete their tasks. For more information, see [Server Configuration - Service Accounts](./install-sql-server.md) and [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

    To specify the same login account for all service accounts in this instance of SQL Server, provide credentials in the fields at the bottom of the page.

    **Security Note** : [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

    When you are finished specifying login information for SQL Server services, select **Next**.

12. Use the **Server Configuration - Collation** tab to specify non-default collations for the Database Engine and Analysis Services. For more information, see [Server Configuration - Collation](./install-sql-server.md).

13. Use the Database Engine Configuration - Account Provisioning page to specify the following settings:  

    - Security Mode - Select Windows Authentication or Mixed Mode Authentication for your instance of SQL Server. If you select Mixed Mode Authentication, you must provide a strong password for the built-in SQL Server system administrator account.

        After a device establishes a successful connection to SQL Server, the security mechanism is the same for both Windows Authentication and Mixed Mode. For more information, see [Database Engine Configuration - Server Configuration](./install-sql-server.md).  

    - SQL Server  Administrators - You must specify at least one system administrator for the instance of SQL Server. To add the account under which SQL Server Setup is running, select **Add Current User**. To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that will have administrator privileges for the instance of SQL Server. For more information, see [Database Engine Configuration - Server Configuration](./install-sql-server.md).

    When you are finished editing the list, select **OK**. Verify the list of administrators in the configuration dialog box. When the list is complete, select **Next**.

14. Use the Database Engine Configuration - Data Directories page to specify non-default installation directories. To install to default directories, select **Next**.  

    > [!IMPORTANT]
    > If you specify non-default installation directories, ensure that the installation folders are unique to this instance of SQL Server. None of the directories in this dialog box should be shared with directories from other instances of SQL Server.

    For more information, see [Database Engine Configuration - Data Directories](./install-sql-server.md).

15. Use the Database Engine Configuration - FILESTREAM page to enable FILESTREAM for your instance of SQL Server. For more information about FILESTREAM, see [Database Engine Configuration - Filestream](./install-sql-server.md). To continue, select Next.

16. Use the Analysis Services Configuration - Account Provisioning page to specify the server mode and the users or accounts that will have administrator permissions for Analysis Services. Server mode determines which memory and storage subsystems are used on the server. Different solution types run in different server modes. An instance of Analysis Services can support either conventional multidimensional cubes, or tabular models, but an instance cannot support both types of models. For new development, use the default option, **Tabular Mode**. 

    Regarding administrator permissions, you must specify at least one system administrator for Analysis Services. To add the account under which SQL Server Setup is running, select **Add Current User**. To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that will have administrator privileges for Analysis Services. For more information about server mode and administrator permissions, see [Analysis Services Configuration - Account Provisioning](./install-sql-server.md).

    When you are finished editing the list, select **OK**. Verify the list of administrators in the configuration dialog box. When the list is complete, select **Next**.

17. Use the Analysis Services Configuration - Data Directories page to specify non-default installation directories. To install to default directories, select **Next**.

    > [!IMPORTANT]
    > If you specify non-default installation directories, ensure that the installation folders are unique to this instance of SQL Server. None of the directories in this dialog box should be shared with directories from other instances of SQL Server.

    For more information, see [Analysis Services Configuration - Data Directories](./install-sql-server.md).

18. Use the Reporting Services Configuration page to specify the type of Reporting Services installation to create. For more information about Reporting Services configuration modes, see [Reporting Services Configuration Options &#40;SSRS&#41;](./install-sql-server.md).

19. Use the Distributed Replay Controller Configuration page to specify the users you want to grant administrative permissions to for the Distributed Replay controller service. Users that have administrative permissions will have unlimited access to the Distributed Replay controller service.

    Select the **Add Current User** button to add the users to whom you want to grant access permissions for the Distributed Replay controller service. Select the **Add** button to add access permissions for the Distributed Replay controller service. Select the **Remove** button to remove access permissions from the Distributed Replay controller service.

    To continue, select **Next**.

20. Use the Distributed Replay Client Configuration page to specify the users you want to grant administrative permissions to for the Distributed Replay client service. Users that have administrative permissions will have unlimited access to the Distributed Replay client service.

    **Controller Name** is an optional parameter, and the default value is \<*blank*>. Enter the name of the controller that the client computer will communicate with for the Distributed Replay client service. Note the following conditions:

    - If you have already set up a controller, enter the name of the controller while configuring each client.

    - If you have not yet set up a controller, you can leave the controller name blank. However, you must manually enter the controller name in the **client configuration** file.

    Specify the **Working Directory** for the Distributed Replay client service. The default working directory is \<*drive letter*>:\Program Files\Microsoft\SQL Server\DReplayClient\WorkingDir.

    Specify the **Result Directory** for the Distributed Replay client service. The default result directory is \<*drive letter*>:\Program Files\Microsoft\SQL Server\DReplayClient\ResultDir.

    To continue, select **Next**.

21. On the Error Reporting page, specify the information you would like to send to Microsoft that will help to improve SQL Server. By default, the option for error reporting is enabled.

22. The System Configuration Checker will run one more set of rules to validate your computer configuration with the SQL Server features you have specified.  

23. The Ready to Install page displays a tree view of installation options that were specified during Setup. On this page, Setup indicates whether the Product Update feature is enabled or disabled and the final update version. After you select install, SQL Server Setup will first install the required prerequisites for the selected features followed by the feature installation.  

24. During installation, the Installation Progress page provides status so you can monitor installation progress as Setup proceeds.  

25. After installation, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the SQL Server installation process, select **Close**.

26. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you are done with Setup. For information about Setup log files, see [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).

> [!CAUTION]
> Apply servicing updates
>
> Adding features to an existing installation of SQL Server will install the feature at the version level of the installation media, which may be behind the version level other features of SQL Server. This may result in unexpected behavior or errors. Always follow up installing the new feature by bringing the new feature up to the same version level. Install service packs (SPs), cumulative updates (CUs), and/or general distribution releases (GDRs) as needed. To determine the version of the server and new features, see [Determine the version, edition, and update level of SQL Server and its components](/troubleshoot/sql/general/determine-version-edition-update-level).

## Next Steps

Configure your SQL Server installation

- To reduce the attackable surface area of a system, SQL Server selectively installs and activates key services and features. For more information, see [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md).

## See Also
- [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)
- [Validate a SQL Server Installation](../../database-engine/install-windows/validate-a-sql-server-installation.md)
- [Repair a Failed SQL Server 2016 Installation](../../database-engine/install-windows/repair-a-failed-sql-server-installation.md)
- [Upgrade SQL Server Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)
- [Install SQL Server from the Command Prompt](./install-sql-server-from-the-command-prompt.md)
- [Latest Updates for SQL Server](latest-updates-for-microsoft-sql-server.md)