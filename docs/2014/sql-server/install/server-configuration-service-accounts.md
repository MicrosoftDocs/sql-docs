---
title: "Server Configuration - Service Accounts | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "service account configuration, SQL Server"
ms.assetid: c283702d-ab20-4bfa-9272-f0c53c31cb9f
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Server Configuration - Service Accounts
  Use the Server Configuration page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to assign login accounts to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services configured on this page depend on the features you have selected to install.  
  
 Startup accounts used to start and run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be  HYPERLINK "ms-help://SQL11_I1033/s11sq_GetStart_I/html/309b9dac-0b3a-4617-85ef-c4519ce9d014.htm" \l "Domain_User" domain user accounts, local user accounts, managed service accounts, virtual accounts, or built-in system accounts.  
  
## Options  
 You can assign the same login account to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, are started manually, or are disabled. The default account is recommended for most installations.  
  
 On Windows 7 and [!INCLUDE[nextref_longhorn](../../includes/nextref-longhorn-md.md)] R2 most accounts default to a virtual account.  
  
 If you configure services to use domain accounts, [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you configure service accounts individually to provide least privileges for each service, where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they need to complete their tasks. For more information including descriptions of the types of accounts, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
 **Configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service accounts individually (recommended)**  
 Use the grid to provision each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service with a logon user name and password, and to set the startup type for the service. You can use built-in system accounts, a local account, local group, domain group, or domain user accounts for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services.  
  
 Select any of the following services to customize its settings.  
  
|Select this service|To configure authentication settings for|  
|-------------------------|----------------------------------------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent|The service that executes jobs, monitors, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and allows automation of administrative tasks.<br /><br /> There is no default logon account for this service.<br /><br /> The default startup type is Manual.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|The default startup type is Automatic.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|The default startup type is Automatic.<br /><br /> For SharePoint integrated mode, you must specify a Windows domain user account. The account you specify is used for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service. The account you specify for the current instance must also be used for any additional [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that you subsequently add to the same farm.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|Service accounts are used to configure a report server database connection. Choose the built-in network service if you want to use default authentication settings. If you specify a domain user account, be sure to register a service principle name (SPN) for it if you are using Windows Authentication on the report server. For more information, see [Configure Windows Authentication on the Report Server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md).<br /><br /> The default startup type is Automatic.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is a set of graphical tools and programmable objects for moving, copying, and transforming data.<br /><br /> The default startup type is Automatic.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Client|The service account used for the Distributed Replay client service.<br /><br /> Provide an account in which to run the Distributed Replay client service. This account should be different from the account that you use for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.<br /><br /> The default startup type is Manual.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Controller|The service account used for the Distributed Replay controller service.<br /><br /> Provide an account in which to run the Distributed Replay controller service. This account should be different from the account that you use for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.<br /><br /> The default startup type is Manual.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Full-text Filter Daemon Launcher|The service that creates the fdhost.exe processes. This is required to host the word breakers and filters that process textual data for full-text indexing.<br /><br /> If you provide a domain account in which to run the FDHOST Launcher service, we highly recommend that you use a low privilege account. This account should be different from the account that you use for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser is the name resolution service that provides [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection information to client computers. This service is shared across multiple [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] instances. The default logon account is NT Authority\Local service and cannot be changed during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup. You can change the account after the setup has been completed. If the startup type is not specified during setup, it is determined as follows:<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser is set to Automatic and running in the installation scenarios described below:<br />-<br />                            [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance<br />-<br />                            Named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where TCP or NP is enabled<br />-<br />                            Named instance of Analysis Server and is not clustered<br /><br /> If none of the above scenarios apply, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser is already installed, the current state of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser will be maintained.<br /><br /> The startup type is set to Disabled and stopped if there is not an existing instance of an older [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version prior to the installation.|  
  
## See Also  
 [Security Considerations for a SQL Server Installation](../../../2014/sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
  
