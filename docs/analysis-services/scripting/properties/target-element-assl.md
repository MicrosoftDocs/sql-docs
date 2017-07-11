---
title: "Target Element (ASSL) | Microsoft Docs"
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
  - "Target Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Target"
helpviewer_keywords: 
  - "Target element"
ms.assetid: 08ce0441-94b6-4f1d-acba-f31c8212cb79
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Target Element (ASSL)
  Identifies the target of the [Action](../../../analysis-services/scripting/objects/action-element-assl.md) element.  
  
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
|Parent element|[Action](../../../analysis-services/scripting/objects/action-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The expected value of this element depends on the value of the [TargetType](../../../analysis-services/scripting/properties/targettype-element-assl.md) element for the parent **Action**. The following table describes the expected values of **Target** based on the value of **TargetType**.  
  
|TargetType value|Expected value|  
|----------------------|--------------------|  
|*Cube*|The name of a cube.|  
|*Cells*|A subcube expression.|  
|*Set*|A set expression or the name of a named set.<br /><br /> Note: A subselect statement cannot be used.|  
|*Hierarchy, HierarchyMembers*|The name of a hierarchy.|  
|*Dimension, DimensionMembers*|The name of a dimension.|  
|*Level, LevelMembers*|The name of a level.|  
|*Attribute, AttributeMembers*|The name of an attribute.|  
  
 The element that corresponds to the parent of **Target** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  