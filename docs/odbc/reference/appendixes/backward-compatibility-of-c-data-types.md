---
description: "Backward Compatibility of C Data Types"
title: "Backward Compatibility of C Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "backward compatibility [ODBC], C data types"
  - "compatibility [ODBC], C data types"
  - "data types [ODBC], backward compatibility"
  - "C data types [ODBC], backward compatibility"
ms.assetid: b1453a65-ae03-4061-b0cf-a8434d8bc40b
author: David-Engel
ms.author: v-davidengel
---
# Backward Compatibility of C Data Types
SQL_C_SHORT, SQL_C_LONG, and SQL_C_TINYINT have been replaced in ODBC by signed and unsigned types: SQL_C_SSHORT and SQL_C_USHORT, SQL_C_SLONG and SQL_C_ULONG, and SQL_C_STINYINT and SQL_C_UTINYINT. An ODBC *3.x* driver that should work with ODBC *2.x* applications should support SQL_C_SHORT, SQL_C_LONG, and SQL_C_TINYINT, because when they are called, the Driver Manager passes them through to the driver.
