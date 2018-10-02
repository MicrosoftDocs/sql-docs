---
title: "Resultset Data Type (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Resultset Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Resultset"
  - "urn:schemas-microsoft-com:xml-analysis#Resultset"
  - "Resultset"
helpviewer_keywords: 
  - "Resultset data type"
ms.assetid: 45e7d7d6-1f89-4dc8-b39d-9270ea2db541
author: minewiskan
ms.author: owend
manager: craigg
---
# Resultset Data Type (XMLA)
  Defines an abstract primitive data type that represents data returned from a [Discover](../xml-elements-methods-discover.md) or [Execute](../xml-elements-methods-execute.md) method call.  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis:resultset  
  
## Syntax  
  
```xml  
  
<Resultset>  
   <Exception>...</Exception>  
   <Messages>...</Messages>  
</Resultset>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[MDDataSet](mddataset-data-type-xmla.md), [olapxmla_EmptyResult](emptyresult-data-type-xmla.md), [Rowset](rowset-data-type-xmla.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Exception](../xml-elements-properties/exception-element-xmla.md), [Messages](../xml-elements-properties/messages-element-xmla.md)|  
|Derived elements|None|  
  
## Remarks  
 The `Resultset` data type is a self-describing XML result set that can include both schema and data, depending on the type of information to be returned.  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](xml-data-types-xmla.md)  
  
  
