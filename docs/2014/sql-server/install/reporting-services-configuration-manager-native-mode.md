---
title: "Reporting Services Configuration Manager (Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services Configuration tool"
  - "configuration options [Reporting Services]"
  - "report servers [Reporting Services], configuring"
  - "components [Reporting Services], Reporting Services Configuration tool"
ms.assetid: 379eab68-7f13-4997-8d64-38810240756e
author: markingmyname
ms.author: maghan
manager: craigg
---
# Reporting Services Configuration Manager (Native Mode)
  Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to configure a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native Mode installation. If you installed a report server by using the files-only installation option, you must use the Configuration Manager to configure the server before you can use it. If you installed a report server by using the default configuration installation option, you can use the Configuration Manager to verify or modify the settings that were specified during setup. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager can be used to configure a local or remote report server instance.  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
> [!NOTE]  
>  Starting with the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] release, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager is not designed to manage SharePoint mode report servers. SharePoint mode is managed and configured by using SharePoint Central Administration and PowerShell scripts. For information, see [Reporting Services SharePoint Mode Installation &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md).  
  
 **In this section:**  
  
 [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)  
 Provides recommendations and steps on how to modify the service account and password.  
  
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)  
 Describes how to configure URLs used to access the Report Server Web service and Report Manager.  
  
 [Create a Report Server Database  &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/create-a-report-server-database-ssrs-configuration-manager.md)  
 Describes how to create a report server database, required for storing server metadata and objects.  
  
 [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md)  
 Describes how to modify the connection string used by the report server to connect to the report server database.  
  
 [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md)  
 Describes how to configure a user account to process reports in unattended mode.  
  
 [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md)  
 Describes how to configure a report server to support e-mail report distribution.  
  
 [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md)  
 Provides information about configuring multiple report server instances to use a shared report server database.  
  
 [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)  
 Explains how to use and manage encryption keys that are used when storing sensitive data.  
  
 [Manage a Reporting Services Native Mode Report Server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)  
 Provides step-by-step instruction for common tasks.  
  
 [Reporting Services Configuration Manager F1 Help Topics &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-f1-help-topics-ssrs-native-mode.md)  
 Provides help topics for the pages in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool.  
  
 **In this topic:**  
  
-   [Scenarios to use Reporting Services Configuration Manager](#bkmk_scenarios)  
  
-   [Requirements](#bkmk_requirements)  
  
-   [To Start the Reporting Services Configuration Manager](#bkmk_start_configuration_manager)  
  
##  <a name="bkmk_scenarios"></a> Scenarios to use Reporting Services Configuration Manager  
 You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to perform the following tasks:  
  
-   Configure the Report Server service account. The account is initially configured during setup, but can be modified by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager if you update the password or want to use a different account.  
  
-   Create and configure URLs. The report server and Report Manager are [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications accessed through URLs. The report server URL provides access to the SOAP endpoints of the report server. The Report Manager URL is used to open Report Manager. You can configure a single URL or multiple URLs for each application.  
  
-   Create and configure the report server database. The report server is a stateless server that requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database for internal storage. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to create and configure a connection to the report server database. You can also select an existing report server database that already contains the content you want to use.  
  
-   Configure a Native mode scale-out deployment. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports a deployment topology that allows multiple report server instances use a single, shared report server database. To deploy a report server scale-out deployment, you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to connect each report server to the shared report server database.  
  
-   Backup, restore, or replace the symmetric key that is used to encrypt stored connection strings and credentials. You must have a backup of the symmetric key if you change the service account, or move a report server database to another computer.  
  
-   Configure the unattended execution account. This account is used for remote connections during scheduled operations or when user credentials are not available.  
  
-   Configure report server e-mail. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a report server e-mail delivery extension that uses a Simple Mail Transfer Protocol (SMTP) to deliver reports or report processing notification to an electronic mailbox. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to specify which SMTP server or gateway on your network to use for e-mail delivery.  
  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager does not help you manage report server content, enable additional features, or grant access to the server. Full deployment requires that you also use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to enable additional features or modify default values, and Report Manager to grant user access to the server.  
  
##  <a name="bkmk_requirements"></a> Requirements  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration manager is version-specific. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager that installs with this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be used to configure an earlier version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you are running older and newer versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] side-by-side on the same computer, you must use the Reporting Service Configuration manager that comes with each version to configure each instance.  
  
 To use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration manager, you must have the following:  
  
-   Local system administrator permissions on the computer that hosts the report server you want to configure. If you are configuring a remote computer, you must have local system administrator permissions on that computer as well.  
  
-   You must have permission to create databases on the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] used to host the report server database.  
  
-   Windows Management Instrumentation (WMI) service must be enabled and running on any report server you are configuring. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager uses the report server WMI provider to connect to local and remote report servers. If you are configuring a remote report server, the computer must allow remote WMI access. For more information, see [Configure a Report Server for Remote Administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md).  
  
-   Before you can connect to and configure a remote report server instance, you must enable remote Windows Management Instrumentation (WMI) calls to pass through Windows Firewall. For more information, see [Configure a Report Server for Remote Administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager is installed automatically when you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
##  <a name="bkmk_start_configuration_manager"></a> To Start the Reporting Services Configuration Manager  
  
#### To start Reporting Services Configuration  
  
1.  Use the following step that is appropriate for your version of Microsoft Windows:  
  
    -   From the Windows start screen, type **Reporting** and select **Reporting Services Configuration Manager** from the search results.  
  
    -   Click **Start**, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], and then point to **Configuration Tools**.  
  
         If you want to configure a report server instance from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], open the program folder for that version. For example, point to [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] instead of [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)] to open the configuration tools for [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] server components.  
  
         Click **Reporting Services Configuration Manager**.  
  
2.  The **Reporting Services Configuration Connection** dialog box appears so that you can select the report server instance you want to configure. Click **Connect**.  
  
3.  In **Server Name**, specify the name of the computer on which the report server instance is installed. The name of the local computer appears by default, but you can type the name of a remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance if you want to connect to a report server that is installed on a remote computer.  
  
4.  If you specify a remote computer, click **Find** to establish a connection.  
  
5.  In **Report Server Instance**, select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that you want to configure. Only report server instances for this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] appear in the list. You cannot configure earlier versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
6.  Click **Connect**.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Reporting Services Tools](../../reporting-services/tools/reporting-services-tools.md)   
 [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
 [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)   
 [Configure and Administer a Report Server &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)  
  
  
