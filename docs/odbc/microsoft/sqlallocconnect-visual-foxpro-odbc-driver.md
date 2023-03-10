---
title: "SQLAllocConnect (Visual FoxPro ODBC Driver)"
description: "SQLAllocConnect (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "SQLAllocConnect function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLAllocConnect (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Core Level  
  
 Allocates memory for a connection handle, *hdbc*, within the environment identified by *henv*. The Driver Manager processes this call and calls the driver's **SQLAllocConnect** whenever [SQLConnect](../../odbc/microsoft/sqlconnect-visual-foxpro-odbc-driver.md), **SQLBrowseConnect**, or [SQLDriverConnect](../../odbc/microsoft/sqldriverconnect-visual-foxpro-odbc-driver.md) is called.  
  
 For more information, see [SQLAllocConnect](../../odbc/reference/syntax/sqlallocconnect-function.md) in the *ODBC Programmer's Reference*.
