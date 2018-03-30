---
title: "Object Element (Dimension) (XMLA) | Microsoft Docs"
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
  - "Object Element (Dimension)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Object"
  - "urn:schemas-microsoft-com:xml-analysis#Object"
  - "microsoft.xml.analysis.object"
helpviewer_keywords: 
  - "Object element"
ms.assetid: db7feb39-7cc1-4b54-8979-77ce402ef71f
caps.latest.revision: 10
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Object Element (Dimension) (XMLA)
  Contains an object reference for the dimension on which the parent [Insert](../../../2014/analysis-services/dev-guide/insert-element-xmla.md), [Update](../../../2014/analysis-services/dev-guide/update-element-xmla.md), or [Drop](../../../2014/analysis-services/dev-guide/drop-element-xmla.md) command is executed.  
  
## Syntax  
  
```xml  
  
<Insert> <!-- or any of the parent elements in the Element Relationships table -->  
...  
   <Object>  
      <Database>...</Database>  
      <Cube>...</Cube>  
      <Dimension>...</Dimension>  
   </Object>  
...  
</Insert>  
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
|Parent elements|[Drop](../../../2014/analysis-services/dev-guide/drop-element-xmla.md), [Insert](../../../2014/analysis-services/dev-guide/insert-element-xmla.md), [Update](../../../2014/analysis-services/dev-guide/update-element-xmla.md)|  
|Child elements|[Cube](../../../2014/analysis-services/dev-guide/cube-element-xmla.md), [Database](../../../2014/analysis-services/dev-guide/database-element-xmla.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  