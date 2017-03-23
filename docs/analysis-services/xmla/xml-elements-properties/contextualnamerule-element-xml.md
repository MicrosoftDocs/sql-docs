---
title: "ContextualNameRule Element (XML) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
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
ms.assetid: eb567ef8-f412-4d34-837a-75e53b88b3ce
caps.latest.revision: 6
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# ContextualNameRule Element (XML)
  Provides a hint on the best way to construct a composite name for the attribute.  
  
## Syntax  
  
```xml  
  
<RelationshipEndVisualizationProperties>  
   ...  
   <ContextualNameRule>...</ContextualNameRule>  
   ...  
</RelationshipEndVisualizationProperties>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|-1|  
|Cardinality|0-1: Optional element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[RelationshipEndVisualizationProperties](../../../analysis-services/scripting/data-type/relationshipendvisualizationproperties-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Provides a hint to client applications about how to create unambiguous names for this attribute.  
  
 The value of the **ContextualNameRule** element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*None*|Use the name of the attribute.|  
|*Context*|Use the name of the incoming relationship.|  
|*Merge*|Following the rules of the application language, concatenate the name of the incoming relationship and the attribute name.|  
  
  