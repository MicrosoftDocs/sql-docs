---
title: "Server properties (General page)"
description: Learn about the options on the Report Server Properties page.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/08/2016
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.serverproperties.general.f1"
---
# Server properties (General page)
  Use this page to: 
  - View or modify the title used in Report Manager
  - Enable or disable My Reports
  - Select a role definition for My Reports security
  - Enable or disable the client print control  
  
 **To open this page:**
 1) Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]
 2) Connect to a report server instance.
 3) Right-click the report server name, and then select **Properties**.  
  
 The server mode determines which server properties you can set. If you manage a report server that is configured for SharePoint integrated mode, you can't enable My Reports or set the title for the web portal.  
  
## Options  
 **Name**  
 Enter a name that appears on top of the web portal. By default, this value is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The name that you specify appears only in Report Manager.  
  
 **Version**  
 This property is read-only. Specifies the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] that you're using.  
  
 **Edition**  
 This property is read-only. Specifies the current report server instance. Report Manager isn't available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).
  
 **Authentication Mode**  
 This property is read-only. It identifies the types of authentication requests accepted by the report server instance. To change the authentication mode, you must edit the **RSReportServer.config** file. For more information, see [Authentication with the report server](../../reporting-services/security/authentication-with-the-report-server.md).  
  
 **URL**  
 This property is read-only. Specifies the URL to the Report Server Web service. This value is specified in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. For more information, see [Configure a URL  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md).  
  
 **Enable a My Reports folder for each user**  
 Make **My Reports** available to users. This option is only available for native mode report servers.  
  
 **Select the role to apply to each My Reports folder**  
 Specify a role definition to use for My Reports security. The role definition identifies the set of tasks that are supported in each My Reports folder.  

  
## Related content  
 [Set report server properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md)   
 [Connect to a report server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
 [Enable and disable My Reports](../../reporting-services/report-server/enable-and-disable-my-reports.md)   
 [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
 [Secure My Reports](../../reporting-services/security/secure-my-reports.md)  
  
  

