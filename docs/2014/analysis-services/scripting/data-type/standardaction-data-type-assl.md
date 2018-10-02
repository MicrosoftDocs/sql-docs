---
title: "StandardAction Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "StandardAction Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "StandardAction"
helpviewer_keywords: 
  - "StandardAction data type"
ms.assetid: 81b574d5-06c1-4587-8bd2-0e5c5e3b1d99
author: minewiskan
ms.author: owend
manager: craigg
---
# StandardAction Data Type (ASSL)
  Defines a derived data type that represents any [Action](../objects/action-element-assl.md) element other than a [DrillThroughAction](action-data-type-assl.md) element or a [ReportAction](reportaction-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<StandardAction>  
   <!-- The following elements extend Action -->  
   <Expression>...</Expression>  
</StandardAction>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Action](action-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Expression](../properties/expression-element-assl.md)|  
|Derived elements|[Action](../objects/action-element-assl.md) ([Actions](../collections/actions-element-assl.md) collection of [Cube](../objects/cube-element-assl.md) or [Perspective](../objects/perspective-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.StandardAction>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
