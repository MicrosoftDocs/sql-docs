---
title: "Analysis Services | Microsoft Docs"
ms.date: "05/11/2017"
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
# What is Analysis Services?
  Analysis Services is an analytical data engine used in decision support and business analytics, providing the analytical data for business reports and client applications such as Power BI, Excel, Reporting Services reports, and other data visualization tools.  
  
 A typical workflow includes authoring a multidimensional or tabular data model, deploying the model as a database to an on-premises SQL Server Analysis Services or Azure Analysis Services server instance, setting up recurring data processing, and assigning permissions to allow data access by end-users. When it's ready to go, your semantic data model can be accessed by any client application supporting Analysis Services as a data source.  
 
## Analysis Services on-premises and in the cloud
Analysis Services is now available in the cloud as an Azure service. Azure Analysis Services supports tabular models at the 1200 and higher compatibility levels. DirectQuery, partitions, row-level security, bi-directional relationships, and translations are all supported. To learn more and give it a try for free, see [Azure Analysis Services](https://azure.microsoft.com/en-us/services/analysis-services/). 
  
## Server mode  
 When installing Analysis Services by using SQL Server Setup, during configuration you specify a server mode for that instance.  Each mode includes different features unique to a particular  Analysis Services solution.   
  
-   **Tabular Mode** - Implement in-memory relational data modeling constructs (model, tables, columns, measures, hierarchies).  

-   **Multidimensional and Data Mining Mode** - Implement OLAP modeling constructs (cubes, dimensions, measures). 

-   **Power Pivot Mode** - Implement Power Pivot and Excel data models in SharePoint (Power Pivot for SharePoint is a middle-tier data engine that loads, queries, and refreshes data models hosted in SharePoint).  
  
 A single instance can be configured with only one mode,  and cannot be changed later.  You can install multiple instances with different modes on the same server, but you'll need to run Setup and specify configuration settings for each instance. For detailed information and a comparison of different features offered in each of the modes, see [Comparing Tabular and Multidimensional Solutions](../analysis-services/comparing-tabular-and-multidimensional-solutions-ssas.md).
  
## Authoring and managing solutions  
 To create a model and deploy it to a server, you use SQL Server Data Tools, choosing either a Tabular or Multidimensional and Data Mining project template. The project template contains folders for all of the objects needed in a model. Wizards and designers help you create many of the basic elements such as connecting to data sources, relationships, measures, and roles. Once your model database is deployed to a server, you use SQL Server Management Studio (SSMS) to configure data processing, monitor, and manage your server and databases. To learn more, see [Tools and applications used in Analysis Services](../analysis-services/tools-and-applications-used-in-analysis-services.md). 
  
## Documentation by area  
In-general, documentation for Azure Analysis Services is included with Azure documentation. And, documentation for SQL Server Analysis Services is included with SQL documentation. However, at least for tabular models, how you create and deploy your projects is much the same regardless of what platform you're using.  
   
*  [Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/)
*  [What's New in SQL Server Analysis Services](../analysis-services/what-s-new-in-analysis-services.md)   
*  [Comparing Tabular and Multidimensional Solutions](../analysis-services/comparing-tabular-and-multidimensional-solutions-ssas.md)   
*  [Tabular Models](../analysis-services/tabular-models/tabular-models-ssas.md)  
*  [Multidimensional Models](../analysis-services/multidimensional-models/multidimensional-models-ssas.md)  
*  [Data Mining](../analysis-services/data-mining/data-mining-ssas.md)  
*  [Power Pivot for SharePoint](../analysis-services/power-pivot-sharepoint/power-pivot-for-sharepoint-ssas.md)  
*  [Instance Management](../analysis-services/instances/analysis-services-instance-management.md)    
*  [Tutorials](../analysis-services/analysis-services-tutorials-ssas.md)   
*  [Developer Documentation](https://msdn.microsoft.com/library/bb500153(SQL.130).aspx)  
*  [Technical Reference (SSAS)](../analysis-services/powershell/technical-reference-ssas.md)