---
title: Secure your SQL Server
description: Learn how to secure SQL Server, with best practices for protecting data, manage access, and defend against common threats.
author: VanMSFT
ms.author: vanto
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom: horz-security
ms.date: 07/01/2025
ai-usage: ai-assisted
---

# Secure your SQL Server

SQL Server is a relational database management system (RDBMS) that stores and manages critical business data for applications, analytics, and reporting. Because it often contains sensitive information, such as customer records, financial data, and intellectual property, securing SQL Server is essential to protect your organization from data breaches, unauthorized access, and compliance risks.

This article provides guidance on how to best secure your SQL Server.

## Network security

Securing network access to SQL Server helps prevent unauthorized connections, reduces exposure to attacks, and ensures only trusted sources can reach your databases.

- **Restrict inbound traffic with firewalls and NSGs**: Limit network access to SQL Server by [Configuring Windows Firewall for Database Engine access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md). For SQL Server in Azure virtual machines (VMs), use [Azure Firewall](/azure/firewall/features) and [Network Security Groups (NSGs)](/azure/virtual-network/network-security-groups-overview) to enforce these restrictions.

- **Encrypt connections to SQL Server**: Configure SQL Server Database Engine to encrypt connections using a certificate. This ensures data in transit is protected from eavesdropping and tampering. For more information, see [Encrypt connections to SQL Server Database Engine](../../database-engine/configure-windows/configure-sql-server-encryption.md).

- **Secure remote management**: Use encrypted protocols (such as TLS 1.3) for all remote connections. For more information, see [Configure TLS 1.3](networking/connect-with-tls-1-3.md).

- **Connect to SQL Server with strict encryption**: SQL Server 2022 introduced the `strict` encryption option, which requires all connections to use encryption. This helps ensure that all data in transit is protected. For more information, see [Connect to SQL Server with strict encryption](networking/connect-with-strict-encryption.md).

## Identity management

Strong identity and authentication controls help ensure only authorized users and applications can access SQL Server resources.

- **Use Windows authentication or Microsoft Entra authentication**: Prefer Windows authentication or Microsoft Entra authentication over SQL authentication for centralized identity management and easier account lifecycle control. For more information, see [Choose an authentication mode](choose-an-authentication-mode.md).

- **Use group-managed service accounts (gMSA) for services**: Use gMSA to manage service account credentials automatically and securely. For more information, see [Group-Managed Service Accounts overview](/windows-server/security/group-managed-service-accounts/group-managed-service-accounts-overview).

- **Enforce strong password policies**: If you're using SQL authentication, require complex passwords that can't be easily guessed and aren't used for other accounts. Regularly update passwords and enforce Active Directory policies. For more information, see [Strong passwords](strong-passwords.md).

- **Use contained database users when appropriate**: Consider contained database users for applications that need database-level authentication without requiring server-level logins. For more information, see [Contained database users](contained-database-users-making-your-database-portable.md).

## Privileged access

Limiting and monitoring privileged access helps prevent unauthorized changes and reduces the impact of compromised accounts.

