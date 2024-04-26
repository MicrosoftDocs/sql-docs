---
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/16/2023
ms.topic: include
ms.custom: ignite-2023
---

The following table identifies features available by SQL Server version:

| Feature | 2012 | 2014 | 2016 | 2017 | 2019 | 2022 |
| ---- | ---- | ---- | ---- | ---- | ---- | ---- |
| [Azure pay-as-you-go billing](../manage-configuration.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Best practices assessment](../assess.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Migration assessment (preview)](../migration-assessment.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Detailed database inventory](../view-databases.md#inventory-databases) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Microsoft Entra ID authentication for SQL Server](../../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md) | No | No | No | No | No | Yes |
| [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Microsoft Purview: DevOps policies](/azure/purview/how-to-policies-devops-authoring-generic) | No | No | No | No | No | Yes |
| [Microsoft Purview: data owner policies (preview)](/azure/purview/how-to-policies-data-owner-authoring-generic) | No | No | No | No | No | Yes |
| [Automated backups to local storage (preview)](../backup-local.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Point-in-time-restore (preview)](../point-in-time-restore.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Automatic updates](../update.md) | Yes <sup>1</sup> | Yes | Yes | Yes | Yes | Yes |
| [Failover cluster instances (preview)](../support-for-fci.md)| Yes | Yes | Yes | Yes | Yes | Yes |
| [Always On availability groups (preview)](../manage-availability-group.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Monitoring (preview)](../sql-monitoring.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Operate with least privilege](../configure-least-privilege.md)| Yes | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> Requires subscription to [Extended Security Updates (ESU) enabled by Azure Arc](../../end-of-support/sql-server-extended-security-updates.md#subscribe-instances-for-esus).
