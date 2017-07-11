---
title: "DrillThroughAction Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DrillThroughAction Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DrillThroughAction"
helpviewer_keywords: 
  - "DrillThroughAction data type"
ms.assetid: e212d575-a0d7-4548-92b4-33542ef59034
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
  
  