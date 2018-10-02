---
title: "DSVTableBinding Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DSVTableBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DSVTableBinding"
helpviewer_keywords: 
  - "DSVTableBinding data type"
ms.assetid: 149e753f-6218-4805-9223-7155b6827e64
author: minewiskan
ms.author: owend
manager: craigg
---
# DSVTableBinding Data Type (ASSL)
  Defines a derived data type that represents the binding between a table and a [DataSourceView](../objects/datasourceview-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DSVTableBinding>  
   <!-- The following elements extend TabularBinding -->  
   <DataSourceViewID>...</DataSourceViewID>  
   <TableID>...</TableID>  
</DSVTableBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[TabularBinding](binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[DataSourceViewID](../properties/id-element-assl.md), [TableID](../properties/tableid-element-assl.md)|  
|Derived elements|See [Binding](binding-data-type-assl.md)|  
  
## Remarks  
 For additional information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding](binding-data-type-assl.md) element.  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DSVTableBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
