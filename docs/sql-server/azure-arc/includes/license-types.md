---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 05/22/2023
ms.topic: include
---

The following table identifies features enabled depending on license type:

|Feature  |License only <sup>*</sup> |License with Software Assurance<br/>or SQL subscription  |Pay-as-you-go  |
|---------|---------|---------|---------|
|[Connect to Azure](../connect.md) |Yes |Yes |Yes |
|[SQL Server inventory](../overview.md#manage-your-sql-servers-at-scale-from-a-single-point-of-control)|Yes |Yes |Yes |
|[Best practices assessment](../assess.md) |No |Yes |Yes |
|[Detailed database inventory](../view-databases.md#inventory-databases) |No |Yes |Yes |
|[Azure Active Directory authentication](../../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md) |Yes |Yes |Yes |
|[Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage)|Yes |Yes |Yes |
|[Govern through Microsoft Purview](/azure/purview/tutorial-register-scan-on-premises-sql-server)|Yes |Yes |Yes |
|[Automated backups](../point-in-time-restore.md)|No |Yes |Yes |
|[Automated patching](../patch.md)|No |Yes |Yes |

<sup>*</sup>License only includes SQL Server instances that are Developer, Express, Web or Evaluation Edition and instances using a Server/CAL license.
