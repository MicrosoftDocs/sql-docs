---
title: "TableNotifications Element (XMLA) | Microsoft Docs"
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
  - "TableNotifications Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#TableNotifications"
  - "microsoft.xml.analysis.tablenotifications"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#TableNotifications"
helpviewer_keywords: 
  - "TableNotifications element"
ms.assetid: 650f307d-1f11-47ce-9d0e-19cf3b1835b7
caps.latest.revision: 10
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# TableNotifications Element (XMLA)
  Contains a collection of [TableNotification](../../../2014/analysis-services/dev-guide/tablenotification-element-xmla.md) elements used by the [NotifyTableChange](../../../2014/analysis-services/dev-guide/notifytablechange-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<NotifyTableChange >  
   ...  
   <TableNotifications>  
      <TableNotification>...</TableNotification>  
   </TableNotifications>  
   ...  
</NotifyTableChange>  
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
|Parent elements|[NotifyTableChange](../../../2014/analysis-services/dev-guide/notifytablechange-element-xmla.md)|  
|Child elements|[TableNotification](../../../2014/analysis-services/dev-guide/tablenotification-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  