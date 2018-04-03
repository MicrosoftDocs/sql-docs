---
title: "CancelAssociated Element (XMLA) | Microsoft Docs"
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
caps.latest.revision: 12
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# CancelAssociated Element (XMLA)
  Indicates whether the parent [Cancel](../../../2014/analysis-services/dev-guide/cancel-element-xmla.md) element should cancel all associated commands.  
  
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
|Parent elements|[Cancel](../../../2014/analysis-services/dev-guide/cancel-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 If this element is specified and set to `True`, every corresponding connection, session, and command identified in the parent `Cancel` command is canceled.  
  
## See Also  
 [ConnectionID Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/connectionid-element-xmla.md)   
 [SessionID Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/sessionid-element-xmla.md)   
 [SPID Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/spid-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  