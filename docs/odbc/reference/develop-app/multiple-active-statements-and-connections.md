---
title: "Multiple Active Statements and Connections | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability [ODBC], multiple active statements and connections"
  - "multiple active statements and connections [ODBC]"
ms.assetid: a6571356-b23e-4f10-a17b-bce09460b71e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Multiple Active Statements and Connections
Some drivers and DBMSs limit the number of statements and connections that can be active at one time. These numbers can be as small as one. For more information, see the SQL_MAX_CONCURRENT_ACTIVITIES and SQL_MAX_DRIVER_CONNECTIONS options in the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description, and [Statement Handles](../../../odbc/reference/develop-app/statement-handles.md) and [Connection Handles](../../../odbc/reference/develop-app/connection-handles.md).
