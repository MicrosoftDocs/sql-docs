---
title: "What is Report Server Configuration Manager (Native Mode)?"
description: "Learn how to use the Report Server Configuration Manager to configure a SQL Server Reporting Services (SSRS) Native mode installation."
author: maggiesMSFT
ms.author: maggies
ms.date: 06/27/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Reporting Services Configuration tool"
  - "configuration options [Reporting Services]"
  - "report servers [Reporting Services], configuring"
  - "components [Reporting Services], Reporting Services Configuration tool"
#customer intent: As a SQL Server user, I want to use Report Server Configuration Manager so that I can efficiently manage and configure SQL Server Reporting Services (SSRS) Native mode.
---

# What is Report Server Configuration Manager (Native Mode)?

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

Use the Report Server Configuration Manager to configure a SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] (SSRS) Native mode installation perform the following tasks:  
  
- **Configure the Report Server service account**: The account is initially configured during setup. Modify the account by by using the Report Server Configuration Manager if you update the password or want to use a different account.  
  
- **Create and configure URLs**: The report server and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] are [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications accessed through URLs. The report server URL provides access to the SOAP endpoints of the report server. The [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] URL is used to open the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)]. Use the Report Server Configuration Manager to configure a single URL or multiple URLs for each application.  
  
- **Create and configure the report server database**: The report server is a stateless server that requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database for internal storage. Use the Report Server Configuration Manager to create and configure a connection to the report server database. You can select an existing report server database that already contains the content you want to use.  
  
- **Configure a Native mode scale-out deployment**: [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports a deployment topology that allows multiple report server instances use a single, shared report server database. To deploy a report server scale-out deployment, you use the Report Server Configuration Manager to connect each report server to the shared report server database.  
  
- **Backup, restore, or replace the symmetric key**: A symmetric key is used to encrypt stored connection strings and credentials. You must have a backup of the symmetric key if you change the service account, or move a report server database to another computer.  
  
- **Configure the unattended execution account**. This account is used for remote connections during scheduled operations or when user credentials are not available.  
  
- **Configure report server email**: [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a report server email delivery extension that uses a Simple Mail Transfer Protocol (SMTP) to deliver reports or report processing notification to an electronic mailbox. Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to specify which SMTP server or gateway on your network to use for email delivery.  
  
A full deployment requires that you also use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to enable additional features or modify default values. These features include managing report server content and enabling additional features. You can also use the web portal to grant access to the server.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016. Starting with the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] release, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager is not designed to manage SharePoint mode report servers. SharePoint mode is managed and configured by using SharePoint Central Administration and PowerShell scripts.  

##  <a name="bkmk_requirements"></a> Versions of Reporting Services

The Report Server Configuration Manager is version-specific. The Report Server Configuration Manager that installs with this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can't be used to configure an earlier version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you are running older and newer versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] side-by-side on the same computer, you must use the Report Server Configuration Manager that comes with each version to configure each instance.  

## Get Started

1. Ensure that your system and permissions are set up correctly. For more information, see [Hardware and software requirements for installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).
 
1. Start the Report Server Configuration Manager. Use appropriate instruction depending on your version of Microsoft Windows:

    - From the Windows Start menu, enter **Report Server** and select **Report Server Configuration Manager** from the search results.

    - Select **Start**, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)] Reporting Services, and then select **Report Server Configuration Manager**.

         If you want to configure a report server instance from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], open the program folder for that version. For example, point to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] instead of [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)] to open the configuration tools for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] server components.

         Select **Report Server Configuration Manager**.

1. Select the report server instance you want to configure in the **Reporting Services Configuration Connection** dialog. 

1. Select **Connect**.

1. In **Server Name**, specify the name of the computer on which the report server instance is installed. The name of the local computer appears by default, but you can type the name of a remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance if you want to connect to a report server that is installed on a remote computer.

1. If you specify a remote computer, select **Find** to establish a connection.

1. In **Report Server Instance**, select the report server instance that you want to configure. Only report server instances for this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] appear in the list. You can't configure earlier versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].

1. Select **Connect**.

## Related content

[Web portal](../../reporting-services/web-portal-ssrs-native-mode.md)   
[Reporting Services Tools](../../reporting-services/tools/reporting-services-tools.md)   
[Configure a Report Server Database Connection](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
[SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)   
[Configure and Administer a Report Server](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
