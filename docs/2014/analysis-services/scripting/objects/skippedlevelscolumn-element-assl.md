---
title: "SkippedLevelsColumn Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "SkippedLevelsColumn Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "SkippedLevelsColumn"
helpviewer_keywords: 
  - "SkippedLevelsColumn element"
ms.assetid: 6b00a288-99c1-4735-9e6b-cd13ed4fa346
author: minewiskan
ms.author: owend
manager: craigg
---
# SkippedLevelsColumn Element (ASSL)
    
> [!NOTE]  
>  This feature is discontinued in this version of Microsoft SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.  
  
 Provides the details of a column that stores the number of skipped (empty) levels between each member and its parent.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
   <SkippedLevelsColumn xsi:type="DataItem">...</SkippedLevelsColumn>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataItem](../data-type/dataitem-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `SkippedLevelsColumn` element is applicable only to parent attributes (in other words, the value of the [Usage](../properties/usage-element-dimensionattribute-assl.md) element for the `DimensionAttribute` parent is set to *Parent*). The `SkippedLevelsColumn` element contains the column or attribute for the parent attribute that stores the number of skipped levels between each member and its parent member. This allows parent-child hierarchies that are based on the parent attribute to skip levels between members. The values contained in this column or attribute must be nonnegative integers; otherwise, a processing error occurs. If the `SkippedLevelsColumn` element is not specified or contains no value, the current member has a level depth one below its parent member.  
  
 For more information about the `DataItem` type, including a table of Analysis Services Scripting Language (ASSL) objects and properties of the `DataItem` table, see [DataItem Data Type &#40;ASSL&#41;](../data-type/dataitem-data-type-assl.md).  
  
 The element that corresponds to the parent of `SkippedLevelsColumn` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Attributes Element &#40;ASSL&#41;](../collections/attributes-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](dimension-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
