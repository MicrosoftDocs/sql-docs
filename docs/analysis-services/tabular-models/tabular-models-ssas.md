---
title: "Tabular Models in Analysis Services  | Microsoft Docs"
ms.date: 09/17/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Tabular models
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]

  Tabular models in Analysis Services are databases that run in-memory or in DirectQuery mode, connecting to data directly from back-end relational data sources. By using state-of-the-art compression algorithms and multi-threaded query processor, the Analysis Services Vertipaq analytics engine delivers fast access to tabular model objects and data by reporting client applications like Power BI and Excel.  
  
 While in-memory models are the default, DirectQuery is an alternative query mode for models that are either too large to fit in memory, or when data volatility precludes a reasonable processing strategy. DirectQuery achieves parity with in-memory models through support for a wide array of data sources, ability to handle calculated tables and columns in a DirectQuery model, row level security via DAX expressions that reach the back-end database, and query optimizations that result in faster throughput.
  
 Tabular models are created in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] using the Tabular model project template. The project template provides a design surface for creating semantic model objects like tables, partitions, relationships, hierarchies, measures, and KPIs. 
  
 Tabular models can be deployed to Azure Analysis Services or an instance of SQL Server Analysis Services configured for Tabular server mode. Deployed tabular models can be managed in SQL Server Management Studio. 

Tabular modeling documentation included here applies, in most cases, to both SQL Server Analysis Services and Azure Analysis Services. Articles specific to Azure Analysis Services are published along with other Azure documentation. To learn more, see [Azure Analysis Services documentation](https://docs.microsoft.com/azure/analysis-services/).
  
