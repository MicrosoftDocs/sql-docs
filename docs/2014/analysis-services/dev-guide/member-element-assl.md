---
title: "Member Element (ASSL) | Microsoft Docs"
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
  - "Member Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Member"
helpviewer_keywords: 
  - "Member element"
ms.assetid: 03b4cfcb-ce87-452f-9e25-8745c0423f56
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Member Element (ASSL)
  Contains the name of a member of a [Group](../../../2014/analysis-services/dev-guide/group-element-assl.md) element or of a [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Members>  
   <Member>  
      <Name>...</Name>  
   </Member>  
</Members>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Members](../../../2014/analysis-services/dev-guide/members-element-assl.md)|  
|Child elements|[Name](../../../2014/analysis-services/dev-guide/name-element-assl.md)|  
  
## Remarks  
 The elements that correspond to the parents of `Member` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Group> and <xref:Microsoft.AnalysisServices.Role>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  