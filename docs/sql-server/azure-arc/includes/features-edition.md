---
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/17/2024
ms.topic: include
ms.custom:
  - ignite-2023
---

The following table identifies features available by [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] edition:

| Feature | Enterprise | Standard | Web | Express | Developer | Evaluation |
| --- | --- | --- | --- | --- | --- | --- |
| [Azure pay-as-you-go billing](../manage-configuration.md) | Yes | Yes | Not applicable | Not applicable | Not applicable | Not applicable |
| [Best practices assessment](../assess.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Migration readiness (preview)](../migration-assessment.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Detailed database inventory](../view-databases.md#inventory-databases) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Microsoft Entra authentication](../../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage) | Yes | Yes | Yes | Yes <sup>1</sup> | Yes | Yes |
| [Microsoft Purview: Govern using DevOps and data owner policies](/azure/purview/tutorial-register-scan-on-premises-sql-server) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Automated backups to local storage (preview)](../backup-local.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Point-in-time-restore](../point-in-time-restore.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Automatic updates](../update.md) | Yes | Yes | Yes | Yes | Yes | Yes |
| [Failover cluster instances](../support-for-fci.md) | Yes | Yes | Not applicable | Not applicable | Yes | Not applicable |
| [Always On availability groups](../manage-availability-group.md) | Yes | Yes | Not applicable | Not applicable | Yes | Not applicable |
| [Monitoring (preview)](../sql-monitoring.md) | Yes | Yes | No | No | No | No |
| [Operate with least privilege](../configure-least-privilege.md) | Yes | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> [Express LocalDB isn't supported.](/azure/purview/register-scan-on-premises-sql-server#supported-capabilities)
