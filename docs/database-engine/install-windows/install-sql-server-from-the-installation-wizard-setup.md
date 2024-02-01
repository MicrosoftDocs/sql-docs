---
title: Install using graphical user interface
description: This article provides a step-by-step procedure for installing a new instance of SQL Server by using the SQL Server Setup Installation Wizard.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: pijocoder
ms.date: 09/27/2023
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: intro-installation
helpviewer_keywords:
  - "installing SQL Server, steps"
  - "Setup [SQL Server], steps"
  - "SQL Server, installing"
monikerRange: ">=sql-server-2016"
---
# Install SQL Server from the Installation Wizard (Setup)

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article explains how to install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with the Installation Wizard.

The installation experience depends on the version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Select your version.

::: moniker range=">=sql-server-2016 <=sql-server-2017"

:::row:::
    :::column:::
        **_\* SQL Server 2016 & SQL Server 2017 \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Server 2019](install-sql-server-from-the-installation-wizard-setup.md?view=sql-server-ver15&preserve-view=true) &nbsp;
    :::column-end:::
    :::column:::
        [SQL Server 2022](install-sql-server-from-the-installation-wizard-setup.md?view=sql-server-ver16&preserve-view=true) &nbsp;
    :::column-end:::
:::row-end:::

:::moniker-end

::: moniker range="=sql-server-ver15"

:::row:::
    :::column:::
        [SQL Server 2016 & SQL Server 2017](install-sql-server-from-the-installation-wizard-setup.md?view=sql-server-2017&preserve-view=true) &nbsp;
    :::column-end:::
    :::column:::
        **_\* SQL Server 2019 \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Server 2022](install-sql-server-from-the-installation-wizard-setup.md?view=sql-server-ver16&preserve-view=true) &nbsp;
    :::column-end:::
:::row-end:::

:::moniker-end

::: moniker range="=sql-server-ver16"

:::row:::
    :::column:::
        [SQL Server 2016 & SQL Server 2017](install-sql-server-from-the-installation-wizard-setup.md?view=sql-server-2017&preserve-view=true) &nbsp;
    :::column-end:::
    :::column:::
        [SQL Server 2019](install-sql-server-from-the-installation-wizard-setup.md?view=sql-server-ver15&preserve-view=true) &nbsp;
    :::column-end:::
    :::column:::
        **_\* SQL Server 2022 \*_** &nbsp;
    :::column-end:::
:::row-end:::

:::moniker-end

This article provides a step-by-step procedure for installing a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup Installation Wizard. The Installation Wizard provides a single feature tree for installation of all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components so that you don't have to install them individually. To install the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components individually, see [Install SQL Server](install-sql-server.md#individual-component-installation).

For other ways to install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see:

- [Install SQL Server from the command prompt](./install-sql-server-from-the-command-prompt.md)
- [Install SQL Server by using a configuration file](./install-sql-server-using-a-configuration-file.md)
- [Install SQL Server by using SysPrep](install-sql-server-using-sysprep.md)
- [Create a new SQL Server failover cluster (Setup)](../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)
- [Upgrade SQL Server by using the Installation Wizard (Setup)](upgrade-sql-server-using-the-installation-wizard-setup.md)

## Get the installation media

[!INCLUDE [GetInstallationMedia](../../includes/getssmedia.md)]

## Prerequisites

Before you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], review [Planning a SQL Server installation](../../sql-server/install/planning-a-sql-server-installation.md).

> [!NOTE]  
> For local installations, you must run Setup as an administrator. If you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

::: moniker range=">=sql-server-2016 <=sql-server-2017"

### <a id="bkmk_ga_instalpatch"></a> Install patch requirement

Microsoft has identified a problem with the Microsoft Visual C++ 2013 runtime binaries that are installed as a prerequisite by [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. An update is available to fix this problem. If this update to the Visual C++ runtime binaries isn't installed, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] may experience stability issues in certain scenarios. Before you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], follow the instructions in the [SQL Server release notes](../../sql-server/sql-server-2016-release-notes.md#bkmk_ga_instalpatch) to see if your computer requires a patch for the Visual C++ runtime binaries.

This isn't applicable to [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions.

## Install SQL Server 2016 and SQL Server 2017

#### <a id="installation-media-2016-2017"></a> 1. Installation media

Insert the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click **setup.exe**. To install from a network share, locate the root folder on the share, and then double-click **setup.exe**.

#### <a id="installation-center-2016-2017"></a> 2. SQL Server Installation Center

The Installation Wizard runs the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To create a new installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], select **Installation** in the left navigation area, and then select **New [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] standalone installation or add features to an existing installation**.

#### <a id="product-key-2016-2017"></a> 3. Product Key

On the **Product Key** page, select an option to indicate whether you're installing a free edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or a production version that has a PID key. For more information, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

To continue, select **Next**.

#### <a id="license-terms-2016-2017"></a> 4. License Terms

