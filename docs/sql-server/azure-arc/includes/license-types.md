---
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/03/2024
ms.topic: include
ms.custom:
  - ignite-2023
---

The following table identifies the features that are enabled for each license type:

| Feature | License only <sup>1</sup> | License with Software Assurance<br />or SQL Server subscription | Pay-as-you-go |
| --- | --- | --- | --- |
| [Connect your SQL Server to Azure Arc](../connect.md) | Yes | Yes | Yes |
| [SQL Server Extended Security Updates enabled by Azure Arc](../extended-security-updates.md) | Yes | Yes | Yes |
| [SQL Server inventory](../overview.md#manage-your-sql-server-instances-at-scale-from-a-single-point-of-control) | Yes | Yes | Yes |
| [Best practices assessment](../assess.md) | No | Yes | Yes |
| [Migration readiness (preview)](../migration-assessment.md) | Yes | Yes | Yes |
| [Detailed database inventory](../view-databases.md#inventory-databases) | Yes | Yes | Yes |
| [Microsoft Entra authentication](../../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md) | Yes | Yes | Yes |
| [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage) | Yes | Yes | Yes |
| [Govern through Microsoft Purview](/azure/purview/tutorial-register-scan-on-premises-sql-server) | Yes | Yes | Yes |
| [Automated backups to local storage (preview)](../backup-local.md) | No | Yes | Yes |
| [Point-in-time restore](../point-in-time-restore.md) | No | Yes | Yes |
| [Automatic updates](../update.md) | No | Yes | Yes |
| [Failover cluster instances](../support-for-fci.md) | Yes | Yes | Yes |
| [Always On availability groups](../manage-availability-group.md) | Yes | Yes | Yes |
| [Monitoring (preview)](../sql-monitoring.md) | No | Yes | Yes |
| [Operate with least privilege](../configure-least-privilege.md) | Yes | Yes | Yes |

<sup>1</sup> The license-only option includes SQL Server instances that are Developer, Express, Web, or Evaluation edition and instances that use a Server+CAL license.
