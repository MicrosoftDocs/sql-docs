---
title: "Calculations (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 738816e3-0e1d-44a5-8d1b-81068dce8ac0
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculations (SSAS Tabular)
  After you have imported data into your model, you can add calculations to aggregate, filter, extend, combine, and secure that data. Tabular models use Data Analysis Expressions (DAX), a formula language for creating custom calculations. In tabular models, the calculations you create by using DAX formulas are used in *Calculated Columns*, *Measures*, and *Row Filters*.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Understanding DAX in Tabular Models &#40;SSAS Tabular&#41;](understanding-dax-in-tabular-models-ssas-tabular.md)|Describes the Data Analysis Expressions (DAX) formula language used to create calculations for calculated columns, measures, and row filters in tabular models.|  
|[Formula Compatibility in DirectQuery Mode](../dax-formula-compatibility-in-directquery-mode-ssas-2014.md)|Describes the differences, lists the functions that are not supported in DirectQuery mode, and lists the functions that are supported but could return different results.|  
|[Data Analysis Expressions &#40;DAX&#41; Reference](https://msdn.microsoft.com/library/gg413422(v=sql.120).aspx)|This section provides detailed descriptions of DAX syntax, operators, and functions.|  
  
> [!NOTE]  
>  Step-by-step tasks for creating calculations are not provided in this section. Because calculations are specified in calculated columns, measures, and row filters (in roles), instructions on where to create DAX formulas are provided in tasks related to those features. For more information, see [Create a Calculated Column &#40;SSAS Tabular&#41;](ssas-calculated-columns-create-a-calculated-column.md), [Create and Manage Measures &#40;SSAS Tabular&#41;](measures-ssas-tabular.md), and [Create and Manage Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md).  
  
> [!NOTE]  
>  While DAX can also be used to query a tabular model, topics in this section focus specifically on using DAX formulas to create calculations.  
  
  
