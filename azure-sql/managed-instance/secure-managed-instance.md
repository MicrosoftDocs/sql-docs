---
title: Secure your Azure SQL Managed Instance
description: Learn how to secure Azure SQL Managed Instance with best practices for protecting data, managing access, and defending against common threats.
author: VanMSFT
ms.author: vanto
ms.service: azure-sql-managed-instance
ms.subservice: security
ms.topic: concept-article
ms.custom: horz-security
ms.date: 05/12/2026
ai-usage: ai-assisted
---

# Secure your Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

Azure SQL Managed Instance is a fully managed platform as a service (PaaS) database engine that provides near 100% compatibility with the latest SQL Server Database Engine. It combines the best of SQL Server with the operational benefits of a fully managed service. Because it often stores critical business data including customer records, financial information, and intellectual property, securing your SQL Managed Instance is essential to protect against data breaches, unauthorized access, and compliance violations.

This article provides guidance on how to best secure your Azure SQL Managed Instance deployment.

## Network security

Network security for SQL Managed Instance helps prevent unauthorized connections, reduces exposure to attacks, and ensures only trusted sources can reach your databases through proper network isolation and access controls.

- **Deploy in a dedicated virtual network**: Place your managed instance in a dedicated subnet within your virtual network to provide network isolation and control traffic flow. This ensures your database is isolated from other resources and protected by network boundaries. For more information, see [Virtual network requirements](connectivity-architecture-overview.md#network-requirements).

- **Configure Network Security Groups (NSGs)**: Apply NSGs to your SQL managed instance subnet to control inbound and outbound traffic. Restrict access to only required ports and sources to minimize attack surface. For more information, see [Network Security Groups](connectivity-architecture-overview.md#network-requirements).

- **Use private endpoints when possible**: Connect to your SQL managed instance over private IP addresses to avoid exposing your database to the public internet. Private connectivity reduces the risk of external attacks and data exfiltration. For more information, see [Private endpoint connectivity](connectivity-architecture-overview.md#private-endpoints).

- **Disable public endpoints by default**: Only enable public endpoints if absolutely necessary for your architecture. When enabled, use strict firewall rules to limit access to authorized IP addresses only. For more information, see [Public endpoint overview](public-endpoint-overview.md).

- **Implement ExpressRoute or VPN for hybrid connectivity**: Use Azure ExpressRoute or site-to-site VPN for secure, private connections between your on-premises network and Azure. This ensures data doesn't traverse the public internet. For more information, see [Connectivity architecture](connectivity-architecture-overview.md).

- **Enable connection encryption**: Configure all client connections to use encryption in transit. SQL Managed Instance supports TLS 1.2 by default, ensuring data is protected while moving between clients and the database. TLS 1.3 is also available for SQL Managed Instance. For more information, see [Connection security](connectivity-architecture-overview.md#networking-constraints).

## Identity management

Strong identity and authentication controls ensure only authorized users and applications can access your SQL Managed Instance resources while providing centralized identity management and easier account lifecycle control.

- **Configure a Microsoft Entra admin**: Designate a Microsoft Entra administrator for your SQL managed instance to enable centralized identity management and advanced security features. This admin can manage access and authentication policies. For more information, see [Microsoft Entra admin](../database/authentication-aad-configure.md#provision-azure-ad-admin-sql-managed-instance).

- **Use Microsoft Entra authentication**: Prefer Microsoft Entra authentication over SQL authentication for centralized identity management and easier account lifecycle control. Microsoft Entra ID provides superior security and enables advanced features like conditional access. For more information, see [Configure Microsoft Entra authentication](../database/authentication-aad-configure.md).

- **Create contained database users**: Use contained database users that map to Microsoft Entra groups instead of server-level logins when possible. This simplifies permission management and improves security by eliminating the need for server-level access. For more information, see [Make your database portable by using contained databases](/sql/relational-databases/security/contained-database-users-making-your-database-portable).

- **Enable multifactor authentication**: Require multifactor authentication for administrative accounts and privileged users to add an extra layer of security beyond passwords. For more information, see [Multifactor authentication (MFA)](../database/authentication-aad-overview.md#multifactor-authentication-mfa).

- **Use managed identities for applications**: Enable managed identities for Azure resources to allow applications to authenticate without storing credentials. This eliminates the need to manage connection strings with embedded passwords. For more information, see [Managed identities for Azure resources](/azure/active-directory/managed-identities-azure-resources/overview).

- **Enforce strong password policies**: If using SQL authentication, require complex passwords that can't be easily guessed. Implement password rotation policies and avoid reusing passwords across different accounts. For more information, see [Password policy](frequently-asked-questions-faq.yml#password-policy).

## Privileged access

Controlling privileged access prevents unauthorized changes, reduces the impact of compromised accounts, and ensures administrative actions are properly monitored and controlled.

- **Implement least privilege access**: Grant users only the minimum permissions required to perform their job functions. Regularly review and adjust permissions to maintain the principle of least privilege. For more information, see [Getting started with database engine permissions](/sql/relational-databases/security/authentication-access/getting-started-with-database-engine-permissions).

- **Separate administrative roles**: Avoid granting admin rights to all database administrators. Use more granular permissions like CONTROL SERVER when possible, and implement separation of duties between different administrative functions. For more information, see [Permissions](/sql/relational-databases/security/permissions-database-engine).

- **Use Azure role-based access control (RBAC)**: Implement Azure RBAC to control access to SQL managed instance management operations. Create custom roles that provide only the necessary permissions for specific administrative tasks. For more information, see [Azure RBAC for SQL Managed Instance](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor).

- **Monitor privileged activities**: Enable auditing to track all actions performed by privileged accounts. Regularly review audit logs for suspicious activities or unauthorized changes. For more information, see [SQL Server Audit in Azure SQL Managed Instance](auditing.md).

- **Use server roles for database-level access**: Use built-in server roles and create custom roles to implement role-based security. Assign users to roles rather than granting individual permissions to simplify management and reduce errors. For more information, see [Server-level roles](/sql/relational-databases/security/authentication-access/server-level-roles).

## Data protection

Data protection safeguards your information through encryption, access controls, and data classification to prevent unauthorized disclosure, tampering, or loss of sensitive information.

- **Enable Transparent Data Encryption (TDE)**: Use TDE to encrypt your database, log, and backup files at rest. Consider using customer-managed keys in Azure Key Vault for additional control over encryption keys. For more information, see [Transparent Data Encryption](../database/transparent-data-encryption-tde-overview.md).

- **Implement Always Encrypted for sensitive data**: Use Always Encrypted to protect highly sensitive data in use, at rest, and in transit. This ensures that even database administrators can't view sensitive data in plaintext. For more information, see [Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-database-engine).

- **Use ledger in SQL Managed Instance**: Enable ledger to create an immutable record of changes to sensitive data, providing tamper-evident logging. For more information, see [Ledger overview](/sql/relational-databases/security/ledger/ledger-overview).

- **Use Dynamic Data Masking**: Apply dynamic data masking to obfuscate sensitive data for nonprivileged users while preserving data functionality for applications. This helps prevent unauthorized access to sensitive information. For more information, see [Dynamic Data Masking](../database/dynamic-data-masking-overview.md).

- **Classify and label sensitive data**: Use SQL Data Discovery and Classification to identify, classify, and label sensitive data in your databases. This enables better protection and compliance reporting. For more information, see [Data Discovery and Classification](../database/data-discovery-and-classification-overview.md).

- **Implement column-level security**: Grant permissions at the column level to restrict access to sensitive data. Only provide SELECT, UPDATE, or REFERENCES permissions to users who specifically need access to sensitive columns. For more information, see [Encrypt a column of data](/sql/relational-databases/security/encryption/encrypt-a-column-of-data).

- **Use Row-Level Security (RLS)**: Implement RLS to ensure users can only access data rows that are relevant to them. This provides application-level security without requiring significant application changes. For more information, see [Row-Level Security](/sql/relational-databases/security/row-level-security).

## Backup and recovery

Reliable backup and recovery processes protect your data from loss due to failures, disasters, or attacks while ensuring you can meet your recovery time and point objectives.

- **Verify automated backup configuration**: Ensure automated backups are properly configured and retention periods meet your business requirements. Azure SQL Managed Instance provides automated backups by default with configurable retention periods up to 35 days. Backup storage is independent from instance storage and isn't limited in size. For more information, see [Automated backups](automated-backups-overview.md).

- **Monitor backup activity**: Track when automated backups are performed on your SQL managed instance to ensure backup operations are completing successfully. For more information, see [Monitor backup activity](backup-activity-monitor.md).

- **Use geo-redundant backup storage**: Configure geo-redundant storage for backups to protect against regional disasters. This ensures your data can be recovered even if your primary region becomes unavailable. For more information, see [Backup storage redundancy](automated-backups-overview.md#backup-storage-redundancy).

- **Test backup and restore procedures**: Regularly test your backup and restore procedures to ensure they work correctly and meet your recovery time objectives. Validate that restored databases are fully functional and data integrity is maintained. For more information, see [Point-in-time restore](recovery-using-backups.md#point-in-time-restore).

- **Use native backup and restore capabilities**: Take advantage of native backup and restore from Azure Blob Storage for migration scenarios. You can create copy-only full backups and restore from .bak files (SQL Server 2005+). Copy-only backup isn't possible if the database is encrypted by service-managed TDE. For more information, see [Native restore from URL](/data-migration/sql-server/managed-instance/guide).

- **Implement long-term retention policies**: Configure long-term backup retention for compliance requirements that exceed the default retention period. This ensures you can meet regulatory requirements for data retention. For more information, see [Long-term backup retention](automated-backups-overview.md#long-term-retention-ltr).

## Monitoring and threat detection

Comprehensive monitoring and threat detection help you identify security issues, detect anomalous activities, and respond quickly to potential threats against your SQL managed instance.

- **Enable Microsoft Defender for SQL**: Configure Microsoft Defender for SQL to detect unusual and potentially harmful attempts to access or exploit your databases. This provides advanced threat protection capabilities including vulnerability assessment and threat detection. For more information, see [Microsoft Defender for SQL](../database/azure-defender-for-sql.md).

- **Configure Advanced Threat Protection**: Set up Advanced Threat Protection to detect specific threats including potential SQL injection, access from unusual locations or data centers, access from unfamiliar principals, and brute force SQL credentials. Configure email notifications and storage accounts for threat alerts. For more information, see [Configure Advanced Threat Protection](threat-detection-configure.md).

- **Configure SQL auditing**: Enable comprehensive auditing to track database events and write them to Azure Storage, Log Analytics, or Event Hubs. This provides detailed logs for security analysis and compliance reporting. For more information, see [SQL Server Audit in Azure SQL Managed Instance](auditing.md).

- **Set up Azure Monitor**: Use Azure Monitor to collect platform metrics, diagnostic logs, and create custom alerts for your SQL managed instance. Monitor resource consumption, performance metrics, and security events in a centralized location. For more information, see [Monitor Azure SQL Managed Instance](monitoring-sql-managed-instance-azure-monitor.md).

- **Create metric alerts**: Set up alerts for suspicious activities, failed login attempts, unusual database access patterns, and resource consumption thresholds. Alerting metrics are available for the SQL managed instance level, not individual databases. For more information, see [Create alerts for SQL Managed Instance](alerts-create.md).

- **Use SQL Vulnerability Assessment**: Run regular vulnerability assessments to identify security misconfigurations and potential vulnerabilities in your database. Remediate identified issues promptly to maintain a strong security posture. For more information, see [SQL Vulnerability Assessment](../database/sql-vulnerability-assessment.md).

- **Monitor with Dynamic Management Views (DMVs)**: Use DMVs to monitor performance, detect blocked queries, resource bottlenecks, and security-related activities. DMVs provide detailed insights into database engine operations and security events. For more information, see [Monitor Azure SQL Managed Instance performance using dynamic management views](monitoring-with-dmvs.md).

- **Implement Query Store monitoring**: Enable Query Store to track query performance over time, identify performance regressions, and monitor query execution patterns. This helps detect unusual query behavior that might indicate security issues. For more information, see [Monitor performance by using the Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store).

- **Use Extended Events for detailed monitoring**: Implement Extended Events (XEvents) for low-overhead, detailed monitoring of database activities, including security events and performance issues. XEvents provide more granular monitoring than SQL Profiler with less performance impact. For more information, see [Extended Events](../database/xevent-db-diff-from-svr.md?view=azuresql-mi&preserve-view=true).

- **Set up Database watcher (preview)**: Consider using Database watcher for in-depth workload monitoring with centralized dashboards and detailed performance insights across your Azure SQL estate. For more information, see [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md).

## Compliance and governance

Maintaining compliance and governance ensures your SQL Managed Instance deployment meets regulatory requirements and organizational security policies through proper controls and documentation.

- **Apply Azure Policy for governance**: Use Azure Policy to enforce organizational security standards and ensure consistent configuration across all SQL managed instances. Create custom policies for specific compliance requirements. Consider policies for enforcing Microsoft Entra-only authentication, backup storage redundancy, and data residency requirements. For more information, see [Azure Policy for SQL Managed Instance](/azure/governance/policy/samples/built-in-policies#sql).

- **Enforce Microsoft Entra-only authentication**: Use Azure Policy to require Microsoft Entra-only authentication for new SQL managed instances, ensuring SQL authentication is disabled for enhanced security. This provides centralized identity management and eliminates password-based authentication vulnerabilities. For more information, see [Using Azure Policy to enforce Microsoft Entra-only authentication](../database/authentication-azure-ad-only-authentication-policy-how-to.md).

- **Implement resource tagging strategy**: Apply consistent Azure tags to identify resource ownership, environment classification, cost centers, and compliance requirements. Use tags for automated governance, cost tracking, and resource management. For more information, see [Develop your naming and tagging strategy for Azure resources](/azure/cloud-adoption-framework/ready/azure-best-practices/naming-and-tagging).

- **Leverage Microsoft Purview for data governance**: Integrate with Microsoft Purview for data classification, lineage tracking, and unified data governance across your data estate. Use sensitivity labels and data loss prevention policies to protect classified data. For more information, see [Microsoft Purview Information Protection](/microsoft-365/compliance/sensitivity-labels).

- **Document security procedures**: Maintain comprehensive documentation of your security procedures, incident response plans, and access control policies. Regularly review and update documentation to reflect current practices and compliance requirements. For more information, see [Security best practices](../database/security-best-practice.md).

- **Conduct regular security assessments**: Perform periodic security assessments to evaluate your security posture and identify areas for improvement. Include penetration testing and vulnerability assessments in your security program. Document remediation efforts for compliance reporting. For more information, see [SQL vulnerability assessment](/azure/defender-for-cloud/sql-azure-vulnerability-assessment-overview).

- **Maintain audit trails**: Ensure all administrative actions and data access are properly logged and audit trails are maintained for the required retention period. Protect audit logs from tampering or unauthorized access. Configure audit log retention based on regulatory requirements (SOX, PCI DSS). For more information, see [Auditing overview](../database/auditing-overview.md).

- **Meet regulatory compliance standards**: Azure SQL Managed Instance supports compliance with various regulatory frameworks including ISO 27001, PCI DSS, FedRAMP, and SOX. Review and implement controls specific to your industry requirements. For more information, see [Azure compliance documentation](/azure/compliance/).

- **Use resource locks**: Apply resource locks to prevent accidental deletion or modification of critical SQL managed instance resources. This helps maintain configuration integrity and prevents unauthorized changes that could affect compliance. For more information, see [Resource locks](/azure/azure-resource-manager/management/lock-resources).

- **Monitor with Azure Advisor**: Use Azure Advisor to receive personalized recommendations for security, cost optimization, performance, and operational excellence. Regularly review and implement advisor recommendations to maintain best practices. For more information, see [Azure Advisor](/azure/advisor/advisor-overview).

## Automatic internal connectivity tests

[!INCLUDE [auto-connectivity-tests](../includes/auto-connectivity-tests.md)]

## Related content

For comprehensive security guidance, see [Azure SQL Managed Instance security best practices](../database/security-best-practice.md) and [Overview of Azure SQL Managed Instance security capabilities](../database/security-overview.md).
