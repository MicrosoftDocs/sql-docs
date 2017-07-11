---
title: "AggregationDesignAttribute Data Type (ASSL) | Microsoft Docs"
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
  - "AggregationDesignAttribute Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "AggregationDesignAttribute"
helpviewer_keywords: 
  - "AggregationDesignAttribute data type"
ms.assetid: 03d29d76-e4bd-4035-92cc-35149d83fbf9
caps.latest.revision: 42
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AggregationDesignAttribute Data Type (ASSL)
  Defines a primitive data type that represents the association between an attribute and an [AggregationDesignDimension](../../../analysis-services/scripting/data-type/aggregationdesigndimension-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AggregationDesignAttribute>  
   <AttributeID>...</AttributeID>  
      <EstimatedCount>...</EstimatedCount>  
</AggregationDesignAttribute>  
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
|Child elements|[AttributeID](../../../analysis-services/scripting/properties/attributeid-element-assl.md), [EstimatedCount](../../../analysis-services/scripting/properties/estimatedcount-element-assl.md)|  
|Derived elements|[Attribute](../../../analysis-services/scripting/objects/attribute-element-assl.md) ([Attributes](../../../analysis-services/scripting/collections/attributes-element-assl.md) collection of [AggregationDesignDimension](../../../analysis-services/scripting/data-type/aggregationdesigndimension-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationDesignAttribute>.  
  
## See Also  
 [AggregationDesignDimension Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/aggregationdesigndimension-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  