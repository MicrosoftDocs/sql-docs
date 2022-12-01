---
description: "SQLGetConnectOption (Visual FoxPro ODBC Driver)"
title: "SQLGetConnectOption (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLGetConnectOption function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 5703eb39-f3b2-4f3a-8676-a5625ae29a41
author: David-Engel
ms.author: v-davidengel
---
# SQLGetConnectOption (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Partial  
  
 ODBC API Conformance: Level 1  
  
 Returns the current setting of a connection option. This function is partially supported: The driver supports all values for the *fOption* argument but does not support some of *vParam* values for the *fOption* argument SQL_TXN_ISOLATION.  
  
 The following table describes only those arguments with behavior specific to the Visual FoxPro ODBC Driver implementation of **SQLGetConnectOption**.  
  
|*fOption*|Remarks|  
|---------------|-------------|  
|SQL_AUTOCOMMIT|If you choose SQL_AUTOCOMMIT_OFF, your application must explicitly commit or roll back transactions with [SQLTransact](../../odbc/microsoft/sqltransact-visual-foxpro-odbc-driver.md); the Visual FoxPro ODBC Driver does not automatically commit a transactable statement upon completion. The driver does begin a transaction if the statement is transactable.|  
|SQL_CURRENT_QUALIFIER|Can be a fully qualified database (.dbc file) name or fully qualified path to a directory containing zero or more tables (.dbf files).|  
|SQL_LOGINTIMEOUT|Returns "Driver Not Capable" error.|  
|SQL_CURSORS|Returns "Driver Not Capable" error.|  
|SQL_PACKET_SIZE|Returns "Driver Not Capable" error.|  
|SQL_TXN_ISOLATION|The driver allows only SQL_TXN_READ_COMMITTED.<br /><br /> The following *vParam*s are not supported:<br /><br /> SQL_TXN_READ_UNCOMMITTED<br /><br /> SQL_TXN_REAPEATABLE_READ<br /><br /> SQL_TXN_SERIALIZABLE|  
  
 For more information, see [SQLGetConnectOption](../../odbc/reference/syntax/sqlgetconnectoption-function.md) in the *ODBC Programmer's Reference*.
