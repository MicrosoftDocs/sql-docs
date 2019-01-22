---
title: "UPDATE, DELETE, and INSERT Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "updating data [ODBC], about updating data"
  - "DELETE [ODBC]"
  - "UPDATE [ODBC]"
  - "INSERT [ODBC]"
  - "data updates [ODBC], about data updates"
ms.assetid: 5004ea72-4c49-4064-9752-f7032ba7f133
author: MightyPen
ms.author: genemi
manager: craigg
---
# UPDATE, DELETE, and INSERT Statements
SQL-based applications make changes to tables by executing the **UPDATE**, **DELETE**, and **INSERT** statements. These statements are part of the Minimum SQL grammar conformance level and must be supported by all drivers and data sources.  
  
 The syntax of these statements is:  
  
 **UPDATE** _table-name_  
  
 **SET** _column-identifier_ **=** {*expression* &#124; **NULL**}  
  
 [**,** _column-identifier_ **=** {*expression* &#124; **NULL**}]...  
  
 [**WHERE** _search-condition_]  
  
 **DELETE FROM** _table-name_[**WHERE** _search-condition_]  
  
 **INSERT INTO** _table-name_[**(** _column-identifier_ [**,** _column-identifier_]...**)**]  
  
 {*query-specification* &#124; **VALUES (** _insert-value_ [**,** _insert-value_]...**)**}  
  
 Note that the *query-specification* element is valid only in the Core and Extended SQL grammars, and that the *expression* and *search-condition* elements become more complex in the Core and Extended SQL grammars.  
  
 Like other SQL statements, **UPDATE**, **DELETE**, and **INSERT** statements are often more efficient when they use parameters. For example, the following statement can be prepared and repeatedly executed to insert multiple rows in the Orders table:  
  
```  
INSERT INTO Orders (PartID, Description, Price) VALUES (?, ?, ?)  
```  
  
 This efficiency can be increased by passing arrays of parameter values. For more information about statement parameters and arrays of parameter values, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md).
