---
title: "SQLBindParameter (Excel Driver)"
description: "SQLBindParameter (Excel Driver)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "Excel driver [ODBC], SQLBindParameter"
  - "SQLBindParameter function [ODBC], Excel Driver"
---
# SQLBindParameter (Excel Driver)
> [!NOTE]  
>  This topic provides Excel Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 When the Microsoft Excel driver is used, executing an INSERT statement that uses a parameter to insert a NULL into a SQL_CHAR column will return SQL_SUCCESS_WITH_INFO with SQLSTATE 01004, "Data Truncated."
