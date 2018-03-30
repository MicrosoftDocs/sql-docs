---
title: "Action Data Type (ASSL) | Microsoft Docs"
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
  - "Action Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Action data type"
ms.assetid: 8c4d2ff7-17e1-4e74-bec7-637e0b191acf
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Action Data Type (ASSL)
  Defines an abstract primitive data type that represents an action in a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element or a [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Action>  
   <Name>...</Name>  
   <ID>...</ID>  
   <Caption>...</Caption>  
      <CaptionIsMdx>...</CaptionIsMdx>  
      <Translations>...</Translations>  
   <TargetType>...</TargetType>  
   <Target>...</Target>  
   <Condition>...</Condition>  
   <Type>...</Type>  
   <Invocation>...</Invocation>  
   <Application>...</Application>  
      <Description>...</Description>  
      <Annotations>...</Annotations>  
</Action>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[DrillThroughAction](../../../2014/analysis-services/dev-guide/drillthroughaction-data-type-assl.md), [ReportAction](../../../2014/analysis-services/dev-guide/reportaction-data-type-assl.md), [StandardAction](../../../2014/analysis-services/dev-guide/standardaction-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Actions](../../../2014/analysis-services/dev-guide/actions-element-assl.md)|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Application](../../../2014/analysis-services/dev-guide/application-element-assl.md), [Caption](../../../2014/analysis-services/dev-guide/caption-element-assl.md), [CaptionIsMdx](../../../2014/analysis-services/dev-guide/captionismdx-element-assl.md), [Condition](../../../2014/analysis-services/dev-guide/condition-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Invocation](../../../2014/analysis-services/dev-guide/invocation-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Target](../../../2014/analysis-services/dev-guide/target-element-assl.md), [TargetType](../../../2014/analysis-services/dev-guide/targettype-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-action-assl.md)|  
|Derived elements|[DrillThroughAction](../../../2014/analysis-services/dev-guide/drillthroughaction-data-type-assl.md), [ReportAction](../../../2014/analysis-services/dev-guide/reportaction-data-type-assl.md), [StandardAction](../../../2014/analysis-services/dev-guide/standardaction-data-type-assl.md)|  
  
## Remarks  
 For more information about actions, see [Actions &#40;Analysis Services - Multidimensional Data&#41;](../../../2014/analysis-services/actions-analysis-services-multidimensional-data.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/cube-element-assl.md)   
 [Perspective Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)   
 [PerspectiveAction Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/perspectiveaction-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  