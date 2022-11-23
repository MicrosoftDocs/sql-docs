---
description: "Driver-Specific Data Types, Descriptor Types, Information Types, Diagnostic Types, and Attributes"
title: "Driver-Specific Types - Data, Descriptor, Information, Diagnostic | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver-specific diagnostic values [ODBC]"
  - "diagnostic information [ODBC], driver-specific values"
  - "ODBC drivers [ODBC], driver-specific diagnostic values"
ms.assetid: ad4c76d3-5191-4262-b47c-5dd1d19d1154
author: David-Engel
ms.author: v-davidengel
---
# Driver-Specific Data Types, Descriptor Types, Information Types, Diagnostic Types, and Attributes
Drivers can allocate driver-specific values for the following:  
  
-   **SQL Data Type Indicators** These are used in *ParameterType* in **SQLBindParameter** and in *DataType* in **SQLGetTypeInfo** and returned by **SQLColAttribute**, **SQLColumns**, **SQLDescribeCol**, **SQLGetTypeInfo**, **SQLDescribeParam**, **SQLProcedureColumns**, and **SQLSpecialColumns**.  
  
-   **Descriptor Fields** These are used in *FieldIdentifier* in **SQLColAttribute**, **SQLGetDescField**, and **SQLSetDescField**.  
  
-   **Diagnostic Fields** These are used in *DiagIdentifier* in **SQLGetDiagField** and **SQLGetDiagRec**.  
  
-   **Information Types** These are used in *InfoType* in **SQLGetInfo**.  
  
-   **Connection and Statement Attributes** These are used in *Attribute* in **SQLGetConnectAttr**, **SQLGetStmtAttr**, **SQLSetConnectAttr**, and **SQLSetStmtAttr**.  
  
 For each of these items, there are two sets of values: values reserved for use by ODBC, and values reserved for use by drivers. Before implementing driver-specific values, a driver writer must request a value for each driver-specific type, field, or attribute from Open Group. For new driver development, use the range described in the table below. The ODBC 3.8 Driver Manager will not generate an error if an unknown value is used that is not in the range described below. However, later versions of the Driver Manager might generate an error if unknown values are received that are not in the range.  
  
 When any of these values is passed to an ODBC function, the driver must check whether the value is valid. Drivers return SQLSTATE HYC00 (Optional feature not implemented) for driver-specific values that apply to other drivers.  
  
 Starting with ODBC 3.8, driver writers can allocate driver-specific attributes within a reserved range.  
  
> [!NOTE]  
>  The ODBC 3.8 Driver Manager neither validates nor enforces these ranges for backward compatibility. A future version of the Driver Manager might enforce them, however.  
  
|Attribute type|ODBC data type|Driver-specific range base|Driver-specific range limit|ODBC constant for driver-specific value range base|  
|--------------------|--------------------|---------------------------------|----------------------------------|---------------------------------------------------------|  
|SQL data type indicators|SQLSMALLINT|0x4000|0x7FFF|SQL_DRIVER_SQL_TYPE_BASE|  
|Descriptor fields|SQLSMALLINT|0x4000|0x7FFF|SQL_DRIVER_DESCRIPTOR_BASE|  
|Diagnostic fields|SQLSMALLINT|0x4000|0x7FFF|SQL_DRIVER_DIAGNOSTIC_BASE|  
|Information types|SQLUSMALLINT|0x4000|0x7FFF|SQL_DRIVER_INFO_TYPE_BASE|  
|Connection attributes|SQLINTEGER|0x00004000|0x00007FFF|SQL_DRIVER_CONNECT_ATTR_BASE|  
|Statement attributes|SQLINTEGER|0x00004000|0x00007FFF|SQL_DRIVER_STATEMENT_ATTR_BASE|  
  
> [!NOTE]  
>  Driver-specific data types, descriptor fields, diagnostic fields, information types, statement attributes, and connection attributes must be described in the driver documentation. When any of these values is passed to an ODBC function, the driver must check whether the value is valid. Drivers return SQLSTATE HYC00 (Optional feature not implemented) for driver-specific values that apply to other drivers.  
  
 The base values are defined to facilitate driver development. For example, driver specific diagnostic attributes can be defined in the following format:  
  
```  
SQL_DRIVER_DIAGNOSTIC_BASE+0, SQL_DRIVER_DIAGNOSTIC_BASE +1  
```
