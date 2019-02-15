---
title: "Configure Windows Service Accounts and Permissions | Microsoft Docs"
ms.custom: ""
ms.date: "05/08/2018"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "startup service states [SQL Server]"
  - "Setup [SQL Server], user accounts"
  - "Windows permissions [SQL Server]"
  - "modifying user accounts"
  - "default accounts"
  - "domains [SQL Server], user accounts"
  - "startup accounts [SQL Server]"
  - "system accounts [SQL Server]"
  - "services [SQL Server], permissions"
  - "ACL (access control list)"
  - "local system accounts [SQL Server]"
  - "instance-aware services [SQL Server]"
  - "permissions [SQL Server], services"
  - "SQL Server Agent service, user accounts"
  - "Windows NT permissions [SQL Server]"
  - "user accounts [SQL Server]"
  - "identifying instance-unaware services [SQL Server]"
  - "installing SQL Server, user accounts"
  - "disabled startup state [SQL Server]"
  - "user accounts [SQL Server], users"
  - "Local Service account [SQL Server]"
  - "SQL Server Installation Wizard"
  - "instance-unaware services [SQL Server]"
  - "services [SQL Server], configuring at installation"
  - "Windows accounts [SQL Server]"
  - "SQL Server services, user accounts"
  - "user accounts [SQL Server], services"
  - "MSSQLServer"
  - "identifying instance-aware services [SQL Server]"
  - "services [SQL Server], accounts"
  - "access control lists"
  - "optional accounts [SQL Server]"
  - "service accounts [SQL Server]"
  - "accounts [SQL Server], services"
  - "built-in system accounts [SQL Server]"
  - "automatic startup state"
  - "domains [SQL Server]"
  - "manual startup state [SQL Server]"
  - "accounts [SQL Server], user"
ms.assetid: 309b9dac-0b3a-4617-85ef-c4519ce9d014
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure Windows Service Accounts and Permissions
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

  Each service in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] represents a process or a set of processes to manage authentication of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] operations with Windows. This topic describes the default configuration of services in this release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and configuration options for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services that you can set during and after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. This topic helps advanced users understand the details of the service accounts.  
  
 Most services and their properties can be configured by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. Here are the paths to the last four versions when Windows is installed on the C drive.  
  
|||  
|-|-|  
|SQL Server 2017|C:\Windows\SysWOW64\SQLServerManager14.msc| 
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016|C:\Windows\SysWOW64\SQLServerManager13.msc|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|C:\Windows\SysWOW64\SQLServerManager12.msc|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|C:\Windows\SysWOW64\SQLServerManager11.msc|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|C:\Windows\SysWOW64\SQLServerManager10.msc|  
  

##  <a name="Service_Details"></a> Services Installed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 Depending on the components that you decide to install, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the following services:  
  
-   **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Services** - The service for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The executable file is \<MSSQLPATH>\MSSQL\Binn\sqlservr.exe.  
  
-   **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent** - Executes jobs, monitors [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], fires alerts, and enables automation of some administrative tasks. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is present but disabled on instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. The executable file is \<MSSQLPATH>\MSSQL\Binn\sqlagent.exe.  
  
-   **[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]**  - Provides online analytical processing (OLAP) and data mining functionality for business intelligence applications. The executable file is \<MSSQLPATH>\OLAP\Bin\msmdsrv.exe.  
  
-   **[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]**  - Manages, executes, creates, schedules, and delivers reports. The executable file is \<MSSQLPATH>\Reporting Services\ReportServer\Bin\ReportingServicesService.exe.  
  
-   **[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]**  - Provides management support for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package storage and execution. The executable path is \<MSSQLPATH>\130\DTS\Binn\MsDtsSrvr.exe  
  
-   **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser** - The name resolution service that provides [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection information for client computers. The executable path is c:\Program Files (x86)\Microsoft SQL Server\90\Shared\sqlbrowser.exe  
  
-   **Full-text search** - Quickly creates full-text indexes on content and properties of structured and semistructured data to provide document filtering and word-breaking for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **SQL Writer** - Allows backup and restore applications to operate in the Volume Shadow Copy Service (VSS) framework.
  
-   **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Controller** - Provides trace replay orchestration across multiple Distributed Replay client computers.  
  
-   **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Client** - One or more Distributed Replay client computers that work together with a Distributed Replay controller to simulate concurrent workloads against an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
-   **[!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)]**  - A trusted service that hosts external executables that are provided by Microsoft, such as the R runtime installed as part of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. Satellite processes can be launched by the Launchpad process but will be resource governed based on the configuration of the individual instance. The Launchpad service runs under its own user account, and each satellite process for a specific, registered runtime will inherit the user account of the Launchpad. Satellite processes are created and destroyed on demand during execution time.

    Launchpad cannot create the accounts it uses if you install SQL Server on a computer that is also used as a domain controller. Hence, setup of R Services (In-Database) or Machine Learning Services (In-Database) fails on a domain controller.

  
 - **SQL Server PolyBase Engine** - Provides distributed query capabilities to external data sources.
 
 - **SQL Server PolyBase Data Movement Service** - Enables data movement between SQL Server and External Data Sources and between SQL nodes in PolyBase Scaleout Groups.
  
##  <a name="Serv_Prop"></a> Service Properties and Configuration

