---
title: "ProactiveCachingObjectNotificationBinding Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ProactiveCachingObjectNotificationBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ProactiveCachingObjectNotificationBinding data type"
ms.assetid: b3cf5fb6-6121-4f25-8de6-f171792c440d
author: minewiskan
ms.author: owend
manager: craigg
---
# ProactiveCachingObjectNotificationBinding Data Type (ASSL)
  Defines an abstract derived data type that represents information to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about data source changes, either in specified tables and views or in tables and views identified through existing data bindings that require rebuilding the cache.  
  
## Syntax  
  
```xml  
  
<ProactiveCachingObjectNotificationBinding>  
   <!-- The following elements extend ProactiveCachingBinding -->  
   <NotificationTechnique>...</NotificationTechnique>  
</ProactiveCachingObjectNotificationBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[ProactiveCachingBinding](binding-data-type-assl.md)|  
|Derived data types|[ProactiveCachingInheritedBinding](inheritedbinding-data-type-assl.md), [ProactiveCachingTablesBinding](proactivecachingtablesbinding-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[NotificationTechnique](../properties/notificationtechnique-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 For more information about the `ProactiveCachingBinding` type, including a table of the inheritance hierarchy of `ProactiveCachingBinding` types, see [ProactiveCachingBinding Data Type &#40;ASSL&#41;](binding-data-type-assl.md).  
  
 For more information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCachingObjectNotificationBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
