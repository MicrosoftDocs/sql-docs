---
title: "Manual-Commit Mode"
description: "Manual-Commit Mode"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "rolling back transactions [ODBC]"
  - "manual-commit mode [ODBC]"
  - "transactions [ODBC], commit modes"
  - "committing transactions [ODBC]"
  - "commit modes [ODBC]"
  - "transactions [ODBC], rolling back"
---
# Manual-Commit Mode
*In manual-commit mode,* applications must explicitly complete transactions by calling **SQLEndTran** to commit them or roll them back. This is the normal transaction mode for most relational databases.  
  
 Transactions in ODBC do not have to be explicitly initiated. Instead, a transaction begins implicitly whenever the application starts operating on the database. If the data source requires explicit transaction initiation, the driver must provide it whenever the application executes a statement requiring a transaction and there is no current transaction.
