---
title: "HideMemberIf Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "HideMemberIf Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "HideMemberIf"
helpviewer_keywords: 
  - "HideMemberIf element"
ms.assetid: ff0e6b19-6216-43ac-ba76-1628da8c333b
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent element|[Level](../../../analysis-services/scripting/objects/level-element-assl.md)|  
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
 The enumeration that corresponds to the allowed values for **HideMemberIf** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.HideIfValue>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  