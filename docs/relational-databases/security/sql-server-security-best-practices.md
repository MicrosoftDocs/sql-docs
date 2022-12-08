---
title: "SQL Server security best practices"
description: This topic provides general guidance for securing SQL Server running in an Azure virtual machine.
ms.custom: ""
ms.date: "03/04/2022"
ms.service: sql
ms.reviewer: "rohitna"
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "logins [SQL Server], passwords"
  - "passwords [SQL Server], strong"
  - "symbols [SQL Server]"
  - "security [SQL Server], passwords"
  - "passwords [SQL Server], symbols"
  - "characters [SQL Server], password policies"
  - "strong passwords [SQL Server]"
ms.assetid: 338548f4-c4d8-47ca-b597-5c9c0f2fa205
author: dplessMSFT
ms.author: dpless
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server security best practices
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides information about best practices and guidelines that help establish security for SQL Server. For a comprehensive review of SQL Server security features, see [Securing SQL Server](securing-sql-server.md). 

For specific product security best practices, see [Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/security-best-practice) and [SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/security-considerations-best-practices). 


## Overview

A layered security methodology provides a defense-in-depth solution by leveraging multiple security capabilities targeted at different security scopes. The security features made available in SQL Server 2016, and improved in subsequent releases, help counter security threats and provide well-secured database applications. 

