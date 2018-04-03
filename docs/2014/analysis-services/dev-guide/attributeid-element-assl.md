---
title: "AttributeID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 43
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
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
|Parent element|[AggregationAttribute](../../../2014/analysis-services/dev-guide/aggregationattribute-data-type-assl.md), [AggregationDesignAttribute](../../../2014/analysis-services/dev-guide/aggregationdesignattribute-data-type-assl.md), [AggregationInstanceAttribute](../../../2014/analysis-services/dev-guide/aggregationinstanceattribute-data-type-assl.md), [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md), [AttributePermission](../../../2014/analysis-services/dev-guide/attributepermission-element-assl.md), [AttributeRelationship](../../../2014/analysis-services/dev-guide/attributerelationship-element-assl.md), [CubeAttribute](../../../2014/analysis-services/dev-guide/cubeattribute-data-type-assl.md), [CubeAttributeBinding](../../../2014/analysis-services/dev-guide/cubeattributebinding-data-type-assl.md), [DimensionAttributeBinding](../../../2014/analysis-services/dev-guide/dimensionattributebinding-data-type-out-of-line-assl.md), [MeasureGroupAttribute](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md), [MeasureGroupAttributeBinding](../../../2014/analysis-services/dev-guide/measuregroupattributebinding-data-type-out-of-line-assl.md), [PerspectiveAttribute](../../../2014/analysis-services/dev-guide/perspectiveattribute-data-type-assl.md), [UserDefinedGroupBinding](../../../2014/analysis-services/dev-guide/userdefinedgroupbinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `AttributeID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationAttribute>, <xref:Microsoft.AnalysisServices.AggregationDesignAttribute>, <xref:Microsoft.AnalysisServices.AggregationInstanceAttribute>, <xref:Microsoft.AnalysisServices.AttributeBinding>, <xref:Microsoft.AnalysisServices.AttributePermission>, <xref:Microsoft.AnalysisServices.AttributeRelationship>, <xref:Microsoft.AnalysisServices.CubeAttribute>, <xref:Microsoft.AnalysisServices.CubeAttributeBinding>, <xref:Microsoft.AnalysisServices.PerspectiveAttribute>, and <xref:Microsoft.AnalysisServices.UserDefinedGroupBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  