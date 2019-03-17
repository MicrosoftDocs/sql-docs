---
title: "Improving Performance and Reliability with the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "07/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: e1592499-b87b-45ee-bab8-beaba8fde841
author: MightyPen
ms.author: genemi
manager: craigg
---

# Improving Performance and Reliability with the JDBC Driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

One aspect of application development that is common to all applications is the constant need to improve performance and reliability. There are a number of techniques for doing this with the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)].  
  
The topics in this section describe various techniques for improving application performance and reliability when using the JDBC driver.  

## In This Section

|Topic|Description|  
|-----------|-----------------|  
|[Closing Objects when Not In Use](../../connect/jdbc/closing-objects-when-not-in-use.md)|Describes the importance of closing JDBC driver objects when they are no longer needed.|  
|[Managing Transaction Size](../../connect/jdbc/managing-transaction-size.md)|Describes techniques for improving transaction performance.|  
|[Working with Statements and Result Sets](../../connect/jdbc/working-with-statements-and-result-sets.md)|Describes techniques for improving performance when using the Statement or ResultSet objects.|  
|[Using Adaptive Buffering](../../connect/jdbc/using-adaptive-buffering.md)|Describes an adaptive buffering feature, which is designed to retrieve any kind of large-value data without the overhead of server cursors.|  
|[Sparse Columns](../../connect/jdbc/sparse-columns.md)|Discusses the JDBC driver's support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sparse columns.|  
|[Prepared Statement Metadata Caching for the JDBC Driver](../../connect/jdbc/prepared-statement-metadata-caching-for-the-jdbc-driver.md)|Discusses the techniques for improving performance with prepared statement queries.|
|[Using Bulk Copy API for Batch Insert Operation](../../connect/jdbc/use-bulk-copy-api-batch-insert-operation.md)|Describes how to enable Bulk Copy API for batch insert operations and its benefits.|

## See also

[Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
