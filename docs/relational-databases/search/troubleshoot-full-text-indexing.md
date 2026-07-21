---
title: Troubleshoot Full-Text Indexing
description: Troubleshoot Full-Text Indexing
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: troubleshooting-general
helpviewer_keywords:
  - "indexes [full-text search]"
  - "troubleshooting [SQL Server], full-text search"
  - "troubleshooting [full-text search]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Troubleshoot full-text indexing

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

<a id="failure"></a>

## Troubleshoot full-text indexing failures

While populating or maintaining a full-text index, the full-text indexer might fail to index one or more rows for the reasons described in the following list. These row-level errors don't prevent the population from completing. The indexer skips these rows, which means that you can't query for content contained in these rows.

Indexing failures can occur when:

- The indexer can't find or load a filter or word breaker component. This failure can occur if the table row contains a document format or content in a language that isn't registered with the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This failure can also happen if the registered word breaker or filter component isn't signed, or if signature verification fails when the component is being loaded. Azure SQL and Azure SQL Managed Instance don't support third-party word breakers.

- A component, such as a word breaker or filter, fails and returns an error to the indexer. This failure can happen if the document being indexed is corrupt and the filter can't extract text from the document. This failure can also occur when a component can't handle the content of a single row that's above a certain size, due to memory limits on the full-text filter daemon host (`fdhost.exe`).

For each row-level failure, the crawl log contains details on the reason for the failure. The error counts are summarized at the end of a full or incremental population.

Other failures can impact the indexing process and prevent the population from completing:

- The full-text index exceeds the limit for the number of rows that a full-text catalog can contain.

- You alter, drop, or rebuild a clustered index or full-text key index on the table being indexed.

- A hardware failure or disk corruption results in the corruption of the full-text catalog.

- A filegroup that contains the table being full-text indexed goes offline or is made read-only.

Examine the crawl log at the end of any significant full-text index population operation, or when you find that a population didn't complete.

### Unsigned components

By default, the full-text indexer requires the filters and word breakers that it loads to be signed. If they're not signed, which can happen when you install custom components, you must configure the full-text indexer to ignore signature verification.

> [!IMPORTANT]  
> Ignoring signature verification makes the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] less secure. You should sign any components that you implement, or ensure that any components that you acquire are signed. For information about signing components, see [sp_fulltext_service](../system-stored-procedures/sp-fulltext-service-transact-sql.md).

<a id="state"></a>

## Full-text index in inconsistent state after transaction log restored

When restoring the transaction log of a database, you might see a warning indicating that the full-text index isn't in a consistent state. This warning appears because the full-text index on a table was modified after the database was backed up. To bring the full-text index to a consistent state, you must run a full population (crawl) on the table. For more information, see [Populate Full-Text Indexes](populate-full-text-indexes.md).

## Related content

- [ALTER FULLTEXT CATALOG (Transact-SQL)](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md)
- [Populate Full-Text Indexes](populate-full-text-indexes.md)
