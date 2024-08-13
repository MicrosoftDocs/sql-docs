---
title: "Stored procedure parameter limitations"
description: "Stored procedure parameter limitations"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "stored procedures [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], stored procedures"
---
# Stored procedure parameter limitations

> [!IMPORTANT]  
> This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.

When running Oracle stored procedures that utilize 10 or more output parameters, the stored procedure call fails, resulting in an Access Violation or ActiveX Data Objects (ADO) error. This can occur when using the Microsoft ODBC Driver for Oracle with versions 8.0.4.0.0 and 8.0.4.0.4 of the Oracle client software.

To correct the problem, the Oracle client software must be upgraded to version 8.0.4.2.0 or higher. Contact Oracle Corporation for more information about the [patches](oracle-software-patches.md).

> [!NOTE]  
> This problem doesn't occur with the early release of Oracle client software version 8.0.3.0.0.