On the **License Terms** page, review the license agreement. If you agree, select the **I accept the license terms** check box, and then select **Next**.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] transmits information about your installation experience, as well as other usage and performance data to help Microsoft improve the product. To learn more about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data processing and privacy controls, see the [privacy statement](https://privacy.microsoft.com/privacystatement) and [Configure SQL Server to send feedback to Microsoft](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md).

#### <a id="global-rules-2016-2017"></a> 5. Global Rules

In the **Global Rules** page, Setup will automatically advance to the **Product Updates** page if there are no rule errors.

#### <a id="microsoft-update-2016-2017"></a> 6. Microsoft Update

The **Microsoft Update** page appears next if the **Microsoft Update** check box in **Control Panel** > **All Control Panel Items** > **Windows Update** > **Change settings** isn't selected. Selecting the **Microsoft Update** check box changes the computer settings to include the latest updates for all Microsoft products when you scan for Windows updates.

#### <a id="product-updates-2016-2017"></a> 7. Product Updates

On the **Product Updates** page, the latest available [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product updates are displayed. If no product updates are discovered, Setup doesn't display this page and automatically advances to the **Install Setup Files** page.

#### <a id="install-setup-files-2016-2017"></a> 8. Install Setup Files

On the **Install Setup Files** page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for Setup is found and you specify to include it, that update will also be installed. If no update is found, Setup will automatically advance.

#### <a id="install-rules-2016-2017"></a> 9. Install Rules

On the **Install Rules** page, Setup checks for potential problems that might occur while running Setup. If failures occur, select an item in the **Status** column for more information. Otherwise, select **Next**.

#### <a id="azure-extension-for-sql-server-2016-2017"></a> 10. Azure Extension for SQL Server

If this is the first installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the machine, Setup skips the **Installation Type** page and goes directly to the **Feature Selection** page. If [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is already installed on the system, you can use the **Installation Type** page to select either to perform a new installation, or to add features to an existing installation. To continue, select **Next**.

#### <a id="feature-selection-2016-2017"></a> 11. Feature Selection

On the **Feature Selection** page, select the components for your installation. For example, to install a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)], select **Database Engine Services**.

A description for each component group appears in the **Feature description** pane after you select the feature name. You can select any combination of check boxes. For more information, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

The prerequisites for the selected features are displayed in the **Prerequisites for selected features** pane. Setup installs the prerequisites that aren't already installed during the installation step described later in this procedure.

You can also specify a custom directory for shared components by using the field at the bottom of the **Feature Selection** page. To change the installation path for shared components, either update the path in the field at the bottom of the dialog box or select **Browse** to go to an installation directory. The default installation path is [!INCLUDE [ssInstallPath](../../includes/ssinstallpath-md.md)].

> [!NOTE]  
> The path specified for the shared components must be an absolute path. The folder must not be compressed or encrypted. Mapped drives aren't supported.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses two directories for shared features:

- Shared feature directory
- Shared feature directory (x86)

> [!NOTE]  
> The path specified for each of the above options must be different.

#### <a id="feature-roles-2016-2017"></a> 12. Feature Rules

The **Feature Rules** page automatically advances if all rules pass.

#### <a id="instance-configuration-2016-2017"></a> 13. Instance Configuration

On the **Instance Configuration** page, specify whether to install a default instance or a named instance. For more information, see [Instance configuration](../../sql-server/install/instance-configuration.md#instance-configuration-page).

- **Instance ID**: By default, the instance name is used as the instance ID. This ID is used to identify the installation directories and registry keys for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The same behavior occurs for default instances and named instances. For a default instance, the instance name and instance ID are `MSSQLSERVER`. To use a nondefault instance ID, specify a different value in the **Instance ID** text box.

  > [!NOTE]  
  > Typical standalone instances of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)], whether default or named instances, don't use a nondefault value for the instance ID.

  All [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades apply to every component of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- **Installed instances**: The grid shows the instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running. If a default instance is already installed on the computer, you must install a named instance of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)].

The workflow for the rest of the installation depends on the features that you've specified for your installation. Depending on your selections, you might not see all the pages.

#### <a id="polybase-configuration-2016-2017"></a> 14. PolyBase Configuration

Selecting to install the PolyBase feature will add the **PolyBase Configuration** page to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup, displayed after the **Instance Configuration** page. PolyBase requires the Oracle JRE 7 Update 51 (at least), and if this hasn't already been installed, your installation is blocked. On the **PolyBase Configuration** page, you can choose to use the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] as a standalone PolyBase-enabled instance, or you can use this [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] as part of a PolyBase scale-out group. If you choose to use the scale-out group, you need to specify a port range of up to six or more ports.

#### <a id="server-configuration-2016-2017"></a> 15. Server Configuration

Use the **Service Accounts** tab on the **Server Configuration** page to specify the accounts for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that you configure on this page depend on the features that you selected to install. For more information about configuration settings, see [Installation Wizard help](../../sql-server/install/instance-configuration.md#serverconfig).

You can assign the same account to all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, start manually, or are disabled. We recommend you configure service accounts individually to provide the least privileges for each service. Make sure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they must have to complete their tasks. For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

To specify the same account for all service accounts in this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], provide the credentials in the fields at the bottom of the page.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

> [!NOTE]  
> With [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, select the **Grant Perform Volume Maintenance Task privilege to SQL Server Database Engine Service** check box to allow the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] service account to use [database instant file initialization](../../relational-databases/databases/database-instant-file-initialization.md).

Use the **Collation** tab on the **Server Configuration** page to specify non-default collations for the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)].

The default installation setting is determined by the operating system (OS) locale. The server-level collation can either be changed during setup, or by changing the OS locale before installation. The default collation is set to the oldest available version that is associated with each specific locale. This is due to backward compatibility reasons. Therefore, this isn't always the recommended collation. To take full advantage of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] features, change the default installation settings to use Windows collations. For example, for the OS locale **English (United States)** (code page 1252), the default collation during setup is **SQL_Latin1_General_CP1_CI_AS** and can be changed to its closest Windows collation counterpart **Latin1_General_100_CI_AS_SC**.

