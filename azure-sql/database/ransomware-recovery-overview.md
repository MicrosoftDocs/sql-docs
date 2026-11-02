---
title: "Overview: Recover Azure SQL Database and Azure SQL Managed Instance from a ransomware attack"
description: Understand the ransomware recovery lifecycle for Azure databases and the steps to protect, detect, contain, assess, restore, validate, and harden your environment.
author: sudhanshukhera
ms.author: skhera
ms.title: "Overview: Recover Azure SQL Database and Azure SQL Managed Instance from a ransomware attack"
ms.date: 10/31/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Overview: Recover Azure SQL Database and Azure SQL Managed Instance from a ransomware attack

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This guide helps you prepare Azure SQL Database and Azure SQL Managed Instance for ransomware incidents and recover safely if one occurs. It covers preventive controls, emergency containment, evidence-based scoping, clean-point selection, restore options, validation, and post-incident hardening.
## 🚨 First 15 minutes: Emergency actions card

This card covers immediate actions only. Continue through the full guide to complete your incident response and recovery.

**If you have reason to believe your database is compromised, start these actions now, in parallel where possible:**

1. **Disable public network access and remove untrusted firewall rules** for the affected server or instance, not as a subscription-wide sweep. → **Step 2: Contain, [Action 3](ransomware-recovery-contain.md#action-3-block-attacker-access-at-the-network-and-identity-layer)**
2. **Disable the specific compromised SQL authentication and kill its live sessions.** → **Step 2: Contain, [Action 4](ransomware-recovery-contain.md#action-4-rotate-sql-credentials-and-application-secrets)** (disable) + **[Action 2](ransomware-recovery-contain.md#action-2-revoke-all-active-sessions-for-suspicious-accounts)** (kill sessions)
3. **Revoke that identity's Entra session from a break-glass account. Never revoke your own session.** → **Step 2: Contain, [Action 2](ransomware-recovery-contain.md#action-2-revoke-all-active-sessions-for-suspicious-accounts)**
4. **Preserve available recovery points now:** extend PITR to the database's maximum supported retention (35 days, or 7 days for Basic-tier Azure SQL Database), and preserve a durable recovery copy before available points expire. → **Step 3: Assess, [3.6](ransomware-recovery-assess.md#36-preserve-available-recovery-points)**
5. **[Open an Azure support case](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation), preserve evidence, and don't delete anything.** The linked support guidance explains how to open the case and what to include. The evidence-preservation checklist is at the start of **[Step 2: Contain](ransomware-recovery-contain.md#step-2-contain-stop-the-attack)**. If a managed instance was deleted, [contact Support](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation) **within 5 days of deletion** and follow **Step 4: Restore, [Path 4](ransomware-recovery-restore.md#path-4-recover-a-deleted-database-server-or-instance)**.
6. **Follow your organization’s incident-response process (see [Microsoft’s incident-response guidance](/security/operations/incident-response-overview#incident-response-process)) and engage appropriate legal, privacy, compliance, records-management, and cyber-insurance teams.** They determine notification, preservation, disclosure, and ransom-response obligations.

**Tenant-level check:** Is a Global Administrator or Privileged Role Administrator account suspected compromised? If yes, don't run a broad Entra sweep on steps 2–3 above; go straight to **Step 4: Restore, [clearing the identity plane first](ransomware-recovery-restore.md#first-if-the-compromise-is-tenant-level-clear-the-identity-plane)**.

**Not entirely sure an attack is even happening?** Go to **[Step 1: Detect](ransomware-recovery-detect.md#step-1-detect-recognize-the-attack)** first to confirm before acting on the steps above.

Once containment is underway, continue to **[Step 1: Detect](ransomware-recovery-detect.md#step-1-detect-recognize-the-attack)**, then follow **Steps 2–6**. Completing this card doesn't mean recovery is complete.

---

## Who does what during response

**You** drive the containment and recovery actions in this guide. You're responsible for managing your incident response, including detection, containment, scoping, restore decisions, and evidence preservation. Involve the appropriate legal, privacy, and compliance teams. Microsoft can assist with support, escalation, and platform-side operations, but recovery options depend on the recovery copies, retention, and other protections available in your configured environment.

The roles break down like this:

| Party | Role during your incident |
|---|---|
| **You (customer)** | **Drive the response end to end:** detection, containment, scoping, restore decisions, and evidence preservation. Engage appropriate legal, privacy, compliance, and other stakeholders to assess applicable notification and disclosure requirements. |
| **Microsoft** | **Assists.** Microsoft Support triages your case, advises on platform behavior, and performs platform-side operations. [Microsoft Incident Response](https://www.microsoft.com/en-us/security/business/microsoft-incident-response), formerly DART, is a separate specialist service that can help investigate the compromise, evict the attacker, and plan recovery when engaged through your organization's contracted channel. |

**Bottom line:** Microsoft supports and advises; you make the decisions and execute the recovery. Engage [Azure Support](ransomware-recovery-contain.md#action-1-activate-incident-response-and-microsoft-escalation) early and, if contracted, engage Microsoft Incident Response separately for tenant-level compromise. Don't wait on either engagement to begin containment.

---

## About this guide

**Whose environment this guide is about.** This guide covers attacks that reach your database through customer-controlled identities, networks, applications, and configurations, not compromise of the Azure platform itself. The response actions in this guide therefore focus on your subscription and tenant: resetting identities, closing network paths, and rebuilding trust boundaries.

**What we mean by a ransomware attack.** Any incident where an attacker with unauthorized access makes your data unavailable and demands payment to restore it. Against databases, it can take one or more of these forms:

- **Encryption in place**: a mass `UPDATE` that rewrites your values
- **Destruction**: bulk `DELETE`, `DROP TABLE`, or `TRUNCATE`
- **Deletion**: the database, logical server, managed instance, or backups removed outright
- **Extortion**: a copy stolen and threatened with public release, sometimes called "double extortion"

The common thread is loss of access to trustworthy data. **A ransom demand isn't required to use this guide.** An intrusion can end in pure destruction, or in theft alone, with no offer to restore anything: an attacker who deletes your databases beyond recovery might not have a restoration to offer. Those incidents fall outside the strict definition of ransomware, but much of the detection, containment, and recovery work overlaps. Use this guide whenever an attacker reaches your data, whether or not anyone asks you for money. If a ransom demand is received, handle it through your organization’s established incident-response and decision-making processes rather than as a technical recovery decision. [Step 3.5](ransomware-recovery-assess.md#35-determine-what-the-attacker-did) has the operational version of this list: what each kind of damage looks like in your logs, and what it changes about your recovery.

---

## Incident response flow

This guide spans **seven steps**: Use this table as a map. Each row briefly describes one step, and the last column links to the full procedure. If you're in the middle of an incident, use the 🚨 Emergency Actions Card first, then work through the steps in order.

| Step | Purpose | Full procedure |
|---|---|---|
| **0. Protect** (before any attack) | Prepares the environment to reduce attack risk and support recovery. | [Step 0: Protect](ransomware-recovery-protect.md#step-0-protect-prepare-your-azure-sql-databases) |
| **1. Detect** | Identifies signs of a suspected or confirmed compromise. | [Step 1: Detect](ransomware-recovery-detect.md#step-1-detect-recognize-the-attack) |
| **2. Contain** | Stops ongoing attacker activity and limits further damage or spread. | [Step 2: Contain](ransomware-recovery-contain.md#step-2-contain-stop-the-attack) |
| **3. Assess** | Determines the incident's scope, timeline, and impact to guide recovery decisions. | [Step 3: Assess](ransomware-recovery-assess.md#step-3-assess-damage-determine-what-was-affected) |
| **4. Restore** | Recovers affected data or access in a trusted environment after attacker access is contained. | [Step 4: Restore](ransomware-recovery-restore.md#step-4-restore-recover-your-data) |
| **5. Validate** | Confirms recovered data and systems are trustworthy before production resumes. | [Step 5: Validate](ransomware-recovery-validate.md#step-5-validate-confirm-the-recovery-is-clean) |
| **6. Harden** | Addresses the gaps exposed by the incident to strengthen future protection and recovery. | [Step 6: Harden](ransomware-recovery-harden.md#step-6-harden-close-the-gaps-that-let-the-attack-succeed) |

---

## Related content

- [Microsoft incident response process](/security/operations/incident-response-overview#incident-response-process)
- [Create an Azure support request](/azure/azure-portal/supportability/how-to-create-azure-support-request)
- [Microsoft Incident Response ransomware case study](/security/ransomware/dart-ransomware-case-study)

## Page navigation

- Next: [Step 0: Protect](ransomware-recovery-protect.md)
