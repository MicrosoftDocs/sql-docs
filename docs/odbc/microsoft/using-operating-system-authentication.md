---
description: "Using Operating System Authentication"
title: "Using Operating System Authentication | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC driver for Oracle [ODBC], authentication"
  - "authentication [ODBC]"
ms.assetid: 613daef7-3171-42d0-b7e3-3879280f864d
author: David-Engel
ms.author: v-davidengel
---
# Using Operating System Authentication
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 Oracle operating system authentication relies on the underlying operating system to control access to database accounts. Users need not enter a password when using this type of login.  
  
 To take advantage of this feature, specify "/" as the user ID and do not specify a password when connecting using any of the following connection APIs: [SQLBrowseConnect](../../odbc/microsoft/level-2-api-functions-odbc-driver-for-oracle.md), [SQLConnect](../../odbc/microsoft/core-level-api-functions-odbc-driver-for-oracle.md), or [SQLDriverConnect](../../odbc/microsoft/level-1-api-functions-odbc-driver-for-oracle.md).  
  
 Oracle databases use SQL*Net Authentication Services to authenticate users that are logged on. This service works well if users are logged into Oracle through SQLPlus; however, when the logged-in user is a service such as Internet Information Services, the authentication fails. This is a known limitation of SQL\*Net Authentication and produces the following error: "[Microsoft][ODBC driver for Oracle][Oracle]ORA-12641: TNS:authentication service failed to initialize."  
  
 You can correct this problem by editing the Sqlnet.ora file. This configuration file is usually stored in the Network\Admin subdirectory of the Oracle home directory. Add the following line to Sqlnet.ora:  
  
```  
SQLNET.AUTHENTICATION_SERVICES = (none)  
```
