---
title: "IsDefaultImage Element (XML) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: e29cd137-af82-4753-a681-0d3e705513f3
author: minewiskan
ms.author: owend
manager: craigg
---
# IsDefaultImage Element (XML)
  Indicates that it is possible to obtain the default image for this entity by navigating this relationship to the other table and fetching the member that has the attribute, IsDefaultImage.  
  
## Syntax  
  
```xml  
  
<RelationshipEndVisualizationProperties>  
   ...  
   <IsDefaultImage>...</IsDefaultImage>  
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
 For `RelationshipEndVisualizationProperties` elements, the `IsDefaultImage` element indicates that the default image for this entity can be obtained by navigating to the other end of this relationship. The default value of `false` indicates there is no default image to be obtained.  
  
  