For more information, see [Collations and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

#### <a id="database-engine-configuration-2016-2017"></a> 16. Database Engine Configuration

- Use the **Server Configuration** tab on the **Database Engine Configuration** page to specify the following options:

  - **Authentication Mode**: Select **Windows Authentication** or **Mixed Mode Authentication** for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you select **Mixed Mode Authentication**, you must provide a strong password for the built-in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account.

    After a device establishes a successful connection to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the security mechanism is the same for both Windows authentication and mixed mode authentication. For more information, see [Database Engine Configuration - Server Configuration page](../../sql-server/install/instance-configuration.md#serverconfig).

  - **SQL Server Administrators**: You must specify at least one system administrator for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To add the account under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, select **Add Current User**. To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that have administrator privileges for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Use the **Data Directories** tab on the **Database Engine Configuration** page to specify nondefault installation directories. To install in the default directories, select **Next**.

  > [!IMPORTANT]  
  > If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

  For more information, see [Database Engine Configuration - Data Directories page](../../sql-server/install/instance-configuration.md#datadir).

- Use the **TempDB** tab on the **Database Engine Configuration** page to configure the file size, number of files, nondefault installation directories, and file-growth settings for `tempdb`. For more information, see [Database Engine Configuration - TempDB page](../../sql-server/install/instance-configuration.md#tempdb).

- Use the **FILESTREAM** tab on the **Database Engine Configuration** page to enable FILESTREAM for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Database Engine Configuration - FILESTREAM page](../../sql-server/install/instance-configuration.md#database-engine-configuration---filestream-page).

#### <a id="analysis-services-configuration-2016-2017"></a> 17. Analysis Services Configuration

- Use the **Server Configuration** tab on the **Analysis Services Configuration** page to specify the server mode and the users or accounts that have administrator permissions for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]. The server mode determines which memory and storage subsystems are used on the server. Different solution types run in different server modes. **Tabular mode** is the default.

  You must specify at least one system administrator for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]:

  - To add the account under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, select **Add Current User**.

  - To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that have administrator privileges for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)].

  For more information about server mode and administrator permissions, see [Analysis Services Configuration](../../sql-server/install/instance-configuration.md#analysis-services-configuration---account-provisioning-page).

  When you're finished editing the list, select **OK**. Verify the list of administrators in the configuration dialog box.

- Use the **Data Directories** tab on the **Analysis Services Configuration** page to specify non-default installation directories. To install to the default directories, select **Next**.

  > [!IMPORTANT]  
  > When installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if you specify the same directory path for `INSTANCEDIR` and `SQLUSERDBDIR`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and Full Text Search won't start due to missing permissions.  
  >  
  > If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

  For more information, see [Analysis Services Configuration - Data Directories page](../../sql-server/install/instance-configuration.md#analysis-services-configuration---data-directories-page).

#### <a id="distributed-replay-controller-2016-2017"></a> 18. Distributed Replay Controller

Use the **Distributed Replay Controller** page to specify the users you want to grant administrative permissions to for the Distributed Replay controller service. Users that have administrative permissions have unlimited access to the Distributed Replay controller service.

- To grant access permissions for the Distributed Replay controller service to the user who's running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup, select the **Add Current User** button.

- To grant access permissions for the Distributed Replay controller service to other users, select the **Add** button.

- To remove access permissions from the Distributed Replay controller service, select the **Remove** button.

- To continue, select **Next**.

#### <a id="distributed-replay-client-2016-2017"></a> 19. Distributed Replay Client

Use the **Distributed Replay Client** page to specify the users you want to grant administrative permissions to for the Distributed Replay client service. Users that have administrative permissions have unlimited access to the Distributed Replay client service.

- **Controller Name** is optional. The default value is \<*blank*>. Enter the name of the controller that the client computer will communicate with for the Distributed Replay client service:

  - If you've already set up a controller, enter the name of the controller while configuring each client.

  - If you haven't yet set up a controller, you can leave the controller name blank. However, you must manually enter the controller name in the **client configuration** file.

- Specify the **Working Directory** for the Distributed Replay client service. The default working directory is \<*drive letter*>:\Program Files\\[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]\DReplayClient\WorkingDir\\.

- Specify the **Result Directory** for the Distributed Replay client service. The default result directory is \<*drive letter*>:\Program Files\\[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]\DReplayClient\ResultDir\\.

- To continue, select **Next**.

#### <a id="ready-to-install-2016-2017"></a> 20. Ready to Install

The **Ready to Install** page shows a tree view of the installation options that you specified during Setup. On this page, Setup indicates whether the **Product Update** feature is enabled or disabled and the final update version.

To continue, select **Install**. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup first installs the required prerequisites for the selected features, then it installs the selected features.

#### <a id="installation-progress-2016-2017"></a> 21. Installation Progress

During installation, the **Installation Progress** page provides status updates so that you can monitor the installation progress as Setup continues.

#### <a id="complete-2016-2017"></a> 22. Complete

After installation, the **Complete** page provides a link to the summary log file for the installation and other important notes.

> [!IMPORTANT]  
> Make sure you read the message from the Installation Wizard when you've finished with Setup. For more information, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

To complete the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation process, select **Close**.

#### <a id="restart-2016-2017"></a> 23. Restart

If you're instructed to restart the computer, do so now.

::: moniker-end

::: moniker range="=sql-server-ver15"

## Install SQL Server 2019

#### <a id="installation-media-2019"></a> 1. Installation media

Insert the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click **setup.exe**. To install from a network share, locate the root folder on the share, and then double-click **setup.exe**.

#### <a id="installation-center-2019"></a> 2. SQL Server Installation Center

The Installation Wizard runs the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To create a new installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], select **Installation** in the left navigation area, and then select **New [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] standalone installation or add features to an existing installation**.

#### <a id="product-key-2019"></a> 3. Product Key

On the **Product Key** page, select an option to indicate whether you're installing a free edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or a production version that has a PID key. For more information, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

To continue, select **Next**.

#### <a id="license-terms-2019"></a> 4. License Terms

On the **License Terms** page, review the license agreement. If you agree, select the **I accept the license terms and [privacy statement](https://privacy.microsoft.com/privacystatement)** check box, and then select **Next**.

> [!NOTE]  
> If an Enterprise Server/CAL license product key is entered, and the machine has more than 20 physical cores, or 40 logical cores when simultaneous multithreading (SMT) is enabled, a warning is shown during setup. You can still continue setup by selecting the **Check this box to acknowledge this limitation or select Back/Cancel to enter an Enterprise Core product license that supports the operating system maximum** check box, or select **Back** and enter a License Key that supports the operating system maximum number of processors.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] transmits information about your installation experience, as well as other usage and performance data to help Microsoft improve the product. To learn more about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data processing and privacy controls, see the [privacy statement](https://privacy.microsoft.com/privacystatement) and [Configure SQL Server to send feedback to Microsoft](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md).

#### <a id="global-rules-2019"></a> 5. Global Rules

In the **Global Rules** page, Setup will automatically advance to the **Product Updates** page if there are no rule errors.

#### <a id="microsoft-update-2019"></a> 6. Microsoft Update

The **Microsoft Update** page appears next if the **Microsoft Update** check box in **Control Panel** > **All Control Panel Items** > **Windows Update** > **Change settings** isn't selected. Selecting the **Microsoft Update** check box changes the computer settings to include the latest updates for all Microsoft products when you scan for Windows updates.

#### <a id="product-updates-2019"></a> 7. Product Updates

On the **Product Updates** page, the latest available [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product updates are displayed. If no product updates are discovered, Setup doesn't display this page and automatically advances to the **Install Setup Files** page.

#### <a id="install-setup-files-2019"></a> 8. Install Setup Files

On the **Install Setup Files** page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for Setup is found and you specify to include it, that update will also be installed. If no update is found, Setup will automatically advance.

#### <a id="install-rules-2019"></a> 9. Install Rules

On the **Install Rules** page, Setup checks for potential problems that might occur while running Setup. If failures occur, select an item in the **Status** column for more information. Otherwise, select **Next**.

#### <a id="installation-type-2019"></a> 10. Installation Type

If this is the first installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the machine, Setup skips the **Installation Type** page and goes directly to the **Feature Selection** page. If [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is already installed on the system, you can use the **Installation Type** page to select either to perform a new installation, or to add features to an existing installation. To continue, select **Next**.

#### <a id="feature-selection-2019"></a> 11. Feature Selection

On the **Feature Selection** page, select the components for your installation. For example, to install a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)], select **Database Engine Services**.

A description for each component group appears in the **Feature description** pane after you select the feature name. You can select any combination of check boxes. For more information, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

The prerequisites for the selected features are displayed in the **Prerequisites for selected features** pane. Setup installs the prerequisites that aren't already installed during the installation step described later in this procedure.

You can also specify a custom directory for shared components by using the field at the bottom of the **Feature Selection** page. To change the installation path for shared components, either update the path in the field at the bottom of the dialog box or select **Browse** to go to an installation directory. The default installation path is [!INCLUDE [ssInstallPath](../../includes/ssinstallpath-md.md)].

> [!NOTE]  
> The path specified for the shared components must be an absolute path. The folder must not be compressed or encrypted. Mapped drives aren't supported.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses two directories for shared features:

- Shared feature directory
- Shared feature directory (x86)

> [!NOTE]  
> The path specified for each of the above options must be different.

#### <a id="feature-rules-2019"></a> 12. Feature Rules

The **Feature Rules** page automatically advances if all rules pass.

#### <a id="instance-configuration-2019"></a> 13. Instance Configuration

On the **Instance Configuration** page, specify whether to install a default instance or a named instance. For more information, see [Instance configuration](../../sql-server/install/instance-configuration.md#instance-configuration-page).

- **Instance ID**: By default, the instance name is used as the instance ID. This ID is used to identify the installation directories and registry keys for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The same behavior occurs for default instances and named instances. For a default instance, the instance name and instance ID are `MSSQLSERVER`. To use a nondefault instance ID, specify a different value in the **Instance ID** text box.

  > [!NOTE]  
  > Typical standalone instances of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)], whether default or named instances, don't use a nondefault value for the instance ID.

  All [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades apply to every component of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- **Installed instances**: The grid shows the instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running. If a default instance is already installed on the computer, you must install a named instance of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)].

