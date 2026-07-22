---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 04/01/2026
ms.topic: include
ms.custom: ignite-2023
---
The following table identifies features available by operating system:

| Feature | Windows | Linux |
| --- | --- | --- |
| [Discover and register SQL Server instances in Azure](../prerequisites.md) | Yes | Yes |
| [Azure pay-as-you-go billing](../manage-configuration.md) | Yes | Yes <sup>2</sup> |
| [Install Azure extension for SQL Server during setup](../../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022) <sup>1</sup> | Yes | No |
| [Best practices assessment](../assess.md) | Yes | No |
| [Migration assessment](../migration-assessment.md) | Yes | No |
| [Database migration](../migrate-to-azure-sql-managed-instance.md) | Yes | No |
| [Detailed inventory](../view-inventory.md#inventory-databases) | Yes | No |
| [Microsoft Entra ID authentication](../../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md) <sup>1</sup> | Yes | Yes |
| [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage) | Yes | No |
| [Microsoft Purview](/azure/purview/tutorial-register-scan-on-premises-sql-server) | Yes | Yes |
| [Automated backups to local storage (preview)](../backup-local.md) | Yes | No |
| [Point-in-time-restore (preview)](../point-in-time-restore.md) | Yes | No |
| [Automatic updates](../update.md) | Yes | No |
| [SQL Server extended security updates](../../end-of-support/sql-server-extended-security-updates.md) | Yes | Not applicable |
| [Failover cluster instances](../support-for-fci.md) | Yes | Not applicable |
| [Always On availability groups](../manage-availability-group.md) | Yes | Not applicable |
| [Monitoring (preview)](../sql-monitoring.md) | Yes | No |
| [Client connection summary](../sql-connection-summary.md) | Yes | No |
| [Operate with least privilege](../configure-least-privilege.md)| Yes | No |

<sup>1</sup> [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] only.

<sup>2</sup> PAYG billing is supported on Linux with limitations. Passive instance detection, connected user verification, and Database Engine-level core visibility aren't available. All instances are billed as active. For details, see [Manage licensing and billing](../manage-license-billing.md).
