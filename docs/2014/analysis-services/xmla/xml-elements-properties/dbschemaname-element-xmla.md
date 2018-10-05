---
title: "DbSchemaName Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DbSchemaName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#DbSchemaName"
  - "microsoft.xml.analysis.dbschemaname"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DbSchemaName"
helpviewer_keywords: 
  - "DbSchemaName element"
ms.assetid: 40ca10c9-7597-48fe-a9d9-ee2c7b84d4d1
author: minewiskan
ms.author: owend
manager: craigg
---
# DbSchemaName Element (XMLA)
  Contains the name of the schema used by the parent [TableNotification](tablenotification-element-xmla.md) element in the table identified by the [DbTableName](name-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<TableNotification>  
   ...  
   <DbSchemaName>...</DbSchemaName>  
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
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
