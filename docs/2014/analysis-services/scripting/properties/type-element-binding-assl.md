---
title: "Type Element (Binding) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Type Element (Binding)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: b5f5c485-dc83-4d66-a8d2-e96e96d068f9
author: minewiskan
ms.author: owend
manager: craigg
---
# Type Element (Binding) (ASSL)
  Contains the type of the attribute binding.  
  
## Syntax  
  
```xml  
  
<AttributeBinding> <!-- or CubeAttributeBinding -->  
   ...  
   <Type>...</Type>  
   ...  
</AttributeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AttributeBinding](../data-type/binding-data-type-assl.md), [CubeAttributeBinding](../data-type/cubeattributebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*All*|All level|  
|*Key*|Member keys|  
|*Name*|Member name|  
|*Value*|Member value|  
|*Translation*|Member translations|  
|*UnaryOperator*|Unary operators|  
|*SkippedLevels*|Skipped levels|  
|*CustomRollup*|Custom rollup formulas|  
|*CustomRollupProperties*|Custom rollup properties|  
  
 The elements that correspond to the parents of `Type` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AttributeBinding> and <xref:Microsoft.AnalysisServices.CubeAttributeBinding>.  
  
## See Also  
 [Binding Data Type &#40;ASSL&#41;](../data-type/binding-data-type-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
