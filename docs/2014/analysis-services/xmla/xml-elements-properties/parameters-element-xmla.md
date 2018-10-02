---
title: "Parameters Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Parameters Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Parameters"
  - "urn:schemas-microsoft-com:xml-analysis#Parameters"
  - "microsoft.xml.analysis.parameters"
helpviewer_keywords: 
  - "Parameters element"
ms.assetid: d46454a1-a1d1-4aa8-95ea-54be22a53e83
author: minewiskan
ms.author: owend
manager: craigg
---
# Parameters Element (XMLA)
  Contains a collection of [Parameter](parameter-element-xmla.md) elements used by the [Execute](../xml-elements-methods-execute.md) method.  
  
 **Namespace:** `urn:schemas-microsoft-com:xml-analysis`  
  
## Syntax  
  
```  
  
<Execute>  
...  
   <Parameters>  
      <Parameter>...</Parameter>  
   </Parameters>  
...  
</Execute>  
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
|Parent elements|[Execute](../xml-elements-methods-execute.md)|  
|Child elements|[Parameter](parameter-element-xmla.md)|  
  
## Remarks  
 Some XML for Analysis (XMLA) commands, such as the [Process](../xml-elements-commands/process-element-xmla.md) command, can require additional information. The `Parameters` element provides a mechanism for providing additional information, including chunked information, for an XMLA command.  
  
 If the XMLA command does not use the `Parameters` element, the element can be omitted when calling the `Execute` method.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