The workflow for the rest of the installation depends on the features that you've specified for your installation. Depending on your selections, you might not see all the pages.

#### <a id="java-install-location-2019"></a> 14. Java Install Location

Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], PolyBase no longer requires that Oracle JRE 7 Update 51 (at least) be pre-installed on the computer prior to installing the feature. Selecting to install the PolyBase feature will add the **Java Install Location** page to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup displayed after the **Instance Configuration** page. On the Java Install Location page, you can choose to install the Azul Zulu Open JRE included with the [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] installation, or provide a location of a different JRE or JDK that has already been installed on the computer.

Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], Java has been added with Language Extensions. Selecting to install the Java feature will add the **Java Install Location** page to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup dialog window, displayed after the **Instance Configuration** page. On the **Java Install Location** page, you can choose to install the Zulu Open JRE included with the [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] installation, or provide a location of a different JRE or JDK that has already been installed on the computer.

#### <a id="server-configuration-2019"></a> 15. Server Configuration

Use the **Service Accounts** tab under the **Server Configuration** page to specify the accounts for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that you configure on this page depend on the features that you selected to install. For more information about configuration settings, see [Installation Wizard help](../../sql-server/install/instance-configuration.md#serverconfig).

You can assign the same account to all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, start manually, or are disabled. We recommend you configure service accounts individually to provide the least privileges for each service. Make sure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they must have to complete their tasks. For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

To specify the same account for all service accounts in this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], provide the credentials in the fields at the bottom of the page.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

