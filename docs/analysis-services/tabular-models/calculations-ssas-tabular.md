---
title: "Calculations (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 738816e3-0e1d-44a5-8d1b-81068dce8ac0
caps.latest.revision: 19
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Calculations (SSAS Tabular)
  After you have imported data into your model, you can add calculations to aggregate, filter, extend, combine, and secure that data. Tabular models use Data Analysis Expressions (DAX), a formula language for creating custom calculations. In tabular models, the calculations you create by using DAX formulas are used in *Calculated Columns*, *Measures*, and *Row Filters*.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Understanding DAX in Tabular Models &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/understanding-dax-in-tabular-models-ssas-tabular.md)|Describes the Data Analysis Expressions (DAX) formula language used to create calculations for calculated columns, measures, and row filters in tabular models.|  
|[DAX Formula Compatibility in DirectQuery Mode (SQL Server Analysis Services)](http://msdn.microsoft.com/en-us/981b6a68-434d-4db6-964e-d92f8eb3ee3e)|Describes the differences, lists the functions that are not supported in DirectQuery mode, and lists the functions that are supported but could return different results.|  
|[Data Analysis Expressions (DAX) Reference](http://msdn.microsoft.com/en-us/70a82136-0926-4a91-bcb3-e18e82593b0d)|This section provides detailed descriptions of DAX syntax, operators, and functions.|  
  
> [!NOTE]  
>  Step-by-step tasks for creating calculations are not provided in this section. Because calculations are specified in calculated columns, measures, and row filters (in roles), instructions on where to create DAX formulas are provided in tasks related to those features. For more information, see [Create a Calculated Column &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/ssas-calculated-columns-create-a-calculated-column.md), [Create and Manage Measures &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/create-and-manage-measures-ssas-tabular.md), and [Create and Manage Roles &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/create-and-manage-roles-ssas-tabular.md).  
  
> [!NOTE]  
>  While DAX can also be used to query a tabular model, topics in this section focus specifically on using DAX formulas to create calculations.  
  
  