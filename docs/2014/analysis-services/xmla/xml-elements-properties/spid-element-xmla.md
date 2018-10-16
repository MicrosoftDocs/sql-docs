---
title: "SPID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "SPID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#SPID"
  - "microsoft.xml.analysis.spid"
  - "urn:schemas-microsoft-com:xml-analysis#SPID"
helpviewer_keywords: 
  - "SPID element"
ms.assetid: c4a54dcb-a0cd-4255-9e0f-a34eb990854f
author: minewiskan
ms.author: owend
manager: craigg
---
# SPID Element (XMLA)
  Identifies an active server process identifier (SPID) on which to execute the parent [Cancel](../xml-elements-commands/cancel-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Cancel>  
   ...  
   <SPID>...</SPID>  
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
 The `SPID` element represents the server process ID (SPID) used for a given session on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## See Also  
 [CancelAssociated Element &#40;XMLA&#41;](cancelassociated-element-xmla.md)   
 [ConnectionID Element &#40;XMLA&#41;](id-element-xmla.md)   
 [SessionID Element &#40;XMLA&#41;](sessionid-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
