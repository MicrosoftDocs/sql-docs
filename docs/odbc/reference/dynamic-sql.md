---
description: Learn how Dynamic SQL can affect the performance of your application and how prepared statements in ODBC may be a faster solution.

title: Dynamic SQL performance in ODBC
ms.custom: ""
ms.date: 04/02/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL [ODBC], embedded SQL"
  - "SQL statements [ODBC], dynamic SQL"
  - "SQL statements [ODBC], embedded SQL"
  - "dynamic SQL [ODBC]"
  - "SQL [ODBC], dynamic SQL"
  - "embedded SQL [ODBC]"
ms.assetid: 0bfb9ab7-9c15-4433-93bc-bad8b6c9d287
author: David-Engel
ms.author: v-davidengel
---
# Dynamic SQL performance in ODBC

Although static SQL works well in many situations, there's a class of applications in which the data access cannot be determined in advance. For example, suppose a spreadsheet allows a user to enter a query, which the spreadsheet then sends to the DBMS to retrieve data. The contents of this query obviously cannot be known to the programmer when the spreadsheet program is written.

## Dynamic execution

To solve this problem, the spreadsheet uses a form of embedded SQL called dynamic SQL. Unlike static SQL statements, which are hard-coded in the program, dynamic SQL statements can be built at run time and placed in a string host variable. They're then sent to the DBMS for processing. Because the DBMS must generate an access plan at run time for dynamic SQL statements, dynamic SQL is generally slower than static SQL. When a program containing dynamic SQL statements is compiled, the dynamic SQL statements aren't stripped from the program, as in static SQL. Instead, they're replaced by a function call that passes the statement to the DBMS; static SQL statements in the same program are treated normally.

The simplest way to execute a dynamic SQL statement is with an EXECUTE IMMEDIATE statement. This statement passes the SQL statement to the DBMS for compilation and execution.

One disadvantage of the EXECUTE IMMEDIATE statement is that the DBMS must go through each of the [five steps of processing an SQL statement](processing-a-sql-statement.md) each time the statement is executed. The overhead involved in this process can be significant if many statements are executed dynamically, and it's wasteful if those statements are similar.

## Prepared execution

To address the above situation, dynamic SQL offers an optimized form of execution called prepared execution, which uses the following steps:

1. The program constructs an SQL statement in a buffer, just as it does for the EXECUTE IMMEDIATE statement. Instead of host variables, a question mark (?) can be substituted for a constant anywhere in the statement text to indicate that a value for the constant will be supplied later. The question mark is called as a parameter marker.

2. The program passes the SQL statement to the DBMS with a PREPARE statement, which requests that the DBMS parse, validate, and optimize the statement and generate an execution plan for it. The program then uses an EXECUTE statement (not an EXECUTE IMMEDIATE statement) to execute the PREPARE statement at a later time. It passes parameter values for the statement through a special data structure called the SQL Data Area or SQLDA.

3. The program can use the EXECUTE statement repeatedly, supplying different parameter values each time the dynamic statement is executed.

Prepared execution is still not the same as static SQL. In static SQL, the first four steps of processing an SQL statement take place at compile time. In prepared execution, these steps still take place at run time, but they're only done once. Execution of the plan takes place only when EXECUTE is called. This behavior helps eliminate some of the performance disadvantages inherent in the architecture of dynamic SQL.

## See also

[EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)  
[sp_executesql (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-executesql-transact-sql.md)  
