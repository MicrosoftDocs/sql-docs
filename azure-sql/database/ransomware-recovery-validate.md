---
title: Validate the recovery
description: Learn how to validate your restored Azure database and confirm that it is ready to return to operation.
author: sudhanshukhera
ms.author: skhera
ms.title: Step 5 - Validate - Confirm the recovery is clean
ms.date: 10/31/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Step 5: Validate: Confirm the recovery is clean

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Complete and document all five checks before reconnecting production traffic. For each check, record the evidence, result, owner, and UTC completion time. Cutover requires explicit approval from the incident lead and the accountable application or business owner. Any failed or unresolved check blocks production reconnection.

## 1. Verify the restored data is clean and unpoisoned

Don't assume a restored database is clean. Verify it thoroughly before reconnecting to production.
- Compare row counts against your last known-good baseline (application-level metrics, monitoring dashboards, or business records).
- Spot-check critical records against trusted business records and validate application behavior by using isolated test traffic. Don't redirect production traffic during this check. Cutover follows completion of all five checks and explicit approval.
- Perform integrity checks: run [`DBCC CHECKDB`](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql) to confirm physical and logical database integrity, and validate constraints, triggers, and indexes. For Hyperscale databases, which don't support `DBCC CHECKDB`, run [`DBCC CHECKTABLE ('TableName') WITH TABLOCK`](/sql/t-sql/database-console-commands/dbcc-checktable-transact-sql) on each table instead.
- Independently restored databases aren't automatically application-consistent. Use a common approved recovery cutoff where the services support it, then reconcile cross-database and cross-system transactions, identifiers, queues, caches, and downstream state. Document any unavoidable timestamp skew and obtain application-owner acceptance before cutover.

## 2. Audit all RBAC assignments and automation accounts

1. In `portal.azure.com`, open **Access control (IAM)** → **Role assignments** at every scope that can affect the recovered resources. Check the subscription, the resource group, the logical server or managed instance, and the key vaults and storage accounts that recovery depends on. Include assignments inherited from management groups.
2. Review every privileged built-in and custom role assignment, not only Owner, Contributor, and User Access Administrator. SQL-specific roles also grant powerful management access. For example, a [SQL Server Contributor](/azure/role-based-access-control/built-in-roles#sql-server-contributor) can set or remove the Microsoft Entra admin. For custom roles, check the permissions they grant, not just the role name.
3. Remove any identity you don't recognize or that is no longer needed.
4. Pay special attention to service principals, managed identities, and automation accounts created during the attack window. Cross-reference with the Activity Log roleAssignment entries from Step 3.
5. Review Azure Lighthouse and delegated admin assignments.

## 3. Review database permissions and executable objects

Don't assume that a restored database or managed instance is secure because its data passes validation. Compare permissions and executable objects with a trusted baseline.

- For each database, review users, role memberships, explicit permissions, ownership, stored procedures, triggers, database-scoped credentials, external data sources, and other supported objects that can execute code or access external resources.
- For Azure SQL Database, also review the logical server's Microsoft Entra administrator assignment, logins and users in the virtual `master` database, `dbmanager` and `loginmanager` role memberships, and fixed server-role memberships.
- For SQL Managed Instance, also review server logins and roles, CLR assemblies, SQL Agent jobs, credentials, proxies, and linked servers.

Before removing or disabling unauthorized accounts, permissions, or objects, preserve the evidence and obtain approval from the incident lead.

## 4. Rotate all credentials, keys, certificates, and secrets

- On the recovered resource, reset all SQL logins and the server or instance admin login. Also reset every password-authenticated contained database user's password with `ALTER USER [user_name] WITH PASSWORD = '<new_password>';`; these credentials are stored in the database and can revert to their backed-up values during restore.
- Reset service principal credentials used by applications and automation. You can't manually rotate managed identities; remove or restrict their permissions, or disable and recreate them, as described in [Step 2, Action 4](ransomware-recovery-contain.md#action-4-rotate-sql-credentials-and-application-secrets).
- Rotate all secrets, certificates, and keys in Azure Key Vault that were accessible to any identity in the compromised environment. For the TDE protector, create a new key version or key and make it the active protector, but **retain every previous protector version** in an enabled, recoverable state until all backups encrypted with it expire. Deleting or disabling an old protector makes the backups that depend on it unrestorable.
- Update the rotated password, secret, or certificate value in application connection strings, configuration files, and Key Vault references.
- Review and rotate credentials for CI/CD pipelines, automation accounts, and webhooks.

## 5. Verify monitoring on the recovered resource

Don't assume monitoring follows the restored resource. Before production traffic returns, verify effective auditing, Defender coverage, alert scope, and successful log delivery for the restored database or managed instance. Confirm the destination receives a test event and that incident responders can query it.

---

## Related content

- [DBCC CHECKDB database integrity checks](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql)
- [List Azure role assignments using the Azure portal](/azure/role-based-access-control/role-assignments-list-portal)
- [Rotate the TDE protector and retain previous key versions](transparent-data-encryption-byok-key-rotation.md#important-considerations-when-rotating-the-tde-protector)
- [Auditing for Azure SQL Database](auditing-overview.md)
- [Microsoft Defender for Azure SQL Databases](/azure/defender-for-cloud/defender-for-sql-introduction)
- [Azure Lighthouse delegated resource management](/azure/lighthouse/overview)

## Page navigation

- [Overview](ransomware-recovery-overview.md)
- Previous: [Step 4: Restore](ransomware-recovery-restore.md)
- Next: [Step 6: Harden](ransomware-recovery-harden.md)
