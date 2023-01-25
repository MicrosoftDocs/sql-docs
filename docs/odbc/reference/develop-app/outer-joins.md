---
description: "Outer Joins"
title: "Outer Joins | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "outer join escape sequences [ODBC]"
  - "escape sequences [ODBC], outer join"
ms.assetid: be1a0203-5da9-4871-9566-4bd3fbc0895c
author: David-Engel
ms.author: v-davidengel
---
# Outer Joins
ODBC supports the SQL-92 left, right, and full outer join syntax. The escape sequence for outer joins is  
  
 **{oj** _outer-join_**}**  
  
 where *outer-join* is  
  
 *table-reference* {**LEFT &#124; RIGHT &#124; FULL} OUTER JOIN** {*table-reference* &#124; *outer-join*} **ON** _search-condition_  
  
 *table-reference* specifies a table name, and *search-condition* specifies the join condition between the *table-references*.  
  
 An outer join request must appear after the **FROM** keyword and before the **WHERE** clause (if one exists). For complete syntax information, see [Outer Join Escape Sequence](../../../odbc/reference/appendixes/outer-join-escape-sequence.md) in Appendix C: SQL Grammar.  
  
 For example, the following SQL statements create the same result set that lists all customers and shows which has open orders. The first statement uses the escape-sequence syntax. The second statement uses the native syntax for Oracle and is not interoperable.  
  
```  
SELECT Customers.CustID, Customers.Name, Orders.OrderID, Orders.Status  
   FROM {oj Customers LEFT OUTER JOIN Orders ON Customers.CustID=Orders.CustID}  
   WHERE Orders.Status='OPEN'  
  
SELECT Customers.CustID, Customers.Name, Orders.OrderID, Orders.Status  
   FROM Customers, Orders  
   WHERE (Orders.Status='OPEN') AND (Customers.CustID= Orders.CustID(+))  
```  
  
 To determine the types of outer joins that a data source and driver support, an application calls **SQLGetInfo** with the SQL_OJ_CAPABILITIES flag. The types of outer joins that might be supported are left, right, full, or nested outer joins; outer joins in which the column names in the **ON** clause do not have the same order as their respective table names in the **OUTER JOIN** clause; inner joins in conjunction with outer joins; and outer joins using any ODBC comparison operator. If the SQL_OJ_CAPABILITIES information type returns 0, no outer join clause is supported.
