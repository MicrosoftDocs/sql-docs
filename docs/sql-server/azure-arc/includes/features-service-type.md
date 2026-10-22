---
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 02/26/2025
ms.topic: include
ms.custom:
---

The following table identifies features available by [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] service type:

| Feature | SQL Server Database Engine | SQL Server Integration Services | SQL Server Reporting Services | SQL Server Analysis Services | Power BI Report Server |
| --- | --- | --- | --- | --- | --- |
| [Connect to Azure Arc](../connect.md) | Yes | Yes | Yes | Yes | Yes |
| [Azure pay-as-you-go billing](../manage-configuration.md) | Yes | Yes | Yes | Yes | Yes |
| [ESU subscription](../manage-license-billing.md) | Yes | Yes | Yes | Yes | Yes |
| SQL Server inventory | Yes | Yes | Yes | Yes | Yes |
| [Best practices assessment](../assess.md) | Yes | No | No | No | No |
| [Migration readiness](../migration-assessment.md) | Yes | No | No | No | No |
| [Database migration](../migrate-to-azure-sql-managed-instance.md) | Yes | No | No | No | No |
| [Detailed inventory](../view-inventory.md#inventory-databases) | Yes | Not applicable | Not applicable | Not applicable | Not applicable |
| [Microsoft Entra ID authentication](../../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md)  | Yes <sup>1</sup> | No | No | No | No |
| [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage) | Yes | No | No | No | No |
| [Microsoft Purview: Govern using DevOps and data owner policies](/azure/purview/tutorial-register-scan-on-premises-sql-server) | Yes | No | No | No | No |
| [Automated backups to local storage (preview)](../backup-local.md) | Yes | No | No | No | No |
| [Point-in-time-restore](../point-in-time-restore.md) | Yes | No | No | No | No |
| [Automatic updates](../update.md) | Yes | Yes | Yes | Yes | Yes |
| [Failover cluster instances](../support-for-fci.md) | Yes | Not applicable | Not applicable | Not applicable | Not applicable |
| [Always On availability groups](../manage-availability-group.md) | Yes | Not applicable | Not applicable | Not applicable | Not applicable |
| [Monitoring (preview)](../sql-monitoring.md)  | Yes | No | No | No | No |
| [Client connection summary](../sql-connection-summary.md)  | Yes | No | No | No | No |
| [Operate with least privilege](../configure-least-privilege.md) | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] only.
