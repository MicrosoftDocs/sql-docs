---
title: "SQL Server Management Studio Replication Dialog Boxes"
description: A reference for articles that describe the various dialog boxes for Replication within SQL Server Management Studio.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 02/24/2026
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
ai-usage: ai-assisted
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# SQL Server Management Studio replication dialog boxes

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This section includes information on the replication dialog boxes that are available in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

## Agent security

These dialog boxes configure security settings for the replication agents that transfer data between publishers and subscribers.

| Dialog box | Description |
| --- | --- |
| [Snapshot agent security](snapshot-agent-security.md) | Configure security for the account under that runs the snapshot agent makes connections. |
| [Log reader agent security](log-reader-agent-security.md) | Configure security settings for the log reader agent that reads the transaction log. |
| [Distribution agent security](distribution-agent-security.md) | Configure security for the account that runs the distribution agent and connects to subscribers. |
| [Merge agent security](merge-agent-security.md) | Configure security for the merge agent that synchronizes data in merge replication. |
| [Queue reader agent security](queue-reader-agent-security.md) | Configure the queue reader agent for queued updating subscriptions. |

## Agent profiles

These dialog boxes manage agent profiles that control replication agent behavior and performance.

| Dialog box | Description |
| --- | --- |
| [Agent profiles (single agent)](agent-profiles-single-agent.md) | View and modify profiles for a single replication agent. |
| [Agent profiles](agent-profiles.md) | Manage all replication agent profiles on the Distributor. |
| [\<AgentProfileName\> properties](agentprofilename-properties.md) | View and modify the properties of a specific agent profile. |
| [New agent profile](new-agent-profile.md) | Create a new replication agent profile with custom parameters. |

## Subscription validation

These dialog boxes validate that subscriber data matches publisher data to ensure replication consistency.

| Dialog box | Description |
| --- | --- |
| [Validate all subscriptions](validate-all-subscriptions.md) | Validate data for all subscriptions to a publication. |
| [Validate subscriptions](validate-subscriptions.md) | Select multiple subscriptions to validate at once. |
| [Validate subscription](validate-subscription.md) | Validate a single subscription's data against the publisher. |
| [Subscription validation options (Transactional subscriptions)](subscription-validation-options-transactional-subscriptions.md) | Configure row count or checksum validation options for transactional replication. |
| [Subscription validation options (Merge subscriptions)](subscription-validation-options-merge-subscriptions.md) | Configure validation options specific to merge replication subscriptions. |

## Subscription reinitialization

These dialog boxes reinitialize subscriptions with a new snapshot when data resynchronization is required.

| Dialog box | Description |
| --- | --- |
| [Reinitialize subscription(s) - all subscriptions](reinitialize-subscription-s-all-subscriptions.md) | Reinitialize all subscriptions to a publication with a new snapshot. |
| [Reinitialize subscription(s) - one subscription](reinitialize-subscription-s-one-subscription.md) | Reinitialize a single subscription with a new snapshot. |

## Scripting and connectivity

These dialog boxes generate replication scripts and configure connections to Oracle publishers.

| Dialog box | Description |
| --- | --- |
| [Generate SQL script (replication objects)](generate-sql-script-replication-objects.md) | Generate Transact-SQL scripts for creating and dropping replication objects. |
| [Connect to server (Oracle), login](connect-to-server-oracle-login.md) | Enter login credentials to connect to an Oracle Publisher. |
| [Connect to server (Oracle), connection properties](connect-to-server-oracle-connection-properties.md) | Configure connection properties such as timeout values for Oracle connections. |

## Related content

- [SQL Server replication](sql-server-replication.md)
