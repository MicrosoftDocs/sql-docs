---
title: Contain the attack
description: Learn how to contain a ransomware attack and limit further impact on your Azure database environment.
author: sudhanshukhera
ms.author: skhera
ms.title: Step 2 - Contain - Stop the attack
ms.date: 10/31/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Step 2: Contain: Stop the attack

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

> [!IMPORTANT]
> Don't delete affected resources, logs, or diagnostic settings. Preserve evidence in parallel with containment; don't delay containment while exports complete.

### Preserve incident evidence

Follow your organization's incident-response and records-preservation procedures. Preserve affected SQL databases, logical servers, managed instances, Log Analytics workspaces, diagnostic settings, storage accounts containing logs, and other resources relevant to the incident.

1. Export available Activity Log records for the last 90 days to CSV.
2. Export available Microsoft Entra sign-in and audit logs to CSV, and preserve available SQL audit and diagnostic logs.
3. Save copies to a restricted location that compromised identities can't access. Record the export scope, time, and file hashes.

Exported files remain mutable. Apply immutable storage protection when available, but don't wait for it before preserving evidence.

## Action 1: Activate incident response and Microsoft escalation

**How to open:** To open a support request, see [Create an Azure support request](/azure/azure-portal/supportability/how-to-create-azure-support-request) for portal steps, required permissions, and support-plan requirements. Select the service and subscription affected by the issue, and choose a [severity that reflects the business impact](https://azure.microsoft.com/support/plans/response/). Available severity levels and response targets depend on your support plan.

If your organization has a Microsoft Incident Response agreement and you suspect tenant-level compromise, separately engage [Microsoft Incident Response](https://www.microsoft.com/en-us/security/business/microsoft-incident-response) through your contracted channel. Opening an Azure support case doesn't initiate that separate engagement. If destructive activity is ongoing, containment begins immediately and doesn't wait for case triage. Otherwise, coordinate disruptive eviction with the incident lead while support escalation and evidence capture proceed in parallel.

**What to include:** Provide what you know now; don't delay urgent escalation to complete the list.
- **Affected resources:** tenant and subscription IDs, resource IDs or names, Azure SQL service, region, and affected databases or instances.
- **Impact and timeline:** affected business functions, whether destructive activity is ongoing, the incident start time in UTC, and any resource-deletion time.
- **Requested assistance:** the platform operation or recovery question that needs help, such as a restore failure, loss of key access, deleted-resource recovery eligibility, or tenant lockout.
- **Recovery and diagnostics:** available backup types and dates, the desired recovery point in UTC, target-resource details where applicable, relevant error messages, and operation or correlation IDs. For a deleted managed instance, include the target-instance details required by [Step 4, Path 4](ransomware-recovery-restore.md#path-4-recover-a-deleted-database-server-or-instance).
- **Actions and contact:** containment or recovery attempts and their results; complete the request form's contact-information and availability fields.

Don't put passwords, tokens, private keys, or customer records in the case description. Share only approved diagnostic material through the [support file-upload process](/azure/azure-portal/supportability/how-to-manage-azure-support-request#file-upload-guidelines), and preserve the original forensic evidence unchanged.

**If the tenant is locked out:** Follow [Microsoft's tenant-lockout recovery guidance](/entra/architecture/recoverability-tenant#recover-from-customer-microsoft-entra-id-tenant-lockout) and use the [customer-service phone numbers](https://support.microsoft.com/topic/customer-service-phone-numbers-c0389ade-5640-e588-8b0e-28de8afeb3f2) when you can't access the portals. Provide the affected tenant ID if known and follow Microsoft's ownership-verification process.

## Action 2: Revoke all active sessions for suspicious accounts

1. Go to: `portal.azure.com` → **Microsoft Entra ID** → **Users**
2. Find the compromised or suspected account
3. Select the account name
4. Select **Revoke sessions** in the top action menu
5. Select **Reset password** and require MFA re-registration for that user

> [!IMPORTANT]
> Revoking Microsoft Entra sessions doesn't immediately block every access path. Existing access tokens can remain valid until they expire. SQL credentials, workload identities, application sessions, and open database connections require separate containment.

Combine session revocation with the actions required for the identified access paths: password resets, role and consent removal, workload shutdown or identity detachment, SQL login disablement, database-session termination, secret rotation, and network restrictions.

If you can't identify the compromised account, use a trusted break-glass account and coordinate a broader privileged-session reset with the identity-plane incident lead. Preserve at least one verified emergency access path, record which accounts are included, and combine session revocation with password resets, role review, workload-identity containment, and active database-session termination.

For Global Administrator accounts included in that coordinated reset, use these portal steps:

1. **Entra ID** → **Roles and administrators** → **Global Administrator**
2. Select only the members included in the agreed reset → **Revoke sessions**

> [!IMPORTANT]
> Before starting a coordinated privileged-session reset, verify access through a trusted [emergency access account](/entra/identity/role-based-access-control/security-emergency-access). Exclude the account used to perform the reset from that operation. Don't revoke your own session, and preserve at least one verified emergency access path throughout containment.

Also review non-human identities:
- Disable or restrict suspicious service principals and enterprise applications
- Review managed identities used by automation or applications and disable any you don't trust
- Remove or suspend privileged role assignments you don't recognize until the incident is contained

**Entra revocation doesn't end an already-open SQL connection.** Block the identity **before** you kill its sessions, or it can simply reconnect:

- **SQL-authenticated login:** disable it first with `ALTER LOGIN [login_name] DISABLE;` in `master`. For Azure SQL Database, then run [`DBCC FLUSHAUTHCACHE`](/sql/t-sql/database-console-commands/dbcc-flushauthcache-transact-sql) in each user database accessible to the login to refresh cached login metadata before terminating sessions (see [Action 4](#action-4-rotate-sql-credentials-and-application-secrets)).
- **Microsoft Entra identity:** complete the identity-containment steps above, then block effective SQL access through the applicable user, group, login, or administrator mapping, or restrict the compromised network path. Account for every access mapping: Azure role removal alone doesn't remove SQL permissions, and issued access tokens can remain usable after session revocation. Confirm the identity can't reconnect before proceeding.

Then kill its live sessions. Run in a query window against the affected database (Azure SQL Database) or instance (SQL Managed Instance):
```sql
SELECT session_id FROM sys.dm_exec_sessions
WHERE original_login_name = '<name>' OR login_name = '<name>';
KILL <session_id>;  -- repeat for each row returned
```

`KILL` requires the `KILL DATABASE CONNECTION` permission in Azure SQL Database (the server admin has it) or `ALTER ANY CONNECTION` on SQL Managed Instance.

## Action 3: Block attacker access at the network and identity layer

- Azure SQL firewall rules are allow rules, not an IP deny-list. Remove broad or untrusted allow rules, disable public network access where possible, and restrict connectivity to trusted private endpoints, subnets, or responder jump hosts. Use NSGs, Azure Firewall, or application controls when a specific source must be blocked.
- For dynamic or unknown attacker IPs, temporarily restrict all public access and allow only trusted responder paths.
- If you're using private endpoints, restrict connectivity to trusted subnets and confirm the endpoint and DNS paths are controlled.
- Apply a [Conditional Access](/entra/identity/conditional-access/overview) emergency policy approved by the incident lead.
- Review and disable any [Azure Lighthouse](/azure/lighthouse/overview) or delegated admin assignments that might allow untrusted third-party access.

## Action 4: Rotate SQL credentials and application secrets

Revoking Entra sessions (Action 2) doesn't affect database-level or application-level credentials. An attacker who loses their Entra access but keeps a SQL login is both alerted and still inside, which is the worst combination. Sequence this action with Action 2 rather than after it.

**What to rotate depends on how the identity authenticates**: Azure SQL supports SQL authentication and Microsoft Entra authentication side by side, and each method requires a different action:

- **SQL authentication (logins and contained database users with a password):** Reset the password, then update the password value in every connection string, environment variable, deployment secret, and pipeline variable that references it.
  - Server-level logins: `ALTER LOGIN [login_name] WITH PASSWORD = '<new_password>'`
  - Contained database users: `ALTER USER [user_name] WITH PASSWORD = '<new_password>'`
  - **Stopgap for a server-level login before rotating its password:** run `ALTER LOGIN [login_name] DISABLE;`. For Azure SQL Database, then run [`DBCC FLUSHAUTHCACHE`](/sql/t-sql/database-console-commands/dbcc-flushauthcache-transact-sql) in every affected user database to refresh cached login metadata. Complete this sequence before terminating existing sessions in Action 2, and maintain network containment. Re-enable the login only after rotating its password.
  - See [Manage logins and user accounts](logins-create-manage.md).
- **Microsoft Entra-authenticated human users:** There's no separate database password to rotate. The credential is the Entra identity itself; resetting the user's password and revoking sessions in Action 2 already covers this.
- **Microsoft Entra-authenticated app or service principals:** Rotate the client secret or certificate on the app registration in Microsoft Entra ID, then update every place that stores it (Key Vault, pipeline variables, application configuration). There's nothing separate to change on the SQL side.
- **Managed identities:** The platform fully generates, rotates, and protects credential material; you can't manually rotate it. If you suspect a managed identity is compromised or misused, remove or restrict its database permissions and Azure role assignments, or disable and recreate the identity to force new credential material.

**Also rotate:**
- **Key Vault:** Rotate all secrets, certificates, and keys used by the SQL workload or accessible to any compromised identity.
- **Automation and backup jobs:** Review any service principals or managed identities they use against the guidance in this article, and restrict or remove any exposed role assignments and permissions.

## Action 5 (conditional): Revoke customer-managed key (CMK) access for BYOK accounts

If your database or managed instance uses customer-managed keys (CMK / BYOK) for transparent data encryption (TDE), contact your key custodian immediately to revoke the attacker's access to the Key Vault key. Restoration requires restoring key access first, so coordinate the timing with your incident response lead so the restore doesn't stall on a missing key.

> [!WARNING]
> Revoking the logical server's or managed instance's managed identity access to the TDE protector can make affected databases inaccessible. Preserve the database service's required key access when removing permissions from suspicious users or applications.

If containment requires intentionally making the database unavailable, obtain and record the incident lead's approval before revoking the database service's key access.

1. Go to: `portal.azure.com` → **Key Vaults** → [the vault holding your TDE protector key]
2. **Access policies** or **Access control (IAM)** → remove or deny access for any identity you suspect is compromised.
3. If the compromised identity can't be isolated yet, preserve key access for the SQL server or managed instance identity that requires `get`, `wrapKey`, and `unwrapKey`, plus explicitly approved key custodians or emergency responders. Never restrict the protector to a human [break-glass](ransomware-recovery-protect.md#authentication-verify-identity-before-granting-access) identity alone. A break-glass account is for emergency sign-in, not routine key-vault access, and if it's the only path to the key you created a new single point of failure.
4. Notify your incident response lead before any restore is attempted; restoring an encrypted database without the wrapping key fails.
5. **Don't delete or purge the vault or the key.** If the attacker already deleted either, recover it from soft-delete before any restore attempt. Audit who holds purge permissions.
6. **If you suspect the key itself was copied, still don't delete it.** Revoke untrusted backup and restore permissions, create a replacement key in a separate vault, change the active TDE protector, and verify database health. Retain every previous protector and key version in an enabled, recoverable state until all backups encrypted with it have expired or been retired through an approved retention decision. Don't disable, delete, or purge an old protector merely because rotation completed. See [Key Vault backup and restore](/azure/key-vault/general/backup#security-considerations).

---

## Related content

- [Create an Azure support request](/azure/azure-portal/supportability/how-to-create-azure-support-request)
- [Revoke user access in Microsoft Entra ID](/entra/identity/users/users-revoke-access)
- [Manage emergency access accounts in Microsoft Entra ID](/entra/identity/role-based-access-control/security-emergency-access)
- [Manage Azure SQL logins and user accounts](logins-create-manage.md)
- [IP firewall rules for Azure SQL Database](firewall-configure.md)
- [Transparent data encryption with customer-managed keys](transparent-data-encryption-byok-overview.md)
- [Key Vault backup and restore security considerations](/azure/key-vault/general/backup#security-considerations)

## Page navigation

- [Overview](ransomware-recovery-overview.md)
- Previous: [Step 1: Detect](ransomware-recovery-detect.md)
- Next: [Step 3: Assess](ransomware-recovery-assess.md)
