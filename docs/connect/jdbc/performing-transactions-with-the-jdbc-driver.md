---
title: Performing transactions
description: Learn how the JDBC Driver for SQL Server supports transactions including isolation levels, savepoints, and result set holdability.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Performing transactions with the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Transaction processing is a mandatory requirement of all applications that must guarantee consistency of their persistent data. With the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], transaction processing can either be performed locally or distributed. Transactions are atomic, consistent, isolated, and durable (ACID) modules of execution.

 The articles in this section describe how the JDBC driver supports transactions including isolation levels, transaction savepoints, and result set holdability.

## In this section

|Article|Description|
|-----------|-----------------|
|[Understanding transactions](understanding-transactions.md)|Provides an overview of how transactions are used with the JDBC driver.|
|[Understanding XA transactions](understanding-xa-transactions.md)|Provides an overview of how XA transactions are used with the JDBC driver.|
|[Understanding isolation levels](understanding-isolation-levels.md)|Describes the various isolation levels that are supported by the JDBC driver.|
|[Using savepoints](using-savepoints.md)|Describes how to use the JDBC driver with transaction savepoints.|
|[Using holdability](using-holdability.md)|Describes how to use the JDBC driver with result set holdability.|

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
