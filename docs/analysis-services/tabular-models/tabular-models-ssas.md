---
title: "Tabular Models | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2018"
ms.prod: analysis-services
ms.prod_service: "analysis-services, azure-analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  
ms.component: multidimensional-tabular
ms.component: data-mining
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 80027288-c203-4667-a3e1-40fa572b4975
caps.latest.revision: 17
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "Active"
---
# Tabular models
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Tabular models are Analysis Services databases that run in-memory or in DirectQuery mode, accessing data directly from backend relational data sources. By using state-of-the-art compression algorithms and multi-threaded query processor, the analytics engine delivers fast access to tabular model objects and data by reporting client applications such as Power BI and Excel.  
  
 While in-memory models are the defualt, DirectQuery is an alternative query mode for models that are either too big to fit in memory, or when data volatility precludes a reasonable processing strategy. DirectQuery achieves parity with in-memory models through support for a wide array of data sources, ability to handle calculated tables and columns in a DirectQuery model, row level security via DAX expressions that reach the backend database, and query optimizations that result in faster throughput.
  
 Tabular models are authored in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] using the Tabular model project template that provides a design surface for creating a model, tables, relationships, and DAX expressions. You can import data from multiple sources, and then enrich the model by adding relationships, calculated tables and columns, measures, KPIs, hierarchies, and translations.  
  
 Models can then be deployed to Azure Analysis Services or an instance of SQL Server Analysis Services configured for Tabular server mode. Deployed tabular models can be managed in SQL Server Management Studio. As your models grow, they can be partitioned for optimized processing and secured to the row-level by using role based security.  
  
## See also  
 [What's new in Analysis Services](../../analysis-services/what-s-new-in-analysis-services.md)   
 [Comparing Tabular and Multidimensional solutions](../../analysis-services/comparing-tabular-and-multidimensional-solutions-ssas.md)   
 [Tools and applications used in Analysis Services](../../analysis-services/tools-and-applications-used-in-analysis-services.md)   
 [DirectQuery mode](../../analysis-services/tabular-models/directquery-mode-ssas-tabular.md)   
 [Compatibility level](../../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md)  
  
  
