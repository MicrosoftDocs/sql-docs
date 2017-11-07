---
title: "Validate Replicated Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "inline data validation [SQL Server replication]"
  - "administering replication, validating data"
  - "replication [SQL Server], validating data"
  - "transactional replication, validating data"
  - "validating data"
  - "merge replication data validation [SQL Server replication]"
  - "merge replication data validation [SQL Server replication], about data validation"
  - "validating replicated data"
ms.assetid: f7500a2b-61cb-41b5-816d-27609a6c58e7
caps.latest.revision: 45
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Validate Replicated Data
  Transactional and merge replication allow you to validate that data at the Subscriber matches data at the Publisher. Validation can be performed for specific subscriptions or for all subscriptions to a publication. Specify one of the following validation types and the Distribution Agent or Merge Agent will validate data the next time it runs:  
  
-   Row count only. This validates whether the table at the Subscriber has the same number of rows as the table at the Publisher, but does not validate that the content of the rows matches. Row count validation provides a lightweight approach to validation that can make you aware of issues with your data.  
  
-   Row count and binary checksum. In addition to taking a count of rows at the Publisher and Subscriber, a checksum of all the data is calculated using the checksum algorithm. If the row count fails, the checksum is not performed.  
  
 In addition to validating that data at the Subscriber and Publisher match, merge replication provides the ability to validate that data is partitioned correctly for each Subscriber. For more information, see [Validate Partition Information for a Merge Subscriber](../../relational-databases/replication/validate-partition-information-for-a-merge-subscriber.md).  
  
 **To validate data**  
  
 To validate all articles in a subscription, use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], stored procedures or Replication Management Objects (RMO). For more information, see [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md). To validate individual articles in snapshot and transactional publications, you must use stored procedures.  
  
## Data Validation Results  
 When validation is complete, the Distribution Agent or Merge Agent logs messages regarding success or failure (replication does not report which rows failed). These messages can be viewed in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], Replication Monitor, and replication system tables. The how-to topic listed above demonstrates how to run validation and view the results.  
  
 To handle validation failures, consider the following:  
  
-   Configure the replication alert named **Replication: Subscriber has failed data validation** so that you are notified of the failure. For more information, see [Configure Predefined Replication Alerts &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/administration/configure-predefined-replication-alerts-sql-server-management-studio.md).  
  
-   Is the fact that validation failed an issue for your application? If the validation failure is an issue, manually update the data so that it is synchronized, or reinitialize the subscription:  
  
    -   Data can be updated using the [tablediff utility](../../tools/tablediff-utility.md). For more information about using this utility, see [Compare Replicated Tables for Differences &#40;Replication Programming&#41;](../../relational-databases/replication/administration/compare-replicated-tables-for-differences-replication-programming.md).  
  
    -   For more information about reinitializaton, see [Reinitialize Subscriptions](../../relational-databases/replication/reinitialize-subscriptions.md).  
  
## Considerations for Data Validation  
 Take the following issues into consideration when validating data:  
  
-   You must stop all update activity at Subscribers before validating data (it is not necessary to stop activity at the Publisher when validation is occurring).  
  
-   Because checksums and binary checksums can require large amounts of processor resources when validating a large data set, you should schedule validation to occur when there is the least activity on the servers used in replication.  
  
-   Replication validates tables only; it does not validate whether schema only articles (such as stored procedures) are the same at the Publisher and Subscriber.  
  
-   Binary checksum can be used with any published table. Checksum cannot validate tables with column filters, or logical table structures where column offsets differ (due to ALTER TABLE statements that drop or add columns).  
  
-   Replication validation uses the **checksum** and **binary_checksum** functions. For  information about their behavior, see  [CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md) and [BINARY_CHECKSUM  &#40;Transact-SQL&#41;](../../t-sql/functions/binary-checksum-transact-sql.md).  
  
-   Validation by using binary checksum or checksum can incorrectly report a failure if data types are different at the Subscriber than they are at the Publisher. This can occur if you do any one of the following:  
  
    -   Explicitly set schema options to map data types for earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
    -   Set the publication compatibility level for a merge publication to an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and published tables contain one or more data types that must be mapped for this version.  
  
    -   Manually initialize a subscription and are using different data types at the Subscriber.  
  
-   Binary checksum and checksum validations are not supported for transformable subscriptions for transactional replication.  
  
-   Validation is not supported for data replicated to non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.  
  
## How Data Validation Works  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] validates data by calculating a row count or a checksum at the Publisher and then comparing those values to the row count or checksum calculated at the Subscriber. One value is calculated for the entire publication table and one value is calculated for the entire subscription table, but data in **text**, **ntext**, or **image** columns is not included in the calculations.  
  
 While the calculations are performed, shared locks are placed temporarily on tables for which row counts or checksums are being run, but the calculations are completed quickly and the shared locks removed, usually in a matter of seconds.  
  
 When binary checksums are used, 32-bit redundancy check (CRC) occurs on a column-by-column basis rather than a CRC on the physical row on the data page. This allows the columns with the table to be in any order physically on the data page, but still compute to the same CRC for the row. Binary checksum validation can be used when there are row or column filters on the publication.  
  
## See Also  
 [Best Practices for Replication Administration](../../relational-databases/replication/administration/best-practices-for-replication-administration.md)  
  
  