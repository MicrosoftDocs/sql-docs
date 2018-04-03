---
title: "EmptyResult Data Type (XMLA) | Microsoft Docs"
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
  - "EmptyResult Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#EmptyResult"
  - "olapxmla_EmptyResult"
  - "urn:schemas-microsoft-com:xml-analysis#EmptyResult"
helpviewer_keywords: 
  - "EmptyResult data type"
ms.assetid: 63818123-acbb-4220-9d60-1aa20a7326a1
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# EmptyResult Data Type (XMLA)
  Defines a derived data type that represents a [root](../../../2014/analysis-services/dev-guide/root-element-xmla.md) element that does not return data from a [Discover](../../../2014/analysis-services/dev-guide/discover-method-xmla.md) or [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method call.  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis:empty  
  
## Syntax  
  
```xml  
  
<root xmlns="urn:schemas-microsoft-com:xml-analysis:empty">  
   <!-- All elements are inherited from Resultset -->  
</root>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Resultset](../../../2014/analysis-services/dev-guide/resultset-data-type-xmla.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|[root](../../../2014/analysis-services/dev-guide/root-element-xmla.md)|  
  
## Remarks  
 Some XML for Analysis (XMLA) commands are not expected to return a result, or could not return a result because of an error. XMLA commands that do not return a result return the `EmptyResult` data type, a namespace on the `root` element.  
  
## Example  
 The following example represents a `root` element returned for an empty result.  
  
```  
<return>  
   <root xmlns="urn:schemas-microsoft-com:xml-analysis:empty"/>  
</return>  
```  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/xml-data-types-xmla.md)  
  
  