- **Grant the minimum permissions required**: Assign the lowest level of privilege needed for each user or service. Regularly review and adjust permissions to maintain least privilege. For more information, see [Getting started with database engine permissions](authentication-access/getting-started-with-database-engine-permissions.md#grant-the-least-permission).

- **Separate database administrators (DBA) and `sysadmin` roles**: Avoid granting `sysadmin` rights to all DBAs. Use the **CONTROL SERVER** permission when possible, as it respects `DENY` permissions and allows for more granular control. Consider a separation of duties that limit access to the virtual machine, the ability to log into the operating system, the ability to modify error and auditing logs, and the ability to install applications and/or features. For more information, see [Permissions (Database Engine)](permissions-database-engine.md#chart-of-sql-server-permissions).

- **Monitor and audit privileged activities**: Enable auditing to track changes made by privileged accounts and review logs regularly for suspicious activity. For more information, see [SQL Server Audit (Database Engine)](auditing/sql-server-audit-database-engine.md).

- **Implement role separation**: Place Active Directory users in AD groups, map AD groups to SQL Server roles, and grant SQL Server roles the minimum permissions required by applications. For more information, see [Getting started with database engine permissions](authentication-access/getting-started-with-database-engine-permissions.md#grant-the-least-permission).

## Data protection

Protecting data at rest and in transit is critical to prevent unauthorized disclosure or tampering.

- **Encrypt sensitive information with Always Encrypted**: Use Always Encrypted and Always Encrypted with secure enclaves to protect sensitive data in SQL Server. Prefer randomized encryption for stronger security. For more information, see [Always Encrypted](encryption/always-encrypted-database-engine.md).

- **Use Transparent Data Encryption (TDE) for database files**: Enable TDE to encrypt database, backup, and tempdb files, protecting data if physical media is compromised. For more information, see [Transparent Data Encryption (TDE)](encryption/transparent-data-encryption.md).

- **Mask sensitive data with Dynamic Data Masking (DDM)**: Use DDM to obfuscate sensitive data in query results when encryption isn't possible. For more information, see [Dynamic Data Masking](dynamic-data-masking.md#creating-a-dynamic-data-mask).

- **Grant column-level permissions**: Limit access to sensitive columns by granting `SELECT`, `REFERENCES`, or `UPDATE` permissions only to authorized users. For more information, see [GRANT permissions](../../t-sql/statements/grant-object-permissions-transact-sql.md).

- **Use Row-Level Security (RLS) to restrict data access**: Implement RLS to ensure users only see data relevant to them. Use `SESSION_CONTEXT` for middle-tier applications where users share SQL accounts. For more information, see [Row-Level Security](row-level-security.md#Typical).

- **Combine security features for maximum protection**: Use Row-Level Security together with Always Encrypted or Dynamic Data Masking to maximize your organization's security posture. For more information, see [Row-Level Security best practices](row-level-security.md#Best).

## Logging and threat detection

Comprehensive logging and monitoring help detect threats, investigate incidents, and meet compliance requirements.

- **Enable and configure SQL Server Audit**: Audit access and changes to sensitive data and configurations at both the server and database level. Consider auditing tables and columns with sensitive data that have security measures applied to them. Regularly review audit logs, especially for tables containing sensitive information where complete security measures aren't possible. For more information, see [SQL Server Audit (Database Engine)](auditing/sql-server-audit-database-engine.md).

- **Use ledger in SQL Server**: Enable ledger to create an immutable record of changes to sensitive data, providing tamper-evident logging. For more information, see [Configure a ledger database](ledger/ledger-how-to-configure-ledger-database.md).

## Backup and recovery

Reliable backup and recovery processes protect your data from loss due to failures, disasters, or attacks.

- **Automate regular backups**: Schedule automated backups for databases, system configurations, and transaction logs. Use Azure Backup or SQL Server native tools. For more information, see [Backup and restore a SQL Server database with SSMS](../backup-restore/quickstart-backup-restore-database.md) and [Backup and restore for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/backup-restore).

- **Encrypt backup data**: Protect backup files with [Transparent Data Encryption (TDE)](encryption/transparent-data-encryption.md).

- **Test recovery procedures regularly**: Periodically restore backups to validate your recovery process and ensure you can meet recovery time and point objectives. For more information, see [Restore files from VM backup](/azure/backup/backup-azure-restore-files-from-vm).

## Security assessment and threat mitigation

Regularly assessing your SQL Server environment helps identify vulnerabilities and improve your security posture.

- **Limit enabled features to reduce attack surface**: Enable only the SQL Server features required for your environment. For more information, see [Surface area configuration](surface-area-configuration.md).

- **Run vulnerability assessments**: Use SQL vulnerability assessment, delivered through Microsoft Defender for SQL, to discover and remediate potential database vulnerabilities across cloud and on-premises resources. For more information, see [Vulnerability assessment for SQL Server](sql-vulnerability-assessment.md).

- **Classify and label sensitive data**: Use SQL Data Discovery and Classification to identify and label sensitive data for better protection and compliance. For more information, see [SQL Data Discovery and Classification](sql-data-discovery-and-classification.md).

- **Review and mitigate common threats**: Protect against SQL injection, side-channel attacks, brute force, password spray, and ransomware by following recommended practices for input validation, patching, and access control. For more information, see [SQL injection](sql-injection.md) and [Ransomware attacks](/security/ransomware/human-operated-ransomware).

- **Implement defense-in-depth security**: Use multiple security capabilities targeted at different security scopes to provide comprehensive protection against various threats. For more information, see [SQL Server security best practices](sql-server-security-best-practices.md).

## Related content

- [Securing SQL Server](securing-sql-server.md)
- [Security considerations for SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/security-considerations-best-practices)
