---
title: Restore your data
description: Learn how to restore your Azure database to an appropriate recovery point after a ransomware attack.
author: sudhanshukhera
ms.author: skhera
ms.title: Step 4 - Restore - Recover your data
ms.date: 10/31/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Step 4: Restore: Recover your data

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Use the findings from [Step 3: Assess](ransomware-recovery-assess.md) to decide whether you need a data restore, choose the recovery pattern for the scope of the compromise, and restore to a clean point.

## Determine whether a data restore is required

Review the damage recorded in [Step 3.5](ransomware-recovery-assess.md#35-determine-what-the-attacker-did) before selecting a restore path:

- **Data theft only:** If the investigation confirms that data integrity and availability are unchanged, a data restore isn't required. Complete the applicable [scope-based cleanup](#recovery-posture-by-scope), then continue to [Step 5: Validate](ransomware-recovery-validate.md) and [Step 6: Harden](ransomware-recovery-harden.md).
- **Key access compromised, data intact:** Restore access to the required TDE protector by using the guidance in [Step 3.5](ransomware-recovery-assess.md#35-determine-what-the-attacker-did). A data restore might not be necessary, but scope-based cleanup and validation are still required.
- **Recovery options compromised:** Confirm which backups and other validated recovery artifacts remain available and restorable before selecting a recovery path. Don't rely on configured retention policies alone.
- **Data altered, deleted, or corrupted:** Continue with the applicable restore procedure and validate the recovered data before reconnecting production traffic.

How much you can recover depends on the backup retention configured before the attack: PITR reaches back up to 35 days, LTR up to 10 years, and neither can be extended retroactively. If the attacker still has active access when you restore, the same access path remains available and can compromise the restored database again.

Use the approved recovery cutoff from Step 3. Select a restore point before that cutoff, applying an extra safety margin when audit evidence is incomplete. Record the chosen point and validate it against technical and business records before reconnecting production traffic.

> [!IMPORTANT]
> Close the identified attacker entry path before restoring databases. For tenant-level compromise, obtain explicit clearance from the identity-plane incident lead before resuming database recovery.

Address the compromised credentials, network access, and role assignments identified in [Step 3: Assess](ransomware-recovery-assess.md). Complete the required checks in [Step 5: Validate](ransomware-recovery-validate.md) before reconnecting production traffic. After production traffic is restored, complete the remaining post-incident actions in [Step 6: Harden](ransomware-recovery-harden.md).

## First: if the compromise is tenant-level, clear the identity plane

If Step 3.4 Check 1 matched, don't restore any database or managed instance yet. The attacker might control Microsoft Entra ID itself. In this case, the same access path remains available and can compromise a restored database again. The identity-plane incident lead must clear that access before resource-level recovery resumes. Engage Microsoft Incident Response if needed.

Your CISO or security team leads this branch. Engage Microsoft Incident Response if needed. As the database administrator, your role is to preserve evidence and pause data-plane recovery.

### Recover tenant administrator access

If no administrator can sign in, including emergency access accounts, contact [Microsoft Support by phone](https://support.microsoft.com/topic/customer-service-phone-numbers-c0389ade-5640-e588-8b0e-28de8afeb3f2) and provide the [incident details listed in Step 2](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation).

Follow the [tenant-lockout recovery guidance](/entra/architecture/recoverability-tenant#recover-from-customer-microsoft-entra-id-tenant-lockout) and complete the tenant-ownership verification requested by Microsoft Support.

Restoring administrator access doesn't establish that the tenant is free of attacker access. Continue the tenant-compromise response below and obtain explicit clearance from the identity-plane incident lead before resuming database recovery.

### Contain tenant-level compromise

1. Activate the organization's tenant-compromise response plan under the CISO or incident commander.
2. [Engage Azure Support](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation) by using the case-opening instructions and checklist, and separately engage the customer's contracted Microsoft Incident Response channel.
3. Preserve Entra sign-in, audit, application, and workload-identity evidence.
4. Pause database restore work until the identity-plane incident lead provides explicit clearance.

**Don't resume database recovery until:**
- Microsoft Incident Response is engaged if your organization activated its contracted channel.
- Global Administrator sessions are revoked and privileged identities are reset.
- Suspicious enterprise apps, service principals, or OAuth grants are removed or blocked.
- Emergency Conditional Access controls are in place.
- Entra sign-in and audit logs are exported and preserved.
- You have explicit clearance to resume resource-level recovery.


## Recovery posture by scope

Use the scope from Step 3.4 to choose the recovery pattern before restoring affected databases, managed instances, or related Azure resources.

| Scope from Step 3.4 | What changes about your restore |
|---|---|
| **Tenant-level** | Don't restore anything yet. Clear the identity plane first, using the tenant-level actions in the preceding section. Otherwise, the same access path remains available and can compromise restored resources again. |
| **Multi-subscription** | Repeat Step 3 for each affected subscription. Treat each one as its own subscription-wide case and run those restores in parallel where possible. [Open an Azure support case](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation) and, if contracted, separately engage Microsoft Incident Response. Rotate the credentials of cross-subscription service principals. You can't rotate managed identities, so remove or restrict the permissions of cross-subscription managed identities as described in [Step 2, Action 4](ransomware-recovery-contain.md#action-4-rotate-sql-credentials-and-application-secrets). Don't take tenant-level actions such as revoking Global Administrator sessions unless Step 3.4 Check 1 also matched. |
| **Subscription-wide** | Treat the entire subscription as untrusted until cleanup is complete, and make a clean subscription your production recovery destination. Rotate every service principal credential and Key Vault secret scoped to the subscription. Review every managed identity in the subscription, and remove or restrict the permissions of any you don't trust, as described in [Step 2, Action 4](ransomware-recovery-contain.md#action-4-rotate-sql-credentials-and-application-secrets). Plan a full RBAC audit in Step 5 before reconnecting applications. Successful individual restores don't make the subscription safe. **If you use customer-managed keys:** a Key Vault key backup can only be restored into a vault in the *same* subscription. This limitation affects key-backup portability, not necessarily a recovery target's access to an existing key. Verify that the required key versions are available to the target through a trusted, supported configuration, then choose a restore, copy, or migration method using the service-specific requirements in the following section. |
| **Single resource** | Restore only the affected databases or managed instances. Keep containment scoped: rotate only the logins, secrets, and credentials tied to them, and restrict only the network paths they use. Audit role assignments on those resources and their resource group. Don't rotate every credential in the subscription or rebuild subscription-wide RBAC if you've confirmed that the compromise is limited to these resources. |

## Restore options at a glance

Both services offer point-in-time restore up to 35 days (7 by default) and long-term retention up to 10 years, if LTR was configured before the attack. **If your clean point predates even your LTR coverage**, no PITR or LTR restore point reaches it: [open a Microsoft Support case](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation) and fall back to any WORM export you hold.

### Where each restore method can land

This table covers **direct restore**, not a subsequent database copy, migration, or resource move. Every restore creates a **new database**, without overwriting the original.

| Restore method | Same server or instance | Different server or instance, same subscription | Different subscription | Different region |
|---|---|---|---|---|
| **SQL Database: PITR** | Yes | No | No | No |
| **SQL Database: LTR restore** | Yes | Yes | No | Yes, within the same subscription |
| **SQL Database: geo-restore** | Yes | Yes | No | Yes, within the same subscription |
| **Managed Instance: PITR** | Yes | Yes, in the same region | Yes, within the same tenant and region; requirements below | No |
| **Managed Instance: LTR restore** | Yes | Yes | No | Yes, within the same subscription |
| **Managed Instance: geo-restore** | Yes | Yes | No | Yes, within the same subscription |

The regional and subscription boundaries are documented in the [SQL Database capability table](automated-backups-overview.md#restore-capabilities) and [Managed Instance capability table](../managed-instance/automated-backups-overview.md#restore-capabilities-and-features). Geo-restore can target the same server or instance and region; "geo" describes the backup source, not a mandatory destination. See the [SQL Database geo-restore procedure](recovery-using-backups.md#geo-restore) and [Managed Instance geo-restore procedure](../managed-instance/recovery-using-backups.md#geo-restore).

### Restore prerequisites

**Backup availability.** PITR needs an available restore point; LTR needs an existing retained backup. Geo-restore needs an available geo-replicated backup, using **GRS or GZRS** backup storage.

**Managed Instance cross-subscription PITR requirements.** All of the following requirements must be true. See the [full requirements](../managed-instance/point-in-time-restore.md#limitations).

- Source and target subscriptions are in the **same tenant**.
- Source and target managed instances are in the **same region**. Cross-region PITR isn't supported.
- The subscription type is Enterprise Agreement, Cloud Solution Provider, Microsoft Certified Partner, or pay-as-you-go.
- The restore action uses the **primary instance** and a backup from the **primary region**, not the geo-replicated secondary region.
- The caller holds **SQL Managed Instance Contributor**, or `Microsoft.Sql/managedInstances/databases/readBackups/action` on the source instance **and** `Microsoft.Sql/managedInstances/crossSubscriptionPITR/action` on the target instance.
- If you use BYOK or customer-managed keys, **the key must be present in both subscriptions**. Reconcile this requirement against the CMK containment step (Step 2, Action 5) before you attempt the restore.
- Service endpoint policies don't block the restore. A service endpoint policy on a subnet prevents point-in-time restores from instances in other subnets.
- The target managed instance must use a matching update policy or a supported higher-version update policy. Verify [update-policy compatibility](../managed-instance/automated-backups-overview.md#restore-capabilities-and-features) before provisioning the target.

**If no method reaches a clean subscription**, restore into the source subscription as an **isolated holding copy**, validate it, then transfer it to the clean subscription. For Azure SQL Database, Microsoft documents two workarounds:

- **[Cross-subscription database copy](database-copy.md#copy-to-a-different-subscription)** into a new logical server you create in the clean subscription. The copy is transactionally consistent. It requires a SQL-authentication login with the same name and password on the source and target servers, and that login must be a server admin or a member of the `dbmanager` role on both. If you enforce Microsoft Entra-only authentication, create a temporary, approved exception for the copy and remove the login when it completes.
- **Resource Move** of a newly created holding server to the clean subscription. The moved server keeps its configuration, so rebuild or re-verify its firewall rules, identities, and access before trusting it.

You can also use a BACPAC (see the [BACPAC safety rule](ransomware-recovery-assess.md#36-preserve-available-recovery-points)) or another supported migration method. Don't reconnect production traffic to the source subscription. If the tenant itself was compromised, a second subscription in that tenant isn't a clean security boundary until tenant-level remediation is complete.

### Required permissions for restore

Permissions and built-in role names change over time. Verify role assignments before an attack; mid-incident is the wrong time to discover a missing permission. For the current required roles, see each service's official documentation:

- **Azure SQL Database:** [Recover a database using automated backups](recovery-using-backups.md)
- **Azure SQL Managed Instance:** [Restore a database in Azure SQL Managed Instance](../managed-instance/point-in-time-restore.md)

## Azure SQL Database and Azure SQL Managed Instance

Both services support PITR and optional LTR, with service-specific scope and target limitations. PITR retention defaults to 7 days and can be configured up to 35 days where supported; Basic-tier Azure SQL Database is capped at 7 days. Choose a recovery path using the database's actual available PITR window and verified backup inventory, not the maximum supported retention. An LTR policy alone doesn't prove that a suitable backup exists.

Pick your path:

| Your situation | Go to |
|---|---|
| A suitable clean point exists within the database's actual available PITR window | [**Path 1**: Point-in-Time Restore](#path-1-point-in-time-restore-clean-point-within-available-pitr-window) |
| No suitable clean point is available through PITR, but a verified clean LTR backup exists | [**Path 2**: Long-Term Retention restore](#path-2-long-term-retention-restore-clean-point-unavailable-in-pitr) |
| The primary region is unavailable, or an approved resilience requirement selects another region | [**Path 3**: cross-region recovery](#path-3-cross-region-recovery-regional-unavailability-or-approved-resilience-requirement) |
| The database, logical server, or managed instance was deleted | [**Path 4**: recover the deleted resource](#path-4-recover-a-deleted-database-server-or-instance) |

### Path 1: Point-in-time restore (clean point within available PITR window)

1. Go to: your SQL Database (or Managed Instance database) → **Restore** (top action menu) → **Point in time**
2. Select an available restore point within the database's PITR window and before the approved recovery cutoff.
3. Enter a name for the new database
4. Select **Review + create**, then **Create**

Full procedure: [Azure SQL Database](recovery-using-backups.md) | [SQL Managed Instance](../managed-instance/point-in-time-restore.md)

### Path 2: Long-term retention restore (clean point unavailable in PITR)

Use this path only when a verified clean LTR backup exists before the approved recovery cutoff. You might need LTR even for a point less than 35 days old if PITR retention is shorter or the required PITR point is unavailable. Enabling LTR now doesn't recreate expired historical recovery points.

1. Go to: `portal.azure.com` → your SQL Server (or Managed Instance) → **Backups** → **Available backups** → select the long-term retention backups for the database.
2. Select a backup from before the approved recovery cutoff.
3. Select **Restore** and follow the wizard.

LTR restores are coarser in granularity than PITR: weekly, monthly, or yearly, depending on your policy. Expect some data loss beyond the backup interval.

Full procedure: [Azure SQL Database](long-term-backup-retention-configure.md) | [SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md)

### Plan recovery for SQL Managed Instance

[Point-in-time restore](/azure/azure-sql/managed-instance/point-in-time-restore#limitations) recovers an individual database, not an entire SQL Managed Instance deployment.

- Restore each affected database separately to a trusted target managed instance, using the approved recovery cutoff.
- Rebuild instance-level configuration separately, including logins, SQL Agent jobs, linked servers, and network configuration.
- If you use failover groups, review their status and assess the secondary instance before considering failover. Don't assume that the secondary is unaffected or represents a historical clean recovery point.

### Path 3: Cross-region recovery (regional unavailability or approved resilience requirement)

Use cross-region recovery only when the primary region is unavailable or an approved resilience requirement selects another region. Another Azure region isn't a security boundary: identity, subscription access, replicated changes, and compromised credentials can span regions.

Select a backup or isolated export from before the approved recovery cutoff. Geo-restore and failover groups normally use the latest replicated state and can carry attacker changes, so don't treat them as historical clean-point recovery unless incident evidence proves the replicated point is clean. Apply the same destination-trust, validation, persistence, monitoring, and reconciliation gates used for an in-region restore.

### Path 4: Recover a deleted database, server, or instance

Your recovery path depends on what was deleted and what protections existed beforehand.

- **Azure SQL Database deleted:** source logical server → **Deleted databases** → select the database → **Restore**. This process uses automated backups, so the source logical server must still exist.
- **SQL Managed Instance database deleted:** source managed instance → **Backups** → **Deleted** → **Restore**.
- **SQL Managed Instance deleted:** [contact Microsoft Support](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation) **within 5 days of deletion** to assess eligibility for support-assisted database recovery.
  - Provide an existing trusted target instance with a matching service tier and an equal or higher service objective. For databases protected by customer-managed TDE, the target must have access to the required key.
  - This path restores **user-created databases only**, to the last point-in-time backup taken just before instance deletion. It doesn't provide an arbitrary historical clean point, and it doesn't restore system databases or databases from deleted free instances.
  - The recovered state might already contain attacker changes. Validate it before use; if unsuitable, assess available clean LTR backups or other validated recovery artifacts instead.
  - Full procedure: [Restore databases from a deleted SQL managed instance](../managed-instance/recovery-using-backups.md#restoring-a-database-from-deleted-sql-managed-instance).
- **Azure SQL logical server deleted, soft delete retention was enabled:** the server is recoverable for 1 to 7 days.
  1. `portal.azure.com` → **Azure SQL hub** → **SQL logical servers** → **Restore** (command bar)
  2. Select the **Subscription**, the **Location** of the deleted server, and the **Deleted server**, then select **Restore**
  3. After the server is back, go to the server → **Data management** → **Backups** and restore each database to a point in time before the approved recovery cutoff

  Three things to know before you rely on this path:
  - **Retention defaults decide whether it works at all.** Logical servers older than two years automatically carry a 7-day soft delete retention. Servers less than two years old have soft delete **disabled** by default, in which case the deletion was permanent and this path is unavailable.
  - **Treat a retention change to `0` as a destructive action in its own right.** Retention is modifiable by anyone holding SQL Server Contributor rights, and setting it to `0` removes the recovery window. Alert on the change, and check the Activity Log for one during the incident window before you conclude a deletion was unrecoverable.
  - **Server-level settings don't come back.** After restoring, rebuild failover groups, geo-replication links, server DNS aliases, firewall rules, private endpoint connections, elastic job agents, the server's managed identity in Microsoft Entra ID, CMK/TDE encryption keys, Data Sync groups, and any connected Synapse workspace link. If an Azure Policy enforcing Entra-only authentication blocks restoration, work with the policy owner to approve the narrowest supported, time-limited exception or change. Record it and restore the required enforcement after recovery.

  Full procedure: [Restore a deleted logical server](deleted-logical-server-restore.md).
- **Azure SQL logical server deleted, soft delete not enabled:** point-in-time backups are gone with the server. Restore from LTR only, and only if LTR was configured before the attack. If neither the server nor LTR is available, [open a Microsoft Support case](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation).

### After any restore path

1. Keep production applications disconnected from the restored database or instance while validation is in progress.
2. Don't assume monitoring follows the restored resource. Verify effective auditing, Defender coverage, alert scope, and log delivery before cutover.
3. Complete all validation gates in [Step 5](ransomware-recovery-validate.md#step-5-validate-confirm-the-recovery-is-clean) and record explicit approval from the incident lead and the accountable application or business owner.
4. Only after approval, update production connection strings and reconnect through a controlled canary before expanding traffic. Use agreed success and rollback criteria; don't roll back to a known-compromised environment.
5. Don't delete the original compromised database, server, or instance until forensics are complete.

---
## Related content

- [Recover Azure SQL Database using automated backups](recovery-using-backups.md)
- [Point-in-time restore limitations for SQL Managed Instance](../managed-instance/point-in-time-restore.md#limitations)
- [Restore databases from a deleted SQL managed instance](../managed-instance/recovery-using-backups.md#restoring-a-database-from-deleted-sql-managed-instance)
- [Manage and restore long-term backups in Azure SQL Database](long-term-backup-retention-configure.md)
- [Manage and restore long-term backups in SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md)
- [Restore a deleted logical server](deleted-logical-server-restore.md)
- [Plan for tenant recoverability in Microsoft Entra](/entra/architecture/recoverability-tenant)
- [Copy an Azure SQL database to a different subscription](database-copy.md#copy-to-a-different-subscription)

## Page navigation

- [Overview](ransomware-recovery-overview.md)
- Previous: [Step 3: Assess](ransomware-recovery-assess.md)
- Next: [Step 5: Validate](ransomware-recovery-validate.md)
