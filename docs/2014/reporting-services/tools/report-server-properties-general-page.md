---
title: "Server Properties (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.reportserver.serverproperties.general.f1"
ms.assetid: 23537d52-4356-450f-a671-5921cef2431f
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Server Properties (General Page)
  Use this page to view or modify the title used in Report Manager, enable or disable My Reports, select a role definition for My Reports security, and enable or disable the client print control.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server instance, right-click the report server name, and then select **Properties**.  
  
 The server mode determines which server properties you can set. If you are managing a report server that is configured for SharePoint integrated mode, you cannot enable My Reports or set the application title for Report Manager.  
  
## Options  
 **Name**  
 Type an application name that appears in Report Manager. By default, this value is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The name that you specify appears only in Report Manager.  
  
 **Version**  
 This property is read-only. Specifies the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] that you are using.  
  
 **Edition**  
 This property is read-only. Specifies the current report server instance. Report Manager is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 **Authentication Mode**  
 This property is read-only. It identifies the types of authentication requests accepted by the report server instance. To change the authentication mode, you must edit the RSReportServer.config file. For more information, see [Authentication with the Report Server](../security/authentication-with-the-report-server.md).  
  
 **URL**  
 This property is read-only. Specifies the URL to the Report Server Web service. This value is specified in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. For more information, see [Configure a URL  &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-url-ssrs-configuration-manager.md).  
  
 **Enable a My Reports folder for each user**  
 Make My Reports available to users. This option is only available for native mode report servers.  
  
 **Select the role to apply to each My Reports folder**  
 Specify a role definition to use for My Reports security. The role definition identifies the set of tasks that are supported in each My Reports folder.  
  
 **Enable download for the ActiveX client print control**  
 Sets the `EnableClientPrinting` report server system property. If you enable client printing, users who have local administrator permissions have the option of downloading a signed ActiveX control for printing HTML reports. For more information, see [Enable and Disable Client-Side Printing for Reporting Services](../report-server/enable-and-disable-client-side-printing-for-reporting-services.md).  
  
## See Also  
 [Set Report Server Properties &#40;Management Studio&#41;](set-report-server-properties-management-studio.md)   
 [Connect to a Report Server in Management Studio](connect-to-a-report-server-in-management-studio.md)   
 [Enable and Disable My Reports](../report-server/enable-and-disable-my-reports.md)   
 [Report Server in Management Studio F1 Help](report-server-in-management-studio-f1-help.md)   
 [Secure My Reports](../security/secure-my-reports.md)  
  
  
