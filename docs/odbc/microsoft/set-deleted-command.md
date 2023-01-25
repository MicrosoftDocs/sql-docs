---
description: "SET DELETED Command"
title: "SET DELETED Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SET DELETED command [ODBC]"
ms.assetid: 6b5e0086-156d-471d-8e7f-6c5fa9686cd5
author: David-Engel
ms.author: v-davidengel
---
# SET DELETED Command
Specifies whether records marked for deletion are processed and whether they are available for use in other commands.  
  
## Syntax  
  
```  
  
SET DELETED ON | OFF  
```  
  
## Arguments  
 ON  
 (Default for the driver; the default for Visual FoxPro is OFF.) Specifies that commands that operate on records (including records in related tables) using a scope ignore records marked for deletion.  
  
 OFF  
 Specifies that records marked for deletion can be accessed by commands that operate on records (including records in related tables) using a scope.  
  
## Remarks  
 Queries that use DELETED( ) to test the status of records can be optimized using Visual FoxPro Rushmore technology if the table is indexed on DELETED( ).  
  
> [!IMPORTANT]  
>  SET DELETED is ignored if the default scope for the command is the current record or if you include a scope of a single record. INDEX always ignores SET DELETED and indexes all records in the table.  
  
## See Also  
 [DELETE - SQL Command](../../odbc/microsoft/delete-sql-command.md)
