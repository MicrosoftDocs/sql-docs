---
title: "Ordinal Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Ordinal Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Ordinal"
helpviewer_keywords: 
  - "Ordinal element"
ms.assetid: 64e68ad5-439c-4c1d-9df4-ee90c56761b4
author: minewiskan
ms.author: owend
manager: craigg
---
# Ordinal Element (ASSL)
  Indicates the ordinal number to bind to in collections such as keys and translations.  
  
## Syntax  
  
```xml  
  
<AttributeBinding> <!-- or CubeAttributeBinding -->  
   ...  
   <Ordinal>...</Ordinal>  
   ...  
</AttributeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|`0`|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AttributeBinding](../data-type/binding-data-type-assl.md), [CubeAttributeBinding](../data-type/cubeattributebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 `AttributeBinding` and `CubeAttributeBinding` elements in which the [Type](type-element-binding-assl.md) property is set to either *Key* or *Translation* can be bound to an attribute that is in turn bound to a collection of columns in the data source view. The value of the `Ordinal` element determines to which column the `AttributeBinding` or `CubeAttributeBinding` refers in that collection.  
  
 The elements that correspond to the parents of `Ordinal` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AttributeBinding> and <xref:Microsoft.AnalysisServices.CubeAttributeBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
