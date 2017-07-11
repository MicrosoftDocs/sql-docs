---
title: "Change a partition source to use a different fact table | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "fact tables [Analysis Services]"
  - "partitions [Analysis Services], fact tables"
ms.assetid: 5508312f-8e46-4802-9362-6688ca03d098
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Change a partition source to use a different fact table
  When you create a partition for a cube, you can choose to use a different fact table. Different tables may be from a single data source view, from different data source views, or from different data sources. A data source view may also contain different tables from more than one data source.  
  
 All fact tables and dimensions for a cube's partitions must have the same structure as the fact table and dimensions of the cube. For example, different fact tables can have the same structure but contain data for different years or different product lines.  
  
 You specify a fact table on the **Specify Source Information** page of the Partition Wizard. On this page, in the Partition Source group next to **Look in**, specify a data source or data source view in which to look. Next, click **Find Tables**, and then, under **Available Tables**, select the table you want to use.  
  
 When you use different fact tables, ensure that no data is duplicated among the partitions. For example, if one fact table contains transactions for 2012 only, and another fact table contains transactions for 2013 only, these tables contain independent data. Similarly, fact tables for distinct product lines or distinct geographical areas are independent.  
  
 It is possible, but not recommended, to use different fact tables that contain duplicated data. In this case, you must use filters in the partitions to ensure that data used by one partition is not used by any other partition. For more information, see [Create and Manage a Local Partition &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/create-and-manage-a-local-partition-analysis-services.md).  
  
## See Also  
 [Create and Manage a Local Partition &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/create-and-manage-a-local-partition-analysis-services.md)  
  
  