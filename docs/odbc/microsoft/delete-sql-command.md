---
title: "DELETE - SQL Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "DELETE [ODBC]"
ms.assetid: 0d5bd477-626f-4f22-a05a-f531d9f8c5e7
author: MightyPen
ms.author: genemi
manager: craigg
---
# DELETE - SQL Command
Marks records for deletion.  
  
 The Visual FoxPro ODBC Driver supports the native Visual FoxPro language syntax for this command. For driver-specific information, see the Remarks.  
  
## Syntax  
  
```  
  
DELETE FROM [DatabaseName!]TableName  
   [WHERE FilterCondition1 [AND | OR FilterCondition2 ...]]  
```  
  
## Arguments  
 FROM [ *DatabaseName!*] *TableName*  
 Specifies the table in which records are marked for deletion.  
  
 *DatabaseName!* specifies the name of a database that contains the table if the containing database is not the database specified with the data source. You must include the name of a database that contains the table if the database is not the database specified with the data source. Include the exclamation point (!) delimiter after the database name and before the table name.  
  
 WHERE *FilterCondition1*[AND &#124; OR *FilterCondition2*...]  
 Specifies that Visual FoxPro mark only certain records for deletion.  
  
 *FilterCondition* specifies the criteria that records must meet to be marked for deletion. You can include as many filter conditions as you want, connecting them with the AND or OR operator. You can also use the NOT operator to reverse the value of a logical expression, or you can use **EMPTY**( ) to check for an empty field.  
  
## Remarks  
 If SET DELETED is set to ON, records marked for deletion are ignored by all commands that include a scope.  
  
 DELETE - SQL uses record locking when marking multiple records for deletion in tables opened for shared access. This reduces record contention in multiuser situations but can decrease performance. For maximum performance, open the table for exclusive use.  
  
## Driver Remarks  
 When your application sends the ODBC SQL statement DELETE to the data source, the Visual FoxPro ODBC Driver converts the command into the Visual FoxPro DELETE command without translation.  
  
## See Also  
 [SET DELETED Command](../../odbc/microsoft/set-deleted-command.md)
