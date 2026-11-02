---
title: Assess the damage
description: Learn how to assess the impact of a ransomware attack and identify affected resources and data in your Azure database environment.
author: sudhanshukhera
ms.author: skhera
ms.title: Step 3 - Assess damage - Determine what was affected
ms.date: 10/31/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Step 3: Assess damage: Determine what was affected

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Before you restore, you need to know: **when did the cyberattack start, and what data was destroyed?** Restoring to a point in time that the cyberattacker already controlled is as bad as not restoring at all.

## 3.1 Start with the Azure Activity Log

The Azure Activity Log is a Microsoft-managed record of Azure control-plane operations, retained for a limited period by default. It's an important timeline source, but it doesn't record SQL data-plane activity or necessarily reveal the attacker's original entry point.

1. Go to: `portal.azure.com` → **Monitor** → **Activity Log**.
2. Set the required time range and record the selected scope and filters.
3. Export the available results as a working evidence copy. Record the scope, filters, export time, and exporting identity; calculate a hash; and preserve the original export in restricted, preferably immutable storage. A CSV file is mutable and isn't tamper-resistant by itself.
4. Work through these searches in the Activity Log search bar:

| Search term | What to look for |
|---|---|
| `delete` | Every resource deleted and exact timestamp |
| `write` | Configuration **changes**, not just deletions: modified firewall rules, public network access turned on, auditing or diagnostic settings altered, PITR retention shortened to the minimum supported value, LTR policies or backups removed, backup protections changed, logical-server soft-delete retention set to `0`, TDE/CMK key changes, or other server or instance settings changed. Review any `write` operation from an unfamiliar Caller in the incident window. |
| `roleAssignments` | New Owner or Contributor grants you didn't make |
| `elevateAccess` | A Microsoft Entra Global Administrator invoked Azure RBAC elevation, granting that caller User Access Administrator at root scope. This indicates root-scope Azure authorization activity, not proof that the attacker became a Global Administrator. Validate the caller, timing, resulting assignments, and affected scope. |
| `diagnosticSettings` | If deleted: log export was disabled during the incident window |
| `backup` | Were backup or LTR policies modified or removed? |
| `Microsoft.KeyVault` | If you use CMK / BYOK: was the vault deleted, or were its access policies, role assignments, or firewall changed? The database might already be in *Inaccessible* state. Key-level operations such as key delete, purge, backup, and restore are Key Vault data-plane events and don't appear in the Activity Log. Check the vault's diagnostic logs (`AuditEvent` category) for them. Recover the vault or key from soft-delete **before** attempting any restore. |

For each suspicious entry, record:
- **Caller:** who performed the action (user or service principal)
- **Timestamp:** when it happened
- **Resource:** what was affected

Record the earliest observed suspicious event and the latest known-good evidence. The earliest observed event isn't necessarily the initial compromise. Define a compromise window, document uncertainty, and establish an approved recovery cutoff before the earliest credible indicator, with an extra safety margin for evidence gaps.

---

## 3.2 Check Microsoft Entra sign-in logs

Microsoft Entra sign-in logs help you identify the first suspicious successful sign-in and a candidate entry identity.

1. Go to: `portal.azure.com` → **Microsoft Entra ID** → **Monitoring** → [**Sign-in logs**](/entra/identity/monitoring-health/concept-sign-ins)
2. Export the available sign-in window immediately.
3. Look for successful sign-ins from unfamiliar locations, risky sign-ins, unfamiliar applications or service principals, and successful sign-ins that occur just before suspicious Activity Log events.
4. Record the earliest suspicious successful sign-in as a candidate entry point.

**How far back you can see** depends on your Microsoft Entra ID license tier and whether you configured log export before the attack. Check first: **Microsoft Entra ID** → **Monitoring** → **Diagnostic settings**. If a setting points to Log Analytics or a Storage Account, you have logs beyond the default buffer. Then check your license under **Entra ID** → **Overview**.

