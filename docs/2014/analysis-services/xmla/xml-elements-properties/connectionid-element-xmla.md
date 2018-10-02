---
title: "ConnectionID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ConnectionID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#ConnectionID"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ConnectionID"
  - "microsoft.xml.analysis.connectionid"
helpviewer_keywords: 
  - "ConnectionID element"
ms.assetid: de044fb2-f713-46b2-8899-14e8d515e823
author: minewiskan
ms.author: owend
manager: craigg
---
# ConnectionID Element (XMLA)
  Identifies an active connection on which to execute the parent [Cancel](../xml-elements-commands/cancel-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Cancel>  
   ...  
   <ConnectionID>...</ConnectionID>  
   ...  
</Cancel>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cancel](../xml-elements-commands/cancel-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [CancelAssociated Element &#40;XMLA&#41;](cancelassociated-element-xmla.md)   
 [SessionID Element &#40;XMLA&#41;](id-element-xmla.md)   
 [SPID Element &#40;XMLA&#41;](spid-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
