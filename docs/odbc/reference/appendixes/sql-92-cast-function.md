---
title: "SQL-92 CAST Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "functions [ODBC], SQL-92 functions"
  - "SQL-92 functions [ODBC]"
  - "CAST function [ODBC]"
ms.assetid: 982f09e5-8205-41b9-98b3-8f898e24743f
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL-92 CAST Function
The **CAST** function defined in SQL-92 is equivalent to the **CONVERT** function defined in ODBC. The syntax of the equivalent functions is as follows:  
  
```  
{ fn CONVERT (value-exp, data-type) } /* ODBC  
CAST (value-exp AS data-type) /* SQL92  
```  
  
 The SQL-92 **CAST** function mandates which data types can be converted to which other data types. (For more information, see the SQL-92 specification.) The **CAST** function is supported at the FIPS Transitional level.  
  
 An application can determine support for the **CAST** function as follows:  
  
1.  Call **SQLGetInfo** with the SQL_SQL_CONFORMANCE information type. If the return value for the information type is SQL_SC_FIPS127_2_TRANSITIONAL, SQL_SC_SQL92_INTERMEDIATE, or SQL_SC_SQL92_FULL, the **CAST** function is supported.  
  
2.  If the return value of the SQL_SQL_CONFORMANCE information type is SQL_SC_ENTRY_LEVEL or 0, call **SQLGetInfo** with the SQL_SQL92_VALUE_EXPRESSIONS information type. If the SQL_SVE_CAST bit is set, the **CAST** function is supported.
