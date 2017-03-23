---
title: "BaseProperty Element (CSDLBI) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: d0f63e52-7330-4b2c-a929-7a517acc6921
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# BaseProperty Element (CSDLBI)
  The BaseProperty element is a complex type that serves as the base for other elements.  
  
 Its attributes can appear in columns and in measures.  
  
## Elements and Attributes  
 The following table lists the elements and attributes that define the BaseProperty element.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|Alignment|No|The name given to the member (a column, measure, navigation property, hierarchy, or level) that is defined by the implementation of the Member type|  
|FormatString|No|The display name for the member.|  
|IsRightToLeft|No|A Boolean value that indicates whether the field contains text that can be read from right to left.<br /><br /> If this attribute is omitted, the default setting (of the model) is used.|  
|SortDirection|No|A value that indicates how the field values are typically sorted. The contents of this attribute are defined by the SortDirection simple type.<br /><br /> If this attribute is omitted, a sort direction is assigned based on the field's data type.|  
|Units|No|The symbol that is applied to field values to express units.<br /><br /> If omitted, the units are unknown.|  
  
## Alignment Element  
 This simple type defines the naming format that is used to disambiguate members.  
  
|Value|Description|  
|-----------|-----------------|  
|None|Use the attribute name.|  
|Context|Use the incoming relationship name.|  
|Merge|Concatenate the incoming relationship name and the property name, according to the rules of the current grammar.|  
  
## SortDirection Element  
 This simple type defines the naming format that is used to disambiguate members.  
  
|Value|Description|  
|-----------|-----------------|  
|None|Use the attribute name.|  
|Context|Use the incoming relationship name.|  
|Merge|Concatenate the incoming relationship name and the property name.|  
  
## See Also  
 [Understanding the Tabular Object Model at Compatibility Levels 1050 through 1103](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/representation/understanding-tabular-object-model-at-levels-1050-through-1103.md)  
  
  