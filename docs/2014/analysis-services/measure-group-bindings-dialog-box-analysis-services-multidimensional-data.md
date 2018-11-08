---
title: "Measure Group Bindings Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.dimensionusage.definerelationship.measuregroupbindings.f1"
helpviewer_keywords: 
  - "Measure Group Bindings dialog box"
ms.assetid: ed642780-5350-438e-af73-b9ceab3f876d
author: minewiskan
ms.author: owend
manager: craigg
---
# Measure Group Bindings Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Measure Group Bindings** dialog box to create and modify direct relationships between non-granularity attributes in a cube dimension and columns in a measure group for a regular dimension relationship, as well as to specify null processing options for any attribute in a cube dimension, from the **Define Relationship** dialog box.  
  
## Options  
 **Measure group table**  
 Displays the name of the fact table for the selected measure group.  
  
 **Attributes**  
 Displays a grid of attributes and dimension tables. Select an attribute to create or modify properties in **Relationship** for the selected attribute. The grid contains the following columns:  
  
|Option|Definition|  
|------------|----------------|  
|**Attribute Name**|Displays the name of the attribute.|  
|**Dimension Table**|Displays the name of the dimension table on which the attribute is based.|  
  
 **Relationship**  
 Displays a grid of relationships between dimension table columns for the selected attribute and fact table columns for the selected measure group as well as the null processing option for the relationship. The grid contains the following columns:  
  
|Option|Definition|  
|------------|----------------|  
|**Dimension Columns**|Displays the columns from the dimension table on which the attribute selected in **Attributes** is based.|  
|**Measure Group Columns**|Select either **Inherited from dimension** to use the measure group relationship inherited from the dimension, or select a column from the fact table on which the measure group is based to explicitly define a relationship.|  
|**Null Processing**|Select a null processing option for the attribute. For more information about null processing options, see [NullProcessing Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/nullprocessing-element-assl).|  
  
## See Also  
 [Define Relationship Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](define-relationship-dialog-box-analysis-services-multidimensional-data.md)   
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)  
  
  
