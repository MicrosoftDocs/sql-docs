---
title: "TableNotification Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "TableNotification Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#TableNotification"
  - "microsoft.xml.analysis.tablenotification"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#TableNotification"
helpviewer_keywords: 
  - "TableNotification element"
ms.assetid: 097b0d53-cb0b-4454-963f-60964fd429e0
author: minewiskan
ms.author: owend
manager: craigg
---
# TableNotification Element (XMLA)
  Represents a table notification for a [NotifyTableChange](../xml-elements-commands/notifytablechange-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<TableNotifications>  
   ...  
   <TableNotification>  
      <DbSchemaName>...</DbSchemaName>  
      <DbTableName>...</DbTableName>  
   </TableNotification>  
   ...  
</TableNotifications>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[TableNotifications](tablenotifications-element-xmla.md)|  
|Child elements|[DbSchemaName](name-element-xmla.md), [DbTableName](dbtablename-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
