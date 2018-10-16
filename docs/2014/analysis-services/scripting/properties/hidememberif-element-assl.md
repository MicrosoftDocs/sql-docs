---
title: "HideMemberIf Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "HideMemberIf Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "HideMemberIf"
helpviewer_keywords: 
  - "HideMemberIf element"
ms.assetid: ff0e6b19-6216-43ac-ba76-1628da8c333b
author: minewiskan
ms.author: owend
manager: craigg
---
# HideMemberIf Element (ASSL)
  Indicates whether and when a member in a level should be hidden from client applications.  
  
## Syntax  
  
```xml  
  
<Level>  
   ...  
   <HideMemberIf>...</HideMemberIf>  
   ...  
</Level>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Never*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Level](../objects/level-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Never*|Members are never hidden.|  
|*OnlyChildWithNoName*|A member is hidden when it is the only child of its parent and its name is empty.|  
|*OnlyChildWithParentName*|A member is hidden when it is the only child of its parent and its name is identical to that of its parent.|  
|*NoName*|A member is hidden when its name is empty.|  
|*ParentName*|A member is hidden when its name is identical to that of its parent.|  
  
## Remarks  
 The enumeration that corresponds to the allowed values for `HideMemberIf` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.HideIfValue>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
