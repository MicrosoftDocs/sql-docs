---
title: "Transaction Support in DBMSs"
description: "Transaction Support in DBMSs"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "interoperability [ODBC], transaction support"
  - "transactions [ODBC], DBMS support"
---
# Transaction Support in DBMSs
Some databases, especially desktop databases such as dBASE, Paradox, and Btrieve, do not support transactions. Even among databases that support transactions, there is variation in what kinds of SQL statements can be in a transaction. For more information, see the SQL_TXN_CAPABLE option in the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description.
