---
title: "QueryBinding Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "QueryBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "QueryBinding"
helpviewer_keywords: 
  - "QueryBinding data type"
ms.assetid: 7b58fc89-0060-4e56-ad99-6f74fe8cfc6d
author: minewiskan
ms.author: owend
manager: craigg
---
# QueryBinding Data Type (ASSL)
  Defines a derived data type that represents the association of a [DataSource](../objects/datasource-element-assl.md) element with a [QueryDefinition](../properties/querydefinition-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<QueryBinding>  
   <!-- The following elements extend TabularBinding -->  
   <DataSourceID>...</DataSourceID>  
      <QueryDefinition>...</QueryDefinition>  
</QueryBinding>  
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
|Child elements|[DataSourceID](../properties/id-element-assl.md), [QueryDefinition](../properties/querydefinition-element-assl.md)|  
|Derived elements|See [Binding](binding-data-type-assl.md)|  
  
## Remarks  
 For more information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.QueryBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
