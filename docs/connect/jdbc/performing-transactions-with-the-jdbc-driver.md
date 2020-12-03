---
title: "Performing transactions with the JDBC driver"
description: "Learn how the JDBC Driver for SQL Server supports transactions including isolation levels, savepoints, and result set holdability."
ms.custom: ""
ms.date: "08/12/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: afbb776f-05dc-4e79-bb25-2c340483e401
author: David-Engel
ms.author: v-daenge
---
# Performing transactions with the JDBC driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  Transaction processing is a mandatory requirement of all applications that must guarantee consistency of their persistent data. With the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], transaction processing can either be performed locally or distributed. Transactions are atomic, consistent, isolated, and durable (ACID) modules of execution.  
  
 The topics in this section describe how the JDBC driver supports transactions including isolation levels, transaction savepoints, and result set holdability.  
  
## In this section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Understanding transactions](../../connect/jdbc/understanding-transactions.md)|Provides an overview of how transactions are used with the JDBC driver.|  
|[Understanding XA transactions](../../connect/jdbc/understanding-xa-transactions.md)|Provides an overview of how XA transactions are used with the JDBC driver.|  
|[Understanding isolation levels](../../connect/jdbc/understanding-isolation-levels.md)|Describes the various isolation levels that are supported by the JDBC driver.|  
|[Using savepoints](../../connect/jdbc/using-savepoints.md)|Describes how to use the JDBC driver with transaction savepoints.|  
|[Using holdability](../../connect/jdbc/using-holdability.md)|Describes how to use the JDBC driver with result set holdability.|  
  
## See also  
 [Overview of the JDBC driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
  
