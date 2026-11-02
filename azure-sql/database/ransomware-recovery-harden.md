---
title: Close security gaps
description: Learn how to harden your Azure database environment after recovery to reduce the risk and impact of future ransomware attacks.
author: sudhanshukhera
ms.author: skhera
ms.title: Step 6 - Harden - Close the gaps that let the attack succeed
ms.date: 10/31/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Step 6: Harden: Close the gaps that let the attack succeed

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Begin this step after production traffic is restored. Convert every identified control gap, failed validation, and drill finding into a tracked corrective action with an owner, due date, and verification method. These actions reduce recurrence risk, strengthen future recovery readiness, and support post-incident review by leadership, auditors, regulators, and insurers.

## 1. Close every protection gap from Step 0

- Work through every item in the [Step 0 Protection Checklist](ransomware-recovery-protect.md#step-0-protect-prepare-your-azure-sql-databases) that you didn't have in place before the attack. Confirm that audit logs are routed to WORM storage in a separate subscription or tenant.
- Confirm the restored database has extended PITR retention and LTR configured. On Azure SQL Database, confirm LTR backup immutability is enabled and locked for regulated workloads. On SQL Managed Instance, LTR backup immutability is **in preview**; confirm availability and verify protection and restorability of the intended backups before relying on it. Where the preview is unavailable or unsuitable, retain a validated alternative. If that alternative uses `COPY_ONLY`, apply the [portable backup prerequisites](ransomware-recovery-protect.md#immutable-backup): service-managed TDE isn't supported for that workflow, and required customer-managed keys must remain available.

## 2. Maintain heightened monitoring for at least 30 days

- Connect [Microsoft Sentinel](/azure/sentinel/overview) or your existing security information and event management (SIEM) system.
- Watch for new suspicious sign-ins, credential use, or automation account changes.
- Treat any repeat of the original entry-point signal as a re-compromise until proven otherwise.

## 3. Legal, privacy, compliance, and insurance escalation

Follow your organization’s incident-response process and engage appropriate legal, privacy, compliance, records-management, cyber-insurance, and other stakeholders as appropriate. Notification, preservation, disclosure, ransom-response, insurance, and related requirements depend on the incident facts, applicable requirements, and policy terms. Consider these issues promptly rather than waiting for technical recovery to be completed.

Provide them with:
- the UTC incident timeline, affected systems and data, and identities with confirmed or potential access;
- preserved Activity Log, SQL audit, Entra, application, network, and other relevant evidence, including export metadata and hashes;
- known facts, evidence gaps, and uncertainties; and
- applicable cyber-insurance policy and contact information.

These materials support the assessment but do not, by themselves, determine notification decisions or constitute a complete forensic record.

Address any ransom demand through your organization’s established incident-response and decision-making processes, with involvement from appropriate legal and other stakeholders.

---

## Related content

- [Long-term backup retention overview](long-term-retention-overview.md)
- [Long-term backup immutability in Azure SQL Database](backup-immutability.md)
- [Auditing for Azure SQL Database](auditing-overview.md)
- [Microsoft Sentinel overview](/azure/sentinel/overview)
- [Microsoft incident response process](/security/operations/incident-response-overview#incident-response-process)
- [Microsoft Incident Response ransomware case study](/security/ransomware/dart-ransomware-case-study)

## Page navigation

- [Overview](ransomware-recovery-overview.md)
- Previous: [Step 5: Validate](ransomware-recovery-validate.md)
