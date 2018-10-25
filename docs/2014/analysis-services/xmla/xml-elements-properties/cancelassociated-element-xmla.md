---
title: "CancelAssociated Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CancelAssociated Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.cancelassociated"
  - "urn:schemas-microsoft-com:xml-analysis#CancelAssociated"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CancelAssociated"
helpviewer_keywords: 
  - "CancelAssociated element"
ms.assetid: fd890440-d1a7-4c05-9e81-c81e6b8c274c
author: minewiskan
ms.author: owend
manager: craigg
---
# CancelAssociated Element (XMLA)
  Indicates whether the parent [Cancel](../xml-elements-commands/cancel-element-xmla.md) element should cancel all associated commands.  
  
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
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cancel](../xml-elements-commands/cancel-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 If this element is specified and set to `True`, every corresponding connection, session, and command identified in the parent `Cancel` command is canceled.  
  
## See Also  
 [ConnectionID Element &#40;XMLA&#41;](id-element-xmla.md)   
 [SessionID Element &#40;XMLA&#41;](sessionid-element-xmla.md)   
 [SPID Element &#40;XMLA&#41;](spid-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
