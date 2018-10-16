---
title: "SELECT Statement Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC SQL grammar, SELECT statement limitations"
  - "SELECT statement limitations [ODBC]"
ms.assetid: c6b05955-f8fd-4706-a1a7-a8dbd74870c2
author: MightyPen
ms.author: genemi
manager: craigg
---
# SELECT Statement Limitations
An aggregate-function column cannot be mixed with a non-aggregate column in a SELECT statement.  
  
 The select list of a SELECT statement that has a GROUP BY clause can only have expressions from the GROUP BY clause or set functions.  
  
 The use of an asterisk (to select all columns) in a SELECT statement containing a GROUP BY clause is not supported. The names of the columns to be selected must be specified.  
  
 The use of a vertical bar in a SELECT statement is not supported. Use a parameter in the SELECT statement if you need to refer to a data value that contains a vertical bar.  
  
 When using a column alias in a SELECT statement, the word "as" must precede the alias. For example, "SELECT col1 as a from b." Without the "as", the statement will return an error.  
  
 If an incorrect column name is entered into a SELECT statement, a SQLSTATE 07001 error, "Wrong Number of Parameters," is returned instead of a SQLSTATE S0022 error, "Column Not Found."  
  
 When the Microsoft Excel driver is used, if an empty string is inserted into a column, the empty string is converted to a NULL; a searched SELECT statement that is executed with an empty string in the WHERE clause will not succeed on that column.
