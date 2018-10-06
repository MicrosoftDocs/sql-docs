---
title: "Tabular Modeling (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 80027288-c203-4667-a3e1-40fa572b4975
author: minewiskan
ms.author: owend
manager: craigg
---
# Tabular Modeling (SSAS Tabular)
  Tabular models are in-memory databases in Analysis Services. Using state-of-the-art compression algorithms and multi-threaded query processor, the xVelocity in-memory analytics engine (VertiPaq) delivers fast access to tabular model objects and data by reporting client applications such as Microsoft Excel and Microsoft [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)].  
  
 Tabular models support data access through two modes: Cached mode and DirectQuery mode. In cached mode, you can integrate data from multiple sources including relational databases, data feeds, and flat text files. In DirectQuery mode, you can bypass the in-memory model, allowing client applications to query data directly at the (SQL Server relational) source.  
  
 Tabular models are authored in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] using new tabular model project templates. You can import data from multiple sources, and then enrich the model by adding relationships, calculated columns, measures, KPIs, and hierarchies. Models can then be deployed to an instance of Analysis Services where client reporting applications can connect to them. Deployed models can be managed in SQL Server Management Studio just like multidimensional models. They can also be partitioned for optimized processing and secured to the row-level by using role based security.  
  
## In This Section  
 [Tabular Model Solutions &#40;SSAS Tabular&#41;](../tabular-model-solutions-ssas-tabular.md)  
  
 [Tabular Model Databases &#40;SSAS Tabular&#41;](tabular-model-databases-ssas-tabular.md)  
  
 [Tabular Model Data Access](tabular-model-data-access.md)  
  
  
