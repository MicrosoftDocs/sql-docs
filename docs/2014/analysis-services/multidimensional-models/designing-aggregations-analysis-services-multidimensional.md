---
title: "Designing Aggregations (Analysis Services - Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "aggregations [Analysis Services], partitions"
  - "partitions [Analysis Services], aggregations"
ms.assetid: 3072b7e0-6961-42ad-a287-16f391f2cec4
author: minewiskan
ms.author: owend
manager: craigg
---
# Designing Aggregations (Analysis Services - Multidimensional)
  Aggregations are precalculated summaries of cube data that help enable [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to provide rapid query responses.  
  
 To set storage options and design aggregations for a partition, use the Aggregation Design Wizard. The wizard operates on a single partition of a measure group at a time so that you can select different options and designs for each partition. The wizard takes you through steps to configure storage and design aggregation for a partition. For more information about configuring storage, see.  
  
 Select a method for controlling the number of aggregations the wizard will design, and then let the wizard design the aggregations.  
  
 The goal is to design the optimal number of aggregations. This number should not only provide satisfactory response time, but also prevent excessive partition size. A greater number of aggregations produces faster response times but it also requires more storage space and may take longer to compute. Moreover, as the wizard designs more and more aggregations, earlier aggregations produce considerably larger performance gains than later aggregations. Reduction in less useful aggregations increases performance as well. You can control the number of aggregations the wizard designs by one of the following methods available in the wizard:  
  
-   Specify a storage space limit for the aggregations.  
  
-   Specify a performance gain limit.  
  
-   Stop the wizard manually when the displayed performance versus size curve starts to level off at an acceptable performance gain.  
  
-   Choose not to design aggregations.  
  
 To design storage, the wizard must be able to connect to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on the target server. The wizard will display an error message if [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is not running on the target server or if the storage design process is otherwise unable to connect to the target server.  
  
 The final step of the wizard allows you to process or defer processing. Processing creates the aggregations you design with the wizard, while deferring processing saves the designed aggregations for future processing, thus allowing design activities to continue without processing. Depending on the size of the partition, processing may take considerable time. If you choose, you can interrupt processing a partition.  
  
## See Also  
 [Aggregations and Aggregation Designs](../multidimensional-models-olap-logical-cube-objects/aggregations-and-aggregation-designs.md)  
  
  
