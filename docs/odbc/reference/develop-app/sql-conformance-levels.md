---
description: "SQL Conformance Levels"
title: "SQL Conformance Levels | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "conformance levels [ODBC], SQL"
  - "SQL conformance levels [ODBC]"
  - "data sources [ODBC], conformance levels"
  - "ODBC drivers [ODBC], conformance levels"
ms.assetid: 3529df2c-a09b-4c16-9c60-eae7a06d903a
author: David-Engel
ms.author: v-davidengel
---
# SQL Conformance Levels
The level of SQL-92 grammar supported by a driver is indicated by the value returned by a call to **SQLGetInfo** with the SQL_SQL_CONFORMANCE information type. This indicates whether the driver conforms to the Entry, FIPS Transitional, Intermediate, or Full levels defined in SQL-92.  
  
 All ODBC drivers must support the minimum SQL grammar described in [SQL Minimum Grammar](../../../odbc/reference/appendixes/sql-minimum-grammar.md) in Appendix C: SQL Grammar. This grammar is a subset of the Entry level of SQL-92. Drivers may support additional SQL and be conformant to the SQL-92 Entry, Intermediate, or Full level, or to the FIPS 127-2 Transitional level. Drivers that comply to a given level of SQL-92 or FIPS 127-2 can support additional features in any of the higher levels yet not be fully conformant to that level. To determine whether a feature is supported, an application should call **SQLGetInfo** with the appropriate information type. The conformance level of an SQL feature is described in the corresponding information type. (See the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description.)
