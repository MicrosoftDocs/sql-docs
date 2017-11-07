---
title: "Value Element (Parameter) (XMLA) | Microsoft Docs"
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
  - "Value Element (Parameter)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.value"
  - "urn:schemas-microsoft-com:xml-analysis#Value"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Value"
helpviewer_keywords: 
  - "Value element"
ms.assetid: e590d189-91aa-40c7-8669-09c87812f4ce
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Value Element (Parameter) (XMLA)
  Contains the value of a parameter represented by the [Parameter](../../../analysis-services/xmla/xml-elements-properties/parameter-element-xmla.md) element.  
  
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
|Parent elements|[Parameter](../../../analysis-services/xmla/xml-elements-properties/parameter-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Value** element can store any simple XML type, as well as the XML for Analysis (XMLA) **Rowset** data type, for parameters used by XMLA commands in the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  