---
title: "Configure a firewall for report server access"
description: "Learn how to manually configure Windows Firewall to allow access to Report Server applications and published reports by opening necessary ports."
author: maggiesMSFT
ms.author: maggies
ms.date: 08/29/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "firewall systems [Reporting Services]"
  - "configuring servers [Reporting Services]"
#customer intent: As an IT administrator, I want to configure Windows Firewall to allow access to Report Server applications and published reports by opening the necessary ports.
---
# Configure a firewall for report server access

In this article, you learn how to manually open necessary ports in Windows Firewall to allow access to SQL Server Reporting Services (SSRS) report server applications and published reports. You access SSRS report server applications and published reports through URLs that specify an IP address, port, and virtual directory. If you enable Windows Firewall, it might block the port used by the report server, leading to issues like receiving a blank page when trying to access the web portal or when requesting a report. To resolve these issues, you must perform this step manually. Follow the instructions in this article to use the Windows Firewall utility to open a port on the report server.

## Prerequisites

- [Configure the service account](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)
- [Create the report server database](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md)
- [Configure URLs for the Report Server web service and the web portal](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md).
- Access to the report server over a local web browser connection
- Verify that the installation is configured correctly
- [Add the report server to Trusted Sites if you're on Windows Server](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).

## Open ports in Windows Firewall

> [!NOTE]
> By default, the report server listens for HTTP requests on port 80. The following instructions include steps to open this port. If you configure the report server URLs to use a different port, replace port 80 with the correct port number in the instructions. For more information, see [Configure report server URLs (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md).
>
> Open port 1433 and 1434 on the external computer if one of the following statements is true:
>
> - You access [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases on external computers.
> - The report server database is on an external [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

To open port 80, complete the following steps:

1. Open the **Windows Firewall** configuration settings on your report server. Find the configuration settings by searching in **Control Panel** for **Windows Firewall**.
1. Select **Advanced Settings**.
1. Select **Inbound Rules**.
1. In the **Actions** pane, select **New Rule**.
1. Choose the **Port** rule type and select **Next**.
1. On the **Protocol and Ports** page, choose **TCP**.
1. Select **Specific Local Ports**, enter a value of **80**, and select **Next**.
1. On the **Action** page, select **Allow the connection** and select **Next**.
1. On the **Profile** page, select the appropriate options for your environment and select **Next**.
1. On the **Name** page, enter the name **ReportServer (TCP on port 80)** and select **Finish**. 
1. Restart the computer.

## Validate port configuration

After you open the port, grant user access to the report server through role assignments on at the Home and Site levels. This action is done before confirming whether remote users can access the report server on the opened port. Even if the port is opened correctly, Report Server connections might still fail if users lack sufficient permissions. For more information, see [Grant user access to a report server](../../reporting-services/security/grant-user-access-to-a-report-server.md).

You can also verify that the port is opened correctly by starting the web portal on a different computer. For more information, see [What is the report server web portal (Native mode)?](../../reporting-services/web-portal-ssrs-native-mode.md).

## Related content

- [Configure Windows Firewall for database engine access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md) 
- [Configure the Windows Firewall to allow SQL Server access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)