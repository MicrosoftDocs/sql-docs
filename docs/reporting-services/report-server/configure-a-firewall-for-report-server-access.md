---
title: "Configure a firewall for report server access"
description: Learn how to configure Windows Firewall to allow access to Report Server applications and published reports that are accessed through URLs.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/18/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "firewall systems [Reporting Services]"
  - "configuring servers [Reporting Services]"
---
# Configure a firewall for report server access

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server applications and published reports are accessed through URLs that specify an IP address, port, and virtual directory. If Windows Firewall is turned on, the port that the report server is configured to use is most likely closed. Indications that a port might be closed are: receiving a blank page when you attempt to open the web portal from a remote client computer, or a blank Web page after requesting a report.

To open a port, you must use the Windows Firewall utility on the report server computer. Reporting Services doesn't open ports for you. You must perform this step manually.

By default, the report server listens for HTTP requests on port 80. As such, the following instructions include steps that specify that port. If you configured the report server URLs to use a different port, you must specify that port number when following the instructions in this article.

You must open port 1433 and 1434 on the external computer if one of the following statements is true:

- If you're accessing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases on external computers
- If the report server database is on an external [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance

For more information, see [Configure a Windows Firewall for database engine access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md). For more information about the default Windows Firewall settings, and a description of the TCP ports that affect the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Configure the Windows Firewall to allow SQL Server access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).

## Prerequisites

These instructions assume that you already configured the service account. They also assume you created the report server database and configured URLs for the Report Server Web service and the web portal. For more information, see [Manage a Reporting Services native mode report server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md).

You should also verify that the report server is accessible over a local Web browser connection to the local report server instance. This step establishes that you have a working installation. You should verify that the installation is configured correctly before you begin opening ports. To complete this step on Windows Server, you must also add the report server site to Trusted Sites. For more information, see [Configure a native mode report server for local administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).

## Open ports in Windows Firewall

To open port 80:

1. From the **Start** menu, select **Control Panel**, select **System and Security**, and then choose **Windows Firewall**. Control Panel isn't configured for **Category** view, you only need to select **Windows Firewall.**

1. Select **Advanced Settings**.

1. Select **Inbound Rules**.

1. Select **New Rule** in the **Actions** window.

1. Choose the **Port** rule type.

1. Select **Next**.

1. On the **Protocol and Ports** page, choose **TCP**.

1. Select **Specific Local Ports** and enter a value of **80**.

1. Select **Next**.

1. On the **Action** page, select **Allow the connection**.

1. Select **Next**.

1. On the **Profile** page, select the appropriate options for your environment.

1. Select **Next**.

1. On the **Name** page, enter the name of **ReportServer (TCP on port 80)**.

1. Select **Finish**.

1. Restart the computer.

## Next steps

After you open the port, you must grant user access to the report server through role assignments on Home and at the site level. This action is done before confirming whether remote users can access the report server on the opened port. You can open a port correctly and still have report server connections fail if users don't have sufficient permissions. For more information, see [Grant user access to a report server](../../reporting-services/security/grant-user-access-to-a-report-server.md).

You can also verify that the port is opened correctly by starting the web portal on a different computer. For more information, see [The web portal of a report server (SSRS Native Mode)](../../reporting-services/web-portal-ssrs-native-mode.md).

## Related content

- [Configure the report server service account (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)
- [Configure report server URLs (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)
- [Create a report server database (Report Server Configuration Manager)](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md)
- [Configure the report server service account (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)
- [Manage a Reporting Services native mode report server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)  
