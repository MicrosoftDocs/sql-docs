---
title: Detect the attack
description: Learn how to detect signs of a potential ransomware attack affecting your Azure database environment.
author: sudhanshukhera
ms.author: skhera
ms.title: Step 1 - Detect - Recognize the attack
ms.date: 10/31/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Step 1: Detect: Recognize the attack

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

**Possible initial signals:**
- A Microsoft Defender for Cloud alert arrives by email or in the portal
- Application monitoring shows sudden spikes in errors or empty query results
- A user reports that data they entered previously can't be found
- An automated job fails because a table or database no longer exists
- A billing notification shows an unexpected spike (data export can increase egress charges)
- Routine IT review of the Azure portal reveals missing resources

## If you're not sure whether an attack occurred, run these checks now

### 1. Check the Azure Activity Log for deletions or unauthorized control-plane changes
1. Go to: `portal.azure.com` → **Monitor** → [**Activity Log**](/azure/azure-monitor/essentials/activity-log)
2. Set time range to **Last 7 days** (extend to 90 days if needed)
3. Use the **Operation** filter in **Activity Log** to review deletions and create/update operations, including role-assignment and diagnostic-setting changes. Clear the operation filter between checks.
4. Review the results for unauthorized deletions or configuration changes on SQL databases, servers, managed instances, networking resources, backup resources, or storage accounts.

### 2. Check database metrics for sudden data loss

Check whether stored data dropped unexpectedly:
- **Azure SQL Database:** **Metrics** → **Data space used** (`storage`). Treat an unexplained drop as a high-priority anomaly.
- **SQL Managed Instance:** **Metrics** → **Storage space used** (`storage_space_used_mb`). This measure *instance-level allocated* storage, so a mass `DELETE` or `DROP TABLE` might not move it. Treat an unexplained drop as a high-priority anomaly, but don't treat a flat line as an all-clear. Pair it with **Log rate** (`log_rate_mbps`) and **IO bytes written** (`io_bytes_written`), which can spike during bulk destruction or in-place encryption.

Corroborate any metric anomaly with SQL audit logs, the Activity Log, application telemetry, and business records before attributing it to an attack.

Azure Monitor collects platform metrics automatically without configuration and stores them for 93 days. A single metrics chart displays at most 30 days at a time; pan the chart to see the full 93-day window.

### 3. Check Microsoft Entra ID sign-in logs
1. Go to: `portal.azure.com` → **Microsoft Entra ID** → **Monitoring** → [**Sign-in logs**](/entra/identity/monitoring-health/concept-sign-ins)
2. Look for sign-ins from countries or regions that you don't recognize.
3. Look for sign-ins at unusual hours.
4. Look for users or applications you don't recognize.

### Microsoft Defender for SQL alert coverage

When enabled, [Microsoft Defender for SQL](/azure/defender-for-cloud/defender-for-sql-introduction) can generate alerts for potential SQL injection, suspected brute-force attacks, and anomalous database logins. Alert availability depends on the protected resource type and configuration. For details, see the [published alert catalog](/azure/defender-for-cloud/alerts-sql-database-and-azure-synapse-analytics).

For Azure SQL Database and Azure SQL Managed Instance, consider the following limitations:

- **Destructive SQL operations:** The published alert catalog doesn't list dedicated alerts for mass `DELETE`, `DROP TABLE`, `TRUNCATE`, or bulk in-place `UPDATE` operations against these services. Recently added alerts might not yet be documented. Review the current catalog and validate detection coverage in your environment.
- **Row-extraction detection:** The "An abnormally large number of rows were extracted from your SQL Server" alert (`SQL.VM_DataExfiltration`) is in preview and applies only to Defender for SQL on machines. It doesn't apply to Azure SQL Database or Azure SQL Managed Instance. Don't rely on this alert to detect data exfiltration from these services.

Investigate suspicious access or reconnaissance alerts promptly and correlate them with SQL audit logs and other available telemetry. Don't wait for a data-loss alert to begin an investigation. The absence of alerts doesn't rule out a compromise.

---
## Related content

- [Defender alerts for SQL Database and Azure Synapse Analytics](/azure/defender-for-cloud/alerts-sql-database-and-azure-synapse-analytics)
- [Azure Activity Log](/azure/azure-monitor/fundamentals/activity-log)
- [Microsoft Entra sign-in logs](/entra/identity/monitoring-health/concept-sign-ins)
- [Supported metrics for Azure SQL Database](/azure/azure-monitor/reference/supported-metrics/microsoft-sql-servers-databases-metrics)
- [Supported metrics for Azure SQL Managed Instance](/azure/azure-monitor/reference/supported-metrics/microsoft-sql-managedinstances-metrics)

## Page navigation

- [Overview](ransomware-recovery-overview.md)
- Previous: [Step 0: Protect](ransomware-recovery-protect.md)
- Next: [Step 2: Contain](ransomware-recovery-contain.md)
