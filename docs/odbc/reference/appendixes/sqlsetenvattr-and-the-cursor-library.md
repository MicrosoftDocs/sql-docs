---
title: "SQLSetEnvAttr and the Cursor Library | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetEnvAttr function [ODBC], Cursor Library"
ms.assetid: 59cc8eae-09ae-4796-869a-c5806488ae83
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetEnvAttr and the Cursor Library
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLSetEnvAttr** function with the cursor library. For general information about **SQLSetEnvAttr**, see [SQLSetEnvAttr Function](../../../odbc/reference/syntax/sqlsetenvattr-function.md).  
  
 The cursor library is unaffected by the setting of the SQL_ATTR_ODBC_VERSION environment attribute, regardless of the application version or driver version.
