---
title: "Supported Data Types"
description: "Supported Data Types"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "data types [ODBC], DBMS support"
  - "interoperability [ODBC], data types"
---
# Supported Data Types
The data types supported by DBMSs vary considerably. An application can determine the names and characteristics of supported data types by calling **SQLGetTypeInfo**. Because of wide variation in data type names, the application must use the data type names returned by **SQLGetTypeInfo** in **CREATE TABLE** statements. For more information, see [Data Types in ODBC](../../../odbc/reference/develop-app/data-types-in-odbc.md).
