---
title: Data Integrity
description: This article discusses data integrity in Azure SQL Database, including Microsoft's monitoring, actions, and responsibility.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman, mathoma, oslake, adbadram, dinethi
ms.date: 07/20/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: concept-article
---

# Data integrity in Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
>  
> - [Azure SQL Database](data-integrity.md?view=azuresql-db&preserve-view=true)
> - [Azure SQL Managed Instance](../managed-instance/data-integrity.md?view=azuresql-mi&preserve-view=true)

Microsoft is responsible for managing data integrity in Azure SQL Database. While traditional techniques exist for DBAs to monitor data integrity and recover from database corruptions in SQL Server, the Microsoft SQL engineering team developed new techniques that handle some classes of corruption automatically and without data loss. The service uses these techniques to avoid data loss and downtime in cases where it can avoid them.

This article outlines some of those techniques, how they work, and how they affect customers concerned with what steps they should take to safeguard their data in Azure SQL Database.

## How Microsoft manages data integrity

Protecting data integrity in Azure SQL Database involves a combination of techniques and evolving methods:

- **Extensive data integrity error alert monitoring.** The [SQL Database Engine](/sql/database-engine/sql-database-engine) emits alerts for all errors and unhandled exceptions that indicate data integrity concerns. The engineering team handles and investigates these alerts.

- **I/O system "lost write" detection.** The database engine has additional functionality to detect what has been the most common cause of observed physical corruption issues: I/O system "lost writes". This functionality tracks page writes and their associated LSNs (Log Sequence Numbers). A subsequent read of a data page from disk is compared with the page's expected LSN. If there's a mismatch in LSNs between what's on disk and what's expected, the page is stale, resulting in an immediate alert to the engineering team.

- **Automatic page repair.** Some service tiers provide database replicas for business continuity purposes. The service then leverages automatic page repair, which is similar to the technology used in availability groups. In the event that a replica can't read a page due to a data integrity issue, the service retrieves a fresh copy of the page from another replica, replacing the unreadable page without data loss or customer downtime.

- **Data integrity at rest and in transit.** All databases in the service are configured to verify pages by using the `CHECKSUM` setting, which calculates the checksum over the entire page and stores it in the page header for verification during read. Transport Layer Security (TLS) is also used for all communication in addition to the base transport level checksums provided by TCP/IP.

- **Backup and restore integrity checks.** Azure SQL Database performs page verification both during service-managed backups and during each restore operation. Any problems found result in an immediate alert to the engineering team.

## How Microsoft handles data integrity incidents

Microsoft treats incidents of wrong results or corruption with the highest severity. The company provides 24×7 support from across all Azure engineering teams. When handling integrity incidents, the goals are to minimize unavailability and minimize the amount of data loss.

### System data integrity issues

Microsoft corrects problems that don't affect customer data or database availability without notifying customers. Examples include problems that automatic page repair can address, or corruption to internal database metadata or telemetry that doesn't affect customer data or query results.

### Customer data integrity issues

When Microsoft detects a wrong-results or customer data corruption problem, Microsoft takes the following actions:

1. Makes a best-effort attempt to contact the customer as soon after confirmed detection as possible, in compliance with privacy laws.
1. If contact is established, works directly with the customer to explain the scope of corruption, outline recovery options, and allow the customer to choose the option that works best for their application and scenario.
1. Where possible, assists the customer in understanding the scope of impact to their application, for example identifying whether data corruption has caused the application to change other data in an unexpected way.

Microsoft repairs data corruption by using various methods and steps taken in coordination with customers. It doesn't attempt repair that can result in data loss without customer approval. Customers can't perform `DBCC CHECKDB` repair options in Azure SQL Database because you can't place a database in `SINGLE_USER` mode. However, the Microsoft SQL team can take repair steps including but not limited to:

- Rebuild the index. For example, rebuild a nonclustered index where the base table isn't also corrupted.
- Run `DBCC CHECKDB` with `REPAIR_REBUILD` where the repair has no possibility of data loss.
- Run `DBCC CHECKDB` with `REPAIR_ALLOW_DATA_LOSS` where repairs can cause some data loss.
- For scenarios where `DBCC CHECKDB` can't be used to repair the data integrity problem, engineers might use point-in-time restore to the point before the data integrity problem occurred, followed by a manual replay of relevant transactions from the transaction log. An example when this technique applies is when the transaction log is corrupted in a way that prevents automatic replay of all transactions but without corrupting customer data.

The engineering team conducts detailed postmortems on problems that lead to wrong-results or data corruption. The team closely tracks the associated repair items created because of the problem. These postmortems lead to many significant enhancements, including the "lost writes" functionality described earlier.

## Customer-initiated integrity checks 

The Microsoft-managed data integrity protection provides early detection of new data integrity issues and repairs them when possible. Customer-initiated integrity checks using `DBCC CHECKDB` provide an extra layer of protection because `DBCC CHECKDB` is a comprehensive corruption detection mechanism for the entire database. `DBCC CHECKDB` is complementary to the Microsoft-managed data integrity protection features.

This breadth and depth of detection require significant time and extra compute and I/O resources during `DBCC CHECKDB` execution. As a result, `DBCC CHECKDB` might affect your workloads due to resource contention.

In addition to the monitoring and protection provided by the service, you can run `DBCC CHECKDB` at a frequency and time of your choice, balancing the extra data integrity protection against the extra resource consumption.

`DBCC CHECKDB` is not available in Azure SQL Database Hyperscale. As an alternative, use the `DBCC CHECKTABLE ('<TableName>') WITH TABLOCK` for individual tables in the database.

## Customer feedback and evolving methodologies

The Azure SQL engineering team regularly reviews and enhances the data integrity issue detection capabilities of the service. Although data integrity errors are rare, if you encounter an error before receiving notification from [Azure Support](https://azure.microsoft.com/support/options/), file a support case.

If you have feedback to share regarding Microsoft's data integrity strategy, the engineering team would like to hear from you. To contact the engineering team with feedback or comments on this subject, see [https://aka.ms/sqlfeedback](https://aka.ms/sqlfeedback). Your feedback helps Microsoft improve existing and develop new data integrity protection capabilities.

## Related content

- [Automated backups in Azure SQL Database](automated-backups-overview.md)
- [Backup immutability for long-term retention backups in Azure SQL Database](backup-immutability.md)