---
description: "Choosing an SQL Grammar"
title: "Choosing an SQL Grammar | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], interoperability"
  - "interoperability of SQL statements [ODBC], SQL grammar"
  - "SQL grammar [ODBC], selecting"
ms.assetid: 4e0d189b-e407-47e0-92a9-f9982230dd0e
author: David-Engel
ms.author: v-davidengel
---
# Choosing an SQL Grammar
The first decision to make when constructing SQL statements is which grammar to use. In addition to the grammars available from the various standards bodies, such as Open Group, ANSI, and ISO, virtually every DBMS vendor defines its own grammar, each of which varies slightly from the standard.  
  
 [Appendix C: SQL Grammar](../../../odbc/reference/appendixes/appendix-c-sql-grammar.md), describes the minimum SQL grammar that all ODBC drivers must support. This grammar is a subset of the Entry level of SQL-92. Drivers may support additional grammar to conform to the Intermediate, Full, or FIPS 127-2 Transitional levels defined by SQL-92. For more information, see [SQL Minimum Grammar](../../../odbc/reference/appendixes/sql-minimum-grammar.md) in Appendix C: SQL Grammar, and SQL-92.  
  
 Appendix C also defines *escape sequences* containing standard grammar for commonly available language features, such as outer joins, that are not covered by the SQL-92 grammar. For more information, see [ODBC Escape Sequences](../../../odbc/reference/appendixes/odbc-escape-sequences.md) in Appendix C: SQL Grammar, and [Escape Sequences](../../../odbc/reference/develop-app/escape-sequences.md), later in this section.  
  
 The grammar that is chosen affects how the driver processes the statement. Drivers must modify SQL-92 SQL and the ODBC-defined escape sequences to DBMS-specific SQL. Because most SQL grammars are based on one or more of the various standards, most drivers do little or no work to meet this requirement. It often consists only of searching for the escape sequences defined by ODBC and replacing them with DBMS-specific grammar. When a driver encounters grammar it does not recognize, it assumes the grammar is DBMS-specific and passes the SQL statement without modification to the data source for execution.  
  
 Therefore, there are really two choices of grammar to use: the SQL-92 grammar (and the ODBC escape sequences) and a DBMS-specific grammar. Of the two, only the SQL-92 grammar is interoperable, so all interoperable applications should use it. Applications that are not interoperable can use the SQL-92 grammar or a DBMS-specific grammar. DBMS-specific grammars have two advantages: They can exploit any features not covered by SQL-92, and they are marginally faster because the driver does not have to modify them. The latter feature can be partially enforced by setting the SQL_ATTR_NOSCAN statement attribute, which stops the driver from searching for and replacing escape sequences.  
  
 If the SQL-92 grammar is used, the application can discover how it is modified by the driver by calling **SQLNativeSql**. This is often useful when debugging applications. **SQLNativeSql** accepts an SQL statement and returns it after the driver has modified it. Because this function is in the Core interface conformance level, it is supported by all drivers.
