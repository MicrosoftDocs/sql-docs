---
description: "Scrollable Cursors and Transaction Isolation"
title: "Scrollable Cursors and Transaction Isolation | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "isolation levels [ODBC]"
  - "scrollable cursors [ODBC]"
  - "transaction isolation [ODBC]"
  - "transactions [ODBC], isolation"
ms.assetid: f0216f4a-46e3-48ae-be0a-e2625e8403a6
author: David-Engel
ms.author: v-davidengel
---
# Scrollable Cursors and Transaction Isolation
The following table lists the factors governing the visibility of changes.  
  
|Changes made by:|Visibility depends on:|  
|----------------------|----------------------------|  
|Cursor|Cursor type, cursor implementation|  
|Other statements in same transaction|Cursor type|  
|Statements in other transactions|Cursor type, transaction isolation level|  
  
 These factors are shown in the following illustration.  
  
 ![Factors governing the visibility of changes](../../../odbc/reference/develop-app/media/pr23.gif "pr23")  
  
 The following table summarizes the ability of each cursor type to detect changes made by itself, by other operations in its own transaction, and by other transactions. The visibility of the latter changes depends on the cursor type and the isolation level of the transaction containing the cursor.  
  
|Cursor type\action|Self|Own<br /><br /> Txn|Othr<br /><br /> Txn<br /><br /> (RU[a])|Othr<br /><br /> Txn<br /><br /> (RC[a])|Othr<br /><br /> Txn<br /><br /> (RR[a])|Othr<br /><br /> Txn<br /><br /> (S[a])|  
|-------------------------|----------|-----------------|----------------------------------|----------------------------------|----------------------------------|---------------------------------|  
|Static|||||||  
|Insert|Maybe[b]|No|No|No|No|No|  
|Update|Maybe[b]|No|No|No|No|No|  
|Delete|Maybe[b]|No|No|No|No|No|  
|Keyset-driven|||||||  
|Insert|Maybe[b]|No|No|No|No|No|  
|Update|Yes|Yes|Yes|Yes|No|No|  
|Delete|Maybe[b]|Yes|Yes|Yes|No|No|  
|Dynamic|||||||  
|Insert|Yes|Yes|Yes|Yes|Yes|No|  
|Update|Yes|Yes|Yes|Yes|No|No|  
|Delete|Yes|Yes|Yes|Yes|No|No|  
  
 [a]   The letters in parentheses indicate the isolation level of the transaction containing the cursor; the isolation level of the other transaction (in which the change was made) is irrelevant.  
  
 RU: Read uncommitted  
  
 RC: Read committed  
  
 RR: Repeatable read  
  
 S:  Serializable  
  
 [b]   Depends on how the cursor is implemented. Whether the cursor can detect such changes is reported through the SQL_STATIC_SENSITIVITY option in **SQLGetInfo**.
