---
title: "PerspectiveAttribute Data Type (ASSL) | Microsoft Docs"
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
  - "PerspectiveAttribute Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "PerspectiveAttribute"
helpviewer_keywords: 
  - "PerspectiveAttribute data type"
ms.assetid: bf4d45c1-e48d-4ada-bbab-49c3ac74948d
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# PerspectiveAttribute Data Type (ASSL)
  Defines a primitive data type that represents information about an attribute in a [PerspectiveDimension](../../../2014/analysis-services/dev-guide/perspectivedimension-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PerspectiveAttribute>  
      <AttributeID>...</AttributeID>  
   <DefaultMember>...</DefaultMember>  
   <AttributeHierarchyVisible>...</AttributeHierarchyVisible>  
      <Annotations>...</Annotations>  
</PerspectiveAttribute>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [AttributeHierarchyVisible](../../../2014/analysis-services/dev-guide/attributehierarchyvisible-element-assl.md), [AttributeID](../../../2014/analysis-services/dev-guide/attributeid-element-assl.md), [DefaultMember](../../../2014/analysis-services/dev-guide/defaultmember-element-assl.md)|  
|Derived elements|[Attribute](../../../2014/analysis-services/dev-guide/attribute-element-assl.md) ([Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md) collection of [PerspectiveDimension](../../../2014/analysis-services/dev-guide/perspectivedimension-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveAttribute>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  