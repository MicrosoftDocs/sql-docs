---
title: "Supported Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [ODBC], DBMS support"
  - "interoperability [ODBC], data types"
ms.assetid: a89d4bab-ef3c-45c2-aa72-2639b3e0f856
author: MightyPen
ms.author: genemi
manager: craigg
---
# Supported Data Types
The data types supported by DBMSs vary considerably. An application can determine the names and characteristics of supported data types by calling **SQLGetTypeInfo**. Because of wide variation in data type names, the application must use the data type names returned by **SQLGetTypeInfo** in **CREATE TABLE** statements. For more information, see [Data Types in ODBC](../../../odbc/reference/develop-app/data-types-in-odbc.md).
