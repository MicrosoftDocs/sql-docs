---
title: "Reporting Services Configuration Manager (Native Mode) | Microsoft Docs"
ms.date: 09/20/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"

ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services Configuration tool"
  - "configuration options [Reporting Services]"
  - "report servers [Reporting Services], configuring"
  - "components [Reporting Services], Reporting Services Configuration tool"
author: markingmyname
ms.author: maghan
---

# Reporting Services Configuration Manager (Native Mode)

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to configure a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native Mode installation. If you installed a report server by using the files-only installation option, you must use the Configuration Manager to configure the server before you can use it. If you installed a report server by using the default configuration installation option, you can use the Configuration Manager to verify or modify the settings that were specified during setup. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager can be used to configure a local or remote report server instance.

> [!NOTE]
> Starting with the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] release, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager is not designed to manage SharePoint mode report servers. SharePoint mode is managed and configured by using SharePoint Central Administration and PowerShell scripts.  
  
##  <a name="bkmk_scenarios"></a> Scenarios to use Reporting Services Configuration Manager  
 You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to perform the following tasks:  
  
-   Configure the Report Server service account. The account is initially configured during setup, but can be modified by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager if you update the password or want to use a different account.  
  
-   Create and configure URLs. The report server and the [!INCLUDE[ssRSWebPortal-Non-Markdown](../../includes/ssrswebportal-non-markdown-md.md)] are [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications accessed through URLs. The report server URL provides access to the SOAP endpoints of the report server. The [!INCLUDE[ssRSWebPortal-Non-Markdown](../../includes/ssrswebportal-non-markdown-md.md)] URL is used to open the [!INCLUDE[ssRSWebPortal-Non-Markdown](../../includes/ssrswebportal-non-markdown-md.md)] You can configure a single URL or multiple URLs for each application.  
  
-   Create and configure the report server database. The report server is a stateless server that requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database for internal storage. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to create and configure a connection to the report server database. You can also select an existing report server database that already contains the content you want to use.  
  
-   Configure a Native mode scale-out deployment. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports a deployment topology that allows multiple report server instances use a single, shared report server database. To deploy a report server scale-out deployment, you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to connect each report server to the shared report server database.  
  
-   Backup, restore, or replace the symmetric key that is used to encrypt stored connection strings and credentials. You must have a backup of the symmetric key if you change the service account, or move a report server database to another computer.  
  
-   Configure the unattended execution account. This account is used for remote connections during scheduled operations or when user credentials are not available.  
  
-   Configure report server e-mail. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a report server e-mail delivery extension that uses a Simple Mail Transfer Protocol (SMTP) to deliver reports or report processing notification to an electronic mailbox. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to specify which SMTP server or gateway on your network to use for e-mail delivery.  
  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager does not help you manage report server content, enable additional features, or grant access to the server. Full deployment requires that you also use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to enable additional features or modify default values, and the web portal to grant user access to the server.

##  <a name="bkmk_requirements"></a> Requirements

The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager is version-specific. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager that installs with this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be used to configure an earlier version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you are running older and newer versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] side-by-side on the same computer, you must use the Reporting Service Configuration manager that comes with each version to configure each instance.  

To use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration manager, you must have the following:

- Local system administrator permissions on the computer that hosts the report server you want to configure. If you are configuring a remote computer, you must have local system administrator permissions on that computer as well.

- You must have permission to create databases on the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] used to host the report server database.

- Windows Management Instrumentation (WMI) service must be enabled and running on any report server you are configuring. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager uses the report server WMI provider to connect to local and remote report servers. If you are configuring a remote report server, the computer must allow remote WMI access. For more information, see [Configure a Report Server for Remote Administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md).  

- Before you can connect to and configure a remote report server instance, you must enable remote Windows Management Instrumentation (WMI) calls to pass through Windows Firewall. For more information, see [Configure a Report Server for Remote Administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

The Reporting Services Configuration Manager is installed automatically when you install SQL Server Reporting Services.

##  <a name="bkmk_start_configuration_manager"></a> To Start the Reporting Services Configuration Manager

1.  Use the following step that is appropriate for your version of Microsoft Windows:

    - From the Windows start screen, type **Reporting** and select **Reporting Services Configuration Manager** from the search results.

    - Select **Start**, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], and then point to **Configuration Tools**.

         If you want to configure a report server instance from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], open the program folder for that version. For example, point to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] instead of [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)] to open the configuration tools for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] server components.

         Select **Reporting Services Configuration Manager**.

2. The **Reporting Services Configuration Connection** dialog box appears so that you can select the report server instance you want to configure. Select **Connect**.

3. In **Server Name**, specify the name of the computer on which the report server instance is installed. The name of the local computer appears by default, but you can type the name of a remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance if you want to connect to a report server that is installed on a remote computer.

4. If you specify a remote computer, select **Find** to establish a connection.

5. In **Report Server Instance**, select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that you want to configure. Only report server instances for this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] appear in the list. You cannot configure earlier versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].

6. Select **Connect**.

## Next steps

[Web portal](../../reporting-services/web-portal-ssrs-native-mode.md)   
[Reporting Services Tools](../../reporting-services/tools/reporting-services-tools.md)   
[Configure a Report Server Database Connection](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
[SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)   
[Configure and Administer a Report Server](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
