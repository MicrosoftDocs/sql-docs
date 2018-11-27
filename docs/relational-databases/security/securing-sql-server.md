---
title: "Securing SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "01/31/2017"
ms.prod: sql
ms.prod_service: security
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "Security [SQL Server]"
helpviewer_keywords: 
  - "database objects [SQL Server], security"
  - "SQL Server, security"
  - "operating systems [SQL Server], security"
  - "security [SQL Server], planning"
  - "applications [SQL Server], security"
ms.assetid: 4d93489e-e9bb-45b3-8354-21f58209965d
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Securing SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Securing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be viewed as a series of steps, involving four areas: the platform, authentication, objects (including data), and applications that access the system. The following topics will guide you through creating and implementing an effective security plan.  
  
 You can find more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security at the [SQL Server](https://go.microsoft.com/fwlink/?LinkID=31629) Web site. This includes a best practice guide and a security checklist. This site also contains the latest service pack information and downloads.  
  
## Platform and Network Security  
 The platform for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes the physical hardware and networking systems connecting clients to the database servers, and the binary files that are used to process database requests.  
  
### Physical Security  
 Best practices for physical security strictly limit access to the physical server and hardware components. For example, use locked rooms with restricted access for the database server hardware and networking devices. In addition, limit access to backup media by storing it at a secure offsite location.  
  
 Implementing physical network security starts with keeping unauthorized users off the network. The following table contains more information about networking security information.  
  
|For information about|See|  
|---------------------------|---------|  
|[!INCLUDE[ssEW](../../includes/ssew-md.md)] and network access to other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] editions|"Configuring and Securing the Server Environment" in [!INCLUDE[ssEW](../../includes/ssew-md.md)] Books Online|  
  
### Operating System Security  
 Operating system service packs and upgrades include important security enhancements. Apply all updates and upgrades to the operating system after you test them with the database applications.  
  
 Firewalls also provide effective ways to implement security. Logically, a firewall is a separator or restrictor of network traffic, which can be configured to enforce your organization's data security policy. If you use a firewall, you will increase security at the operating system level by providing a chokepoint where your security measures can be focused. The following table contains more information about how to use a firewall with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|For information about|See|  
|---------------------------|---------|  
|Configuring a firewall to work with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)|  
|Configuring a firewall to work with [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|[Integration Services Service &#40;SSIS Service&#41;](../../integration-services/service/integration-services-service-ssis-service.md)|  
|Configuring a firewall to work with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|[Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md)|  
|Opening specific ports on a firewall to enable access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)|  
|Configuring support for Extended Protection for Authentication by using channel binding and service binding|[Connect to the Database Engine Using Extended Protection](../../database-engine/configure-windows/connect-to-the-database-engine-using-extended-protection.md)|  
  
 Surface area reduction is a security measure that involves stopping or disabling unused components. Surface area reduction helps improve security by providing fewer avenues for potential attacks on a system. The key to limiting the surface area of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes running required services that have "least privilege" by granting services and users only the appropriate rights. The following table contains more information about services and system access.  
  
|For information about|See|  
|---------------------------|---------|  
|Services required for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)|  
  
 If your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system uses Internet Information Services (IIS), additional steps are required to help secure the surface of the platform. The following table contains information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Internet Information Services.  
  
|For information about|See|  
|---------------------------|---------|  
|IIS security with [!INCLUDE[ssEW](../../includes/ssew-md.md)]|"IIS Security" in [!INCLUDE[ssEW](../../includes/ssew-md.md)] Books Online|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Authentication|[Authentication in Reporting Services](../../reporting-services/extensions/security-extension/authentication-in-reporting-services.md)|  
|[!INCLUDE[ssEW](../../includes/ssew-md.md)] and IIS access|"Internet Information Services Security Flowchart" in [!INCLUDE[ssEW](../../includes/ssew-md.md)] Books Online|  
  
### SQL Server Operating System Files Security  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses operating system files for operation and data storage. Best practices for file security requires that you restrict access to these files. The following table contains information about these files.  
  
