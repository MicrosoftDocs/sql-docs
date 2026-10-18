---
title: Example of Merge Conflict Resolution Based on Subscription Type and Assigned Priorities
description: Walk through an example that describes a series of updates to a row over several merge synchronizations.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 07/18/2025
ms.service: sql
ms.subservice: replication
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "merge replication [SQL Server replication], merge conflict resolution example"
---
# Example of merge conflict resolution based on subscription type and assigned priorities

To understand how conflicts resolve according to assigned priority values and whether a subscription is a server or client subscription, consider the following example, which describes a series of updates to a row over several merge synchronizations.

Here are the initial priority values for four sites in a basic merge replication topology (one Publisher, two Subscribers with server subscriptions, and one Subscriber with a client subscription).

| Site | Type | Priority value |
| --- | --- | --- |
| A | Publisher | 100.00 |
| B | Server Subscription | 75.00 (assigned) |
| C | Server Subscription | 50.00 (assigned) |
| D | Client Subscription | 0.00 (default) |

## Phase 1: Initial values

Initially, Site A (the Publisher) creates version one of the row containing value='Nebraska', which is replicated to Sites B, C, and D during the next merge synchronization. After synchronization, here are the values for the row.

| Site | Type | Row value |
| --- | --- | --- |
| A (Publisher) | 100.00 | Nebraska |
| B (Server Subscription) | 75.00 | Nebraska |
| C (Server Subscription) | 50.00 | Nebraska |
| D (Client Subscription) | 0.00 | Nebraska |

## Phase 2: Publisher and subscriber with server subscription both update row

Site A updates the row value to Texas and site B updates the row value to New Jersey. When the next merge synchronization occurs, there's a conflict between sites A and B. Site A wins the conflict. The conflict winner value from site A is propagated to sites B, C, and D.

| Site | Priority value | Row value |
| --- | --- | --- |
| A (Publisher) | 100.00 | Texas |
| B (Server Subscription) | 75.00 | Texas |
| C (Server Subscription) | 50.00 | Texas |
| D (Client Subscription) | 0.00 | Texas |

## Phase 3: Multiple changes made to the same row

Suppose site C updates the row (changes it to North Carolina) and synchronizes with the Publisher. This isn't a conflict because C already successfully merged the last update from A (with the row value='Texas' successfully merged). Then suppose Site B updates the row (changes it to Idaho).

| Site | Priority value | Row value |
| --- | --- | --- |
| A (Publisher) | 100.00 | North Carolina |
| B (Server Subscription) | 75.00 | Idaho |
| C (Server Subscription) | 50.00 | North Carolina |
| D (Client Subscription) | 0.00 | Texas |

When site B synchronizes with the Publisher, there's an update conflict. Because both B and C are server subscriptions and the priority of B is greater than that of C, site B wins the conflict. After the other two sites are also merged, the value of B is propagated to the other Subscribers.

| Site | Priority value | Row value |
| --- | --- | --- |
| A (Publisher) | 100.00 | Idaho |
| B (Server Subscription) | 75.00 | Idaho |
| C (Server Subscription) | 50.00 | Idaho |
| D (Client Subscription) | 0.00 | Idaho |

## Phase 4: Subscribers with server and client subscriptions both update row

Suppose site D updates the row (changes it to New Mexico) and synchronizes with the Publisher. Then suppose Site B updates the row (changes it to California).

| Site | Priority value | Row value |
| --- | --- | --- |
| A (Publisher) | 100.00 | New Mexico |
| B (Server Subscription) | 75.00 | California |
| C (Server Subscription) | 50.00 | Idaho |
| D (Client Subscription) | 0.00 | New Mexico |

When site B synchronizes with the Publisher, there's an update conflict. Unlike the previous example, because D has a client subscription, it assumes the priority value of the Publisher (site A) upon synchronization. Because the priority of A is greater than B, B loses the conflict; the value initially entered into D wins. (Had Subscriber B synchronized with A before Subscriber D did, site B would have won the conflict.) Site D winning the conflict relies on the Publisher not having made a change or received another change since the version of the row updated at Site D was last synchronized. If any Subscriber with a server subscription or any other Subscriber with a client subscription synchronizes first, the rule of highest priority or "first in to the Publisher wins" is followed.)

The final values after all the sites are synchronized, are shown here.

| Site | Priority value | Row value |
| --- | --- | --- |
| A (Publisher) | 100.00 | New Mexico |
| B (Server Subscription) | 75.00 | New Mexico |
| C (Server Subscription) | 50.00 | New Mexico |
| D (Client Subscription) | 0.00 | New Mexico |

Synchronization order and priority value determine the outcome of conflicts when mixing server and client subscriptions at the same level in your topology. This last set of updates illustrates why caution must be exercised. Although the Subscriber had the lowest priority value of the three Subscribers, it won the conflict because it synchronized with the Publisher (thus assuming the Publisher priority value of 100.00) first. Had site C (server subscription with a priority value of 50.00) entered New Mexico instead of site D, site B (server subscription with a priority value of 75.00) would have won the conflict, and the result would have been California.

## Related content

- [How merge replication detects and resolves conflicts](merge-replication-detect-resolve-conflicts.md)
