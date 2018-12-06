---
title: "Introduction to SQL Server Management Studio for Business Intelligence | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Management Studio for Integration Services"
  - "SQL Server Management Studio for Reporting Services"
  - "SQL Server Management Studio for Analysis Services"
ms.assetid: ffaa77b7-03d0-4d7a-aa42-c5091a4f2ceb
author: stevestein
ms.author: sstein
manager: craigg
---
# Introduction to SQL Server Management Studio for Business Intelligence
  To access, configure, manage, and administer [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], and [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. Although all three business intelligence technologies rely on [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], the administrative tasks associated with each of these technologies are slightly different.  
  
> [!NOTE]  
>  To create and modify [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], and [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] solutions, use [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], not [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] is a development environment that is based on [!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[vsprvs](../includes/vsprvs-md.md)].  
  
## Managing Analysis Services Solutions Using SQL Server Management Studio  
 [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] enables you to manage [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] objects, such as performing back-ups and processing objects.  
  
 [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] provides an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Script project in which you develop and save scripts written in Multidimensional Expressions (MDX), Data Mining Extensions (DMX), and XML for Analysis (XMLA). You use [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Scripts projects to perform management tasks or re-create objects, such as database and cubes, on [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instances. For example, you can develop an XMLA script in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Script project that creates new objects directly on an existing [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance. The [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Scripts projects can be saved as part of a solution and integrated with source code control.  
  
 For more information about how to use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], see [Analysis Services Scripts Project in SQL Server Management Studio](../analysis-services/instances/analysis-services-scripts-project-in-sql-server-management-studio.md).  
  
## Managing Integration Services Solutions Using SQL Server Management Studio  
 [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] enables you to use the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service to manage packages and monitor running packages. You can also use [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] to organize packages into folders, run packages, import and export packages, migrate Data Transformation Services (DTS) packages, and upgrade [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages.  
  
## Managing Reporting Services Projects Using SQL Server Management Studio  
 Use SQL Server Management Studio to enable Reporting Services features, administer the server and databases, and manage roles and jobs.  
  
 You manage shared schedules by using the Shared Schedules folder, and manage report server databases (ReportServer, ReportServerTempdb). You also create a RSExecRole in the Master system database when you move a report server database to a new or different SQL Server Database Engine ([!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)]). For more information about these tasks, see the following topics:  
  
-   [Reporting Services in SQL Server Management Studio &#40;SSRS&#41;](../reporting-services/tools/reporting-services-in-sql-server-management-studio-ssrs.md)  
  
-   [Administer a Report Server Database &#40;SSRS Native Mode&#41;](../reporting-services/report-server/report-server-database-ssrs-native-mode.md)  
  
-   [Create the RSExecRole](../reporting-services/security/create-the-rsexecrole.md)  
  
 You also manage the server by enabling and configuring various features, setting server defaults, and managing roles and jobs. For more information about these tasks, see the following topics:  
  
-   [Set Report Server Properties &#40;Management Studio&#41;](../reporting-services/tools/set-report-server-properties-management-studio.md)  
  
-   [Create, Delete, or Modify a Role &#40;Management Studio&#41;](../reporting-services/security/role-definitions-create-delete-or-modify.md)  
  
-   [Enable and Disable Client-Side Printing for Reporting Services](../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md)  
  
## See Also  
 [Creating Multidimensional Models Using SQL Server Data Tools &#40;SSDT&#41;](../analysis-services/multidimensional-models/creating-multidimensional-models-using-sql-server-data-tools-ssdt.md)   
 [Reporting Services in SQL Server Data Tools &#40;SSDT&#41;](../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md)  
  
  
