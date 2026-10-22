---
title: Secure your Azure SQL Database
description: Learn how to secure Azure SQL Database with best practices for protecting data, managing access, and defending against common threats.
author: msmbaldwin
ms.author: mbaldwin
ms.service: azure-sql-database
ms.subservice: security
ms.topic: best-practice
ms.custom: horz-security
ms.date: 09/10/2026
ai-usage: ai-generated
---

# Secure your Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database is a fully managed platform as a service (PaaS) database engine that handles most database management functions, such as upgrading, patching, backups, and monitoring, without user involvement. Because it often stores critical business data, including customer records, financial information, and intellectual property, securing your Azure SQL Database is essential to protect against data breaches, unauthorized access, and compliance violations.

This article provides security recommendations to help protect your Azure SQL Database deployment.

[!INCLUDE [Security horizontal Zero Trust statement](~/../reusable-content/ce-skilling/azure/includes/security/zero-trust-security-horizontal.md)]

## Reduce attack surface and mitigate threats

Because Azure SQL Database is a managed PaaS engine, Microsoft hardens the operating system and infrastructure. Your responsibility centers on reducing the application and data surface exposed to attack and on detecting threats early.

- **Run SQL vulnerability assessments**: Use SQL vulnerability assessment to discover, track, and remediate potential database misconfigurations and vulnerabilities. Schedule recurring scans and act on the baseline it establishes. For more information, see [SQL vulnerability assessment](/azure/defender-for-cloud/sql-azure-vulnerability-assessment-overview).

- **Protect against SQL injection**: Use parameterized queries and stored procedures in your applications, and never concatenate user input into SQL statements. SQL injection remains one of the most common attack vectors against database applications. For more information, see [SQL injection](/sql/relational-databases/security/sql-injection).

- **Apply defense in depth**: Combine network isolation, identity controls, data protection, and monitoring so that no single control is a single point of failure. For more information, see [Playbook for addressing common security requirements](security-best-practice.md).

## Network security

Network security for Azure SQL Database helps prevent unauthorized connections and reduces exposure to attacks so that only trusted sources can reach your databases.

- **Use private endpoints**: Connect to your Azure SQL Database over private IP addresses by using Azure Private Link to avoid exposing your database to the public internet. Private connectivity reduces the attack surface and the risk of data exfiltration. For more information, see [Azure Private Link for Azure SQL Database](private-endpoint-overview.md).