> [!NOTE]  
> Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], select the **Grant Perform Volume Maintenance Task privilege to SQL Server Database Engine Service** check box to allow the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] service account to use [database instant file initialization](../../relational-databases/databases/database-instant-file-initialization.md).

Use the **Collation** tab under the **Server Configuration** page to specify nondefault collations for the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information, see [Collations and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

#### <a id="database-engine-configuration-2019"></a> 16. Database Engine Configuration

- Use the **Server Configuration** tab under the **Database Engine Configuration** page to specify the following options:

  - **Security Mode**: Select **Windows Authentication** or **Mixed Mode Authentication** for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you select **Mixed Mode Authentication**, you must provide a strong password for the built-in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account (sa).

    After a device establishes a successful connection to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the security mechanism is the same for both Windows authentication and mixed mode authentication. For more information, see [Database Engine Configuration - Server Configuration page](../../sql-server/install/instance-configuration.md#serverconfig).

  - **SQL Server Administrators**: You must specify at least one system administrator for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To add the account under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, select **Add Current User**. To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that have administrator privileges for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  You can also add a Windows Domain Group, to establish a shared SQL Administrator Group in Active Directory with sysadmin Access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Use the **Data Directories** tab under the **Database Engine Configuration** page to specify nondefault installation directories. To install to the default directories, select **Next**.

  > [!IMPORTANT]  
  > If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

  For more information, see [Database Engine Configuration - Data Directories page](../../sql-server/install/instance-configuration.md#datadir).

- Use the **TempDB** tab under the **Database Engine Configuration** page to configure the file size, number of files, nondefault installation directories, and file-growth settings for `tempdb`. For more information, see [Database Engine Configuration - TempDB page](../../sql-server/install/instance-configuration.md#tempdb).

- Use the **MaxDOP** tab under the **[!INCLUDE [ssDE](../../includes/ssde-md.md)] Configuration** page to specify your max degree of parallelism. This setting determines how many processors a single statement can use during execution. The recommended value is automatically calculated during installation.

  > [!NOTE]  
  > This page is only available in Setup starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)].

  For more information, see the [Database Engine Configuration - MaxDOP page](../../sql-server/install/instance-configuration.md?view=sql-server-ver15&preserve-view=true#maxdop).

- Use the **Memory** tab under the **Database Engine Configuration** page to specify the **min server memory** and **max server memory** values that this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] will use after startup. You can use the default values, use the calculated recommended values, or manually specify your own values after you've chosen the **Recommended** option.

  > [!NOTE]  
  > This page is only available in Setup starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)].

  For more information, see the [Database Engine Configuration - Memory page](../../sql-server/install/instance-configuration.md?view=sql-server-ver15&preserve-view=true#memory).

- Use **FILESTREAM** tab under the **Database Engine Configuration** page to enable FILESTREAM for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Database Engine Configuration - FILESTREAM page](../../sql-server/install/instance-configuration.md#database-engine-configuration---filestream-page).

#### <a id="analysis-services-configuration-2019"></a> 17. Analysis Services Configuration

Use the **Analysis Services Configuration - Account Provisioning** page to specify the server mode and the users or accounts that have administrator permissions for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]. The server mode determines which memory and storage subsystems are used on the server. Different solution types run in different server modes. If you plan to run multidimensional cube databases on the server, select the default server mode option, **Multidimensional and Data Mining**.

You must specify at least one system administrator for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]:

- To add the account under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, select **Add Current User**.

- To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that have administrator privileges for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)].

