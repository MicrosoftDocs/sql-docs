---
title: "Resultset Data Type (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Resultset Data Type (XMLA)
  Defines an abstract primitive data type that represents data returned from a [Discover](../../../2014/analysis-services/dev-guide/discover-method-xmla.md) or [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method call.  
  
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
|Derived data types|[MDDataSet](../../../2014/analysis-services/dev-guide/mddataset-data-type-xmla.md), [olapxmla_EmptyResult](../../../2014/analysis-services/dev-guide/emptyresult-data-type-xmla.md), [Rowset](../../../2014/analysis-services/dev-guide/rowset-data-type-xmla.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Exception](../../../2014/analysis-services/dev-guide/exception-element-xmla.md), [Messages](../../../2014/analysis-services/dev-guide/messages-element-xmla.md)|  
|Derived elements|None|  
  
## Remarks  
 The `Resultset` data type is a self-describing XML result set that can include both schema and data, depending on the type of information to be returned.  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/xml-data-types-xmla.md)  
  
  