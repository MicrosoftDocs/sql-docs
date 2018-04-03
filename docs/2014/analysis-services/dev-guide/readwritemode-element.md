---
title: "ReadWriteMode Element | Microsoft Docs"
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
helpviewer_keywords: 
  - "ReadWriteMode command"
ms.assetid: 379bcaca-bb7e-4934-a9e7-21f8ede2fdc7
caps.latest.revision: 13
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# ReadWriteMode Element
  The `ReadWriteMode` database property specifies whether the database is in `ReadWrite` mode or in `ReadOnly` mode. These are the only two possible values of the property.  
  
## Syntax  
  
```xml  
  
<Database>  
...  
   <ddlns_100_0:ReadWriteMode>...</ddlns_100_0:ReadWriteMode>  
...  
</Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|ReadWrite|  
|Cardinality|0-1: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](../../../2014/analysis-services/dev-guide/database-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 Databases are created in `ReadWrite` mode only. Databases cannot be created in `ReadOnly` mode.  
  
 The value of the `ReadWriteMode` element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*ReadOnly*|No changes or updates can be applied to the database.|  
|*ReadWrite*|Changes and updates can be applied to the database.|  
  
## See Also  
 [Attach Element](../../../2014/analysis-services/dev-guide/attach-element.md)   
 [Attach and Detach Analysis Services Databases](../../../2014/analysis-services/attach-and-detach-analysis-services-databases.md)   
 [Move an Analysis Services Database](../../../2014/analysis-services/move-an-analysis-services-database.md)   
 [Database ReadWriteModes](../../../2014/analysis-services/database-readwritemodes.md)   
 [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../../2014/analysis-services/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md)  
  
  