For more information about server mode and administrator permissions, see [Analysis Services Configuration - Account Provisioning page](../../sql-server/install/instance-configuration.md#analysis-services-configuration---account-provisioning-page).

When you're finished editing the list, select **OK**. Verify the list of administrators in the configuration dialog box. After the list is complete, select **Next**.

Use the **Data Directories** tab under the **Analysis Services Configuration** page to specify nondefault installation directories. To install to the default directories, select **Next**.

> [!IMPORTANT]  
> When installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if you specify the same directory path for `INSTANCEDIR` and `SQLUSERDBDIR`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and Full Text Search won't start due to missing permissions.  
>  
> If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

For more information, see [Analysis Services Configuration - Data Directories page](../../sql-server/install/instance-configuration.md#analysis-services-configuration---data-directories-page).

#### <a id="distributed-replay-controller-2019"></a> 18. Distributed Replay Controller

Use the **Distributed Replay Controller** page to specify the users you want to grant administrative permissions to for the Distributed Replay controller service. Users that have administrative permissions have unlimited access to the Distributed Replay controller service.

- To grant access permissions for the Distributed Replay controller service to the user who's running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup, select the **Add Current User** button.

- To grant access permissions for the Distributed Replay controller service to other users, select the **Add** button.

- To remove access permissions from the Distributed Replay controller service, select the **Remove** button.

- To continue, select **Next**.

#### <a id="distributed-replay-client-2019"></a> 19. Distributed Replay Client

Use the **Distributed Replay Client** page to specify the users you want to grant administrative permissions to for the Distributed Replay client service. Users that have administrative permissions have unlimited access to the Distributed Replay client service.

- **Controller Name** is optional. The default value is \<*blank*>. Enter the name of the controller that the client computer will communicate with for the Distributed Replay client service:

  - If you've already set up a controller, enter the name of the controller while configuring each client.

  - If you haven't yet set up a controller, you can leave the controller name blank. However, you must manually enter the controller name in the **client configuration** file.

- Specify the **Working Directory** for the Distributed Replay client service. The default working directory is \<*drive letter*>:\Program Files\\[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]\DReplayClient\WorkingDir\\.

- Specify the **Result Directory** for the Distributed Replay client service. The default result directory is \<*drive letter*>:\Program Files\\[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]\DReplayClient\ResultDir\\.

- To continue, select **Next**.

#### <a id="ready-to-install-2019"></a> 20. Ready to Install

The **Ready to Install** page shows a tree view of the installation options that you specified during Setup. On this page, Setup indicates whether the **Product Update** feature is enabled or disabled and the final update version.

   To continue, select **Install**. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup first installs the required prerequisites for the selected features, then it installs the selected features.

#### <a id="installation-progress-2019"></a> 21. Installation Progress

During installation, the **Installation Progress** page provides status updates so that you can monitor the installation progress as Setup continues.

#### <a id="complete-2019"></a> 22. Complete

After installation, the **Complete** page provides a link to the summary log file for the installation and other important notes.

> [!IMPORTANT]  
> Make sure you read the message from the Installation Wizard when you've finished with Setup. For more information, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

To complete the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation process, select **Close**.

#### <a id="restart-2019"></a> 23. Restart

If you're instructed to restart the computer, do so now.

::: moniker-end

::: moniker range="=sql-server-ver16"

## Install SQL Server 2022

#### <a id="installation-media-2022"></a> 1. Installation media

Insert the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click **setup.exe**. To install from a network share, locate the root folder on the share, and then double-click **setup.exe**.

#### <a id="installation-center-2022"></a> 2. SQL Server Installation Center

The Installation Wizard runs the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To create a new installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], select **Installation** in the left navigation area, and then select **New [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] standalone installation or add features to an existing installation**.

#### <a id="edition-2022"></a> 3. Edition

On the **Edition** page, select the edition you want to install.

- **Specify a free edition** allows you to select Evaluation, Developer, or Web edition.
- **Use pay-as-you-go billing through Microsoft Azure** is an alternative to using the traditional license agreement. [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] introduces this option in setup and allows you to activate your instance for use in production without supplying a product key. This option requires an active Azure subscription. For more information, see [Manage SQL Server license type](../../sql-server/azure-arc/manage-configuration.md). With this option you can specify Standard or Enterprise edition.
- **Enter the product key** allows you to provide a product key for a specific edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You may also specify if you have a license with Software Assurance or SQL Software Subscription, and if you have a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] license only.

To continue, select **Next**.

#### <a id="license-terms-2022"></a> 4. License Terms

On the **License Terms** page, review the license agreement. If you agree, select the **I accept the license terms and [Privacy Statement](https://privacy.microsoft.com/privacystatement)** check box, and then select **Next**.

> [!NOTE]  
> If an Enterprise Server/CAL license product key is entered, and the machine has more than 20 physical cores, or 40 logical cores when simultaneous multithreading (SMT) is enabled, a warning is shown during setup. You can still continue setup by selecting the **Check this box to acknowledge this limitation or select Back/Cancel to enter an Enterprise Core product license that supports the operating system maximum** check box, or select **Back** and enter a product key that supports the operating system maximum number of processors.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] transmits information about your installation experience, as well as other usage and performance data to help Microsoft improve the product. To learn more about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data processing and privacy controls, see the [privacy statement](https://privacy.microsoft.com/privacystatement) and [Configure SQL Server to send feedback to Microsoft](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md).

#### <a id="global-rules-2022"></a> 5. Global Rules

In the **Global Rules** page, Setup automatically advances to the **Microsoft Update** page if there are no rule errors.

#### <a id="microsoft-update-2022"></a> 6. Microsoft Update

1. The **Microsoft Update** page appears next if the **Microsoft Update** check box in **Control Panel** > **All Control Panel Items** > **Windows Update** > **Change settings** isn't selected. Selecting the **Microsoft Update** check box changes the computer settings to include the latest updates for all Microsoft products when you scan for Windows updates.

#### <a id="product-updates-2022"></a> 7. Product Updates

On the **Product Updates** page, the latest available [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] critical product updates are displayed. If no product updates are discovered, Setup doesn't display this page and automatically advances to the **Install Setup Files** page.

#### <a id="install-setup-files-2022"></a> 8. Install Setup Files

On the **Install Setup Files** page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for Setup is found and you specify to include it, that update will also be installed. If no update is found, Setup will automatically advance.

#### <a id="install-rules-2022"></a> 9. Install Rules

On the **Install Rules** page, Setup checks for potential problems that might occur while running Setup. If failures occur, select an item in the **Status** column for more information. Otherwise, select **Next**.

#### <a id="azure-extension-for-sql-server-2022"></a> 10. Azure Extension for SQL Server

On the **Azure Extension for SQL Server** page, you can configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to connect to Azure. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces this extension to enable using Azure services such as Microsoft Defender for Cloud, Microsoft Purview, Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), and others. This feature is selected by default. If you wish to proceed without connecting to Azure, you can unselect **Azure Extension for SQL Server**.

- If you're installing [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on a VMware ESX host or an Azure VMware Solution (AVS) host, you won't see the **Azure Extension for SQL Server** page during installation. You can [install the extension during setup using the command line parameters](install-sql-server-from-the-command-prompt.md) or you can install [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] without the Azure Extension for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] first and then later install the [Azure Extension for SQL Server](../../sql-server/azure-arc/connect.md).

