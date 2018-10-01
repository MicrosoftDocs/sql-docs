---
title: "Embedded SQL | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL [ODBC], embedded SQL"
  - "sending SQL statements to DBMS [ODBC]"
  - "SQL statements [ODBC], embedded SQL"
  - "ODBC [ODBC], SQL"
  - "embedded SQL [ODBC]"
ms.assetid: 8eee3527-f225-4aa2-bd18-a16bd3ab0fb7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Embedded SQL
The first technique for sending SQL statements to the DBMS is embedded SQL. Because SQL does not use variables and control-of-flow statements, it is often used as a database sublanguage that can be added to a program written in a conventional programming language, such as C or COBOL. This is a central idea of embedded SQL: placing SQL statements in a program written in a host programming language. Briefly, the following techniques are used to embed SQL statements in a host language:  
  
-   Embedded SQL statements are processed by a special SQL precompiler. All SQL statements begin with an introducer and end with a terminator, both of which flag the SQL statement for the precompiler. The introducer and terminator vary with the host language. For example, the introducer is "EXEC SQL" in C and "&SQL(" in MUMPS, and the terminator is a semicolon (;) in C and a right parenthesis in MUMPS.  
  
-   Variables from the application program, called host variables, can be used in embedded SQL statements wherever constants are allowed. These can be used on input to tailor an SQL statement to a particular situation and on output to receive the results of a query.  
  
-   Queries that return a single row of data are handled with a singleton SELECT statement; this statement specifies both the query and the host variables in which to return data.  
  
-   Queries that return multiple rows of data are handled with cursors. A cursor keeps track of the current row within a result set. The DECLARE CURSOR statement defines the query, the OPEN statement begins the query processing, the FETCH statement retrieves successive rows of data, and the CLOSE statement ends query processing.  
  
-   While a cursor is open, positioned update and positioned delete statements can be used to update or delete the row currently selected by the cursor.  
  
 This section contains the following topics.  
  
-   [Embedded SQL Example](../../odbc/reference/embedded-sql-example.md)  
  
-   [Compiling an Embedded SQL Program](../../odbc/reference/compiling-an-embedded-sql-program.md)  
  
-   [Static SQL](../../odbc/reference/static-sql.md)  
  
-   [Dynamic SQL](../../odbc/reference/dynamic-sql.md)
