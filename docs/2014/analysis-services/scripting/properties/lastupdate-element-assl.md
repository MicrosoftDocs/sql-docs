---
title: "LastUpdate Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "LastUpdate Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "LastUpdate element"
ms.assetid: 639db733-a082-4f57-868d-a3bcd5e7a4f6
author: minewiskan
ms.author: owend
manager: craigg
---
# LastUpdate Element (ASSL)
  Contains a read-only timestamp that indicates the last time that the associated [Database](../objects/database-element-assl.md) or any of the major objects that the database contains were altered.  
  
## Syntax  
  
```xml  
  
<Database> <!-- or one of the elements that are listed in the Element Relationships table -->  
   ...  
   <LastUpdate>...</LastUpdate>  
   ...  
</Assembly>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|DateTime|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Database](../objects/database-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `LastUpdate` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
