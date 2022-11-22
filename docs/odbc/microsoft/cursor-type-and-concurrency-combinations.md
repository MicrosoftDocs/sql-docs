---
description: "Cursor Type and Concurrency Combinations"
title: "Cursor Type and Concurrency Combinations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC driver for Oracle [ODBC], concurrency options"
  - "cursors [ODBC], ODBC driver for Oracle"
  - "concurrency options [ODBC]"
  - "ODBC driver for Oracle [ODBC], cursor options"
ms.assetid: db63d610-f86f-4029-9d66-fed616c8a818
author: David-Engel
ms.author: v-davidengel
---
# Cursor Type and Concurrency Combinations
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 Cursor types control the functionality of the cursor provided to the user. Concurrency options control the updatability and locking behavior of a result set.  
  
|Cursor type|Concurrency (allowed values)|  
|-----------------|------------------------------------|  
|SQL_CURSOR_FORWARD_ONLY|SQL_CONCUR_READ_ONLY|  
|SQL_CURSOR_STATIC|SQL_CONCUR_READ_ONLY|  
|SQL_CURSOR_KEYSET_DRIVEN<sup>[1]</sup>|SQL_CONCUR_READ_ONLY SQL_CONCUR_LOCK<sup>[2]</sup> SQL_CONCUR_VALUES|  
  
 <sup>[1]</sup> See [Limitations of Using Keyset-Driven Cursors](../../odbc/microsoft/limitations-of-using-keyset-driven-cursors.md).  
  
 <sup>[2]</sup> SQL_CONCUR_LOCK is supported only when the SQL_AUTOCOMMIT connection option is set to SQL_AUTOCOMMIT_OFF.  
  
## See Also  
 [Connect Options](../../odbc/microsoft/connect-options.md)
