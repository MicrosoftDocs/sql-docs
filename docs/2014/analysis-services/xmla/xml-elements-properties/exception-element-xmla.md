---
title: "Exception Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Exception Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Exception"
  - "urn:schemas-microsoft-com:xml-analysis#Exception"
  - "microsoft.xml.analysis.exception"
helpviewer_keywords: 
  - "Exception element"
ms.assetid: 0be4cc2f-c03e-490a-a6f7-8b1ede5d09ba
author: minewiskan
ms.author: owend
manager: craigg
---
# Exception Element (XMLA)
  Indicates that an exception was returned from a [Discover](../xml-elements-methods-discover.md) or [Execute](../xml-elements-methods-execute.md) method call.  
  
 **Namespace** http://schemas.microsoft.com/analysisservices/2003/exception  
  
## Syntax  
  
```xml  
  
<root>  
   ...  
   <Exception />  
   ...  
</root>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[root](root-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 If an error occurs during the execution of a `Discover` method call or a single XMLA command in an `Execute` method call that prevents the method or command from completing, the `root` element for that method or command contains an `Exception` element and a `Messages` element. The `Exception` element indicates that an error which prevented the method or command from successfully executing occurred, and the `Messages` element contains the list of error or warning messages related to the error.  
  
## See Also  
 [Messages Element &#40;XMLA&#41;](messages-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
