---
title: "Action Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# Action Data Type (ASSL)
  Defines an abstract primitive data type that represents an action in a [Cube](../objects/cube-element-assl.md) element or a [Perspective](../objects/perspective-element-assl.md) element.  
  
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
|Derived data types|[DrillThroughAction](action-data-type-assl.md), [ReportAction](reportaction-data-type-assl.md), [StandardAction](standardaction-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Actions](../collections/actions-element-assl.md)|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [Application](../properties/application-element-assl.md), [Caption](../properties/caption-element-assl.md), [CaptionIsMdx](../properties/captionismdx-element-assl.md), [Condition](../properties/condition-element-assl.md), [Description](../properties/description-element-assl.md), [ID](../properties/id-element-assl.md), [Invocation](../properties/invocation-element-assl.md), [Name](../properties/name-element-assl.md), [Target](../properties/target-element-assl.md), [TargetType](../properties/targettype-element-assl.md), [Translations](../collections/translations-element-assl.md), [Type](../properties/type-element-action-assl.md)|  
|Derived elements|[DrillThroughAction](action-data-type-assl.md), [ReportAction](reportaction-data-type-assl.md), [StandardAction](standardaction-data-type-assl.md)|  
  
## Remarks  
 For more information about actions, see [Actions &#40;Analysis Services - Multidimensional Data&#41;](../../multidimensional-models/actions-analysis-services-multidimensional-data.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md)   
 [Perspective Element &#40;ASSL&#41;](../objects/perspective-element-assl.md)   
 [PerspectiveAction Data Type &#40;ASSL&#41;](perspectiveaction-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
