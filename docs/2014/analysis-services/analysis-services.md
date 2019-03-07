---
title: "Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Analysis Services, about Analysis Services - Multidimensional Data"
  - "SSAS"
  - "Analysis Services"
  - "SQL Server Analysis Services, about Analysis Services - Multidimensional Data"
  - "SQL Server Analysis Services"
  - "multidimensional data [Analysis Services]"
  - "SSAS, about Analysis Services - Multidimensional Data"
ms.assetid: 49d186f4-4b4d-4a5a-bb1a-e2699c64a731
author: minewiskan
ms.author: owend
manager: craigg
---
# Analysis Services
  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] is an online analytical data engine used in decision support and business intelligence (BI) solutions, providing the analytical data for business reports and client applications such as Excel, Reporting Services reports, and other third-party BI tools. A typical workflow for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] includes building an OLAP or tabular data model, deploy the model as a database to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance, process the database to load it with data, and then assign permissions to allow data access. When it's ready to go, this multi-purpose data model can be accessed by any client application supporting [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] as a data source.  
  
 To create a model, use SQL Server Data Tools (see [Tools and applications used in Analysis Services](tools-and-applications-used-in-analysis-services.md)), choosing either a Tabular or Multidimensional and Data Mining project template. The project template contains folders for all of the objects needed in a model. You can use wizards to create all of the basic elements, such as data sources, data source views, dimensions, cubes, and roles.  
  
 Models are populated with data from external data systems, usually data warehouses hosted on a SQL Server or Oracle relational database engine (Tabular models support additional data source types). Models specify query objects, such as cubes, but also specify dimensions that can be used in multiple cubes, calculations and KPIs that encapsulate business logic, and interactions such as navigation and drill-through behaviors.  
  
 To use a model, it's deployed to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance that runs databases in a particular server mode, making the data available to authorized users who connect through Excel or other applications.  
  
 You can install an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance in one of three server modes:  
  
-   As a Tabular instance, running Tabular models.  
  
-   As a Multidimensional and Data Mining instance, running OLAP cubes and data mining models (this is the default).  
  
-   As PowerPivot for SharePoint, running PowerPivot and Excel data models in SharePoint (PowerPivot for SharePoint is a middle-tier data engine that loads, queries, and refreshes data models hosted in SharePoint).  
  
 Same data engine; three different ways to use it. Note that server modes are set during installation and cannot be changed later. You should install a new instance if you require a different mode.  
  
 Foundational documentation for Analysis Services is organized into sections that correspond to the type of project you are building. Choose from the following links to learn more about each mode or feature area.  
  
 **Browse Content by Area**  
 ![Small File Folder Icon](../../2014/integration-services/media/filefolder-small.gif "Small File Folder Icon") [Comparing Tabular and Multidimensional Solutions &#40;SSAS&#41;](comparing-tabular-and-multidimensional-solutions-ssas.md)  
  
 ![Small File Folder Icon](../../2014/integration-services/media/filefolder-small.gif "Small File Folder Icon") [Analysis Services Instance Management](instances/analysis-services-instance-management.md)  
  
 ![Small File Folder Icon](../../2014/integration-services/media/filefolder-small.gif "Small File Folder Icon") [Tabular Modeling &#40;SSAS Tabular&#41;](tabular-models/tabular-models-ssas.md)  
  
 ![Small File Folder Icon](../../2014/integration-services/media/filefolder-small.gif "Small File Folder Icon") [Multidimensional Modeling &#40;SSAS&#41;](multidimensional-models/multidimensional-models-ssas.md)  
  
 ![Small File Folder Icon](../../2014/integration-services/media/filefolder-small.gif "Small File Folder Icon") [Data Mining &#40;SSAS&#41;](data-mining/data-mining-ssas.md)  
  
 ![Small File Folder Icon](../../2014/integration-services/media/filefolder-small.gif "Small File Folder Icon") [PowerPivot for SharePoint &#40;SSAS&#41;](power-pivot-sharepoint/power-pivot-for-sharepoint-ssas.md)  
  
> [!NOTE]  
>  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features vary by edition. Multidimensional and data mining models are available in standard edition, but with fewer features than higher editions. Tabular models and PowerPivot for SharePoint are premium features and are not available in a standard edition license. For more information, see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## See Also  
 [Analysis Services Tutorials &#40;SSAS&#41;](analysis-services-tutorials-ssas.md)   
 [Installation for SQL Server 2014](../database-engine/install-windows/installation-for-sql-server.md)   
 [Developer's Guide &#40;Analysis Services&#41;](analysis-services-developer-documentation.md)   
 [SQL Server Resource Center](https://go.microsoft.com/fwlink/?linkID=219676)   
 [SQLCat.com](https://go.microsoft.com/fwlink/?linkID=220963)  
  
  
