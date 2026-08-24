---
title: Security Overview
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: Learn about security in Azure SQL Database and Azure SQL Managed Instance and Azure Synapse Analytics, including how it differs from SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jaszymas, vanto, emlisa, mathoma, maghan
ms.date: 01/23/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - sqldbrb=2
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# An overview of Azure SQL Database and SQL Managed Instance security capabilities

[!INCLUDE [appliesto-sqldb-sqlmi-asa](../includes/appliesto-sqldb-sqlmi-asa.md)]

This article outlines the basics of securing the data tier of an application that uses [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is). The security strategy described in this article follows the layered defense-in-depth approach as shown in the following diagram, and moves from the outside in:

:::image type="content" source="media/security-overview/sql-security-layer.png" alt-text="Diagram of layered defense-in-depth. Customer data is encased in layers of network security, access management, and threat and information protections." lightbox="media/security-overview/sql-security-layer.png":::

## Network security

Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics provide a relational database service for cloud and enterprise applications. To help protect customer data, firewalls prevent network access to the server until you explicitly grant access based on IP address or Azure Virtual network traffic origin.

### IP firewall rules

IP firewall rules grant access to databases based on the originating IP address of each request. For more information, see [Overview of Azure SQL Database and Azure Synapse Analytics firewall rules](firewall-configure.md).

### Virtual network firewall rules

[Virtual network service endpoints](/azure/virtual-network/virtual-network-service-endpoints-overview) extend your virtual network connectivity over the Azure backbone and enable Azure SQL Database to identify the virtual network subnet that traffic originates from. To allow traffic to reach Azure SQL Database, use the SQL [service tags](/azure/virtual-network/network-security-groups-overview) to allow outbound traffic through Network Security Groups.

- [Virtual network rules](vnet-service-endpoint-rule-overview.md) enable Azure SQL Database to only accept communications that are sent from selected subnets inside a virtual network.
- Controlling access with firewall rules doesn't apply to **SQL Managed Instance**. For more information about the networking configuration needed, see [Connecting to a managed instance](../managed-instance/connect-application-instance.md)

> [!NOTE]  
> Controlling access with firewall rules doesn't apply to **SQL Managed Instance**. For more information about the networking configuration needed, see [Connecting to a managed instance](../managed-instance/connect-application-instance.md)

### Network security perimeter

An [Azure network security perimeter](/azure/private-link/network-security-perimeter-concepts#onboarded-private-link-resources) creates logical network boundaries around your platform-as-a-service (PaaS) resources that you deploy outside your virtual networks. 

- An Azure network security perimeter helps you control public network access to Azure SQL Database.
- Controlling access with an Azure network security perimeter doesn't apply to Azure SQL Managed Instance.

> [!IMPORTANT]  
> Azure SQL Database with Network Security Perimeter is currently in preview. The previews are provided without a service level agreement, and it's not recommended for production workloads. Certain features might not be supported or might have constrained capabilities. For more information, see [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

## Authentication

Authentication is the process of proving the user is who they claim to be. Azure SQL Database and SQL Managed Instance support authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) and SQL authentication. SQL Managed instance additionally supports [Windows authentication](/azure/azure-sql/managed-instance/winauth-azuread-setup) for Microsoft Entra principals.

