---
title: "Replication Monitor"
description: "Reference guide for replication monitor pages and dialog boxes in SQL Server, including monitoring Publishers, Distributors, publications, and subscriptions."
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 02/24/2026
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
ai-usage: ai-assisted
f1_keywords:
  - "sql13.rep.monitor.beta2.f1"
helpviewer_keywords:
  - "Replication Monitor, help"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replication monitor

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This section of the documentation includes information on the replication monitor. The pages and dialog boxes displayed in the monitor differ depending on the type of replication and the version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is monitored.

## Replication monitor overview

These pages provide the main monitoring interface and configuration options.

| Page | Description |
| --- | --- |
| [Replication monitor, main page](../../relational-databases/replication/replication-monitor-main-page.md) | The primary interface for monitoring all replication activity on monitored Publishers and Distributors. |
| [Add publisher](../../relational-databases/replication/add-publisher.md) | Adds a Publisher to be monitored in replication monitor. |
| [Distributor settings](../../relational-databases/replication/distributor-settings.md) | Configures settings for how replication monitor connects to the Distributor. |
| [Distributor information, publications](../../relational-databases/replication/distributor-information-publications.md) | Displays information about publications from the Distributor. The information that is displayed about the publications supported by the Distributor includes a column that contains the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance of the Publisher. |

## Publisher monitoring

These pages display information and settings for monitored Publishers.

| Page | Description |
| --- | --- |
| [Publisher settings](../../relational-databases/replication/publisher-settings.md) | Configures connection and refresh settings for a monitored Publisher. |
| [Publisher information, publications](../../relational-databases/replication/publisher-information-publications.md) | Displays a list of all publications on a Publisher with status and performance metrics. |
| [Publisher information, subscription watch list (Transactional publication)](../../relational-databases/replication/publisher-information-subscription-watch-list-transactional.md) | Displays subscriptions that require attention for Transactional publications. |
| [Publisher information, subscription watch list (Merge publication)](../../relational-databases/replication/publisher-information-subscription-watch-list-merge-publication.md) | Displays subscriptions that require attention for Merge publications. |
| [Publisher information, subscription watch list (Snapshot publication)](../../relational-databases/replication/publisher-information-subscription-watch-list-snapshot.md) | Displays subscriptions that require attention for Snapshot publications. |
| [Publisher information, agents](../../relational-databases/replication/publisher-information-agents.md) | Displays information about all replication agents associated with a Publisher. |

## Publication monitoring

These pages display detailed information about publications and their subscriptions.

| Page | Description |
| --- | --- |
| [Publication information, all subscriptions (Transactional publication)](../../relational-databases/replication/publication-information-all-subscriptions-transactional-publication.md) | Displays all subscriptions for a Transactional publication with synchronization status. |
| [Publication information, all subscriptions (Merge publication)](../../relational-databases/replication/publication-information-all-subscriptions-merge-publication.md) | Displays all subscriptions for a Merge publication with synchronization status. |
| [Publication information, all subscriptions (Snapshot publication)](../../relational-databases/replication/publication-information-all-subscriptions-snapshot-publication.md) | Displays all subscriptions for a Snapshot publication with synchronization status. |
| [Publication information, warnings (Transactional publication)](../../relational-databases/replication/publication-information-warnings-transactional-publication.md) | Displays and configures warnings for Transactional publications based on thresholds. |
| [Publication information, warnings (Merge publication)](../../relational-databases/replication/publication-information-warnings-merge-publication-sql-server-2005-and-later.md) | Displays and configures warnings for Merge publications based on thresholds. |
| [Publication information, warnings (Snapshot publication)](../../relational-databases/replication/publication-information-warnings-snapshot-publication-sql-server-2005-and-later.md) | Displays and configures warnings for Snapshot publications based on thresholds. |
| [Publication information, agents (Transactional publication)](../../relational-databases/replication/publication-information-agents-transactional-publication.md) | Displays information about agents associated with a Transactional publication. |
| [Publication information, agents (Merge publication)](../../relational-databases/replication/publication-information-agents-merge-publication.md) | Displays information about agents associated with a Merge publication. |
| [Publication information, agents (Snapshot publication)](../../relational-databases/replication/publication-information-agents-snapshot-publication.md) | Displays information about agents associated with a Snapshot publication. |
| [Publication information, tracer tokens (Transactional publication)](../../relational-databases/replication/publication-information-tracer-tokens-sql-server-2005-and-later.md) | Inserts and monitors tracer tokens to measure latency in transactional replication. |

## Subscription monitoring

These pages display detailed information about individual subscriptions.

| Page | Description |
| --- | --- |
| [Subscription, undistributed commands (Transactional subscription)](../../relational-databases/replication/subscription-undistributed-commands-transactional-subscription.md) | Displays the number of commands in the distribution database that haven't been delivered to the Subscriber. |
| [Subscription, publisher to distributor history (Transactional subscription)](../../relational-databases/replication/subscription-publisher-to-distributor-history-transactional-subscription.md) | Displays the history of transactions moving from Publisher to Distributor. |
| [Subscription, distributor to subscriber history (Transactional subscription)](../../relational-databases/replication/subscription-distributor-to-subscriber-history-transactional-subscription.md) | Displays the history of commands being delivered from Distributor to Subscriber. |
| [Subscription, synchronization history (Merge subscription)](../../relational-databases/replication/subscription-synchronization-history.md) | Displays the detailed synchronization history for a merge subscription. |
| [Subscription, distributor to subscriber history (Snapshot subscription)](../../relational-databases/replication/subscription-distributor-to-subscriber-history-snapshot-subscription.md) | Displays the history of snapshot delivery from Distributor to Subscriber. |

## Replication agents

These pages display information about individual replication agents.

| Page | Description |
| --- | --- |
| [Log reader agent](../../relational-databases/replication/log-reader-agent.md) | Displays status and history for the log reader agent that reads the transaction log. |
| [Queue reader agent](../../relational-databases/replication/queue-reader-agent.md) | Displays status and history for the queue reader agent used with queued updating subscriptions. |
| [Snapshot agent](../../relational-databases/replication/snapshot-agent.md) | Displays status and history for the snapshot agent that generates initial snapshots. |

## Filter and display options

These pages configure how data is filtered and displayed in Replication Monitor.

| Page | Description |
| --- | --- |
| [Filter settings](../../relational-databases/replication/filter-settings.md) | Configures filter criteria for displaying subscriptions and other replication data. |
| [Sort columns](../../relational-databases/replication/sort-columns.md) | Configures which columns are displayed and their sort order in replication monitor grids. |

## Related content

- [Start the replication monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)
