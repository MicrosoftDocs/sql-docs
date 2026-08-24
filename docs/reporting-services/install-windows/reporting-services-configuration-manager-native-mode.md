---
title: "What Is Report Server Configuration Manager (Native Mode)?"
description: "Learn about the Report Server configuration manager so that you can configure a SQL Server Reporting Services (SSRS) installation in native mode."
ms.reviewer: randolphwest
ms.date: 01/07/2026
ms.service: reporting-services
ms.subservice: report-server
ms.topic: overview
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Reporting Services Configuration tool"
  - "configuration options [Reporting Services]"
  - "report servers [Reporting Services], configuring"
  - "components [Reporting Services], Reporting Services Configuration tool"
# customer intent: As a SQL Server user, I want to use the Report Server configuration manager so that I can efficiently manage and configure SQL Server Reporting Services (SSRS) native mode.
---

# What is the Report Server configuration manager (native mode)?

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE [ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

You can use the configuration manager in Power BI Report Server to configure a SQL Server [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] (SSRS) installation in native mode to perform the following tasks:

- **Configure the Report Server service account**: You configure this account during setup. If you update the password or want to use a different account, you can modify it by using the Report Server configuration manager.

- **Create and configure URLs**: The report server and the [!INCLUDE [ssRSWebPortal](../../includes/ssrswebportal.md)] are [!INCLUDE [vstecasp](../../includes/vstecasp-md.md)] applications that you access through URLs. The report server URL provides access to the SOAP endpoints of the report server. The [!INCLUDE [ssRSWebPortal](../../includes/ssrswebportal.md)] URL opens the [!INCLUDE [ssRSWebPortal](../../includes/ssrswebportal.md)]. Use the Report Server configuration manager to configure a single URL or multiple URLs for each application.

- **Create and configure the report server database**: The report server is a stateless server that requires a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database for internal storage. Use the Report Server configuration manager to create and configure a connection to the report server database. You can select an existing report server database that already contains the content that you want to use.

- **Configure a scale-out deployment in native mode**: [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports a deployment topology that allows multiple report server instances to use a single, shared report server database. To deploy a report server scale-out deployment, use the Report Server configuration manager to connect each report server to the shared report server database.

- **Back up, restore, or replace the symmetric key**: A symmetric key encrypts stored connection strings and credentials. You need a backup of the symmetric key if you change the service account or move a report server database to another computer.

- **Configure the unattended execution account**: This account enables remote connections during scheduled operations or when user credentials aren't available.

- **Configure the report server email**: [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a report server email delivery extension that uses Simple Mail Transfer Protocol (SMTP) to deliver reports or report processing notifications to an electronic mailbox. Use the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration manager to specify which SMTP server or gateway on your network to use for email delivery.

To fully deploy, you need to use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] to enable other features or modify default values. You can also use the [web portal](../web-portal-ssrs-native-mode.md) to grant access to the server. For more information about the report server, see [Configure and administer a report server (SSRS native mode)](../report-server/configure-and-administer-a-report-server-ssrs-native-mode.md).

> [!NOTE]  
> Reporting Services integration with SharePoint is no longer available in SQL Server versions 2016 and newer.
From the [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] release and newer, the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration manager isn't designed to manage SharePoint mode report servers. You manage and configure SharePoint mode by using SharePoint Central Administration and PowerShell scripts.

<a id="bkmk_requirements"></a>

## Versions of Reporting Services

The Report Server configuration manager is version-specific. You can't use the Report Server configuration manager that's installed with this version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to configure an earlier version of [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you're running both older and newer versions of [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] side-by-side on the same computer, you must use the Report Server configuration manager that comes with each version to configure each instance.

## Get started

1. Ensure that your system and permissions are set up correctly. For more information, see [Hardware and software requirements for SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2016.md).

1. To start the Report Server configuration manager, go to the Windows **Start** menu and enter **Report Server**. Select **Report Server Configuration Manager** from the search results.

   Alternately, depending on your version of Microsoft Windows, you can select **Start** and go to **All Programs**. Go to [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)] Reporting Services, and select **Report Server Configuration Manager**.

   To configure a report server instance from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], open the program folder for that version. For example, go to [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instead of [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)] to open the configuration tools for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] server components and select **Report Server Configuration Manager**.

1. In **Server Name**, specify the name of the computer where the report server instance is installed. The name of the local computer appears by default. To connect to a report server installed on a remote computer, enter the name of a remote [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

1. If you specify a remote computer, select **Find** to establish a connection.

1. In **Report Server Instance**, select the report server instance that you want to configure. Only report server instances for the specified version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] appear in the list. You can't configure earlier versions of [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)].

1. Select **Connect**.

## Related content

- [SQL Server Reporting Services tools](../tools/reporting-services-tools.md)
- [Configure a report server database connection (Report Server Configuration Manager)](configure-a-report-server-database-connection-ssrs-configuration-manager.md)
- [SQL Server Configuration Manager](../../tools/configuration-manager/sql-server-configuration-manager.md)
- [Ask questions on the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
