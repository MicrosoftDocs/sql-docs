---
title: "Target Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Target Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Target"
helpviewer_keywords: 
  - "Target element"
ms.assetid: 08ce0441-94b6-4f1d-acba-f31c8212cb79
author: minewiskan
ms.author: owend
manager: craigg
---
# Target Element (ASSL)
  Identifies the target of the [Action](../objects/action-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Action>  
   ...  
   <Target>...</Target>  
   ...  
</Action>  
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
|Parent element|[Action](../objects/action-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The expected value of this element depends on the value of the [TargetType](targettype-element-assl.md) element for the parent `Action`. The following table describes the expected values of `Target` based on the value of `TargetType`.  
  
|TargetType value|Expected value|  
|----------------------|--------------------|  
|*Cube*|The name of a cube.|  
|*Cells*|A subcube expression.|  
|*Set*|A set expression or the name of a named set. **Note:**  A subselect statement cannot be used.|  
|*Hierarchy, HierarchyMembers*|The name of a hierarchy.|  
|*Dimension, DimensionMembers*|The name of a dimension.|  
|*Level, LevelMembers*|The name of a level.|  
|*Attribute, AttributeMembers*|The name of an attribute.|  
  
 The element that corresponds to the parent of `Target` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
