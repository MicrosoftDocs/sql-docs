---
description: "Step 5: Commit the Transaction"
title: "Step 5: Commit the Transaction | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "application process [ODBC], committing transactions"
  - "committing transactions [ODBC]"
  - "transaction commit [ODBC]"
ms.assetid: 311685e2-f7b5-4ddc-8020-59380cd2f035
author: David-Engel
ms.author: v-davidengel
---
# Step 5: Commit the Transaction
The next step is to commit the transaction, as shown in the following illustration.  
  
 ![Shows how to commit a transaction](../../../odbc/reference/develop-app/media/pr16.gif "pr16")  
  
 The fifth step is to call **SQLEndTran** to commit or roll back the transaction. The application performs this step only if it set the transaction commit mode to manual-commit; if the transaction commit mode is auto-commit, which is the default, the transaction is automatically committed when the statement is executed. For more information, see [Transactions](../../../odbc/reference/develop-app/transactions-odbc.md).  
  
 To execute a statement in a new transaction, the application returns to step 3. To disconnect from the data source, the application proceeds to step 6.
