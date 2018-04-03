---
title: "ProactiveCachingTablesBinding Data Type (ASSL) | Microsoft Docs"
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
  - "ProactiveCachingTablesBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ProactiveCachingTablesBinding data type"
ms.assetid: f6b3f6fc-757c-4b1e-bb3a-d26482888d14
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ProactiveCachingTablesBinding Data Type (ASSL)
  Defines a derived data type that represents information to the [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md) element about data source changes in specified tables and views that require rebuilding the cache.  
  
## Syntax  
  
```xml  
  
<ProactiveCachingTablesBinding>  
   <!-- The following elements extend ProactiveCachingObjectNotificationBinding -->  
   <TableNotification>...</TableNotification>  
</ProactiveCachingTablesBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[ProactiveCachingObjectNotificationBinding](../../../2014/analysis-services/dev-guide/proactivecachingobjectnotificationbinding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[TableNotification](../../../2014/analysis-services/dev-guide/tablenotification-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 For more information about the `ProactiveCachingBinding` type, including a table of the inheritance hierarchy of `ProactiveCachingBinding` types, see [ProactiveCachingBinding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/proactivecachingbinding-data-type-assl.md).  
  
 For more information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../2014/analysis-services/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCachingTablesBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  