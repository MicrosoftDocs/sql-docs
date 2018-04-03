---
title: "Cube Element (OlapInfo) (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Cube Element (OlapInfo)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.cube"
  - "urn:schemas-microsoft-com:xml-analysis#Cube"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Cube"
helpviewer_keywords: 
  - "Cube element"
ms.assetid: c2b6fe41-6ad4-4181-98a9-3a2517f0b7cc
caps.latest.revision: 10
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Cube Element (OlapInfo) (XMLA)
  Contains information about a cube for the parent [CubeInfo](../../../2014/analysis-services/dev-guide/cubeinfo-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<CubeInfo>  
   <Cube>  
      <CubeName>...</CubeName>  
      <LastDataUpdate>...</LastDataUpdate>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
   </Cube>  
   ...  
</CubeInfo>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CubeInfo](../../../2014/analysis-services/dev-guide/cubeinfo-element-xmla.md)|  
|Child elements|[CubeName](../../../2014/analysis-services/dev-guide/cubename-element-xmla.md), [LastDataUpdate](../../../2014/analysis-services/dev-guide/lastdataupdate-element-xmla.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  