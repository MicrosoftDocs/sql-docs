---
title: "DrillThroughAction Data Type (ASSL) | Microsoft Docs"
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
  - "DrillThroughAction Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DrillThroughAction"
helpviewer_keywords: 
  - "DrillThroughAction data type"
ms.assetid: e212d575-a0d7-4548-92b4-33542ef59034
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DrillThroughAction Data Type (ASSL)
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
|Base data types|[Action](../../../2014/analysis-services/dev-guide/action-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md), [Default](../../../2014/analysis-services/dev-guide/default-element-assl.md)|  
|Derived elements|[Action](../../../2014/analysis-services/dev-guide/action-element-assl.md) ([Actions](../../../2014/analysis-services/dev-guide/actions-element-assl.md), collection of [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), or [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DrillThroughAction>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  