| Your situation | Log availability | Do this now |
|---|---|---|
| P1/P2 license + export configured | As long as your Log Analytics retention policy allows, queryable via Log Analytics | Run the queries in the following section |
| P1/P2 license, no export | 30-day Microsoft-managed buffer only | Export CSV before the window rolls |
| Free Entra tier | 7-day buffer only | Export CSV now (limited forensic value) |

See [Microsoft Entra data retention](/entra/identity/monitoring-health/reference-reports-data-retention) for the full matrix.

**If you have log export**, go to `portal.azure.com` → **Log Analytics workspaces** → [workspace] → **Logs** and run:

```kusto
SigninLogs
| where TimeGenerated > ago(90d)
| where RiskState != 'none'
| project TimeGenerated, UserPrincipalName,
    IPAddress, Location, AppDisplayName, RiskDetail
| sort by TimeGenerated asc
```

Then check for anomalous geographies:

```kusto
SigninLogs
| where TimeGenerated > ago(90d)
| summarize count() by Location, bin(TimeGenerated, 1d)
| sort by count_ desc
```

- Review all identity types that could have accessed the workload. Don't stop with interactive user sign-ins.

```kusto
union isfuzzy=true SigninLogs,
    AADNonInteractiveUserSignInLogs,
    AADServicePrincipalSignInLogs, AADManagedIdentitySignInLogs
| where TimeGenerated > ago(90d)
| project TimeGenerated, Type, ServicePrincipalId, ServicePrincipalName,
   UserPrincipalName, IPAddress, ResourceDisplayName, ResultType
| sort by TimeGenerated asc
```

- Confirm the available table names and schema in the workspace. Correlate suspicious user, service-principal, and managed-identity activity with Azure Activity Log and SQL audit evidence.

> [!NOTE]
> Use the exact table name `SigninLogs` in Log Analytics queries. KQL table names are case-sensitive.

Review sign-ins from unexpected locations and successful sign-ins following repeated MFA failures for the same account. Review **Authentication Details** to investigate possible MFA-fatigue activity. Treat the earliest suspicious sign-in in the available logs as a candidate entry point, not proof of initial compromise.

Download available Microsoft Entra sign-in and audit logs promptly, even if diagnostic export was not previously configured. Record the export scope, filters, time, and exporting identity. Calculate file hashes and store the original exports with restricted access according to your organization's evidence-preservation procedures.

Available history depends on licensing, retention, and any previously configured log destinations. Enabling export now does not recover expired records. If the compromise began before the available log window and no earlier copy exists, the initial sign-in might no longer be available.

---

## 3.3 Check SQL data-plane evidence

Data-plane audit logs show you *what the attacker did inside the database*, not just what they changed in Azure. How much confidence you can place in your restore point depends on whether these logs exist.

**Check audit status:** For Azure SQL Database, open your logical server or database, select **Security** > **Auditing**, and record the effective audit policy and destinations. For SQL Managed Instance, use SSMS or T-SQL to inspect server audits, their running status, and enabled server and database audit specifications. Also check diagnostic settings when using Azure Monitor logs or Event Hubs. See [Configure Managed Instance auditing](../managed-instance/auditing-configure.md). Record the enabled action groups and actual destinations for each affected resource.

**If auditing was on and the destination is Log Analytics**, run:

```kusto
AzureDiagnostics
| where Category == 'SQLSecurityAuditEvents'
| where TimeGenerated > ago(90d)
| where statement_s has_any ('DELETE','DROP','TRUNCATE','UPDATE')
| project TimeGenerated, client_ip_s,
   server_principal_name_s, action_name_s,
   object_name_s, statement_s
| sort by TimeGenerated asc
```

