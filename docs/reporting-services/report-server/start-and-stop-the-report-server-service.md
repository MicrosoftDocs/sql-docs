---
title: "Start and stop the Report Server service"
description: Learn how to start and stop the Windows service that contains the Report Server Web service, the web portal, and a background processing application.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/22/2021
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "stopping Report Server service"
  - "Report Server Windows service, starting"
  - "Report Server service, starting"
  - "starting Report Server service"
---

# Start and stop the Report Server service

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

  A report server is implemented as a Windows service that contains the Report Server web service, the web portal, and a background processing application. The service must be running if you want to use any report server functionality. Stopping the service stops all report server operations.  
  
 While the service is stopped, requests for scheduled report and subscription processing that would occur if the service is running are added to the queue. This result happens because jobs that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs create the events. If you want to avoid a backlog of operations while the service is off, consider stopping [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent as well.  
  
 You can use various tools to start or stop the Report Server service. These tools include the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, and the Services tool provided in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows.  
  
> [!NOTE]
> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is only an option for SQL Server Reporting Services 2016 and earlier. It does not include Reporting Services 2017 and later or Power BI Report Server.
  
 If you're doing more than starting or stopping the service, you must use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. This tool is designed for comprehensive configuration tasks beyond basic service management. Using other tools to change the service account can break your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation and encryption key. For more information, see [Configure the Report Server service account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
 You can't pause and resume the service. There aren't start parameters. Although there are no explicit dependencies, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be running if you support any subscriptions or scheduled report operations on the report server.  
  
## Use the Reporting Services Configuration tool  
  
1. Start [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server.  
  
1. On the **Report Server Status** page, select **Stop** or **Start**.  
  
## Use administrative tools  

1. In Administrative Tools, open **Services**.
1. Right-click **SQL Server Reporting Services (MSSQLSERVER)**, **SQL Server Reporting Services**, or **Power BI Report Server**.
1. Select **Stop** or **Restart**.

  
For Reporting Services 2016 and earlier versions, you must verify that the instance name in parentheses corresponds to the report server instance you want to stop or restart. Specifically, you must follow these steps if you're running multiple instances or if the report server is running as a named instance.

  
## Related content  
 [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
 [Start, stop, or pause the SQL Server agent service](../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)  
  
