---
title: "DROP TABLE Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "drop table command [ODBC]"
ms.assetid: bc50459b-8861-4889-84a9-129ae9065aa8
author: MightyPen
ms.author: genemi
manager: craigg
---
# DROP TABLE Command
Removes a table from the database specified with the data source and deletes it from disk.  
  
 The Visual FoxPro ODBC Driver supports the native Visual FoxPro language syntax for this command. For driver-specific information, see the Remarks.  
  
## Syntax  
  
```  
  
DROP TABLE TableName | FileName | ?  
```  
  
## Settings  
 *TableName*  
 Specifies the table to remove from the database specified with the data source and to delete from disk.  
  
 *FileName*  
 Specifies a free table to delete from disk.  
  
 ?  
 Displays the Remove dialog from which you can choose a table to remove from the database specified with the data source and to delete from disk.  
  
## Remarks  
 When DROP TABLE is issued, all primary indexes, default values, and validation rules associated with the table are also removed. DROP TABLE also affects other tables in the database specified with the data source if those tables have rules or relations associated with the table being removed. The rules and relations are no longer valid when the table is removed from the database.  
  
## Driver Remarks  
 When your application sends the ODBC SQL statement DROP TABLE to the data source, the Visual FoxPro ODBC Driver converts the command into the Visual FoxProDROP TABLE command using the syntax shown in the following table.  
  
|ODBC syntax|Data source|Visual FoxPro syntax|  
|-----------------|-----------------|--------------------------|  
|DROP TABLE *base-table-name*|Database (.dbc file)|REMOVE TABLE *TableName* DELETE|  
||Directory of free tables (.dbf files)|ERASE *dbfName*<br /><br /> ERASE *cdxName*<br /><br /> ERASE *fptName*|
