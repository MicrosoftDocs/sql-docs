---
title: "DataSourceViewBinding Data Type (ASSL) | Microsoft Docs"
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
  - "DataSourceViewBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataSourceViewBinding"
helpviewer_keywords: 
  - "DataSourceViewBinding data type"
ms.assetid: 1f08e2d8-b279-4181-9257-e56f9fcbd9bf
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DataSourceViewBinding Data Type (ASSL)
  Defines a derived data type that represents a binding between a data source view and the parent element.  
  
## Syntax  
  
```xml  
  
<DataSourceViewBinding>  
   <!-- The following elements extend Binding -->  
   <DataSourceViewID>...</DataSourceViewID>  
</DataSourceViewBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[DataSourceViewID](../../../2014/analysis-services/dev-guide/datasourceviewid-element-assl.md)|  
|Derived elements|See [Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
  
## Remarks  
 For additional information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../2014/analysis-services/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSourceViewBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  