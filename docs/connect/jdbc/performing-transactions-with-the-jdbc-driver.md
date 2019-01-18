---
title: "Performing Transactions with the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: afbb776f-05dc-4e79-bb25-2c340483e401
author: MightyPen
ms.author: genemi
manager: craigg
---
# Performing Transactions with the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  Transaction processing is a mandatory requirement of all applications that must guarantee consistency of their persistent data. With the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], transaction processing can either be performed locally or distributed. Transactions are atomic, consistent, isolated, and durable (ACID) modules of execution.  
  
 The topics in this section describe how the JDBC driver supports transactions including isolation levels, transaction savepoints, and result set holdability.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Understanding Transactions](../../connect/jdbc/understanding-transactions.md)|Provides an overview of how transactions are used with the JDBC driver.|  
|[Understanding XA Transactions](../../connect/jdbc/understanding-xa-transactions.md)|Provides an overview of how XA transactions are used with the JDBC driver.|  
|[Understanding Isolation Levels](../../connect/jdbc/understanding-isolation-levels.md)|Describes the various isolation levels that are supported by the JDBC driver.|  
|[Using Savepoints](../../connect/jdbc/using-savepoints.md)|Describes how to use the JDBC driver with transaction savepoints.|  
|[Using Holdability](../../connect/jdbc/using-holdability.md)|Describes how to use the JDBC driver with result set holdability.|  
  
## See Also  
 [Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
  
