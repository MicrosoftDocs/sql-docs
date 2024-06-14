---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 05/22/2023
ms.topic: include
ms.custom: ignite-2023
---

The following table identifies features enabled depending on license type:

|Feature  |License only <sup>1</sup> |License with Software Assurance<br/>or SQL subscription  |Pay-as-you-go  |
|---------|---------|---------|---------|
|[Connect to Azure](../connect.md) |Yes |Yes |Yes |
|[SQL Server inventory](../overview.md#manage-your-sql-server-instances-at-scale-from-a-single-point-of-control)|Yes |Yes |Yes |
|[Best practices assessment](../assess.md) |No |Yes |Yes |
|[Migration assessment (preview)](../migration-assessment.md) |Yes |Yes |Yes |
|[Detailed database inventory](../view-databases.md#inventory-databases) |Yes |Yes |Yes |
|[Microsoft Entra ID authentication](../../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md) |Yes |Yes |Yes |
|[Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage)|Yes |Yes |Yes |
|[Govern through Microsoft Purview](/azure/purview/tutorial-register-scan-on-premises-sql-server)|Yes |Yes |Yes |
|[Automated backups to local storage (preview)](../backup-local.md)|No |Yes |Yes |
|[Point-in-time-restore (preview)](../point-in-time-restore.md)|No |Yes |Yes |
|[Automatic updates](../update.md)|No |Yes |Yes |
|[Failover cluster instances](../support-for-fci.md) |Yes | Yes | Yes|
|[Always On availability groups (preview)](../manage-availability-group.md) |Yes | Yes | Yes|
|[Monitoring (preview)](../sql-monitoring.md) |No |Yes |Yes|
|[Operate with least privilege](../configure-least-privilege.md)|Yes | Yes | Yes|

<sup>1</sup> License only includes SQL Server instances that are Developer, Express, Web, or Evaluation Edition and instances using a Server/CAL license.