|For information about|See|  
|---------------------------|---------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] program files|[File Locations for Default and Named Instances of SQL Server](../../sql-server/install/file-locations-for-default-and-named-instances-of-sql-server.md)|  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades provide enhanced security. To determine the latest available service pack available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the [SQL Server](https://go.microsoft.com/fwlink/?LinkID=31629) Web site.  
  
 You can use the following script to determine the service pack installed on the system.  
  
```  
SELECT CONVERT(char(20), SERVERPROPERTY('productlevel'));  
GO  
```  
  
## Principals and Database Object Security  
 Principals are the individuals, groups, and processes granted access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. "Securables" are the server, database, and objects the database contains. Each has a set of permissions that can be configured to help reduce the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] surface area. The following table contains information about principals and securables.  
  
|For information about|See|  
|---------------------------|---------|  
|Server and database users, roles, and processes|[Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)|  
|Server and database objects security|[Securables](../../relational-databases/security/securables.md)|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security hierarchy|[Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)|  
  
### Encryption and Certificates  
 Encryption does not solve access control problems. However, it enhances security by limiting data loss even in the rare occurrence that access controls are bypassed. For example, if the database host computer is misconfigured and a malicious user obtains sensitive data, such as credit card numbers, that stolen information might be useless if it is encrypted. The following table contains more information about encryption in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|For information about|See|  
|---------------------------|---------|  
|The encryption hierarchy in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)|  
|Implementing secure connections|[Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)|  
|Encryption functions|[Cryptographic Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cryptographic-functions-transact-sql.md)|  
  
 Certificates are software "keys" shared between two servers that enable secure communications by way of strong authentication. You can create and use certificates in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to enhance object and connection security. The following table contains information about how to use certificates with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|For information about|See|  
|---------------------------|---------|  
|Creating a certificate for use by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)|  
|Using a certificate with database mirroring|[Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)|  
  
## Application Security  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security best practices include writing secure client applications.  
  
 For more information about how to help secure client applications at the networking layer, see [Client Network Configuration](../../database-engine/configure-windows/client-network-configuration.md).  
  
## SQL Server Security Tools, Utilities, Views, and Functions  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides tools, utilities, views, and functions that can be used to configure and administer security.  
  
### SQL Server Security Tools and Utilities  
 The following table contains information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools and utilities that you can use to configure and administer security.  
  
|For information about|See|  
|---------------------------|---------|  
|Connecting to, configuring, and controlling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[Use SQL Server Management Studio](https://msdn.microsoft.com/library/f289e978-14ca-46ef-9e61-e1fe5fd593be)|  
|Connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and running queries at the command prompt|[sqlcmd Utility](../../tools/sqlcmd-utility.md)|  
|Network configuration and control for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)|  
|Enabling and disabling features by using Policy-Based Management|[Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)|  
|Manipulating symmetric keys for a report server|[rskeymgmt Utility &#40;SSRS&#41;](../../reporting-services/tools/rskeymgmt-utility-ssrs.md)|  
  
### SQL Server Security Catalog Views and Functions  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] exposes security information in several views and functions that are optimized for performance and utility. The following table contains information about security views and functions.  
  
|For information about|See|  
|---------------------------|---------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security catalog views, which return information about database-level and server-level permissions, principals, roles, and so on. In addition, there are catalog views that provide information about encryption keys, certificates, and credentials.|[Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security functions, which return information about the current user, permissions and schemas.|[Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security dynamic management views.|[Security-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/security-related-dynamic-management-views-and-functions-transact-sql.md)|  
  
## Related Content  
 [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
 [Security Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)  
[SQL Server 2012 Security Best Practices - Operational and Administrative Tasks](https://download.microsoft.com/download/8/F/A/8FABACD7-803E-40FC-ADF8-355E7D218F4C/SQL_Server_2012_Security_Best_Practice_Whitepaper_Apr2012.docx)   
[SQL Server Security Blog](https://blogs.msdn.microsoft.com/sqlsecurity/)  
[Security Best Practice and Label Security Whitepapers](https://blogs.msdn.microsoft.com/sqlsecurity/2012/03/06/security-best-practice-and-label-security-whitepapers/)  
[Row-Level Security](../../relational-databases/security/row-level-security.md)   
[Protecting Your SQL Server Intellectual Property](../../relational-databases/security/protecting-your-sql-server-intellectual-property.md)   
  
