---
title: "SessionID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "SessionID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#SessionID"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#SessionID"
  - "microsoft.xml.analysis.sessionid"
helpviewer_keywords: 
  - "SessionID element"
ms.assetid: 18220e00-76cf-48f6-9465-200465a0c553
author: minewiskan
ms.author: owend
manager: craigg
---
# SessionID Element (XMLA)
  Identifies an active session on which to execute the parent [Cancel](../xml-elements-commands/cancel-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Cancel>  
   ...  
   <SessionID>...</SessionID>  
   ...  
</Cancel>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
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
 [ConnectionID Element &#40;XMLA&#41;](id-element-xmla.md)   
 [SPID Element &#40;XMLA&#41;](spid-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
