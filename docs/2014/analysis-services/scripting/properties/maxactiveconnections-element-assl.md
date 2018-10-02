---
title: "MaxActiveConnections Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MaxActiveConnections Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "MaxActiveConnections element"
ms.assetid: 0dc5b64d-061d-409f-95c0-4c63f87f5ee4
author: minewiskan
ms.author: owend
manager: craigg
---
# MaxActiveConnections Element (ASSL)
  Contains the maximum number of concurrent connections allowed by an element that is derived from the [DataSource](../data-type/datasource-data-type-assl.md) data type.  
  
## Syntax  
  
```xml  
  
<DataSource>  
   ...  
   <MaxActiveConnections>...</MaxActiveConnections>  
   ...  
</DataSource>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|`10`|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataSource](../data-type/datasource-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If the value of this element is set to zero, the maximum number of concurrent connections is determined by the data cartridge that is used to access the data source. If the value of this element is set to a negative value, the maximum number of concurrent connections is unlimited.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
