---
title: "DatabaseID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "DatabaseID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.databaseid"
  - "urn:schemas-microsoft-com:xml-analysis#DatabaseID"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DatabaseID"
helpviewer_keywords: 
  - "DatabaseID element"
ms.assetid: 2df720dd-9b42-449a-9df6-0d12930603f0
caps.latest.revision: 12
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# DatabaseID Element (XMLA)
  Identifies a database within a parent element that contains an object reference.  
  
## Syntax  
  
```xml  
  
<Object> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <DatabaseID>...</DatabaseID>  
   ...  
</Object>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
  
 **Cardinality**  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Source](../../../2014/analysis-services/dev-guide/source-element-xmla.md), [Target](../../../2014/analysis-services/dev-guide/target-element-xmla.md)|1-1: Required element that occurs once and only once.|  
|All others|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Object](../../../2014/analysis-services/dev-guide/object-element-xmla.md), [ParentObject](../../../2014/analysis-services/dev-guide/parentobject-element-xmla.md), [Source](../../../2014/analysis-services/dev-guide/source-element-xmla.md), [Target](../../../2014/analysis-services/dev-guide/target-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  