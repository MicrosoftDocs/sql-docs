---
title: "DefaultMember Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DefaultMember Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DefaultMember"
helpviewer_keywords: 
  - "DefaultMember element"
ms.assetid: db4eea9f-f7cf-40de-abd0-b62014e7ec2d
author: minewiskan
ms.author: owend
manager: craigg
---
# DefaultMember Element (ASSL)
  Contains a Multidimensional Expressions (MDX) expression that identifies the default member of the parent element.  
  
## Syntax  
  
```xml  
  
<AttributePermission> <!-- or DimensionAttribute, ManyToManyMeasureGroupDimension, PerspectiveAttribute -->  
      ...  
   <DefaultMember>...</DefaultMember>  
      ...  
</AttributePermission>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AttributePermission](../objects/attributepermission-element-assl.md), [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md), [ManyToManyMeasureGroupDimension](../data-type/dimension-data-type-assl.md), [PerspectiveAttribute](../data-type/perspectiveattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `DefaultMember` element defines the default member for the parent element. If `DefaultMember` is not specified or is set to an empty string, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] chooses a member to use as the default member.  
  
 For `ManyToManyMeasureGroupDimension` elements, the `DefaultMember` element contains an MDX expression that specifies a member in the dimension identified in the `CubeDimensionID` element of the `ManyToManyMeasureGroupDimension`. The MDX expression is similar to the [StrToMember](/sql/mdx/strtomember-mdx) MDX function with the CONSTRAINED keyword, in that it cannot include MDX or user-defined functions.  
  
 For more information about default members, see [Define a Default Member](../../multidimensional-models/attribute-properties-define-a-default-member.md).  
  
 The elements that correspond to the parents of `DefaultMember` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AttributePermission>, <xref:Microsoft.AnalysisServices.DimensionAttribute>, and <xref:Microsoft.AnalysisServices.PerspectiveAttribute>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
