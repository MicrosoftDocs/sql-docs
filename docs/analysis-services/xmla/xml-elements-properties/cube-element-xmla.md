---
title: "Cube Element (XMLA) | Microsoft Docs"
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
  - "Cube Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.cube"
  - "urn:schemas-microsoft-com:xml-analysis#Cube"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Cube"
helpviewer_keywords: 
  - "Cube element"
ms.assetid: 2e8662f4-fb2e-43af-b70a-9e0b5872c9b9
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Cube Element (XMLA)
  Identifies the cube that contains the dimension represented by the parent [Object](../../../analysis-services/xmla/xml-elements-properties/object-element-dimension-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Object>  
   ...  
   <Cube>...</Cube>  
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
|Parent elements|[Object](../../../analysis-services/xmla/xml-elements-properties/object-element-dimension-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Cube** element is an object identifier that contains the name of the cube that contains the dimension represented by the **Object** element.  
  
## See Also  
 [Database Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/database-element-xmla.md)   
 [Dimension Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/dimension-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  