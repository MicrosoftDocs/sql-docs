---
title: "Appendix D: Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "C data types [ODBC], defined"
  - "SQL data types [ODBC], defined"
  - "data types [ODBC]"
  - "data types [ODBC], about data types"
ms.assetid: 981d49c3-3531-4543-aa75-5bd9e4f67000
author: MightyPen
ms.author: genemi
manager: craigg
---
# Appendix D: Data Types
ODBC defines two sets of data types: SQL data types and C data types. SQL data types indicate the data type of data stored at the data source. C data types indicate the data type of data stored in application buffers.  
  
 SQL data types are defined by each DBMS in accordance with the SQL-92 standard. For each SQL data type specified in the SQL-92 standard, ODBC defines a type identifier, which is a **#define** value that is passed as an argument in ODBC functions or returned in the metadata of a result set. The only SQL-92 data types not supported by ODBC are BIT (the ODBC SQL_BIT type has different characteristics), BIT_VARYING, TIME_WITH_TIMEZONE, TIMESTAMP_WITH_TIMEZONE, and NATIONAL_CHARACTER. Drivers are responsible for mapping data source-specific SQL data types to ODBC SQL data type identifiers and driver-specific SQL data type identifiers. The SQL data type is specified in the SQL_DESC_CONCISE_TYPE field of an implementation descriptor.  
  
 ODBC defines the C data types and their corresponding ODBC type identifiers. An application specifies the C data type of the buffer that will receive result set data by passing the appropriate C type identifier in the *TargetType* argument in a call to **SQLBindCol** or **SQLGetData**. It specifies the C type of the buffer containing a statement parameter by passing the appropriate C type identifier in the *ValueType* argument in a call to **SQLBindParameter**. The C data type is specified in the SQL_DESC_CONCISE_TYPE field of an application descriptor.  
  
> [!NOTE]  
>  There are no driver-specific C data types.  
  
 Each SQL data type corresponds to an ODBC C data type. Before returning data from the data source, the driver converts it to the specified C data type. Before sending data to the data source, the driver converts it from the specified C data type.  
  
 This appendix contains the following topics.  
  
-   [Using Data Type Identifiers](../../../odbc/reference/appendixes/using-data-type-identifiers.md)  
  
-   [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md)  
  
-   [C Data Types](../../../odbc/reference/appendixes/c-data-types.md)  
  
-   [Data Type Identifiers and Descriptors](../../../odbc/reference/appendixes/data-type-identifiers-and-descriptors.md)  
  
-   [Pseudo-Type Identifiers](../../../odbc/reference/appendixes/pseudo-type-identifiers.md)  
  
-   [Transferring Data in Its Binary Form](../../../odbc/reference/appendixes/transferring-data-in-its-binary-form.md)  
  
-   [Guidelines for Interval and Numeric Data Types](../../../odbc/reference/appendixes/guidelines-for-interval-and-numeric-data-types.md)  
  
-   [Constraints of the Gregorian Calendar](../../../odbc/reference/appendixes/constraints-of-the-gregorian-calendar.md)  
  
-   [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md)  
  
-   [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md)  
  
-   [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md)  
  
 For an explanation of ODBC data types, see [Data Types in ODBC](../../../odbc/reference/develop-app/data-types-in-odbc.md). For information about driver-specific SQL data types, see the driver's documentation.
