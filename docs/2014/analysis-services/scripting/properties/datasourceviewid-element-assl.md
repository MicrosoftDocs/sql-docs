---
title: "DataSourceViewID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataSourceViewID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataSourceViewID"
helpviewer_keywords: 
  - "DataSourceViewID element"
ms.assetid: dcf617fe-0bf6-4767-af35-07c0c7fd96e5
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSourceViewID Element (ASSL)
  Identifies the [DataSourceView](../objects/datasourceview-element-assl.md) element associated with the [Binding](../data-type/binding-data-type-assl.md) parent element.  
  
## Syntax  
  
```xml  
  
<DataSourceViewBinding> <!-- or DSVTableBinding -->  
   ...  
   <DataSourceViewID>...</DataSourceViewID>  
   ...  
</DataSourceViewBinding>  
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
|Parent element|[DataSourceViewBinding](../data-type/datasourceviewbinding-data-type-assl.md), [DSVTableBinding](../data-type/tablebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `DataSourceViewID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.DataSourceViewBinding> and <xref:Microsoft.AnalysisServices.DSVTableBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