> [!IMPORTANT]
> Do not filter only on `action_name_s`. Default batch auditing can include SQL text containing `SELECT`, `DELETE`,` or other statements in `statement_s`, while `action_name_s` can remain `BATCH COMPLETED`. Query `statement_s` and preserve the complete audit record.

Long statements can be truncated in `statement_s`. Treat partial SQL text as incomplete evidence.

For Azure SQL Database and SQL Managed Instance, audit events sent to Log Analytics are stored in `AzureDiagnostics`, with `SQLSecurityAuditEvents` as the diagnostic category, not in a dedicated table of that name.

**If the destination is a Storage Account**, for Azure SQL Database go to SQL Server, select **Auditing** > **View audit logs** and filter to the suspected attack window. For SQL Managed Instance, read the audit files by using the SSMS audit-log viewer or `sys.fn_get_audit_file` and filter to the suspected attack window.

Either way, look for mass `DELETE` or `TRUNCATE TABLE` operations, `DROP TABLE` or `DROP SCHEMA`, and unfamiliar source IPs or authentication names.

**If auditing wasn't enabled**, you might lack a reliable SQL audit trail of data-plane activity. Fall back to:
- **Metrics**: Data space used (Azure SQL Database); Storage space used, Log rate, and IO bytes written (SQL Managed Instance)
- **Activity Log**: control-plane only. You can see when a server, database, or instance was deleted and when firewall or configuration settings changed. You **cannot** see which tables were dropped or whether data was copied out via `SELECT`. Treat this as a significant forensic gap and widen your restore-point safety margin accordingly.

## 3.4 Determine the scope of compromise

Use the following checks to decide whether this incident affects a tenant, multi-subscription, subscription, or single-resource. Stop at the first one that matches; that is your scope.

Use this section only to classify the incident. Do not start recovery actions here. Once you know the scope, move to Step 4 for the response path.

### Check 1: Tenant-level compromise

**Signals:**
- Entra sign-in logs show unauthorized use of a Global Administrator or Privileged Role Administrator account.
- An unauthorized identity was added to a Microsoft Entra directory role such as Global Administrator or Privileged Role Administrator.
- Conditional Access, MFA, federation, or other tenant identity controls were modified without authorization.

Treat `elevateAccess` and unauthorized User Access Administrator assignments as Azure control-plane evidence. They justify wider Azure RBAC investigation but don't, by themselves, prove tenant identity-plane compromise.

If tenant identity-plane compromise is confirmed or can't be excluded, stop database recovery and use the tenant-level stop condition in Step 4.

### Check 2: Multi-subscription compromise

**Signals (any one is sufficient):**
- Your Activity Log search (Step 3.1) returned suspicious entries (`delete`, `roleAssignments`, `diagnosticSettings` deletion) in **two or more subscriptions** during the incident window.
- The Caller identity that performed suspicious actions has Owner or Contributor role across multiple subscriptions, and you see suspicious activity in more than one of them.
- A management group-scoped role assignment was created during the incident window.

**If any signal matches → classify this incident as multi-subscription compromise.** Continue to Step 4 and use the multi-subscription recovery posture.

**If no signal matches → continue to Check 3.**

### Check 3: Subscription-wide compromise (single subscription)

**Signals (any one is sufficient):**
- Suspicious activity affects **most or all** database resources in the subscription, not just one or two.
- A subscription-scoped Owner or Contributor role assignment was created for an unrecognized identity during the incident window.
- Subscription-level diagnostic settings, policy assignments, or Resource Locks were deleted or modified during the incident window.
- Resources outside your SQL scope (storage accounts, key vaults, networking) also show suspicious changes in the same subscription.

**If any signal matches → classify this incident as subscription-wide compromise.** Continue to Step 4 and use the subscription-wide recovery posture.

In this document, **subscription-wide compromise** means the subscription is no longer trustworthy as a boundary. Treat all SQL resources in that subscription as potentially compromised from a control-plane perspective until you complete cleanup. That does **not** automatically mean every database or instance needs restore. Restore only the resources with confirmed damage or suspected poisoned state.

**If no signal matches → continue to Check 4.**

### Check 4: Single-resource or small-scope compromise

**Signals (all should be true):**
- Suspicious activity is confined to **one or a small named set** of databases or managed instances.
- The Caller identity that performed the suspicious actions has role assignments scoped only to those specific resources, or to a single resource group containing those resources.
- No subscription-scoped or tenant-scoped role assignments were modified.
- Other database resources in the same subscription show no suspicious activity in the Activity Log or service-level audit logs.

**If all signals match → classify this incident as single-resource-scoped compromise.** Continue to Step 4 and use the single-resource recovery posture.

### Scope classification summary

| Scope (from checks above) | What it means | Continue to |
|---|---|---|
| Tenant-level | The identity plane might be compromised | [Step 4](ransomware-recovery-restore.md#step-4-restore-recover-your-data), starting with the tenant-level actions |
| Multi-subscription | More than one subscription shows suspicious activity | Step 4, multi-subscription recovery posture |
| Subscription-wide | Treat the subscription's control plane as untrusted; assess database damage separately | Step 4, subscription-wide recovery posture |
| Single resource | The impact appears limited to named resources or one resource group | Step 4, single-resource recovery posture |

**If you can't determine your scope with confidence (signals are ambiguous):** classify the incident at the next-wider scope level, then use that wider scope in Step 4.

## 3.5 Determine what the attacker did

**Unlike scope, this section is not stop-at-first-match.** Work through every row. Multiple damage types can occur in the same incident. For example, theft followed by encryption or destruction is a double-extortion pattern, and backup tampering can precede visible damage.


| What happened | Evidence to look for | Impact on your data | What it changes |
|---|---|---|---|
| **Theft only**: data copied out, nothing altered | Billing or egress spike; high read volume; no drop in data space used or row counts | **Potential confidentiality impact.** Integrity and availability appear intact | **No restore needed.** Document confirmed and potential access for assessment by legal, privacy, and compliance teams |
| **Encryption in place**: values overwritten with attacker-encrypted content | Write throughput, log rate, and IO spikes with **no** row-count drop; `UPDATE` in `statement_s` | **Integrity lost.** Records present but unusable | Restore to a point before the first suspicious write |
| **Destruction**: rows, tables, or schemas removed | An unexplained data-space anomaly corroborated by `DELETE`, `DROP`, or `TRUNCATE` in audit evidence | **Availability and integrity lost.** Records destroyed | Restore to a point before the first destructive statement |
| **Resource deletion**: the database, logical server, or managed instance removed | `delete` in the Activity Log against the SQL resource | **Availability lost.** Records destroyed with the resource | Path 4 in Step 4; what you recover depends on soft delete and LTR |
| **Key access seized**: TDE protector rotated, revoked, or deleted | Vault-level `Microsoft.KeyVault` operations in the Activity Log; key delete, purge, or rotation in the Key Vault `AuditEvent` diagnostic logs; the database moves to *Inaccessible* | **Availability lost.** This signal alone does not establish whether confidentiality or integrity were affected; assess them using the other rows | Restore key access first. If key access is restored and no data was altered, a data restore might not be needed; see below |
| **Recovery path sabotaged**: backups or their protection removed | PITR retention shortened to the minimum supported value, LTR policies or backups removed, backup protections changed, logical-server soft-delete retention set to `0`, resource locks removed, or `diagnosticSettings` deleted | **Recoverability lost.** Production data may be untouched | Determines whether a restore is possible; backup tampering can precede visible damage |
| **Slow tampering**: small, targeted changes over a long period | Data that looks plausible but does not reconcile against business records, application history, or independent system-of-record evidence | **Integrity lost, partially and covertly.** Records available but untrustworthy | You can still restore, but *which* point is clean becomes the hard question. Widen your margin |

### Assess recovery when TDE key access is compromised

Rotating the TDE protector re-encrypts the database encryption key, not the database. Existing backups aren't updated to use the new protector and still require the key versions used to encrypt them. For details, see [TDE protector rotation considerations](/azure/azure-sql/database/transparent-data-encryption-byok-key-rotation#important-considerations-when-rotating-the-tde-protector).

Assess the available recovery options:

- **Restore database access:** If the database is inaccessible because it can't access the TDE protector, restore authorized access to the required key and follow the [key-access recovery procedure](/azure/azure-sql/database/transparent-data-encryption-byok-overview#inaccessible-tde-protector). Recovery steps depend on the cause and duration of key unavailability.
- **Restore clean data:** If a data restore is required, select a verified clean backup from before the approved recovery cutoff. Confirm that the recovery target has all key versions required to restore that backup.

> [!IMPORTANT]
> Don't delete, disable, or purge keys required by existing databases or retained backups. Restoring key access doesn't mean the database contents are unchanged.

### Assess confirmed and potential data exposure

Document:

- Confirmed data access or exfiltration supported by evidence.
- Potential exposure based on the compromised identity's effective permissions during the incident window.
- Supporting evidence, known gaps, and uncertainties.

SQL audit records can include `SELECT` statements, but they don't by themselves establish which data was returned or exfiltrated. Correlate them with application, network, and other available evidence. Missing audit evidence doesn't establish that no access occurred.

Provide these findings to the incident-response, legal, privacy, compliance, and other appropriate stakeholders. They determine applicable notification, preservation, disclosure, and response obligations.

**Also check for these conditions, which can accompany the damage above without being damage themselves:**

- **Persistence**: new logins, service principals, role assignments, or firewall rules created so the attacker can return after you evict them. This condition does not change your restore; it changes what "recovered" means. Audit for it in Step 5.
- **Lockout without data damage**: the admin password changed, logins disabled, or the firewall set to deny-all. The data is fine. Rebuild the access configuration rather than restoring data.
- **A demand with no evidence**: an extortion claim where nothing in your logs shows access. Do not act on the demand alone; hand it to Microsoft Support and your incident response lead.

**Record which types apply.** You use this information at the Decision Point below, and again in Step 4, where the restore paths differ.

## 3.6 Preserve available recovery points

You might not be able to restore immediately: the attacker might still be present, and your incident-response team, with Microsoft Incident Response if engaged, might still be defining the recovery strategy. Meanwhile, you can lose the clean restore point you identified in several ways, including:

- Automated PITR backups roll off as retention expires (7 to 35 days).
- An attacker with surviving access deletes the backups or the server.
- LTR was never configured, so there's no older LTR backup to fall back to.

Protect the clean point now so it survives the time it takes to evict the attacker and plan recovery.

1. **Extend PITR retention to the maximum supported value immediately** so available clean points don't roll off during the investigation. (Azure SQL Database / Managed Instance → **Backups** → **Retention policies**.) Most databases support 35 days; Basic-tier databases in Azure SQL Database cap at 7 days. Increasing retention doesn't recreate expired points. If the clean point is outside the available PITR window, don't attempt PITR in step 2: create a holding copy from a verified existing clean LTR backup using [Step 4, Path 2](ransomware-recovery-restore.md#path-2-long-term-retention-restore-clean-point-unavailable-in-pitr), or preserve another validated recovery artifact. Then continue with step 3 to protect backups from deletion.
2. **Make a durable copy of the clean point now, before you're ready to cut over:**
   - **Azure SQL Database:** point-in-time restore the clean point into an isolated holding database on the same logical server. Cross-server and cross-subscription PITR aren't supported. Keep this database disconnected from production; after validation, transfer it to a clean subscription using a [cross-subscription database copy](database-copy.md#copy-to-a-different-subscription) (transactionally consistent; same-tenant copies support Microsoft Entra authentication with sufficient access on both servers, while cross-tenant copies require the documented SQL-authentication workflow), a BACPAC, or another supported migration method.
   - **SQL Managed Instance:** first perform PITR for the approved recovery cutoff into an isolated holding database on a trusted target managed instance. Validate that restored database. When a durable portable copy is required, check encryption eligibility before creating a `COPY_ONLY` full backup: this workflow is unavailable for databases encrypted with service-managed TDE. For an eligible database using customer-managed TDE, retain the required key versions and test restoration to the intended target, then protect the completed backup with Blob immutability. If ineligible, preserve available PITR/LTR recovery options and use only a separately tested export or migration alternative for a portable copy. A `COPY_ONLY` backup taken from the current compromised database doesn't recover a historical clean point.

   **BACPAC safety rule:** A BACPAC is a logical export, not a backup. Create it only from a quiesced, isolated, validated copy. It excludes server-level configuration and doesn't provide consistency across databases. Record the source database, recovery cutoff, export time, validation evidence, and all dependencies that must be rebuilt. Use a common approved cutoff and separate reconciliation when multiple databases or services are involved.
3. **Protect backups from deletion:** Automatic backup immutability protects up to the **most recent 7 days** of PITR backups by default on supported databases. It excludes Hyperscale databases and SQL Managed Instances using the SQL Server 2022 update policy. A very recent clean point on a supported database therefore has some built-in protection. PITR points older than seven days remain mutable unless separately protected and can be lost if the source logical server or instance is deleted. Apply a [`CanNotDelete` Resource Lock](/azure/azure-resource-manager/management/lock-resources) and don't delete the compromised server or instance. For a clean point captured in an immutable LTR backup, verify the actual backup's protection state and retention. This verification includes SQL Managed Instance LTR immutability where the preview is available and configured; don't assume every LTR backup is immutable. For completed and validated BACPAC or `COPY_ONLY` artifacts, verify the applicable Blob immutability policy or legal hold. Confirm the selected recovery artifact exists, is protected from the compromised identities, and can be restored before relying on it.
4. **Alert on backup tampering:** watch the Activity Log for PITR retention shortened to the minimum supported value, LTR policies or backups removed, backup protections changed, logical-server soft-delete retention set to `0`, and backup deletions during the incident window.

> [!IMPORTANT]
> Keep recovery copies isolated from production. Complete the [recovery-readiness checks](#decision-point-are-you-ready-to-restore), [Step 4: Restore](ransomware-recovery-restore.md), and [Step 5: Validate](ransomware-recovery-validate.md), and obtain the required cutover approvals before reconnecting production traffic.

## Decision point: Are you ready to restore?

Don't proceed until you can confidently answer these questions. You need to defend the restore point and avoid immediate re-compromise. **Write the answers down.** They form the basis for the restore decision and are part of your incident record.

- What is the compromise window, in UTC, and what evidence defines its earliest and latest bounds?
- Which identity or application path entered the environment, and did you close that access?
- Which databases, managed instances, logins, keys, and related Azure resources were affected?
- What type of damage occurred, and does this incident require a data restore?
- What approved recovery cutoff predates the earliest credible indicator with an appropriate safety margin?
- Do SQL audit logs exist for the incident window, or are you working from control-plane evidence alone? If the latter, widen your safety margin.
- Did any evidence indicate tenant-level compromise that requires clearing the identity plane before database recovery?
- Is your breach window within the available PITR (up to 35 days) or LTR (up to 10 years) retention? If not, see the limitations in Step 4.
- Which databases and services must use a common approved recovery cutoff?
- What cross-database and cross-system reconciliation is required, and who accepts any unavoidable timestamp skew before cutover?

---

## Related content

- [Azure Activity Log](/azure/azure-monitor/fundamentals/activity-log)
- [Microsoft Entra data retention](/entra/identity/monitoring-health/reference-reports-data-retention)
- [Auditing for Azure SQL Database](auditing-overview.md)
- [Azure Key Vault logging](/azure/key-vault/general/logging)
- [Change automated backup retention settings](automated-backups-change-settings.md)
- [Immutable storage for Azure Blob Storage](/azure/storage/blobs/immutable-storage-overview)
- [Respond to a compromised TDE protector](transparent-data-encryption-byok-remove-tde-protector.md)

## Page navigation

- [Overview](ransomware-recovery-overview.md)
- Previous: [Step 2: Contain](ransomware-recovery-contain.md)
- Next: [Step 4: Restore](ransomware-recovery-restore.md)
