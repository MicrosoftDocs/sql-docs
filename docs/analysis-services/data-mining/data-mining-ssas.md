---
title: "Data Mining (SSAS) | Microsoft Docs"
ms.date: 01/09/2019
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining (SSAS)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]

> [!IMPORTANT]
> Data mining is deprecated in SQL Server Analysis Services 2017. Documentation is not updated for deprecated features. To learn more, see [Analysis Services backward compatibility (SQL 2017)](../analysis-services-backward-compatibility-sql2017.md).

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has been a leader in predictive analytics since the 2000 release, by providing data mining in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The combination of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining provides an integrated platform for predictive analytics that encompasses data cleansing and preparation, machine learning, and reporting. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining includes multiple standard algorithms, including EM and K-means clustering models, neural networks, logistic regression and linear regression, decision trees, and naive bayes classifiers. All models have integrated visualizations to help you develop, refine, and evaluate your models.  Integrating data mining into business intelligence solution helps you make intelligent decisions about complex problems.  
  
## Benefits of Data Mining  
 Data mining (also called predictive analytics and machine learning) uses well-researched statistical principles to discover patterns in your data. By applying the data mining algorithms in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to your data, you can forecast trends, identify patterns, create rules and recommendations, analyze the sequence of events in complex data sets, and gain new insights.  
  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], data mining is powerful, accessible, and integrated with the tools that many people prefer to use for analysis and reporting.  
  
## Key Data Mining Features  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining provides the following features in support of integrated data mining solutions:  
  
-   Multiple data sources: You can use any tabular data source for data mining, including spreadsheets and text files. You can also easily mine OLAP cubes created in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. However, you cannot use data from an in-memory database.  
  
-   Integrated data cleansing, data management, and reporting: [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides tools for profiling and cleansing data. You can build ETL processes for cleaning data in preparation for modeling, and ssISnoversion also makes it easy to retrain and update models.  
  
-   Multiple customizable algorithms: In addition to providing algorithms such as clustering, neural networks, and decisions trees, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining supports development of your own custom plug-in algorithms.  
  
-   Model testing infrastructure: Test your models and data sets using important statistical tools as cross-validation, classification matrices, lift charts, and scatter plots. Easily create and manage testing and training sets.  
  
-   Querying and drillthrough: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining provides the DMX language for integrating  prediction queries into applications. You can also retrieve detailed statistics and patterns from the models, and drill through to case data.  
  
-   Client tools: In addition to the development and design studios provided by SQL Server, you can use the Data Mining Add-ins for Excel to create, query, and browse models. Or, create custom clients, including Web services.  
  
-   Scripting language support and managed API: All data mining objects are fully programmable. Scripting is possible through MDX, XMLA, or the PowerShell extensions for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Use the Data Mining Extensions (DMX) language for fast querying and scripting.  
  
-   Security and deployment: Provides role-based security through [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], including separate permissions for drillthrough to model and structure data. Easy deployment of models to other servers, so that users can access the patterns or perform predictions  
  
## In This Section  
 The topics in this section introduce the principal features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining and related tasks.  
  
-   [Data Mining Concepts](../../analysis-services/data-mining/data-mining-concepts.md)  
  
-   [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)  
  
-   [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)  
  
-   [Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-models-analysis-services-data-mining.md)  
  
-   [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md)  
  
-   [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)  
  
-   [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md)  
  
-   [Data Mining Tools](../../analysis-services/data-mining/data-mining-tools.md)  
  
-   [Data Mining Architecture](../../analysis-services/data-mining/data-mining-architecture.md)  
  
-   [Security Overview &#40;Data Mining&#41;](../../analysis-services/data-mining/security-overview-data-mining.md)  
  
## See Also  
 [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md)  
  
  
