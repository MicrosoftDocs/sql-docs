---
title: "ProactiveCachingBinding Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ProactiveCachingBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ProactiveCachingBinding data type"
ms.assetid: 02e6ff2f-2f18-4607-9198-bb46f113f9ac
author: minewiskan
ms.author: owend
manager: craigg
---
# ProactiveCachingBinding Data Type (ASSL)
  Defines an abstract derived data type that represents information to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about data source changes that require rebuilding the cache, or about the status of the rebuilding process.  
  
## Syntax  
  
```xml  
  
<ProactiveCachingBinding>  
   <!-- ProactiveCachingBinding is an abstract base class with no child elements -->  
</ProactiveCachingBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Binding](binding-data-type-assl.md)|  
|Derived data types|[ProactiveCachingIncrementalProcessingBinding](proactivecachingincrementalprocessingbinding-data-type-assl.md), [ProactiveCachingObjectNotificationBinding](proactivecachingobjectnotificationbinding-data-type-assl.md), [ProactiveCachingQueryBinding](querybinding-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|None|  
  
## Remarks  
 For more information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCachingBinding>.  
  
## Inheritance Hierarchy of ProactiveCachingBinding Types  
 The following hierarchy shows the inheritance of `ProactiveCachingBinding` types.  
  
 [Binding](binding-data-type-assl.md) element  
  
 `ProactiveCachingBinding` element  
  
 [ProactiveCachingObjectNotificationBinding](proactivecachingobjectnotificationbinding-data-type-assl.md) element  
  
 [ProactiveCachingInheritedBinding](inheritedbinding-data-type-assl.md) element  
  
 [ProactiveCachingTablesBinding](proactivecachingtablesbinding-data-type-assl.md) element  
  
 [ProactiveCachingQueryBinding](querybinding-data-type-assl.md) element  
  
 [ProactiveCachingIncrementalProcessingBinding](proactivecachingincrementalprocessingbinding-data-type-assl.md) element  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