- If you're installing [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on an Azure VM, you won't see the **Azure Extension for SQL Server** page during installation. Connectivity to Azure Services for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Azure VMs is handled through the [SQL IaaS Agent Extension](/azure/azure-sql/virtual-machines/windows/sql-server-iaas-agent-extension-automate-management), which is automatically pushed to your Azure VM shortly after [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation. You can register your VM with the extension [manually](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm) if you don't want to wait for automatic registration. For more information on supported configurations, see [Supported SQL Server versions and environments](../../sql-server/azure-arc/prerequisites.md#supported-sql-server-versions-and-environments).

To use the Azure extension for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you must have an active Azure subscription and provide a set of additional Azure-related parameters. You also need to make sure the following [Azure resource providers](../../sql-server/azure-arc/prerequisites.md) are registered in your subscription:

- **Microsoft.AzureArcData**
- **Microsoft.HybridCompute**

To authenticate the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance with Azure, you can sign in using an Azure account, or you can use an Azure service principal. For specific security requirements to install the extension, see [Required permissions](../../sql-server/azure-arc/prerequisites.md).

To sign in with your Azure account, select **Use Azure Login**. Windows may prompt you to add one or more sites to the Trusted sites zone. Follow your organization's security requirements. After you sign in to Azure, proceed to provide the additional registration information.

Alternatively, you can use a service principal:

- **Azure service principal**: If you provide the service principal, provide the service principal secret. This is used to authenticate the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to Azure.
- **Azure subscription ID**: Azure subscription where the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance resource is created.

Provide the following information:

- **Azure resource group**: Azure resource group where the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance resource is created.
- **Azure region**: Azure region where the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance resource is created.
- **Azure tenant ID**: Azure tenant ID in which the service principal exists.
- **Proxy server URL**: (Optional) - Name of the HTTP proxy server used to connect to Azure Arc.

> [!NOTE]  
> To create a service principal, retrieve its password and Tenant ID, see [Connect multiple SQL Server instances to Azure Arc](../../sql-server/azure-arc/connect-at-scale-policy.md). If the server is already connected to Azure via Azure Arc, the subscription ID, resource group, and region will be populated and you won't be able to change them.

Select **Next** to proceed.

#### <a id="feature-selection-2022"></a> 11. Feature Selection

On the **Feature Selection** page, select the components for your installation. For example, to install a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)], select **Database Engine Services**.

A description for each component group appears in the **Feature description** pane after you select the feature name. You can select any combination of check boxes.

The prerequisites for the selected features are displayed in the **Prerequisites for selected features** pane. Setup installs the prerequisites that aren't already installed during the installation step described later in this procedure.

You can also specify a custom directory for shared components by using the field at the bottom of the **Feature Selection** page. To change the installation path for shared components, either update the path in the field at the bottom of the dialog box or select **Browse** to go to an installation directory. The default installation path is [!INCLUDE [ssInstallPath](../../includes/ssinstallpath-md.md)].

> [!NOTE]  
> The path specified for the shared components must be an absolute path. The folder must not be compressed or encrypted. Mapped drives aren't supported.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses two directories for shared features:

- Shared feature directory
- Shared feature directory (x86)

> [!NOTE]  
> The path specified for each of the above options must be different.

#### <a id="feature-rules-2022"></a> 12. Feature Rules

The **Feature Rules** page automatically advances if all rules pass.

#### <a id="instance-configuration-2022"></a> 13. Instance Configuration

On the **Instance Configuration** page, specify whether to install a default instance or a named instance. For more information, see [Instance configuration](../../sql-server/install/instance-configuration.md#instance-configuration-page).

- **Instance ID**: By default, the instance name is used as the instance ID. This ID is used to identify the installation directories and registry keys for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The same behavior occurs for default instances and named instances. For a default instance, the instance name and instance ID are `MSSQLSERVER`. To use a nondefault instance ID, specify a different value in the **Instance ID** text box.

  > [!NOTE]  
  > Typical standalone instances of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)], whether default or named instances, don't use a nondefault value for the instance ID.

  All [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades apply to every component of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- **Installed instances**: The grid shows the instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running. If a default instance is already installed on the computer, you must install a named instance of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)].

The workflow for the rest of the installation depends on the features that you've specified for your installation. Depending on your selections, you might not see all the pages.

#### <a id="java-install-location-2022"></a> 14. Java Install Location

Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], PolyBase no longer requires that Oracle JRE 7 Update 51 (at least) be pre-installed on the computer prior to installing the feature. Selecting to install the PolyBase feature will add the **Java Install Location** page to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup displayed after the **Instance Configuration** page. On the Java Install Location page, you can choose to install the Azul Zulu Open JRE included with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation, or provide a location of a different JRE or JDK that has already been installed on the computer.

Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], Java has been added with Language Extensions. Selecting to install the Java feature will add the **Java Install Location** page to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup dialog window, displayed after the **Instance Configuration** page. On the **Java Install Location** page, you can choose to install the Zulu Open JRE included with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation, or provide a location of a different JRE or JDK that has already been installed on the computer.

#### <a id="server-configuration-2022"></a> 15. Server Configuration

