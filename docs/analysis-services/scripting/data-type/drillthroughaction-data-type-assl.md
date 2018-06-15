---
title: "DrillThroughAction Data Type (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DrillThroughAction Data Type (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Defines a derived data type that represents a drillthrough action.  
  
## Syntax  
  
```xml  
  
<DrillThroughAction>  
   <!-- The following elements extend Action -->  
   <Default>...</Default>  
   <Columns>...</Columns>  
</DrillThroughAction>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Action](../../../analysis-services/scripting/data-type/action-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Columns](../../../analysis-services/scripting/collections/columns-element-assl.md), [Default](../../../analysis-services/scripting/properties/default-element-assl.md)|  
|Derived elements|[Action](../../../analysis-services/scripting/objects/action-element-assl.md) ([Actions](../../../analysis-services/scripting/collections/actions-element-assl.md), collection of [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md), or [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DrillThroughAction>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
