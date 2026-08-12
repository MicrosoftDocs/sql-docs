---
title: "Install SQL Server Using SysPrep"
description: This article describes how to prepare and complete images by using SysPrep in SQL Server installation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
monikerRange: ">=sql-server-2017"
ms.custom:
  - intro-installation
  - sfi-ropc-blocked
---
# Install SQL Server with SysPrep

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep related setup actions can be accessed through the Installation Center. The **Advanced** Page of the **Installation Center** has two options - **Image preparation of a stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]** and **Image completion of a prepared stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]**. The [Prepare](#prepare) and [Complete](#complete) sections describe the installation process in detail. For more information, see [Considerations for installing SQL Server using SysPrep](considerations-for-installing-sql-server-using-sysprep.md).

You can also prepare and complete an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using the command prompt or a configuration file. For more information, see:

- [Install and configure SQL Server on Windows from the command prompt](install-sql-server-from-the-command-prompt.md)
- [Install SQL Server using a configuration file](install-sql-server-using-a-configuration-file.md)

## Prerequisites

Before you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], review the articles in [Plan a SQL Server installation](../../sql-server/install/planning-a-sql-server-installation.md).

For more information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] editions and the hardware and software requirements, see:

[!INCLUDE [editions-supported-features-windows](../../includes/editions-supported-features-windows.md)]

<a id="sysprep"></a>

## SQL Server SysPrep cluster support

Beginning in [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], SysPrep supports clustered [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances in command line installations. For more information, see [What is Sysprep?](/previous-versions/windows/it-pro/windows-vista/cc721940(v=ws.10))

### Prepare a SQL Server failover cluster (unattended)

1. Prepare the image (as discussed in [Considerations for installing SQL Server using SysPrep](considerations-for-installing-sql-server-using-sysprep.md)) and capture the Windows image through SysPrep Generalization. The following sample prepares the image:

   ```console
   Setup.exe /q /ACTION=PrepareImage l /FEATURES=SQLEngine /InstanceID =<MYINST> /IACCEPTSQLSERVERLICENSETERMS
   ```

   Then run Windows SysPrep Generalization.

1. Deploy the image by running Windows SysPrep Specialize.

1. Create the Windows Failover Cluster.

1. Run setup.exe with **/ACTION=PrepareFailoverCluster** all nodes. For example:

   ```console
   setup.exe /q /ACTION=PrepareFailoverCluster /InstanceName=<InstanceName> /Features=SQLEngine /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="xxxxxxxxxxx" /IACCEPTSQLSERVERLICENSETERMS
   ```

### Complete a SQL Server failover cluster (Unattended)

Run setup.exe with **/ACTION=CompleteFailoverCluster** on the node that owns the available storage group:

```console
setup.exe /q /ACTION=CompleteFailoverCluster /InstanceName=<InstanceName> /FAILOVERCLUSTERDISKS="<Cluster Disk Resource Name - for example, 'Disk S:'>:" /FAILOVERCLUSTERNETWORKNAME="<Insert FOI Network Name>" /FAILOVERCLUSTERIPADDRESSES="IPv4;xx.xxx.xx.xx;Cluster Network;xxx.xxx.xxx.x" /FAILOVERCLUSTERGROUP="MSSQLSERVER" /INSTALLSQLDATADIR="<Drive>:\<Path>\MSSQLSERVER" /SQLCOLLATION="SQL_Latin1_General_CP1_CS_AS" /SQLSYSADMINACCOUNTS="<DomainName\UserName>"
```

### Add a node to an existing SQL Server failover cluster (unattended)

1. Deploy the image by running Windows SysPrep Specialize.

1. Join the Windows Failover Cluster.

1. Run setup.exe with **/ACTION=AddNode** on all nodes:

   ```console
   setup.exe /q /ACTION=AddNode /InstanceName=<InstanceName> /Features=SQLEngine /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="xxxxxxxxxxx" /IACCEPTSQLSERVERLICENSETERMS
   ```

<a id="prepare"></a>

## Prepare a stand-alone instance of SQL Server

1. Insert the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click Setup.exe. To install from a network share, locate the root folder on the share, and then double-click Setup.exe.

1. The Installation Wizard runs the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To prepare an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], select **Image preparation of a stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]** on the **Advanced** page.

