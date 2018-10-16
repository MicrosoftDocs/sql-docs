---
title: "Value Element (Parameter) (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Value Element (Parameter)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.value"
  - "urn:schemas-microsoft-com:xml-analysis#Value"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Value"
helpviewer_keywords: 
  - "Value element"
ms.assetid: e590d189-91aa-40c7-8669-09c87812f4ce
author: minewiskan
ms.author: owend
manager: craigg
---
# Value Element (Parameter) (XMLA)
  Contains the value of a parameter represented by the [Parameter](parameter-element-xmla.md) element.  
  
## Syntax  
  
```  
  
<Parameter>  
   ...  
   <Value>...</Value>  
   ...  
</Parameter>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Any|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Parameter](parameter-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `Value` element can store any simple XML type, as well as the XML for Analysis (XMLA) `Rowset` data type, for parameters used by XMLA commands in the [Execute](../xml-elements-methods-execute.md) method.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
