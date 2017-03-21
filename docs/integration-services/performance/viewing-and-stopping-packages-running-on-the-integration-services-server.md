---
title: "Viewing and Stopping Packages Running on the Integration Services Server | Microsoft Docs"
ms.custom: ""
ms.date: "2016-08-26"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "packages [Integration Services], managing"
  - "managing running packages [Integration Services]"
  - "packages [Integration Services], running"
  - "running package [Integration Services], managing"
ms.assetid: 11bf44e6-f6b0-475f-b816-40e914dbac80
caps.latest.revision: 18
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Viewing and Stopping Packages Running on the Integration Services Server
  The **SSISDB** database stores execution history in internal tables that are not visible to users. However it exposes the information that you need through public views that you can query. It also provides stored procedures that you can call to perform common tasks related to packages.  
  
 Typically you manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects on the server in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. However you can also query the database views and call the stored procedures directly, or write custom code that calls the managed API. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and the managed API query the views and call the stored procedures to perform many of their tasks. For example, you can view the list of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages that are currently running on the server, and request packages to stop if you have to.  
  
## Viewing the List of Running Packages  
 You can view the list of packages that are currently running on the server in the **Active Operations** dialog box. For more information, see [Active Operations Dialog Box](../../integration-services/performance/active-operations-dialog-box.md).  
  
 For information about the other methods that you can use to view the list of running packages, see the following topics.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] access  
 To view the list of packages that are running on the server, query the view, [catalog.executions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-executions-ssisdb-database.md) for packages that have a status of 2.  
  
 Programmatic access through the managed API  
 See the <xref:Microsoft.SqlServer.Management.IntegrationServices> namespace and its classes.  
  
## Stopping a Running Package  
 You can request a running package to stop in the **Active Operations** dialog box. For more information, see [Active Operations Dialog Box](../../integration-services/performance/active-operations-dialog-box.md).  
  
 For information about the other methods that you can use to stop a running package, see the following topics.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] access  
 To stop a package that is running on the server, call the stored procedure, [catalog.stop_operation &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-stop-operation-ssisdb-database.md).  
  
 Programmatic access through the managed API  
 See the <xref:Microsoft.SqlServer.Management.IntegrationServices> namespace and its classes.  
  
## Viewing the History of Packages That Have Run  
 To view the history of packages that have run in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], use the **All Executions** report. For more information on the **All Executions** report and other standard reports, see [Reports for the Integration Services Server](../../integration-services/performance/reports-for-the-integration-services-server.md).  
  
 For information about the other methods that you can use to view the history of running packages, see the following topics.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] access  
 To view information about packages that have run, query the view, [catalog.executions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-executions-ssisdb-database.md).  
  
 Programmatic access through the managed API  
 See the <xref:Microsoft.SqlServer.Management.IntegrationServices> namespace and its classes.  
  
## See Also  
 [Execution of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx)   
 [Troubleshooting Reports for Package Execution](https://msdn.microsoft.com/library/gg471512.aspx)  
  
  