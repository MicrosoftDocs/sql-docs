---
description: "Security Overview (Integration Services)"
title: "Security Overview (Integration Services) | Microsoft Docs"
ms.custom: security
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SSIS packages, security"
  - "Integration Services, security"
  - "security [Integration Services], about security"
  - "passwords [Integration Services]"
  - "packages [Integration Services], security"
  - "SQL Server Integration Services, security"
  - "SSIS, security"
  - "Integration Services packages, security"
  - "SQL Server Integration Services packages, security"
ms.assetid: 01aa0b88-d477-4581-9a3b-2efc3de2b133
author: chugugrace
ms.author: chugu
---
# Security Overview (Integration Services)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Security in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] consists of several layers that provide a rich and flexible security environment. These security layers include the use of digital signatures, package properties, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database roles, and operating system permissions. Most of these security features fall into the categories of identity and access control.  

## Threat and Vulnerability Mitigation
  Although [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a variety of security mechanisms, packages and the files that packages create or use could be exploited for malicious purposes.  
  
 The following table describes these risks and the proactive steps that you can take to lessen the risks.  
  
|Threat or vulnerability|Definition|Mitigation|  
|-----------------------------|----------------|----------------|  
|Package source|The source of a package is the individual or organization that created the package. Running a package from an unknown or untrusted source might be risky.|Identify the source of a package by using a digital signature, and run packages that come from only known, trusted sources. For more information, see [Identify the Source of Packages with Digital Signatures](../../integration-services/security/identify-the-source-of-packages-with-digital-signatures.md).|  
|Package contents|Package contents include the elements in the package and their properties. The properties can contain sensitive data such as a password or a connection string. Package elements such as an SQL statement can reveal the structure of your database.|Control access to a package and to the contents by doing the following steps:<br /><br /> 1) To control access to the package itself, apply [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security features to packages that are saved to the **msdb** database in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To packages that are saved in the file system, apply file system security features, such as access controls lists (ACLs).<br /><br /> 2) To control access to the package's contents, set the protection level of the package.<br /><br /> For more information, see [Security Overview \(Integration Services\)](../../integration-services/security/security-overview-integration-services.md) and [Access Control for Sensitive Data in Packages](../../integration-services/security/access-control-for-sensitive-data-in-packages.md).|  
|Package output|When you configure a package to use configurations, checkpoints, and logging, the package stores this information outside the package. The information that is stored outside the package might contain sensitive data.|To protect configurations and logs that the package saves to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database tables, use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security features.<br /><br /> To control access to files, use the access control lists (ACLs) available in the file system.<br /><br /> For more information, see [Access to Files Used by Packages](#files)|  
  
## Identity Features  
 By implementing identity features in your packages, you can achieve the following goal:  
  
 **Ensure that you only open and run packages from trusted sources**.  
  
 To ensure that you only open and run packages from trusted sources, you first have to identify the source of packages. You can identify the source by signing packages with certificates. Then, when you open or run the packages, you can have [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] check for the presence and the validity of the digital signatures. For more information, see [Identify the Source of Packages with Digital Signatures](../../integration-services/security/identify-the-source-of-packages-with-digital-signatures.md).  
  
## Access Control Features  
 By implementing identity features in your packages, you can achieve the following goal:  
  
 **Ensure that only authorized users open and run packages**.  
  
 To ensure that only authorized users open and run packages, you have to control access to the following information:  
  
-   Control access to the contents of packages, especially sensitive data.  
  
-   Control access to packages and package configurations that are stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Control access to packages and to related files such as configurations, logs, and checkpoint files that are stored in the file system.  
  
-   Control access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service and to the information about packages that the service displays in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
### Controlling Access to the Contents of Packages  
 To help restrict access to the contents of a package, you can encrypt packages by setting the ProtectionLevel property of the package. You can set this property to the level of protection that your package requires. For example, in a team development environment, you can encrypt a package by using a password that is known only to the team members who work on the package.  
  
 When you set the ProtectionLevel property of a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] automatically detects sensitive properties and handles these properties according to the specified package protection level. For example, you set the ProtectionLevel property for a package to a level that encrypts sensitive information with a password. For this package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] automatically encrypts the values of all sensitive properties and will not display the corresponding data without the correct password being supplied.  
  
 Typically, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] identifies properties as sensitive if those properties contain information, such as a password or a connection string, or if those properties correspond to variables or task-generated XML nodes. Whether [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] considers a property sensitive depends on whether the developer of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component, such as a connection manager or task, has designated the property as sensitive. Users cannot add properties to, nor can they remove properties from, the list of properties that are considered sensitive.If you write custom tasks, connection managers, or data flow components, you can specify which properties [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] should treat as sensitive.  
  
 For more information, see [Access Control for Sensitive Data in Packages](../../integration-services/security/access-control-for-sensitive-data-in-packages.md).  
  
