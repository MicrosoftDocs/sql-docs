---
title: "Parameter Element (XMLA) | Microsoft Docs"
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
  - "Parameter Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Parameter"
  - "microsoft.xml.analysis.parameter"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Parameter"
helpviewer_keywords: 
  - "Parameter element"
ms.assetid: fe31ac3d-a3e8-4f60-a81a-c43271ddbed4
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Parameter Element (XMLA)
  Contains the name and value of a parameter used by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
## Syntax  
  
```  
  
<Parameters>  
...  
   <Parameter>  
      <Name>...</Name>  
      <Value>...</Value>  
   </Parameter>  
...  
</Parameters>  
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
|Parent elements|[Parameters](../../../analysis-services/xmla/xml-elements-properties/parameters-element-xmla.md)|  
|Child elements|[Name](../../../analysis-services/xmla/xml-elements-properties/name-element-parameter-xmla.md), [Value](../../../analysis-services/xmla/xml-elements-properties/value-element-parameter-xmla.md)|  
  
## Remarks  
 Some XML for Analysis (XMLA) commands, such as the [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) command, can require additional information. The **Parameter** element provides a mechanism for providing additional information, including chunked information, for an XMLA command.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  