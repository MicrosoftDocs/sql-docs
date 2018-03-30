---
title: "ID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 12
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# ID Element (XMLA)
  Identifies a lock on which to execute the parent [Lock](../../../2014/analysis-services/dev-guide/lock-element-xmla.md) or [Unlock](../../../2014/analysis-services/dev-guide/unlock-element-xmla.md) element.  
  
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
|Parent elements|[Lock](../../../2014/analysis-services/dev-guide/lock-element-xmla.md), [Unlock](../../../2014/analysis-services/dev-guide/unlock-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `ID` element contains a globally unique identifier (GUID) used to identify a lock.  
  
## See Also  
 [Object Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/object-element-xmla.md)   
 [Mode Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/mode-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  