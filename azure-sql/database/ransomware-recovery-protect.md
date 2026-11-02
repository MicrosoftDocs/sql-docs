---
title: Protect your Azure SQL databases
description: Learn how to protect your Azure database environment and prepare for recovery from a ransomware attack.
author: sudhanshukhera
ms.author: skhera
ms.title: Step 0 - Protect - Prepare your Azure SQL databases
ms.date: 10/31/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Step 0: Protect: Prepare your Azure SQL databases

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

> [!IMPORTANT]
> If you suspect an active attack, start with [Step 1: Detect](ransomware-recovery-detect.md#step-1-detect-recognize-the-attack). If compromise is confirmed, begin containment immediately. The configuration steps in this article don't restore deleted, encrypted, or altered data. Return to this checklist during post-incident hardening.

The most important recovery fact is simple: key database ransomware recovery options must exist and be tested before the attack. Enabling backup retention, long-term retention, audit logging, immutable log storage, and privileged access controls after compromise doesn't recreate expired backups, missing historical logs, or prior protection. The following checklist closes the gaps that determine whether a recovery is fast, partial, or impossible.

## The checklist at a glance

Fourteen controls in three groups, plus a Hyperscale caveat. Each control pulls one or more of three levers: **Prevent** lowers the chance the attack lands, **Reveal** raises the chance you find out and shows what was done, **Recover** raises the chance you get your data back. For each control, record whether it's implemented, who owns it, when it's last tested, and what evidence proves it works. Use this table to jump to whichever gaps you have.

| Group | Control | Lever | What it buys you |
|---|---|---|---|
| **1. Reveal and preserve evidence** | [Custom Azure Monitor alerts](#custom-azure-monitor-alerts-catch-destruction-phase-signals) | Reveal | Alerts on destructive activity and changes that weaken protection |
| | [Microsoft Defender for SQL](#defender-for-cloud-flag-reconnaissance-and-suspicious-access) | Reveal | Alerts on access and reconnaissance, before destruction starts |
| | [Audit and diagnostic logging](#audit-and-diagnostic-logging-preserve-forensic-evidence) | Reveal + Recover | An important forensic evidence source for estimating when the attack started and which restore point might be clean |
| **2. Limit who can reach the database** | [Authentication](#authentication-verify-identity-before-granting-access) | Prevent | A stolen password alone isn't enough to sign in |
| | [Authorization](#authorization-limit-what-an-authenticated-identity-can-do) | Prevent + Recover | Limits how much a compromised identity can change, and limits who can delete your backups |
| | [Network isolation](#network-isolation-shrink-the-attack-surface) | Prevent | A stolen credential alone isn't enough to connect |
| | [Production and nonproduction separation](#separate-production-and-nonproduction-environments) | Prevent | Limits paths from compromised nonproduction workloads into production |
| **3. Preserve a recoverable copy** | [Know your P0 data and rehearse recovery](#know-your-p0-data-and-rehearse-the-recovery) | Recover | Proof that the restore works, and a pre-agreed answer to "which database first?" |
| | [Short-term backup (PITR)](#short-term-automated-backup-pitr) | Recover | Up to 35 days of restore points instead of the default 7 |
| | [Long-term retention (LTR)](#long-term-backup-retention-recover-beyond-the-short-term-window) | Recover | A clean point up to 10 years back, surviving server and instance deletion |
| | [Immutable backup](#immutable-backup) | Recover | Backups an attacker can't shorten or delete |
| | [Preserve the TDE protector](#preserve-the-tde-protector-customer-managed-keys-only) | Recover | Keeps current and historical TDE protector versions recoverable so CMK-encrypted databases and backups remain restorable |
| | [Logical server soft delete](#logical-server-soft-delete-retention-azure-sql-database) | Recover | A recovery window if the whole logical server is deleted |
| | [Resource locks](#resource-locks-protect-production-resources) | Prevent + Recover | Reduces the risk of deleting production resources and recovery dependencies |
| *Caveat, not a control* | [Hyperscale exceptions](#hyperscale-exceptions-azure-sql-database) | N/A | Where the guidance above changes if you run Hyperscale |

---

## Group 1: Reveal the attack and preserve the evidence

### Custom Azure Monitor alerts: Catch destruction-phase signals

**Why it matters:** This control provides direct alerting for destruction-phase signals while an attack is in progress. Without custom alerting, your first signal might come from a user, an application error, or the attacker's own ransom note.

**What to configure:** Use the evidence source that exposes each signal:
- **Documented platform metrics:** Alert on signals such as **Data space used** for Azure SQL Database and **Storage space used**, **Log rate**, and **IO bytes written** for Azure SQL Managed Instance, where available.
- **SQL auditing, application telemetry, or scheduled queries:** Use these for row-count changes, DML or DDL statements, mass `UPDATE` activity, and other statement-level signals that aren't generic Azure Monitor platform metrics.
- **Activity Log or log-query alerts:** Monitor removal or modification of resource locks, server-delete retention, PITR/LTR retention, backup immutability, Azure Policy assignments or exemptions, privileged role assignments, and networking settings. Include changes to audit logging, alert rules, and notification routing. Use the relevant log source for each setting, and prioritize unexpected changes or reductions in protection. Assign an alert owner and test notification delivery.

To create an alert, see [Create or edit a metric alert rule](/azure/azure-monitor/alerts/alerts-create-metric-alert-rule):
1. Go to: `portal.azure.com` → **Monitor** → **Alerts** → **Create** → **Alert rule**
2. Select your scope (subscription or specific resource)
3. Select a documented metric condition for the scoped resource, or create a log-query or Activity Log alert for signals that aren't exposed as platform metrics.

### Defender for Cloud: Flag reconnaissance and suspicious access

**Why it matters:** Microsoft Defender for SQL can alert on access and reconnaissance signals on your databases and managed instances such as SQL injection patterns, brute-force attempts, and sign-ins from an unusual principal, domain, or location.

**What to configure:** Enable [Microsoft Defender for SQL](/azure/defender-for-cloud/defender-for-sql-introduction) on your databases and managed instances, and pair it with custom Azure Monitor alerts because authenticated destructive operations such as a mass `DELETE`, `DROP TABLE`, or `TRUNCATE` might look like legitimate maintenance.

**Limitation:** The published Defender alert catalog doesn't list an alert for mass `DELETE`, `DROP TABLE`, `TRUNCATE`, or bulk in-place `UPDATE` against Azure SQL Database or Azure SQL Managed Instance. The catalog lists one Execution-tagged alert that applies to these services, covering a SQL Server spawning a Windows command shell against an abnormal external source (`SQL.DB_ShellExternalSourceAnomaly` / `SQL.MI_ShellExternalSourceAnomaly`). Its published data-exfiltration alert (`SQL.VM_DataExfiltration`) is in preview and applies only to Defender for SQL on machines. Microsoft notes that some recently added alerts might be undocumented, so check the current [alert reference](/azure/defender-for-cloud/alerts-sql-database-and-azure-synapse-analytics) and your environment. Use custom Azure Monitor and KQL alerts for direct destruction-phase signals not covered by the published catalog.

### Audit and diagnostic logging: Preserve forensic evidence

**Why it matters:** Audit logs are an important forensic evidence source for estimating when the attack started, which data was affected, and which restore point is likely clean. [Azure SQL Auditing](auditing-overview.md) must be enabled; it is not on by default.

**What to configure:**
1. Set up auditing for [Azure SQL Database](auditing-setup.md) and [Azure SQL Managed Instance](../managed-instance/auditing-configure.md) using the procedure for each service.
2. Configure both tamper-resistant evidence preservation and searchable monitoring. Protect these destinations from the compromised identities:
   - **For evidence preservation:** A storage account with a locked [Azure Blob Storage immutability policy](/azure/storage/blobs/immutable-storage-overview). SQL auditing writes logs to append blobs, so set **Allow protected append writes** to **Append blobs** or **Block and append blobs** before you lock the policy. The **None** option isn't supported, and you can't change this setting after you lock the policy. For Azure SQL Database, the policy's retention interval must also be shorter than the SQL auditing retention setting, and a SQL auditing retention of `0` isn't supported. For details, see [Auditing for Azure SQL Database](auditing-overview.md) and [Configure auditing for SQL Managed Instance](../managed-instance/auditing-configure.md).
   - **For detection and investigation:** A Log Analytics workspace connected to [Microsoft Sentinel](/azure/sentinel/overview), or your existing SIEM. SIEM connectivity alone doesn't make logs immutable. A different subscription in the same compromised tenant isn't automatically a trusted evidence destination.

**Limitation:** A default log destination isn't protected if it's in the same subscription and tenant as the database, because an attacker who can reach the database might also reach the logs. The [Azure Activity Log](/azure/azure-monitor/essentials/activity-log) is retained separately from your database resources and can help reconstruct control-plane activity, but it doesn't capture table-level data-plane actions. For attacks with dwell time longer than 90 days, export the Activity Log to Log Analytics or Storage before an incident begins.

## Group 2: Limit who can reach the database

### Authentication: Verify identity before granting access

**Why it matters:** Authentication proves who is signing in, before authorization decides what they can do. Weak or password-based sign-in is a common entry path for ransomware operators. Centralizing identity and requiring multiple verification factors reduces the chance that one stolen credential is enough to get in.

**What to configure:**
- Enable [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md) for your SQL servers and managed instances where supported, and disable SQL authentication where possible.
- Require multifactor authentication for all admin accounts, enforced through [Conditional Access](/entra/identity/conditional-access/overview).
- Create and validate [emergency access (break-glass) accounts](/entra/identity/role-based-access-control/security-emergency-access) now: cloud-only, excluded from Conditional Access, credentials stored offline, and alerted on any use. During containment you need an identity that isn't part of the sweep you're performing.


### Authorization: Limit what an authenticated identity can do

**Why it matters:** The privilege level of a signed-in identity affects how much data and configuration it can change. Azure SQL splits this authorization decision across two separate systems: **Azure RBAC** controls management-plane operations (configuring, restoring, or deleting the server or database), while the **SQL native permission system** controls data-plane access (reading, writing, or deleting rows and objects inside the database). Least privilege and just-in-time elevation on both reduce the chance that one identity can delete data, disable logs, and remove recovery options.

**What to configure:**

*Azure RBAC (management plane):*
- Remove stale accounts, old service principals, and overly broad Owner/Contributor assignments.
- Set up [Privileged Identity Management (PIM)](/entra/id-governance/privileged-identity-management/pim-configure) for just-in-time access to subscription Owner.

**Azure Policy configuration guardrails:** Use [Azure Policy](policy-reference.md) to audit or enforce approved networking, authentication, auditing, and recovery settings where supported. Validate each policy's resource coverage and available effect before enforcement. Govern policy assignments and exemptions separately from day-to-day workload administration, alert on changes, and test deployment and recovery workflows against the policies.

*SQL native permissions (data plane):*
- Grant permissions to [database roles](/sql/relational-databases/security/authentication-access/database-level-roles) (or SQL Managed Instance [server-level roles](security-server-roles.md)), never directly to individual users or applications.
- Limit `db_owner` and other broad admin role membership to a small named set; use narrower roles such as `db_datareader` or `db_datawriter`, or custom roles, for applications.
- Regularly audit role memberships and any direct object-level `GRANT`s (query `sys.database_permissions` and `sys.server_permissions`); direct grants are the ones most likely to survive a review unnoticed.

### Network isolation: shrink the attack surface

**Why it matters:** An attacker with stolen credentials can reach the database over a permitted public endpoint. If you disable public network access and only trusted networks can reach the server or instance, a leaked SQL login or Entra credential alone isn't enough to connect.

**What to configure:**
- Disable public network access on the logical server (Azure SQL Database) or managed instance, and require Private Link or private endpoints for all application connectivity.
- Where public access must stay on for third-party or hybrid scenarios, replace broad firewall rules (for example `0.0.0.0`-`255.255.255.255`) with explicit IP or VNet-rule allow-lists scoped to known application subnets.
- **Turn off "Allow Azure services and resources to access this server."** It adds a server-level rule with a start and end IP of `0.0.0.0` that permits connections from any resource inside the Azure boundary, [including services running in other customers' subscriptions](firewall-configure.md#server-level-versus-database-level-ip-firewall-rules). It's off by default on new servers, so if it's on, someone turned it on.
- **Audit database-level firewall rules, not just server-level ones.** Database-level rules are [evaluated before the server-level rules](firewall-configure.md#server-level-versus-database-level-ip-firewall-rules) and can be set by someone holding `CONTROL` on a single database, without server admin rights. They don't appear in the server's **Networking** blade, so check each database's own rules as well.
- For SQL Managed Instance, keep it in a dedicated subnet with restrictive NSG rules and no direct internet-facing NSG or UDR exceptions.
- Use service-specific Azure Policy definitions to audit or deny noncompliant public-network-access settings. Ensure the policy matches the chosen connectivity design, and govern policy changes and exemptions separately from workload administration.
- Log and alert on any firewall rule, virtual network rule, or public-network-access change (covered under [Custom Azure Monitor Alerts](#custom-azure-monitor-alerts-catch-destruction-phase-signals)).

**Network Security Perimeter for Azure SQL Database (preview):** [Network Security Perimeter](network-security-perimeter.md) restricts public inbound and outbound traffic on supported connections. It doesn't apply to **SQL Managed Instance**. Review feature compatibility and your organization's requirements for preview features before associating resources. Validate required traffic in transition mode before enabling enforcement. Private endpoint traffic isn't governed by perimeter access rules.

**Limitation:** Network isolation blocks remote access paths but doesn't stop an attacker who already has valid credentials and connects from an allowed network or a compromised application server. Pair it with the Authentication and Authorization controls in the previous section.

### Separate production and nonproduction environments

**Why it matters:** A compromised test application, identity, or deployment pipeline shouldn't provide a route into production.

**What to configure:** Separate production and nonproduction role assignments, application identities, secrets, deployment permissions, and network access. Use separate subscriptions where appropriate. Restrict cross-environment access to approved needs. Use synthetic or appropriately masked test data, or apply production-equivalent protection to approved copies of production data.

**Limitation:** Separate subscriptions or resource groups don't protect production if the same compromised identity has privileged access to both. Tenant-level compromise can still cross these boundaries.

---

## Group 3: Preserve a recoverable copy

### Know your P0 data and rehearse the recovery

**Why it matters:** The technical controls in this group don't by themselves tell you *which* database matters most at 3 a.m., who is allowed to make the restore call, or whether your restore actually works. A backup you never restored is a hypothesis, not a recovery plan.

**What to put in place** (these are process, not portal settings):

- **Map your P0 data before an incident.** Rank databases by business criticality and keep that list somewhere your responders can reach *without* the compromised environment. During eviction you must secure the top assets first and extend outward; a pre-agreed list lets responders make that decision without reconstructing priorities during the incident.
- **Write an incident response plan and keep it current.** Name the person who decides restore-versus-wait, the escalation paths to Azure Support and your contracted Microsoft Incident Response channel, and who talks to legal, regulators, and insurers.
- **Test the restore, not just the backup configuration.** Record the configured server-deletion retention where supported, earliest available PITR point, available LTR backups, and backup immutability state. Perform scheduled PITR and, where used, LTR restores into an approved test environment; time recovery and verify the data. Confirm the tooling *and* the runbook.
- **Run drills.** Rehearse the sequence end to end at least annually, including the decision points, the way you would rehearse a fire evacuation. The first time you do this should not be during an attack.
- **Record your measured recovery time.** Measure the complete time to business recovery, including emergency-access recovery, target provisioning, database restore or migration, security validation, data reconciliation, application testing, and production cutover. Report the measured total, its assumptions, and remaining uncertainty against the agreed maximum acceptable outage (RTO).

**Limitation:** The platform generally doesn't enforce these process controls. They might not appear in standard platform compliance or Advisor checks, which is why you might overlook them.

### Short-term automated backup: PITR

**Why it matters:** Azure SQL Database and SQL Managed Instance provide automated point-in-time backups, but the default retention is seven days. If attacker dwell time exceeds the retention window, you might not be able to cleanly recover from short-term backups.

**What to configure:** Increase the retention window up to **35 days** before an attack to maximize your available PITR window. See [Change automated backup settings](automated-backups-change-settings.md).
- **Azure SQL Database:** Your logical server → **Backups** → **Retention policies** → select the database → **Configure policies** → set the **Point-in-time restore** retention period.
- **SQL Managed Instance:** Your managed instance → **Backups** → **Retention policies** → select the database → **Configure policies** → set the PITR retention period.

**Limitation:** The 1-to-35-day range applies to most databases. **Basic-tier** databases in Azure SQL Database are capped at **1 to 7 days**, so a Basic database has no way to extend PITR coverage past a week. For workloads requiring a longer recovery window, move off Basic or rely on LTR.

### Long-term backup retention: Recover beyond the short-term window

**Why it matters:** If an attacker remains undetected for longer than 35 days, automated short-term backups might already roll over and no longer contain a clean restore point.

**What to configure:** Turn on [long-term retention (LTR)](long-term-retention-overview.md):
1. Your SQL Server (or Managed Instance) → **Backups** → **Retention policies** → select the database → **Configure policies**
2. Set weekly, monthly, or yearly retention as needed
3. On Azure SQL Database, also enable and **lock** LTR backup immutability (see [Immutable Backup](#immutable-backup) below) so an attacker can't shorten retention or delete the clean point. For SQL Managed Instance, LTR backup immutability is **in preview**; confirm availability and protection settings for your instance before relying on it.

What LTR gives you:
- **GA on both services**, retaining weekly, monthly, or yearly backups for up to **10 years**
- Backup storage separated from the live database, supporting recovery after database, logical-server, or managed-instance deletion
- Cross-region restore when the selected service and backup support it

Step-by-step: [Manage LTR for Azure SQL Database](long-term-backup-retention-configure.md) | [Manage LTR for SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md)

| Service | LTR Status | Max Retention | LTR backup immutability |
|---|---|---|---|
| Azure SQL Database | ✅ GA | Up to 10 years | ✅ Supported |
| Azure SQL Managed Instance | ✅ GA | Up to 10 years | **Preview** |

**Limitation:** Sufficiently privileged subscription access can still change or delete mutable LTR policies or backups. Locked LTR immutability reduces this risk for Azure SQL Database. For SQL Managed Instance, LTR immutability is **in preview**; confirm availability and protection settings for your instance before relying on it. LTR doesn't create a security boundary from a compromised subscription.

### Immutable backup

**Why it matters:** Cyberattackers target backups as well as production data to prevent recovery. Immutability helps protect covered backups from modification or deletion during their protection period. Coverage varies by service and backup type.

There are **two types of backup immutability**: automatic PITR protection and LTR protection. Coverage and availability differ by service.

| Capability | What it protects | Services | How you get it |
|---|---|---|---|
| [**Automatic backup immutability**](../automatic-backup-immutability.md) | Up to the **most recent seven days** of PITR backups, regardless of your configured PITR retention period | Supported Azure SQL Database and SQL Managed Instance databases | **Included and on by default for supported databases.** No onboarding or configuration is required. |
| [**SQL Database LTR backup immutability**](backup-immutability.md) (time-based or legal hold) | LTR backups written to WORM storage for a user-defined period | **Azure SQL Database** | Enable and lock time-based protection, or apply a legal hold to an eligible backup |
| **SQL Managed Instance LTR backup immutability (preview)** | LTR backups covered by the preview's protection settings | **SQL Managed Instance** | Confirm preview availability and supported configuration with your Microsoft contact |

**What to configure:** Nothing for automatic backup immutability on supported databases; it is already active. For Azure SQL Database workloads requiring immutable long-term retention, enable and **lock** a [time-based immutability policy](backup-immutability-time-based.md). Apply a [legal hold](backup-immutability-legal-hold.md) only through your organization's counsel-led or records-management process. The following portal steps apply to **Azure SQL Database**:
1. Your logical server → **Data management** → **Backups** → **Retention Policies** → choose the database → **Configure Policies**
2. Under **LTR Immutability**, check **Enable time-based immutability policy**, then check **Lock the time-based immutable policy**

**SQL Managed Instance (preview):** Confirm availability and supported configuration with your Microsoft contact. Verify protection of the intended LTR backups and test restoration before counting this feature as a recovery control.

**Limitations:**
- Automatic backup immutability is **not available for the Hyperscale service tier** of Azure SQL Database.
- For Azure SQL Managed Instance, automatic backup immutability is available only with the **Always-up-to-date** or **SQL Server 2025** update policy, not the **SQL Server 2022** update policy.
- Automatic backup immutability covers seven days only. If your PITR retention is 35 days, days 8 through 35 are still mutable.
- SQL Managed Instance LTR backup immutability is **in preview**. Do not assume that the SQL Database configuration steps or legal-hold capabilities apply to the MI preview.
- If you need an independent portable MI copy, verify the `COPY_ONLY` full-backup prerequisites: MI databases encrypted with **service-managed TDE cannot use the `COPY_ONLY` workflow**. For eligible databases using customer-managed TDE, retain the required key versions and test restoration to the intended target before protecting the completed backup with Blob immutability. If ineligible, use available PITR/LTR recovery and establish a separately tested export or migration alternative before relying on a portable immutable copy. See [copy-only backup limitations](/sql/relational-databases/backup-restore/copy-only-backups-sql-server).
- For **Azure SQL Database**, time-based immutability doesn't apply retroactively to existing LTR backups. To protect eligible existing LTR backups, use the SQL Database legal-hold workflow through your organization's approved process.

### Preserve the TDE protector (Customer-managed keys only)

**Why it matters:** Customer-managed keys (CMK) for TDE let you control the TDE protector yourself in Azure Key Vault, outside the database, including the ability to revoke Azure SQL's access to shut off decryption. That same control makes the protector a single point of failure: delete the key or the vault and the database moves to **Inaccessible** state, and no restore can complete decryption until key access is restored. Every historical protector version a backup depended on must also stay available, or that backup becomes unrestorable.

**What to configure:** mostly verification. Azure SQL doesn't let you configure a TDE protector unless the vault already has soft-delete and purge protection; [TDE protector setup fails with an error](transparent-data-encryption-byok-overview.md#requirements-to-configure-customer-managed-tde) if either is missing. If CMK is working today, both are already on. What is left is confirming the settings around them:

- **Verify [soft-delete and purge protection](/azure/key-vault/general/key-vault-recovery) on every CMK vault.** Once enabled, neither can be turned off, including by an attacker.
- **Set the soft-delete retention interval deliberately.** It's configurable from 7 to 90 days (default 90) and **can't be changed once saved** for that vault. Pick it at vault creation with your incident-response window in mind.
- **Restrict who can purge.** Audit every built-in or custom role that grants vault or key purge actions, and remove assignments that aren't required.
- **Audit who can back up and restore keys** on every CMK vault. Review every principal with key backup or restore actions, and inspect the vault's audit logs for activity you didn't initiate. Treat these permissions with the same scrutiny as purge.
- **Keep an offline copy of the TDE protector key material** in a hardware security module or local key store. Microsoft lists this as recommended in the [TDE configuration prerequisites](transparent-data-encryption-byok-configure.md#prerequisites-for-powershell).
- **Keep every previous TDE protector version.** Rotating the protector doesn't re-encrypt old backups, so restoring a backup requires the key that protected it at the time. Microsoft recommends keeping all previously used keys in the vault [for exactly this reason](transparent-data-encryption-byok-key-rotation.md#important-considerations-when-rotating-the-tde-protector), including when you switch from customer-managed back to service-managed keys.
- **Protect the server's managed identity as carefully as the key.** Deleting the SQL server's or managed instance's managed identity in Microsoft Entra ID takes the database offline just as surely as deleting the key. It's one of [five common ways key access is lost](transparent-data-encryption-byok-overview.md#inaccessible-tde-protector), alongside revoking the vault's `get`, `wrapKey`, or `unwrapKey` permissions, deleting the key, deleting the vault, and changing the vault firewall rules.
- **Enforce soft-delete across all vaults** org-wide with [Azure Policy](/azure/key-vault/policy-reference).

**Limitation:** Purge protection blocks permanent deletion only during the configured soft-delete retention interval. An attacker with sufficient privilege can still **soft-delete** the key or vault and take the database offline. If you don't recover it before the retention interval expires, permanent deletion can follow. Recover the vault or key from soft-delete before attempting any restore.

### Logical server soft delete retention (Azure SQL Database)

**Why it matters:** A ransomware operator with sufficient privilege can delete the entire Azure SQL **logical server**, which deletes all its databases and removes the point-in-time restore path along with the server. [Soft delete retention](deleted-logical-server-restore.md) (preview) keeps a deleted server in a recoverable, soft-deleted state so you can restore the server and then recover its databases from automated backups within the retention window.

**What to configure:** Enable soft delete retention on your logical servers and set the retention to the maximum **7 days**.
- Your logical server → **Data Management** → **Delete protection (preview)** → set **Keep deleted servers (in days)** to `7` → **Apply**
- Configuring `New-AzSqlServer` / `Set-AzSqlServer` with `-SoftDeleteRetentionDays 7`, or `az sql server create` / `update` with `--soft-delete-retention-days 7`, also works.

**Limitations:**
- Preview feature; **Azure SQL Database logical servers only** (not SQL Managed Instance).
- Maximum retention is **7 days**, so it protects against server-deletion only within a short window; it's not a substitute for LTR.
- Servers less than two years old have soft delete **disabled by default**. Verify the setting explicitly rather than assuming it's on.
- Pair it with a [`CanNotDelete` Resource Lock](/azure/azure-resource-manager/management/lock-resources) and alerting on server-delete and retention-change events.

### Resource locks: Protect production resources

**Why it matters:** Delete locks reduce the risk of losing production resources and recovery dependencies through accidental or malicious management operations.

**What to configure:** Apply `CanNotDelete` [resource locks](/azure/azure-resource-manager/management/lock-resources) at appropriate production subscription, resource-group, or resource scopes. Account for inherited locks rather than duplicating them at every level. Restrict lock-removal permissions, alert on lock changes, and test an approved unlock/relock procedure for maintenance and incident response.

**Limitation:** Locks don't block SQL data changes or make backups immutable. Privileged identities can remove them. Test operational impact: locks can block legitimate management or remediation, including removal of scoped role assignments.

### Hyperscale exceptions (Azure SQL Database)

**Why it matters:** If you run workloads with defined retention or recovery requirements on the Hyperscale service tier, four of the protections and restore paths described earlier behave differently. Confirm your tier before you rely on them.

| Area | Hyperscale behavior |
|---|---|
| Automatic backup immutability | **Not available.** The default 7-day PITR immutability protection doesn't apply. |
| Cross-tier restore | **Restoring between Hyperscale and other service tiers isn't supported** in either direction. Your recovery target must also be Hyperscale. |
| LTR restore | Supported, but the **read scale** property is disabled on the restored database and must be re-enabled. |
| Backup scheduling | Hyperscale uses a [different backup mechanism](hyperscale-automated-backups-overview.md) (continuous storage snapshots, not full, differential, or log backups), so you can't configure differential backup frequency. The `DiffBackupIntervalInHours` setting has no effect on Hyperscale databases. |

**What to configure:** Treat Hyperscale as needing compensating controls. Rely on LTR with a locked immutability policy. If you use periodic [BACPAC exports](database-export.md), create and validate the export by using a supported workflow, and then place the completed artifact in Blob Storage protected by an appropriate immutability policy. To use this approach for Hyperscale, review current BACPAC size and tooling limitations.

---

## Related content

- [Automated backups in Azure SQL Database](automated-backups-overview.md)
- [Automated backups in Azure SQL Managed Instance](../managed-instance/automated-backups-overview.md)
- [Long-term backup retention overview](long-term-retention-overview.md)
- [Automatic backup immutability](../automatic-backup-immutability.md)
- [Long-term backup immutability in Azure SQL Database](backup-immutability.md)
- [Auditing for Azure SQL Database](auditing-overview.md)
- [Create or edit an Azure Monitor metric alert rule](/azure/azure-monitor/alerts/alerts-create-metric-alert-rule)
- [Microsoft Entra-only authentication for Azure SQL](authentication-azure-ad-only-authentication.md)
- [IP firewall rules for Azure SQL Database](firewall-configure.md)
- [Transparent data encryption with customer-managed keys](transparent-data-encryption-byok-overview.md)
- [Lock Azure resources to protect your infrastructure](/azure/azure-resource-manager/management/lock-resources)

## Page navigation

- [Overview](ransomware-recovery-overview.md)
- Next: [Step 1: Detect](ransomware-recovery-detect.md)
