---
description: "Escape Sequences in ODBC"
title: "Escape Sequences in ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "escape sequences [ODBC]"
  - "SQL statements [ODBC], escape sequences"
  - "escape sequences [ODBC], about escape sequences"
ms.assetid: cf229f21-6c38-4b5b-aca8-f1be0dfeb3d0
author: David-Engel
ms.author: v-davidengel
---
# Escape Sequences in ODBC
A number of language features, such as outer joins and scalar function calls, are commonly implemented by DBMSs. However, the syntaxes for these features tend to be DBMS-specific, even when standard syntaxes are defined by the various standards bodies. Because of this, ODBC defines escape sequences that contain standard syntaxes for the following language features:  
  
-   Date, time, timestamp, and datetime interval literals  
  
-   Scalar functions such as numeric, string, and data type conversion functions  
  
-   LIKE predicate escape character  
  
-   Outer joins  
  
-   Procedure calls  
  
 The escape sequence used by ODBC is as follows:  
  
```  
  
(extension)  
  
```  
  
## Remarks  
 The escape sequence is recognized and parsed by drivers, which replace the escape sequences with DBMS-specific grammar. For more information about escape sequence syntax, see [ODBC Escape Sequences](../../../odbc/reference/appendixes/odbc-escape-sequences.md) in Appendix C: SQL Grammar.  
  
> [!NOTE]  
>  In ODBC 2.*x*, this was the standard syntax of the escape sequence:            **--(\*vendor(**_vendor-name_**), product(**_product-name_**)**_extension_ **\*)--**  
>   
>  In addition to this syntax, a shorthand syntax was defined of the form:            **{**_extension_**}**  
>   
>  In ODBC 3.*x*, the long form of the escape sequence has been deprecated, and the shorthand form is used exclusively.  
  
 Because the escape sequences are mapped by the driver to DBMS-specific syntaxes, an application can use either the escape sequence or DBMS-specific syntax. However, applications that use the DBMS-specific syntax will not be interoperable. When using the escape sequence, applications should make sure that the SQL_ATTR_NOSCAN statement attribute is turned off, which it is by default. Otherwise, the escape sequence will be sent directly to the data source, where it will generally cause a syntax error.  
  
 Drivers support only those escape sequences that they can map to underlying language features. For example, if the data source does not support outer joins, neither will the driver. To determine which escape sequences are supported, an application calls **SQLGetTypeInfo** and **SQLGetInfo**. For more information, see the next section, [Date, Time, and Timestamp Literals](../../../odbc/reference/develop-app/date-time-and-timestamp-literals.md).  
  
 This section contains the following topics.  
  
-   [Date, Time, and Timestamp Literals](../../../odbc/reference/develop-app/date-time-and-timestamp-literals.md)  
  
-   [Scalar Function Calls](../../../odbc/reference/develop-app/scalar-function-calls.md)  
  
-   [LIKE Predicate Escape Character](../../../odbc/reference/develop-app/like-predicate-escape-character.md)  
  
-   [Outer Joins](../../../odbc/reference/develop-app/outer-joins.md)  
  
-   [Procedure Calls](../../../odbc/reference/develop-app/procedure-calls.md)
