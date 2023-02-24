---
description: "Implementing SQLGetDiagRec and SQLGetDiagField"
title: "Implementing SQLGetDiagRec and SQLGetDiagField | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], SqlGetDiagField"
  - "SQLGetDiagField function [ODBC], and SQLGetDiagRec"
  - "SQLGetDiagRec function [ODBC], and SQLGetDiagField"
  - "diagnostic information [ODBC], SqlGetDiagRec"
  - "retrieving diagnostic information [ODBC]"
ms.assetid: 11ba1857-b533-4517-8131-a2a8a0154a0a
author: David-Engel
ms.author: v-davidengel
---
# Implementing SQLGetDiagRec and SQLGetDiagField
**SQLGetDiagRec** and **SQLGetDiagField** are implemented by the Driver Manager and each driver. The Driver Manager and each driver maintain diagnostic records for each environment, connection, statement, and descriptor handle, and free those records only when another function is called with that handle or the handle is freed.  
  
 Although both the Driver Manager and each driver must determine the first status record according to the rankings in [Sequence of Status Records](../../../odbc/reference/develop-app/sequence-of-status-records.md), the Driver Manager determines the final sequence of records.  
  
 **SQLGetDiagRec** and **SQLGetDiagField** do not post diagnostic records about themselves.  
  
 This section contains the following topics.  
  
-   [Diagnostic Handling Rules](../../../odbc/reference/develop-app/diagnostic-handling-rules.md)  
  
-   [Role of the Driver Manager](../../../odbc/reference/develop-app/role-of-the-driver-manager.md)  
  
-   [Role of the Driver](../../../odbc/reference/develop-app/role-of-the-driver.md)
