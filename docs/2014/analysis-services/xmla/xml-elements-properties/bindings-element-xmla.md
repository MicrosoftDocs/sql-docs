---
title: "Bindings Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Bindings Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.bindings"
  - "urn:schemas-microsoft-com:xml-analysis#Bindings"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Bindings"
helpviewer_keywords: 
  - "Bindings element"
ms.assetid: caa34cab-f61f-4f39-b800-af1601714daa
author: minewiskan
ms.author: owend
manager: craigg
---
# Bindings Element (XMLA)
  Contains a collection of [Binding](binding-element-xmla.md) elements for the parent [Batch](../xml-elements-commands/batch-element-xmla.md) or [Process](../xml-elements-commands/process-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Batch> <!-- or Process>  
...  
   <Bindings>  
      <Binding>...</Binding>  
   </Bindings>  
...  
</Alter>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Batch](../xml-elements-commands/batch-element-xmla.md), [Process](../xml-elements-commands/process-element-xmla.md)|  
|Child elements|[Binding](binding-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