- **Microsoft Entra authentication**:

    Microsoft Entra authentication is a mechanism to connect to [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) by using identities in Microsoft Entra ID. Microsoft Entra authentication allows administrators to centrally manage the identities and permissions of database users along with other Azure services in one central location. This feature can help eliminate the use of secrets and passwords.

     To use Microsoft Entra authentication with SQL Database, create a server admin called the **Microsoft Entra administrator**. For more information, see [Connecting to SQL Database with Microsoft Entra authentication](authentication-aad-overview.md). Microsoft Entra authentication supports both managed and federated accounts. The federated accounts support Windows users and groups for a customer domain federated with Microsoft Entra ID.

    Microsoft Entra supports several different authentication options, including [multifactor authentication](/azure/active-directory/authentication/concept-mfa-howitworks), [Integrated Windows authentication](/azure/active-directory/develop/msal-authentication-flows#integrated-windows-authentication-iwa), and [Conditional Access](conditional-access-configure.md).

- **Windows authentication for Microsoft Entra principals**:

    [Kerberos authentication for Microsoft Entra principals](../managed-instance/winauth-azuread-overview.md) enables Windows authentication for Azure SQL Managed Instance. Windows authentication for managed instances empowers customers to move existing services to the cloud while maintaining a seamless user experience and provides the basis for infrastructure modernization.

    To enable Windows authentication for Microsoft Entra principals, turn your Microsoft Entra tenant into an independent Kerberos realm and create an incoming trust in the customer domain. Learn [how Windows authentication for Azure SQL Managed Instance is implemented with Microsoft Entra ID and Kerberos](../managed-instance/winauth-implementation-aad-kerberos.md).

- **SQL authentication**:

    SQL authentication refers to the authentication of a user when connecting to Azure SQL Database or Azure SQL Managed Instance by using a username and password. You must specify a **server admin** login with a username and password when creating the server. Using these credentials, a **server admin** can authenticate to any database on that server or instance as the database owner. After that, the server admin can create other SQL logins and users, which enable users to connect by using a username and password.

    [!INCLUDE [server-admin-login-security-note](../includes/server-admin-login-security-note.md)]

## Authorization and access management

Authorization refers to controlling access to management of servers and databases, and to data, resources, and commands within a database. You assign permissions to a user within a database in Azure SQL Database or Azure SQL Managed Instance. Your portal user account's role assignments control managing databases and servers within Azure. For more information, see [Azure role-based access control in the Azure portal](/azure/role-based-access-control/overview).

Manage permissions by adding user accounts to [database roles](/sql/relational-databases/security/authentication-access/database-level-roles) and assigning database-level permissions to those roles. Alternatively, grant certain [object-level permissions](/sql/relational-databases/security/permissions-database-engine) to an individual user. For more information, see [Logins and users](logins-create-manage.md).

In addition, Azure SQL Managed Instance provides [server-level roles](/sql/relational-databases/security/authentication-access/server-level-roles) (fixed or custom) to manage permissions for an instance. Server-level roles have server-wide permissions scope. You can add server-level principals into server-level roles.

As a best practice, create custom roles when needed. Add users to the role with the least privileges required to do their job function. Don't assign permissions directly to users. The server admin account is a member of the built-in db_owner role, which has extensive permissions and should only be granted to a few users with administrative duties. To further limit the scope of what a user can do, use the [EXECUTE AS](/sql/t-sql/statements/execute-as-clause-transact-sql) to specify the execution context of the called module. Following these best practices is also a fundamental step toward Separation of Duties.

### Row-level security

Row-Level Security enables you to control access to rows in a database table based on the characteristics of the user executing a query (for example, group membership or execution context). Use Row-Level Security to implement custom Label-based security concepts. For more information, see [Row-Level security](/sql/relational-databases/security/row-level-security).

:::image type="content" source="media/security-overview/row-level-security.png" alt-text="Diagram showing that Row-Level Security shields individual rows of an SQL database from access by users via a client app.":::

## Threat protection

Azure SQL Database and SQL Managed Instance secure customer data by providing auditing and threat detection capabilities.

### SQL auditing in Azure Monitor logs and Event Hubs

SQL Database and SQL Managed Instance auditing tracks database activities and helps maintain compliance with security standards by recording database events to an audit log in a customer-owned Azure storage account. Auditing allows you to monitor ongoing database activities, as well as analyze and investigate historical activity to identify potential threats or suspected abuse and security violations. For more information, see Get started with [SQL Database Auditing](./auditing-overview.md).

### Advanced Threat Protection

Advanced Threat Protection analyzes your logs to detect unusual behavior and potentially harmful attempts to access or exploit databases. It creates alerts for suspicious activities such as SQL injection, potential data infiltration, and brute force attacks or for anomalies in access patterns to catch privilege escalations and breached credentials use. You can view alerts from [Microsoft Defender for Cloud](https://azure.microsoft.com/services/security-center/), where the details of the suspicious activities are provided and recommendations for further investigation given along with actions to mitigate the threat. You can enable Advanced Threat Protection per server for an additional fee. For more information, see [Get started with SQL Database Advanced Threat Protection](threat-detection-configure.md).

:::image type="content" source="media/security-overview/advanced-threat-detection.png" alt-text="Diagram showing SQL Threat Detection monitoring access to the SQL database for a web app from an external attacker and malicious insider.":::

## Information protection and encryption

### Transport Layer Security (encryption-in-transit)

SQL Database, SQL Managed Instance, and Azure Synapse Analytics secure customer data by encrypting data in motion with [Transport Layer Security (TLS)](/troubleshoot/sql/database-engine/connect/tls-1-2-support-microsoft-sql-server). These services always enforce TLS encrypted connections to ensure all data is encrypted in transit between the client and server.

Specifically, SQL Database, SQL Managed Instance, and Azure Synapse Analytics set the configuration flag `ForceEncryption` to `Yes`. Clients and drivers must support encrypted connections to connect to these services. The lowest version of the TDS protocol that can connect is TDS 7.1.

As a best practice, if you have [TDS 8.0](/sql/relational-databases/security/networking/tds-8)-capable SQL drivers, use [Strict connection encryption](/sql/relational-databases/security/networking/tds-8#strict-connection-encryption). 

If your drivers don't support TDS 8.0, use mandatory encryption and don't trust the server certificate. For example, when using the ADO.NET driver, use `Encrypt=True` and `TrustServerCertificate=False` in the connection string to accomplish this. The connection string you get from the Azure portal is already configured with these values.

Avoid setting the parameter `TrustServerCertificate` to `True` in production use. `TrustServerCertificate=True` is too permissive and doesn't protect against man-in-the-middle attacks. Instead, if your client expects a different domain name in the server certificate, use the `HostNameInCertificate` parameter to provide the correct domain name for validation.

For example, when using the ADO.NET driver to connect to your managed instance `contoso-instance.123456.database.windows.net` through a custom domain name `contoso-instance.contoso.com`, set the connection parameters `Encrypt=True` and set `HostNameInCertificate=contoso-instance.123456.database.windows.net`. This configuration allows the driver to validate the server certificate against an expected VNet-local endpoint domain name.

> [!IMPORTANT]  
> Some non-Microsoft drivers might not use TLS by default or might rely on an older version of TLS (earlier than 1.2) to function. In this case, the server still allows you to connect to your database. However, evaluate the security risks of allowing such drivers and applications to connect to SQL Database, especially if you store sensitive data.
>
> For more information about TLS and connectivity, see [TLS considerations](connect-query-content-reference-guide.md#tls-considerations-for-database-connectivity).

### Transparent data encryption (encryption-at-rest) with service-managed keys

[Transparent data encryption (TDE) for SQL Database, SQL Managed Instance, and Azure Synapse Analytics](transparent-data-encryption-tde-overview.md) adds a layer of security to help protect data at rest from unauthorized or offline access to raw files or backups. Common scenarios include data center theft or unsecured disposal of hardware or media such as disk drives and backup tapes. TDE encrypts the entire database by using an AES encryption algorithm, which doesn't require application developers to make any changes to existing applications.

In Azure, all newly created databases are encrypted by default and the database encryption key is protected by a built-in server certificate. The service manages certificate maintenance and rotation and requires no input from the user. If you prefer to take control of the encryption keys, you can manage the keys in [Azure Key Vault](/azure/key-vault/general/security-features).

### Transparent data encryption (encryption-at-rest) with customer-managed keys

If you need greater control over encryption keys, [transparent data encryption](/sql/relational-databases/security/encryption/transparent-data-encryption) (TDE) supports [customer-managed keys (CMK)](transparent-data-encryption-byok-overview.md). This CMK is associated with the logical server and wraps the database encryption keys for all databases within that server. Alternatively, you can configure a CMK at the individual [database level](transparent-data-encryption-byok-database-level-overview.md). By managing the CMK, you can control key rotation, revocation, and auditing, which is often necessary for compliance or strict security policies.

### Always Encrypted and Always Encrypted with secure enclaves (encryption-in-use)

:::image type="content" source="media/security-overview/always-encrypted.png" alt-text="Diagram showing the basics of the Always Encrypted feature. An SQL database with a lock is only accessed by an app containing a key.":::

[Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-database-engine) and [Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/always-encrypted-enclaves) are features designed to protect sensitive data stored in specific database columns from access (for example, credit card numbers, national/regional identification numbers, or data on a _need to know_ basis). This protection includes database administrators or other privileged users who are authorized to access the database to perform management tasks but have no business need to access the particular data in the encrypted columns. The data is always encrypted, which means the encrypted data is decrypted only for processing by client applications with access to the encryption key. The encryption key is never exposed to SQL Database or SQL Managed Instance and can be stored either in the [Windows Certificate Store](always-encrypted-certificate-store-configure.md) or in [Azure Key Vault](always-encrypted-azure-key-vault-configure.md).

### Dynamic data masking

:::image type="content" source="media/security-overview/dynamic-data-masking.png" alt-text="Diagram showing dynamic data masking. A business app sends data to an SQL database that masks the data before sending it back to the business app.":::

Dynamic data masking limits sensitive data exposure by masking it to nonprivileged users. Dynamic data masking automatically discovers potentially sensitive data in Azure SQL Database and SQL Managed Instance and provides actionable recommendations to mask these fields, with minimal impact to the application layer. It works by obfuscating the sensitive data in the result set of a query over designated database fields, while the data in the database isn't changed. For more information, see [Get started with SQL Database and SQL Managed Instance dynamic data masking](dynamic-data-masking-overview.md).

### Ledger

[Ledger](/sql/relational-databases/security/ledger/ledger-overview) in Azure SQL Database and SQL Managed Instance is a feature that provides cryptographic proof of data integrity. With ledger, you have tamper-evidence capabilities for your data. You can cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with.

Ledger uses tamper-evident technology to record database changes in an immutable ledger, ensuring that any unauthorized modifications can be detected. This feature is particularly useful for scenarios requiring regulatory compliance, auditability, and trust between multiple parties. By enabling ledger, you can verify the integrity of your data, reducing the risk of fraud or data manipulation.

## Security management

### Vulnerability assessment

[Vulnerability assessment](/azure/defender-for-cloud/sql-azure-vulnerability-assessment-overview) is an easy to configure service that can discover, track, and help remediate potential database vulnerabilities with the goal to proactively improve overall database security. Vulnerability assessment (VA) is part of the Microsoft Defender for SQL offering, which is a unified package for advanced SQL security capabilities. You can access and manage vulnerability assessment via the central Microsoft Defender for SQL portal.

### Data discovery and classification

Data discovery and classification provides basic capabilities built into Azure SQL Database and SQL Managed Instance for discovering, classifying, and labeling the sensitive data in your databases. Discovering and classifying your most sensitive data (business, financial, healthcare, personal data, and more) plays a pivotal role in your organizational information protection stature. It serves as infrastructure for:

- Various security scenarios, such as monitoring (auditing) and alerting on anomalous access to sensitive data.
- Controlling access to, and hardening the security of, databases containing highly sensitive data.
- Helping meet data privacy standards and regulatory compliance requirements.

For more information, see [Get started with data discovery and classification](data-discovery-and-classification-overview.md).

### Compliance

In addition to the features and functionality that help your application meet various security requirements, Azure SQL Database also participates in regular audits. It has been certified against a number of compliance standards. For more information, see the [Microsoft Azure Trust Center](https://www.microsoft.com/trust-center/compliance/compliance-overview) where you can find the most current list of SQL Database compliance certifications.

## Related content

- [SQL Server and client encryption summary](/sql/database-engine/configure-windows/sql-server-and-client-encryption-summary)
- [Manage logins and user accounts](logins-create-manage.md)
- [auditing](./auditing-overview.md)
- [threat detection](threat-detection-configure.md)
