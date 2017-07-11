---
title: "InvalidXmlCharacters Element (ASSL) | Microsoft Docs"
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
  - "InvalidXmlCharacters Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "InvalidXmlCharacters"
helpviewer_keywords: 
  - "InvalidXmlCharacters element"
ms.assetid: 92b1210c-1075-4f2f-a2c4-dfdc6d7e5c05
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# InvalidXmlCharacters Element (ASSL)
  Specifies the handling method for XML characters in the source data that are not valid.  
  
## Syntax  
  
```xml  
  
<DataItem>  
   ...  
   <InvalidXmlCharacters>...</InvalidXmlCharacters>  
   ...  
</DataItem  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Preserve*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Preserve*|Invalid XML characters are preserved.|  
|*Remove*|Invalid XML characters are removed.|  
|*Replace*|Invalid XML characters are replaced with question mark (?) characters.|  
  
 The enumeration that corresponds to the allowed values for **InvalidXmlCharacters** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.InvalidXmlCharacters>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  