- **Disable public network access**: When you rely on private endpoints, disable public network access entirely so all connections route through the private endpoint. For more information, see [Deny public network access](connectivity-settings.md#deny-public-network-access).

- **Configure server-level firewall rules**: Control access to your [logical server in Azure](logical-servers.md) with IP firewall rules that specify which addresses or ranges can connect, following the principle of least privilege. For more information, see [Azure SQL Database and Azure Synapse IP firewall rules](firewall-configure.md).

- **Configure database-level firewall rules**: For more granular control, configure database-level firewall rules that apply to individual databases, enabling per-database access policies. For more information, see [Database-level IP firewall rules](firewall-configure.md#database-level-ip-firewall-rules).

- **Restrict traffic to specific virtual networks**: Use virtual network rules to allow traffic only from specific subnets within your Azure virtual networks, adding network isolation beyond IP-based rules. For more information, see [Virtual network rules for Azure SQL Database](vnet-service-endpoint-rule-overview.md).

- **Set the minimum TLS version**: Configure the logical server's minimum TLS version to 1.2 or higher so connections that use older, weaker protocols are rejected. For more information, see [Minimum TLS version](connectivity-settings.md#minimum-tls-version).

- **Choose an appropriate connection policy**: Use the Proxy connection policy when you want all traffic to flow through the gateway on a single port instead of exposing the range of back-end node ports, and use Redirect for lower latency from within Azure. Understand the tradeoffs before overriding the default. For more information, see [Azure SQL Database connectivity architecture](connectivity-architecture.md).

## Identity and access management

Strong identity and authentication controls limit access to your Azure SQL Database resources to authorized users and applications, with centralized identity management and easier account lifecycle control.

### Authentication and account management

- **Configure a Microsoft Entra admin**: Designate a Microsoft Entra administrator for your logical server to enable centralized identity management and advanced authentication policies. For more information, see [Configure Microsoft Entra authentication](authentication-aad-configure.md).

- **Use Microsoft Entra authentication**: Prefer Microsoft Entra authentication over SQL authentication for centralized identity management and access to features like Conditional Access and multifactor authentication. For more information, see [Microsoft Entra authentication](authentication-aad-overview.md).

- **Disable SQL authentication when possible**: For maximum security, require all connections to use Microsoft Entra authentication and turn off SQL authentication, eliminating the risk of weak or compromised SQL passwords. For more information, see [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md).

- **Create contained database users**: Map contained database users to Microsoft Entra identities or groups instead of server-level logins when possible, simplifying permission management and reducing server-level access. For more information, see [Contained database users](/sql/relational-databases/security/contained-database-users-making-your-database-portable).

- **Enforce multifactor authentication**: Require multifactor authentication (MFA) through Conditional Access for the Microsoft Entra identities that administer and connect to the logical server, adding a layer of protection beyond passwords. For more information, see [How multifactor authentication works](/entra/identity/authentication/concept-mfa-howitworks).

- **Apply Conditional Access policies**: Use Conditional Access to control access based on user location, device compliance, and risk, providing adaptive security for each access attempt. For more information, see [Conditional access](conditional-access-configure.md).

- **Use a managed identity for the logical server**: Assign a system-assigned or user-assigned managed identity to the logical server so it can reach Azure Key Vault (for TDE customer-managed keys) and Azure Storage (for auditing) without stored secrets. For more information, see [Managed identity in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

- **Enforce strong password policies**: If you use SQL authentication, require complex passwords that can't be easily guessed, rotate them regularly, and avoid reusing them across accounts. For more information, see [Password policy](/sql/relational-databases/security/password-policy).

### Privileged access

- **Grant least privilege**: Give users only the minimum permissions required for their job functions, and review permissions regularly. For more information, see [Getting started with database engine permissions](/sql/relational-databases/security/authentication-access/getting-started-with-database-engine-permissions).

- **Separate administrative roles**: Avoid granting broad admin rights to all administrators. Use granular permissions and separation of duties across administrative functions. For more information, see [Permissions](/sql/relational-databases/security/permissions-database-engine).

- **Assign access with database roles**: Use built-in and custom database roles to implement role-based security, assigning users to roles instead of granting individual permissions. For more information, see [Database-level roles](/sql/relational-databases/security/authentication-access/database-level-roles).

- **Use Azure RBAC for management operations**: Control access to Azure SQL Database management operations with Azure role-based access control, creating custom roles that grant only the permissions needed for specific tasks. For more information, see [Azure built-in roles for databases](/azure/role-based-access-control/built-in-roles/databases).

- **Provide just-in-time privileged access**: Use Microsoft Entra Privileged Identity Management (PIM) to grant time-limited, approval-based access to administrative roles so users hold elevated privileges only when needed. For more information, see [Privileged Identity Management](/entra/id-governance/privileged-identity-management/pim-configure).

- **Monitor privileged activities**: Enable auditing to track actions performed by privileged accounts, review the logs for suspicious changes, and alert on sensitive operations. For more information, see [Auditing for Azure SQL Database](auditing-overview.md).

## Data protection

Data protection safeguards your information through encryption, masking, and classification to prevent unauthorized disclosure, tampering, or loss of sensitive information.

- **Enable Transparent Data Encryption (TDE)**: Encrypt your database, log, and backup files at rest. TDE is enabled by default for new databases; use customer-managed keys in Azure Key Vault for additional control over encryption keys. For more information, see [Transparent data encryption (TDE)](transparent-data-encryption-tde-overview.md) and [TDE with customer-managed keys](transparent-data-encryption-byok-overview.md).

- **Protect sensitive data with Always Encrypted**: Use Always Encrypted to protect highly sensitive data in use, at rest, and in transit, so that even administrators can't view plaintext values. Use secure enclaves for richer functionality. For more information, see [Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-database-engine).

- **Create tamper-evident data with ledger**: Enable ledger to create an immutable, cryptographically verifiable record of changes to sensitive data, which can help meet regulatory requirements. For more information, see [Ledger](/sql/relational-databases/security/ledger/ledger-overview).

- **Mask sensitive data with dynamic data masking**: Apply dynamic data masking to obfuscate sensitive data for nonprivileged users while preserving functionality for applications, without changing application code. For more information, see [Dynamic data masking](dynamic-data-masking-overview.md).

- **Classify and label sensitive data**: Use SQL Data Discovery and Classification to identify, classify, and label sensitive data so features like auditing and masking can use the metadata and you can report on where sensitive data resides. For more information, see [Data discovery and classification](data-discovery-and-classification-overview.md).

- **Restrict access with column-level permissions**: Grant or deny permissions at the column level so only users who need a sensitive column can read or modify it. For more information, see [GRANT object permissions](/sql/t-sql/statements/grant-object-permissions-transact-sql).

- **Restrict rows with Row-Level Security (RLS)**: Implement RLS so users can access only the data rows relevant to them, providing application-level security ideal for multitenant scenarios. For more information, see [Row-Level Security](/sql/relational-databases/security/row-level-security).

## Logging and monitoring

Comprehensive logging and monitoring help you detect anomalous activity, investigate incidents, and demonstrate compliance for your databases.

- **Enable Microsoft Defender for SQL**: Turn on Microsoft Defender for SQL to detect unusual and potentially harmful attempts to access or exploit your databases, including vulnerability assessment and advanced threat protection. For more information, see [Microsoft Defender for SQL](azure-defender-for-sql.md).

- **Integrate with Microsoft Defender for Cloud**: Use Microsoft Defender for Cloud for centralized security management, security recommendations, and integrated threat protection across your Azure resources. For more information, see [Microsoft Defender for SQL in Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-introduction).

- **Configure threat protection alerts**: Set up alerts for suspicious activity such as SQL injection attempts, anomalous access patterns, and brute-force authentication, and route notifications to the right recipients. For more information, see [Advanced threat protection](threat-detection-overview.md).

- **Enable auditing**: Configure auditing to track database events and write them to a Log Analytics workspace, Azure Storage, or Event Hubs. Audit both server-level and database-level events for full coverage. For more information, see [Auditing for Azure SQL Database](auditing-overview.md).

- **Stream diagnostic logs**: Configure diagnostic settings to send logs, including security categories such as `SQLSecurityAuditEvents`, to Azure Monitor Logs, Event Hubs, or Azure Storage for retention and analysis. For more information, see [Monitor Azure SQL Database with Azure Monitor](monitoring-sql-database-azure-monitor.md).

- **Review audit logs regularly**: Establish a process to review audit logs for suspicious activity, focusing on privileged account actions, failed authentication attempts, and access to sensitive data. For more information, see [Auditing for Azure SQL Database](auditing-overview.md).

## Compliance and governance

Governance controls help you demonstrate regulatory compliance and enforce consistent security configurations across your databases.

- **Track regulatory compliance with Microsoft Defender for Cloud**: Use the regulatory compliance dashboard to assess your databases against standards such as PCI DSS, ISO 27001, and SOC, and to track remediation of failing controls. For more information, see [Regulatory compliance dashboard](/azure/defender-for-cloud/regulatory-compliance-dashboard).

- **Enforce configurations with Azure Policy**: Assign built-in Azure Policy definitions for Azure SQL Database to audit and enforce controls such as TDE, auditing, private endpoints, and Microsoft Entra-only authentication. For more information, see [Azure Policy built-in definitions for Azure SQL Database](policy-reference.md).

- **Govern sensitive data with Microsoft Purview**: Register and scan Azure SQL Database in Microsoft Purview to catalog, classify, and govern sensitive data across your data estate. For more information, see [Connect to and manage Azure SQL Database in Microsoft Purview](/purview/register-scan-azure-sql-database).

- **Apply the Microsoft cloud security benchmark**: Use the Microsoft cloud security benchmark as an Azure-wide baseline to prioritize and validate the controls you apply to Azure SQL Database. For more information, see [Microsoft cloud security benchmark](/security/benchmark/azure/introduction).

## Backup and recovery

Reliable backup and recovery processes protect your data from loss due to failures, disasters, or attacks. They also help you meet your recovery objectives.

- **Verify automated backup configuration**: Confirm that automated backups are configured and that retention meets your requirements. Azure SQL Database provides automated backups by default with configurable retention from 1 to 35 days. For more information, see [Automated backups](automated-backups-overview.md).

- **Configure backup storage redundancy**: Choose the backup storage redundancy that matches your availability and disaster recovery needs: locally redundant (LRS), zone-redundant (ZRS), geo-redundant (GRS), or geo-zone-redundant (GZRS). For more information, see [Backup storage redundancy](automated-backups-overview.md#backup-storage-redundancy).

- **Use long-term retention for compliance**: Configure long-term retention (LTR) to store full backups for up to 10 years when compliance requires retention beyond the default period. For more information, see [Long-term retention](long-term-retention-overview.md).

- **Test backup and restore procedures**: Regularly test restores to confirm they meet your recovery time objectives and that restored databases are fully functional with intact data. For more information, see [Recover a database](recovery-using-backups.md).

- **Implement geo-restore for disaster recovery**: Use geo-restore to restore a database from geo-redundant backups to any Azure region, protecting against regional outages. For more information, see [Geo-restore](recovery-using-backups.md#geo-restore).

- **Monitor backup activity**: Track backup operations and configure alerts for failures by using Azure Monitor. For more information, see [Monitor and troubleshoot backup storage consumption](automated-backups-overview.md#monitor-costs).

## Related content

- [Security overview](security-overview.md)
- [Playbook for addressing common security requirements](security-best-practice.md)
- [Secure your Azure SQL Managed Instance](../managed-instance/secure-managed-instance.md)
- [Secure your SQL Server](/sql/relational-databases/security/secure-sql-server)
- [Well-Architected Framework security pillar](/azure/well-architected/security/)
- [Zero Trust guidance center](/security/zero-trust/)
