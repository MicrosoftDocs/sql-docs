---
title: "TableNotification Element (XMLA) | Microsoft Docs"
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
caps.latest.revision: 10
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# TableNotification Element (XMLA)
  Represents a table notification for a [NotifyTableChange](../../../2014/analysis-services/dev-guide/notifytablechange-element-xmla.md) command.  
  
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
|Parent elements|[TableNotifications](../../../2014/analysis-services/dev-guide/tablenotifications-element-xmla.md)|  
|Child elements|[DbSchemaName](../../../2014/analysis-services/dev-guide/dbschemaname-element-xmla.md), [DbTableName](../../../2014/analysis-services/dev-guide/dbtablename-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  