---
title: "Ordinal Element (ASSL) | Microsoft Docs"
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
  - "Ordinal Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Ordinal"
helpviewer_keywords: 
  - "Ordinal element"
ms.assetid: 64e68ad5-439c-4c1d-9df4-ee90c56761b4
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Default value|**0**|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md), [CubeAttributeBinding](../../../analysis-services/scripting/data-type/cubeattributebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 **AttributeBinding** and **CubeAttributeBinding** elements in which the [Type](../../../analysis-services/scripting/properties/type-element-binding-assl.md) property is set to either *Key* or *Translation* can be bound to an attribute that is in turn bound to a collection of columns in the data source view. The value of the **Ordinal** element determines to which column the **AttributeBinding** or **CubeAttributeBinding** refers in that collection.  
  
 The elements that correspond to the parents of **Ordinal** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AttributeBinding> and <xref:Microsoft.AnalysisServices.CubeAttributeBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  