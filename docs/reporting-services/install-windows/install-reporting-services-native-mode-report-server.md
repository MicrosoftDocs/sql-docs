---
title: Install a Reporting Services 2016 native mode report server
description: See how to install Reporting Services in native mode. View steps for how to use the SQL Server installation wizard to install and configure the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/05/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom:
  - intro-installation
  - updatefrequency5
helpviewer_keywords:
  - "default configuration [Reporting Services]"
  - "report servers [Reporting Services], default configurations"
  - "installation options [Reporting Services]"

#customer intent: As a SQL Server user, I want to see how to install Reporting Services in native mode so that I can access a web portal for managing reports.
---

# Install a Reporting Services 2016 native mode report server

[!INCLUDE[SSRS applies to](../../includes/ssrs-appliesto.md)] [!INCLUDE[SSRS applies to 2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[SSRS applies to not 2017](../../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE[SSRS applies to not Power BI Report Server](../../includes/ssrs-appliesto-not-pbirs.md)]

The native mode installation of SQL Server Reporting Services (SSRS) provides access to a [!INCLUDE[SSRS web portal](../../includes/ssrswebportal.md)] that you can use to manage reports and other items.

> [!NOTE]
> For information about Power BI Report Server, see [Install Power BI Report Server](https://powerbi.microsoft.com/documentation/reportserver-install-report-server/).

You can install an SSRS native mode report server from the [!INCLUDE[SQL Server no version](../../includes/ssnoversion-md.md)] installation wizard or from a command line. If you use the installation wizard, you have two options:

- Install files and configure default settings for the server
- Only install the files

This article shows the first option of installing and configuring a report server instance, which is the default configuration for native mode. After the installation is complete, the report server is ready to use for basic report viewing and report management.

If you want to use features like [!INCLUDE[Power BI](../../includes/sspowerbi-md.md)] integration and email delivery with subscription processing, you need to take extra configuration steps.

## Prerequisites

- The hardware and software that are required for SQL Server 2016. For more information, see [SQL Server 2016 and 2017: Hardware and software requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).

- The ability to install SSRS and the [!INCLUDE[SQL Server no version](../../includes/ssnoversion-md.md)] database engine together in the same instance. The database engine instance hosts the report server databases that the installation process creates and configures.

- A user account that meets the following requirements:
  - Is a member of the local administrator group
  - Has permission to access and create databases on the database engine instance that hosts the report server databases

- The default values that are needed to reserve the report server and [!INCLUDE[SSRS web portal](../../includes/ssrswebportal.md)] URLs. These values are port 80, a strong wildcard, and virtual directory names in the format *ReportServer_\<instance-name\>* and *Reports_\<instance-name\>*.

- The default values that are needed to create the report server databases **ReportServer** and **ReportServerTempDB**. Any databases with these names from previous instances block the installation procedure. To unblock it, you must rename, move, or delete the databases.

- Access to a read-write domain controller.

  > [!IMPORTANT]
  > You can install SSRS in an environment that has a read-only domain controller (RODC). But SSRS needs access to a read-write domain controller to function properly. If SSRS only has access to an RODC, you might encounter errors when you try to administer the service.

## Default configuration

The installation wizard installs the following SSRS features when you select the option for the default configuration for native mode:

- The report server service, which includes the following components:
  - The report server web service
  - The background processing application
  - The [!INCLUDE[SSRS web portal](../../includes/ssrswebportal.md)] for viewing and managing reports and permissions
- Report Server Configuration Manager
- The SSRS command-line utilities rsconfig.exe, rskeymgmt.exe, and rs.exe

If you want to use [!INCLUDE[SQL Server no version](../../includes/ssnoversion-md.md)] [!INCLUDE[SQL Server Management Studio](../../includes/ssmanstudio-md.md)] and [!INCLUDE[SQL Server Data Tools](../../includes/ssbidevstudiofull-md.md)], you need to download and install those components separately.

The installation wizard configures the following components during a native mode report server installation:

- A service account for the report server service
- The report server web service URL
- The [!INCLUDE[SSRS web portal](../../includes/ssrswebportal.md)] URL
- The report server databases
- Service account access to the report server databases
- Connection information, also known as the data source name (DSN), for the report server databases

The installation doesn't configure the unattended execution account, report server email, or a scale-out deployment. It also doesn't back up the encryption keys. You can use Report Server Configuration Manager to configure these properties. For more information, see [What is Report Server Configuration Manager (native mode)?](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).

## When to install the default configuration for native mode

If you want to install SSRS in an operational state, select the default configuration. This mode automates required Report Server Configuration Manager tasks. You can then use the report server immediately after the installation is complete.

If your computer doesn't meet all requirements for a default installation, you can't use the default configuration. Instead, you must install SSRS in files-only mode and then use Report Server Configuration Manager to configure SSRS after the installation is complete. For more information, see [Files-only installation (Reporting Services)](../../reporting-services/install-windows/files-only-installation-reporting-services.md).

## Default URL reservations

URL reservations consist of a prefix, host name, port, and virtual directory:

|Part|Description|
|----------|-----------------|
|Prefix|The default prefix is HTTP. If you install a Transport Layer Security (TLS) certificate beforehand, the installation process tries to create URL reservations that use the HTTPS prefix.|
|Host name|The default host name is a strong wildcard (+). It specifies that the report server accepts any HTTP request on the designated port for any host name that resolves to the computer, including `https://<computername>/reportserver`, `https://localhost/reportserver`, and `https://<IPAddress>/reportserver`.|
|Port|The default port is 80. If you use any port other than port 80, you have to explicitly add that port to the URL when you open an SSRS web application in a browser window.|
|Virtual directory|By default, the installation process creates virtual directories. For the report server web service, the directory format is *ReportServer_\<instance-name\>*, and the default virtual directory is reportserver. For the [!INCLUDE[SSRS web portal](../../includes/ssrswebportal.md)], the format is *Reports_\<instance-name\>*, and the default virtual directory is reports.|

The following URL strings provide examples of the URL reservations:

- `https://+:80/reportserver`, for the report server
- `https://+:80/reports`, for the [!INCLUDE[SSRS web portal](../../includes/ssrswebportal.md)]

If you configure a named instance during the installation process, you need to use the instance name in the report server URL and the web portal URL. For example, if your instance name is *THESQLINSTANCE*, use the following URLs:

- `https://<server-name>/ReportServer_THESQLINSTANCE`
- `https://<server-name>/Reports_THESQLINSTANCE`

For more information, see [Configure report server URLs (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md).

## Install native mode by using the SQL Server installation wizard

Take the steps in the following sections to install SSRS in native mode and configure default settings for the server.

### Take preliminary steps

1. Start the SQL Server installation wizard by using one of the following options:

   - Download SQL Server 2016 and run the downloaded file. Select a custom installation.
   - Insert the SQL Server installation media. From the root folder, run **setup.exe**.

   A SQL Server Installation Center window opens.

1. Under **SQL Server Installation Center**, select **Installation**, and then select **New SQL Server stand-alone installation or add features to an existing installation**.

   :::image type="content" source="media/install-reporting-services-native-mode-report-server/select-installation.png" alt-text="Screenshot of the SQL Server Installation Center in the installation wizard. The Installation item and the option for adding features are highlighted.":::

1. Step through the following preliminary pages:
   - Product Key
   - License Terms
   - Global Rules
   - Microsoft Update
   - Install Setup Files
   - Install Rules

### Select and check settings

1. On the Feature Selection page of the SQL Server installation wizard, select the following features:

   - **Database Engine Services**, unless an instance of the database engine is already installed
   - **Reporting Services - Native**

   :::image type="content" source="media/install-reporting-services-native-mode-report-server/feature-selection-page.png" alt-text="Screenshot of the Feature Selection page in the installation wizard. Database Engine Services and Reporting Services - Native are highlighted.":::

1. On the Feature Rules page, verify that the configuration passes each test.

1. If you want to configure a named instance, take the following steps:
   1. On the Instance Configuration page, select **Named instance**.
   1. Next to **Named instance**, enter the instance name.

   :::image type="content" source="media/install-reporting-services-native-mode-report-server/instance-configuration-page.png" alt-text="Screenshot of the Instance Configuration page in the installation wizard. The Named instance option and the field next to it are highlighted.":::

1. If you want to use the SSRS subscription feature, take the following steps:
   1. On the Server Configuration page, find the row for **SQL Server Agent**.
   1. Under **Startup Type**, select **Automatic**.

   :::image type="content" source="media/install-reporting-services-native-mode-report-server/sql-server-agent-startup-type.png" alt-text="Screenshot of the Server Configuration page. In a table, the SQL Server Agent service and a startup type of Automatic are highlighted.":::

1. On the Database Engine Configuration page, add SQL Server administrators.

1. On the Reporting Services Configuration page, select **Install and configure**.

   :::image type="content" source="media/install-reporting-services-native-mode-report-server/reporting-services-configuration-page.png" alt-text="Screenshot of the Reporting Services Configuration page. The Install and configure option is selected and highlighted.":::

   > [!NOTE]
   > The **Install and Configure** option is available only if you select **Database Engine Services** earlier on the Feature Selection page.

1. On the Feature Configuration Rules page, verify that the configuration passes each test. The setup wizard automatically advances to the next page if no test fails. Among other conditions, the tests verify that a report server catalog and temporary catalog database don't already exist.

### Install the components

1. On the Ready to Install page, note the path to the configuration file, and then select **Install**. The configuration file provides a summary of the server's initial [!INCLUDE[SQL Server no version](../../includes/ssnoversion-md.md)] configuration, including the installed components, the service accounts, and the administrators.

   :::image type="content" source="media/install-reporting-services-native-mode-report-server/configuration-file-path.png" alt-text="Screenshot of the Ready to Install page. Under Configuration file path, a path to a configuration file is highlighted.":::

1. On the Complete page, select **Close**.

   :::image type="content" source="media/install-reporting-services-native-mode-report-server/installation-complete.png" alt-text="Screenshot of the Complete page in the installation wizard. A table lists several features and a status of Succeeded for each one.":::

### Verify the installation

Selecting the default configuration doesn't guarantee that the report server works when the installation process is complete. For instance, the default URLs might not be registered when the service starts. After the SQL Server installation wizard finishes, take the following basic steps to check the installation:

1. Open Report Server Configuration Manager and confirm that you can connect to the report server.

1. Open a browser as an administrator and connect to the [!INCLUDE[SSRS web portal](../../includes/ssrswebportal.md)]. For example, go to `https://localhost/Reports` or `http://localhost/Reports`.

1. Open a browser as an administrator and connect to the SSRS report server page. For example, go to `https://localhost/ReportServer` or `http://localhost/ReportServer`.

For more information, see the following articles:

- [Verify a Reporting Services installation](../../reporting-services/install-windows/verify-a-reporting-services-installation.md)
- [Troubleshoot a Reporting Services installation](../../reporting-services/install-windows/troubleshoot-a-reporting-services-installation.md)

## Other configurations

- To configure [!INCLUDE[Power BI](../../includes/sspowerbi-md.md)] integration so that you can pin report items to a [!INCLUDE[Power BI](../../includes/sspowerbi-md.md)] dashboard, see [Power BI report server integration (Configuration Manager)](../../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md).

- To configure email for subscriptions processing, see [Email settings in Reporting Services native mode (Report Server Configuration Manager)](../../reporting-services/install-windows/e-mail-settings-reporting-services-native-mode-configuration-manager.md) and [Email delivery in Reporting Services](../../reporting-services/subscriptions/e-mail-delivery-in-reporting-services.md).

- To configure the web portal so that you can access it on a report computer to view and manage reports, see [Configure a firewall for report server access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md) and [Configure a report server for remote administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md).

## Related content

- [Configure the report server service account (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)
- [Configure a report server database connection (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)
- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
