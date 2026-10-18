---
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 07/28/2025
ms.topic: include

---

The following table identifies settings, if they're enabled by default, and where the setting is configured:

| Setting Name                                                               | Default (Enabled or Disabled) | Can be disabled | Configuration location |
|----------------------------------------------------------------------------|----------------------------|-----------------|------------------------|
| [Extended security updates](../extended-security-updates.md)               | Disabled                   | Yes             | Extension              |
| [Least privilege mode](../configure-least-privilege.md)                    | Disabled                   | Yes             | Extension              |
| [Automated patching](../update.md)                                         | Disabled                   | Yes             | Extension               |
| [Best practices assessment](../assess.md)                                  | Disabled                   | Yes             | Extension               |
| [Microsoft Entra Authentication](../entra-authentication-setup-tutorial.md)| Disabled                   | Yes             | Extension per instance |
| [Purview](/purview/register-scan-azure-arc-enabled-sql-server)             | Disabled                   | Yes             | Extension, Instance    |
| [Automated backups](../backup-local.md)                                    | Disabled                   | Yes             | Instance, Database     |
| [Collect performance metrics (preview)](../sql-monitoring.md)              | Enabled                    | Yes             | Instance               |
| [Migration assessment](../migration-assessment.md)                        | Enabled                    | Yes             | Instance               |
| [Database migration](../migrate-to-azure-sql-managed-instance.md) | Enabled                   | No             | Extension               |
| [Availability Group discovery management](../manage-availability-group.md) | Enabled                    | Yes             | `AvailabilityGroupDiscovery` feature flag |
| [Extension log collection](../troubleshoot-deployment.md#log-file-locations) | Enabled                    | No              | Not configurable       |
| [SQL Server instance and DB discovery](../view-inventory.md)               | Enabled                    | No              | Not configurable       |