1. The System Configuration Checker runs a discovery operation on your computer. To continue, select **OK**. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. On the Product Updates page, the latest available [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product updates are displayed. If you don't want to include the updates, clear the **Include [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product updates** check box. If no product updates are discovered, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup doesn't display this page and auto advances to the **Install Setup Files** page.

1. On the Install Setup files page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is found, and is specified to be included, that update is also installed.

1. The System Configuration Checker verifies the system state of your computer before Setup continues. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. On the **Prepare Image Type** page, select **Prepare a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]**.

   The **Prepare Image Type** page is displayed only when you have an existing unconfigured prepared instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the machine. You can choose to prepare a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or add sys prep supported features to an existing prepared instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the machine. For more information on how to add features to a prepared instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Add Features to a prepared instance](#AddFeatures).

1. On the **License Terms** page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE [msCoName](../../includes/msconame-md.md)].

   [!INCLUDE [sql-eula-link](../../includes/sql-eula-link.md)]

1. On the **Feature Selection** page, select the components for your installation:

   | Installation | Components |
   | --- | --- |
   | [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] SysPrep | [!INCLUDE [ssDE](../../includes/ssde-md.md)]<br />[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Replication<br />Full-Text Features<br />Data Quality Services<br />[!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] in Native mode<br />[!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]<br />Redistributable Features<br />Shared Features |

   A description for each component group appears in the right pane when you highlight the feature name. You can select any combination of check boxes. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

   The prerequisites for the selected features are displayed on the right-hand pane. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the prerequisites that aren't already installed during the installation step described later in this procedure.

1. On the **Prepare Image Rules** page, the System Configuration Checker verifies the system state of your computer before Setup continues. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. On the Instance Configuration page, specify the Instance ID for the Instance. Select **Next** to continue.

   **Instance ID** - The Instance ID is used to identify installation directories and registry keys for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. If the prepared instance is completed as a default Instance during the Complete step, the instance name is overwritten as MSSQLSERVER. The Instance ID remains the same as specified.

   **Instance root directory** - By default, the instance root directory is [!INCLUDE [ssInstallPath](../../includes/ssinstallpath-md.md)]. To specify a non-default root directory, use the field provided, or select **Browse** to locate an installation folder. The directory specified in the prepare step is used during configuration in the Complete step.

   All [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades apply to every component of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   **Installed Instances** - The grid shows instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running.

1. The **Disk Space Requirements** page calculates the required disk space for the features that you specify. Then it compares the required space to the available disk space.

1. The System Configuration Checker runs prepare image rules to validate your computer configuration with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] features that you have specified. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. The **Ready to Prepare Image** page shows a tree view of installation options that were specified during Setup. On this page, Setup indicates whether the Product Update feature is enabled or disabled and the final update version. To continue, select **Prepare**. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup first installs the required prerequisites for the selected features followed by the feature installation.

1. During installation, the **Prepare Image Progress** page provides status so that you can monitor installation progress as Setup continues.

1. After installation, the **Complete** page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation process, select **Close**.

1. If you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you finish with Setup. For more information, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

1. This completes the prepare step. You might complete the image or deploy the prepared image as described in [Considerations for installing SQL Server using SysPrep](considerations-for-installing-sql-server-using-sysprep.md).

<a id="complete"></a>

## Complete a prepared instance of SQL Server

1. If you have a prepared instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] included in the image of your machine, you see a shortcut in the Start Menu. You can also launch the Installation Center and select **Image completion of a prepared stand-alone instance** on the **Advanced** page.

1. The System Configuration Checker runs a discovery operation on your computer. To continue, select **OK**. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. On the **Setup Support Files** page, select **Install** to install the Setup support files.

1. The System Configuration Checker verifies the system state of your computer before Setup continues. After the check is complete, select **Next** to continue. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. On the **Product Key** page, select an option button to indicate whether you're installing a free edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or a production version of the product that has a PID key. If you're installing Evaluation edition the 180-day trial period starts when you complete this step. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

1. On the **License Terms** page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE [msCoName](../../includes/msconame-md.md)].

