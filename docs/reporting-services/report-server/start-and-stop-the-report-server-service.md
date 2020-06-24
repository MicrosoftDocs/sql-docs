---
title: "Start and Stop the Report Server Service | Microsoft Docs"
description: Learn how to start and stop the Windows service that contains the Report Server Web service, the web portal, and a background processing application.
ms.date: 05/21/2019
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: report-server


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

  A report server is implemented as a Windows service that contains the Report Server Web service, the web portal, and a background processing application. The service must be running if you want to use any report server functionality. Stopping the service stops all report server operations.  
  
 While the service is stopped, requests for scheduled report and subscription processing that would have occurred had the service been running are added to the queue. This is because jobs that are run by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent create the events. If you want to avoid a backlog of operations while the service is off, consider stopping [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent as well.  
  
 You can use a variety of tools to start or stop the Report Server service, including the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, and the Services tool provided in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows.  
  
 If you're doing more than starting or stopping the service, such as changing the service account, you must use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. Using other tools to change the service account can break your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation. For more information, see [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
 You can't pause and resume the service. There aren't start parameters. Although there are no explicit dependencies, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be running if you support any subscriptions or scheduled report operations on the report server.  
  
## Use the Reporting Services Configuration tool  
  
1. Start [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server.  
  
2. On the Report Server Status page, select **Stop** or **Start**.  
  
## Use Administrative Tools  

- In Administrative Tools, open Services, right-click **SQL Server  Reporting Services (MSSQLSERVER)**, and select **Stop** or **Restart**.  
  
- If you are running multiple instances, or if the report server is running as a named instance, verify that the instance name in parentheses corresponds to the report server instance you want to stop or restart.  
  
## See also  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
 [Start, Stop, or Pause the SQL Server Agent Service](https://msdn.microsoft.com/library/c95a9759-dd30-4ab6-9ab0-087bb3bfb97c)  
  