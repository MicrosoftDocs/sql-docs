---
description: "Mapping Data Types (ODBC Driver for Oracle)"
title: "Mapping Data Types (ODBC Driver for Oracle) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "mapping data types [ODBC]"
  - "data types [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], data types"
ms.assetid: a5d9ce12-19da-4943-8493-e3d56fa08348
author: David-Engel
ms.author: v-davidengel
---
# Mapping Data Types (ODBC Driver for Oracle)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 The Oracle Server supports a set of data types. The ODBC Driver for Oracle maps these data types to their appropriate ODBC SQL data types. The following table lists the Oracle 7.3 Server data types and their corresponding ODBC SQL data types.  
  
 The ODBC Driver for Oracle supports Oracle 7.3 and some Oracle8 data types. For more information about supported Oracle8 data types, see [Supported Data Types](../../odbc/microsoft/supported-data-types-odbc-driver-for-oracle.md).  
  
|Oracle Server data type|ODBC SQL data type|  
|-----------------------------|------------------------|  
|CHAR|SQL_CHAR|  
|DATE|SQL_TIMESTAMP|  
|FLOAT|SQL_DOUBLE|  
|INTEGER|SQL_DECIMAL|  
|LONG|SQL_LONGVARCHAR|  
|LONG RAW|SQL_LONGVARBINARY|  
|NUMBER|SQL_DECIMAL|  
|RAW|SQL_VARBINARY|  
|VARCHAR2|SQL_VARCHAR|  
  
> [!NOTE]  
>  For more information about the allowable size of the VARCHAR column, see [VARCHAR Column Size](../../odbc/microsoft/varchar-column-size-odbc-driver-for-oracle.md) in this guide.
