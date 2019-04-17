---
title: "Reserved Keywords | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC function call reserved words [ODBC]"
  - "reserved keywords [ODBC]"
ms.assetid: 8eeede59-a828-44bf-866c-1ca9a77a2c5e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Reserved Keywords
The following words are reserved for use in ODBC function calls. These words do not constrain the minimum SQL grammar; however, to ensure compatibility with drivers that support the core SQL grammar, applications should avoid using any of these keywords. The #**define** value SQL_ODBC_KEYWORDS contains a comma-separated list of these keywords.  
  
|||  
|-|-|  
|ABSOLUTE|IS|  
|ACTION|ISOLATION|  
|ADA|JOIN|  
|ADD|KEY|  
|ALL|LANGUAGE|  
|ALLOCATE|LAST|  
|ALTER|LEADING|  
|AND|LEFT|  
|ANY|LEVEL|  
|ARE|LIKE|  
|AS|LOCAL|  
|ASC|LOWER|  
|ASSERTION|MATCH|  
|AT|MAX|  
|AUTHORIZATION|MIN|  
|AVG|MINUTE|  
|BEGIN|MODULE|  
|BETWEEN|MONTH|  
|BIT|NAMES|  
|BIT_LENGTH|NATIONAL|  
|BOTH|NATURAL|  
|BY|NCHAR|  
|CASCADE|NEXT|  
|CASCADED|NO|  
|CASE|NONE|  
|CAST|NOT|  
|CATALOG|NULL|  
|CHAR|NULLIF|  
|CHAR_LENGTH|NUMERIC|  
|CHARACTER|OCTET_LENGTH|  
|CHARACTER_LENGTH|OF|  
|CHECK|ON|  
|CLOSE|ONLY|  
|COALESCE|OPEN|  
|COLLATE|OPTION|  
|COLLATION|OR|  
|COLUMN|ORDER|  
|COMMIT|OUTER|  
|CONNECT|OUTPUT|  
|CONNECTION|OVERLAPS|  
|CONSTRAINT|PAD|  
|CONSTRAINTS|PARTIAL|  
|CONTINUE|PASCAL|  
|CONVERT|POSITION|  
|CORRESPONDING|PRECISION|  
|COUNT|PREPARE|  
|CREATE|PRESERVE|  
|CROSS|PRIMARY|  
|CURRENT|PRIOR|  
|CURRENT_DATE|PRIVILEGES|  
|CURRENT_TIME|PROCEDURE|  
|CURRENT_TIMESTAMP|PUBLIC|  
|CURRENT_USER|READ|  
|CURSOR|REAL|  
|DATE|REFERENCES|  
|DAY|RELATIVE|  
|DEALLOCATE|RESTRICT|  
|DEC|REVOKE|  
|DECIMAL|RIGHT|  
|DECLARE|ROLLBACK|  
|DEFAULT|ROWS|  
|DEFERRABLE|SCHEMA|  
|DEFERRED|SCROLL|  
|DELETE|SECOND|  
|DESC|SECTION|  
|DESCRIBE|SELECT|  
|DESCRIPTOR|SESSION|  
|DIAGNOSTICS|SESSION_USER|  
|DISCONNECT|SET|  
|DISTINCT|SIZE|  
|DOMAIN|SMALLINT|  
|DOUBLE|SOME|  
|DROP|SPACE|  
|ELSE|SQL|  
|END|SQLCA|  
|END-EXEC|SQLCODE|  
|ESCAPE|SQLERROR|  
|EXCEPT|SQLSTATE|  
|EXCEPTION|SQLWARNING|  
|EXEC|SUBSTRING|  
|EXECUTE|SUM|  
|EXISTS|SYSTEM_USER|  
|EXTERNAL|TABLE|  
|EXTRACT|TEMPORARY|  
|FALSE|THEN|  
|FETCH|TIME|  
|FIRST|TIMESTAMP|  
|FLOAT|TIMEZONE_HOUR|  
|FOR|TIMEZONE_MINUTE|  
|FOREIGN|TO|  
|FORTRAN|TRAILING|  
|FOUND|TRANSACTION|  
|FROM|TRANSLATE|  
|FULL|TRANSLATION|  
|GET|TRIM|  
|GLOBAL|TRUE|  
|GO|UNION|  
|GOTO|UNIQUE|  
|GRANT|UNKNOWN|  
|GROUP|UPDATE|  
|HAVING|UPPER|  
|HOUR|USAGE|  
|IDENTITY|USER|  
|IMMEDIATE|USING|  
|IN|VALUE|  
|INCLUDE|VALUES|  
|INDEX|VARCHAR|  
|INDICATOR|VARYING|  
|INITIALLY|VIEW|  
|INNER|WHEN|  
|INPUT|WHENEVER|  
|INSENSITIVE|WHERE|  
|INSERT|WITH|  
|INT|WORK|  
|INTEGER|WRITE|  
|INTERSECT|YEAR|  
|INTERVAL|ZONE|  
|INTO||
