---
author: AttinderPalSingh
ms.author: atsingh
ms.reviewer: amitkh
ms.date: 01/02/2026
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
<a id="pacemakerNotify"></a>

## Understand SQL Server resource agent for Pacemaker

[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] introduced `sequence_number` to `sys.availability_groups` to show if a replica marked as `SYNCHRONOUS_COMMIT` is up to date. `sequence_number` is a monotonically increasing **bigint** that represents how up-to-date the local availability group replica is with respect to the rest of the replicas in the availability group.

This number updates when you perform failovers, add or remove replicas, and other availability group operations.

The primary replica updates the number and then pushes it to secondary replicas. A secondary replica that's up-to-date has the same `sequence_number` as the primary.

When Pacemaker decides to promote a replica to primary, it first sends a notification to all replicas to extract the sequence number and store it. This notification is called the pre-promote notification. Next, when Pacemaker tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers from all replicas. Otherwise, it rejects the promotion operation. By using this process, only the replica with the highest sequence number can be promoted to primary, ensuring no data loss.

Promotion works as long as at least one replica available for promotion has the same sequence number as the previous primary. The default behavior is for the Pacemaker resource agent to automatically set `REQUIRED_COPIES_TO_COMMIT` so that at least one synchronous commit secondary replica is up to date and available as the target of an automatic failover. With each monitoring action, the value of `REQUIRED_COPIES_TO_COMMIT` is computed (and updated if necessary) as ('number of synchronous commit replicas' / 2). Then, at failover time, the resource agent requires (`total number of replicas` - `required_copies_to_commit` replicas) to respond to the pre-promote notification to be able to promote one of them to primary. The replica with the highest `sequence_number` is promoted to primary.

For example, consider the case of an availability group with three synchronous replicas - one primary replica and two synchronous commit secondary replicas.

- `REQUIRED_COPIES_TO_COMMIT` is 3 / 2 = 1

- The required number of replicas to respond to pre-promote action is 3 - 1 = 2. So two replicas have to be up for the failover to be triggered. When a primary outage occurs, if one of the secondary replicas is unresponsive and only one of the secondaries responds to the pre-promote action, the resource agent can't guarantee that the secondary that responded has the highest `sequence_number`, and a failover isn't triggered.

You can override the default behavior and configure the availability group resource to not set `REQUIRED_COPIES_TO_COMMIT` automatically.

> [!IMPORTANT]  
> When `REQUIRED_COPIES_TO_COMMIT` is `0`, you risk data loss. If there's an outage of the primary, the resource agent doesn't automatically trigger a failover. You must choose to wait for primary to recover, or manually fail over.

To set `REQUIRED_COPIES_TO_COMMIT` to `0`, run:

```bash
sudo pcs resource update <ag_cluster> required_copies_to_commit=0
```

The equivalent command using **crm** (on SUSE Linux Enterprise Server) is:

```bash
sudo crm resource param <ag_cluster> set required_synchronized_secondaries_to_commit 0
```

To revert to default computed value, run:

```bash
sudo pcs resource update <ag_cluster> required_copies_to_commit=
```

> [!NOTE]  
> Updating resource properties causes all replicas to stop and restart. This change temporarily demotes the primary to secondary, then promotes it again, which causes temporary write unavailability. The new value for `REQUIRED_COPIES_TO_COMMIT` is set only after replicas restart, so it isn't instantaneous with running the **pcs** command.

## Balance high availability and data protection

The default behavior described earlier also applies to the case of two synchronous replicas (primary and secondary). Pacemaker sets `REQUIRED_COPIES_TO_COMMIT` to `1` to ensure the secondary replica is always up to date for maximum data protection.

> [!WARNING]  
> This setting comes with a higher risk of unavailability of the primary replica due to planned or unplanned outages on the secondary. You can choose to change the default behavior of the resource agent and override the `REQUIRED_COPIES_TO_COMMIT` value to `0`:

```bash
sudo pcs resource update <ag1> required_copies_to_commit=0
```

When you override this value, the resource agent uses the new setting for `REQUIRED_COPIES_TO_COMMIT` and stops computing it. You must manually update it if needed (for example, if you increase the number of replicas).

The following tables describe the outcome of an outage for primary or secondary replicas in different availability group resource configurations:

### Availability group - two sync replicas

| Configuration | Primary outage | One secondary replica outage |
| --- | --- | --- |
| `REQUIRED_COPIES_TO_COMMIT = 0` | You must manually issue a `FAILOVER`.<br /><br />Can cause data loss.<br /><br />New primary is R/W. | Primary is R/W, running exposed to data loss. |
| `REQUIRED_COPIES_TO_COMMIT = 1` <sup>1</sup> | Cluster automatically issues `FAILOVER`<br /><br />No data loss.<br /><br />New primary rejects all connections until former primary recovers and joins availability group as secondary. | Primary rejects all connections until secondary recovers. |

<sup>1</sup> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] resource agent for Pacemaker default behavior.

### Availability group - three sync replicas

| Configuration | Primary outage | One secondary replica outage |
| --- | --- | --- |
| `REQUIRED_COPIES_TO_COMMIT = 0` | You must manually issue a `FAILOVER`.<br /><br />Can cause data loss.<br /><br />New primary is R/W. | Primary is R/W. |
| `REQUIRED_COPIES_TO_COMMIT = 1` <sup>1</sup> | Cluster automatically issues `FAILOVER`.<br /><br />No data loss.<br /><br />New primary is R/W. | Primary is R/W. |

<sup>1</sup> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] resource agent for Pacemaker default behavior.
