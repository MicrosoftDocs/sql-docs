---
title: "Security Center for SQL Server Database Engine and Azure SQL Database | Microsoft Docs"
ms.custom: ""
ms.date: "09/27/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "Security [SQL Server]"
helpviewer_keywords: 
  - "SQL Server, security"
  - "security [SQL Server]"
  - "database security [SQL Server]"
  - "databases [SQL Server], security"
ms.assetid: dfb39d16-722a-4734-94bb-98e61e014ee7
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Security Center for SQL Server Database Engine and Azure SQL Database
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  This page provides links to help you locate the information that you need about security and protection in the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)].  
  
 **Legend**  
  
 ![security-center-legend](../performance/media/security-center-legend.PNG "security-center-legend")  
  
##  <a name="Who"></a> Authentication: Who are you?  
  
|||  
|-|-|  
|**Who Authenticates?**<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") Windows Authentication<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> ![security-center-sqldb](../../relational-databases/security/media/security-center-sqldb.png "security-center-sqldb")  Azure Active Directory|Who Authenticates? (Windows or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)])<br /><br /> [Choose an Authentication Mode](../../relational-databases/security/choose-an-authentication-mode.md)<br /><br /> [Connecting to SQL Database By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication/)|  
|**Where Authenticated?**<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") At master Database: Logins and DB Users<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") At User Database: Contained DB Users|Authenticate at the master database (Logins and database users)<br /><br /> [Create a SQL Server Login](../../relational-databases/security/authentication-access/create-a-login.md)<br /><br /> [Managing Databases and Logins in Azure SQL Database](https://msdn.microsoft.com/library/ee336235.aspx)<br /><br /> [Create a Database User](../../relational-databases/security/authentication-access/create-a-database-user.md)<br /><br /> <br /><br /> Authenticate at a user database<br /><br /> [Contained Database Users - Making Your Database Portable](../../relational-databases/security/contained-database-users-making-your-database-portable.md)|  
|**Using Other Identities**<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Credentials<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") Execute as Another Login<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Execute as Another Database User|[Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)<br /><br /> [Execute as Another Login](../../t-sql/statements/execute-as-transact-sql.md)<br /><br /> [Execute as Another Database User](../../t-sql/statements/execute-as-transact-sql.md)|  
  
##  <a name="What"></a> Authorization: What can you do?  
  
|||  
|-|-|  
|**Granting, Revoking, and Denying Permissions**<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Securable Classes<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") Granular Server Permissions<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Granular Database Permissions|[Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)<br /><br /> [Permissions](../../relational-databases/security/permissions-database-engine.md)<br /><br /> [Securables](../../relational-databases/security/securables.md)<br /><br /> [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)|  
|**Security by Roles**<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") Server Level Roles<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Database Level Roles|[Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md)<br /><br /> [Database-Level Roles](../../relational-databases/security/authentication-access/database-level-roles.md)|  
|**Restricting Data Access to Selected Data Elements**<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Restrict Data Access With Views/Procedures<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Row-Level Security<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Dynamic Data Masking<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Signed Objects|Restrict Data Access Using [Views](../../relational-databases/views/views.md) and [Procedures](../../relational-databases/stored-procedures/stored-procedures-database-engine.md)<br /><br /> [Row-Level Security (SQL Server)](../../relational-databases/security/row-level-security.md)<br /><br /> [Row-Level Security (Azure SQL Database)](https://msdn.microsoft.com/library/azure/dn765131.aspx)<br /><br /> [Dynamic Data Masking (SQL Server)](../../relational-databases/security/dynamic-data-masking.md)<br /><br /> [Dynamic Data Masking (Azure SQL Database)](https://azure.microsoft.com/documentation/articles/sql-database-dynamic-data-masking-get-started/)<br /><br /> [Signed Objects](../../t-sql/statements/add-signature-transact-sql.md)|  
  
##  <a name="Encrypt"></a> Encryption: Storing Secret Data  
  
|||  
|-|-|  
|**Encrypting Files**<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") BitLocker Encryption (Drive Level)<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") NTFS Encryption (Folder Level)<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Transparent Data Encryption (File Level)<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Backup Encryption (File Level)|[BitLocker (Drive Level)](https://support.microsoft.com/kb/2855131)<br /><br /> [NTFS Encryption (Folder Level)](https://msdn.microsoft.com/library/dd163562.aspx)<br /><br /> [Transparent Data Encryption (File Level)](../../relational-databases/security/encryption/transparent-data-encryption.md)<br /><br /> [Backup Encryption (File Level)](../../relational-databases/backup-restore/backup-encryption.md)|  
|**Encrypting Sources**<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") Extensible Key Management Module<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") Keys Stored in the Azure Key Vault<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Always Encrypted|[Extensible Key Management Module](../../relational-databases/security/encryption/extensible-key-management-ekm.md)<br /><br /> [Keys Stored in the Azure Key Vault](../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)<br /><br /> [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md)|  
|**Column, Data, & Key Encryption**<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Encrypt by Certificate<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Encrypt by Symmetric Key<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Encrypt by Asymmetric Key<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Encrypt by Passphrase|[Encrypt by Certificate](../../t-sql/functions/encryptbycert-transact-sql.md)<br /><br /> [Encrypt by Asymmetric Key](../../t-sql/functions/encryptbyasymkey-transact-sql.md)<br /><br /> [Encrypt by Symmetric Key](../../t-sql/functions/encryptbykey-transact-sql.md)<br /><br /> [Encrypt by Passphrase](../../t-sql/functions/encryptbypassphrase-transact-sql.md)<br /><br /> [Encrypt a Column of Data](../../relational-databases/security/encryption/encrypt-a-column-of-data.md)|  
  
##  <a name="Connect"></a> Connection Security: Restricting and Securing  
  
|||  
|-|-|  
|**Firewall Protection**<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") Windows Firewall Settings<br /><br /> ![security-center-sqldb](../../relational-databases/security/media/security-center-sqldb.png "security-center-sqldb") Azure Service Firewall Settings<br /><br /> ![security-center-sqldb](../../relational-databases/security/media/security-center-sqldb.png "security-center-sqldb") Database Firewall Settings|[Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)<br /><br /> [Azure SQL Database Firewall Settings](../../relational-databases/system-stored-procedures/sp-set-database-firewall-rule-azure-sql-database.md)<br /><br /> [Azure Service Firewall Settings](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md)|  
|**Encrypting Data in Transit**<br /><br /> ![security-center-both](../performance/media/security-center-both.png "security-center-both") Forced SSL Connections<br /><br /> ![security-center-sqlserver](../performance/media/security-center-sqlserver.png "security-center-sqlserver") Optional SSL Connections|[Secure Sockets Layer for the Database Engine](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)<br /><br /> [Secure Sockets Layer for SQL Database](https://msdn.microsoft.com/library/azure/ff394108.aspx)<br /><br /> [TLS 1.2 support for Microsoft SQL Server](https://support.microsoft.com/kb/3135244)|  
  
##  <a name="Audit"></a> Auditing: Recording Access  
  
|||  
|-|-|  
|**Automated Auditing**<br /><br /> ![security-center-sqlserver](../../relational-databases/performance/media/security-center-sqlserver.png "security-center-sqlserver") [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit (Server and DB Level)<br /><br /> ![security-center-sqldb](../../relational-databases/security/media/security-center-sqldb.png "security-center-sqldb") [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Audit (Database Level)<br /><br /> ![security-center-sqldb](../../relational-databases/security/media/security-center-sqldb.png "security-center-sqldb") Threat Detection| <br /><br /> [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md)<br /><br /> [SQL Database Auditing](https://azure.microsoft.com/documentation/articles/sql-database-auditing-get-started/)<br /><br /> [Get started with SQL Database Threat Detection](https://azure.microsoft.com/documentation/articles/sql-database-threat-detection-get-started/) <br /><br /> [SQL Database Vulnerability Assessment](https://docs.microsoft.com/azure/sql-database/sql-vulnerability-assessment) |  
|**Custom Audit**<br /><br /> ![security-center-both](../../relational-databases/performance/media/security-center-both.png "security-center-both") Triggers|Custom Audit Implementation: Creating [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md) and [DML Triggers](../../relational-databases/triggers/dml-triggers.md)|  
|**Compliance**<br /><br /> ![security-center-both](../../relational-databases/performance/media/security-center-both.png "security-center-both") Compliance|SQL Server:<br />                        [Common Criteria](https://go.microsoft.com/fwlink/?LinkId=616319)<br /><br /> SQL Database:<br />                        [Microsoft Azure Trust Center: Compliance by Feature](https://azure.microsoft.com/support/trust-center/services/)|  
  
##  <a name="SQLInjection"></a> SQL Injection  
 SQL injection is an attack in which malicious code is inserted into strings that are later passed to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for parsing and execution. Any procedure that constructs SQL statements should be reviewed for injection vulnerabilities because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will execute all syntactically valid queries that it receives. All database systems have some risk of SQL Injection, and many of the vulnerabilities are introduced in the application that is querying the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. You can thwart SQL injection attacks by using stored procedures and parameterized commands, avoiding dynamic SQL, and restricting permissions on all users.  For more information, see [SQL Injection](../../relational-databases/security/sql-injection.md).  
  
 Additional links for application programmers:  
  
-   [Application Security Scenarios in SQL Server](/dotnet/framework/data/adonet/sql/application-security-scenarios-in-sql-server)  
  
-   [Writing Secure Dynamic SQL in SQL Server](/dotnet/framework/data/adonet/sql/writing-secure-dynamic-sql-in-sql-server)  
  
-   [How To: Protect From SQL Injection in ASP.NET](https://msdn.microsoft.com/library/ff648339.aspx)  
  
## See Also  
 [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)   
 [Securing SQL Server](../../relational-databases/security/securing-sql-server.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [SQL Server Certificates and Asymmetric Keys](../../relational-databases/security/sql-server-certificates-and-asymmetric-keys.md)   
 [SQL Server Encryption](../../relational-databases/security/encryption/sql-server-encryption.md)   
 [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md)   
 [Strong Passwords](../../relational-databases/security/strong-passwords.md)   
 [TRUSTWORTHY Database Property](../../relational-databases/security/trustworthy-database-property.md)   
 [Database Engine Features and Tasks](https://msdn.microsoft.com/library/d9efe145-3306-4d61-bd77-e2af43e19c34)  
 [Protecting Your SQL Server Intellectual Property](../../relational-databases/security/protecting-your-sql-server-intellectual-property.md)   
  
[!INCLUDE[get-help-security](../../includes/get-help-security.md)]
