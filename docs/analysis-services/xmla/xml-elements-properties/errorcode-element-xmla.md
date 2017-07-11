---
title: "ErrorCode Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ErrorCode Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#ErrorCode"
  - "microsoft.xml.analysis.errorcode"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ErrorCode"
helpviewer_keywords: 
  - "ErrorCode element"
ms.assetid: da187661-b15e-4b95-8b49-7820ebcced40
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# ErrorCode Element (XMLA)
  Contains the numeric return code of the parent [Error](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Error>  
   ...  
   <ErrorCode>...</ErrorCode>  
   ...  
</Error>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|UnsignedInt|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Error](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  