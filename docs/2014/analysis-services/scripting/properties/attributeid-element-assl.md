---
title: "AttributeID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AttributeID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AttributeID"
helpviewer_keywords: 
  - "AttributeID element"
ms.assetid: 13d2e92b-e4bf-4f2d-b34c-a6f483da3a9e
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributeID Element (ASSL)
  Contains the ID of the attribute associated with the parent element.  
  
## Syntax  
  
```xml  
  
<AggregationAttribute> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <AttributeID>...</AttributeID>  
   ...  
</AggregationAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AggregationAttribute](../data-type/aggregationattribute-data-type-assl.md), [AggregationDesignAttribute](../data-type/aggregationdesignattribute-data-type-assl.md), [AggregationInstanceAttribute](../data-type/aggregationinstanceattribute-data-type-assl.md), [AttributeBinding](../data-type/binding-data-type-assl.md), [AttributePermission](../objects/attributepermission-element-assl.md), [AttributeRelationship](../objects/attributerelationship-element-assl.md), [CubeAttribute](../data-type/cubeattribute-data-type-assl.md), [CubeAttributeBinding](../data-type/cubeattributebinding-data-type-assl.md), [DimensionAttributeBinding](../data-type/dimensionattributebinding-data-type-out-of-line-assl.md), [MeasureGroupAttribute](../data-type/measuregroupattribute-data-type-assl.md), [MeasureGroupAttributeBinding](../data-type/measuregroupattributebinding-data-type-out-of-line-assl.md), [PerspectiveAttribute](../data-type/perspectiveattribute-data-type-assl.md), [UserDefinedGroupBinding](../data-type/userdefinedgroupbinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `AttributeID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationAttribute>, <xref:Microsoft.AnalysisServices.AggregationDesignAttribute>, <xref:Microsoft.AnalysisServices.AggregationInstanceAttribute>, <xref:Microsoft.AnalysisServices.AttributeBinding>, <xref:Microsoft.AnalysisServices.AttributePermission>, <xref:Microsoft.AnalysisServices.AttributeRelationship>, <xref:Microsoft.AnalysisServices.CubeAttribute>, <xref:Microsoft.AnalysisServices.CubeAttributeBinding>, <xref:Microsoft.AnalysisServices.PerspectiveAttribute>, and <xref:Microsoft.AnalysisServices.UserDefinedGroupBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
