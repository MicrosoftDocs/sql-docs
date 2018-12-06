---
title: "Data Mining (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining [Analysis Services], about data mining"
ms.assetid: b1c912da-72f6-4d96-89c8-55a2c4f19e88
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Mining (SSAS)
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides an integrated platform for solutions that incorporate data mining. You can use either relational or cube data to create business intelligence solutions with predictive analytics.  
  
## Benefits of Data Mining  
 Data mining uses well-researched statistical principles to discover patterns in your data, helping you make intelligent decisions about complex problems. By applying the data mining algorithms in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to your data, you can forecast trends, identify patterns, create rules and recommendations, analyze the sequence of events in complex data sets, and gain new insights.  
  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], data mining is powerful, accessible, and integrated with the tools that many people prefer to use for analysis and reporting. See the links in this section to get the broad background about data mining that you need to get started.  
  
## Key Data Mining Features  
 SQL Server provides the following features in support of integrated data mining solutions:  
  
-   Multiple data sources: You do not have to create a data warehouse or an OLAP cube to do data mining. You can use tabular data from external providers, spreadsheets, and even text files. You can also easily mine OLAP cubes created in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. However, you cannot use data from an in-memory database.  
  
-   Integrated data cleansing, data management, and ETL: Data Quality Services provides advanced tools for profiling and cleansing data. Integration Services can be used to build ETL processes for cleaning data, and also for building, processing, training, and updating models.  
  
-   Multiple customizable algorithms: In addition to providing algorithms such as clustering, neural networks, and decisions trees, the platform supports development of your own custom plug-in algorithms.  
  
-   Model testing infrastructure: Test your models and data sets using important statistical tools as cross-validation, classification matrices, lift charts, and scatter plots. Easily create and manage testing and training sets.  
  
-   Querying and drillthrough: Create prediction queries, retrieve model patterns and statistics, and drill through to case data.  
  
-   Client tools: In addition to the development and design studios provided by SQL Server, you can use the Data Mining Add-ins for Excel to create, query, and browse models. Or, create custom clients, including Web services.  
  
-   Scripting language support and managed API: All data mining objects are fully programmable. Scripting is possible through MDX, XMLA, or the PowerShell extensions for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Use the Data Mining Extensions (DMX) language for fast querying and scripting.  
  
-   Security and deployment: Provides role-based security through [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], including separate permissions for drillthrough to model and structure data. Easy deployment of models to other servers, so that users can access the patterns or perform predictions  
  
## In This Section  
 The topics in this section introduce the principal features of SQL Server Data Mining and related tasks.  
  
-   [Data Mining Concepts](data-mining-concepts.md)  
  
-   [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md)  
  
-   [Mining Structures &#40;Analysis Services - Data Mining&#41;](mining-structures-analysis-services-data-mining.md)  
  
-   [Mining Models &#40;Analysis Services - Data Mining&#41;](mining-models-analysis-services-data-mining.md)  
  
-   [Testing and Validation &#40;Data Mining&#41;](testing-and-validation-data-mining.md)  
  
-   [Data Mining Queries](data-mining-queries.md)  
  
-   [Data Mining Solutions](data-mining-solutions.md)  
  
-   [Data Mining Tools](data-mining-tools.md)  
  
-   [Data Mining Architecture](data-mining-architecture.md)  
  
-   [Security Overview &#40;Data Mining&#41;](security-overview-data-mining.md)  
  
  
