---
title: "Migrate a Reporting Services Installation (Native Mode) | Microsoft Docs"
author: maggiesMSFT
ms.author: maggies
manager: kfile
ms.custom: ""
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "database-engine"
ms.topic: conceptual
ms.date: "08/10/2017"
---

# Migrate a Reporting Services Installation (Native Mode)

  This topic provides step-by-step instructions for migrating one of the following supported versions of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode deployment to a new [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance:  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]  
  
-   [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]  
  
-   [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] (Requires more steps, see [You cannot use SQL Server 2005 to host Report Server 2014 Databases](https://support.microsoft.com/kb/2796721).  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.|  
  
 For information on migrating a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode deployment, see [Migrate a Reporting Services Installation &#40;SharePoint Mode&#41;](migrate-a-reporting-services-installation-sharepoint-mode.md).  
  
 Migration is defined as moving application data files to a new [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance. The following are common reasons you must migrate your installation:  
  
-   You have a large-scale deployment or uptime requirements.  
  
-   You are changing the hardware or topology of your installation.  
  
-   You encounter an issue that blocks upgrade.  
  
##  <a name="bkmk_nativemode_migration_overview"></a> Native Mode Migration Overview  
 The migration process for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes manual and automated steps. The following tasks are part of a report server migration:  
  
-   Back up database, application, and configuration files.  
  
-   Back up the encryption key.  
  
-   Install a new instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. If you are using the same hardware, you can install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] side-by-side with your existing installation if it was one of the supported versions.  
  
    > [!TIP]  
    >  A side-by-side installation may require that you install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] as a named instance.  
  
-   Move the report server database and other application files from your existing installation to your new [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation.  
  
-   Move any custom application files to the new installation.  
  
-   Configure the report server.  
  
-   Edit **RSReportServer.config** to include any custom settings from your previous installation.  
  
-   Optionally, configure custom Access Control Lists (ACLs) for the new [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Windows service group.  
  
-   Remove unused applications and tools after you have confirmed that the new instance is fully operational.  
  
 There are restrictions on the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that host the report server database. Review the following topic if you are reusing a report server database that was created in a previous installation.  
  
-   [Create a Report Server Database  &#40;SSRS Configuration Manager&#41;](../../sql-server/install/create-a-report-server-database-ssrs-configuration-manager.md)  
  
##  <a name="bkmk_fixed_database_name"></a> Fixed Database Name  
 You cannot rename the report server database. The identity of the database is recorded in report server stored procedures when the database is created. Renaming either the report server primary or temporary databases causes errors when the procedures run, invalidating your report server installation.  
  
 If the database name from the existing installation is not suited for the new installation, you should consider creating a new database that has the name, and then load existing application data using the techniques in the following list:  
  
-   Write a [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] script that calls Report Server Web service SOAP methods to copy data between databases. You can use the RS.exe utility to run the script. For more information about this approach, see [Scripting and PowerShell with Reporting Services](../tools/scripting-and-powershell-with-reporting-services.md).  
  
-   Write code that calls the WMI provider to copy data between databases. For more information about this approach, see [Access the Reporting Services WMI Provider](../tools/access-the-reporting-services-wmi-provider.md).  
  
-   If you have just a few items, you can republish reports, report models, and shared data sources from Report Designer, Model Designer, and Report Builder to the new report server. You must re-create role assignments, subscriptions, shared schedules, report snapshot schedules, custom properties that you set on reports or other items, model item security, and properties that you set on the report server. You will lose report history and report execution log data.  
  
##  <a name="bkmk_before_you_start"></a> Before You Start  
 Even though you are migrating rather than upgrading the installation, consider running Upgrade Advisor on your existing installation help identify any issues that could affect migration. This step is especially helpful if you are migrating a report server that you did not install or configure. By running Upgrade Advisor, you can find out about custom settings that might not be supported in a new [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation.  
  
 In addition, you should be aware of several important changes in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] that will affect how you migrate your installation:  
  
-   Starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], IIS is no longer a prerequisite. If you are migrating a report server installation to a new computer, you do not need to add the Web server role. In addition, steps for configuring URLs and authentication are different from the previous release, as are techniques and tools for diagnosing and troubleshooting problems.  
  
-   Report Server Web service, Report Manager, and the Report Server Windows service are consolidated within a single Report Server service. All three applications run under the same account. All three applications read configuration settings from RSReportServer.config file, making RSWebApplication.config obsolete.  
  
-   Report Manager and SQL Server Management Studio are redesigned to remove overlapping features. Each tool supports a distinct set of tasks; the tools are no longer interchangeable.  
  
-   ISAPI filters are not supported in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and later versions. If you use ISAPI filters, you must redesign your reporting solution prior to migration.  
  
-   IP address restrictions are not supported in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and later versions. If you use IP address restrictions, you must redesign your reporting solution prior to migration or use a technology such as a firewall, router, or Network Address Translation (NAT) to configure addresses that are restricted from accessing the report server.  
  
-   Client Secure Sockets Layer (SSL) certificates are not supported in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and later versions. If you use client SSL certificates, you must redesign your reporting solution prior to migration.  
  
-   If you use an authentication type other than Windows Integrated authentication, you must update the `<AuthenticationTypes>` element in the **RSReportServer.config** file with a supported authentication type. The supported authentication types are NTLM, Kerberos, Negotiate, and Basic. Anonymous, .NET Passport, and Digest authentication are not supported in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and later versions.  
  
-   If you use custom cascading style sheets in your reporting environment, they will not be migrated. You must manually move them following migration.  
  
 For more information about changes in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see the Upgrade Advisor documentation and [What's New &#40;Reporting Services&#41;](../what-s-new-reporting-services.md).  
  
##  <a name="bkmk_backup"></a> Backup Files and Data  
 Before you install a new instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], be sure to back up all of the files in your current installation.  
  
1.  Back up the encryption key for the report server database. This step is critical to migration success. Further on in the migration process, you must restore it for the report server to regain access to encrypted data. To back up the key, use the Reporting Services Configuration Manager.  
  
2.  Back up the report server database using any of the supported methods for backing up a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. For more information, see the instructions on how to back up the report server database in [Moving the Report Server Databases to Another Computer &#40;SSRS Native Mode&#41;](../report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md).  
  
3.  Back up the report server configuration files. Files to back up include:  
  
    1.  Rsreportserver.config  
  
    2.  Rswebapplication.config  
  
    3.  Rssvrpolicy.config  
  
    4.  Rsmgrpolicy.config  
  
    5.  Reportingservicesservice.exe.config  
  
    6.  Web.config for both the Report Server and Report Manager [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications.  
  
    7.  Machine.config for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] if you modified it for report server operations.  
  
##  <a name="bkmk_install_ssrs"></a> Install SQL Server Reporting Services  
 Install a new report server instance in files-only mode so that you can configure it to use non-default values. For command line installation, use the `FilesOnly` argument. In the Installation Wizard, select the **Install but do not configure option**.  
  
 Click one of the following links to view instructions on how to install a new instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]:  
  
-   [Install SQL Server 2014 from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)  
  
-   [Install SQL Server 2014 from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)  
  
##  <a name="bkmk_move_database"></a> Move the Report Server Database  
 The report server database contains published reports, models, shared data sources, schedules, resources, subscriptions, and folders. It also contains system and item properties, and permissions for accessing report server content.  
  
 If your migration includes using a different [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance, you must move the report server database to the new [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance. If you are using the same [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance, skip to section [Move Custom Assemblies or Extensions](#bkmk_move_custom).  
  
 To move the report server database, do the following:  
  
1.  Choose the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance to use. [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] requires that you use one of the following versions to host the report server database:  
  
    -   [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
  
    -   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
    -   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]  
  
    -   [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]  
  
2.  Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
3.  Create the `RSExecRole` in the system databases if the [!INCLUDE[ssDE](../../includes/ssde-md.md)] has never hosted a report server database. For more information, see [Create the RSExecRole](../security/create-the-rsexecrole.md).  
  
4.  Follow the instructions in [Moving the Report Server Databases to Another Computer &#40;SSRS Native Mode&#41;](../report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md).  
  
 Remember that both the report server database and the temporary database are interdependent and must be moved together. Do not copy the databases; copying does not transfer all of the security settings to the new installation. Do not move SQL Server Agent jobs for scheduled report server operations. The report server will recreate these jobs automatically.  
  
##  <a name="bkmk_move_custom"></a> Move Custom Assemblies or Extensions  
 If your installation includes custom report items, assemblies, or extensions, you must redeploy the custom components. If you are not using custom components, skip to section [Configure the Report Server](#bkmk_configure_reportserver).  
  
 To redeploy the custom components, do the following:  
  
1.  Determine whether the assemblies are supported or need recompilation:  
  
    -   Custom authentication extensions created for the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] release must be recompiled.  
  
    -   Custom rendering extensions for [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] must be rewritten using the Rendering Object Model (ROM).  
  
    -   HTML 3.2 and HTML OWC renderers are not supported in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and later versions.  
  
    -   Other custom assemblies should not require recompilation.  
  
2.  Move the assemblies to the new report server and Report Manager \bin folders. In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the report server binaries are located in the following location for the default [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance:  
  
     `\Program files\Microsoft SQL Server\MSRS12.MSSQLSERVER\Reporting Services\ReportServer\bin`  
  
3.  Modify the configuration files to add entries for your custom component. The entries will vary depending on the kind of assembly you are using. For instructions on where to place files and add configuration entries, see the following:  
  
    1.  [Deploying a Custom Assembly](../custom-assemblies/deploying-a-custom-assembly.md)  
  
    2.  [How to: Deploy a Custom Report Item](../custom-report-items/how-to-deploy-a-custom-report-item.md)  
  
    3.  [Deploying a Data Processing Extension](../extensions/data-processing/deploying-a-data-processing-extension.md)  
  
    4.  [Deploying a Delivery Extension](../extensions/delivery-extension/deploying-a-delivery-extension.md)  
  
    5.  [Deploying a Rendering Extension](../extensions/rendering-extension/deploying-a-rendering-extension.md)  
  
    6.  [Implementing a Security Extension](../extensions/security-extension/implementing-a-security-extension.md)  
  
##  <a name="bkmk_configure_reportserver"></a> Configure the Report Server  
 Configure URLs for the Report Server Web service and Report Manager, and configure the connection to the report server database.  
  
 If you are migrating a scale-out deployment, take all of the report server nodes offline and migrate each server one at a time. Once the first report server is migrated and it successfully connects to the report server database, the report server database version is automatically upgraded to the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] database version.  
  
> [!IMPORTANT]  
>  If any of the report servers in the scale-out deployment are online and have not been migrated, they might encounter an rsInvalidReportServerDatabase exception because they are using an older schema when connected to the upgraded.  
  
> [!NOTE]  
>  If the report server you migrated was configured as the shared database for a scale-out deployment, you need to delete any of the old encryption keys from the **Keys** table in the **ReportServer** database, before configuring the report server service. If the keys are not removed, the migrated report server will try to initialize in scale-out deployment mode. For more information, see [Add and Remove Encryption Keys for Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](add-and-remove-encryption-keys-for-scale-out-deployment.md) and [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-manage-encryption-keys.md).  
>   
>  The scale-out keys cannot be deleted by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager. The old keys must be deleted from the **Keys** table in the **ReportServer** database using SQL Server Management Studio. Delete all rows in the Keys table. This will clear the table and prepare it for restoring the Symmetric key only, as documented in the following steps.  
>   
>  Prior to deleting the keys it is recommended you first back up the Symmetric Encryption key. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to back up the key. Open the Configuration Manager open, click the **Encryption Keys** tab and then click the **Backup** button. You can also script WMI commands to back up the encryption key. For more information on WMI, see [BackupEncryptionKey Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../wmi-provider-library-reference/configurationsetting-method-backupencryptionkey.md).  
  
1.  Start the Reporting Services Configuration Manager and connect to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance you just installed. For more information, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
2.  Configure URLs for the report server and Report Manager. For more information, see [Configure a URL  &#40;SSRS Configuration Manager&#41;](configure-a-url-ssrs-configuration-manager.md).  
  
3.  Configure the report server database, selecting the existing report server database from your previous installation. After successful configuration, the report server services will restart, and once a connection is made to the report server database, the database will be automatically upgraded to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For more information about how to run the Change Database Wizard that you use to create or select a report server database, see [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](ssrs-report-server-create-a-native-mode-report-server-database.md).  
  
4.  Restore the encryption keys. This step is necessary for enabling reversible encryption on pre-existing connection strings and credentials that are already in the report server database. For more information, see [Back Up and Restore Reporting Services Encryption Keys](ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).  
  
5.  If you installed report server on a new computer and you are using Windows Firewall, be sure that the TCP port on which the report server listens is open. By default, this port is 80. For more information, see [Configure a Firewall for Report Server Access](../report-server/configure-a-firewall-for-report-server-access.md).  
  
6.  If you want to administer your native mode report server locally, you need to configure the operating system to allow local administration with Report Manager. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](../report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
##  <a name="bkmk_copy_custom_config"></a> Copy Custom Configuration Settings to RSReportServer.config File  
 If you modified the RSReportServer.config file or RSWebApplication.config file in the previous installation, you should make the same modifications in the new RSReportServer.config file. The following list summarizes some of the reasons why you might have modified the previous configuration file and provides links to additional information about how to configure the same settings in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
|Customization|Information|  
|-------------------|-----------------|  
|Report Server E-mail delivery with custom settings|[Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md) and [E-mail Settings - Configuration Manager &#40;SSRS Native Mode&#41;](e-mail-settings-reporting-services-native-mode-configuration-manager.md).|  
|Device information settings|[Customize Rendering Extension Parameters in RSReportServer.Config](../customize-rendering-extension-parameters-in-rsreportserver-config.md)|  
|Report Manager on a remote instance|[Configure Report Manager &#40;Native Mode&#41;](../report-server/configure-web-portal.md)|  
  
##  <a name="bkmk_windowsservice_group"></a> Windows Service Group and Security ACLs  
 In [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)], there is one service group, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Windows Service group, which is used to create security ACLs for all the registry keys, files, and folders that are installed with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. This Windows group name appears in the format SQLServerReportServerUser$\<*computer_name*>$\<*instance_name*>. This group takes the place of the two Windows service groups in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you have custom ACLs associated with either of the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Windows groups, you will need to apply those ACLs to the new group for your new report server instance in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
##  <a name="bkmk_verify"></a> Verify Your Deployment  
  
1.  Test the report server and Report Manager virtual directories by opening a browser and typing in the URL address. For more information, see [Verify a Reporting Services Installation](verify-a-reporting-services-installation.md).  
  
2.  Test reports and verify they contain the data you expect. Review data source information to see whether the data source connection information is still specified. The report server uses the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report object model when processing and rendering reports, but it does not replace [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] constructs with new report definition language elements. To learn more about how existing reports run on a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report server, see [Upgrade Reports](upgrade-reports.md).  
  
##  <a name="bkmk_remove_unused"></a> Remove Unused Programs and Files  
 Once you have successfully migrated your report server to a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance, you might want to perform the following steps to remove programs and files that are no longer necessary.  
  
1.  Uninstall the previous version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] if you no longer need it. This step does not delete the following items, but you can manually remove them if you no longer need them:  
  
    -   The old Report Server database  
  
    -   RsExec role  
  
    -   Report Server service accounts  
  
    -   Application pool for the Report Server Web service  
  
    -   Virtual directories for Report Manager and the report server  
  
    -   Report server log files  
  
2.  Remove IIS if you no longer need it on this computer.  
  
## See Also  
 [Migrate a Reporting Services Installation &#40;SharePoint Mode&#41;](migrate-a-reporting-services-installation-sharepoint-mode.md)   
 [Report Server Database &#40;SSRS Native Mode&#41;](../report-server/report-server-database-ssrs-native-mode.md)   
 [Upgrade and Migrate Reporting Services](upgrade-and-migrate-reporting-services.md)   
 [Reporting Services Backward Compatibility](../reporting-services-backward-compatibility.md)   
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)  
  
  
