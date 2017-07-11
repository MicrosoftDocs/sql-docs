---
title: "Parameters Element (XMLA) | Microsoft Docs"
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
  - "Parameters Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Parameters"
  - "urn:schemas-microsoft-com:xml-analysis#Parameters"
  - "microsoft.xml.analysis.parameters"
helpviewer_keywords: 
  - "Parameters element"
ms.assetid: d46454a1-a1d1-4aa8-95ea-54be22a53e83
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Parameters Element (XMLA)
  Contains a collection of [Parameter](../../../analysis-services/xmla/xml-elements-properties/parameter-element-xmla.md) elements used by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
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
|Parent elements|[Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md)|  
|Child elements|[Parameter](../../../analysis-services/xmla/xml-elements-properties/parameter-element-xmla.md)|  
  
## Remarks  
 Some XML for Analysis (XMLA) commands, such as the [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) command, can require additional information. The **Parameters** element provides a mechanism for providing additional information, including chunked information, for an XMLA command.  
  
 If the XMLA command does not use the **Parameters** element, the element can be omitted when calling the **Execute** method.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  