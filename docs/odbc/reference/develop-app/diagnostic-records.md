---
title: "Diagnostic Records | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], diagnostic records"
  - "handles [ODBC], diagnostic records"
  - "header records [ODBC]"
  - "status records [ODBC]"
  - "diagnostic records [ODBC]"
ms.assetid: 92c73f9b-3ed7-410d-9cec-2771004aae60
author: MightyPen
ms.author: genemi
manager: craigg
---
# Diagnostic Records
Associated with each environment, connection, statement, and descriptor handle are *diagnostic records*. These records contain diagnostic information about the last function called that used a particular handle. The records are replaced only when another function is called using that handle. There is no limit to the number of diagnostic records that can be stored at any one time.  
  
 There are two types of diagnostic records: a *header record* and zero or more *status records*. The header record is record 0; the status records are records 1 and above. Diagnostic records are composed of a number of separate fields, which are different for the header record and the status records. In addition, ODBC components can define their own diagnostic record fields.  
  
 Although diagnostic records can be thought of as structures, there is no requirement for them to actually be structures; how a driver stores the diagnostic information is driver-specific.  
  
 Fields in diagnostic records are retrieved with **SQLGetDiagField**. The SQLSTATE, native error number, and diagnostic message fields of status records can be retrieved in a single call with **SQLGetDiagRec**.  
  
 This section contains the following topics.  
  
-   [Header Record](../../../odbc/reference/develop-app/header-record.md)  
  
-   [Status Records](../../../odbc/reference/develop-app/status-records.md)
