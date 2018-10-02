---
title: "ID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ID"
  - "urn:schemas-microsoft-com:xml-analysis#ID"
  - "microsoft.xml.analysis.id"
helpviewer_keywords: 
  - "ID element"
ms.assetid: f7d67599-6a70-4455-bfdb-1d127e5eff4e
author: minewiskan
ms.author: owend
manager: craigg
---
# ID Element (XMLA)
  Identifies a lock on which to execute the parent [Lock](../xml-elements-commands/lock-element-xmla.md) or [Unlock](../xml-elements-commands/unlock-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Lock> <!-- or Unlock>  
   ...  
   <ID>...</ID>  
   ...  
</Lock>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Lock](../xml-elements-commands/lock-element-xmla.md), [Unlock](../xml-elements-commands/unlock-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `ID` element contains a globally unique identifier (GUID) used to identify a lock.  
  
## See Also  
 [Object Element &#40;XMLA&#41;](object-element-xmla.md)   
 [Mode Element &#40;XMLA&#41;](mode-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
