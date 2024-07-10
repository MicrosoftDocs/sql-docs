---
title: "Transaction Support"
description: "Transaction Support"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "transactions [ODBC], degree of support"
---
# Transaction Support
The degree of support for transactions is driver-defined. ODBC is designed to be implemented on a single-user or desktop database that has no need to manage multiple updates to its data. Moreover, some databases that support transactions do so only for the Data Manipulation Language (DML) statements of SQL; there are restrictions or special transaction semantics regarding the use of Data Definition Language (DDL) when a transaction is active. That is, there may be transaction support for multiple simultaneous updates to tables but not for changing the number and definition of tables during a transaction.  
  
 An application determines whether transactions are supported, whether DDL can be included in a transaction, and any special effects of including DDL in a transaction, by calling **SQLGetInfo** with the SQL_TXN_CAPABLE option. For more information, see the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description.  
  
 If the driver does not support transactions but the application has the ability (using an API other than ODBC) to lock and unlock data, applications can achieve transaction support by locking and unlocking records and tables as needed. To implement the account-transfer example, the application would lock the records for both accounts, copy the current values, debit the first account, credit the second account, and unlock the records. If any steps failed, the application would reset the accounts using the copies.  
  
 Even data sources that support transactions might not be able to support more than one transaction at a time within a particular environment. Applications call **SQLGetInfo** with the SQL_MULTIPLE_ACTIVE_TXN option to determine whether a data source can support simultaneous active transactions on more than one connection in the same environment. Because there is one transaction per connection, this is only interesting to applications that have multiple connections to the same data source.
