---
title: "Start and Stop the Report Server Service | Microsoft Docs"
description: Learn how to start and stop the Windows service that contains the Report Server Web service, the web portal, and a background processing application.
ms.date: 03/22/2021
ms.service: reporting-services
ms.subservice: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "stopping Report Server service"
  - "Report Server Windows service, starting"
  - "Report Server service, starting"
  - "starting Report Server service"
ms.assetid: 6ec69ac3-27b0-472d-91e1-733af9078ed2
author: maggiesMSFT
ms.author: maggies
---

# Start and stop the report server service

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

  A report server is implemented as a Windows service that contains the Report Server web service, the web portal, and a background processing application. The service must be running if you want to use any report server functionality. Stopping the service stops all report server operations.  
  
 While the service is stopped, requests for scheduled report and subscription processing that would have occurred had the service been running are added to the queue. This is because jobs that are run by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent create the events. If you want to avoid a backlog of operations while the service is off, consider stopping [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent as well.  
  
 You can use a variety of tools to start or stop the Report Server service, including the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, and the Services tool provided in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows.  
  
> [!NOTE]
> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is only an option for SQL Server Reporting Services 2016 and earlier. It does not include Reporting Services 2017 and later or Power BI Report Server.
  
 If you're doing more than starting or stopping the service, such as changing the service account, you must use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. Using other tools to change the service account can break your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation and encryption key. For more information, see [Configure the Report Server Service Account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
 You can't pause and resume the service. There aren't start parameters. Although there are no explicit dependencies, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be running if you support any subscriptions or scheduled report operations on the report server.  
  
## Use the Reporting Services Configuration tool  
  
1. Start [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server.  
  
2. On the Report Server Status page, select **Stop** or **Start**.  
  
## Use Administrative Tools  

1. In Administrative Tools, open **Services**.
2. Right-click **SQL Server Reporting Services (MSSQLSERVER)**, **SQL Server Reporting Services**, or **Power BI Report Server**.
3. Select **Stop** or **Restart**.

  
For Reporting Services 2016 and earlier versions, if you are running multiple instances or if the report server is running as a named instance, verify that the instance name in parentheses corresponds to the report server instance you want to stop or restart.  

  
## See also  
 [Report Server Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
 [Start, Stop, or Pause the SQL Server Agent Service](../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)  
  
