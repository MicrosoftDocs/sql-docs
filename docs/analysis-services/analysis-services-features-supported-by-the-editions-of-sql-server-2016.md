---
title: "Analysis Services Features Supported by the Editions of SQL Server | Microsoft Docs"
ms.date: 07/09/2019
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Analysis Services features supported by SQL Server edition
[!INCLUDE[ssas-appliesto-sql2016-later](../includes/ssas-appliesto-sql2016-later.md)]

This article describes features supported by different editions of SQL Server 2016, 2017, 2019 Analysis Services. Evaluation edition supports Enterprise edition features.

## Analysis Services (servers)
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Scalable shared databases|Yes||||||Yes|  
|Backup/Restore & Attach/Detach databases|Yes|Yes|||||Yes|  
|Synchronize databases|Yes||||||Yes|  
|Always On failover cluster instances|Yes<br /><br /> Number of nodes is the operating system maximum|Yes<br /><br /> Support for 2 nodes|||||Yes<br /><br /> Number of nodes is the operating system maximum|  
|Programmability (AMO, ADOMD.Net, OLEDB, XML/A, ASSL, TMSL)|Yes|Yes|||||Yes|  
  
## Tabular models 
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Hierarchies|Yes|Yes|||||Yes|  
|KPIs|Yes|Yes|||||Yes|  
|Perspectives|Yes||||||Yes|  
|Translations|Yes|Yes|||||Yes|  
|DAX calculations, DAX queries, MDX queries|Yes|Yes|||||Yes|  
|Row-level security|Yes|Yes|||||Yes|  
|Multiple partitions|Yes||||||Yes|  
|Calculation groups|Yes (beginning with SQL Server 2019)|Yes (beginning with SQL Server 2019)|||||Yes (beginning with SQL Server 2019)|  
|In-memory storage mode|Yes|Yes|||||Yes|  
|DirectQuery mode|Yes|Yes (beginning with SQL Server 2019)|||||Yes|  

## Multidimensional models 
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Semi-additive measures|Yes|No <sup>1</sup>|||||Yes|  
|Hierarchies|Yes|Yes|||||Yes|  
|KPIs|Yes|Yes|||||Yes|  
|Perspectives|Yes||||||Yes|  
|Actions|Yes|Yes|||||Yes|  
|Account intelligence|Yes|Yes|||||Yes|  
|Time intelligence|Yes|Yes|||||Yes|  
|Custom rollups|Yes|Yes|||||Yes|  
|Writeback cube|Yes|Yes|||||Yes|  
|Writeback cells|Yes|Yes|||||Yes|  
|Drillthrough|Yes|Yes|||||Yes|  
|Advanced hierarchy types (parent-child and ragged hierarchies)|Yes|Yes|||||Yes|  
|Advanced dimensions (reference dimensions, many-to-many dimensions)|Yes|Yes|||||Yes|  
|Linked measures and dimensions|Yes|Yes  <sup>2</sup> |||||Yes|  
|Translations|Yes|Yes|||||Yes|  
|Aggregations|Yes|Yes|||||Yes|  
|Multiple partitions|Yes|Yes, up to 3|||||Yes|  
|Proactive caching|Yes||||||Yes|  
|Custom assemblies (stored procedures)|Yes|Yes|||||Yes|  
|MDX queries and scripts|Yes|Yes|||||Yes|  
|DAX queries|Yes|Yes|||||Yes|  
|Role-based security model|Yes|Yes|||||Yes|  
|Dimension and cell-level security|Yes|Yes|||||Yes|  
|Scalable string storage|Yes|Yes|||||Yes|  
|MOLAP, ROLAP, and HOLAP storage models|Yes|Yes|||||Yes|  
|Binary and compressed XML transport|Yes|Yes|||||Yes|  
|Push-mode processing|Yes||||||Yes|  
|Measure expressions|Yes||||||Yes|  
  
 <sup>1</sup> The LastChild semi-additive measure is supported in Standard edition, but other semi-additive measures, such as None, FirstChild, FirstNonEmpty, LastNonEmpty, AverageOfChildren, and ByAccount, are not. Additive measures, such as Sum, Count, Min, Max, and non-additive measures (DistinctCount) are supported on all editions.  
  <sup>2</sup> Standard edition supports linking measures and dimensions within the same database, but not from other databases or instances.
  
## Power Pivot for SharePoint  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|SharePoint farm integration based on shared service architecture|Yes||||||Yes|  
|Usage reporting|Yes||||||Yes|  
|Health monitoring rules|Yes||||||Yes|  
|Power Pivot gallery|Yes||||||Yes|  
|Power Pivot data refresh|Yes||||||Yes|  
|Power Pivot data feeds|Yes||||||Yes|  
  
## Data Mining  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|------------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Standard algorithms|Yes|Yes|||||Yes|  
|Data mining tools (Wizards, Editors,  Query Builders)|Yes|Yes|||||Yes|  
|Cross validation|Yes||||||Yes|  
|Models on filtered subsets of mining structure data|Yes||||||Yes|  
|Time series: Custom blending between ARTXP and ARIMA methods|Yes||||||Yes|  
|Time series: Prediction with new data|Yes||||||Yes|  
|Unlimited concurrent DM queries|Yes||||||Yes|  
|Advanced configuration & tuning options for data mining algorithms|Yes||||||Yes|  
|Support for plug-in algorithms|Yes||||||Yes|  
|Parallel model processing|Yes||||||Yes|  
|Time series: cross-series prediction|Yes||||||Yes|  
|Unlimited attributes for association rules|Yes||||||Yes|  
|Sequence prediction|Yes||||||Yes|  
|Multiple prediction targets for naïve Bayes, neural network and logistic regression|Yes||||||Yes|  
  


