---
title: "Security: SQL Server on Azure VMs best practices"
description: This topic provides general guidance for securing SQL Server running in an Azure virtual machine.
services: virtual-machines-windows
documentationcenter: na
author: dplessMSFT
editor: ''
tags: azure-service-management

ms.assetid: d710c296-e490-43e7-8ca9-8932586b71da
ms.service: virtual-machines-sql
ms.subservice: security

ms.topic: conceptual
ms.tgt_pltfrm: vm-windows-sql-server
ms.workload: iaas-sql-server
ms.date: 06/30/2021
ms.author: dpless
ms.reviewer: mathoma
---

# SQL Server on Azure VMs best practices: Security considerations 
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides information about guidelines that help establish security for SQL Server instances hosted on Azure VMs, covering SQL Server, Azure, and SQL Server on Azure VM security topics.

To learn more, see the other articles in this series: [Checklist](performance-guidelines-best-practices-checklist.md), [VM size](performance-guidelines-best-practices-vm-size.md), [Storage](performance-guidelines-best-practices-storage.md), [HADR configuration](hadr-cluster-best-practices.md), [Collect baseline](performance-guidelines-best-practices-collect-baseline.md).

## Overview

A layered security methodology provides a defense-in-depth solution by leveraging multiple security capabilities targeted at different security scopes. The security features made available in SQL Server 2016, and improved in subsequent releases, help counter security threats and provide well-secured database applications. 

