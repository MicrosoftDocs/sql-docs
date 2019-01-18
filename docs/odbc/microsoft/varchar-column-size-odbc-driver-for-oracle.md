---
title: "VARCHAR Column Size (ODBC Driver for Oracle) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [ODBC], ODBC driver for Oracle"
  - "varchar column size [ODBC]"
  - "ODBC driver for Oracle [ODBC], data types"
ms.assetid: eb4cb410-3d00-4251-8c5e-a06f36c4dac7
author: MightyPen
ms.author: genemi
manager: craigg
---
# VARCHAR Column Size (ODBC Driver for Oracle)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 In Oracle8, the maximum size of a VARCHAR column has increased from 2000 to 4000 bytes. The Oracle 7.3.x client software has no way to bind a parameter value larger than 2000 bytes. Therefore, if you create a table with a VARCHAR column of larger than 2000 bytes, you will be unable to perform parameterized inserts, updates, deletes, and queries against it with data that exceeds the 2000-byte limit of the client software. Because both the ODBC Driver for Oracle and the OLE DB Provider for Oracle use parameterized inserts, updates, deletes, and queries, they will report ORA-01026 errors in this case. Data that is within the limits enforced by the Oracle client software will work. To avoid this 2000-byte limit, you must upgrade your client software to Oracle8 (8.0.4.1.1c or higher).
