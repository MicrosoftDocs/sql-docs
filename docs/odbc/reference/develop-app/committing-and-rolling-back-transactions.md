---
title: "Committing and Rolling Back Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "rolling back transactions [ODBC]"
  - "committing transactions [ODBC]"
  - "transactions [ODBC], rolling back"
  - "transactions [ODBC], committing"
ms.assetid: 800f2c1a-6f79-4ed1-830b-aa1a62ff5165
author: MightyPen
ms.author: genemi
manager: craigg
---
# Committing and Rolling Back Transactions
To commit or roll back a transaction in manual-commit mode, an application calls **SQLEndTran**. Drivers for DBMSs that support transactions typically implement this function by executing a **COMMIT** or **ROLLBACK** statement. The Driver Manager does not call **SQLEndTran** when the connection is in auto-commit mode; it simply returns SQL_SUCCESS, even if the application attempts to roll back the transaction. Because drivers for DBMSs that do not support transactions are always in auto-commit mode, they can either implement **SQLEndTran** to return SQL_SUCCESS without doing anything or not implement it at all.  
  
> [!NOTE]  
>  Applications should not commit or roll back transactions by executing **COMMIT** or **ROLLBACK** statements with **SQLExecute** or **SQLExecDirect**. The effects of doing this are undefined. Possible problems include the driver no longer knowing when a transaction is active and these statements failing against data sources that do not support transactions. These applications should call **SQLEndTran** instead.  
  
 If an application passes the environment handle to **SQLEndTran** but does not pass a connection handle, the Driver Manager conceptually calls **SQLEndTran** with the environment handle for each driver that has one or more active connections in the environment. The driver then commits the transactions on each connection in the environment. However, it is important to realize that neither the driver nor the Driver Manager performs a two-phase commit on the connections in the environment; this is merely a programming convenience to simultaneously call **SQLEndTran** for all connections in the environment.  
  
 (A *two-phase commit* is generally used to commit transactions that are spread across multiple data sources. In its first phase, the data sources are polled as to whether they can commit their part of the transaction. In the second phase, the transaction is actually committed on all data sources. If any data sources reply in the first phase that they cannot commit the transaction, the second phase does not occur.)
