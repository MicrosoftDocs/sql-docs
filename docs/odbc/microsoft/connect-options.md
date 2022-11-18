---
description: "Connect Options"
title: "Connect Options | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connection options [ODBC]"
  - "ODBC driver for Oracle [ODBC], connection options"
  - "custom connection options [ODBC]"
ms.assetid: abfdc133-cb33-435f-a467-fbe15444f687
author: David-Engel
ms.author: v-davidengel
---
# Connect Options
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 These options allow customization of the database connection within an application.  
  
|Connect option|Notes|  
|--------------------|-----------|  
|SQL_AUTOCOMMIT|If you choose SQL_AUTOCOMMIT_OFF, your application must explicitly commit or roll back transactions with [SQLTransact](../../odbc/microsoft/core-level-api-functions-odbc-driver-for-oracle.md).|  
|SQL_ODBC_CURSORS|This connection attribute is implemented in the Driver Manager.|  
|SQL_OPT_TRACE|This connection attribute is implemented in the Driver Manager.|  
|SQL_OPT_TRACEFILE|This connection attribute is implemented in the Driver Manager.|  
|SQL_TRANSLATE_DLL|Returns error: "Driver not capable."|  
|SQL_TRANSLATE_OPTION|A 32-bit value passed to the translation .dll.|  
|SQL_TXN_ISOLATION|The driver allows only SQL_TXN_READ_COMMITTED.<br /><br /> The following vParams are not supported:<br /><br /> SQL_TXN_READ_UNCOMMITTED<br /><br /> SQL_TXN_REAPEATABLE_READ<br /><br /> SQL_TXN_SERIALIZABLE|  
|SQL_ATTR_ENLIST_IN_DTC|This ODBC 3.0 connection attribute allows you to use the ODBC Driver for Oracle in distributed transactions coordinated by Microsoft Component Services (or MTS, if you are using Windows NT). It provides the interface pointer *pITransaction* to the transaction as the *vParam* argument.|  
|SQL_ATTR_CONNECTION_DEAD|This read-only ODBC 3.5 connection attribute allows you to determine whether the connection to the Oracle server has failed. Get only; cannot Set.|
