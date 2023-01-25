---
description: "UPDATE - SQL Command"
title: "UPDATE - SQL Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "update [ODBC]"
ms.assetid: ff1e0331-c060-4304-b280-039725b45f63
author: David-Engel
ms.author: v-davidengel
---
# UPDATE - SQL Command
Updates records in a table with new values.  
  
 The Visual FoxPro ODBC Driver supports the native Visual FoxPro language syntax for this command. For driver-specific information, see **Driver Remarks**.  
  
## Syntax  
  
```  
  
UPDATE [DatabaseName1!]TableName1  
SET Column_Name1 = eExpression1  
   [, Column_Name2 = eExpression2 ...]  
   WHERE FilterCondition1 [AND | OR FilterCondition2 ...]  
```  
  
## Arguments  
 UPDATE [ *DatabaseName1!*] *TableName1*  
 Specifies the table in which records are updated with new values.  
  
 *DatabaseName1!* specifies the name of a database other than the database specified with the data source containing the table. You must include the name of the database containing the table if the database is not the current one. Include the exclamation point (!) delimiter after the database name and before the table name.  
  
 SET *Column_Name1*= *eExpression1*[, *Column_Name2*= *eExpression2*  
 Specifies the columns that are updated and their new values. If you omit the WHERE clause, every row in the column is updated with the same value.  
  
 WHERE *FilterCondition1*[AND &#124; OR *FilterCondition2*...]  
 Specifies the records that are updated with new values.  
  
 *FilterCondition* specifies the criteria that records must meet to be updated with new values. You can include as many filter conditions as you like, connecting them with the AND or OR operator. You can also use the NOT operator to reverse the value of a logical expression, or you can use **EMPTY**( ) to check for an empty field.  
  
## Remarks  
 UPDATE - SQL can update only records in a single table.  
  
 Unlike REPLACE, UPDATE - SQL uses record locking when updating multiple records in tables opened for shared access. This reduces record contention in multiuser situations but can reduce performance. For maximum performance, open the table for exclusive use or use **FLOCK**( ) to lock the table.  
  
## Driver Remarks  
 When your application sends the ODBC SQL statement UPDATE to the data source, the Visual FoxPro ODBC Driver converts the command into the Visual FoxProUPDATE command without translation.  
  
## See Also  
 [DELETE - SQL Command](../../odbc/microsoft/delete-sql-command.md)   
 [INSERT - SQL Command](../../odbc/microsoft/insert-sql-command.md)
