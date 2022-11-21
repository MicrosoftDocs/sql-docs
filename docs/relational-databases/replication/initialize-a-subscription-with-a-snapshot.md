---
description: "Initialize a Subscription with a Snapshot for a New Publication"
title: Initialize subscription with snapshot
ms.custom:
ms.date: 03/23/2020
ms.service: sql
ms.reviewer:
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords:
  - "snapshots [SQL Server replication], initializing subscriptions"
  - "initializing subscriptions [SQL Server replication], snapshots"
ms.assetid: 77a9ade2-cdc0-4ae9-a02d-6e29d7c2ada0
author: MashaMSFT
ms.author: mathoma
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Initialize a Subscription with a Snapshot for a New Publication

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes the processes that occur when a replication publication is initialized. An initial snapshot is applied to the Subscribers.

## Snapshot for a new publication

By default, a snapshot is captured after a publication is created.
The snapshot is copied to the snapshot folder. This default behavior occurs for merge publications that are created by using the New Publication Wizard.

### Snapshot is applied to Subscriber

The new snapshot is applied to the Subscriber by an agent. The applying occurs during the initial synchronization of the subscription. Which agent does the applying depends on the type of publication:

- For _transactional_ and _snapshot_ publications:
  - The Distribution Agent.

- For _merge_ publications:
  - The Merge Agent.

### Type of publication

The following table displays the contents of the snapshot, for each type of publication.

&nbsp;

| Publication type that the snapshot is for | Contents of the snapshot |
| :---------------------------------------- | :----------------------- |
| <ul> <li>Snapshot publication</li> <li>Transactional publication</li> <li>Merge publication that doesn't use parameterized filters</li> </ul> | <ul> <li>Schema</li> <li>Data, in files for the bulk copy program (BCP)</li> <li>Constraints</li> <li>Extended properties</li> <li>Indexes</li> <li>Triggers</li> <li>System tables needed for replication</li> </ul> <br/>See [Create and Apply the Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md). |
| <ul> <li>Merge publication that does use parameterized filters</li> </ul> | <ul> <li>Schema snapshot (replication scripts, published objects, but no data)</li> <li>Data that belongs to the subscription's partition</li> </ul> <br/>See [Snapshots for Merge Publications with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md). |

#### Two-part process with merge publication that uses parameterized filters

For a merge publication that uses parameterized filters, the snapshot is created by using the following two-part process:

1. A schema snapshot is created, which contains the following items:
   - Replication scripts.
   - Schema of the published objects.
   - _(But no data.)_

2. Each subscription is then initialized with a snapshot. The snapshot includes the following items:
   - Scripts and schema, copied from the schema snapshot.
   - Data that belongs to the subscription's partition.

## Type of replication

The file types contained in the snapshot depend on the type of replication, and on the articles in your publication.

&nbsp;

| Type of Replication | Common Snapshot Files |
| :------------------ | :-------------------- |
| Snapshot Replication, or<br/>Transactional Replication | &bullet; Schema (.sch) <br/>&bullet; Data (.bcp) <br/>&bullet; Constraints and indexes (.dri) <br/>&bullet; Compressed snapshot files (.cab) <br/>&bullet; Triggers (.tag), only to update a Subscriber <br/><br/>&bullet; Constraints (.idx). |
| Merge Replication                                      | &bullet; Schema (.sch) <br/>&bullet; Data (.bcp) <br/>&bullet; Constraints and indexes (.dri) <br/>&bullet; Compressed snapshot files (.cab) <br/>&bullet; Triggers (.trg) <br/><br/>&bullet; System table data (.sys) <br/>&bullet; Conflict tables (.cft). |

### Snapshot folder

The files are transferred by being copied to the default _snapshot folder_, or to the _alternate folder_ for snapshots.

The snapshot folder is specified when the Distributor is configured. The alternate folder is specified when the publication is created.

### Resume transfer after interruption

Transfer of files to a snapshot folder automatically resumes if the transfer is interrupted by an unreliable connection.

For efficiency, the resumption does not resend any files that were already fully transferred before the interruption.

## Snapshot Options

There are several options available when initializing a subscription with a snapshot. You can:

- Specify an alternate snapshot folder location instead of, or in addition, to the default snapshot folder location. For more information, see [Modify snapshot options](../../relational-databases/replication/snapshot-options.md).

- Compress snapshots for storage on removable media or for transfer over a slow network. For more information, see [Compressed Snapshots](../../relational-databases/replication/snapshot-options.md#compressed-snapshots).

- Execute Transact-SQL scripts before or after the snapshot is applied. For more information, see [Execute Scripts Before and After the Snapshot Is Applied](../../relational-databases/replication/snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied).

- Transfer snapshot files using File Transfer Protocol (FTP). For more information, see [Transfer Snapshots Through FTP](../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md).

## See Also

[Initialize a Subscription](../../relational-databases/replication/initialize-a-subscription.md)

[Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md)
