---
title: "Install SQL Server 2014 Using SysPrep | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 11f4ed8a-aaa9-417b-bdd5-204f551c6bb6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Install SQL Server 2014 Using SysPrep
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep related setup actions can be accessed through the Installation Center. The **Advanced** Page of the **Installation Center** has two options - **Image preparation of a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** and **Image completion of a prepared stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**. The [Prepare](#prepare) and [Complete](#complete) sections describe the installation process in detail. For more information, see [Considerations for Installing SQL Server Using SysPrep](considerations-for-installing-sql-server-using-sysprep.md).  
  
 You can also prepare and complete an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the command prompt or a configuration file. For more information, see:  
  
-   [Install SQL Server 2014 from the Command Prompt](install-sql-server-from-the-command-prompt.md)  
  
-   [Install SQL Server 2014 Using a Configuration File](install-sql-server-using-a-configuration-file.md)  
  
## Prerequisites  
 Before you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], review the topics in [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md).  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] editions and the hardware and software requirements, see [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
> [!IMPORTANT]  
>  The following is not supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep:  
>   
>  WOW 64 installations: a feature of 64-bit edition of Windows that enables 32-bit applications to run natively in 32-bit mode.  
  
 This section includes the following procedures:  
  
-   [SQL Server SysyPrep Cluster Support](#sysprep)  
  
-   [Prepare a stand-alone instance of SQL Server](#prepare)  
  
-   [Complete a prepared instance of SQL Server](#complete)  
  
-   [Add features to a prepared instance of SQL Server](#AddFeatures)  
  
-   [Remove features from a prepared instance of SQL Server](#RemoveFeatures)  
  
-   [Uninstalling a Prepared Instance](install-sql-server-using-sysprep.md#uninstall)  
  
-   [Modifying or Uninstalling a Completed Instance of SQL Server.](install-sql-server-using-sysprep.md#bk_modifying_uninstalling)  
  
##  <a name="sysprep"></a> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep Cluster Support  
 Beginning in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], SysPrep supports clustered [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances in command line installations. For more information, see [What is Sysprep?](https://msdn.microsoft.com/library/cc721940\(v=WS.10\).aspx).  
  
#### To Prepare a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster (Unattended)  
  
1.  Prepare the image (as discussed in [Considerations for Installing SQL Server Using SysPrep](considerations-for-installing-sql-server-using-sysprep.md)) and capture the windows image through SysPrep Generalization. The following sample prepares the image:  
  
    ```  
    Setup.exe /q /ACTION=PrepareImage l /FEATURES=SQLEngine /InstanceID =<MYINST> /IACCEPTSQLSERVERLICENSETERMS  
    ```  
  
     Then run Windows SysPrep Generalization.  
  
2.  Deploy the image by running Windows SysPrep Specialize.  
  
3.  Create the Windows Failover Cluster.  
  
4.  Run setup.exe with `/ACTION=PrepareFailoverCluster` all nodes. For example:  
  
    ```  
    setup.exe /q /ACTION=PrepareFailoverCluster /InstanceName=<InstanceName> /Features=SQLEngine  /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="xxxxxxxxxxx"  /IACCEPTSQLSERVERLICENSETERMS  
    ```  
  
#### Complete a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster (Unattended)  
  
1.  Run setup.exe with `/ACTION=CompleteFailoverCluster` on the node that owns the available storage group:  
  
    ```  
    setup.exe /q /ACTION=CompleteFailoverCluster /InstanceName=<InstanceName>  /FAILOVERCLUSTERDISKS="<Cluster Disk Resource Name - for example, 'Disk S:'>:" /FAILOVERCLUSTERNETWORKNAME="<Insert FOI Network Name>" /FAILOVERCLUSTERIPADDRESSES="IPv4;xx.xxx.xx.xx;Cluster Network;xxx.xxx.xxx.x" /FAILOVERCLUSTERGROUP="MSSQLSERVER" /INSTALLSQLDATADIR="<Drive>:\<Path>\MSSQLSERVER" /SQLCOLLATION="SQL_Latin1_General_CP1_CS_AS" /SQLSYSADMINACCOUNTS="<DomainName\UserName>"  
    ```  
  
#### Adding a Node to an Existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster (Unattended)  
  
1.  Deploy the image by running Windows SysPrep Specialize.  
  
2.  Join the Windows Failover Cluster.  
  
3.  Run setup.exe with `/ACTION=AddNode` on all nodes:  
  
    ```  
    setup.exe /q /ACTION=AddNode /InstanceName=<InstanceName> /Features=SQLEngine  /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="xxxxxxxxxxx"  /IACCEPTSQLSERVERLICENSETERMS  
    ```  
  
##  <a name="prepare"></a> Prepare Image  
  
#### Prepare a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
1.  Insert the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click Setup.exe. To install from a network share, locate the root folder on the share, and then double-click Setup.exe.  
  
2.  The Installation Wizard runs the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To prepare an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], click **Image preparation of a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** on the **Advanced** page.  
  
3.  The System Configuration Checker runs a discovery operation on your computer. To continue, click **OK**. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
4.  On the Product Updates page, the latest available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product updates are displayed. If you don't want to include the updates, clear the **Include [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product updates** check box. If no product updates are discovered, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup does not display this page and auto advances to the **Install Setup Files** page.  
  
5.  On the Install Setup files page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is found, and is specified to be included, that update will also be installed.  
  
6.  The System Configuration Checker verifies the system state of your computer before Setup continues. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
7.  On the **Prepare Image Type** page, select **Prepare a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**.  
  
     The **Prepare Image Type** page is displayed only when you have an existing un-configured prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the machine. You can choose to prepare a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or add sys prep supported features to an existing prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the machine. For more information on how to add features to a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] see [Add Features to a prepared instance](#AddFeatures).  
  
8.  On the **License Terms** page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE[msCoName](../../includes/msconame-md.md)].  
  
9. On the **Feature Selection** page, select the components for your installation:  
  
    |||  
    |-|-|  
    |[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] SysPrep|[!INCLUDE[ssDE](../../includes/ssde-md.md)]<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication<br /><br /> Full-Text Features<br /><br /> Data Quality Services<br /><br /> [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in Native mode<br /><br /> [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]<br /><br /> Redistributable Features<br /><br /> Shared Features|  
  
     A description for each component group appears in the right pane when you highlight the feature name. You can select any combination of check boxes. For more information, see [Editions and Components of SQL Server 2014](../../sql-server/editions-and-components-of-sql-server-2016.md) and [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
     The prerequisites for the selected features are displayed on the right-hand pane. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will install the prerequisite that are not already installed during the installation step described later in this procedure.  
  
10. On the **Prepare Image Rules** page, the System Configuration Checker verifies the system state of your computer before Setup continues. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
11. On the Instance Configuration page, specify the Instance ID for the Instance. Click **Next** to continue.  
  
     **Instance ID** - The Instance ID is used to identify installation directories and registry keys for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. If the prepared instance is completed as a default Instance during the Complete step, the instance name is overwritten as MSSQLSERVER. The Instance ID remains the same as specified.  
  
     **Instance root directory** - By default, the instance root directory is [!INCLUDE[ssInstallPath](../../includes/ssinstallpath-md.md)]. To specify a non-default root directory, use the field provided, or click **Browse** to locate an installation folder. The directory specified in the prepare step will be used during configuration in the Complete step.  
  
     All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades will apply to every component of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     **Installed Instances** - The grid shows instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running.  
  
12. The **Disk Space Requirements** page calculates the required disk space for the features that you specify. Then it compares the required space to the available disk space.  
  
13. The System Configuration Checker will run prepare image rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that you have specified. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
14. The **Ready to Prepare Image** page shows a tree view of installation options that were specified during Setup. On this page, Setup indicates whether the Product Update feature is enabled or disabled and the final update version. To continue, click **Prepare**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will first install the required prerequisites for the selected features followed by the feature installation.  
  
15. During installation, the **Prepare Image Progress** page provides status so that you can monitor installation progress as Setup continues.  
  
16. After installation, the **Complete** page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
17. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](view-and-read-sql-server-setup-log-files.md).  
  
18. This completes the prepare step. You may complete the image or deploy the prepared image as described in [Considerations for Installing SQL Server Using SysPrep](considerations-for-installing-sql-server-using-sysprep.md).  
  
##  <a name="complete"></a> Complete Image  
  
#### Complete a Prepared Instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
1.  If you have a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] included in the image of you machine, you will see a shortcut in the Start Menu. You can also launch the Installation Center and click **Image completion of a prepared stand-alone instance** on the **Advanced** page.  
  
2.  The System Configuration Checker runs a discovery operation on your computer. To continue, click **OK**. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
3.  On the **Setup Support Files** page, click **Install** to install the Setup support files.  
  
4.  The System Configuration Checker verifies the system state of your computer before Setup continues. After the check is complete, click **Next** to continue. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
5.  On the **Product Key** page, select an option button to indicate whether you are installing a free edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or a production version of the product that has a PID key. For more information, see [Editions and Components of SQL Server 2014](../../sql-server/editions-and-components-of-sql-server-2016.md). If you are installing Evaluation edition the 180-day trial period starts when you complete this step.  
  
6.  On the **License Terms** page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE[msCoName](../../includes/msconame-md.md)].  
  
7.  On the **Select a Prepared Instance** page select the prepared instance you want to complete from the drop down box. Select the Un-configured instance from the **Instance ID** list.  
  
     **Installed instances:** This grid displays all the instances including any prepared instance on this machine.  
  
8.  On the **Feature Review** page, you will see the selected features and components included in the install during the prepare step. If you wish to add more features to your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Instance not included in the prepared instance, you must first complete this step to complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Instance, then add the features from the **Add Features** on the **Installation Center**.  
  
    > [!NOTE]  
    >  You can add features that are available for the product version that you are installing. For more information, see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md)  
  
9. On the Instance Configuration page, specify the Instance name for the prepared Instance. This is the name of the instance once you have completed the configuration of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Click **Next** to continue.  
  
     **Instance ID** - The Instance ID is used to identify installation directories and registry keys for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. If the prepared instance is completed as a default Instance during the Complete step, the instance name is overwritten as MSSQLSERVER. The Instance ID remains the same as specified during the Prepare step.  
  
     **Instance root directory** -The directory specified in the prepare step will be used and cannot be modified in this step.  
  
     All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades will apply to every component of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     **Installed instances** - The grid shows instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running.  
  
10. Work flow for the rest of this topic depends on the features that were selected during the prepare step. You might not see all the pages, depending on the selections.  
  
11. On the **Server Configuration** - Service Accounts page, specify login accounts for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that are configured on this page depend on the features that you selected to install.  
  
     You can assign the same login account to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, are started manually, or are disabled. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you configure service accounts individually to provide least privileges for each service, where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they have to have to complete their tasks. For more information, see [Server Configuration - Service Accounts](../../sql-server/install/server-configuration-service-accounts.md) and [Configure Windows Service Accounts and Permissions](../configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
     To specify the same logon account for all service accounts in this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], provide credentials in the fields at the bottom of the page.  
  
     **Security Note** [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]  
  
     When you are finished specifying login information for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, click **Next**.  
  
12. Use the **Server Configuration - Collation** tab to specify non-default collations for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information, see [Server Configuration - Collation](../../sql-server/install/server-configuration-collation.md).  
  
13. Use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Configuration - Account Provisioning page to specify the following:  
  
    -   Security Mode - Select Windows Authentication or Mixed Mode Authentication for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you select Mixed Mode Authentication, you must provide a strong password for the built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account.  
  
         After a device establishes a successful connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the security mechanism is the same for both Windows Authentication and Mixed Mode. For more information, see [Database Engine Configuration - Account Provisioning](../../sql-server/install/database-engine-configuration-account-provisioning.md).  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Administrators - You must specify at least one system administrator for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To add the account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, click **Add Current User**. To add or remove accounts from the list of system administrators, click **Add** or **Remove**, and then edit the list of users, groups, or computers that will have administrator privileges for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Database Engine Configuration - Account Provisioning](../../sql-server/install/database-engine-configuration-account-provisioning.md).  
  
     When you are finished editing the list, click **OK**. Verify the list of administrators in the configuration dialog box. When the list is complete, click **Next**.  
  
14. Use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Configuration - Data Directories page to specify nondefault installation directories. To install to default directories, click **Next**.  
  
    > [!IMPORTANT]  
    >  If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     For more information, see [Database Engine Configuration - Data Directories](../../sql-server/install/database-engine-configuration-data-directories.md).  
  
15. Use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Configuration - FILESTREAM page to enable FILESTREAM for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Database Engine Configuration - Filestream](../../sql-server/install/database-engine-configuration-filestream.md).  
  
16. Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration page to specify the kind of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to create. For more information about [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration modes, see [Reporting Services Configuration Options &#40;SSRS&#41;](../../sql-server/install/reporting-services-configuration-options-ssrs.md).  
  
17. On the **Error Reporting** page, specify the information that you want to send to [!INCLUDE[msCoName](../../includes/msconame-md.md)] that will help improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, options for error reporting is enabled.  
  
18. On the **Complete Image Rules** page, the System Configuration Checker will run the complete image rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configurations that you have specified. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
19. The **Ready to Complete Image** page shows a tree view of installation options that were specified during Setup. To continue, click **Install**.  
  
20. During installation, the **Complete Image Progress** page provides status so that you can monitor installation progress as Setup continues.  
  
21. After installation, the **Complete** page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
22. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](view-and-read-sql-server-setup-log-files.md).  
  
23. This step completes the configuration of the prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and you have completed the installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="AddFeatures"></a> Add Features to a Prepared Instance  
  
#### Add Features to a Prepared Instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
1.  Insert the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click Setup.exe. To install from a network share, locate the root folder on the share, and then double-click Setup.exe.  
  
2.  The Installation Wizard runs the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To add features to a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], click **Image preparation of a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** on the **Advanced** page.  
  
3.  The System Configuration Checker runs a discovery operation on your computer. To continue, click **OK**. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
4.  On the Setup Support Files page, click **Install** to install the Setup support files.  
  
5.  On the **Prepare Image Type** page, select **Add features to an existing prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** option. Select the specific prepared instance you want to add features to from the drop down list of available prepared instances.  
  
6.  On the **Feature Selection** page, specify the features you want to add to the specified prepared instance.  
  
     The prerequisites for the selected features are displayed on the right-hand pane. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will install the prerequisite that are not already installed during the installation step described later in this procedure.  
  
7.  On the **Prepare Image Rules** page, the System Configuration Checker verifies the system state of your computer before Setup continues. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
8.  The Disk Space Requirements page calculates the required disk space for the features that you specify. Then it compares the required space to the available disk space.  
  
9. On the **Prepare Image Rules** page, the System Configuration Checker will run prepare image rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that you have specified. You can view the details on the screen by clicking **Show Details**, or as an HTML report by clicking **View detailed report**.  
  
10. The **Ready to Prepare Image** page shows a tree view of installation options that were specified during Setup. To continue, click **Install**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will first install the required prerequisites for the selected features followed by the feature installation.  
  
11. During installation, the **Prepare Image Progress** page provides status so that you can monitor installation progress as Setup continues.  
  
12. After installation, the **Complete** page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
13. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](view-and-read-sql-server-setup-log-files.md).  
  
##  <a name="RemoveFeatures"></a> Remove Features from a Prepare Instance  
  
#### Removing features from a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
1.  To begin the uninstall process, from the **Start** menu click **Control Panel** and double click **Program and Features**.  
  
2.  Double click the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component to uninstall and click **Remove**.  
  
3.  Setup support rules will run to verify your computer configuration. Click **OK** to continue.  
  
4.  On the **Select Instance** page, select the prepared instance you want to modify. The name of the prepared instance will be displayed as "Unconfigured PreparedInstanceID" where PreparedInstanceID is the instance you select.  
  
5.  On the **Select Features** page, specify the features to remove from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you specified. Click **Next** to continue.  
  
6.  Removal rules will run to verify that the operation can complete successfully.  
  
7.  On the **Ready to Remove** page, review the list of components and features that will be uninstalled.  
  
8.  The **Remove Progress** page will display the status of the operation.  
  
9. On the **Complete** page you can review the completion status of the operation. Click **close** to exit the installation wizard.  
  
##  <a name="Uninstall"></a> Uninstalling a Prepared Instance  
  
#### Uninstall a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
1.  To begin the uninstall process, from the **Start** menu click **Control Panel** and double click **Program and Features**.  
  
2.  Double click the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component to uninstall and click **Remove**.  
  
3.  Setup support rules will run to verify your computer configuration. Click **OK** to continue.  
  
4.  On the **Select Instance** page, select the prepared instance you want to modify. The name of the prepared instance will be displayed as "Unconfigured PreparedInstanceID" where PreparedInstanceID is the instance you select.  
  
5.  On the **Select Features** page, specify the features to remove from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you specified. Click **Next** to continue.  
  
6.  On the **Removal Rules** page, Setup will run rules to verify that the operation can complete successfully.  
  
7.  On the **Ready to Remove** page, review the list of components and features that will be uninstalled.  
  
8.  The **Remove Progress** page will display the status of the operation.  
  
9. On the **Complete** page you can review the completion status of the operation. Click **close** to exit the installation wizard.  
  
10. Repeat steps 1 to 9 until all components of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] have been removed.  
  
##  <a name="bk_Modifying_Uninstalling"></a> Modifying or Uninstalling a Completed Instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
 The process to add or remove features or to uninstall a completed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is similar to the process to an installed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see the following topics:  
  
-   [Add Features to an Instance of SQL Server 2014 &#40;Setup&#41;](add-features-to-an-instance-of-sql-server-setup.md)  
  
-   [Uninstall an Existing Instance of SQL Server &#40;Setup&#41;](../../sql-server/install/uninstall-an-existing-instance-of-sql-server-setup.md)  
  
## See Also  
 [What is Windows SysPrep](https://go.microsoft.com/fwlink/?LinkId=143546)   
 [How does Windows SysPrepWork](https://go.microsoft.com/fwlink/?LinkId=143547)  
  
  