- On the **Server Configuration** page, use the **Service Accounts** tab to specify the accounts for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that you configure on this page depend on the features that you selected to install. For more information about configuration settings, see [Installation Wizard help](../../sql-server/install/instance-configuration.md#serverconfig).

  You can assign the same account to all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, start manually, or are disabled. We recommend you configure service accounts individually to provide the least privileges for each service. Make sure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they must have to complete their tasks. For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

  To specify the same account for all service accounts in this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], provide the credentials in the fields at the bottom of the page.

  > [!IMPORTANT]  
  > [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

  > [!NOTE]  
  > Select the **Grant Perform Volume Maintenance Task privilege to SQL Server Database Engine Service** check box to allow the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] service account to use [database instant file initialization](../../relational-databases/databases/database-instant-file-initialization.md).

- On the **Server Configuration** page use the **Collation** tab to specify nondefault collations for the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information, see [Collations and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

#### <a id="database-engine-configuration-2022"></a> 16. Database Engine Configuration

- Use the **Server Configuration** tab on the **Database Engine Configuration** page to specify the following options:

  - **Security Mode**: Select **Windows Authentication** or **Mixed Mode Authentication** for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you select **Mixed Mode Authentication**, you must provide a strong password for the built-in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account (sa).

    After a device establishes a successful connection to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the security mechanism is the same for both Windows authentication and mixed mode authentication. For more information, see [Database Engine Configuration - Server Configuration page](../../sql-server/install/instance-configuration.md#serverconfig).

  - **SQL Server Administrators**: You must specify at least one system administrator for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To add the account under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, select **Add Current User**. To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that have administrator privileges for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  You can also add a Windows Domain Group, to establish a shared SQL Administrator Group in Active Directory with sysadmin Access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Use the **Data Directories** tab to specify nondefault installation directories. To install to the default directories, select **Next**.

  > [!IMPORTANT]  
  > If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

  For more information, see [Database Engine Configuration - Data Directories page](../../sql-server/install/instance-configuration.md#datadir).

- Use the **TempDB** tab to configure the file size, number of files, nondefault installation directories, and file-growth settings for `tempdb`. For more information, see [Database Engine Configuration - TempDB page](../../sql-server/install/instance-configuration.md#tempdb).

- Use the **MaxDOP** tab to specify your max degree of parallelism. This setting determines how many processors a single statement can use during execution. The recommended value is automatically calculated during installation.

  For more information, see the [Database Engine Configuration - MaxDOP page](../../sql-server/install/instance-configuration.md?view=sql-server-ver15&preserve-view=true#maxdop).

- Use the **Memory** tab to specify the **min server memory** and **max server memory** values that this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] will use after startup. You can use the default values, use the calculated recommended values, or manually specify your own values after you've chosen the **Recommended** option.

  For more information, see the [Database Engine Configuration - Memory page](../../sql-server/install/instance-configuration.md?view=sql-server-ver15&preserve-view=true#memory).

- Use the **FILESTREAM** tab to enable FILESTREAM for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Database Engine Configuration - FILESTREAM page](../../sql-server/install/instance-configuration.md#database-engine-configuration---filestream-page).

#### <a id="analysis-services-configuration-2022"></a> 17. Analysis Services Configuration

On the **Analysis Services Configuration** use the **Account Provisioning** tab to specify the server mode and the users or accounts that have administrator permissions for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]. The server mode determines which memory and storage subsystems are used on the server. Different solution types run in different server modes. If you plan to run multidimensional cube databases on the server, select the default server mode option, **Multidimensional and Data Mining**.

You must specify at least one system administrator for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]:

- To add the account under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, select **Add Current User**.

- To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that have administrator privileges for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)].

  For more information about server mode and administrator permissions, see [Analysis Services Configuration - Account Provisioning page](../../sql-server/install/instance-configuration.md#analysis-services-configuration---account-provisioning-page).

  When you're finished editing the list, select **OK**. Verify the list of administrators in the configuration dialog box. After the list is complete, select **Next**.

On the **Analysis Services Configuration** use the **Data Directories** page to specify nondefault installation directories. To install to the default directories, select **Next**.

> [!IMPORTANT]  
> When installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if you specify the same directory path for `INSTANCEDIR` and `SQLUSERDBDIR`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and Full Text Search won't start due to missing permissions.  
>  
> If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

For more information, see [Analysis Services Configuration - Data Directories page](../../sql-server/install/instance-configuration.md#analysis-services-configuration---data-directories-page).

#### <a id="ready-to-install-2022"></a> 18. Ready to Install

The **Ready to Install** page shows a tree view of the installation options that you specified during Setup. On this page, Setup indicates whether the **Product Update** feature is enabled or disabled and the final update version.

To continue, select **Install**. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup first installs the required prerequisites for the selected features, then it installs the selected features.

#### <a id="installation-progress-2022"></a> 19. Installation Progress

1. During installation, the **Installation Progress** page provides status updates so that you can monitor the installation progress as Setup continues.

#### <a id="complete-2022"></a> 20. Complete

After installation, a *Successful* status on the **Complete** page indicates a successful completion. This page provides a link to the summary log file for the installation and other important notes.

> [!IMPORTANT]  
> Make sure you read the message from the Installation Wizard when you've finished with Setup. For more information, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

To complete the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation process, select **Close**.

If you're instructed to restart the computer, do so now.

::: moniker-end

## Related content

- [Database Engine Instances (SQL Server)](../configure-windows/database-engine-instances-sql-server.md)
- [Surface area configuration](../../relational-databases/security/surface-area-configuration.md)
- [Validate a SQL Server Installation](validate-a-sql-server-installation.md)
- [Repair a failed SQL Server installation](repair-a-failed-sql-server-installation.md)
- [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md)
- [Upgrade to SQL Server by using the Installation Wizard (Setup)](upgrade-sql-server-using-the-installation-wizard-setup.md)
- [Install SQL Server from the command prompt](install-sql-server-from-the-command-prompt.md)
