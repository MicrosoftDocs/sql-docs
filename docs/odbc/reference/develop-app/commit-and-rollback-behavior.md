---
title: "Commit and Rollback Behavior"
description: "Commit and Rollback Behavior"
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
# Commit and Rollback Behavior
A common behavior among server DBMSs is to close cursors and discard prepared statements when a statement is committed or rolled back. Desktop databases are more likely to keep cursors open and keep prepared statements. For more information, see the SQL_CURSOR_COMMIT_BEHAVIOR and SQL_CURSOR_ROLLBACK_BEHAVIOR options in the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description and [Effect of Transactions on Cursors and Prepared Statements](../../../odbc/reference/develop-app/effect-of-transactions-on-cursors-and-prepared-statements.md).
