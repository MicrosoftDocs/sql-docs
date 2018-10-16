---
title: "Member Element (CSDLBI) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 1ba225f5-3867-4aae-a519-e3c277688d1e
author: minewiskan
ms.author: owend
manager: craigg
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
 [Understanding the Tabular Object Model](../representation/understanding-tabular-object-model-at-levels-1050-through-1103.md)  
  
  
