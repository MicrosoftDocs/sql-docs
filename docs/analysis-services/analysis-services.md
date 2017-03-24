---
title: "Analysis Services | Microsoft Docs"
ms.date: "03/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
helpviewer_keywords: 
  - "Analysis Services, about Analysis Services - Multidimensional Data"
  - "SSAS"
  - "Analysis Services"
  - "SQL Server Analysis Services, about Analysis Services - Multidimensional Data"
  - "SQL Server Analysis Services"
  - "multidimensional data [Analysis Services]"
  - "SSAS, about Analysis Services - Multidimensional Data"
ms.assetid: 49d186f4-4b4d-4a5a-bb1a-e2699c64a731
caps.latest.revision: 60
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Analysis Services
  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] is an online analytical data engine used in decision support and business analytics, providing the analytical data for business reports and client applications such as Power BI, Excel, Reporting Services reports, and other data visualization tools.  
  
 A typical workflow for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] includes authoring a Multidimensional or Tabular data model, deploying the model as a database to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance, processing the database to load it with data or metadata, setting up data refresh, and assigning permissions to allow data access by end-users. When it's ready to go, this multi-purpose semantic data model can be accessed by any client application supporting [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] as a data source.  
  
 Models are populated with data from external data systems, usually data warehouses hosted on a SQL Server or Oracle relational database engine (Tabular models support additional data source types). Models specify query objects, such as cubes, but also specify dimensions that can be used in multiple cubes, calculations and KPIs that encapsulate business logic, and interactions such as navigation and drill-through behaviors.  
 
## Analysis Services on-premises and in the cloud
Analysis Services is now available in the cloud as an Azure service. Currently in preview, Azure Analysis Services supports tabular models at the 1200 compatibility level. DirectQuery, partitions, row-level security, bi-directional relationships, and translations are all supported. To learn more and give it a try for free, see [Azure Analysis Services](https://azure.microsoft.com/en-us/services/analysis-services/). 
  
## Server mode  
 When installing Analysis Services by using [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] Setup, during configuration you specify a server mode for that instance.  Each mode includes different features unique to a particular  Analysis Services solution.  
  
-   **Multidimensional and Data Mining Mode** - Implement OLAP modeling constructs (cubes, dimensions, measures).  
  
-   **Tabular Mode** - Implement in-memory relational data modeling constructs (model, tables, columns).  
  
     Tabular models can be created at the default compatibility level 1200; using the latest functionality, or at the older 1103 compatibility level. There are significant differences between  compatibility levels. See [Compatibility Level for Tabular models in Analysis Services](../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md) for information on how the levels compare.  
  
-   **Power Pivot Mode** - Implement [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] and Excel data models in SharePoint ([!INCLUDE[ssGemini](../includes/ssgemini-md.md)] for SharePoint is a middle-tier data engine that loads, queries, and refreshes data models hosted in SharePoint).  
  
 A single instance can be configured with only one mode,  and cannot be changed later.  You can install multiple instances with different modes on the same server, but you'll need to run Setup and specify configuration settings for each instance.  
  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features vary by edition. For more information, see [Editions and Supported Features for SQL Server 2016](../sql-server/editions-and-supported-features-for-sql-server-2016.md) 
  
## Authoring solutions  
 To create a model, you use SQL Server Data Tools (see [Tools and applications used in Analysis Services](../analysis-services/tools-and-applications-used-in-analysis-services.md)), choosing either a Tabular or Multidimensional and Data Mining project template. The project template contains folders for all of the objects needed in a model. Wizards help create many of the basic elements, such as data sources, data source views, dimensions, cubes, and roles.  
  
## Documentation by area  
Documentation for Analysis Services is organized into sections that correspond to the type of project you are building. Choose from the following links to learn more about each mode or feature area.  
   
 ![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [What's New](../analysis-services/what-s-new-in-analysis-services.md)  
  
 ![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Comparing Tabular and Multidimensional Solutions](../analysis-services/comparing-tabular-and-multidimensional-solutions-ssas.md)  
  
 ![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Tabular Models](../analysis-services/tabular-models/tabular-models-ssas.md)  
  
 ![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Multidimensional Models](../analysis-services/multidimensional-models/multidimensional-models-ssas.md)  
  
 ![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Data Mining](../analysis-services/data-mining/data-mining-ssas.md)  
  
 ![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Power Pivot for SharePoint](../analysis-services/power-pivot-sharepoint/power-pivot-for-sharepoint-ssas.md)  
  
 ![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Analysis Services Instance Management](../analysis-services/instances/analysis-services-instance-management.md)  
   
 ![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Analysis Services Tutorials](../analysis-services/analysis-services-tutorials-ssas.md)  
  
![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Analysis Services Developer Documentation](https://msdn.microsoft.com/library/bb500153(SQL.130).aspx)  
 
![Small File Folder Icon](../analysis-services/media/filefolder-small.png "Small File Folder Icon") [Technical Reference (SSAS)](../analysis-services/powershell/technical-reference-ssas.md)