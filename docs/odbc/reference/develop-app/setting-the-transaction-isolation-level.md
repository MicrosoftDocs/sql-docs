---
description: "Setting the Transaction Isolation Level"
title: "Setting the Transaction Isolation Level | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "isolation levels [ODBC]"
  - "transaction isolation [ODBC]"
  - "transactions [ODBC], isolation"
ms.assetid: 64a037f0-5065-4f45-9669-6710404a540c
author: David-Engel
ms.author: v-davidengel
---
# Setting the Transaction Isolation Level
To set the transaction isolation level, an application uses the SQL_ATTR_TXN_ISOLATION connection attribute. If the data source does not support the requested isolation level, the driver or data source can set a higher level. To determine what transaction isolation levels a data source supports and what the default isolation level is, an application calls **SQLGetInfo** with the SQL_TXN_ISOLATION_OPTION and SQL_DEFAULT_TXN_ISOLATION options, respectively.  
  
 Higher levels of transaction isolation offer the most protection for the integrity of database data. Serializable transactions are guaranteed to be unaffected by other transactions and therefore guaranteed to maintain database integrity.  
  
 However, a higher level of transaction isolation can cause slower performance because it increases the chances that the application will have to wait for locks on data to be released. An application can specify a lower level of isolation to increase performance in the following cases:  
  
-   When it can be guaranteed that no other transactions exist that might interfere with an application's transactions. This situation occurs only in limited circumstances, such as when one person in a small company maintains dBASE files that contain personnel data on one computer and does not share these files.  
  
-   When speed is more critical than accuracy and any errors are likely to be small. For example, suppose that a company makes many small sales and that large sales are rare. A transaction that estimates the total value of all open sales might safely use the Read Uncommitted isolation level. Although the transaction would include orders that are being opened or closed and are subsequently rolled back, these would generally cancel each other out and the transaction would be much faster because it is not blocked every time that it encounters such an order.  
  
 For more information, see [Optimistic Concurrency](../../../odbc/reference/develop-app/optimistic-concurrency.md).
