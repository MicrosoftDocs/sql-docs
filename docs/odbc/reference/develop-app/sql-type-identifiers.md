---
description: "SQL Type Identifiers"
title: "SQL Type Identifiers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [ODBC], identifiers"
  - "SQL data types [ODBC], identifiers"
  - "type identifiers [ODBC], SQL"
  - "identifiers [ODBC], SQL type"
  - "SQL type identifiers [ODBC]"
ms.assetid: 22f6793b-2f43-4281-b35a-28f48e504dd8
author: David-Engel
ms.author: v-davidengel
---
# SQL Type Identifiers
Each data source defines its own SQL data types. ODBC defines type identifiers and describes the general characteristics of the SQL data types that might be mapped to each type identifier. It is driver-specific how each data type in the underlying data source is mapped to an SQL type identifier of ODBC.  
  
 For example, SQL_CHAR is the type identifier for a character column with a fixed length, typically between 1 and 254 characters. These characteristics correspond to the CHAR data type found in many SQL data sources. Thus, when an application discovers that the type identifier for a column is SQL_CHAR, it can assume it is probably dealing with a CHAR column. However, it should still check the byte length of the column before assuming it is between 1 and 254 characters; the driver for a non-SQL data source, for example, might map a fixed-length character column of 500 characters to SQL_CHAR or SQL_LONGVARCHAR, because neither is an exact match.  
  
 ODBC defines a wide variety of SQL type identifiers. However, the driver is not required to use all of these identifiers. Instead, it uses only those identifiers it needs to expose the SQL data types supported by the underlying data source. If the underlying data source supports SQL data types to which no type identifier corresponds, the driver can define additional type identifiers. For more information, see [Driver-Specific Data Types, Descriptor Types, Information Types, Diagnostic Types, and Attributes](../../../odbc/reference/develop-app/driver-specific-data-types-descriptor-information-diagnostic.md).  
  
 For a complete description of SQL type identifiers, see [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) in Appendix D: Data Types.
