---
description: "Role of the Driver"
title: "Role of the Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver error checking [ODBC]"
  - "diagnostic information [ODBC], driver error checking"
ms.assetid: cac64c24-a27d-4884-96c0-ea7988351711
author: David-Engel
ms.author: v-davidengel
---
# Role of the Driver
The driver checks for all errors and warnings not checked by the Driver Manager and orders status records that it generates. (An ODBC 2.*x* driver does not order status records.) This includes errors and warnings in data truncation, data conversion, syntax, and some state transitions. The driver might also check errors and warnings partially checked by the Driver Manager. For example, although the Driver Manager checks whether the value of *Operation* in **SQLSetPos** is legal, the driver must check whether it is supported.  
  
 The driver also maps *native errors* - that is, errors returned by the data source - to SQLSTATEs. For example, the driver might map a number of different native errors for illegal SQL syntax to SQLSTATE 42000 (Syntax error or access violation). The driver returns the native error number in the SQL_DIAG_NATIVE field of the status record. Driver documentation should show how errors and warnings are mapped from the data source to arguments in **SQLGetDiagRec** and **SQLGetDiagField**.
