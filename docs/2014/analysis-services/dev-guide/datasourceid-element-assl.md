---
title: "DataSourceID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DataSourceID Element (ASSL)
  Identifies the [DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md) element associated with the parent element.  
  
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
|Parent elements|[CubeBinding](../../../2014/analysis-services/dev-guide/cubebinding-data-type-out-of-line-assl.md), [CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md), [DimensionBinding](../../../2014/analysis-services/dev-guide/dimensionbinding-data-type-assl.md), [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md), [QueryBinding](../../../2014/analysis-services/dev-guide/querybinding-data-type-assl.md), [DataSourceView](../../../2014/analysis-services/dev-guide/datasourceview-element-assl.md), [TableBinding](../../../2014/analysis-services/dev-guide/tablebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `DataSourceID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CubeDimensionBinding>, <xref:Microsoft.AnalysisServices.DimensionBinding>, <xref:Microsoft.AnalysisServices.MeasureGroupBinding>, <xref:Microsoft.AnalysisServices.QueryBinding>, <xref:Microsoft.AnalysisServices.DataSourceView>, and <xref:Microsoft.AnalysisServices.TableBinding>.  
  
## See Also  
 [ID Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/id-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  