---
title: "ReadWriteMode Element | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "ReadWriteMode command"
ms.assetid: 379bcaca-bb7e-4934-a9e7-21f8ede2fdc7
author: minewiskan
ms.author: owend
manager: craigg
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
|Parent elements|[Database](database-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 Databases are created in `ReadWrite` mode only. Databases cannot be created in `ReadOnly` mode.  
  
 The value of the `ReadWriteMode` element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*ReadOnly*|No changes or updates can be applied to the database.|  
|*ReadWrite*|Changes and updates can be applied to the database.|  
  
## See Also  
 [Attach Element](../xml-elements-commands/attach-element.md)   
 [Attach and Detach Analysis Services Databases](../../multidimensional-models/attach-and-detach-analysis-services-databases.md)   
 [Move an Analysis Services Database](../../multidimensional-models/move-an-analysis-services-database.md)   
 [Database ReadWriteModes](../../multidimensional-models/database-readwritemodes.md)   
 [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../multidimensional-models/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md)  
  
  
