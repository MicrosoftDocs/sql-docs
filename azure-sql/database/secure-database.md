---
title: Secure your Azure SQL Database
description: Learn how to secure Azure SQL Database with best practices for protecting data, manage access, and defend against common threats.
author: VanMSFT
ms.author: vanto
ms.service: azure-sql-database
ms.subservice: security
ms.topic: concept-article
ms.custom: horz-security
ms.date: 12/05/2025
ai-usage: ai-generated
---

# Secure your Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database is a fully managed platform as a service (PaaS) database engine that handles database management functions such as upgrading, patching, backups, and monitoring without user involvement. Because it often stores critical business data including customer records, financial information, and intellectual property, securing your Azure SQL Database is essential to protect against data breaches, unauthorized access, and compliance violations.

This article provides guidance on how to best secure your Azure SQL Database deployment.

## Network security

Network security for Azure SQL Database helps prevent unauthorized connections, reduces exposure to attacks, and ensures only trusted sources can reach your databases through proper network isolation and access controls.

- **Use private endpoints for enhanced security**: Connect to your Azure SQL Database over private IP addresses using Azure Private Link to avoid exposing your database to the public internet. Private connectivity eliminates the risk of data exfiltration and reduces attack surface. For more information, see [Azure Private Link for Azure SQL Database](private-endpoint-overview.md).

- **Choose appropriate connection policy**: Understand the difference between Proxy and Redirect connection policies. Redirect provides lower latency and is recommended for connections from within Azure, while Proxy is required for connections from outside Azure. For more information, see [Azure SQL Database connectivity architecture](connectivity-architecture.md).

- **Configure server-level firewall rules**: Control access to your [logical server in Azure](logical-servers.md) by configuring IP firewall rules that specify which IP addresses or ranges can connect. Use the principle of least privilege by only allowing necessary IP addresses. For more information, see [Azure SQL Database and Azure Synapse IP firewall rules](firewall-configure.md).

- **Configure database-level firewall rules**: For more granular control, configure database-level firewall rules that apply to individual databases. This configuration allows you to implement per-database access policies. For more information, see [Database-level firewall rules](firewall-configure.md).

- **Integrate with Azure Virtual Networks**: Use virtual network rules to allow traffic only from specific subnets within your Azure virtual networks. This configuration provides an additional layer of network isolation beyond IP-based rules. For more information, see [Virtual network rules for Azure SQL Database](vnet-service-endpoint-rule-overview.md).

- **Enable connection encryption**: Configure all client connections to use encryption in transit. Azure SQL Database supports Transport Layer Security (TLS) 1.2 by default, ensuring data is protected while moving between clients and the database. TLS 1.3 is also available. For more information, see [Connectivity architecture](connectivity-architecture.md).