Startup accounts used to start and run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be [domain user accounts](#Domain_User), [local user accounts](#Local_User), [managed service accounts](#MSA), [virtual accounts](#VA_Desc), or [built-in system accounts](#Local_Service). To start and run, each service in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must have a startup account configured during installation.
  
 This section describes the accounts that can be configured to start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, the default values used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, the concept of per-service SID's, the startup options, and configuring the firewall.  
  
-   [Default Service Accounts](#Default_Accts)  
  
-   [Automatic Startup](#Auto_Start)  
  
-   [Configuring Service StartupType](#Configure_services)  
  
-   [Firewall Port](#Firewall)  
  
###  <a name="Default_Accts"></a> Default Service Accounts

The following table lists the default service accounts used by setup when installing all components. The default accounts listed are the recommended accounts, except as noted.
  
 **Stand-alone Server or Domain Controller**
  
|Component|[!INCLUDE[nextref_longhorn](../../includes/nextref-longhorn-md.md)]|Windows 7 and [!INCLUDE[nextref_longhorn](../../includes/nextref-longhorn-md.md)] R2 and higher|  
|---------------|------------------------------------|----------------------------------------------------------------|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)]|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)*|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)*|  
|[!INCLUDE[ssAS](../../includes/ssas-md.md)]|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)\* \*\*|  
|[!INCLUDE[ssIS](../../includes/ssis-md.md)]|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)\*|  
|[!INCLUDE[ssRS](../../includes/ssrs.md)]|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)\*|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Controller|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)\*|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Client|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)\*|  
|FD Launcher (Full-text Search)|[LOCAL SERVICE](#Local_Service)|[Virtual Account](#VA_Desc)|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser|[LOCAL SERVICE](#Local_Service)|[LOCAL SERVICE](#Local_Service)|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] VSS Writer|[LOCAL SYSTEM](#Local_System)|[LOCAL SYSTEM](#Local_System)|  
|[!INCLUDE[rsql_extensions](../../includes/rsql-extensions-md.md)]|NTSERVICE\MSSQLLaunchpad|NTSERVICE\MSSQLLaunchpad|  
|PolyBase Engine  |[NETWORK SERVICE](#Network_Service) |[NETWORK SERVICE](#Network_Service)  |
|PolyBase Data Movement Service |[NETWORK SERVICE](#Network_Service) |[NETWORK SERVICE](#Network_Service)  |
  
 \*When resources external to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer are needed, [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends using a Managed Service Account (MSA), configured with the minimum privileges necessary.   
 \*\* When installed on a Domain Controller, a virtual account as the service account is not supported.
  
 **SQL Server Failover Cluster Instance**
  
|Component|[!INCLUDE[nextref_longhorn](../../includes/nextref-longhorn-md.md)]|[!INCLUDE[nextref_longhorn](../../includes/nextref-longhorn-md.md)] R2|  
|---------------|------------------------------------|---------------------------------------|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)]|None. Provide a [domain user](#Domain_User) account.|Provide a [domain user](#Domain_User) account.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent|None. Provide a [domain user](#Domain_User) account.|Provide a [domain user](#Domain_User) account.|  
|[!INCLUDE[ssAS](../../includes/ssas-md.md)]|None. Provide a [domain user](#Domain_User) account.|Provide a [domain user](#Domain_User) account.|  
|[!INCLUDE[ssIS](../../includes/ssis-md.md)]|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)|  
|[!INCLUDE[ssRS](../../includes/ssrs.md)]|[NETWORK SERVICE](#Network_Service)|[Virtual Account](#VA_Desc)|  
|FD Launcher (Full-text Search)|[LOCAL SERVICE](#Local_Service)|[Virtual Account](#VA_Desc)|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser|[LOCAL SERVICE](#Local_Service)|[LOCAL SERVICE](#Local_Service)|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] VSS Writer|[LOCAL SYSTEM](#Local_System)|[LOCAL SYSTEM](#Local_System)|  

####  <a name="Changing_Accounts"></a> Changing Account Properties
  
> [!IMPORTANT]
>  -   Always use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to change the account used by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent services, or to change the password for the account. In addition to changing the account name, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager performs additional configuration such as updating the Windows local security store which protects the service master key for the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Other tools such as the Windows Services Control Manager can change the account name but do not change all the required settings.  
> -   For [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that you deploy in a SharePoint farm, always use SharePoint Central Administration to change the server accounts for [!INCLUDE[ssGeminiMTS](../../includes/ssgeminimts-md.md)] applications and the [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)]. Associated settings and permissions are updated to use the new account information when you use Central Administration.  
> -   To change [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] options, use the Reporting Services Configuration Tool.  
  
###  <a name="New_Accounts"></a> Managed Service Accounts, Group Managed Service Accounts, and Virtual Accounts  
  
Managed service accounts, group managed service accounts, and virtual accounts are designed to provide crucial applications such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the isolation of their own accounts, while eliminating the need for an administrator to manually administer the Service Principal Name (SPN) and credentials for these accounts. These make long term management of service account users, passwords and SPNs much easier.  
  
-   <a name="MSA"></a> **Managed Service Accounts**  
  
     A Managed Service Account (MSA) is a type of domain account created and managed by the domain controller. It is assigned to a single member computer for use running a service. The password is managed automatically by the domain controller. You cannot use a MSA to log into a computer, but a computer can use a MSA to start a Windows service. An MSA has the ability to register Service Principal Name (SPN) with the Active Directory. A MSA is named with a **$** suffix, for example **DOMAIN\ACCOUNTNAME$**. When specifying a MSA, leave the password blank. Because a MSA is assigned to a single computer, it cannot be used on different nodes of a Windows cluster.  
  
    > [!NOTE]  
    >  The MSA must be created in the Active Directory by the domain administrator before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup can use it for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services.  
  
-  <a name="GMSA"></a> **Group Managed Service Accounts**  
  
     A Group Managed Service Account is an MSA for multiple servers. Windows manages a service account for services running on a group of servers. Active Directory automatically updates the group managed service account password without restarting services. You can configure SQL Server services to use a group managed service account principal. Beginning with SQL Server 2014, SQL Server supports group managed service accounts for standalone instances, and SQL Server 2016 and later for failover cluster instances, and availability groups.  
  
    To use a group managed service account for SQL Server 2014 or later, the operating system must be Windows Server 2012 R2 or later. Servers with Windows Server 2012 R2 require [KB 2998082](https://support.microsoft.com/kb/2998082) applied so that the services can log in without disruption immediately after a password change.  
  
    For more information, see [Group Managed Service Accounts](https://technet.microsoft.com/library/hh831782.aspx)  
      
    > [!NOTE]  
    >  The group managed service account must be created in the Active Directory by the domain administrator before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup can use it for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services.  
  
-   <a name="VA_Desc"></a>**Virtual Accounts**  
  
    Virtual accounts (beginning with Windows Server 2008 R2 and Windows 7) are *managed local accounts* that provide the following features to simplify service administration. The virtual account is auto-managed, and the virtual account can access the network in a domain environment. If the default value is used for the service accounts during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup, a virtual account using the instance name as the service name is used, in the format **NT SERVICE\\**_\<SERVICENAME>_. Services that run as virtual accounts access network resources by using the credentials of the computer account in the format *<domain_name>*__\\__*<computer_name>*__$__.  When specifying a virtual account to start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], leave the password blank. If the virtual account fails to register the Service Principal Name (SPN), register the SPN manually. For more information on registering a SPN manually, see [Manual SPN Registration](register-a-service-principal-name-for-kerberos-connections.md).  
  
    > [!NOTE]  
    >  Virtual accounts cannot be used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Instance, because the virtual account would not have the same SID on each node of the cluster.  
  
     The following table lists examples of virtual account names.  
  
    |Service|Virtual Account Name|  
    |-------------|--------------------------|  
    |Default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service|**NT SERVICE\MSSQLSERVER**|  
    |Named instance of a [!INCLUDE[ssDE](../../includes/ssde-md.md)] service named **PAYROLL**|**NT SERVICE\MSSQL$PAYROLL**|  
    |[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|**NT SERVICE\SQLSERVERAGENT**|  
    |[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] named **PAYROLL**|**NT SERVICE\SQLAGENT$PAYROLL**|  
  
 For more information on Managed Service Accounts and Virtual Accounts, see the **Managed service account and virtual account concepts** section of [Service Accounts Step-by-Step Guide](https://technet.microsoft.com/library/dd548356\(WS.10\).aspx) and [Managed Service Accounts Frequently Asked Questions (FAQ)](https://technet.microsoft.com/library/ff641729\(WS.10\).aspx).  
  
 **Security Note:** [!INCLUDE[ssNoteLowRights](../../includes/ssnotelowrights-md.md)] Use a [MSA](#MSA) or [virtual account](#VA_Desc) when possible. When MSA and virtual accounts are not possible, use a specific low-privilege user account or domain account instead of a shared account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services. Use separate accounts for different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services. Do not grant additional permissions to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account or the service groups. Permissions will be granted through group membership or granted directly to a service SID, where a service SID is supported.  
  
###  <a name="Auto_Start"></a> Automatic startup

In addition to having user accounts, every service has three possible startup states that users can control:
  
-   **Disabled** The service is installed but not currently running.  
  
-   **Manual** The service is installed, but will start only when another service or application needs its functionality.  
  
-   **Automatic** The service is automatically started by the operating system.  
  
 The startup state is selected during setup. When installing a named instance, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service should be set to start automatically.  
  
###  <a name="Configure_services"></a> Configuring services during unattended installation

The following table shows the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services that can be configured during installation. For unattended installations, you can use the switches in a configuration file or at a command prompt.  
  
|SQL Server service name|Switches for unattended installations\*|  
|-----------------------------|---------------------------------------------|  
|MSSQLSERVER|SQLSVCACCOUNT, SQLSVCPASSWORD, SQLSVCSTARTUPTYPE|  
|SQLServerAgent\*\*|AGTSVCACCOUNT, AGTSVCPASSWORD, AGTSVCSTARTUPTYPE|  
|MSSQLServerOLAPService|ASSVCACCOUNT, ASSVCPASSWORD, ASSVCSTARTUPTYPE|  
|ReportServer|RSSVCACCOUNT, RSSVCPASSWORD, RSSVCSTARTUPTYPE|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|ISSVCACCOUNT, ISSVCPASSWORD, ISSVCSTARTUPTYPE|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Controller|DRU_CTLR, CTLRSVCACCOUNT,CTLRSVCPASSWORD, CTLRSTARTUPTYPE, CTLRUSERS|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Client|DRU_CLT, CLTSVCACCOUNT, CLTSVCPASSWORD, CLTSTARTUPTYPE, CLTCTLRNAME, CLTWORKINGDIR, CLTRESULTDIR|  
|[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]|EXTSVCACCOUNT, EXTSVCPASSWORD, ADVANCEDANALYTICS\*\*\*|
|PolyBase Engine| PBENGSVCACCOUNT, PBENGSVCPASSWORD, PBENGSVCSTARTUPTYPE, PBDMSSVCACCOUNT,PBDMSSVCPASSWORD, PBDMSSVCSTARTUPTYPE, PBSCALEOUT, PBPORTRANGE
  
 \*For more information and sample syntax for unattended installations, see [Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md).  
  
 \*\*The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is disabled on instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] and [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] with Advanced Services.

 \*\*\*Setting the account for Launchpad through the switches alone is not currently supported. Use SQL Server Configuration Manager to change the account and other service settings.

###  <a name="Firewall"></a> Firewall Port

In most cases, when initially installed, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] can be connected to by tools such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] installed on the same computer as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup does not open ports in the Windows firewall. Connections from other computers may not be possible until the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is configured to listen on a TCP port, and the appropriate port is opened for connections in the Windows firewall. For more information, see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
##  <a name="Serv_Perm"></a> Service Permissions

This section describes the permissions that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup configures for the per-service SID's of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services.  
  
-   [Service Configuration and Access Control](#Serv_SID)  
  
-   [Windows Privileges and Rights](#Windows)  
  
-   [File System Permissions Granted to SQL Server Per-service SIDs or SQL Server Local Windows Groups](#Reviewing_ACLs)  
  
-   [File System Permissions Granted to Other Windows User Accounts or Groups](#File_System_Other)  
  
-   [File System Permissions Related to Unusual Disk Locations](#Unusual_Locations)  
  
-   [Reviewing Additional Considerations](#Review_additional_considerations)  
  
-   [Registry Permissions](#Registry)  
  
-   [WMI](#WMI)  
  
-   [Named Pipes](#Pipes)  
  
###  <a name="Serv_SID"></a> Service Configuration and Access Control

[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] enables per-service SID for each of its services to provide service isolation and defense in depth. The per-service SID is derived from the service name and is unique to that service. For example, a service SID name for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service might be **NT Service\MSSQL$**_\<InstanceName>_. Service isolation enables access to specific objects without the need to run a high-privilege account or weaken the security protection of the object. By using an access control entry that contains a service SID, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service can restrict access to its resources.
  
> [!NOTE]  
>  On Windows 7 and [!INCLUDE[nextref_longhorn](../../includes/nextref-longhorn-md.md)] R2 (and later) the per-service SID can be the virtual account used by the service.
  
 For most components [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configures the ACL for the per-service account directly, so changing the service account can be done without having to repeat the resource ACL process.
  
 When installing [!INCLUDE[ssAS](../../includes/ssas-md.md)], a per-service SID for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service is created. A local Windows group is created, named in the format **SQLServerMSASUser$**_computer_name_**$**_instance_name*. The per-service SID **NT SERVICE\MSSQLServerOLAPService** is granted membership in the local Windows group, and the local Windows group is granted the appropriate permissions in the ACL. If the account used to start the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service is changed, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager must change some Windows permissions (such as the right to log on as a service), but the permissions assigned to the local Windows group will still be available without any updating, because the per-service SID has not changed. This method allows the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service to be renamed during upgrades.
  
 During [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup creates a local Windows groups for [!INCLUDE[ssAS](../../includes/ssas-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service. For these services, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configures the ACL for the local Windows groups.  
  
 Depending on the service configuration, the service account for a service or service SID is added as a member of the service group during install or upgrade.
  
###  <a name="Windows"></a> Windows Privileges and Rights  
 The account assigned to start a service needs the **Start, stop and pause permission** for the service. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup program automatically assigns this.  First install Remote Server Administration Tools (RSAT). See [Remote Server Administration Tools for Windows 7](https://www.microsoft.com/downloads/en/details.aspx?FamilyID=7d2f6ad7-656b-4313-a005-4e344e43997d).
  
 The following table shows permissions that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup requests for the per-service SIDs or local Windows groups used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components.
  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Service|Permissions granted by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup|
|---------------------------------------|------------------------------------------------------------|
|**[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]:**<br /><br /> (All rights are granted to the per-service SID. Default instance: **NT SERVICE\MSSQLSERVER**. Named instance: **NT SERVICE\MSSQL$**InstanceName.)|**Log on as a service** (SeServiceLogonRight)<br /><br /> **Replace a process-level token** (SeAssignPrimaryTokenPrivilege)<br /><br /> **Bypass traverse checking** (SeChangeNotifyPrivilege)<br /><br /> **Adjust memory quotas for a process** (SeIncreaseQuotaPrivilege)<br /><br /> Permission to start SQL Writer<br /><br /> Permission to read the Event Log service<br /><br /> Permission to read the Remote Procedure Call service|  
|**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent:** \*<br /><br /> (All rights are granted to the per-service SID. Default instance: **NT Service\SQLSERVERAGENT**. Named instance: **NT Service\SQLAGENT$**_InstanceName_.)|**Log on as a service** (SeServiceLogonRight)<br /><br /> **Replace a process-level token** (SeAssignPrimaryTokenPrivilege)<br /><br /> **Bypass traverse checking** (SeChangeNotifyPrivilege)<br /><br /> **Adjust memory quotas for a process** (SeIncreaseQuotaPrivilege)|  
|**[!INCLUDE[ssAS](../../includes/ssas-md.md)]:**<br /><br /> (All rights are granted to a local Windows group. Default instance: **SQLServerMSASUser$**_ComputerName_**$MSSQLSERVER**. Named instance: **SQLServerMSASUser$**_ComputerName_**$**_InstanceName_. [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] instance: **SQLServerMSASUser$**_ComputerName_**$**_PowerPivot_.)|**Log on as a service** (SeServiceLogonRight)<br /><br /> For tabular only:<br /><br /> **Increase a process working set** (SeIncreaseWorkingSetPrivilege)<br /><br /> **Adjust memory quotas for a process** (SeIncreaseQuotaPrivilege)<br /><br /> **Lock pages in memory** (SeLockMemoryPrivilege) - this is needed only when paging is turned off entirely.<br /><br /> For failover cluster installations only:<br /><br /> **Increase scheduling priority** (SeIncreaseBasePriorityPrivilege)|  
|**[!INCLUDE[ssRS](../../includes/ssrs.md)]:**<br /><br /> (All rights are granted to the per-service SID. Default instance: **NT SERVICE\ReportServer**. Named instance: **NT SERVICE\\ReportServer$**_InstanceName_.)|**Log on as a service** (SeServiceLogonRight)|  
|**[!INCLUDE[ssIS](../../includes/ssis-md.md)]:**<br /><br /> (All rights are granted to the per-service SID. Default instance and named instance: **NT SERVICE\MsDtsServer130**. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] does not have a separate process for a named instance.)|**Log on as a service** (SeServiceLogonRight)<br /><br /> Permission to write to application event log.<br /><br /> **Bypass traverse checking** (SeChangeNotifyPrivilege)<br /><br /> **Impersonate a client after authentication** (SeImpersonatePrivilege)|  
|**Full-text search:**<br /><br /> (All rights are granted to the per-service SID. Default instance: **NT Service\MSSQLFDLauncher**. Named instance: **NT Service\ MSSQLFDLauncher$**_InstanceName_.)|**Log on as a service** (SeServiceLogonRight)<br /><br /> **Adjust memory quotas for a process** (SeIncreaseQuotaPrivilege)<br /><br /> **Bypass traverse checking** (SeChangeNotifyPrivilege)|  
|**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser:**<br /><br /> (All rights are granted to a local Windows group. Default or named instance: **SQLServer2005SQLBrowserUser**_$ComputerName_. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser does not have a separate process for a named instance.)|**Log on as a service** (SeServiceLogonRight)|  
|**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] VSS Writer:**<br /><br /> (All rights are granted to the per-service SID. Default or named instance: **NT Service\SQLWriter**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] VSS Writer does not have a separate process for a named instance.)|The SQLWriter service runs under the LOCAL SYSTEM account which has all the required permissions. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup does not check or grant permissions for this service.| 
  |**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Controller:**|**Log on as a service** (SeServiceLogonRight)|  
|**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Client:**|**Log on as a service** (SeServiceLogonRight)|  
|**PolyBase Engine and DMS**| **Log on as a service** (SeServiceLogonRight)  |   
|**Launchpad:**|**Log on as a service** (SeServiceLogonRight) <br /><br /> **Replace a process-level token** (SeAssignPrimaryTokenPrivilege)<br /><br />**Bypass traverse checking** (SeChangeNotifyPrivilege)<br /><br />**Adjust memory quotas for a process** (SeIncreaseQuotaPrivilege)|     
|**R Services:** **SQLRUserGroup** (SQL 2016 and 2017)  |**Allow Log on locally** |   
|**Machine Learning** '**All Application Packages' [AppContainer]** (SQL 2019)  |**Read and execute permissions** to the SQL Server 'Binn', R_Services, and PYTHON_Services directories |   

 \*The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is disabled on instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].  
  
###  <a name="Reviewing_ACLs"></a> File System Permissions Granted to SQL Server Per-service SIDs or Local Windows Groups  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service accounts must have access to resources. Access control lists are set for the per-service SID or the local Windows group.  
  
> [!IMPORTANT]  
>  For failover cluster installations, resources on shared disks must be set to an ACL for a local account.  
  
 The following table shows the ACLs that are set by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup:  
  
|Service account for|Files and folders|Access|  
|-------------------------|-----------------------|------------|  
|MSSQLServer|Instid\MSSQL\backup|Full control|  
||Instid\MSSQL\binn|Read, Execute|  
||Instid\MSSQL\data|Full control|  
||Instid\MSSQL\FTData|Full control|  
||Instid\MSSQL\Install|Read, Execute|  
||Instid\MSSQL\Log|Full control|  
||Instid\MSSQL\Repldata|Full control|  
||130\shared|Read, Execute|  
||Instid\MSSQL\Template Data ([!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] only)|Read|  
|SQLServerAgent\*|Instid\MSSQL\binn|Full control|  
||Instid\MSSQL\binn|Full control|  
||Instid\MSSQL\Log|Read, Write, Delete, Execute|  
||130\com|Read, Execute|  
||130\shared|Read, Execute|  
||130\shared\Errordumps|Read, Write|  
||ServerName\EventLog|Full control|  
|FTS|Instid\MSSQL\FTData|Full control|  
||Instid\MSSQL\FTRef|Read, Execute|  
||130\shared|Read, Execute|  
||130\shared\Errordumps|Read, Write|  
||Instid\MSSQL\Install|Read, Execute|  
||Instid\MSSQL\jobs|Read, Write|  
|MSSQLServerOLAPservice|130\shared\ASConfig|Full control|  
||Instid\OLAP|Read, Execute|  
||Instid\Olap\Data|Full control|  
||Instid\Olap\Log|Read, Write|  
||Instid\OLAP\Backup|Read, Write|  
||Instid\OLAP\Temp|Read, Write|  
||130\shared\Errordumps|Read, Write|  
|SQLServerReportServerUser|Instid\Reporting Services\Log Files|Read, Write, Delete|  
||Instid\Reporting Services\ReportServer|Read, Execute|  
||Instid\Reportingservices\Reportserver\global.asax|Full control|  
||Instid\Reportingservices\Reportserver\Reportserver.config|Read|  
||Instid\Reporting Services\reportManager|Read, Execute|  
||Instid\Reporting Services\RSTempfiles|Read, Write, Execute, Delete|  
||130\shared|Read, Execute|  
||130\shared\Errordumps|Read, Write|  
|MSDTSServer100|130\dts\binn\MsDtsSrvr.ini.xml|Read|  
||130\dts\binn|Read, Execute|  
||130\shared|Read, Execute|  
||130\shared\Errordumps|Read, Write|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser|130\shared\ASConfig|Read|  
||130\shared|Read, Execute|  
||130\shared\Errordumps|Read, Write|  
|SQLWriter|N/A (Runs as local system)||  
|User|Instid\MSSQL\binn|Read, Execute|  
||Instid\Reporting Services\ReportServer|Read, Execute, List Folder Contents|  
||Instid\Reportingservices\Reportserver\global.asax|Read|  
||Instid\Reporting Services\ReportManager|Read, Execute|  
||Instid\Reporting Services\ReportManager\pages|Read|  
||Instid\Reporting Services\ReportManager\Styles|Read|  
||130\dts|Read, Execute|  
||130\tools|Read, Execute|  
||100\tools|Read, Execute|  
||90\tools|Read, Execute|  
||80\tools|Read, Execute|  
||130\sdk|Read|  
||Microsoft SQL Server\130\Setup Bootstrap|Read, Execute|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Controller|\<ToolsDir>\DReplayController\Log\ (empty directory)|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayController\DReplayController.exe|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayController\resources\|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayController\\{all dlls}|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayController\DReplayController.config|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayController\IRTemplate.tdf|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayController\IRDefinition.xml|Read, Execute, List Folder Contents|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay Client|\<ToolsDir>\DReplayClient\Log\|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayClient\DReplayClient.exe|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayClient\resources\|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayClient\ (all dlls)|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayClient\DReplayClient.config|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayClient\IRTemplate.tdf|Read, Execute, List Folder Contents|  
||\<ToolsDir>\DReplayClient\IRDefinition.xml|Read, Execute, List Folder Contents|  
|Launchpad|%binn|Read, Execute|
||ExtensiblilityData|Full control|
||Log\ExtensibiltityLog|Full control|
  
 \*The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is disabled on instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] and [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] with Advanced Services.  
  
 When database files are stored in a user-defined location, you must grant the per-service SID access to that location. For more information about granting file system permissions to a per-service SID, see [Configure File System Permissions for Database Engine Access](../../database-engine/configure-windows/configure-file-system-permissions-for-database-engine-access.md).  
  
###  <a name="File_System_Other"></a> File System Permissions Granted to Other Windows User Accounts or Groups  

Some access control permissions might have to be granted to built-in accounts or other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service accounts. The following table lists additional ACLs that are set by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
|Requesting component|Account|Resource|Permissions|  
|--------------------------|-------------|--------------|-----------------|  
|MSSQLServer|Performance Log Users|Instid\MSSQL\binn|List folder contents|  
||Performance Monitor Users|Instid\MSSQL\binn|List folder contents|  
||Performance Log Users, Performance Monitor Users|\WINNT\system32\sqlctr130.dll|Read, Execute|  
||Administrator only|\\\\.\root\Microsoft\SqlServer\ServerEvents\\<sql_instance_name>\*|Full control|  
||Administrators, System|\tools\binn\schemas\sqlserver\2004\07\showplan|Full control|  
||Users|\tools\binn\schemas\sqlserver\2004\07\showplan|Read, Execute|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|\<Report Server Web Service Account>|*\<install>*\Reporting Services\LogFiles|DELETE<br /><br /> READ_CONTROL<br /><br /> SYNCHRONIZE<br /><br /> FILE_GENERIC_READ<br /><br /> FILE_GENERIC_WRITE<br /><br /> FILE_READ_DATA<br /><br /> FILE_WRITE_DATA<br /><br /> FILE_APPEND_DATA<br /><br /> FILE_READ_EA<br /><br /> FILE_WRITE_EA<br /><br /> FILE_READ_ATTRIBUTES<br /><br /> FILE_WRITE_ATTRIBUTES|  
||Report Manager Application pool identity, [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] account, Everyone|*\<install>*\Reporting Services\ReportManager, *\<install>*\Reporting Services\ReportManager\Pages\\\*.\*, *\<install>*\Reporting Services\ReportManager\Styles\\\*.\*, *\<install>*\Reporting Services\ReportManager\webctrl_client\1_0\\*.\*|Read|  
||Report Manager Application pool identity|*\<install>*\Reporting Services\ReportManager\Pages\\*.\*|Read|  
||\<Report Server Web Service Account>|*\<install>*\Reporting Services\ReportServer|Read|  
||\<Report Server Web Service Account>|*\<install>*\Reporting Services\ReportServer\global.asax|Full|  
||Everyone|*\<install>*\Reporting Services\ReportServer\global.asax|READ_CONTROL<br /><br /> FILE_READ_DATA<br /><br /> FILE_READ_EA<br /><br /> FILE_READ_ATTRIBUTES|  
||Network service|*\<install>*\Reporting Services\ReportServer\ReportService.asmx|Full|  
||Everyone|*\<install>*\Reporting Services\ReportServer\ReportService.asmx|READ_CONTROL<br /><br /> SYNCHRONIZE FILE_GENERIC_READ<br /><br /> FILE_GENERIC_EXECUTE<br /><br /> FILE_READ_DATA<br /><br /> FILE_READ_EA<br /><br /> FILE_EXECUTE<br /><br /> FILE_READ_ATTRIBUTES|  
||ReportServer Windows Services Account|*\<install>*\Reporting Services\ReportServer\RSReportServer.config|DELETE<br /><br /> READ_CONTROL<br /><br /> SYNCHRONIZE<br /><br /> FILE_GENERIC_READ<br /><br /> FILE_GENERIC_WRITE<br /><br /> FILE_READ_DATA<br /><br /> FILE_WRITE_DATA<br /><br /> FILE_APPEND_DATA<br /><br /> FILE_READ_EA<br /><br /> FILE_WRITE_EA<br /><br /> FILE_READ_ATTRIBUTES<br /><br /> FILE_WRITE_ATTRIBUTES|  
||Everyone|Report Server keys (Instid hive)|Query Value<br /><br /> Enumerate SubKeys<br /><br /> Notify<br /><br /> Read Control|  
||Terminal Services User|Report Server keys (Instid hive)|Query Value<br /><br /> Set Value<br /><br /> Create SubKey<br /><br /> Enumerate SubKey<br /><br /> Notify<br /><br /> Delete<br /><br /> Read Control|  
||Power Users|Report Server keys (Instid hive)|Query Value<br /><br /> Set Value<br /><br /> Create Subkey<br /><br /> Enumerate Subkeys<br /><br /> Notify<br /><br /> Delete<br /><br /> Read Control|  
  
 \*This is the WMI provider namespace.  
  
###  <a name="Unusual_Locations"></a> File System Permissions Related to Unusual Disk Locations  

The default drive for locations for installation is **systemdrive**, normally drive C. This section describes additional considerations when tempdb or user databases are installed to unusual locations.  
  
 **Non-default drive**  
  
 When installed to a local drive that is not the default drive, the per-service SID must have access to the file location. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will provision the required access.  
  
 **Network share**  
  
 When databases are installed to a network share, the service account must have access to the file location of the user and tempdb databases. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup cannot provision access to a network share. The user must provision access to a tempdb location for the service account before running setup. The user must provision access to the user database location before creating the database.  
  
> [!NOTE]  
>  Virtual accounts cannot be authenticated to a remote location. All virtual accounts use the permission of machine account. Provision the machine account in the format _<domain_name>_**\\**_<computer_name>_**$**.  
  
###  <a name="Review_additional_considerations"></a> Reviewing Additional Considerations  

The following table shows the permissions that are required for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services to provide additional functionality.  
  
|Service/Application|Functionality|Required permission|  
|--------------------------|-------------------|-------------------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (MSSQLSERVER)|Write to a mail slot using xp_sendmail.|Network write permissions.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (MSSQLSERVER)|Run xp_cmdshell for a user other than a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator.|Act as part of operating system and replace a process-level token.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent (MSSQLSERVER)|Use the autorestart feature.|Must be a member of the Administrators local group.|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor|Tunes databases for optimal query performance.|On first use, a user who has system administrative credentials must initialize the application. After initialization, dbo users can use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor to tune only those tables that they own. For more information, see "Initializing [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor on First Use" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.|  
  
> [!IMPORTANT]  
>  Before you upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the required default configuration: that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]sysadmin group.  
  
###  <a name="Registry"></a> Registry Permissions  
 The registry hive is created under **HKLM\Software\Microsoft\Microsoft SQL Server\\**_<Instance_ID>_ for instance-aware components. For example  
  
-   **HKLM\Software\Microsoft\Microsoft SQL Server\MSSQL13.MyInstance**  
  
-   **HKLM\Software\Microsoft\Microsoft SQL Server\MSASSQL13.MyInstance**  
  
-   **HKLM\Software\Microsoft\Microsoft SQL Server\MSSQL.130**  
  
 The registry also maintains a mapping of instance ID to instance name. Instance ID to instance name mapping is maintained as follows:  
  
-   **[HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\Instance Names\SQL] "InstanceName"="MSSQL13"**  
  
-   **[HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\Instance Names\OLAP] "InstanceName"="MSASSQL13"**  
  
-   **[HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\Instance Names\RS] "InstanceName"="MSRSSQL13"**  
  
###  <a name="WMI"></a> WMI  

Windows Management Instrumentation (WMI) must be able to connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. To support this, the per-service SID of the Windows WMI provider (**NT SERVICE\winmgmt**) is provisioned in the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 The SQL WMI provider requires the following permissions:  
  
-   Membership in the **db_ddladmin** or **db_owner** fixed database roles in the msdb database.  
  
-   **CREATE DDL EVENT NOTIFICATION** permission in the server.  
  
-   **CREATE TRACE EVENT NOTIFICATION** permission in the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   **VIEW ANY DATABASE** server-level permission.  
  
     [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup creates a SQL WMI namespace and grants read permission to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service-SID.  
  
###  <a name="Pipes"></a> Named Pipes  

In all installation, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup provides access to the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] through the shared memory protocol, which is a local named pipe.  
  
##  <a name="Provisioning"></a> Provisioning  
 This section describes how accounts are provisioned inside the various [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components.  
  
-   [Database Engine Provisioning](#DE_Prov)  
  
    -   [Windows Principals](#Win_Principals)  
  
    -   [sa Account](#sa)  
  
    -   [SQL Server Per-service SID Login and Privileges](#Logins)  
  
    -   [SQL Server Agent Login and Privileges](#Agent)  
  
    -   [HADRON and SQL Failover Cluster Instance and Privileges](#Hadron)  
  
    -   [SQL Writer and Privileges](#Writer)  
  
    -   [SQL WMI and Privileges](#SQLWMI)  
  
-   [SSAS Provisioning](#SSAS)  
  
-   [SSRS Provisioning](#SSRS)  
  
###  <a name="DE_Prov"></a> Database Engine Provisioning  
 The following accounts are added as logins in the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
####  <a name="Win_Principals"></a> Windows Principals  
 During setup, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup requires at least one user account to be named as a member of the **sysadmin** fixed server role.  
  
####  <a name="sa"></a> sa Account  
 The **sa** account is always present as a [!INCLUDE[ssDE](../../includes/ssde-md.md)] login and is a member of the **sysadmin** fixed server role. When the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is installed using only Windows Authentication (that is when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is not enabled), the **sa** login is still present but is disabled. For information about enabling the **sa** account, see [Change Server Authentication Mode](../../database-engine/configure-windows/change-server-authentication-mode.md).  
  
####  <a name="Logins"></a> SQL Server Per-service SID Login and Privileges  
 The per-service SID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is provisioned as a [!INCLUDE[ssDE](../../includes/ssde-md.md)] login. The per-service SID login is a member of the **sysadmin** fixed server role.  
  
####  <a name="Agent"></a> SQL Server Agent Login and Privileges  
 The per-service SID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is provisioned as a [!INCLUDE[ssDE](../../includes/ssde-md.md)] login. The per-service SID login is a member of the **sysadmin** fixed server role.  
  
####  <a name="Hadron"></a> [!INCLUDE[ssHADRc](../../includes/sshadrc-md.md)] and SQL Failover Cluster Instance and Privileges  
 When installing the [!INCLUDE[ssDE](../../includes/ssde-md.md)] as a [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] or SQL Failover Cluster Instance (SQL FCI), **LOCAL SYSTEM** is provisioned in the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The **LOCAL SYSTEM** login is granted the **ALTER ANY AVAILABILITY GROUP** permission (for [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]) and the **VIEW SERVER STATE** permission (for SQL FCI).  
  
####  <a name="Writer"></a> SQL Writer and Privileges  
 The per-service SID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] VSS Writer service is provisioned as a [!INCLUDE[ssDE](../../includes/ssde-md.md)] login. The per-service SID login is a member of the **sysadmin** fixed server role.  
  
####  <a name="SQLWMI"></a> SQL WMI and Privileges  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup provisions the **NT SERVICE\Winmgmt** account as a [!INCLUDE[ssDE](../../includes/ssde-md.md)] login and adds it to the **sysadmin** fixed server role.  
  
#### SSRS Provisioning  
 The account specified during setup is provisioned as a member of the **RSExecRole** database role. For more information, see [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
###  <a name="SSAS"></a> SSAS Provisioning  
 [!INCLUDE[ssAS](../../includes/ssas-md.md)] service account requirements vary depending on how you deploy the server. If you are installing [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup requires that you configure the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service to run under a domain account. Domain accounts are required to support the managed account facility that is built into SharePoint. For this reason, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup does not provide a default service account, such as a virtual account, for a [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation. For more information about provisioning [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint, see [Configure Power Pivot Service Accounts](../../analysis-services/power-pivot-sharepoint/configure-power-pivot-service-accounts.md).  
  
 For all other standalone [!INCLUDE[ssAS](../../includes/ssas-md.md)] installations, you can provision the service to run under a domain account, built-in system account, managed account, or virtual account. For more information about account provisioning, see [Configure Service Accounts &#40;Analysis Services&#41;](../../analysis-services/instances/configure-service-accounts-analysis-services.md).  
  
 For clustered installations, you must specify a domain account or a built-in system account. Neither managed accounts nor virtual accounts are supported for [!INCLUDE[ssAS](../../includes/ssas-md.md)] failover clusters.  
  
 All [!INCLUDE[ssAS](../../includes/ssas-md.md)] installations require that you specify a system administrator of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. Administrator privileges are provisioned in the Analysis Services **Server** role.  
  
###  <a name="SSRS"></a> SSRS Provisioning  
 The account specified during setup is provisioned in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] as a member of the **RSExecRole** database role. For more information, see [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
##  <a name="Upgrade"></a> Upgrading From Previous Versions  
 This section describes the changes made during upgrade from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] requires [!INCLUDE[nextref_longhorn](../../includes/nextref-longhorn-md.md)] R2 SP1, Windows Server 2012, Windows 8.0, Windows Server 2012 R2, or Windows 8.1, . Any previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on a lower operating system version must have the operating system upgraded before upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   During upgrade of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the following way.  
  
    -   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] runs with the security context of the per-service SID. The per-service SID is granted access to the file folders of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (such as DATA), and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] registry keys.  
  
    -   The per-service SID of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is provisioned in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] as a member of the **sysadmin** fixed server role.  
  
    -   The per-service SID's are added to the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Windows groups, unless [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is a Failover Cluster Instance.  
  
    -   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources remain provisioned to the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Windows groups.  
  
    -   The local Windows group for services is renamed from **SQLServer2005MSSQLUser$**_<computer_name>_**$**_<instance_name>_ to **SQLServerMSSQLUser$**_<computer_name>_**$**_<instance_name>_. File locations for migrated databases will have Access Control Entries (ACE) for the local Windows groups. The file locations for new databases will have ACE's for the per-service SID.  
  
-   During upgrade from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will be preserve the ACE's for the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] per-service SID.  
  
-   For a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Instance, the ACE for the domain account configured for the service will be retained.  
  
##  <a name="Appendix"></a> Appendix  
 This section contains additional information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services.  
  
-   [Description of Service Accounts](#Serv_Accts)  
  
-   [Identifying Instance-Aware and Instance-Unaware Services](#Identify_instance_aware_and_unaware)  
  
-   [Localized Service Names](#Localized_service_names)  
  
###  <a name="Serv_Accts"></a> Description of Service Accounts  
 The service account is the account used to start a Windows service, such as the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
####  <a name="Any_OS"></a> Accounts Available With Any Operating System  
 In addition to the new [MSA](#MSA) and [virtual accounts](#VA_Desc) described earlier, the following accounts can be used.  
  
 <a name="Domain_User"></a> **Domain User Account**  
  
 If the service must interact with network services, access domain resources like file shares or if it uses linked server connections to other computers running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you might use a minimally-privileged domain account. Many server-to-server activities can be performed only with a domain user account. This account should be pre-created by domain administration in your environment.  
  
> [!NOTE]  
>  If you configure the application to use a domain account, you can isolate the privileges for the application, but must manually manage passwords or create a custom solution for managing these passwords. Many server applications use this strategy to enhance security, but this strategy requires additional administration and complexity. In these deployments, service administrators spend a considerable amount of time on maintenance tasks such as managing service passwords and service principal names (SPNs), which are required for Kerberos authentication. In addition, these maintenance tasks can disrupt service.  
  
 <a name="Local_User"></a> **Local User Accounts**  
  
 If the computer is not part of a domain, a local user account without Windows administrator permissions is recommended.  
  
 <a name="Local_Service"></a> **Local Service Account**  
  
 The Local Service account is a built-in account that has the same level of access to resources and objects as members of the Users group. This limited access helps safeguard the system if individual services or processes are compromised. Services that run as the Local Service account access network resources as a null session without credentials. Be aware that the Local Service account is not supported for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent services. Local Service is not supported as the account running those services because it is a shared service and any other services running under local service would have system administrator access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The actual name of the account is **NT AUTHORITY\LOCAL SERVICE**.  
  
 <a name="Network_Service"></a> **Network Service Account**  
  
 The Network Service account is a built-in account that has more access to resources and objects than members of the Users group. Services that run as the Network Service account access network resources by using the credentials of the computer account in the format _<domain_name>_**\\**_<computer_name>_**$**. The actual name of the account is **NT AUTHORITY\NETWORK SERVICE**.  
  
<a name="Local_System"></a> **Local System Account**  
  
 Local System is a very high-privileged built-in account. It has extensive privileges on the local system and acts as the computer on the network. The actual name of the account is **NT AUTHORITY\SYSTEM**.  
  
###  <a name="Identify_instance_aware_and_unaware"></a> Identifying Instance-Aware and Instance-Unaware Services  
 Instance-aware services are associated with a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and have their own registry hives. You can install multiple copies of instance-aware services by running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup for each component or service. Instance-unaware services are shared among all installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances. They are not associated with a specific instance, are installed only once, and cannot be installed side-by-side.  
  
 Instance-aware services in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] include the following:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
     Be aware that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is disabled on instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] and [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] with Advanced Services.  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]\*  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
-   Full-text search  
  
 Instance-unaware services in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] include the following:  
  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser  
  
-   SQL Writer  
  
 \*Analysis Services in SharePoint integrated mode runs as '[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]' as a single, named instance. The instance name is fixed. You cannot specify a different name. You can install only one instance of Analysis Services running as '[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]' on each physical server.  
  
###  <a name="Localized_service_names"></a> Localized Service Names  
 The following table shows service names that are displayed by localized versions of Windows.  
  
|Language|Name for Local Service|Name for Network Service|Name for Local System|Name for Admin Group|  
|--------------|----------------------------|------------------------------|---------------------------|--------------------------|  
|English<br /><br /> Simplified Chinese<br /><br /> Traditional Chinese<br /><br /> Korean<br /><br /> Japanese|NT AUTHORITY\LOCAL SERVICE|NT AUTHORITY\NETWORK SERVICE|NT AUTHORITY\SYSTEM|BUILTIN\Administrators|  
|German|NT-AUTORITÄT\LOKALER DIENST|NT-AUTORITÄT\NETZWERKDIENST|NT-AUTORITÄT\SYSTEM|VORDEFINIERT\Administratoren|  
|French|AUTORITE NT\SERVICE LOCAL|AUTORITE NT\SERVICE RÉSEAU|AUTORITE NT\SYSTEM|BUILTIN\Administrators|  
|Italian|NT AUTHORITY\SERVIZIO LOCALE|NT AUTHORITY\SERVIZIO DI RETE|NT AUTHORITY\SYSTEM|BUILTIN\Administrators|  
|Spanish|NT AUTHORITY\SERVICIO LOC|NT AUTHORITY\SERVICIO DE RED|NT AUTHORITY\SYSTEM|BUILTIN\Administradores|  
|Russian|NT AUTHORITY\LOCAL SERVICE|NT AUTHORITY\NETWORK SERVICE|NT AUTHORITY\СИСТЕМА|BUILTIN\Администраторы|  
  
## Related Content  
 [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
 [File Locations for Default and Named Instances of SQL Server](../../sql-server/install/file-locations-for-default-and-named-instances-of-sql-server.md)  
  
 [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)  
  
  
