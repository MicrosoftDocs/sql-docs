---
title: "PerspectiveDimension Data Type (ASSL) | Microsoft Docs"
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
  - "PerspectiveDimension Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "PerspectiveDimension"
helpviewer_keywords: 
  - "PerspectiveDimension data type"
ms.assetid: c4bc56de-4f42-4ceb-a68d-a4fec92fdfa9
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# PerspectiveDimension Data Type (ASSL)
  Defines a primitive data type that represents information about a dimension in a perspective.  
  
## Syntax  
  
```xml  
  
<PerspectiveDimension>  
   <CubeDimensionID>...</CubeDimensionID>  
   <Attributes>...</Attributes>  
   <Hierarchies>...</Hierarchies>  
      <Annotations>...</Annotations>  
</PerspectiveDimension>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [Attributes](../../../analysis-services/scripting/collections/attributes-element-assl.md), [CubeDimensionID](../../../analysis-services/scripting/properties/cubedimensionid-element-assl.md), [Hierarchies](../../../analysis-services/scripting/collections/hierarchies-element-assl.md)|  
|Derived elements|[Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md) ([Dimensions](../../../analysis-services/scripting/collections/dimensions-element-assl.md) collection of [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  