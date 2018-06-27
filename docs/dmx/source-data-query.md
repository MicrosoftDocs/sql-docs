---
title: "&lt;source data query&gt; | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# &lt;source data query&gt;
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  To train a data mining model and create predictions from a mining model, you have to access data that is external to the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database. You use the \<source data query> clause in Data Mining Extensions (DMX) to define this external data. The [INSERT INTO &#40;DMX&#41;](../dmx/insert-into-dmx.md), [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](../dmx/select-from-model-prediction-join-dmx.md), and [SELECT FROM NATURAL PREDICTION JOIN](../dmx/select-from-model-prediction-join-dmx.md) statements all use **\<source data query>**.  
  
## Query types  
 The three most common ways to specify source data are:  
  
 [OPENQUERY &#40;DMX&#41;](../dmx/source-data-query-openquery.md)  
 This statement queries data that is external to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], by using an existing data source.  
  
 While **OPENQUERY** is similar in function to **OPENROWSET**, **OPENQUERY** has the following benefits:  
  
-   A DMX query is much easier to write with **OPENQUERY**. Instead of creating a new connection string every time that you write a query, you can take advantage of the existing connection string in the data source. The data source object can also control data access for individual users.  
  
-   The administrator has more control over how the data on the server is accessed. For example, the administrator can manage which providers are loaded into the server and which external data can be accessed.  
  
 [OPENROWSET &#40;DMX&#41;](../dmx/source-data-query-openrowset.md)  
 This statement queries data that is external to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], by using an existing data source.  
  
 [SHAPE &#40;DMX&#41;](../dmx/source-data-query-shape.md)  
 This statement queries multiple data sources to create a nested table. By using **SHAPE**, you can combine data from multiple sources into a single hierarchical table. This lets you take advantage of the ability of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to nest tables by imbedding a table within a table.  
  
 To specify the source data, you can also use the following options:  
  
-   Any valid DMX statement  
  
-   Any valid Multidimensional Expressions (MDX) statement  
  
-   A table that returns a stored procedure  
  
-   An XML for Analysis (XMLA) rowset  
  
-   A rowset parameter  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Nested Tables &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/nested-tables-analysis-services-data-mining.md)  
  
  
