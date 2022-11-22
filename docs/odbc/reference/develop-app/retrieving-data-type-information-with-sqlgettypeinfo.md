---
description: "Retrieving Data Type Information with SQLGetTypeInfo"
title: "Retrieving Data Type Information with SQLGetTypeInfo | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL data types [ODBC], identifiers"
  - "SQLGetTypeInfo function [ODBC], retrieving data type information"
  - "retrieving data type information [ODBC]"
  - "type identifiers [ODBC], SQL"
  - "identifiers [ODBC], SQL type"
  - "SQL type identifiers [ODBC]"
ms.assetid: d4f8b152-ab9e-4d05-a720-d10a08a6df81
author: David-Engel
ms.author: v-davidengel
---
# Retrieving Data Type Information with SQLGetTypeInfo
Because the mappings from underlying SQL data types to ODBC type identifiers are approximate, ODBC provides a function (**SQLGetTypeInfo**) through which a driver can completely describe each SQL data type in the data source. This function returns a result set, each row of which describes the characteristics of a single data type, such as name, type identifier, precision, scale, and nullability.  
  
 This information generally is used by generic applications that allow the user to create and alter tables. Such applications call **SQLGetTypeInfo** to retrieve the data type information and then present some or all of it to the user. Such applications need to be aware of two things:  
  
-   More than one SQL data type can map to a single type identifier, which can make it difficult to determine which data type to use. To solve this, the result set is ordered first by type identifier and second by closeness to the type identifier's definition. In addition, data source-defined data types take precedence over user-defined data types. For example, suppose that a data source defines the INTEGER and COUNTER data types to be the same except that COUNTER is auto-incrementing. Suppose also that the user-defined type WHOLENUM is a synonym of INTEGER. Each of these types maps to SQL_INTEGER. In the **SQLGetTypeInfo** result set, INTEGER appears first, followed by WHOLENUM and then COUNTER. WHOLENUM appears after INTEGER because it is user-defined, but before COUNTER because it more closely matches the definition of the SQL_INTEGER type identifier.  
  
-   ODBC does not define data type names for use in **CREATE TABLE** and **ALTER TABLE** statements. Instead, the application should use the name returned in the TYPE_NAME column of the result set returned by **SQLGetTypeInfo**. The reason for this is that although most of SQL does not vary much across DBMSs, data type names vary tremendously. Rather than forcing drivers to parse SQL statements and replace standard data type names with DBMS-specific data type names, ODBC requires applications to use the DBMS-specific names in the first place.  
  
 Note that **SQLGetTypeInfo** does not necessarily describe all of the data types an application can encounter. In particular, result sets might contain data types not directly supported by the data source. For example, the data types of the columns in result sets returned by catalog functions are defined by ODBC and these data types might not be supported by the data source. To determine the characteristics of the data types in a result set, an application calls **SQLColAttribute**.
