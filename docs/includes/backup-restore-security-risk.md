---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/05/2025
ms.service: sql
ms.topic: include
---
This section outlines the security risk associated with restoring backups from untrusted sources to any SQL Server environment, including on-premises, Azure SQL Managed Instance, SQL Server on Azure Virtual Machines (VMs) and any other environment.

### Why this matters 

Restoring SQL backup files (`.bak`) introduces a potential risk if the backup originates from an untrusted source. The security risk is exacerbated further when a SQL Server environment has multiple instances, as it amplifies the area of threat. While backups that remain within a trusted boundary pose no security issue, restoring a malicious backup can compromise the security of the entire environment.

A malicious `.bak` file can: 
- Take over the entire SQL Server instance.
- Escalate privileges and gain unauthorized access to the underlying host or virtual machine.

This attack occurs before any validating scripts or security checks can execute, which makes it extremely dangerous. Restoring an untrusted backup is equivalent to running untrusted applications on a critical server or virtual machine, and introducing arbitrary code execution into your environment. 

### Best practices

Follow these backup security best practices to reduce the threat to your SQL Server environments: 
- Treat restoring backups as a high-risk operation.
- Reduce the threat service area by using isolated instances.
- Only allow trusted backups: never restore backups from unknown or external sources.
- Only allow backups that have remained within a trusted boundary: ensure backups originate from within the trusted boundary.
- Do not bypass security controls for convenience.
- Enable [server-level auditing](/sql/t-sql/statements/create-server-audit-specification-transact-sql) to capture backup and restore events and mitigate audit evasion.