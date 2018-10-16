---
title: "TimeAttributeBinding Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "TimeAttributeBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TimeAttributeBinding"
helpviewer_keywords: 
  - "TimeAttributeBinding data type"
ms.assetid: e8317b1c-a83a-4074-9538-400792500985
author: minewiskan
ms.author: owend
manager: craigg
---
# TimeAttributeBinding Data Type (ASSL)
  Defines a derived data type that represents a "placeholder" binding for generated data items, such as the key columns of an attribute, in a server time dimension.  
  
## Syntax  
  
```xml  
  
<TimeAttributeBinding>  
   <!-- TimeAttributeBinding has no child elements other than those inherited from Binding -->  
</TimeAttributeBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Binding](binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|See [Binding](binding-data-type-assl.md)|  
  
## Remarks  
 For more information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TimeAttributeBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
