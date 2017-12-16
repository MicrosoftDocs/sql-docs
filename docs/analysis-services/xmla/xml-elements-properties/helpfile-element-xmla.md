---
title: "HelpFile Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services, azure-analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "HelpFile Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#HelpFile"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#HelpFile"
  - "microsoft.xml.analysis.helpfile"
helpviewer_keywords: 
  - "HelpFile element"
ms.assetid: 537ea7a8-5064-4a31-b0cd-ab7e891fef09
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "Inactive"
---
# HelpFile Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains the path or URL to the Help file or topic that describes the parent [Error](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Error>  
   ...  
   <HelpFile>...</HelpFile>  
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
  
  
