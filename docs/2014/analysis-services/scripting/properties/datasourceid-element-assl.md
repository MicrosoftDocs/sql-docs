---
title: "DataSourceID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataSourceID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataSourceID"
helpviewer_keywords: 
  - "DataSourceID element"
ms.assetid: 2d71ba53-1684-4da0-8da4-fb75033c971b
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSourceID Element (ASSL)
  Identifies the [DataSource](../objects/datasource-element-assl.md) element associated with the parent element.  
  
## Syntax  
  
```xml  
  
<CubeBinding> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <DataSourceID>...</DataSourceID>  
   ...  
</CubeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|Depends on context|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CubeBinding](../data-type/cubebinding-data-type-out-of-line-assl.md), [CubeDimensionBinding](../data-type/binding-data-type-assl.md), [DimensionBinding](../data-type/dimensionbinding-data-type-assl.md), [MeasureGroupBinding](../data-type/measuregroupbinding-data-type-assl.md), [QueryBinding](../data-type/querybinding-data-type-assl.md), [DataSourceView](../objects/datasourceview-element-assl.md), [TableBinding](../data-type/tablebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `DataSourceID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CubeDimensionBinding>, <xref:Microsoft.AnalysisServices.DimensionBinding>, <xref:Microsoft.AnalysisServices.MeasureGroupBinding>, <xref:Microsoft.AnalysisServices.QueryBinding>, <xref:Microsoft.AnalysisServices.DataSourceView>, and <xref:Microsoft.AnalysisServices.TableBinding>.  
  
## See Also  
 [ID Element &#40;ASSL&#41;](id-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
