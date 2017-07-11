---
title: "CancelAssociated Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "CancelAssociated Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.cancelassociated"
  - "urn:schemas-microsoft-com:xml-analysis#CancelAssociated"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CancelAssociated"
helpviewer_keywords: 
  - "CancelAssociated element"
ms.assetid: fd890440-d1a7-4c05-9e81-c81e6b8c274c
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# CancelAssociated Element (XMLA)
  Indicates whether the parent [Cancel](../../../analysis-services/xmla/xml-elements-commands/cancel-element-xmla.md) element should cancel all associated commands.  
  
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
|Parent elements|[Cancel](../../../analysis-services/xmla/xml-elements-commands/cancel-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 If this element is specified and set to **True**, every corresponding connection, session, and command identified in the parent **Cancel** command is canceled.  
  
## See Also  
 [ConnectionID Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/connectionid-element-xmla.md)   
 [SessionID Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/sessionid-element-xmla.md)   
 [SPID Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/spid-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  