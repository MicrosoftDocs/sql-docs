---
title: "ProactiveCachingQueryBinding Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ProactiveCachingQueryBinding Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "ProactiveCachingQueryBinding data type"
ms.assetid: c1b06e50-9e68-40db-bdab-fc2cb3a8ff64
caps.latest.revision: 15
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ProactiveCachingQueryBinding Data Type (ASSL)
  Defines a derived data type that represents information to the [ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md) element about data source changes in tables and views, identified through the execution of the specified queries that require rebuilding the cache.  
  
## Syntax  
  
```xml  
  
<ProactiveCachingQueryBinding>  
   <!-- The following elements extend ProactiveCachingBinding -->  
   <RefreshInterval>...</RefreshInterval>  
   <QueryNotification>...</QueryNotification>  
</ProactiveCachingQueryBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[ProactiveCachingBinding](../../../analysis-services/scripting/data-type/proactivecachingbinding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[QueryNotification](../../../analysis-services/scripting/objects/querynotification-element-assl.md), [RefreshInterval](../../../analysis-services/scripting/properties/refreshinterval-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 For more information about the **ProactiveCachingBinding** type, including a table of the inheritance hierarchy of **ProactiveCachingBinding** types, see [ProactiveCachingBinding Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/proactivecachingbinding-data-type-assl.md).  
  
 For more information about the **Binding** type, including tables of Analysis Services Scripting Language (ASSL) objects of the **Binding** type and the inheritance hierarchy of **Binding** types, see [Binding Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../analysis-services/multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCachingQueryBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  