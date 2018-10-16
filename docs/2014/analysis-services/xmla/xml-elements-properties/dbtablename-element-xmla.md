---
title: "DbTableName Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DbTableName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DbTableName"
  - "microsoft.xml.analysis.dbtablename"
  - "urn:schemas-microsoft-com:xml-analysis#DbTableName"
helpviewer_keywords: 
  - "DbTableName element"
ms.assetid: 0ffda645-2a88-4f42-8929-9d7385c19a74
author: minewiskan
ms.author: owend
manager: craigg
---
# DbTableName Element (XMLA)
  Contains the name of the table used by the parent [TableNotification](tablenotification-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<TableNotification>  
   ...  
   <DbTableName>...</DbTableName>  
   ...  
</TableNotification>  
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
|Parent elements|[TableNotification](tablenotification-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [DbSchemaName Element &#40;XMLA&#41;](name-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
