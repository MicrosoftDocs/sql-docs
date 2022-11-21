---
description: "Auto-Commit Mode"
title: "Auto-Commit Mode | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "rolling back transactions [ODBC]"
  - "auto-commit mode [ODBC]"
  - "transactions [ODBC], commit modes"
  - "committing transactions [ODBC]"
  - "commit modes [ODBC]"
  - "transactions [ODBC], rolling back"
ms.assetid: c8de5b60-d147-492d-b601-2eeae8511d00
author: David-Engel
ms.author: v-davidengel
---
# Auto-Commit Mode
*In auto-commit mode,* every database operation is a transaction that is committed when performed. This mode is suitable for many real-world transactions that consist of a single SQL statement. It is unnecessary to delimit or specify completion of these transactions. In databases without transaction support, auto-commit mode is the only supported mode. In such databases, statements are committed when they are executed and there is no way to roll them back; they are therefore always in auto-commit mode.  
  
 If the underlying DBMS does not support auto-commit mode transactions, the driver can emulate them by manually committing each SQL statement as it is executed.  
  
 If a batch of SQL statements is executed in auto-commit mode, it is data source-specific when the statements in the batch are committed. They can be committed as they are executed or as a whole after the entire batch has been executed. Some data sources may support both of these behaviors and may provide a way of selecting one or the others. In particular, if an error occurs in the middle of the batch, it is data source-specific whether the already-executed statements are committed or rolled back. Thus, interoperable applications that use batches and require them to be committed or rolled back as a whole should execute batches only in manual-commit mode.
