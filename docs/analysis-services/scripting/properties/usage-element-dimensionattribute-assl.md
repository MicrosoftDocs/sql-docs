---
title: "Usage Element (DimensionAttribute) (ASSL) | Microsoft Docs"
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
  - "Usage Element (DimensionAttribute)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Usage"
helpviewer_keywords: 
  - "Usage element"
ms.assetid: c5e38d2e-5a8e-4157-84e9-285e78c84e74
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Usage Element (DimensionAttribute) (ASSL)
  Describes how an attribute is used.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
      ...  
   <Usage>...</Usage>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Regular*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Regular*|The attribute is a regular attribute.|  
|*Key*|The attribute is a key attribute.|  
|*Parent*|The attribute is a parent attribute.|  
  
 The enumeration that corresponds to the allowed values for **Usage** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeUsage>.  
  
## See Also  
 [Attributes and Attribute Hierarchies](../../../analysis-services/multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  