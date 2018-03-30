---
title: "TabularBinding Data Type (ASSL) | Microsoft Docs"
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
  - "TabularBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TabularBinding"
helpviewer_keywords: 
  - "TabularBinding data type"
ms.assetid: 24587e34-20be-4693-81d8-038a6fc4e8ee
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# TabularBinding Data Type (ASSL)
  Defines an abstract derived data type that represents a binding to a tabular item such as a table or a cube dimension.  
  
## Syntax  
  
```xml  
  
<TabularBinding>  
   <!-- The TabularBinding element has no child elements other than those inherited from Binding -->  
</TabularBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
|Derived data types|[DSVTableBinding](../../../2014/analysis-services/dev-guide/dsvtablebinding-data-type-assl.md), [QueryBinding](../../../2014/analysis-services/dev-guide/querybinding-data-type-assl.md), [TableBinding](../../../2014/analysis-services/dev-guide/tablebinding-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|See [Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
  
## Remarks  
 For additional information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../2014/analysis-services/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TabularBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  