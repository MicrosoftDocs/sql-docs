---
description: "Appendix E: Scalar Functions"
title: "Appendix E: Scalar Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQL-92 functions [ODBC]"
  - "scalar functions [ODBC]"
  - "functions [ODBC], scalar"
ms.assetid: 59c7cd5e-32d6-43ab-bac3-7010322d105a
author: David-Engel
ms.author: v-davidengel
---
# Appendix E: Scalar Functions
ODBC specifies the following types of scalar functions, with detailed information about each of these function types provided in the corresponding sections of this appendix. The function descriptions include associated syntax.  
  
 This appendix contains the following topics.  
  
-   [String Functions](../../../odbc/reference/appendixes/string-functions.md)  
  
-   [Numeric Functions](../../../odbc/reference/appendixes/numeric-functions.md)  
  
-   [Time, Date, and Interval Functions](../../../odbc/reference/appendixes/time-date-and-interval-functions.md)  
  
-   [System Functions](../../../odbc/reference/appendixes/system-functions.md)  
  
-   [Explicit Data Type Conversion Function](../../../odbc/reference/appendixes/explicit-data-type-conversion-function.md)  
  
-   [SQL-92 CAST Function](../../../odbc/reference/appendixes/sql-92-cast-function.md)  
  
 ODBC does not mandate a data type for return values from scalar functions because the functions are often data source-specific. Applications should use the CONVERT scalar function whenever possible to force data type conversion.  
  
## ODBC and SQL-92 Scalar Functions  
 The tables in this appendix include functions that have been added in ODBC 3.0 to align with SQL-92. Those functions added for a particular type of scalar function, as defined in ODBC, are indicated in each section.  
  
 ODBC and SQL-92 classify their scalar functions differently. ODBC classifies scalar functions by argument type; SQL-92 classifies them by return value. For example, the EXTRACT function is classified as a timedate function by ODBC, because the extract-field argument is a datetime keyword and the extract-source argument is a datetime or interval expression. SQL-92, on the other hand, classifies EXTRACT as a numeric scalar function, because the return value is a numeric.  
  
 An application can determine which scalar functions a driver supports by calling **SQLGetInfo**. Information types are included both for ODBC and for SQL-92 classifications of scalar functions. Because these classifications are different, the support for some scalar functions may be indicated in information types that do not correspond to ODBC and SQL-92. For example, support for EXTRACT in ODBC is indicated by the SQL_TIMEDATE_FUNCTIONS information type; support for EXTRACT in SQL-92, on the other hand, is indicated by the SQL_SQL92_NUMERIC_VALUE_FUNCTIONS information type.
