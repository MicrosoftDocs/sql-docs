---
title: "DisplayKeyPosition Element (XML) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
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
ms.assetid: 345a24e6-186c-4570-baf2-7bfe9b7b4cc1
caps.latest.revision: 6
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# DisplayKeyPosition Element (XML)
  Contains information about the position of the element in a collection of elements.  
  
## Syntax  
  
```xml  
  
<RelationshipEndVisualizationProperties>  
   ...  
   <DisplayKeyPosition>...</DisplayKeyPosition>  
   ...  
</RelationshipEndVisualizationProperties>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|-1|  
|Cardinality|0-1: Optional element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[RelationshipEndVisualizationProperties](../../../analysis-services/scripting/data-type/relationshipendvisualizationproperties-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For **RelationshipEndVisualizationProperties** elements, the **DisplayKeyPosition** element contains the position of the display key element in a collection of details. The default value indicates there is no display key to be used.  
  
  