Azure complies with several industry regulations and standards that can enable you to build a compliant solution with SQL Server running in a virtual machine. For information about regulatory compliance with Azure, see [Azure Trust Center](https://azure.microsoft.com/support/trust-center/).


## Column-level Protection

Organizations often need to protect data at the column level as data regarding customers, employees, trade secrets, product data, healthcare, financial, and other sensitive data is often stored in SQL Server databases. Sensitive columns often include national identification/social security numbers, mobile phone numbers, first name, family name, financial account identification, and any other data that could be deemed personally identifiable information (PII). 


The methods and features mentioned in this section raise the level of protection at the column level with minimal overhead, and without requiring extensive changes to application code. 

Use [Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-database-engine)to encrypt data at rest and over the wire. Encrypted data is only decrypted by client libraries at the application client level. Use [randomized encryption over deterministic](/sql/relational-databases/security/encryption/always-encrypted-database-engine#selecting--deterministic-or-randomized-encryption) where possible. Store keys, secrets, and certificates in an [Azure Key Vault](azure-key-vault-integration-configure.md). [Always Encrypted (with enclaves)](/sql/relational-databases/security/encryption/always-encrypted-enclaves) can improve performance for comparison operations such as [BETWEEN, IN, LIKE, DISTINCT, Joins, and more](/sql/relational-databases/security/encryption/always-encrypted-enclaves#confidential-queries) for randomized encryption scenarios.
 
Use [Dynamic Data Masking (DDM)](/sql/relational-databases/security/dynamic-data-masking#creating-a-dynamic-data-mask) to obfuscate data at the column level when Always Encrypted is not an available option. Dynamic Data Masking (DDM) is [not compatible with Always Encrypted](/sql/relational-databases/security/dynamic-data-masking#limitations-and-restrictions). Leverage Always Encrypted over dynamic data masking whenever possible.

You can also [GRANT permissions](/sql/t-sql/statements/grant-object-permissions-transact-sql) at the column level to a table, view, or table-valued function. Consider the following: 
    - Only SELECT, REFERENCES, and UPDATE permissions can be granted on a column.  
    - A table-level DENY does not take precedence over a column-level GRANT.  

## Row-level protection

[Row-Level Security (RLS)](/sql/relational-databases/security/row-level-security#Typical) enables the ability to leverage user execution context in order to control access to rows in a database table. RLS ensures that users can only see the record that pertains to them. This gives your application 'record level' security without having to make significant changes to your application. 

The business logic is encapsulated within table-valued functions controlled by a security policy that toggles the RLS functionality on and off. The security policy also controls the FILTER and BLOCK predicates that are bound to the tables RLS operates against. Use Row-Level Security (RLS) to limit the records that are returned to the user making the call. Leverage [SESSION_CONTEXT (T-SQL)](/sql/relational-databases/security/row-level-security#MidTier) for users who connect to the database through a middle-tier application where application users share the same SQL Server user account. For optimal performance and manageability, follow the [Row-Level Security best practices](/sql/relational-databases/security/row-level-security#Best).

> [!TIP]
> Use Row-Level Security (RLS) together with either Always Encrypted or Dynamic Data Masking (DDM) to maximize the security posture of your organization. 


## File encryption

[Transparent Data Encryption (TDE)](/sql/relational-databases/security/encryption/transparent-data-encryption#enable-tde) protects the data at the file level by providing encryption-at-rest to the database files. Transparent Data Encryption (TDE) ensures that database files, backup files, and tempdb files can't be attached and read without proper certificates decrypting database files. Without Transparent Data Encryption (TDE), it is possible for an attacker to take the physical media (drives or backup tapes) and restore or attach the database to read the contents. Transparent Data Encryption (TDE) is supported to work with all other security capabilities in SQL Server. Transparent Data Encryption (TDE) provides real-time I/O encryption and decryption of the data and log files. TDE encryption leverages a database encryption key (DEK) is stored in the user database. The database encryption key can also be protected using a certificate, which is protected by the database master key of the master database. 

Use TDE to protect data at rest, backups, and tempdb. When bringing your own keys to Azure, store secrets and certificates in the [Azure Key Vault](azure-key-vault-integration-configure.md). 


## Auditing and reporting

It's possible to audit SQL Server on Azure VMs both at the SQL Server instance level, and at the virtual machine and Azure level. 


**Auditing with SQL Server**   

To [audit SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine), create an audit policy at either the server or database level. Server policies apply to all existing and newly created databases on the server. For simplicity, enable server-level auditing and allow the database-level auditing to inherit the server-level property for all databases.

Audit [tables and columns](/sql/relational-databases/security/auditing/sql-server-audit-database-engine) with sensitive data that have security measures applied to them. If a table or column is important enough to need protection by a security capability, then it should be considered important enough to audit. It is especially important to audit and regularly review tables that contain sensitive information but where it is not possible to apply desired security measures due to some kind of application or architectural limitation. 

**Auditing in Azure**   
[Auditing with Log Analytics](../../../azure-monitor/agents/data-sources-windows-events.md#configuring-windows-event-logs) documents events and writes to an audit log in a secure Azure BLOB storage account. Log Analytics can be used to decipher the details of the audit logs. Auditing gives you the ability to save data to a separate storage account and create an audit trail of all events you select. You can also leverage Power BI against the audit log for quick analytics of and insights about your data, as well as to provide a view for regulatory compliance. To learn more about auditing at the VM and Azure levels, see [Azure security logging and auditing](../../../security/fundamentals/log-audit.md). 


## Identities and Authentication

SQL Server supports two [authentication modes](/sql/relational-databases/security/choose-an-authentication-mode), Windows authentication mode and 'SQL Server and Windows Authentication mode' (mixed mode).

Logins are separate from database users. First, map logins or Windows groups to database users or roles separately. Next, grant permissions to users, [server roles](/sql/relational-databases/security/authentication-access/server-level-roles), and/or [database roles](/sql/relational-databases/security/authentication-access/database-level-roles) to access database objects.

SQL Server supports the following types of logins:
- A local Windows user account or Active Directory domain account - SQL Server relies on Windows to authenticate the Windows user accounts.
- Windows group - Granting access to a Windows group grants access to all Windows user logins that are members of the group. Removing a user from a group removes the rights from the user that came from the group. Group membership is the preferred strategy.
- SQL Server login - SQL Server stores the username and a hash of the password in the master database.
- [Contained database users](/sql/relational-databases/security/contained-database-users-making-your-database-portable) authenticate SQL Server connections at the database level. A contained database is a database that is isolated from other databases and from the instance of SQL Server (and the master database) that hosts the database. SQL Server supports contained database users for both Windows and SQL Server authentication.

The following recommendations and best practices help secure your identities and authentication methods: 

- Use [least-privilege role-based security](/sql/relational-databases/security/authentication-access/getting-started-with-database-engine-permissions#grant-the-least-permission) strategies to improve security management.
    - It's standard to place Active Directory users in AD groups, AD groups should exist in SQL Server roles, and SQL Server roles should be granted the minimum permissions required by the application.
- In Azure, leverage least-privilege security by using role-based access (RBAC) controls
- Choose Active Directory over SQL Server authentication whenever possible, and especially choose Active Directory over storing the security at the application or database level.
    - If a user leaves the company it is easy to disable the account. 
    - It is also easy to remove users from groups when users change roles or leave the organization. Group security is considered a best practice. 
- Leverage [Multi-Factor Authentication](../../../active-directory/authentication/concept-mfa-howitworks.md) for accounts that have machine-level access, including accounts that use RDP to log into the machine. This helps guard against credential theft or leaks,  as single-factor password-based authentication is a weaker form of authentication with credentials at risk of being compromised or mistakenly given away. 
- Require [strong and complex passwords](/sql/relational-databases/security/strong-passwords) that cannot be easily guessed, and are not used for any other accounts or purposes. Regularly update passwords and enforce Active Directory policies. 

- [Group-Managed Service Accounts (gMSA)](/windows-server/security/group-managed-service-accounts/group-managed-service-accounts-overview) provide automatic password management, simplified service principal name (SPN) management and delegate the management to other administrators. 
    - With gMSA, the Windows operating system manages passwords for the account instead of relying on the administrator to manage the password.
    - gMSA automatically updates the account passwords without restarting services.
    - gMSA reduces the administrative surface-level and improves the separation of duties.
- Minimize the rights granted to the AD account of the DBA; Consider a separation of duties that limit access to the virtual machine, the ability to log into the operating system, the ability to modify error and auditing logs, and the ability to install applications and/or features.

- Consider removing DBA accounts from the sysadmin role and granting [CONTROL SERVER](/sql/relational-databases/security/permissions-database-engine#chart-of-sql-server-permissions) to DBA accounts rather than making them a member of the sysadmin role. The system admin role does not respect DENY while [CONTROL SERVER](/sql/relational-databases/security/permissions-database-engine#chart-of-sql-server-permissions) does.

## Data Lineage / Data Integrity

Keeping historical records of data changes over time can be beneficial to address accidental changes to the data. It can also be useful for application-change auditing and can provide the ability to recover data elements when a bad actor has introduced data changes that were not authorized. 

- Leverage [temporal tables](/sql/relational-databases/tables/temporal-tables) to preserve record versions over time, and to see data as it has been over the record's life span to provide a historical view of your application's data.
- Temporal Tables can be used to supply a version of the current table at any point in time.

## Security assessment tools and evaluation

The configuration and assessment tools below provide an ability to address surface-area security, identify data security opportunities, and provide a best practice assessment of the security of your SQL Server environment at the instance level.

- [Surface Area Configuration](/sql/relational-databases/security/surface-area-configuration) - It is recommended to enable only the features that are required by your environment in order to minimize the number of features that can be attacked by a malicious user. 
- [Vulnerability assessment for SQL Server (SSMS)](/sql/relational-databases/security/sql-vulnerability-assessment) - SQL vulnerability assessment is a tool in [SSMS v17.4+](/sql/ssms/download-sql-server-management-studio-ssms) that helps discover, track, and remediate potential database vulnerabilities. The vulnerability assessment is a valuable tool to improve your database security and is executed at the database level, per database.
- [SQL Data Discovery and Classification (SSMS)](/sql/relational-databases/security/sql-data-discovery-and-classification) - It is common for DBAs to manage servers and databases and not be aware of sensitivity of the data that is contained in the database. Data Discovery & Classification adds the capability to discover, classify, label and report on the sensitivity level of your data.  Data Discovery & Classification is supported starting with [SSMS 17.5](/sql/ssms/download-sql-server-management-studio-ssms).
 


## SQL VM features

SQL Server on Azure VMs utilize and integrate with the Azure platform for additional security capabilities. 


### Azure Security Center 

[Azure Security Center](../../../security-center/security-center-introduction.md) is a unified security management system that is designed to evaluate and provide opportunities to improve the security posture of your data environment. The Azure Security Center grants a consolidated view of the security health for all assets in the hybrid cloud. 

- Use [security score](../../../security-center/secure-score-security-controls.md) in Azure Security Center. 
- Review the list of the [compute](../../../security-center/recommendations-reference.md#compute-recommendations) and [data recommendations](../../../security-center/recommendations-reference.md#data-recommendations) currently available, for further details.
- Registering your SQL Server VM with the [SQL Server IaaS Agent Extension](sql-agent-extension-manually-register-single-vm.md) surfaces Azure Security Center recommendations to the [SQL virtual machines resource](manage-sql-vm-portal.md) in the Azure portal. 

### Azure Defender for SQL 
    
[Azure Defender for SQL](../../../security-center/defender-for-sql-introduction.md) enables Azure Security Center security features such as [vulnerability assessments](../../database/sql-vulnerability-assessment.md) and security alerts. See [enable Azure Defender for SQL](../../../security-center/defender-for-sql-usage.md) to learn more. 
Use Azure Defender for SQL to discover and mitigate potential database vulnerabilities, and detect anomalous activities that may indicate a threat to your SQL Server instance and database layer. [Vulnerability Assessments](../../database/sql-vulnerability-assessment.md) are a feature of Azure Defender for SQL that can discover and help remediate potential risks to your SQL Server environment. It provides visibility into your security state, and it includes actionable steps to resolve security issues. Registering your SQL Server VM with the [SQL Server IaaS Agent Extension](sql-agent-extension-manually-register-single-vm.md) surfaces Azure Defender for SQL recommendations to the [SQL virtual machines resource](manage-sql-vm-portal.md) in the Azure portal. 

### Azure Advisor 
       
[Azure Advisor](../../../advisor/advisor-security-recommendations.md) is a personalized cloud consultant that helps you follow best practices to optimize your Azure deployments. Azure Advisor analyzes your resource configuration and usage telemetry and then recommends solutions that can help you improve the cost effectiveness, performance, high availability, and security of your Azure resources. Azure Advisor can evaluate at the virtual machine, resource group, or subscription level.


### Portal management

After you've [registered your SQL Server VM with the SQL Server IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md), you can configure a number of security settings using the [SQL virtual machines resource](manage-sql-vm-portal.md) in the Azure portal, such as enabling Azure Key Vault integration, or SQL authentication. 

Additionally, after you've enabled [Azure Defender for SQL](../../../security-center/defender-for-sql-usage.md) you can view Security Center features directly within the [SQL virtual machines resource](manage-sql-vm-portal.md) in the Azure portal, such as vulnerability assessments and security alerts. 

See [manage SQL Server VM in the portal](manage-sql-vm-portal.md) to learn more. 

### Azure Key Vault integration 

There are multiple SQL Server encryption features, such as transparent data encryption (TDE), column level encryption (CLE), and backup encryption. These forms of encryption require you to manage and store the cryptographic keys you use for encryption. The [Azure Key Vault](azure-key-vault-integration-configure.md) service is designed to improve the security and management of these keys in a secure and highly available location. The SQL Server Connector allows SQL Server to use these keys from Azure Key Vault.

Consider the following: 

 - Azure Key Vault stores application secrets in a centralized cloud location to securely control access permissions, and separate access logging.
 - When bringing your own keys to Azure it is recommended to store secrets and certificates in the [Azure Key Vault](/sql/relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server). 
 - Azure Disk Encryption uses [Azure Key Vault](../../../virtual-machines/windows/disk-encryption-key-vault.md) to control and manage disk encryption keys and secrets.

### Access control 

When you create your SQL Server virtual machine, consider how to carefully control who has access to the machine and to SQL Server. In general, you should do the following:

- Restrict access to SQL Server to only the applications and clients that need it.
- Follow best practices for managing user accounts and passwords.

The following sections provide suggestions on thinking through these points.

### Secure connections

When you create a SQL Server virtual machine with an Azure gallery image, the **SQL Server Connectivity** option gives you the choice of **Local (inside VM)**, **Private (within Virtual Network)**, or **Public (Internet)**.

![SQL Server connectivity](./media/security-considerations-best-practices/sql-vm-connectivity-option.png)

For the best security, choose the most restrictive option for your scenario. For example, if you are running an application that accesses SQL Server on the same VM, then **Local** is the most secure choice. If you are running an Azure application that requires access to the SQL Server, then **Private** secures communication to SQL Server only within the specified [Azure virtual network](../../../virtual-network/virtual-networks-overview.md). If you require **Public** (internet) access to the SQL Server VM, then make sure to follow other best practices in this topic to reduce your attack surface area.

The selected options in the portal use inbound security rules on the VM's [network security group](../../../active-directory/identity-protection/concept-identity-protection-security-overview.md) (NSG) to allow or deny network traffic to your virtual machine. You can modify or create new inbound NSG rules to allow traffic to the SQL Server port (default 1433). You can also specify specific IP addresses that are allowed to communicate over this port.

![Network security group rules](./media/security-considerations-best-practices/sql-vm-network-security-group-rules.png)

In addition to NSG rules to restrict network traffic, you can also use the Windows Firewall on the virtual machine.

If you are using endpoints with the classic deployment model, remove any endpoints on the virtual machine if you do not use them. For instructions on using ACLs with endpoints, see [Manage the ACL on an endpoint](/previous-versions/azure/virtual-machines/windows/classic/setup-endpoints#manage-the-acl-on-an-endpoint). This is not necessary for VMs that use the Azure Resource Manager.

Consider enabling [encrypted connections](/sql/database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine) for the instance of the SQL Server Database Engine in your Azure virtual machine. Configure SQL server instance with a signed certificate. For more information, see [Enable Encrypted Connections to the Database Engine](/sql/database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine) and [Connection String Syntax](/dotnet/framework/data/adonet/connection-string-syntax).

Consider the following when **securing the network connectivity or perimeter**: 

- [Azure Firewall](../../../firewall/features.md) - A stateful, managed, Firewall as a Service (FaaS) that grants/ denies server access based on originating IP address, to protect network resources.
- [Azure Distributed Denial of Service (DDoS) protection](../../../ddos-protection/ddos-protection-overview.md) - DDoS attacks overwhelm and exhaust network resources, making apps slow or unresponsive. Azure DDoS protection sanitizes unwanted network traffic before it impacts service availability. 
- [Network Security Groups (NSGs)](../../../virtual-network/network-security-groups-overview.md) - Filters network traffic to, and from, Azure resources on Azure Virtual Networks
- [Application Security Groups](../../../virtual-network/application-security-groups.md) - Provides for the grouping of servers with similar port filtering requirements, and group together servers with similar functions, such as web servers

### Virtual Machine level access

Close management ports on your machine - Open remote management ports are exposing your VM to a high level of risk from internet-based attacks. These attacks attempt to brute force credentials to gain admin access to the machine.
- Turn on [Just-in-time (JIT) access](../../../security-center/security-center-just-in-time.md?tabs=jit-config-asc%2Cjit-request-asc) for Azure virtual machines. 
- Leverage [Azure Bastion](../../../bastion/bastion-overview.md) over Remote Desktop Protocol (RDP). 

### Server-side and Disk Encryption

Managed disks offer server-side encryption, and Azure Disk Encryption. [Server-side encryption](../../../virtual-machines/disk-encryption.md) provides encryption-at-rest and safeguards your data to meet your organizational security and compliance commitments. [Azure Disk Encryption](../../../security/fundamentals/azure-disk-encryption-vms-vmss.md) uses either BitLocker or DM-Crypt technology, and integrates with Azure Key Vault to encrypt both the OS and data disks. 

Consider the following: 

- [Azure Disk Encryption](../../../virtual-machines/windows/disk-encryption-overview.md) - Encrypts virtual machine disks using Azure Disk Encryption both for Windows and Linux virtual machines.
    - When your compliance and security requirements require you to encrypt the data end-to-end using your encryption keys, including encryption of the ephemeral (locally attached temporary) disk, use
[Azure disk encryption](../../../virtual-machines/windows/disk-encryption-windows.md).
    - Azure Disk Encryption (ADE) leverages the industry-standard BitLocker feature of Windows and the DM-Crypt feature of Linux to
provide OS and data disk encryption.
- Managed Disk Encryption
    - [Managed Disks are encrypted](../../../virtual-machines/disk-encryption.md) at rest by default using Azure Storage Service Encryption where the encryption keys are Microsoft managed keys stored in Azure.
    - Data in Azure managed disks is encrypted transparently using 256-bit AES encryption, one of the strongest block ciphers available, and is FIPS 140-2 compliant.
- For a comparison of the managed disk encryption options review the [managed disk encryption comparison chart](../../../virtual-machines/disk-encryption-overview.md#comparison). 

### Virtual Machine Extensions

Azure Virtual Machine extensions are trusted Microsoft or 3rd party extensions that can help address specific needs and risks such as antivirus, malware, threat protection, and more.

- [Guest Configuration extension](../../../virtual-machines/extensions/guest-configuration.md)
    - To ensure secure configurations of in-guest settings of your machine, install the Guest Configuration extension. 
    - In-guest settings include the configuration of the operating system, application configuration or presence, and environment settings. 
    - Once installed, in-guest policies will be available such as 'Windows Exploit guard should be enabled'.
- [Network traffic data collection agent](../../../virtual-machines/extensions/network-watcher-windows.md)
    - Security Center uses the Microsoft Dependency agent to collect network traffic data from your Azure virtual machines. 
    - This agent enables advanced network protection features such as traffic visualization on the network map, network hardening recommendations, and specific network threats.
- [Evaluate extensions](../../../virtual-machines/extensions/overview.md) from Microsoft and 3rd parties to address anti-malware, desired state, threat detection, prevention, and remediation to address threats at the operating system, machine, and network levels.

## Common SQL Threats 

It helps to know what are some common threats that risk SQL Server: 

- [SQL injection](/sql/relational-databases/security/sql-injection) - SQL injection is a type of attack where malicious code is inserted into strings that are passed to an instance of SQL Server for execution. 
    - The injection process works by terminating a text string and appending a new command. Because the inserted command may have additional strings appended to it before it is executed, the attacker terminates the injected string with a comment mark "--".
    - SQL Server will execute any syntactically valid query that is received.
- Be aware of [Side-channel attacks](../../../virtual-machines/mitigate-se.md), [malware & other threats](/windows/security/threat-protection/intelligence/understanding-malware). 

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
            - **Note:** It is not recommended to leverage xp_cmdshell on any SQL Server environment. It is recommended to leverage SQLCLR and look for other alternatives due to the risks xp_cmdshell may introduce.
   - Always [validate user inputs](/sql/relational-databases/security/sql-injection#validate-all-input) and scrub error outputs from being spilled and exposed to the attacker.


### Side-channel risks

To minimize the risk of a side-channel attack, consider the following: 

- Ensure the latest application and operating system patches are applied. 
- For hybrid workloads, ensure the latest firmware patches are applied for any hardware on-premises.
- In Azure, for highly sensitive applications and workloads, you can add additional protection against side-channel attacks with isolated virtual machines, dedicated hosts, or by leveraging Confidential Compute virtual machines such as the [DC-series](../../../virtual-machines/dcv2-series.md) and [Virtual Machines that leverage 3rd Gen AMD EPYC processors](https://azure.microsoft.com/blog/azure-and-amd-enable-lift-and-shift-confidential-computing/).

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
    > If you do not enable SQL Authentication during provisioning, you must manually change the authentication mode to **SQL Server and Windows Authentication Mode**. For more information, see [Change Server Authentication Mode](/sql/database-engine/configure-windows/change-server-authentication-mode).
  - If you must use the **SA** login, enable the login after provisioning and assign a new strong password.


### Ransomware Risks

Consider the following to minimize ransomware risks: 

- The best strategy to guard against [ransomware](/windows/security/threat-protection/intelligence/ransomware-malware) is to pay particular attention to RDP and SSH vulnerabilities. Additionally, consider the following: 
    - Leverage firewalls and lock down ports
    - Ensuring the latest operating system and application security updates are applied
    - Use [group managed service accounts (gMSA)](/windows-server/security/group-managed-service-accounts/group-managed-service-accounts-overview)
    - Limit access to the virtual machines
    - Require  [Just-in-time (JIT) access](../../../security-center/security-center-just-in-time.md) and [Azure Bastion](../../../bastion/bastion-overview.md)
    - Improve Surface Area Security by avoiding installing tools including sysinternals and SSMS on the local machine
    - Avoid installing Windows Features, roles and enabling services that are not required
    - Additionally, there should be a regular full backup scheduled that is separately secured from a common administrator account so it can't delete copies of the databases. 

## Next steps

For other topics related to running SQL Server in Azure VMs, see [SQL Server on Azure Virtual Machines overview](sql-server-on-azure-vm-iaas-what-is-overview.md). If you have questions about SQL Server virtual machines, see the [Frequently Asked Questions](frequently-asked-questions-faq.yml).

To learn more, see the other articles in this series:

- [Quick checklist](performance-guidelines-best-practices-checklist.md)
- [VM size](performance-guidelines-best-practices-vm-size.md)
- [Storage](performance-guidelines-best-practices-storage.md)
- [Security](security-considerations-best-practices.md)
- [HADR settings](hadr-cluster-best-practices.md)
- [Collect baseline](performance-guidelines-best-practices-collect-baseline.md)


