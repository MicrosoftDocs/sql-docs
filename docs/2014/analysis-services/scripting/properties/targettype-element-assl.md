---
title: "TargetType Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "TargetType Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TargetType"
helpviewer_keywords: 
  - "TargetType element"
ms.assetid: 2c69ea6e-2af7-435b-9841-86117d5554a7
author: minewiskan
ms.author: owend
manager: craigg
---
# TargetType Element (ASSL)
  Identifies the item type of the item identified in the [Target](target-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Action>  
   ...  
   <TargetType>...</TargetType>  
   ...  
</Action>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Action](../objects/action-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Cube*|The target of the action is a cube.|  
|*Cells*|The target of the action is a subcube.|  
|*Set*|The target of the action is a set.|  
|*Hierarchy*|The target of the action is a hierarchy.|  
|*Level*|The target of the action is a level.|  
|*DimensionMembers*|The target of the action is the members of a dimension.|  
|*HierarchyMembers*|The target of the action is the members of a hierarchy.|  
|*LevelMembers*|The target of the action is the members of a level.|  
|*AttributeMembers*|The target of the action is the members of an attribute.|  
  
 The enumeration that corresponds to the allowed values for `TargetType` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ActionTargetType>.  
  
 The element that corresponds to the parent of `TargetType` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
