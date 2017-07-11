---
title: "Source Element (Error) (XMLA) | Microsoft Docs"
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
  - "Source Element (Error)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Source"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Source"
  - "microsoft.xml.analysis.source"
helpviewer_keywords: 
  - "Source element"
ms.assetid: eed47b9f-0501-4baf-8cac-3ea839a859c3
caps.latest.revision: 10
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Source Element (Error) (XMLA)
  Contains the name of the component that generated the parent [Error](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Error>  
   ...  
   <Source>...</Source>  
   ...  
</Error>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Error](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  