---
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/15/2022
ms.service: sql
ms.subservice: linux
ms.topic: include
---
## <a id="pacemakerNotify"></a> Understand SQL Server resource agent for Pacemaker

[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] introduced `sequence_number` to `sys.availability_groups` to show if a replica marked as `SYNCHRONOUS_COMMIT` was up to date. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local availability group replica is with respect to the rest of the replicas in the availability group. Performing failovers, adding or removing replicas, and other availability group operations update this number. The number is updated on the primary, then pushed to secondary replicas. Thus a secondary replica that is up-to-date will have the same sequence_number as the primary.

When Pacemaker decides to promote a replica to primary, it first sends a notification to all replicas to extract the sequence number and store it (we call this the pre-promote notification). Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers from all replicas and rejects the promote operation otherwise. In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss.

This is only guaranteed to work as long as at least one replica available for promotion has the same sequence number as the previous primary. To ensure this, the default behavior is for the Pacemaker resource agent to automatically set `REQUIRED_COPIES_TO_COMMIT` such that at least one synchronous commit secondary replica is up to date and available to be the target of an automatic failover. With each monitoring action, the value of `REQUIRED_COPIES_TO_COMMIT` is computed (and updated if necessary)  as ('number of synchronous commit replicas' / 2). Then, at failover time, the resource agent will require (`total number of replicas` - `required_copies_to_commit` replicas) to respond to the pre-promote notification to be able to promote one of them to primary. The replica with the highest `sequence_number` will be promoted to primary.

For example, let's consider the case of an availability group with three synchronous replicas - one primary replica and two synchronous commit secondary replicas.

- `REQUIRED_COPIES_TO_COMMIT` is 3 / 2 = 1

- The required number of replicas to respond to pre-promote action is 3 - 1 = 2. So 2 replicas have to be up for the failover to be triggered. This means that, in the case of primary outage, if one of the secondary replicas is unresponsive and only one of the secondaries responds to the pre-promote action, the resource agent can't guarantee that the secondary that responded has the highest sequence_number, and a failover isn't triggered.

A user can choose to override the default behavior, and configure the availability group resource to not set `REQUIRED_COPIES_TO_COMMIT` automatically as above.

> [!IMPORTANT]  
>When `REQUIRED_COPIES_TO_COMMIT` is 0 there is risk of data loss. In the case of an outage of the primary, the resource agent will not automatically trigger a failover. The user has to decide if they want to wait for primary to recover or manually fail over.

To set `REQUIRED_COPIES_TO_COMMIT` to 0, run:

```bash
sudo pcs resource update <ag_cluster> required_copies_to_commit=0
```

The equivalent command using crm (on SLES) is:

```bash
sudo crm resource param <ag_cluster> set required_synchronized_secondaries_to_commit 0
```

To revert to default computed value, run:

```bash
sudo pcs resource update <ag_cluster> required_copies_to_commit=
```

> [!NOTE]  
>Updating resource properties causes all replicas to stop and restart. This means primary will temporarily be demoted to secondary, then promoted again which will cause temporary write unavailability. The new value for REQUIRED_COPIES_TO_COMMIT will only be set once replicas are restarted, so it won't be instantaneous with running the pcs command.

## Balance high availability and data protection

The above default behavior applies to the case of 2 synchronous replicas (primary + secondary) as well. Pacemaker will default `REQUIRED_COPIES_TO_COMMIT = 1` to ensure the secondary replica is always up to date for maximum data protection.

> [!WARNING]  
> This comes with higher risk of unavailability of the primary replica due to planned or unplanned outages on the secondary. The user can choose to change the default behavior of the resource agent and override the `REQUIRED_COPIES_TO_COMMIT` to 0:

```bash
sudo pcs resource update <ag1> required_copies_to_commit=0
```

Once overridden, the resource agent will use the new setting for `REQUIRED_COPIES_TO_COMMIT` and stop computing it. This means that users have to manually update it accordingly (for example, if they increase the number of replicas).

The tables below describes the outcome of an outage for primary or secondary replicas in different availability group resource configurations:

### Availability group - 2 sync replicas

| |Primary outage |One secondary replica outage
|:---|:--- |:--- |
|`REQUIRED_COPIES_TO_COMMIT=0`|User has to issue a manual FAILOVER.<br />Might have data loss.<br />New primary is R/W |Primary is R/W, running exposed to data loss
|`REQUIRED_COPIES_TO_COMMIT=1` <sup>1</sup> |Cluster will automatically issue FAILOVER<br />No data loss.<br />New primary will reject all connections until former primary recovers and joins availability group as secondary. |Primary will reject all connections until secondary recovers.

<sup>1</sup> SQL Server resource agent for Pacemaker default behavior.

### Availability group - 3 sync replicas

| |Primary outage |One secondary replica outage
|:---|:--- |:--- |
|`REQUIRED_COPIES_TO_COMMIT=0`|User has to issue a manual FAILOVER.<br />Might have data loss.<br />New primary is R/W |Primary is R/W
|`REQUIRED_COPIES_TO_COMMIT=1` <sup>1</sup> |Cluster will automatically issue FAILOVER.<br />No data loss.<br />New primary is RW |Primary is R/W

<sup>1</sup> SQL Server resource agent for Pacemaker default behavior.
