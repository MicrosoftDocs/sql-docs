---
title: "ManagedProvider Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ManagedProvider Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ManagedProvider element"
ms.assetid: ed5a1077-20a4-40b9-b62d-0db0d53b9624
author: minewiskan
ms.author: owend
manager: craigg
---
# ManagedProvider Element (ASSL)
  Contains the name of the managed provider used by an element that is derived from the [DataSource](../data-type/datasource-data-type-assl.md) data type.  
  
## Syntax  
  
```xml  
  
<DataSource>  
   ...  
   <ManagedProvider>...</ManagedProvider>  
   ...  
</DataSource>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataSource](../data-type/datasource-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If a data source uses a managed provider, the `ManagedProvider` element contains the name of the managed provider.  
  
## See Also  
 [Name Element &#40;ASSL&#41;](name-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