1. On the **Select a Prepared Instance** page select the prepared instance you want to complete from the dropdown list box. Select the Unconfigured instance from the **Instance ID** list.

   **Installed instances:** This grid displays all the instances including any prepared instance on this machine.

1. On the **Feature Review** page, you see the selected features and components included in the install during the prepare step. If you wish to add more features to your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Instance not included in the prepared instance, you must first complete this step to complete the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Instance, then add the features from the **Add Features** on the **Installation Center**.

   You can add features that are available for the product version that you're installing. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

1. On the Instance Configuration page, specify the Instance name for the prepared Instance. This is the name of the instance once you have completed the configuration of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Select **Next** to continue.

   **Instance ID** - The Instance ID is used to identify installation directories and registry keys for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. If the prepared instance is completed as a default Instance during the Complete step, the instance name is overwritten as MSSQLSERVER. The Instance ID remains the same as specified during the Prepare step.

   **Instance root directory** - The directory specified in the prepare step is used, and can't be modified in this step.

   All [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades apply to every component of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   **Installed instances** - The grid shows instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running.

1. Work flow for the rest of this article depends on the features that were selected during the prepare step. You might not see all the pages, depending on the selections.

1. On the **Server Configuration** - Service Accounts page, specify login accounts for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that are configured on this page depend on the features that you selected to install.

   You can assign the same login account to all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, are started manually, or are disabled. [!INCLUDE [msCoName](../../includes/msconame-md.md)] recommends that you configure service accounts individually to provide least privileges for each service, where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they need to complete their tasks. For more information, see [SQL Server installation guide](install-sql-server.md) and [Configure Windows service accounts and permissions](../configure-windows/configure-windows-service-accounts-and-permissions.md).

   To specify the same account for all service accounts in this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], provide credentials in the fields at the bottom of the page.

   **Security Note** [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

   When you're finished specifying login information for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, select **Next**.

1. Use the **Server Configuration - Collation** tab to specify non-default collations for the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information, see [SQL Server installation guide](install-sql-server.md).

1. Use the [!INCLUDE [ssDE](../../includes/ssde-md.md)] Configuration - Account Provisioning page to specify:

   - Security Mode - Select Windows Authentication or Mixed Mode Authentication for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you select Mixed Mode Authentication, you must provide a strong password for the built-in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account.

     After a device establishes a successful connection to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the security mechanism is the same for both Windows Authentication and Mixed Mode. For more information, see [SQL Server installation guide](install-sql-server.md).

   - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Administrators - You must specify at least one system administrator for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To add the account under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, select **Add Current User**. To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that have administrator privileges for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SQL Server installation guide](install-sql-server.md).

   When you're finished editing the list, select **OK**. Verify the list of administrators in the configuration dialog box. When the list is complete, select **Next**.

1. Use the [!INCLUDE [ssDE](../../includes/ssde-md.md)] Configuration - Data Directories page to specify nondefault installation directories. To install to default directories, select **Next**.

   If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   For more information, see [SQL Server installation guide](install-sql-server.md).

1. Use the [!INCLUDE [ssDE](../../includes/ssde-md.md)] Configuration - FILESTREAM page to enable FILESTREAM for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SQL Server installation guide](install-sql-server.md).

1. Use the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration page to specify the kind of [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to create. For more information about [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration modes, see [SQL Server installation guide](install-sql-server.md).

1. On the **Error Reporting** page, specify the information that you want to send to [!INCLUDE [msCoName](../../includes/msconame-md.md)] that will help improve [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. By default, the option for error reporting is enabled.

1. On the **Complete Image Rules** page, the System Configuration Checker runs the complete image rules to validate your computer configuration with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] configurations that you have specified. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. The **Ready to Complete Image** page shows a tree view of installation options that were specified during Setup. To continue, select **Install**.

1. During installation, the **Complete Image Progress** page provides status so that you can monitor installation progress as Setup continues.

1. After installation, the **Complete** page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation process, select **Close**.

1. If you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you finish with Setup. For more information, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

1. This step completes the configuration of the prepared instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and you have completed the installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

<a id="AddFeatures"></a>

## Add features to a prepared instance of SQL Server

1. Insert the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click Setup.exe. To install from a network share, locate the root folder on the share, and then double-click Setup.exe.

1. The Installation Wizard runs the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To add features to a prepared instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], select **Image preparation of a stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]** on the **Advanced** page.

