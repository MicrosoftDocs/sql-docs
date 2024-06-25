---
title: "Setting the Date Format on Connection"
description: "Setting the Date Format on Connection"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "date formats [ODBC]"
  - "ODBC driver for Oracle [ODBC], date formats"
---
# Setting the Date Format on Connection
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 The new version of the Microsoft ODBC Driver for Oracle does not automatically set the date format for Oracle date fields. Previously when the driver connected, it used `ALTER SESSION SET NLS_DATE_FORMAT ='YYYY-MM-DD HH:MI:SS'`.  
  
 To set the date format, call ALTER SESSION SET and then perform the insert. For example:  
  
```  
conn.Execute "ALTER SESSION SET NLS_DATE_FORMAT = 'YYYY-MM-DD HH:MI:SS' "  
sSql = "INSERT INTO DATETEST VALUES (24,'1988-12-01 10:23:03')"  
conn.Execute sSql  
```
