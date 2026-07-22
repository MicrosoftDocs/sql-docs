---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 06/22/2026
ms.topic: include
ms.custom: ignite-2023
---

The following table identifies features available by SQL Server version:

| Feature availability based on SQL Server version | 2014 | 2016 <br /> 2017 <br /> 2019 | 2022 | 2025 |
|--|--|--|--|--|
| [Azure pay-as-you-go billing](../manage-configuration.md) | Yes | Yes | Yes | Yes |
| [Best practices assessment](../assess.md) | Yes | Yes | Yes | Yes |
| [Migration assessment](../migration-assessment.md) | Yes | Yes | Yes | Yes |
| [Database migration](../migrate-to-azure-sql-managed-instance.md) | LRS only | LRS & MI link | LRS & MI link | LRS & MI link |
| [Detailed inventory](../view-inventory.md#inventory-databases) | Yes | Yes | Yes | Yes |
| [Microsoft Entra ID authentication for SQL Server](../../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md) | No | No | Yes | Yes |
| [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage) | Yes | Yes | Yes | Yes |
| [Microsoft Purview: DevOps policies](/azure/purview/how-to-policies-devops-authoring-generic) | No | No | Yes | No |
| [Microsoft Purview: data owner policies (preview)](/purview/legacy/how-to-policies-data-owner-authoring-generic) | No | No | Yes | No |
| [Automated backups to local storage (preview)](../backup-local.md) | Yes | Yes | Yes | Yes |
| [Point-in-time-restore (preview)](../point-in-time-restore.md) | Yes | Yes | Yes | Yes |
| [Automatic updates](../update.md) | Yes <sup>1</sup> | Yes | Yes | Yes |
| [Failover cluster instances](../support-for-fci.md) | Yes | Yes | Yes | Yes |
| [Always On availability groups](../manage-availability-group.md) | Yes | Yes | Yes | Yes |
| [Monitoring (preview)](../sql-monitoring.md) | No | Yes <sup>2</sup> | Yes | Yes |
| [Client connection summary](../sql-connection-summary.md) | No | Yes <sup>2</sup> | Yes | Yes |
| [Operate with least privilege](../configure-least-privilege.md) | Yes | Yes | Yes | Yes |

<sup>1</sup> Requires subscription to [Extended Security Updates (ESU) enabled by Azure Arc](../../end-of-support/sql-server-extended-security-updates.md#subscribe-to-esus-for-sql-server-instances) for [!INCLUDE [sssql14-md](../../../includes/sssql14-md.md)].

<sup>2</sup> Requires [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] SP1 or later versions. For more information, see [prerequisites](../../azure-arc/sql-monitoring.md#prerequisites). 




