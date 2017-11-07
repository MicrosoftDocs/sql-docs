---
title: "Member Element (CSDLBI) | Microsoft Docs"
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
ms.assetid: 1ba225f5-3867-4aae-a519-e3c277688d1e
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Member Element (CSDLBI)
  The Member element is a complex type that serves as the base for other elements.  
  
 Its attributes can appear in columns, measures, navigation properties, hierarchies, and levels.  
  
## Elements and Attributes  
 The following table lists the elements and attributes that define the Member element.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|Name||The name given to the member (a column, measure, navigation property, hierarchy, or level) that is defined by the implementation of the TMember type|  
|Caption|Yes|The display name for the member.|  
|ContextualNameRule|Yes|The naming format that is used to disambiguate members. The contents of this attribute are defined by the ContextualNameRule simple type.|  
|Hidden||A Boolean value that indicates whether the member will be hidden from the client.<br /><br /> The default is false, meaning that columns are visible in the client.|  
|ReferenceName||The identifier that is used to reference the member in a DAX query. If this attribute is omitted, the field name is used|  
  
## ContextualNameRule Element  
 This simple type defines the naming format that is used to disambiguate members.  
  
|Value|Description|  
|-----------|-----------------|  
|None|Use the attribute name.|  
|Context|Use the incoming relationship name.|  
|Merge|Concatenate the incoming relationship name and the property name.|  
  
## See Also  
 [Understanding the Tabular Object Model at Compatibility Levels 1050 through 1103](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/representation/understanding-tabular-object-model-at-levels-1050-through-1103.md)  
  
  