1. The System Configuration Checker runs a discovery operation on your computer. To continue, select **OK**. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. On the Setup Support Files page, select **Install** to install the Setup support files.

1. On the **Prepare Image Type** page, select **Add features to an existing prepared instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]** option. Select the specific prepared instance you want to add features to from the dropdown list of available prepared instances.

1. On the **Feature Selection** page, specify the features you want to add to the specified prepared instance.

   The prerequisites for the selected features are displayed on the right-hand pane. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the prerequisites that aren't already installed during the installation step described later in this procedure.

1. On the **Prepare Image Rules** page, the System Configuration Checker verifies the system state of your computer before Setup continues. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. The Disk Space Requirements page calculates the required disk space for the features that you specify. Then it compares the required space to the available disk space.

1. On the **Prepare Image Rules** page, the System Configuration Checker runs prepare image rules to validate your computer configuration with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] features that you have specified. You can view the details on the screen by selecting **Show Details**, or as an HTML report by selecting **View detailed report**.

1. The **Ready to Prepare Image** page shows a tree view of installation options that were specified during Setup. To continue, select **Install**. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup first installs the required prerequisites for the selected features followed by the feature installation.

1. During installation, the **Prepare Image Progress** page provides status so that you can monitor installation progress as Setup continues.

1. After installation, the **Complete** page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation process, select **Close**.

1. If you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you finish with Setup. For more information, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

<a id="RemoveFeatures"></a>

## Remove features from a prepared instance of SQL Server

1. To begin the uninstall process, from the **Start** menu select **Control Panel** and double-click **Program and Features**.

1. Double-click the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] component to uninstall and select **Remove**.

1. Setup support rules run to verify your computer configuration. Select **OK** to continue.

1. On the **Select Instance** page, select the prepared instance you want to modify. The name of the prepared instance is displayed as "Unconfigured PreparedInstanceID" where PreparedInstanceID is the instance you select.

1. On the **Select Features** page, specify the features to remove from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance you specified. Select **Next** to continue.

1. Removal rules run to verify that the operation can complete successfully.

1. On the **Ready to Remove** page, review the list of components and features that will be uninstalled.

1. The **Remove Progress** page displays the status of the operation.

1. On the Complete page, you can review the completion status of the operation. Select **Close** to exit the installation wizard.

<a id="Uninstall"></a>

## Uninstall a prepared instance of SQL Server

1. To begin the uninstall process, from the **Start** menu select **Control Panel** and double-click **Program and Features**.

1. Double-click the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] component to uninstall and select **Remove**.

1. Setup support rules run to verify your computer configuration. Select **OK** to continue.

1. On the **Select Instance** page, select the prepared instance you want to modify. The name of the prepared instance is displayed as "Unconfigured PreparedInstanceID" where PreparedInstanceID is the instance you select.

1. On the **Select Features** page, specify the features to remove from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance you specified. Select **Next** to continue.

1. On the **Removal Rules** page, Setup runs rules to verify that the operation can complete successfully.

1. On the **Ready to Remove** page, review the list of components and features that will be uninstalled.

1. The **Remove Progress** page displays the status of the operation.

1. On the Complete page, you can review the completion status of the operation. Select **Close** to exit the installation wizard.

1. Repeat steps 1 to 9 until all components of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] have been removed.

<a id="bk_Modifying_Uninstalling"></a>

## Modify or uninstall a completed instance of SQL Server

The process to add or remove features or to uninstall a completed instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is similar to the process to an installed instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see the following articles:

- [Add Features to an Instance of SQL Server (Setup)](add-features-to-an-instance-of-sql-server-setup.md)
- [Uninstall an existing instance of SQL Server (Setup)](../../sql-server/install/uninstall-an-existing-instance-of-sql-server-setup.md)

## Related content

- [What is Windows SysPrep](/previous-versions/windows/it-pro/windows-vista/cc721940(v=ws.10))
- [How does Windows SysPrepWork](/previous-versions/windows/it-pro/windows-vista/cc766514(v=ws.10))
