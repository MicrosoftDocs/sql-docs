---
description: "SQLTransact (Visual FoxPro ODBC Driver)"
title: "SQLTransact (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLTransact function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 92cf86c0-f7a8-44d7-b59f-a1342677440b
author: David-Engel
ms.author: v-davidengel
---
# SQLTransact (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Core Level  
  
 Requests a commit or rollback operation for all active operations on all statement handles (*hstmt*s) associated with a connection or for all connections associated with the environment handle, *henv*. **SQLTransact** works only for data sources that are [databases](../../odbc/microsoft/visual-foxpro-terminology.md).  
  
 If a commit fails when in manual mode, the transaction remains active; you can choose to roll back the transaction or retry the commit operation. If a commit operation fails when in automatic transaction mode, the transaction is rolled back automatically; the transaction cannot be inactive.  
  
 For more information, see [SQLTransact](../../odbc/reference/syntax/sqltransact-function.md) in the *ODBC Programmer's Reference*.
