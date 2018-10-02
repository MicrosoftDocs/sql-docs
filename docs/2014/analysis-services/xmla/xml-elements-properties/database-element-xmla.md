---
title: "Database Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Database Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.database"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Database"
  - "urn:schemas-microsoft-com:xml-analysis#Database"
helpviewer_keywords: 
  - "Database element"
ms.assetid: 2ded06c4-4eaf-4ccb-a416-41ee51ced8bc
author: minewiskan
ms.author: owend
manager: craigg
---
# Database Element (XMLA)
  Identifies the database that contains the dimension represented by the parent [Object](object-element-dimension-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Object>  
   ...  
   <Database>...</Database>  
   ...  
</Object>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Object](object-element-dimension-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `Database` element is an object identifier that contains the name of the Analysis Services database that contains the dimension represented by the `Object` element.  
  
## See Also  
 [Cube Element &#40;XMLA&#41;](cube-element-xmla.md)   
 [Dimension Element &#40;XMLA&#41;](dimension-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
