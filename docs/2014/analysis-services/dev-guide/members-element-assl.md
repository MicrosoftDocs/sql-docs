---
title: "Members Element (ASSL) | Microsoft Docs"
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
  - "Members Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Members"
helpviewer_keywords: 
  - "Members element"
ms.assetid: 4bf585a3-b681-486d-852b-1244c5658a04
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Members Element (ASSL)
  Contains the collection of [Member](../../../2014/analysis-services/dev-guide/member-element-assl.md) elements of the parent element.  
  
## Syntax  
  
```xml  
  
<Group> <!-- or Role --<  
   ...  
   <Members>  
      <Member>...</Member>  
   </Members>  
   ...  
</Group>  
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
|Parent elements|[Group](../../../2014/analysis-services/dev-guide/group-element-assl.md), [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md)|  
|Child elements|[Member](../../../2014/analysis-services/dev-guide/member-element-assl.md)|  
  
## Remarks  
 The elements that correspond to the parents of `Members` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Group> and <xref:Microsoft.AnalysisServices.Role>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  