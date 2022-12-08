---
title: "Transaction Log Disk Space for Index Operations"
description: Transaction Log Disk Space for Index Operations
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "index disk space [SQL Server]"
  - "space [SQL Server], indexes"
  - "transaction logs [SQL Server], disk space"
  - "disk space [SQL Server], transaction logs"
  - "space [SQL Server], transaction logs"
ms.assetid: 4f8a4922-4507-4072-be67-c690528d5c3b
---
# Transaction Log Disk Space for Index Operations
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Large-scale index operations can generate large data loads that can cause the transaction log to fill quickly. To make sure that the index operation can be rolled back, the transaction log cannot be truncated until the index operation has completed; however, the log can be backed up during the index operation. Therefore, the transaction log must have sufficient room to store both the index operation transactions and any concurrent user transactions for the duration of the index operation. This is true for both offline and online index operations. Because the underlying tables cannot be accessed during an offline index operation, there may be few user transactions and the log may not grow as quickly. Online index operations do not prevent concurrent user activity, therefore, large-scale online index operations combined with significant concurrent user transactions can cause continuous growth of the transaction log without an option to truncate the log.  
  
## Recommendations  
 When you run large-scale index operations, consider the following recommendations:  
  
1.  Make sure the transaction log has been backed up and truncated before running large-scale index operations online, and that the log has sufficient space to store the projected index and user transactions.  
  
2.  Consider setting the SORT_IN_TEMPDB option to ON for the index operation. This separates the index transactions from the concurrent user transactions. The index transactions will be stored in the **tempdb** transaction log, and the concurrent user transactions will be stored in the transaction log of the user database. This allows for the transaction log of the user database to be truncated during the index operation if it is required. Additionally, if the **tempdb** log is not on the same disk as the user database log, the two logs are not competing for the same disk space.  
  
    > [!NOTE]  
    >  Verify that the **tempdb** database and transaction log have sufficient disk space to handle the index operation. The **tempdb** transaction log cannot be truncated until the index operation is completed.  
  
3.  Use a database recovery model that allows for minimal logging of the index operation. This may reduce the size of the log and prevent the log from filling the log space.  
  
4.  Do not run the online index operation in an explicit transaction. The log will not be truncated until the explicit transaction ends.  
  
## Related Content  
 [Disk Space Requirements for Index DDL Operations](../../relational-databases/indexes/disk-space-requirements-for-index-ddl-operations.md)  
  
 [Index Disk Space Example](../../relational-databases/indexes/index-disk-space-example.md)  
  
  
