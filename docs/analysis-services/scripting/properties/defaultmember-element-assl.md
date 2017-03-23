---
title: "DefaultMember Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DefaultMember Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DefaultMember"
helpviewer_keywords: 
  - "DefaultMember element"
ms.assetid: db4eea9f-f7cf-40de-abd0-b62014e7ec2d
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent element|[AttributePermission](../../../analysis-services/scripting/objects/attributepermission-element-assl.md), [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md), [ManyToManyMeasureGroupDimension](../../../analysis-services/scripting/data-type/manytomanymeasuregroupdimension-data-type-assl.md), [PerspectiveAttribute](../../../analysis-services/scripting/data-type/perspectiveattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **DefaultMember** element defines the default member for the parent element. If **DefaultMember** is not specified or is set to an empty string, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] chooses a member to use as the default member.  
  
 For **ManyToManyMeasureGroupDimension** elements, the **DefaultMember** element contains an MDX expression that specifies a member in the dimension identified in the **CubeDimensionID** element of the **ManyToManyMeasureGroupDimension**. The MDX expression is similar to the [StrToMember](../../../mdx/strtomember-mdx.md) MDX function with the CONSTRAINED keyword, in that it cannot include MDX or user-defined functions.  
  
 For more information about default members, see [Define a Default Member](../../../analysis-services/multidimensional-models/attribute-properties-define-a-default-member.md).  
  
 The elements that correspond to the parents of **DefaultMember** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AttributePermission>, <xref:Microsoft.AnalysisServices.DimensionAttribute>, and <xref:Microsoft.AnalysisServices.PerspectiveAttribute>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  