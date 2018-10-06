---
title: "IsDefaultMeasure Element (XML) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 523cf3d7-9df0-4f9d-8486-9109de8d3cca
author: minewiskan
ms.author: owend
manager: craigg
---
# IsDefaultMeasure Element (XML)
  Indicates that it is possible to obtain the default measure for this entity by navigating this relationship to the other table and fetching the member that has the attribute, DefaultMeasure.  
  
## Syntax  
  
```xml  
  
<RelationshipEndVisualizationProperties>  
   ...  
   <IsDefaultMeasure>...</IsDefaultMeasure>  
   ...  
</RelationshipEndVisualizationProperties>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|false|  
|Cardinality|0-1: Optional element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[RelationshipEndVisualizationProperties](../../scripting/data-type/relationshipendvisualizationproperties-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For `RelationshipEndVisualizationProperties` elements, the `IsDefaultMeasure` element indicates that it is possible to obtain the default measure for this entity by navigating to the other end of this relationship. The default value of `false` indicates there is no default measure to be obtained.  
  
  
