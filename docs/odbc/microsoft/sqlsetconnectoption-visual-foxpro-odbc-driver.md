---
title: "SQLSetConnectOption (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetConnectOption function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 5a35449e-4694-4ee5-9fa1-45d5a8fe7823
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetConnectOption (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Partial  
  
 ODBC API Conformance: Level 1  
  
 Sets options that govern aspects of connections. This function is partially supported: The driver supports all values for the *fOption* argument but does not support some of *vParam* values for the *fOption* argument SQL_TXN_ISOLATION.  
  
 The following table describes only those arguments with behavior specific to the Visual FoxPro ODBC Driver implementation of **SQLSetConnectOption**.  
  
|*fOption*|Remarks|  
|---------------|-------------|  
|SQL_AUTOCOMMIT|If you choose SQL_AUTOCOMMIT_OFF, your application must explicitly commit or roll back transactions with [SQLTransact](../../odbc/microsoft/sqltransact-visual-foxpro-odbc-driver.md); the Visual FoxPro ODBC Driver does not automatically commit a transactable statement upon completion. The driver does begin a transaction if the statement is transactable.|  
|SQL_CURRENT_QUALIFIER|Can be a fully qualified [database](../../odbc/microsoft/visual-foxpro-terminology.md) name or fully qualified path to a directory containing zero or more [free tables](../../odbc/microsoft/visual-foxpro-terminology.md).|  
|SQL_LOGINTIMEOUT|Returns "Driver not capable" error.|  
|SQL_CURSORS|Returns "Driver not capable" error.|  
|SQL_PACKET_SIZE|Returns "Driver not capable" error.|  
|SQL_TXN_ISOLATION|The driver allows only SQL_TXN_READ_COMMITTED.<br /><br /> The following *vParam*s are not supported:<br /><br /> SQL_TXN_READ_UNCOMMITTED<br /><br /> SQL_TXN_REAPEATABLE_READ<br /><br /> SQL_TXN_SERIALIZABLE|  
  
 For more information, see [SQLSetConnectOption](../../odbc/reference/syntax/sqlsetconnectoption-function.md) in the *ODBC Programmer's Reference*.