- **Disable public access when using private endpoints**: When using private endpoints, disable public network access entirely to ensure all connections go through the private endpoint. This configuration provides the highest level of network security. For more information, see [Deny public network access](connectivity-settings.md#deny-public-network-access).

## Identity management

Strong identity and authentication controls ensure only authorized users and applications can access your Azure SQL Database resources while providing centralized identity management and easier account lifecycle control.

- **Configure a Microsoft Entra admin**: Designate a Microsoft Entra (formerly Azure Active Directory) administrator  for your logical server to enable centralized identity management and advanced security features. This admin can manage access and authentication policies. For more information, see [Configure Microsoft Entra authentication](authentication-aad-configure.md).

- **Use Microsoft Entra authentication**: Use Microsoft Entra authentication instead of SQL authentication for centralized identity management and easier account lifecycle control. Microsoft Entra ID provides superior security and enables advanced features like conditional access and multifactor authentication. For more information, see [Microsoft Entra authentication](authentication-aad-overview.md).

- **Configure conditional access policies**: Use conditional access policies to control access based on conditions such as user location, device compliance, and risk level. This approach provides adaptive security that responds to the context of each access attempt. For more information, see [Conditional access](conditional-access-configure.md).

- **Create contained database users**: Use contained database users that map to Microsoft Entra identities or groups instead of server-level logins when possible. This approach simplifies permission management and improves security by eliminating the need for server-level access. For more information, see [Contained database users](/sql/relational-databases/security/contained-database-users-making-your-database-portable).

- **Enable multifactor authentication**: Require multifactor authentication (MFA) for administrative accounts and privileged users to add an extra layer of security beyond passwords. Microsoft Entra ID supports various MFA methods including phone verification, authenticator apps, and FIDO2 security keys. For more information, see [Multifactor authentication](authentication-mfa-ssms-overview.md).

- **Use managed identities for applications**: Enable managed identities for Azure resources to allow applications to authenticate without storing credentials. This approach eliminates the need to manage connection strings with embedded passwords and reduces the risk of credential exposure. For more information, see [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

- **Enforce strong password policies**: If you use SQL authentication, require complex passwords that can't be easily guessed. Implement password rotation policies and avoid reusing passwords across different accounts. For more information, see [Password policy](/sql/relational-databases/security/password-policy).

- **Disable SQL authentication when possible**: For maximum security, consider disabling SQL authentication entirely and requiring all connections to use Microsoft Entra authentication. This approach eliminates the risk of weak or compromised SQL passwords. For more information, see [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md).

## Privileged access

Controlling privileged access prevents unauthorized changes, reduces the impact of compromised accounts, and ensures administrative actions are properly monitored and controlled.

- **Implement least privilege access**: Grant users only the minimum permissions required to perform their job functions. Regularly review and adjust permissions to maintain the principle of least privilege. For more information, see [Getting started with database engine permissions](/sql/relational-databases/security/authentication-access/getting-started-with-database-engine-permissions).

- **Separate administrative roles**: Avoid granting admin rights to all database administrators. Use more granular permissions when possible, and implement separation of duties between different administrative functions. For more information, see [Permissions](/sql/relational-databases/security/permissions-database-engine).

- **Use Azure role-based access control (RBAC)**: Implement Azure RBAC to control access to Azure SQL Database management operations. Create custom roles that provide only the necessary permissions for specific administrative tasks. For more information, see [Azure RBAC for Azure SQL Database](/azure/role-based-access-control/built-in-roles#sql-db-contributor).

- **Monitor privileged activities**: Enable auditing to track all actions performed by privileged accounts. Regularly review audit logs for suspicious activities or unauthorized changes. Configure alerts for sensitive operations. For more information, see [Auditing for Azure SQL Database](auditing-overview.md).

- **Use database roles for access management**: Use built-in database roles and create custom roles to implement role-based security. Assign users to roles rather than granting individual permissions to simplify management and reduce errors. For more information, see [Database-level roles](/sql/relational-databases/security/authentication-access/database-level-roles).

- **Implement just-in-time access**: Use Microsoft Entra Privileged Identity Management (PIM) to provide time-limited, approval-based access to administrative roles. This approach ensures users only have elevated privileges when needed. For more information, see [Privileged Identity Management](/azure/active-directory/privileged-identity-management/pim-configure).

## Data protection

Data protection safeguards your information through encryption, access controls, and data classification to prevent unauthorized disclosure, tampering, or loss of sensitive information.

- **Enable Transparent Data Encryption (TDE)**: Use TDE to encrypt your database, log, and backup files at rest. TDE is enabled by default for all newly created Azure SQL databases. Consider using customer-managed keys in Azure Key Vault for additional control over encryption keys. For more information, see [Transparent Data Encryption (TDE)](transparent-data-encryption-tde-overview.md) and [TDE with customer-managed keys](transparent-data-encryption-byok-overview.md).

- **Implement Always Encrypted for sensitive data**: Use Always Encrypted to protect highly sensitive data in use, at rest, and in transit. This protection ensures that even database administrators can't view sensitive data in plaintext. Use Always Encrypted with secure enclaves for enhanced functionality. For more information, see [Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-database-engine).

- **Use ledger for tamper-evident data**: Enable ledger to create an immutable record of changes to sensitive data, providing tamper-evident logging. Ledger tables provide cryptographic proof of data integrity and can help meet regulatory requirements. For more information, see [Ledger](ledger-overview.md).

- **Use dynamic data masking**: Apply dynamic data masking to obfuscate sensitive data for nonprivileged users while preserving data functionality for applications. This feature helps prevent unauthorized access to sensitive information without requiring application changes. For more information, see [Dynamic Data Masking](dynamic-data-masking-overview.md).

- **Classify and label sensitive data**: Use SQL Data Discovery and Classification to identify, classify, and label sensitive data in your databases. This classification enables better protection and compliance reporting, and helps you understand where sensitive data resides. Integration with Microsoft Purview provides enhanced data governance capabilities. For more information, see [Data Discovery and Classification](data-discovery-and-classification-overview.md) and [Microsoft Purview integration](/azure/purview/register-scan-azure-sql-database).

- **Implement column-level security**: Grant permissions at the column level to restrict access to sensitive data. Only provide SELECT, UPDATE, or REFERENCES permissions to users who specifically need access to sensitive columns. For more information, see [Column-level security](/sql/relational-databases/security/encryption/encrypt-a-column-of-data).

- **Use Row-Level Security (RLS)**: Implement RLS to ensure users can only access data rows that are relevant to them. This feature provides application-level security without requiring significant application changes and is ideal for multitenant scenarios. For more information, see [Row-Level Security](/sql/relational-databases/security/row-level-security).

## Backup and recovery

Reliable backup and recovery processes protect your data from loss due to failures, disasters, or attacks while ensuring you can meet your recovery time and point objectives.

- **Verify automated backup configuration**: Ensure automated backups are properly configured and retention periods meet your business requirements. Azure SQL Database provides automated backups by default with configurable retention periods from 1 to 35 days. For more information, see [Automated backups](automated-backups-overview.md).

- **Configure backup storage redundancy**: Choose the appropriate backup storage redundancy option based on your availability and disaster recovery requirements. Options include locally redundant (LRS), zone-redundant (ZRS), geo-redundant (GRS), and geo-zone redundant (GZRS) storage. For more information, see [Backup storage redundancy](automated-backups-overview.md#backup-storage-redundancy).

- **Use long-term retention for compliance**: Configure long-term backup retention (LTR) for compliance requirements that exceed the default retention period. LTR allows you to store full backups for up to 10 years. For more information, see [Long-term retention](long-term-retention-overview.md).

- **Test backup and restore procedures**: Regularly test your backup and restore procedures to ensure they work correctly and meet your recovery time objectives (RTO). Validate that restored databases are fully functional and data integrity is maintained. For more information, see [Recover a database](recovery-using-backups.md).

- **Monitor backup activity**: Track backup operations to ensure they complete successfully. Use Azure Monitor and configure alerts for backup failures. For more information, see [Monitor and troubleshoot backup storage consumption](automated-backups-overview.md#monitor-costs).

- **Protect backups with encryption**: Backup files are automatically encrypted when TDE is enabled on the source database. Ensure TDE is enabled to protect backup data at rest. For more information, see [Transparent Data Encryption](transparent-data-encryption-tde-overview.md).

- **Implement geo-restore for disaster recovery**: Use geo-restore capability to restore a database from geo-redundant backups to any Azure region. This feature provides protection against regional outages and disasters. For more information, see [Geo-restore](recovery-using-backups.md#geo-restore).

## Monitoring and threat detection

Comprehensive monitoring and threat detection help you identify security issues, detect anomalous activities, and respond quickly to potential threats against your Azure SQL Database.

- **Enable Microsoft Defender for SQL**: Configure Microsoft Defender for SQL to detect unusual and potentially harmful attempts to access or exploit your databases. This feature provides advanced threat protection capabilities, including vulnerability assessment and threat detection. For more information, see [Microsoft Defender for SQL](azure-defender-for-sql.md).

- **Integrate with Microsoft Defender for Cloud**: Use Microsoft Defender for Cloud for centralized security management and threat protection across your Azure resources. This feature provides security recommendations, regulatory compliance tracking, and integrated threat protection. For more information, see [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-introduction).

- **Configure advanced threat protection alerts**: Set up alerts for suspicious database activities such as SQL injection attacks, anomalous database access patterns, and brute force authentication attempts. Configure notification recipients and integrate with Azure Security Center. For more information, see [Advanced Threat Protection](threat-detection-overview.md).

- **Enable auditing**: Configure auditing to track database events and write them to an audit log in Azure Storage, Log Analytics workspace, or Event Hubs. Audit both server-level and database-level events for comprehensive coverage. For more information, see [Auditing for Azure SQL Database](auditing-overview.md).

- **Use Azure Monitor for database monitoring**: Integrate with Azure Monitor to collect and analyze metrics and logs. Create custom dashboards and alerts for security-related events. For more information, see [Monitoring Azure SQL Database](monitoring-sql-database-azure-monitor.md).

- **Review audit logs regularly**: Establish a process for regularly reviewing audit logs to identify suspicious activities or unauthorized access attempts. Focus on privileged account activities, failed authentication attempts, and access to sensitive data. For more information, see [Auditing for Azure SQL Database](auditing-overview.md).

- **Enable diagnostic logs**: Configure diagnostic settings to stream logs to Azure Monitor Logs, Event Hubs, or Azure Storage for long-term retention and analysis. Include security-relevant log categories such as SQLSecurityAuditEvents. For more information, see [Diagnostic settings](monitoring-sql-database-azure-monitor.md).

## Security assessment and threat mitigation

Regularly assessing your Azure SQL Database environment helps you identify vulnerabilities and improve your security posture while ensuring compliance with security standards.

- **Run vulnerability assessments**: Use SQL Vulnerability Assessment in the Azure portal to discover and fix potential database vulnerabilities. Schedule regular scans and track remediation progress. For more information, see [SQL Vulnerability Assessment](sql-vulnerability-assessment.md).

- **Classify and label sensitive data**: Use SQL Data Discovery and Classification to identify and label sensitive data for better protection and compliance. Other security features like auditing and dynamic data masking can use classification metadata. For more information, see [Data Discovery and Classification](data-discovery-and-classification-overview.md).

- **Review security recommendations**: Regularly review security recommendations in Azure Security Center and Microsoft Defender for SQL. Prioritize and fix findings based on severity and potential impact. For more information, see [Microsoft Defender for SQL](azure-defender-for-sql.md).

- **Implement database-level encryption**: Beyond TDE, consider using Always Encrypted for highly sensitive columns, and implement proper key management practices. Use Azure Key Vault for centralized key management. For more information, see [Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-database-engine).

- **Protect against SQL injection**: Implement parameterized queries and stored procedures in your applications to prevent SQL injection attacks. Never concatenate user input into SQL statements. For more information, see [SQL injection](/sql/relational-databases/security/sql-injection).

- **Review access permissions regularly**: Conduct regular access reviews to ensure users have appropriate permissions. Remove unnecessary privileges and deactivate accounts for users who no longer require access. For more information, see [Getting started with database engine permissions](/sql/relational-databases/security/authentication-access/getting-started-with-database-engine-permissions).

- **Enable database ledger for critical data**: For databases containing audit-critical or tamper-sensitive data, enable ledger to provide cryptographic proof of data integrity. For more information, see [Ledger](ledger-overview.md).

- **Implement defense-in-depth security**: Use multiple security capabilities targeted at different security scopes to provide comprehensive protection against various threats. Combine network security, identity management, data protection, and monitoring for maximum security. For more information, see [Security best practices](security-best-practice.md).

- **Follow the Microsoft Cloud Security Benchmark**: Implement security controls based on the Microsoft Cloud Security Benchmark for Azure SQL Database. This benchmark provides a comprehensive security baseline aligned with industry standards. For more information, see [Security baseline for Azure SQL Database](/security/benchmark/azure/baselines/sql-database-security-baseline).

## Related content

- [Security overview](security-overview.md)
- [SQL Database security checklist](security-best-practice.md)
- [Azure SQL Database security best practices playbook](https://aka.ms/sqlsecuritybestpractices)
- [Microsoft Cloud Security Benchmark](/security/benchmark/azure/baselines/sql-database-security-baseline)