Azure complies with several industry regulations and standards that can enable you to build a compliant solution with SQL Server running in a virtual machine. For information about regulatory compliance with Azure, see [Azure Trust Center](https://azure.microsoft.com/support/trust-center/).

## Column-level protection

Organizations often need to protect data at the column level as data regarding customers, employees, trade secrets, product data, healthcare, financial, and other sensitive data is often stored in SQL Server databases. Sensitive columns often include national identification/social security numbers, mobile phone numbers, first name, family name, financial account identification, and any other data that could be deemed personally identifiable information (PII). 

The methods and features mentioned in this section raise the level of protection at the column level with minimal overhead, and without requiring extensive changes to application code. 

Use [Always Encrypted](encryption/always-encrypted-database-engine.md) to encrypt data at rest and over the wire. Encrypted data is only decrypted by client libraries at the application client level. Use [randomized encryption over deterministic](encryption/always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption) where possible. [Always Encrypted (with enclaves)](encryption/always-encrypted-enclaves.md) can improve performance for comparison operations such as [BETWEEN, IN, LIKE, DISTINCT, Joins, and more](encryption/always-encrypted-enclaves.md#confidential-queries) for randomized encryption scenarios.
 
Use [Dynamic Data Masking (DDM)](dynamic-data-masking.md#creating-a-dynamic-data-mask) to obfuscate data at the column level when Always Encrypted is not an available option. Dynamic Data Masking (DDM) is [not compatible with Always Encrypted](dynamic-data-masking.md#limitations-and-restrictions). Leverage Always Encrypted over dynamic data masking whenever possible.

You can also [GRANT permissions](../../t-sql/statements/grant-object-permissions-transact-sql.md) at the column level to a table, view, or table-valued function. Consider the following: 
    - Only SELECT, REFERENCES, and UPDATE permissions can be granted on a column.  
    - A table-level DENY does not take precedence over a column-level GRANT.  

## Row-level protection

[Row-Level Security (RLS)](row-level-security.md#Typical) enables the ability to leverage user execution context in order to control access to rows in a database table. RLS ensures that users can only see the record that pertains to them. This gives your application 'record level' security without having to make significant changes to your application. 

The business logic is encapsulated within table-valued functions controlled by a security policy that toggles the RLS functionality on and off. The security policy also controls the FILTER and BLOCK predicates that are bound to the tables RLS operates against. Use Row-Level Security (RLS) to limit the records that are returned to the user making the call. Leverage [SESSION_CONTEXT (T-SQL)](row-level-security.md#MidTier) for users who connect to the database through a middle-tier application where application users share the same SQL Server user account. For optimal performance and manageability, follow the [Row-Level Security best practices](row-level-security.md#Best).

> [!TIP]
> Use Row-Level Security (RLS) together with either Always Encrypted or Dynamic Data Masking (DDM) to maximize the security posture of your organization. 


## File encryption

[Transparent Data Encryption (TDE)](encryption/transparent-data-encryption.md#enable-tde) protects the data at the file level by providing encryption-at-rest to the database files. Transparent Data Encryption (TDE) ensures that database files, backup files, and tempdb files can't be attached and read without proper certificates decrypting database files. Without Transparent Data Encryption (TDE), it is possible for an attacker to take the physical media (drives or backup tapes) and restore or attach the database to read the contents. Transparent Data Encryption (TDE) is supported to work with all other security capabilities in SQL Server. Transparent Data Encryption (TDE) provides real-time I/O encryption and decryption of the data and log files. TDE encryption leverages a database encryption key (DEK) is stored in the user database. The database encryption key can also be protected using a certificate, which is protected by the database master key of the master database. 

Use TDE to protect data at rest, backups, and tempdb. 


## Auditing and reporting

To [audit SQL Server](auditing/sql-server-audit-database-engine.md), create an audit policy at either the server or database level. Server policies apply to all existing and newly created databases on the server. For simplicity, enable server-level auditing and allow the database-level auditing to inherit the server-level property for all databases.

Audit [tables and columns](auditing/sql-server-audit-database-engine.md) with sensitive data that have security measures applied to them. If a table or column is important enough to need protection by a security capability, then it should be considered important enough to audit. It is especially important to audit and regularly review tables that contain sensitive information but where it is not possible to apply desired security measures due to some kind of application or architectural limitation. 


## Identities and authentication

SQL Server supports two [authentication modes](choose-an-authentication-mode.md), Windows authentication mode and 'SQL Server and Windows Authentication mode' (mixed mode).

Logins are separate from database users. First, map logins or Windows groups to database users or roles separately. Next, grant permissions to users, [server roles](authentication-access/server-level-roles.md), and/or [database roles](authentication-access/database-level-roles.md) to access database objects.

SQL Server supports the following types of logins:
- A local Windows user account or Active Directory domain account - SQL Server relies on Windows to authenticate the Windows user accounts.
- Windows group - Granting access to a Windows group grants access to all Windows user logins that are members of the group. Removing a user from a group removes the rights from the user that came from the group. Group membership is the preferred strategy.
- SQL Server login - SQL Server stores the username and a hash of the password in the master database.
- [Contained database users](contained-database-users-making-your-database-portable.md) authenticate SQL Server connections at the database level. A contained database is a database that is isolated from other databases and from the instance of SQL Server (and the master database) that hosts the database. SQL Server supports contained database users for both Windows and SQL Server authentication.

The following recommendations and best practices help secure your identities and authentication methods: 

- Use [least-privilege role-based security](authentication-access/getting-started-with-database-engine-permissions.md#grant-the-least-permission) strategies to improve security management.
    - It's standard to place Active Directory users in AD groups, AD groups should exist in SQL Server roles, and SQL Server roles should be granted the minimum permissions required by the application.
- In Azure, leverage least-privilege security by using role-based access (RBAC) controls
- Choose Active Directory over SQL Server authentication whenever possible, and especially choose Active Directory over storing the security at the application or database level.
    - If a user leaves the company it is easy to disable the account. 
    - It is also easy to remove users from groups when users change roles or leave the organization. Group security is considered a best practice. 
- Leverage [Multi-Factor Authentication](/azure/active-directory/authentication/concept-mfa-howitworks) for accounts that have machine-level access, including accounts that use RDP to log into the machine. This helps guard against credential theft or leaks,  as single-factor password-based authentication is a weaker form of authentication with credentials at risk of being compromised or mistakenly given away. 
- Require [strong and complex passwords](strong-passwords.md) that cannot be easily guessed, and are not used for any other accounts or purposes. Regularly update passwords and enforce Active Directory policies. 

- [Group-Managed Service Accounts (gMSA)](/windows-server/security/group-managed-service-accounts/group-managed-service-accounts-overview) provide automatic password management, simplified service principal name (SPN) management and delegate the management to other administrators. 
    - With gMSA, the Windows operating system manages passwords for the account instead of relying on the administrator to manage the password.
    - gMSA automatically updates the account passwords without restarting services.
    - gMSA reduces the administrative surface-level and improves the separation of duties.
- Minimize the rights granted to the AD account of the DBA; Consider a separation of duties that limit access to the virtual machine, the ability to log into the operating system, the ability to modify error and auditing logs, and the ability to install applications and/or features.

- Consider removing DBA accounts from the sysadmin role and granting [CONTROL SERVER](permissions-database-engine.md#chart-of-sql-server-permissions) to DBA accounts rather than making them a member of the sysadmin role. The system admin role does not respect DENY while [CONTROL SERVER](permissions-database-engine.md#chart-of-sql-server-permissions) does.

## Data lineage and data integrity

Keeping historical records of data changes over time can be beneficial to address accidental changes to the data. It can also be useful for application-change auditing and can provide the ability to recover data elements when a bad actor has introduced data changes that were not authorized. 

- Leverage [temporal tables](../../relational-databases/tables/temporal-tables.md) to preserve record versions over time, and to see data as it has been over the record's life span to provide a historical view of your application's data.
- Temporal Tables can be used to supply a version of the current table at any point in time.

## Security assessment tools and evaluation

The configuration and assessment tools below provide an ability to address surface-area security, identify data security opportunities, and provide a best practice assessment of the security of your SQL Server environment at the instance level.

- [Surface Area Configuration](surface-area-configuration.md) - It is recommended to enable only the features that are required by your environment in order to minimize the number of features that can be attacked by a malicious user. 
- [Vulnerability assessment for SQL Server (SSMS)](sql-vulnerability-assessment.md) - SQL vulnerability assessment is a tool in [SSMS v17.4+](../../ssms/download-sql-server-management-studio-ssms.md) that helps discover, track, and remediate potential database vulnerabilities. The vulnerability assessment is a valuable tool to improve your database security and is executed at the database level, per database.
- [SQL Data Discovery and Classification (SSMS)](sql-data-discovery-and-classification.md) - It is common for DBAs to manage servers and databases and not be aware of sensitivity of the data that is contained in the database. Data Discovery & Classification adds the capability to discover, classify, label and report on the sensitivity level of your data.  Data Discovery & Classification is supported starting with [SSMS 17.5](../../ssms/download-sql-server-management-studio-ssms.md).
 

## Common SQL threats 

It helps to know what are some common threats that risk SQL Server: 

- [SQL injection](sql-injection.md) - SQL injection is a type of attack where malicious code is inserted into strings that are passed to an instance of SQL Server for execution. 
    - The injection process works by terminating a text string and appending a new command. Because the inserted command may have additional strings appended to it before it is executed, the attacker terminates the injected string with a comment mark "--".
    - SQL Server will execute any syntactically valid query that is received.
- Be aware of [Side-channel attacks](/azure/virtual-machines/mitigate-se), [malware & other threats](/windows/security/threat-protection/intelligence/understanding-malware). 

### SQL injection risks

To minimize the risk of a SQL injection, consider the following: 

   - Review any SQL process that constructs SQL statements for injection vulnerabilities.
   - Construct dynamically generated SQL statements in a parameterized manner.
   - Developers and security admins should review all code that calls EXECUTE, EXEC, or sp_executesql.
   - Disallow the following input characters
        - **;**	   Query delimiter
        - **'**	   Character data string delimiter
        - **--**	Single-line comment delimiter. 
        - **/ * ... * /**    Comment delimiters. 
        - **xp_**    Catalog-extended stored procedures, such as xp_cmdshell.
            - It is not recommended to leverage xp_cmdshell on any SQL Server environment. Use SQLCLR instead or look for other alternatives due to the risks xp_cmdshell may introduce.
   - Always [validate user inputs](sql-injection.md#validate-all-input) and scrub error outputs from being spilled and exposed to the attacker.


### Side-channel risks

To minimize the risk of a side-channel attack, consider the following: 

- Ensure the latest application and operating system patches are applied. 
- For hybrid workloads, ensure the latest firmware patches are applied for any hardware on-premises.
- In Azure, for highly sensitive applications and workloads, you can add additional protection against side-channel attacks with isolated virtual machines, dedicated hosts, or by leveraging Confidential Compute virtual machines such as the [DC-series](/azure/virtual-machines/dcv2-series) and [Virtual Machines that leverage 3rd Gen AMD EPYC processors](https://azure.microsoft.com/blog/azure-and-amd-enable-lift-and-shift-confidential-computing/).

## Infrastructure threats 

Consider the following common infrastructure threats: 

- [Brute force access](/defender-for-identity/compromised-credentials-alerts) - the attacker attempts to authenticate with multiple passwords on different accounts until a correct password is found.
- Password cracking / [password spray](/security/compass/incident-response-playbook-password-spray) - attackers try a single carefully crafted password against all of the known user accounts (one password to many accounts). If the initial password spray fails, they try again, utilizing a different carefully crafted password, normally waiting a set amount of time between attempts to avoid detection.
- [Ransomware attacks](/windows/security/threat-protection/intelligence/ransomware-malware) is a type of targeted attack where malware is used to encrypt data and files, preventing access to important content. The attackers then attempt to extort money from victims, usually in the form of cryptocurrencies, in exchange for the decryption key. Most ransomware infections start with email messages with attachments that try to install ransomware,  or websites hosting exploit kits that attempt to use vulnerabilities in web browsers and other software to install ransomware. 

### Password risks

Since you don't want attackers to easily guess account names, or passwords, the following steps help reduce the risk of passwords being discovered: 

- Create a unique local administrator account that is not named **Administrator**.
- Use complex strong passwords for all your accounts. For more information about how to create a strong password, see [Create a strong password](https://support.microsoft.com/account-billing/how-to-create-a-strong-password-for-your-microsoft-account-f67e4ddd-0dbe-cd75-cebe-0cfda3cf7386) article.
- By default, Azure selects Windows Authentication during SQL Server virtual machine setup. Therefore, the **SA** login is disabled and a password is assigned by setup. We recommend that the **SA** login should not be used or enabled. If you must have a SQL login, use one of the following strategies:
     - Create a SQL account with a unique name that has **sysadmin** membership. You can do this from the portal by enabling **SQL Authentication** during provisioning.

    > [!TIP] 
    > If you do not enable SQL Authentication during provisioning, you must manually change the authentication mode to **SQL Server and Windows Authentication Mode**. For more information, see [Change Server Authentication Mode](../../database-engine/configure-windows/change-server-authentication-mode.md).
  - If you must use the **SA** login, enable the login after provisioning and assign a new strong password.


### Ransomware risks

Consider the following to minimize ransomware risks: 

- The best strategy to guard against [ransomware](/windows/security/threat-protection/intelligence/ransomware-malware) is to pay particular attention to RDP and SSH vulnerabilities. Additionally, consider the following: 
    - Leverage firewalls and lock down ports
    - Ensuring the latest operating system and application security updates are applied
    - Use [group managed service accounts (gMSA)](/windows-server/security/group-managed-service-accounts/group-managed-service-accounts-overview)
    - Limit access to the virtual machines
    - Require  [Just-in-time (JIT) access](/azure/defender-for-cloud/just-in-time-access-usage) and [Azure Bastion](/azure//bastion/bastion-overview)
    - Improve Surface Area Security by avoiding installing tools including sysinternals and SSMS on the local machine
    - Avoid installing Windows Features, roles and enabling services that are not required
    - Additionally, there should be a regular full backup scheduled that is separately secured from a common administrator account so it can't delete copies of the databases. 

## Next steps

For a comprehensive review of SQL Server security features, see [Securing SQL Server](securing-sql-server.md). 

For specific product security best practices, see [Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/security-best-practice) and [SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/security-considerations-best-practices).