---
title: "Replication Backward Compatibility"
description: Review these resources for backward compatibility in replication before you upgrade or if you have several versions of SQL Server in a replication topology.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 09/28/2022
ms.service: sql
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords:
  - "transactional replication, backward compatibility"
  - "backward compatibility [SQL Server replication]"
  - "merge replication backward compatibility [SQL Server replication]"
  - "replication [SQL Server], backward compatibility"
  - "backward compatibility [SQL Server], replication"
  - "snapshot replication [SQL Server], backward compatibility"
  - "compatibility [SQL Server replication]"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replication backward compatibility

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Backward compatibility is important to understand if you are upgrading, or if you have more than one version of SQL Server in a replication topology.

The general rules are:

- A Distributor can be any version as long as it is greater than or equal to the Publisher version (in many cases the Distributor is the same instance as the Publisher).
- A Publisher can be any version as long as it less than or equal to the Distributor version.
- Subscriber version depends on the type of publication:
  - A Subscriber to a transactional publication can be any version within two versions of the Publisher version. For example: a SQL Server 2012 (11.x) Publisher can have SQL Server 2014 (12.x) and SQL Server 2016 (13.x) Subscribers; and a SQL Server 2016 (13.x) Publisher can have SQL Server 2014 (12.x) and SQL Server 2012 (11.x) Subscribers.     
  - A Subscriber to a merge publication can be all versions equal to or lower than the Publisher version which are supported as per the versions life cycle support cycle.

## Replication matrix

[!INCLUDE[repl matrix](../../includes/replication-compat-matrix.md)]

## Next steps

- [Deprecated Features in SQL Server Replication](../../relational-databases/replication/deprecated-features-in-sql-server-replication.md)

  Replication features that have been retained in newer versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for backward compatibility, but, which will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- [Breaking Changes in SQL Server Replication](../../relational-databases/replication/breaking-changes-in-sql-server-replication.md)  

  Replication feature changes that might require changes to applications.

- [Upgrade Replicated Databases](../../database-engine/install-windows/upgrade-replicated-databases.md)  

  Steps and considerations when upgrading SQL Servers participating in a replication topology.