### Controlling Access to Packages  
 You can save [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages to the msdb database in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or to the file system as XML files that have the .dtsx file name extension. For more information, see [Save Packages](../../integration-services/save-packages.md).  
  
#### Saving Packages to the msdb Database  
 Saving the packages to the msdb database helps provide security at the server, database, and table levels. In the msdb database, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages are stored in the sysssispackages table. Because the packages are saved to the sysssispackages and sysdtspackages tables in the msdb database, the packages are automatically backed up when you backup the msdb database.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] packages stored in the msdb database can also be protected by applying the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] database-level roles. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes three fixed database-level roles db_ssisadmin, db_ssisltduser, and db_ssisoperator for controlling access to packages. A reader and a writer role can be associated with each package. You can also define custom database-level roles to use in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. Roles can be implemented only on packages that are saved to the msdb database in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Integration Services Roles \(SSIS Service\)](../../integration-services/security/integration-services-roles-ssis-service.md).  
  
#### Saving Packages to the File System  
 If you store packages to the file system instead of in the msdb database, make sure to secure the package files and the folders that contain package files.  
  
### Controlling Access to Files Used by Packages  
 Packages that have been configured to use configurations, checkpoints, and logging generate information that is stored outside the package. This information might be sensitive and should be protected. Checkpoint files can be saved only to the file system, but configurations and logs can be saved to the file system or to tables in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Configurations and logs that are saved to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are subject to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security, but information written to the file system requires additional security.  
  
 For more information, see [Access to Files Used by Packages](#files).  
  
#### Storing Package Configurations Securely  
 Package configurations can be saved to a table in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database or to the file system.  
  
 Configurations can be saved to any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, not just the msdb database. Thus, you are able to specify which database serves as the repository of package configurations. You can also specify the name of the table that will contain the configurations, and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] automatically creates the table with the correct structure. Saving the configurations to a table makes it possible to provide security at the server, database, and table levels. In addition, configurations that are saved to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are automatically backed up when you back up the database.  
  
 If you store configurations in the file system instead of in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure to secure the folders that contain the package configuration files.  
  
 For more information about configurations, see [Package Configurations](../packages/legacy-package-deployment-ssis.md).  
  
### Controlling Access to the Integration Services Service  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service to list stored packages. To prevent unauthorized users from viewing information about packages that are stored on local and remote computers, and thereby learning private information, restrict access to computers that run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
 For more information, see [Access to the Integration Services Service](#service).  

## <a name="files"></a> Access to Files Used by Packages
  The package protection level does not protect files that are stored outside the package. These files include the following:  
  
-   Configuration files  
  
-   Checkpoint files  
  
-   Log files  
  
 These files must be protected separately, especially if they include sensitive information.  
  
### Configuration Files  
 If you have sensitive information in a configuration, such as login and password information, you should consider saving the configuration to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or use an access control list (ACL) to restrict access to the location or folder where you store the files and allow access only to certain accounts. Typically, you would grant access to the accounts that you permit to run packages, and to the accounts that manage and troubleshoot packages, which may include reviewing the contents of configuration, checkpoint, and log files. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the more secure storage because it offers protection at the server and database levels. To save configurations to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configuration type. To save to the file system, you use the XML configuration type.  
  
 For more information, see [Package Configurations](../packages/legacy-package-deployment-ssis.md), [Create Package Configurations](../packages/legacy-package-deployment-ssis.md), and [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md).  
  
### Checkpoint Files  
 Similarly, if the checkpoint file that the package uses includes sensitive information, you should use an access control list (ACL) to secure the location or folder where you store the file. Checkpoint files save current state information on the progress of the package as well as the current values of variables. For example, the package may include a custom variable that contains a telephone number. For more information, see [Restart Packages by Using Checkpoints](../../integration-services/packages/restart-packages-by-using-checkpoints.md).  
  
### Log Files  
 Log entries that are written to the file system should also be secured using an access control list (ACL). Log entries can also be stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables and protected by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security. Log entries may include sensitive information, For example, if the package contains an Execute SQL task that constructs an SQL statement that refers to a telephone number, the log entry for the SQL statement includes the telephone number. The SQL statement may also reveal private information about table and column names in databases. For more information, see [Integration Services \(SSIS\) Logging](../../integration-services/performance/integration-services-ssis-logging.md).  

## <a name="service"></a> Access to the Integration Services Service
  Package protection levels can limit who is allowed to edit and execute a package. Additional protection is needed to limit who can view the list of packages currently running on a server and who can stop currently executing packages in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service to list running packages. Members of the Windows Administrators group can view and stop all currently running packages. Users who are not members of the Administrators group can view and stop only packages that they started.  
  
 It is important to restrict access to computers that run an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service, especially an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service that can enumerate remote folders. Any authenticated user can request the enumeration of packages. Even if the service does not find the service, the service enumerates folders. These folder names may be useful to a malicious user. If an administrator has configured the service to enumerate folders on a remote machine, users may also be able to see folder names that they would normally not be able to see.  

## Related Tasks  
 The following list contains links to topics that show you how to perform a certain task pertaining to the security.  
  
-   [Create a User-Defined Role](../../integration-services/security/integration-services-roles-ssis-service.md#create)  
  
-   [Assign a Reader and Writer Role to a Package](../../integration-services/security/integration-services-roles-ssis-service.md#assign)  
  
-   [Implement a Signing Policy by Setting a Registry Value](../../integration-services/security/identify-the-source-of-packages-with-digital-signatures.md#registry)  
  
-   [Sign a Package by Using a Digital Certificate](../../integration-services/security/identify-the-source-of-packages-with-digital-signatures.md#cert)  
  
-   [Set or Change the Protection Level of Packages](../../integration-services/security/access-control-for-sensitive-data-in-packages.md#set_protection)
