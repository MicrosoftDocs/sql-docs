---
title: "Object Element (Dimension) (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# Object Element (Dimension) (XMLA)
  Contains an object reference for the dimension on which the parent [Insert](../xml-elements-commands/insert-element-xmla.md), [Update](../xml-elements-commands/update-element-xmla.md), or [Drop](../xml-elements-commands/drop-element-xmla.md) command is executed.  
  
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
|Parent elements|[Drop](../xml-elements-commands/drop-element-xmla.md), [Insert](../xml-elements-commands/insert-element-xmla.md), [Update](../xml-elements-commands/update-element-xmla.md)|  
|Child elements|[Cube](cube-element-xmla.md), [Database](database-element-xmla.md), [Dimension](dimension-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
