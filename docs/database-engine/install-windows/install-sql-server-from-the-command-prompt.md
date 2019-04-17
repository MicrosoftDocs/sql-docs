---
title: "Install SQL Server from the Command Prompt | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2019"
ms.prod: sql
ms.technology: install
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "installing SQL Server, command prompt"
  - "installation scripts [SQL Server]"
  - "maintenance scripts [SQL Server]"
  - "REMOVENODE property"
  - "components [SQL Server], removing"
  - "command prompt [SQL Server], SQL Server installations"
  - "ASACCOUNT parameter"
  - "failover clustering [SQL Server], installing"
  - "master database [SQL Server], rebuilding"
  - "SQLCOLLATION parameter"
  - "clusters [SQL Server], installing"
  - "unattended installations [SQL Server]"
  - "modifying collations"
  - "AGTPASSWORD parameter"
  - "USESYSDB parameter"
  - "RSPASSWORD parameter"
  - "AUTOSTART parameter"
  - "ASPASSWORD parameter"
  - "stand-alone installations [SQL Server]"
  - "SAMPLEDATABASESERVER parameter"
  - "adding components"
  - "SAPWD parameter"
  - "scripts [SQL Server], uninstallations"
  - "remote installations [SQL Server]"
  - "components [SQL Server], installing"
  - "TARGETCOMPUTER parameter"
  - "REMOVENODE parameter"
  - "REINSTALLMODE parameter"
  - "scripts [SQL Server], maintenance"
  - "rebuilding registry"
  - "SQLPASSWORD parameter"
  - "rebuilding databases"
  - "IP property"
  - "PIDKEY parameter"
  - "RSCONFIGURATION parameter"
  - "ADDLOCAL parameter"
  - "Setup [SQL Server], command prompt"
  - "REBUILDDATABASE parameter"
  - "SECURITYMODE parameter"
  - "REMOVE property"
  - "DISABLENETWORKPROTOCOLS parameter"
  - "INSTALLDATADIR parameter"
  - "REMOVE parameter"
  - "removing components"
  - "SQLACCOUNT parameter"
  - "parameters [SQL Server], SQL Server installations"
  - "UPGRADE parameter"
  - "shortcuts [SQL Server]"
  - "updating components"
  - "removing SQL Server"
  - "clustered instance of SQL Server"
  - "INSTALLSQLDATADIR parameter"
  - "RSACCOUNT parameter"
  - "ADMINPASSWORD parameter"
  - "GROUP property"
  - "ERRORREPORTING property"
  - "uninstallation scripts [SQL Server]"
  - "AGTACCOUNT parameter"
  - "SAVESYSDB parameter"
  - "INSTALLVS parameter"
  - "INSTANCENAME parameter"
  - "scripts [SQL Server], installations"
  - "rebuilding database, master"
  - "uninstalling SQL Server"
  - "ASCOLLATION parameter"
  - ".ini files"
  - "ADDNODE parameter"
  - "command line installations [SQL Server]"
  - "VS parameter"
  - "INSTALLASDATADIR parameter"
  - "INSTALLSQLDIR parameter"
  - "nodes [Faillover Clustering], command prompt"
  - "INSTALLSQLSHAREDDIR parameter"
ms.assetid: df40c888-691c-4962-a420-78a57852364d
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Install SQL Server from the Command Prompt

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  
  Before you run SQL Server Setup, review [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md).  
  
 Installing a new instance of SQL Server at the command prompt enables you to specify the features to install and how they should be configured. You can also specify silent, basic, or full interaction with the Setup user interface.  
  
> [!NOTE]
> When installing through the command prompt, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports full quiet mode by using the /Q parameter, or Quiet Simple mode by using the /QS parameter. The /QS switch only shows progress, does not accept any input, and displays no error messages if encountered. The /QS parameter is only supported when /Action=install is specified.  
  
 Regardless of the installation method, you are required to confirm acceptance of the software license terms as an individual or on behalf of an entity, unless your use of the software is governed by a separate agreement such as a Microsoft volume licensing agreement or a third-party agreement with an ISV or OEM.  
  
 The license terms are displayed for review and acceptance in the Setup user interface. Unattended installations (using the /Q or /QS parameters) must include the /IACCEPTSQLSERVERLICENSETERMS parameter. You can review the license terms separately at [Microsoft Software License Terms](https://go.microsoft.com/fwlink/?LinkId=148209).  
  
> [!NOTE] 
> Depending on how you received the software (for example, through Microsoft volume licensing), your use of the software may be subject to additional terms and conditions.  
  
 Command prompt installation is supported in the following scenarios:  
  
-   Installing, upgrading, or removing an instance and shared components of SQL Server on a local computer by using syntax and parameters specified at the command prompt.  
  
-   Installing, upgrading, or removing a failover cluster instance.  
  
-   Upgrading from one [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition to another edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Installing an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a local computer by using syntax and parameters specified in a configuration file. You can use this method to copy an installation configuration to multiple computers, or to install multiple nodes of a failover cluster installation.  
  
 When you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] at the command prompt, specify Setup parameters for your installation at the command prompt as part of your installation syntax.  
  
> [!NOTE]
> For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share. For failover cluster installations, you must be a local administrator with permissions to login as a service, and to act as part of the operating system on all failover cluster nodes.  
  
##  <a name="ProperUse"></a> Proper use of setup parameters  
Use the following guidelines to develop installation commands that have correct syntax:  
  
-   /PARAMETER  
  
-   /PARAMETER=true/false  
  
-   /PARAMETER=1/0 for Boolean types  
  
-   /PARAMETER="value" for all single-value parameters. Using double quotation marks is recommended, but required if the value contains a space  
  
-   /PARAMETER="value1" "value2" "value3" for all multiple-value parameters. Using double quotation marks is recommended, but required if the value contains a space  
  
**Exceptions:**  
  
-   `/FEATURES`, which is a multivalued parameter, but its format is `/FEATURES=AS,RS,IS` without a space, comma-delimited  
  
**Examples:**  
  
-   `/INSTANCEDIR=c:\Path` is supported.  
  
-   `/INSTANCEDIR="c:\Path"` is supported  
  
> [!IMPORTANT]  
> When installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if you specify the same directory path for INSTANCEDIR and SQLUSERDBDIR, SQL Server Agent and Full Text Search do not start due to missing permissions.  
  
> [!NOTE]  
> The relational server values support the additional terminating backslash formats (backslash or two backslash characters) for the path.  

> [!IMPORTANT]  
> The value for the /PID parameter should be enclosed in double quotation marks.  
  
## SQL Server Parameters  
 The following sections provide parameters to develop command line installation scripts for install, update, and repair scenarios.  
  
 Parameters that are listed for a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component are specific to that component. SQL Server Agent and SQL Server Browser parameters are applicable when you install the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
-   [Installation Parameters](#Install)  
  
-   [SysPrep Parameters](#SysPrep)  
  
-   [Upgrade Parameters](#Upgrade)  
  
-   [Repair Parameters](#Repair)  
  
-   [Rebuild System Database Parameters](#Rebuild)  
  
-   [Uninstall Parameters](#Uninstall)  
  
-   [Failover Cluster Parameters](#ClusterInstall)  
  
-   [Service Accounts Parameters](#Accounts)  
  
-   [Feature Parameters](#Feature)  
  
-   [Role Parameters](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#RoleParameters)  
  
-   [Controlling Failover Behavior using the /FAILOVERCLUSTERROLLOWNERSHIP Parameter](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#RollOwnership)  
  
-   [Instance ID or InstanceID Configuration](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#InstanceID) 

##  <a name="Install"></a> Installation Parameters  
 Use the parameters in the following table to develop command line scripts for installation.  
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the installation workflow.<br /><br /> Supported values: **Install**.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/SUPPRESSPRIVACYSTATEMENTNOTICE<br /><br /> |Suppresses the privacy notice statement. By using this flag, you are agreeing with the [privacy notice](../../sql-server/sql-server-privacy.md).  |  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/IACCEPTSQLSERVERLICENSETERMS<br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations.**|Required to acknowledge acceptance of the license terms.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Python Setup Control|/IACCEPTPYTHONLICENSETERMS <br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations that include the Anaconda Python package.**|Required to acknowledge acceptance of the license terms.| 
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] R Setup Control|/IACCEPTROPENLICENSETERMS <br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations that include the Microsoft R Open package.**|Required to acknowledge acceptance of the license terms.| 
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/UpdateEnabled<br /><br /> **Optional**|Specify whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will include updates that are found.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/UpdateSource<br /><br /> **Optional**|Specify the location where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will obtain product updates. The valid values are "MU" to search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update, a valid folder path, a relative path such as `.\MyUpdates` or a UNC share. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update or a Windows Update Service through the Windows Server Update Services.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/ERRORREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] . <br/><br/> To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies the error reporting for SQL Server.<br /><br /> For more information, see [Privacy Statement for the Microsoft Error Reporting Service](https://go.microsoft.com/fwlink/?LinkID=868444).<br /><br /> Supported values:<br /><br /> 0=disabled<br /><br /> 1=enabled|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/FEATURES<br /><br /> - Or -<br /><br /> /ROLE<br /><br /> **Required**|Specifies the components to install.<br /><br /> Choose **/FEATURES** to specify individual [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components to install. For more information, see [Feature Parameters](#Feature) below.<br /><br /> Choose **/ROLE** to specify a setup role. Setup roles install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a predetermined configuration.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for installation parameters.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file is piped to the console.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTALLSHAREDDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for 64-bit shared components.<br /><br /> Default is `%Program Files%\Microsoft SQL Server`<br /><br /> Cannot be set to `%Program Files(x86)%\Microsoft SQL Server`|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTALLSHAREDWOWDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for 32-bit shared components. Supported only on a 64-bit system.<br /><br /> Default is `%Program Files(x86)%\Microsoft SQL Server`<br /><br /> Cannot be set to `%Program Files%\Microsoft SQL Server`|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTANCEDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for instance-specific components.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTANCEID<br /><br /> **Optional**|Specifies a nondefault value for an [InstanceID](#InstanceID).|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|PolyBase|/PBENGSVCACCOUNT<br /><br /> **Optional**|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|PolyBase|/PBDMSSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the engine service account.|  
|PolyBase|/PBENGSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual.|  
|PolyBase|/PBPORTRANGE<br /><br /> **Optional**|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  
|PolyBase|/PBSCALEOUT<br /><br /> **Optional**|Specifies if the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance will be used as a part of PolyBase Scale-out computational group. Supported values: **True**, **False**|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/PID<br /><br /> **Optional**|Specifies the product key for the edition of SQL Server. If this parameter is not specified, Evaluation is used.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/Q or /QUIET<br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/QS or /QUIETSIMPLE <br /><br /> **Optional**|Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/UIMODE<br /><br /> **Optional**|Specifies whether to present only the minimum number of dialog boxes during setup.<br /><br /> **/UIMode** can only be used with the **/ACTION=INSTALL** and **UPGRADE** parameters. Supported values:<br /><br /> **/UIMODE=Normal** is the default for non-Express editions and presents all setup dialog boxes for the selected features.<br /><br /> **/UIMODE=AutoAdvance** is the default for Express editions and skips nonessential dialog boxes.<br /><br /> <br /><br /> Note that when combined with other parameters, **UIMODE** is overridden. For example, when **/UIMODE=AutoAdvance** and **/ADDCURRENTUSERASSQLADMIN=FALSE** are both provided, the provisioning dialog box is not auto populated with the current user.<br /><br /> The **UIMode** setting cannot be used with the **/Q** or **/QS** parameters.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/SQMREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] . <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies feature usage reporting for SQL Server.<br /><br />Supported values:<br /><br /> 0=disabled<br /><br /> 1=enabled|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent|/AGTSVCACCOUNT<br /><br /> **Required**|Specifies the account for the SQL Server Agent service.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent|/AGTSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQL Server Agent service account.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent|/AGTSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the SQL Server Agent service.<br /><br /> Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASBACKUPDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] backup files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Backup`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Backup`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASCOLLATION<br /><br /> **Optional**|Specifies the collation setting for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].<br /><br /> Default value: **Latin1_General_CI_AS**|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASCONFIGDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configuration files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Config`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Config`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASDATADIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Data`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Data`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASLOGDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] log files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Log`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Log`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSERVERMODE<br /><br /> **Optional**|Specifies the server mode of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. Valid values are MULTIDIMENSIONAL, POWERPIVOT or TABULAR. **ASSERVERMODE** is case-sensitive. All values must be expressed in upper case. For more information about valid values, see [Install Analysis Services](../../analysis-services/instances/install-windows/install-analysis-services.md).|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSVCACCOUNT<br /><br /> **Required**|Specifies the account for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service. Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSYSADMINACCOUNTS<br /><br /> **Required**|Specifies the administrator credentials for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASTEMPDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] temporary files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server \<INSTANCEDIR>\<ASInstanceID>\OLAP\Temp`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Temp`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASPROVIDERMSOLAP<br /><br /> **Optional**|Specifies whether the MSOLAP provider can run in-process.<br /><br /> Default value: 1=enabled|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/FARMACCOUNT<br /><br /> **Required for SPI_AS_NewFarm**|Specifies a domain user account for running SharePoint Central Administration services and other essential services in a farm.<br /><br /> This parameter is used only for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that are installed through /ROLE = SPI_AS_NEWFARM.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/FARMPASSWORD<br /><br /> **Required for SPI_AS_NewFarm**|Specifies a password for the farm account.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/PASSPHRASE<br /><br /> **Required for SPI_AS_NewFarm**|Specifies a passphrase that is used to add additional application servers or Web front end servers to a SharePoint farm.<br /><br /> This parameter is used only for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that are installed through /ROLE = SPI_AS_NEWFARM.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/FARMADMINIPORT<br /><br /> **Required for SPI_AS_NewFarm**|Specifies a port used to connect to the SharePoint Central Administration web application.<br /><br /> This parameter is used only for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that are installed through /ROLE = SPI_AS_NEWFARM.|  
|SQL Server Browser|/BROWSERSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for SQL Server Browser service. Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/ENABLERANU<br /><br /> **Optional**|Enables run-as credentials for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] installations.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/INSTALLSQLDATADIR<br /><br /> **Optional**|Specifies the data directory for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SAPWD<br /><br /> **Required when /SECURITYMODE=SQL**|Specifies the password for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **SA** account.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SECURITYMODE<br /><br /> **Optional**|Specifies the security mode for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If this parameter is not supplied, then Windows-only authentication mode is supported.<br /><br /> Supported value: **SQL**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLBACKUPDIR<br /><br /> **Optional**|Specifies the directory for backup files.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Backup`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLCOLLATION<br /><br /> **Optional**|Specifies the collation settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> The default value is based on the locale of your Windows operating system. For more information, see [Collation Settings in Setup](https://msdn.microsoft.com/library/ms143508%28v=sql.105%29.aspx).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/ADDCURRENTUSERASSQLADMIN<br /><br /> **Optional**|Adds the current user to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **sysadmin** fixed server role. The /ADDCURRENTUSERASSQLADMIN parameter can be used when installing Express editions or when /Role=ALLFeatures_WithDefaults is used. For more information, see /ROLE below.<br /><br /> Use of /ADDCURRENTUSERASSQLADMIN is optional, but either /ADDCURRENTUSERASSQLADMIN or /SQLSYSADMINACCOUNTS is required. Default values:<br /><br /> **True** for editions of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> **False** for all other editions|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQLSVCACCOUNT.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSYSADMINACCOUNTS<br /><br /> **Required**|Use this parameter to provision logins to be members of the sysadmin role.<br /><br /> For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] editions other than [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], /SQLSYSADMINACCOUNTS is required. For editions of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], use of /SQLSYSADMINACCOUNTS is optional, but either /SQLSYSADMINACCOUNTS  or /ADDCURRENTUSERASSQLADMIN is required.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBDIR<br /><br /> **Optional**|Specifies the directories for tempdb data files. When specifying more than one directory, separate the directories with a blank space. If multiple directories are specified the tempdb data files will be spread across the directories in a round-robin fashion.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> NOTE: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGDIR<br /><br /> **Optional**|Specifies the directory for tempdb log file.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> Note: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILECOUNT<br /><br /> **Optional**|Specifies the number of tempdb data files to be added by setup. This value can be increased up to the number of cores. Default value:<br /><br /> 1  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8 or the number of cores, whichever is lower for all other editions<br /><br /> **Important** The primary database file for tempdb will still be tempdb.mdf. The additional tempdb files are named as tempdb_mssql_#.ndf where # represents a unique number for each additional tempdb database file created during setup. The purpose of this naming convention is to make them unique. Uninstalling an instance of SQL Server deletes the files with naming convention tempdb_mssql_#.ndf. Do not use tempdb_mssql_\*.ndf naming convention for user database files.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILESIZE<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb data file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILEGROWTH<br /><br /> **Optional**|Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024 .<br /><br /> Default value: 64. Allowed range: Min = 0, Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILESIZE<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb log file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILEGROWTH<br /><br /> **Optional**|Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024.<br /><br /> Default value: 64. Allowed range: Min = 0, Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLUSERDBDIR<br /><br /> **Optional**|Specifies the directory for the data files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCINSTANTFILEINIT<br /><br /> **Optional**|Enables instant file initialization for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account. For security and performance considerations, see [Database Instant File Initialization](../../relational-databases/databases/database-instant-file-initialization.md).<br /><br /> Default value: "False"<br /><br /> Optional value: "True"|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLUSERDBLOGDIR<br /><br /> **Optional**|Specifies the directory for the log files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|  
|FILESTREAM|/FILESTREAMLEVEL<br /><br /> **Optional**|Specifies the access level for the FILESTREAM feature. Supported values:<br /><br /> 0 =Disable FILESTREAM support for this instance. (Default value)<br /><br /> 1=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access.<br /><br /> 2=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] and file I/O streaming access. (Not valid for cluster scenarios)<br /><br /> 3=Allow remote clients to have streaming access to FILESTREAM data.|  
|FILESTREAM|/FILESTREAMSHARENAME<br /><br /> **Optional**<br /><br /> **Required when FILESTREAMLEVEL is greater than 1.**|Specifies the name of the windows share in which the FILESTREAM data will be stored.|  
|SQL Server Full Text|/FTSVCACCOUNT<br /><br /> **Optional**|Specifies the account for Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher. ServiceSID is used to help secure the communication between SQL Server and Full-text Filter Daemon. If the values are not provided, the Full-text Filter Launcher Service is disabled. You have to use SQL Server Control Manager to change the service account and enable full-text functionality.<br /><br /> Default value: Local Service Account|  
|SQL Server Full Text|/FTSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCACCOUNT<br /><br /> **Required**|Specifies the account for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].<br /><br /> Default value: NT AUTHORITY\NETWORK SERVICE|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] password.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.|  
|SQL Server Network Configuration|/NPENABLED<br /><br /> **Optional**|Specifies the state of the Named Pipes protocol for the SQL Server service. Supported values:<br /><br /> 0=disable the Named Pipes protocol<br /><br /> 1=enable the Named Pipes protocol|  
|SQL Server Network Configuration|/TCPENABLED<br /><br /> **Optional**|Specifies the state of the TCP protocol for the SQL Server service. Supported values:<br /><br /> 0=disable the TCP protocol<br /><br /> 1=enable the TCP protocol|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSINSTALLMODE<br /><br /> **Optional**|Specifies the Install mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Supported Values:<br /><br /> **SharePointFilesOnlyMode**<br /><br /> **DefaultNativeMode**<br /><br /> **FilesOnlyMode**<br /><br /> <br /><br /> Note: If the installation includes the SQL Server[!INCLUDE[ssDE](../../includes/ssde-md.md)], the default RSINSTALLMODE is DefaultNativeMode.<br /><br /> If the installation does not include the SQL Server[!INCLUDE[ssDE](../../includes/ssde-md.md)], the default RSINSTALLMODE is FilesOnlyMode.<br /><br /> If you choose DefaultNativeMode but the installation does not include the SQL Server[!INCLUDE[ssDE](../../includes/ssde-md.md)], the installation will automatically change the RSINSTALLMODE to FilesOnlyMode.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for the startup account for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|Python/Machine Learning Services (In-Database)|MPYCACHEDIRECTORY|Reserved for future use. Use %TEMP% to store Python .CAB files for installation on a computer that does not have an internet connection. |  
|R/Machine Learning Services (In-Database)|MRCACHEDIRECTORY|Use this parameter to specify the Cache directory for Microsoft R Open, SQL Server 2016 R Services, SQL Server 2016 R Server (Standalone), or R feature support in SQL Server 2017 Machine Learning Services or Machine Learning Server (Standalone). This setting is typically used when installing R components from the [command line on a computer without Internet access](https://docs.microsoft.com/sql/advanced-analytics/r-services/installing-r-components-without-internet-access).|  
  
###### Sample Syntax:  
 To install a new, stand-alone instance with the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], Replication, and Full-Text Search components and enable instant file initialization for [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. 
  
```  
setup.exe /q /ACTION=Install /FEATURES=SQL /INSTANCENAME=MSSQLSERVER /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="<StrongPassword>" /SQLSYSADMINACCOUNTS="<DomainName\UserName>" /AGTSVCACCOUNT="NT AUTHORITY\Network Service" /SQLSVCINSTANTFILEINIT="True" /IACCEPTSQLSERVERLICENSETERMS  
```  
  
##  <a name="SysPrep"></a> SysPrep Parameters  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep, see  
  
 [Install [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Using SysPrep](../../database-engine/install-windows/install-sql-server-using-sysprep.md). 
  
#### Prepare Image Parameters  
 Use the parameters in the following table to develop command-line scripts for preparing an instance of SQL Server without configuring it. 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the installation workflow.<br /><br /> Supported values: **PrepareImage**|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/IACCEPTSQLSERVERLICENSETERMS<br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations.**|Required to acknowledge acceptance of the license terms.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of SQL Server on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/UpdateEnabled<br /><br /> **Optional**|Specify whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, SQL Server setup will include updates that are found.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/UpdateSource<br /><br /> **Optional**|Specify the location where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will obtain product updates. The valid values are "MU" to search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update, a valid folder path, a relative path such as `.\MyUpdates` or a UNC share. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update or a Windows Update Service through the Windows Server Update Services.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/FEATURES<br /><br /> **Required**|Specifies [components](#Feature) to install.<br /><br /> Supported values are SQLEngine, Replication, FullText, DQ, AS, AS_SPI, RS, RS_SHP, RS_SHPWFE, DQC, Conn, IS, BC, SDK, DREPLAY_CTLR, DREPLAY_CLT, SNAC_SDK, SQLODBC, SQLODBC_SDK, LocalDB, MDS, POLYBASE|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for installation parameters.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file is piped to the console.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTALLSHAREDDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for 64-bit shared components.<br /><br /> Default is `%Program Files%\Microsoft SQL Server`<br /><br /> Cannot be set to `%Program Files(x86)%\Microsoft SQL Server`|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTANCEDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for instance-specific components.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTANCEID<br /><br /> Prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2 (January 2013) **Required**<br /><br /> Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2 **Required** for instance features.|Specifies an InstanceID for the instance that is being prepared.|  
|PolyBase|/PBENGSVCACCOUNT<br /><br /> **Optional**|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|PolyBase|/PBDMSSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the engine service account.|  
|PolyBase|/PBENGSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual.|  
|PolyBase|/PBPORTRANGE<br /><br /> **Optional**|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  
|PolyBase|/PBSCALEOUT<br /><br /> **Optional**|Specifies if the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance will be used as a part of PolyBase Scale-out computational group. Supported values: **True**, **False**|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/QS or /QUIETSIMPLE<br /><br /> **Optional**|Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages.|  
  
###### Sample Syntax:  
 To prepare a new, stand-alone instance with the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], Replication, and Full-Text Search components, and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. 
  
```  
setup.exe /q /ACTION=PrepareImage /FEATURES=SQL,RS /InstanceID =<MYINST> /IACCEPTSQLSERVERLICENSETERMS  
```  
  
#### Complete Image Parameters  
 Use the parameters in the following table to develop command-line scripts for completing and configuring a prepared instance of SQL Server. 
  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the installation workflow.<br /><br /> Supported values: **CompleteImage**|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/IACCEPTSQLSERVERLICENSETERMS<br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations.**|Required to acknowledge acceptance of the license terms.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of SQL Server on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/ERRORREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies the error reporting for SQL Server.<br /><br /> For more information, see [Privacy Statement for the Microsoft Error Reporting Service](https://go.microsoft.com/fwlink/?LinkID=868444). Supported values:<br /><br /> 1=enabled<br /><br /> 0=disabled|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for installation parameters.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file is piped to the console.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTANCEID<br /><br /> Prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2 (January 2013) **Required**<br /><br /> Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2 **Optional**|Use the Instance ID specified during the prepare image step.<br /><br /> Supported Values: InstanceID of a Prepared Instance.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> Prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2 (January 2013) **Required**<br /><br /> Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2 **Optional**|Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name for the instance that is being completed.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|PolyBase|/PBENGSVCACCOUNT<br /><br /> **Optional**|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|PolyBase|/PBDMSSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the engine service account.|  
|PolyBase|/PBENGSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual.|  
|PolyBase|/PBPORTRANGE<br /><br /> **Optional**|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  
|PolyBase|/PBSCALEOUT<br /><br /> **Optional**|Specifies if the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance will be used as a part of PolyBase Scale-out computational group. Supported values: **True**, **False**|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/PID<br /><br /> **Optional**|Specifies the product key for the edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If this parameter is not specified, Evaluation is used.<br /><br /> **Note:** If you are installing [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] with tools, or [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] with Advanced Services, the PID is predefined.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter. |  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/QS or /QUIETSIMPLE<br /><br /> **Optional**|Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/SQMREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies feature usage reporting for SQL Server.<br /><br />Supported values:<br /><br /> 0=disabled<br /><br /> 1=enabled|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
|SQL Server Agent|/AGTSVCACCOUNT<br /><br /> **Required**|Specifies the account for the SQL Server Agent service.|  
|SQL Server Agent|/AGTSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQL Server Agent service account.|  
|SQL Server Agent|/AGTSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the SQL Server Agent service. Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|SQL Server Browser|/BROWSERSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for SQL Server Browser service. Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/ENABLERANU<br /><br /> **Optional**|Enables run-as credentials for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] installations.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/INSTALLSQLDATADIR<br /><br /> **Optional**|Specifies the data directory for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SAPWD<br /><br /> **Required when /SECURITYMODE=SQL**|Specifies the password for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **SA** account.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SECURITYMODE<br /><br /> **Optional**|Specifies the security mode for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If this parameter is not supplied, then Windows-only authentication mode is supported.<br /><br /> Supported value: **SQL**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLBACKUPDIR<br /><br /> **Optional**|Specifies the directory for backup files.<br /><br /> Default value:<br /><br /> `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Backup`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLCOLLATION<br /><br /> **Optional**|Specifies the collation settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> The default value is based on the locale of your Windows operating system. For more information, see [Collation Settings in Setup](https://msdn.microsoft.com/library/ms143508%28v=sql.105%29.aspx).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQLSVCACCOUNT.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSYSADMINACCOUNTS<br /><br /> **Required**|Use this parameter to provision logins to be members of the sysadmin role.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBDIR<br /><br /> **Optional**|Specifies the directories for tempdb data files. When specifying more than one directory, separate the directories with a blank space. If multiple directories are specified the tempdb data files will be spread across the directories in a round-robin fashion.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> NOTE: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGDIR<br /><br /> **Optional**|Specifies the directory for tempdb log file.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> Note: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILESIZE<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb data file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILEGROWTH<br /><br /> **Optional**|Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024.<br /><br /> Default value: 64<br /><br /> Allowed range: Min = 0, Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILESIZE<br /><br /> **Optional**|Specifies the initial size of the tempdb log file in MB. Setup allows the size up to 1024.<br /><br /> Default value:<br /><br /> 4  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8  for all other editions<br /><br /> Allowed range: Min = default value (4 or 8), Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILEGROWTH<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb log file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILECOUNT<br /><br /> **Optional**|Specifies the number of tempdb data files to be added by setup. This value can be increased up to the number of cores. Default value:<br /><br /> 1  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8 or the number of cores, whichever is lower for all other editions<br /><br /> **Important:** The primary database file for tempdb will still be tempdb.mdf. The additional tempdb files are named as tempdb_mssql_#.ndf where # represents a unique number for each additional tempdb database file created during setup. The purpose of this naming convention is to make them unique. Uninstalling an instance of SQL Server deletes the files with naming convention tempdb_mssql_#.ndf. Do not use tempdb_mssql_\*.ndf naming convention for user database files.<br /><br /> **Warning:** [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] is not supported for configuring this parameter. Setup installs only 1 tempdb data file.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLUSERDBDIR<br /><br /> **Optional**|Specifies the directory for the data files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLUSERDBLOGDIR<br /><br /> **Optional**|Specifies the directory for the log files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|  
|FILESTREAM|/FILESTREAMLEVEL<br /><br /> **Optional**|Specifies the access level for the FILESTREAM feature. Supported values:<br /><br /> 0 =Disable FILESTREAM support for this instance. (Default value)<br /><br /> 1=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access.<br /><br /> 2=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] and file I/O streaming access. (Not valid for cluster scenarios)<br /><br /> 3=Allow remote clients to have streaming access to FILESTREAM data.|  
|FILESTREAM|/FILESTREAMSHARENAME<br /><br /> **Optional**<br /><br /> **Required when FILESTREAMLEVEL is greater than 1.**|Specifies the name of the windows share in which the FILESTREAM data will be stored.|  
|SQL Server Full Text|/FTSVCACCOUNT<br /><br /> **Optional**|Specifies the account for Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher. ServiceSID is used to help secure the communication between SQL Server and Full-text Filter Daemon. If the values are not provided, the Full-text Filter Launcher Service is disabled. You have to use SQL Server Control Manager to change the service account and enable full-text functionality.<br /><br /> Default value: Local Service Account|  
|SQL Server Full Text|/FTSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher.|  
|SQL Server Network Configuration|/NPENABLED<br /><br /> **Optional**|Specifies the state of the Named Pipes protocol for the SQL Server service. Supported values:<br /><br /> 0=disable the Named Pipes protocol<br /><br /> 1=enable the Named Pipes protocol|  
|SQL Server Network Configuration|/TCPENABLED<br /><br /> **Optional**|Specifies the state of the TCP protocol for the SQL Server service. Supported values:<br /><br /> 0=disable the TCP protocol<br /><br /> 1=enable the TCP protocol|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSINSTALLMODE<br /><br /> **Optional**|Specifies the Install mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for the startup account for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
  
###### Sample Syntax:  
 To complete a prepared, stand-alone instance that includes [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], Replication, and Full-Text Search components. 
  
```  
setup.exe /q /ACTION=CompleteImage /INSTANCENAME=MYNEWINST /INSTANCEID=<MYINST> /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="<StrongPassword>" /SQLSYSADMINACCOUNTS="<DomainName\UserName>" /AGTSVCACCOUNT="NT AUTHORITY\Network Service" /IACCEPTSQLSERVERLICENSETERMS  
```  
  
##  <a name="Upgrade"></a> Upgrade Parameters  
 Use the parameters in the following table to develop command-line scripts for upgrade. 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the installation workflow. Supported values:<br /><br /> **Upgrade**<br /><br /> **EditionUpgrade**<br /><br /> <br /><br /> The value **EditionUpgrade** is used to upgrade an existing edition of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to a different edition. For more information about the supported version and edition upgrades, see [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/IACCEPTSQLSERVERLICENSETERMS<br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations.**|Required to acknowledge acceptance of the license terms.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of SQL Server on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateEnabled*<br /><br /> **Optional**|Specify whether SQL Server setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, SQL Server setup will include updates that are found.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateSource*<br /><br /> **Optional**|Specify the location where SQL Server setup will obtain product updates. The valid values are "MU" to search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update, a valid folder path, a relative path such as .\MyUpdates or a UNC share. By default, SQL Server setup will search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update or a Windows Update Service through the Windows Server Update Services.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ERRORREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies the error reporting for SQL Server.<br /><br /> For more information, see [Privacy Statement for the Microsoft Error Reporting Service](https://go.microsoft.com/fwlink/?LinkID=868444). Supported values:<br /><br /> 1=enabled<br /><br /> 0=disabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for the parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file will be piped to the console.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ INSTANCEDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for shared components.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCEID<br /><br /> **Required when you upgrade from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]** or later.<br /><br /> **Optional when you upgrade from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]**|Specifies a nondefault value for an [InstanceID](#InstanceID).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/PID<br /><br /> **Optional**|Specifies the product key for the edition of SQL Server. If this parameter is not specified, Evaluation is used.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q<br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/UIMODE<br /><br /> **Optional**|Specifies whether to present only the minimum number of dialog boxes during setup. <br />                **/UIMode** can only be used with the **/ACTION=INSTALL** and **UPGRADE** parameters. Supported values:<br /><br /> **/UIMODE=Normal** is the default for non-Express editions and presents all setup dialog boxes for the selected features.<br /><br /> **/UIMODE=AutoAdvance** is the default for Express editions and skips nonessential dialog boxes.<br /><br /> Note that the **UIMode** setting cannot be used with the **/Q** or **/QS** parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/SQMREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies feature usage reporting for SQL Server.<br /><br />Supported values:<br /><br /> 1=enabled<br /><br /> 0=disabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies the console window would be hidden or closed.|  
|SQL Server Browser Service|/BROWSERSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for SQL Server Browser service. Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|SQL Server Full-Text|/FTUPGRADEOPTION<br /><br /> **Optional**|Specifies the Full-Text catalog upgrade option. Supported values:<br /><br /> **REBUILD**<br /><br /> **RESET**<br /><br /> **IMPORT**|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCACCOUNT<br /><br /> **Required**|Specifies the account for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].<br /><br /> Default value: NT AUTHORITY\NETWORK SERVICE|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] password.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSUPGRADEDATABASEACCOUNT<br /><br /> **Optional**|The property is only used when upgrading a SharePoint mode Report Server that is version 2008 R2 or earlier. Additional upgrade operations are performed for report servers that use the older SharePoint mode architecture, which was changed in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If this option is not included with the command line installation, the default service account for the old report server instance is used. If this property is used, supply the password for the account using the **/RSUPGRADEPASSWORD** property.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSUPGRADEPASSWORD<br /><br /> **Optional**|Password of the existing Report Server service account.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/ALLOWUPGRADEFORSSRSSHAREPOINTMODE|The switch is required when upgrading a SharePoint Mode installation that is based on the SharePoint shared service architecture. The switch is not needed for upgrading non-shared service versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
  
###### Sample Syntax:  
 To upgrade an existing instance or failover cluster node from a previous [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] version,  
  
```  
setup.exe /q /ACTION=upgrade /INSTANCEID = <INSTANCEID>/INSTANCENAME=MSSQLSERVER /RSUPGRADEDATABASEACCOUNT="<Provide a SQL Server logon account that can connect to the report server during upgrade>" /RSUPGRADEPASSWORD="<Provide a password for the report server upgrade account>" /ISSVCAccount="NT Authority\Network Service" /IACCEPTSQLSERVERLICENSETERMS  
```  
  
##  <a name="Repair"></a> Repair Parameters  
 Use the parameters in the following table to develop command-line scripts for repair. 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the repair workflow.<br /><br /> Supported values: **Repair**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FEATURES<br /><br /> **Required**|Specifies [components](#Feature) to repair.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|PolyBase|/PBENGSVCACCOUNT<br /><br /> **Optional**|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|PolyBase|/PBDMSSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the engine service account.|  
|PolyBase|/PBENGSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual.|  
|PolyBase|/PBPORTRANGE<br /><br /> **Optional**|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  
|PolyBase|/PBSCALEOUT<br /><br /> **Optional**|Specifies if the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance will be used as a part of PolyBase Scale-out computational group. Supported values: **True**, **False**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
  
###### Sample Syntax:  
 Repair an instance and shared components. 
  
```  
setup.exe /q /ACTION=Repair /INSTANCENAME=<instancename>  
```  
  
##  <a name="Rebuild"></a> Rebuild System Database Parameters  
 Use the parameters in the following table to develop command-line scripts for rebuilding the master, model, msdb, and tempdb system databases. For more information, see [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md). 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the rebuild database workflow.<br /><br /> Supported values: **Rebuilddatabase**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLCOLLATION<br /><br /> **Optional**|Specifies a new server-level collation.<br /><br /> The default value is based on the locale of your Windows operating system. For more information, see [Collation Settings in Setup](https://msdn.microsoft.com/library/ms143508%28v=sql.105%29.aspx).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SAPWD<br /><br /> **Required when /SECURITYMODE=SQL was specified during Installation of the Instance.**|Specifies the password for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **SA** account.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSYSADMINACCOUNTS<br /><br /> **Required**|Use this parameter to provision logins to be members of the sysadmin role.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBDIR<br /><br /> **Optional**|Specifies the directories for tempdb data files. When specifying more than one directory, separate the directories with a blank space. If multiple directories are specified the tempdb data files will be spread across the directories in a round-robin fashion.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> NOTE: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGDIR<br /><br /> **Optional**|Specifies the directory for tempdb log file.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> **Note:** This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILECOUNT<br /><br /> **Optional**|Specifies the number of tempdb data files to be added by setup. This value can be increased up to the number of cores. Default value:<br /><br /> 1  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8 or the number of cores, whichever is lower for all other editions<br /><br /> **Important:** The primary database file for tempdb will still be tempdb.mdf. The additional tempdb files are named as tempdb_mssql_#.ndf where # represents a unique number for each additional tempdb database file created during setup. The purpose of this naming convention is to make them unique. Uninstalling an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] deletes the files with naming convention tempdb_mssql_#.ndf. Do not use tempdb_mssql_\*.ndf naming convention for user database files.<br /><br /> **Warning:** [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] is not supported for configuring this parameter. Setup installs only 1 tempdb data file.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILESIZE<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb data file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILEGROWTH<br /><br /> **Optional**|Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024.<br /><br /> Default value: 64<br /><br /> Allowed range: Min = 0, Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILESIZE<br /><br /> **Optional**|Specifies the initial size of the tempdb log file in MB. Setup allows the size up to 1024. Default value:<br /><br /> 4  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8  for all other editions<br /><br /> Allowed range: Min = default value (4 or 8), Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILEGROWTH<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb log file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
  
##  <a name="Uninstall"></a> Uninstall Parameters  
 Use the parameters in the following table to develop command-line scripts for uninstallation. 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the uninstall work flow.<br /><br /> Supported values: **Uninstall**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FEATURES<br /><br /> **Required**|Specifies [components](#Feature) to uninstall.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for the parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file will be piped to the console.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
  
###### Sample Syntax:  
 To uninstall an existing instance of SQL Server. 
  
```  
setup.exe /Action=Uninstall /FEATURES=SQL,AS,RS,IS,Tools /INSTANCENAME=MSSQLSERVER  
```  
  
 To remove a named instance, specify the name of the instance instead of "MSSQLSERVER" in the example that was mentioned earlier in this article. 
  
##  <a name="ClusterInstall"></a> Failover Cluster Parameters  
 Before you install a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster instance, review the following articles:  
  
-   [Hardware and Software Requirements for Installing [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  
  
-   [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)
  
-   [Before Installing Failover Clustering](../../sql-server/failover-clusters/install/before-installing-failover-clustering.md)  
  
-   [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md)  
  
    > [!IMPORTANT]  
    > All failover cluster installation commands require an underlying Windows cluster. All the nodes that will be part of a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster must be part of the same Windows cluster. 
  
 Test and modify the following failover cluster installation scripts to meet the needs of your organization. 
  
#### Integrated Install Failover Cluster Parameters  
 Use the parameters in the following table to develop command-line scripts for failover cluster installation. 
  
 For more information about Integrated Installation, see [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md). 
  
> [!NOTE]
> To add more nodes after the installation, use [Add Node](#AddNode) action. 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Details|  
|-----------------------------------------|---------------|-------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the failover cluster installation work flow.<br /><br /> Supported value: **InstallFailoverCluster**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/IACCEPTSQLSERVERLICENSETERMS<br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations.**|Required to acknowledge acceptance of the license terms.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of SQL Server on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERGROUP<br /><br /> **Optional**|Specifies the name of the resource group to be used for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster. It can be the name of an existing cluster group or the name of a new resource group.<br /><br /> Default value:<br /><br /> SQL Server (\<InstanceName>)|  
|PolyBase|/PBENGSVCACCOUNT<br /><br /> **Optional**|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|PolyBase|/PBDMSSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the engine service account.|  
|PolyBase|/PBENGSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual.|  
|PolyBase|/PBPORTRANGE<br /><br /> **Optional**|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  
|PolyBase|/PBSCALEOUT<br /><br /> **Optional**|Specifies if the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance will be used as a part of PolyBase Scale-out computational group. Supported values: **True**, **False**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateEnabled*<br /><br /> **Optional**|Specify whether SQL Server setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, SQL Server setup will include updates that are found.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateSource*<br /><br /> **Optional**|Specify the location where SQL Server setup will obtain product updates. The valid values are "MU" to search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update, a valid folder path, a relative path such as .\MyUpdates or a UNC share. By default, SQL Server setup will search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update or a Windows Update Service through the Windows Server Update Services.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ERRORREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies the error reporting for SQL Server.<br /><br /> For more information, see [Privacy Statement for the Microsoft Error Reporting Service](https://go.microsoft.com/fwlink/?LinkID=868444). Supported values:<br /><br /> 1=enabled<br /><br /> 0=disabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FEATURES<br /><br /> **Required**|Specifies [components](#Feature) to install.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for the parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file will be piped to the console.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTALLSHAREDDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for 64-bit shared components.<br /><br /> Default is `%Program Files%\Microsoft SQL Server`<br /><br /> Cannot be set to `%Program Files(x86)%\Microsoft SQL Server`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTALLSHAREDWOWDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for 32-bit shared components. Supported only on a 64-bit system.<br /><br /> Default is `%Program Files(x86)%\Microsoft SQL Server`<br /><br /> Cannot be set to `%Program Files%\Microsoft SQL Server`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCEDIR<br /><br /> **Optional**|Specifies nondefault installation directory for instance-specific components.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCEID<br /><br /> **Optional**|Specifies a nondefault value for an [InstanceID](#InstanceID).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/PID<br /><br /> **Optional**|Specifies the product key for the edition of SQL Server. If this parameter is not specified, Evaluation is used.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/QS or /QUIETSIMPLE <br /><br /> **Optional**|Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/SQMREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies feature usage reporting for SQL Server.<br /><br />Supported values:<br /><br /> 1=enabled<br /><br /> 0=disabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies the console window would be hidden or closed.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERDISKS<br /><br /> **Optional**|Specifies the list of shared disks to be included in the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster resource group.<br /><br /> Default value: The first drive is used as the default drive for all databases.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERIPADDRESSES<br /><br /> **Required**|Specifies an encoded IP address. The encodings are semicolon-delimited (;) and follow the format \<IP Type>;\<address>;\<network name>;\<subnet mask>. Supported IP types include DHCP, IPv4, and IPv6.<br />You can specify multiple failover cluster IP addresses with a space in between. See the following examples:<br /><br /> `FAILOVERCLUSTERIPADDRESSES=DEFAULT`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv4;DHCP;ClusterNetwork1`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv4;172.16.0.0;ClusterNetwork1;172.31.255.255`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv6;DHCP;ClusterNetwork1`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv6;2001:db8:23:1002:20f:1fff:feff:b3a3;ClusterNetwork1`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERNETWORKNAME<br /><br /> **Required**|Specifies the network name for the new [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster. This name is used to identify the new [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster instance on the network.|  
|SQL Server Agent|/AGTSVCACCOUNT<br /><br /> **Required**|Specifies the account for the SQL Server Agent service.|  
|SQL Server Agent|/AGTSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQL Server Agent service account.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASBACKUPDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] backup files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Backup`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Backup`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASCOLLATION<br /><br /> **Optional**|Specifies the collation setting for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].<br /><br /> Default value: **Latin1_General_CI_AS**|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASCONFIGDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configuration files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Config`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Config`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASDATADIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Data`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Data`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASLOGDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] log files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Log`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Log`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSYSADMINACCOUNTS<br /><br /> **Required**|Specifies the administrator credentials for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASTEMPDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] temporary files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Temp`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Temp`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASPROVIDERMSOLAP<br /><br /> **Optional**|Specifies whether the MSOLAP provider can run in-process.<br /><br /> Default value: 1=enabled|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSERVERMODE<br /><br /> **Optional**|Specifies the server mode of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. Valid values in a cluster scenario are MULTIDIMENSIONAL or TABULAR. **ASSERVERMODE** is case-sensitive. All values must be expressed in upper case. For more information about the valid values, see Install Analysis Services in Tabular Mode.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/INSTALLSQLDATADIR<br /><br /> **Required**|Specifies the data directory for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files.<br /><br /> The data directory must to specified and on a shared cluster disk.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SAPWD<br /><br /> **Required when /SECURITYMODE=SQL**|Specifies the password for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **SA** account.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SECURITYMODE<br /><br /> **Optional**|Specifies the security mode for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If this parameter is not supplied, then Windows-only authentication mode is supported.<br /><br /> Supported value: **SQL**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLBACKUPDIR<br /><br /> **Optional**|Specifies the directory for backup files.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Backup`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLCOLLATION<br /><br /> **Optional**|Specifies the collation settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> The default value is based on the locale of your Windows operating system. For more information, see [Collation Settings in Setup](https://msdn.microsoft.com/library/ms143508%28v=sql.105%29.aspx).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQLSVCACCOUNT.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSYSADMINACCOUNTS<br /><br /> **Required**|Use this parameter to provision logins to be members of the **sysadmin** role.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLUSERDBDIR<br /><br /> **Optional**|Specifies the directory for the data files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBDIR<br /><br /> **Optional**|Specifies the directories for tempdb data files. When specifying more than one directory, separate the directories with a blank space. If multiple directories are specified the tempdb data files will be spread across the directories in a round-robin fashion.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> NOTE: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGDIR<br /><br /> **Optional**|Specifies the directory for tempdb log file.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> Note: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILECOUNT<br /><br /> **Optional**|Specifies the number of tempdb data files to be added by setup. This value can be increased up to the number of cores. Default value:<br /><br /> 1  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8 or the number of cores, whichever is lower for all other editions<br /><br /> **Important:** The primary database file for tempdb will still be tempdb.mdf. The additional tempdb files are named as tempdb_mssql_#.ndf where # represents a unique number for each additional tempdb database file created during setup. The purpose of this naming convention is to make them unique. Uninstalling an instance of SQL Server deletes the files with naming convention tempdb_mssql_#.ndf. Do not use tempdb_mssql_\*.ndf naming convention for user database files.<br /><br /> **Warning:** [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] is not supported for configuring this parameter. Setup installs only 1 tempdb data file.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILESIZE<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb data file.<br/><br/>Default =  8 MB.<br/><br/>Min =  8 MB.<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILEGROWTH<br /><br /> **Optional**|Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024.<br /><br /> Default value: 64<br /><br /> Allowed range: Min = 0, Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILESIZE<br /><br /> **Optional**|Specifies the initial size of the tempdb log file in MB. Setup allows the size up to 1024. <br /> Default value:<br /><br /> 4  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8  for all other editions<br /><br /> Allowed range: Min = default value (4 or 8), Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILEGROWTH<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb log file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLUSERDBLOGDIR<br /><br /> **Optional**|Specifies the directory for the log files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|  
|FILESTREAM|/FILESTREAMLEVEL<br /><br /> **Optional**|Specifies the access level for the FILESTREAM feature. Supported values:<br /><br /> 0 =Disable FILESTREAM support for this instance. (Default value)<br /><br /> 1=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access.<br /><br /> 2=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] and file I/O streaming access. (Not valid for cluster scenarios)<br /><br /> 3=Allow remote clients to have streaming access to FILESTREAM data.|  
|FILESTREAM|/FILESTREAMSHARENAME<br /><br /> **Optional**<br /><br /> **Required when FILESTREAMLEVEL is greater than 1.**|Specifies the name of the windows share in which the FILESTREAM data will be stored.|  
|SQL Server Full Text|/FTSVCACCOUNT<br /><br /> **Optional**|Specifies the account for Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher. ServiceSID will be used to help secure the communication between SQL Server and Full-text Filter Daemon.<br /><br /> If the values are not provided the Full-text Filter Launcher Service will be disabled. You have to use SQL Server Control Manager to change the service account and enable full-text functionality.<br /><br /> Default value: Local Service Account|  
|SQL Server Full Text|/FTSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCACCOUNT<br /><br /> **Required**|Specifies the account for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].<br /><br /> Default value: NT AUTHORITY\NETWORK SERVICE|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] password.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSINSTALLMODE<br /><br /> **Optional**|Specifies the Install mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for the startup account for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
  
 We recommend that you use Service SID instead of domain groups. 
  
##### Additional Notes:  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] are the only components that are cluster-aware. Other features are not cluster-aware and do not have high availability through failover. 
  
###### Sample Syntax:  
 To install a single-node [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster instance with the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], default instance. 
  
```  
setup.exe /q /ACTION=InstallFailoverCluster /InstanceName=MSSQLSERVER /INDICATEPROGRESS /ASSYSADMINACCOUNTS="<DomainName\UserName>" /ASDATADIR=<Drive>:\OLAP\Data /ASLOGDIR=<Drive>:\OLAP\Log /ASBACKUPDIR=<Drive>:\OLAP\Backup /ASCONFIGDIR=<Drive>:\OLAP\Config /ASTEMPDIR=<Drive>:\OLAP\Temp /FAILOVERCLUSTERDISKS="<Cluster Disk Resource Name - for example, 'Disk S:'" /FAILOVERCLUSTERNETWORKNAME="<Insert Network Name>" /FAILOVERCLUSTERIPADDRESSES="IPv4;xx.xxx.xx.xx;Cluster Network;xxx.xxx.xxx.x" /FAILOVERCLUSTERGROUP="MSSQLSERVER" /Features=AS,SQL /ASSVCACCOUNT="<DomainName\UserName>" /ASSVCPASSWORD="xxxxxxxxxxx" /AGTSVCACCOUNT="<DomainName\UserName>" /AGTSVCPASSWORD="xxxxxxxxxxx" /INSTALLSQLDATADIR="<Drive>:\<Path>\MSSQLSERVER" /SQLCOLLATION="SQL_Latin1_General_CP1_CS_AS" /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="xxxxxxxxxxx" /SQLSYSADMINACCOUNTS="<DomainName\UserName> /IACCEPTSQLSERVERLICENSETERMS  
```  
  
#### Prepare Failover Cluster Parameters  
 Use the parameters in the following table to develop command-line scripts for failover cluster prepare. This is the first step in advanced cluster installation, where you have to prepare the failover cluster instances on all the nodes of the failover cluster. For more information, see [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md). 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the failover cluster prepare work flow.<br /><br /> Supported value: **PrepareFailoverCluster**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/IACCEPTSQLSERVERLICENSETERMS<br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations.**|Required to acknowledge acceptance of the license terms.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of SQL Server on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateEnabled*<br /><br /> **Optional**|Specify whether SQL Server setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, SQL Server setup will include updates that are found.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateSource*<br /><br /> **Optional**|Specify the location where SQL Server setup will obtain product updates. The valid values are "MU" to search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update, a valid folder path, a relative path such as `.\MyUpdates` or a UNC share. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update or a Windows Update Service through the Windows Server Update Services.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ERRORREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies the error reporting for SQL Server.<br /><br /> For more information, see [Privacy Statement for the Microsoft Error Reporting Service](https://go.microsoft.com/fwlink/?LinkID=868444). Supported values:<br /><br /> 0=disabled<br /><br /> 1=enabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FEATURES<br /><br /> **Required**|Specifies [components](#Feature) to install.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for the parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file will be piped to the console.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTALLSHAREDDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for 64-bit shared components.<br /><br /> Default is `%Program Files%\Microsoft SQL Server`<br /><br /> Cannot be set to `%Program Files(x86)%\Microsoft SQL Server`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTALLSHAREDWOWDIR<br /><br /> **Optional**|Specifies a nondefault installation directory for 32-bit shared components. Supported only on a 64-bit system.<br /><br /> Default is `%Program Files(x86)%\Microsoft SQL Server`<br /><br /> Cannot be set to `%Program Files%\Microsoft SQL Server`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCEDIR<br /><br /> **Optional**|Specifies nondefault installation directory for instance specific components.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCEID<br /><br /> **Optional**|Specifies a nondefault value for an [InstanceID](#InstanceID).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|PolyBase|/PBENGSVCACCOUNT<br /><br /> **Optional**|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|PolyBase|/PBDMSSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the engine service account.|  
|PolyBase|/PBENGSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual.|  
|PolyBase|/PBPORTRANGE<br /><br /> **Optional**|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  
|PolyBase|/PBSCALEOUT<br /><br /> **Optional**|Specifies if the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance will be used as a part of PolyBase Scale-out computational group. Supported values: **True**, **False**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/PID<br /><br /> **Optional**|Specifies the product key for the edition of SQL Server. If this parameter is not specified,<br /><br /> Evaluation is used.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/QS or /QUIETSIMPLE <br /><br /> **Optional**|Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/SQMREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies feature usage reporting for SQL Server.<br /><br />Supported values:<br /><br /> 0=disabled<br /><br /> 1=enabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
|SQL Server Agent|/AGTSVCACCOUNT<br /><br /> **Required**|Specifies the account for the SQL Server Agent service.|  
|SQL Server Agent|/AGTSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQL Server Agent service account.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSVCACCOUNT<br /><br /> **Required**|Specifies the account for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for the SQL Server service.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQLSVCACCOUNT.|  
|FILESTREAM|/FILESTREAMLEVEL<br /><br /> **Optional**|Specifies the access level for the FILESTREAM feature. Supported values:<br /><br /> 0 =Disable FILESTREAM support for this instance. (Default value)<br /><br /> 1=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access.<br /><br /> 2=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] and file I/O streaming access. (Not valid for Cluster scenarios)<br /><br /> 3=Allow remote clients to have streaming access to FILESTREAM data.|  
|FILESTREAM|/FILESTREAMSHARENAME<br /><br /> **Optional**<br /><br /> **Required** when FILESTREAMLEVEL is greater than 1.|Specifies the name of the windows share in which the FILESTREAM data will be stored.|  
|SQL Server Full Text|/FTSVCACCOUNT<br /><br /> **Optional**|Specifies the account for Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher. ServiceSID will be used to help secure the communication between SQL Server and Full-text Filter Daemon.<br /><br /> If the values are not provided the Full-text Filter Launcher Service will be disabled. You have to use SQL Server Control Manager to change the service account and enable full-text functionality.<br /><br /> Default value: Local Service Account|  
|SQL Server Full Text|/FTSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCACCOUNT<br /><br /> **Required**|Specifies the account for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].<br /><br /> Default value: NT AUTHORITY\NETWORK SERVICE|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] password.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSINSTALLMODE<br /><br /> **Available only on Files only mode.**|Specifies the Install mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for the startup account for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
  
 We recommend that you use Service SID instead of domain groups. 
  
###### Sample Syntax:  
 To perform the "Preparation" step of a failover cluster advanced installation scenario for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. 
  
 Run the following command at the command prompt to prepare a default instance:  
  
```  
setup.exe /q /ACTION=PrepareFailoverCluster /InstanceName=MSSQLSERVER /Features=AS,SQL /INDICATEPROGRESS /ASSVCACCOUNT="<DomainName\UserName>" /ASSVCPASSWORD="xxxxxxxxxxx" /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="xxxxxxxxxxx" /AGTSVCACCOUNT="<DomainName\UserName>" /AGTSVCPASSWORD="xxxxxxxxxxx" /IACCEPTSQLSERVERLICENSETERMS  
```  
  
 Run the following command at the command prompt to prepare a named instance:  
  
```  
setup.exe /q /ACTION=PrepareFailoverCluster /InstanceName="<Insert Instance name>" /Features=AS,SQL /INDICATEPROGRESS /ASSVCACCOUNT="<DomainName\UserName>" /ASSVCPASSWORD="xxxxxxxxxxx" /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="xxxxxxxxxxx" /AGTSVCACCOUNT="<DomainName\UserName>" /AGTSVCPASSWORD="xxxxxxxxxxx" /IACCEPTSQLSERVERLICENSETERMS  
```  
  
#### Complete Failover Cluster Parameters  
 Use the parameters in the following table to develop command-line scripts for failover cluster complete. This is the second step in the advanced failover cluster install option. After you have run prepare on all the failover cluster nodes, you run this command on the node that owns the shared disks. For more information, see [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md). 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the failover cluster complete work flow.<br /><br /> Supported value: **CompleteFailoverCluster**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of SQL Server on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERGROUP<br /><br /> **Optional**|Specifies the name of the resource group to be used for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster. It can be the name of an existing cluster group or the name of a new resource group.<br /><br /> Default value:<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (\<InstanceName>)|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ERRORREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies the error reporting for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> For more information, see [Privacy Statement for the Microsoft Error Reporting Service](https://go.microsoft.com/fwlink/?LinkID=868444). Supported values:<br /><br /> 1=enabled<br /><br /> 0=disabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for the parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file will be piped to the console.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md)  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/PID<br /><br /> **Optional**|Specifies the product key for the edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If this parameter is not specified, Evaluation is used.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/QS or /QUIETSIMPLE <br /><br /> **Optional**|Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/SQMREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies feature usage reporting for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />Supported values:<br /><br /> 1=enabled<br /><br /> 0=disabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERDISKS<br /><br /> **Optional**|Specifies the list of shared disks to be included in the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster resource group.<br /><br /> Default value:<br /><br /> The first drive is used as the default drive for all databases.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERIPADDRESSES<br /><br /> **Required**|Specifies an encoded IP address. The encodings are semicolon-delimited (;) and follow the format \<IP Type>;\<address>;\<network name>;\<subnet mask>. Supported IP types include DHCP, IPv4, and IPv6.<br />You can specify multiple failover cluster IP addresses with a space in between. See the following examples:<br /><br /> `FAILOVERCLUSTERIPADDRESSES=DEFAULT`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv4;DHCP;ClusterNetwork1`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv4;172.16.0.0;ClusterNetwork1;172.31.255.255`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv6;DHCP;ClusterNetwork1`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv6;2001:db8:23:1002:20f:1fff:feff:b3a3;ClusterNetwork1`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERNETWORKNAME<br /><br /> **Required**|Specifies the network name for the new [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster. This name is used to identify the new [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster instance on the network.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIRMIPDEPENDENCYCHANGE|Indicates the consent to set the IP address resource dependency to OR for multi-subnet failover clusters. For more information, see [Create a new SQL Server Failover Cluster &#40;Setup&#41;](../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md). Supported Values:<br /><br /> 0 = False (Default)<br /><br /> 1 = True|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASBACKUPDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] backup files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Backup`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Backup`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASCOLLATION<br /><br /> **Optional**|Specifies the collation setting for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].<br /><br /> Default value: **Latin1_General_CI_AS**|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASCONFIGDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configuration files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Config`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Config`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASDATADIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Data`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Data`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASLOGDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] log files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Log`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Log`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSERVERMODE<br /><br /> **Optional**|Specifies the server mode of the Analysis Services instance. Valid values in a cluster scenario are MULTIDIMENSIONAL or TABULAR. **ASSERVERMODE** is case-sensitive. All values must be expressed in upper case. For more information about the valid values, see Install Analysis Services in Tabular Mode.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSYSADMINACCOUNTS<br /><br /> **Required**|Specifies the administrator credentials for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASTEMPDIR<br /><br /> **Optional**|Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] temporary files.Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Temp`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Temp`|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASPROVIDERMSOLAP<br /><br /> **Optional**|Specifies whether the MSOLAP provider can run in-process.<br /><br /> Default value: 1=enabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/INSTALLSQLDATADIR<br /><br /> **Required**|Specifies the data directory for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files.<br /><br /> The data directory must to specified and on a shared cluster disk.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SAPWD<br /><br /> **Required when /SECURITYMODE=SQL**|Specifies the password for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **SA** account.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SECURITYMODE<br /><br /> **Optional**|Specifies the security mode for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If this parameter is not supplied, then Windows-only authentication mode is supported<br /><br /> Supported value: **SQL**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLBACKUPDIR<br /><br /> **Optional**|Specifies the directory for backup files.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Backup`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLCOLLATION<br /><br /> **Optional**|Specifies the collation settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> The default value is based on the locale of your Windows operating system. For more information, see [Collation Settings in Setup](https://msdn.microsoft.com/library/ms143508%28v=sql.105%29.aspx).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSYSADMINACCOUNTS<br /><br /> **Required**|Use this parameter to provision logins to be members of the sysadmin role.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLUSERDBDIR<br /><br /> **Optional**|Specifies the directory for the data files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLUSERDBLOGDIR<br /><br /> **Optional**|Specifies the directory for the log files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSINSTALLMODE<br /><br /> **Available on files only mode.**|Specifies the Install mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBDIR<br /><br /> **Optional**|Specifies the directories for tempdb data files. When specifying more than one directory, separate the directories with a blank space. If multiple directories are specified the tempdb data files will be spread across the directories in a round-robin fashion.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> NOTE: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGDIR<br /><br /> **Optional**|Specifies the directory for tempdb log file.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> Note: This parameter is added to RebuildDatabase scenario as well.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILECOUNT<br /><br /> **Optional**|Specifies the number of tempdb data files to be added by setup. This value can be increased up to the number of cores. Default value:<br /><br /> 1  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8 or the number of cores, whichever is lower for all other editions.<br /><br /> **Important:** The primary database file for tempdb will still be tempdb.mdf. The additional tempdb files are named as tempdb_mssql_#.ndf where # represents a unique number for each additional tempdb database file created during setup. The purpose of this naming convention is to make them unique. Uninstalling an instance of SQL Server deletes the files with naming convention tempdb_mssql_#.ndf. Do not use tempdb_mssql_\*.ndf naming convention for user database files.<br /><br /> **Warning:** [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] is not supported for configuring this parameter. Setup installs only 1 tempdb data file.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILESIZE<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb data file.<br/><br/>Default = 8 MB.<br/><br/>Min = 8 MB.<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBFILEGROWTH<br /><br /> **Optional**|Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024.<br /><br /> Default value: 64<br /><br /> Allowed range: Min = 0, Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILESIZE<br /><br /> **Optional**|Specifies the initial size of the tempdb log file in MB. Setup allows the size up to 1024. <br /> Default value:<br /><br /> 4  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8  for all other editions<br /><br /> Allowed range: Min = default value (4 or 8), Max = 1024|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLTEMPDBLOGFILEGROWTH<br /><br /> **Optional**|Introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Specifies the initial size of each tempdb log file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
  
###### Sample Syntax:  
 To perform the "Completion" step of a failover cluster advanced installation scenario for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Run the following command on the computer that will be the active node in the failover cluster to make it usable. You must run the "CompleteFailoverCluster" action on the node that owns the shared disk in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] failover cluster. 
  
 Run the following command at the command prompt to complete failover cluster installation for a default instance:  
  
```  
setup.exe /q /ACTION=CompleteFailoverCluster /InstanceName=MSSQLSERVER /INDICATEPROGRESS /ASSYSADMINACCOUNTS="<DomainName\Username>" /ASDATADIR=<Drive>:\OLAP\Data /ASLOGDIR=<Drive>:\OLAP\Log /ASBACKUPDIR=<Drive>:\OLAP\Backup /ASCONFIGDIR=<Drive>:\OLAP\Config /ASTEMPDIR=<Drive>:\OLAP\Temp /FAILOVERCLUSTERDISKS="<Cluster Disk Resource Name - for example, 'Disk S:'>:" /FAILOVERCLUSTERNETWORKNAME="<Insert FOI Network Name>" /FAILOVERCLUSTERIPADDRESSES="IPv4;xx.xxx.xx.xx;Cluster Network;xxx.xxx.xxx.x" /FAILOVERCLUSTERGROUP="MSSQLSERVER" /INSTALLSQLDATADIR="<Drive>:\<Path>\MSSQLSERVER" /SQLCOLLATION="SQL_Latin1_General_CP1_CS_AS" /SQLSYSADMINACCOUNTS="<DomainName\UserName>"  
```  
  
 Run the following command at the command prompt to complete failover cluster installation for a named instance:  
  
```  
setup.exe /q /ACTION=CompleteFailoverCluster /InstanceName="<Insert Instance Name>" /INDICATEPROGRESS /ASSYSADMINACCOUNTS="<DomainName\UserName>" /ASDATADIR=<Drive>:\KATMAI\Data /ASLOGDIR=<drive>:\KATMAI\Log /ASBACKUPDIR=<Drive>:\KATMAI\Backup /ASCONFIGDIR=<Drive>:\KATMAI\Config /ASTEMPDIR=<Drive>:\KATMAI\Temp /FAILOVERCLUSTERDISKS="<Cluster Disk Resource Name - for example, 'Disk S:'>" /FAILOVERCLUSTERNETWORKNAME="CompNamedFOI" /FAILOVERCLUSTERIPADDRESSES="IPv4;xx.xxx.xx.xx;ClusterNetwork1;xxx.xxx.xxx.x" /FAILOVERCLUSTERGROUP="<Insert New Group Name>" /INSTALLSQLDATADIR="<Drive>:\<Path>\MSSQLSERVER_KATMAI" /SQLCOLLATION="SQL_Latin1_General_CP1_CS_AS" /SQLSYSADMINACCOUNTS="<DomainName\Username>"  
```  
  
#### Upgrade Failover Cluster Parameters  
 Use the parameters in the following table to develop command-line scripts for failover cluster upgrade. For more information, see [Upgrade a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover Cluster Instance &#40;Setup&#41;](../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance-setup.md) and [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md). 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate the installation workflow.<br /><br /> Supported value: **Upgrade**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/IACCEPTSQLSERVERLICENSETERMS<br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations.**|Required to acknowledge acceptance of the license terms.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of SQL Server on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateEnabled*<br /><br /> **Optional**|Specify whether SQL Server setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, SQL Server setup will include updates that are found.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateSource*<br /><br /> **Optional**|Specify the location where SQL Server setup will obtain product updates. The valid values are "MU" to search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update, a valid folder path, a relative path such as `.\MyUpdates` or a UNC share. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update or a Windows Update Service through the Windows Server Update Services.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ERRORREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. <br/><br/>To manage how error feedback is sent to Microsoft, see [How to configure [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to send feedback to Microsoft](https://support.microsoft.com/kb/3153756). <br/><br/>In older versions this specifies the error reporting for SQL Server.<br /><br /> For more information, see [Privacy Statement for the Microsoft Error Reporting Service](https://go.microsoft.com/fwlink/?LinkID=868444). Supported values:<br /><br /> 0=disabled<br /><br /> 1=enabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for the parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file will be piped to the console.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ INSTANCEDIR<br /><br /> **Optional**|Specifies a non-default installation directory for shared components.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCEID<br /><br /> **Required when you upgrade from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or higher.**<br /><br /> **Optional when you upgrade from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]**|Specifies a nondefault value for an [InstanceID](#InstanceID).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/PID<br /><br /> **Optional**|Specifies the product key for the edition of SQL Server. If this parameter is not specified, Evaluation is used.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/SQMREPORTING<br /><br /> **Optional**|Has no effect in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. In older versions this specifies feature usage reporting for SQL Server.<br /><br />Supported values:<br /><br /> 0=disabled<br /><br /> 1=enabled|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERROLLOWNERSHIP|Specifies the [failover behavior](#RollOwnership) during upgrade.|  
|SQL Server Browser Service|/BROWSERSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for SQL Server Browser service. Supported values:<br /><br /> **Automatic**<br /><br /> **Disabled**<br /><br /> **Manual**|  
|SQL Server Full-Text|/FTUPGRADEOPTION<br /><br /> **Optional**|Specifies the Full-Text catalog upgrade option. Supported values:<br /><br /> **REBUILD**<br /><br /> **RESET**<br /><br /> **IMPORT**|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCACCOUNT<br /><br /> **Required**|Specifies the account for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].<br /><br /> Default value: NT AUTHORITY\NETWORK SERVICE|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] password.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCStartupType<br /><br /> **Optional**|Specifies the [startup](#Accounts) mode for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSUPGRADEDATABASEACCOUNT<br /><br /> **Optional**|The property is only used when upgrading a SharePoint mode Report Server that is version 2008 R2 or earlier. Additional upgrade operations are performed for report servers that use the older SharePoint mode architecture, which was changed in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If this option is not included with the command line installation, the default service account for the old report server instance is used. If this property is used, supply the password for the account using the **/RSUPGRADEPASSWORD** property.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSUPGRADEPASSWORD<br /><br /> **Optional**|Password of the existing Report Server service account.|  
  
####  <a name="AddNode"></a> Add Node Parameters  
 Use the parameters in the following table to develop command-line scripts for AddNode. 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate AddNode work flow.<br /><br /> Supported value: **AddNode**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/IACCEPTSQLSERVERLICENSETERMS<br /><br /> **Required only when the /Q or /QS parameter is specified for unattended installations.**|Required to acknowledge acceptance of the license terms.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ENU<br /><br /> **Optional**|Use this parameter to install the English version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateEnabled*<br /><br /> **Optional**|Specify whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, SQL Server setup will include updates that are found.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/*UpdateSource*<br /><br /> **Optional**|Specify the location where SQL Server setup will obtain product updates. The valid values are "MU" to search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update, a valid folder path, a relative path such as `.\MyUpdates` or a UNC share. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will search [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update or a Windows Update Service through the Windows Server Update Services.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for the parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file will be piped to the console.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|PolyBase|/PBENGSVCACCOUNT<br /><br /> **Optional**|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|PolyBase|/PBDMSSVCPASSWORD<br /><br /> **Optional**|Specifies the password for the engine service account.|  
|PolyBase|/PBENGSVCSTARTUPTYPE<br /><br /> **Optional**|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual.|  
|PolyBase|/PBPORTRANGE<br /><br /> **Optional**|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  
|PolyBase|/PBSCALEOUT<br /><br /> **Optional**|Specifies if the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance will be used as a part of PolyBase Scale-out computational group. Supported values: **True**, **False**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/PID<br /><br /> **Optional**|Specifies the product key for the edition of SQL Server. If this parameter is not specified, Evaluation is used.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/QS or /QUIETSIMPLE <br /><br /> **Optional**|Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/FAILOVERCLUSTERIPADDRESSES<br /><br /> **Required**|Specifies an encoded IP address. The encodings are semicolon-delimited (;) and follow the format \<IP Type>;\<address>;\<network name>;\<subnet mask>. Supported IP types include DHCP, IPv4, and IPv6.<br />You can specify multiple failover cluster IP addresses with a space in between. See the following examples:<br /><br /> `FAILOVERCLUSTERIPADDRESSES=DEFAULT`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv4;DHCP;ClusterNetwork1`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv4;172.16.0.0;ClusterNetwork1;172.31.255.255`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv6;DHCP;ClusterNetwork1`<br /><br /> `FAILOVERCLUSTERIPADDRESSES=IPv6;2001:db8:23:1002:20f:1fff:feff:b3a3;ClusterNetwork1`<br /><br /> <br /><br /> For more information, see [Add or Remove Nodes in a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover Cluster &#40;Setup&#41;](../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIRMIPDEPENDENCYCHANGE<br /><br /> **Required**|Indicates the consent to set the IP address resource dependency to OR for multi-subnet failover clusters. For more information, see [Add or Remove Nodes in a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover Cluster &#40;Setup&#41;](../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md). Supported values:<br /><br /> 0 = False (Default)<br /><br /> 1 = True|  
|SQL Server Agent|/AGTSVCACCOUNT<br /><br /> **Required**|Specifies the account for the SQL Server Agent service.|  
|SQL Server Agent|/AGTSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQL Server Agent service account.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSVCACCOUNT<br /><br /> **Required**|Specifies the account for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCACCOUNT<br /><br /> **Required**|Specifies the startup account for the SQL Server service.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the password for SQLSVCACCOUNT.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] password.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSINSTALLMODE<br /><br /> **Available in Files only mode**|Specifies the Install mode for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCPASSWORD<br /><br /> [Required](#Accounts)|Specifies the startup account password for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service.|  
  
##### Additional Notes:  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] are the only components that are cluster-aware. Other features are not cluster-aware and do not have high availability through failover. 
  
###### Sample Syntax:  
 To add a node to an existing failover cluster instance with the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. 
  
```  
setup.exe /q /ACTION=AddNode /INSTANCENAME="<Insert Instance Name>" /SQLSVCACCOUNT="<SQL account that is used on other nodes>" /SQLSVCPASSWORD="<password for SQL account>" /AGTSVCACCOUNT="<SQL Server Agent account that is used on other nodes>", /AGTSVCPASSWORD="<SQL Server Agent account password>" /ASSVCACCOUNT="<AS account that is used on other nodes>" /ASSVCPASSWORD="<password for AS account>" /INDICATEPROGRESS /IACCEPTSQLSERVERLICENSETERMS /FAILOVERCLUSTERIPADDRESSES="IPv4;xx.xxx.xx.xx;ClusterNetwork1;xxx.xxx.xxx.x" /CONFIRMIPDEPENDENCYCHANGE=0  
```  
  
#### Remove Node Parameters  
 Use the parameters in the following table to develop command-line scripts for RemoveNode. To uninstall a failover cluster, you must run RemoveNode on each failover cluster node. For more information, see [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md). 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Parameter|Description|  
|-----------------------------------------|---------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/ACTION<br /><br /> **Required**|Required to indicate RemoveNode work flow.<br /><br /> Supported value: **RemoveNode**|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIGURATIONFILE<br /><br /> **Optional**|Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HELP, H, ?<br /><br /> **Optional**|Displays the usage options for the parameters.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INDICATEPROGRESS<br /><br /> **Optional**|Specifies that the verbose Setup log file will be piped to the console.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/INSTANCENAME<br /><br /> **Required**|Specifies a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance name.<br /><br /> For more information, see [Instance Configuration](../../database-engine/install-windows/install-sql-server.md).|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/Q or /QUIET <br /><br /> **Optional**|Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/QS or /QUIETSIMPLE<br /><br /> **Optional**|Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/HIDECONSOLE<br /><br /> **Optional**|Specifies that the console window is hidden or closed.|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Setup Control|/CONFIRMIPDEPENDENCYCHANGE<br /><br /> **Required**|Indicates the consent to set the IP address resource dependency from OR to AND for multi-subnet failover clusters. For more information, see [Add or Remove Nodes in a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover Cluster &#40;Setup&#41;](../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md). Supported values:<br /><br /> 0 = False (Default)<br /><br /> 1 = True|  
  
###### Sample Syntax:  
 To remove a node from an existing failover cluster instance with the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. 
  
```  
setup.exe /q /ACTION=RemoveNode /INSTANCENAME="<Insert Instance Name>" [/INDICATEPROGRESS] /CONFIRMIPDEPENDENCYCHANGE=0  
```  
  
##  <a name="Accounts"></a> Service Account Parameters  
 You can configure the SQL Server services by using a built-in account, local account, or domain account. 
  
> [!NOTE] 
> When you use a managed service account, virtual account, or a built-in account, you should not specify the corresponding password parameters. For more information about these service accounts, see **New Account Types Available with [!INCLUDE[win7](../../includes/win7-md.md)] and [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)]** section in [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). 
  
 For more information about service account configuration, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). 
  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] component|Account parameter|Password parameter|Startup type|  
|-----------------------------------------|-----------------------|------------------------|------------------|  
|SQL Server Agent|/AGTSVCACCOUNT|/AGTSVCPASSWORD|/AGTSVCSTARTUPTYPE|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|/ASSVCACCOUNT|/ASSVCPASSWORD|/ASSVCSTARTUPTYPE|  
|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|/SQLSVCACCOUNT|/SQLSVCPASSWORD|/SQLSVCSTARTUPTYPE|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|/ISSVCACCOUNT|/ISSVCPASSWORD|/ISSVCSTARTUPTYPE|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|/RSSVCACCOUNT|/RSSVCPASSWORD|/RSSVCSTARTUPTYPE|  
  
##  <a name="Feature"></a> Feature Parameters  
 To install specific features, use the /FEATURES parameter and specify the parent feature or feature values in the following table. For a list of features that are supported by the editions of SQL Server, see [Editions and Supported Features for [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]](../../sql-server/editions-and-supported-features-for-sql-server-2016.md). 
  
|Parent feature parameter|Feature parameter|Description|  
|:---|:---|:---|  
|SQL||Installs the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], Replication, Fulltext, and [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)].|  
||SQLEngine|Installs just the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
||Replication|Installs the Replication component along with [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
||FullText|Installs the FullText component along with [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
||DQ|Copies the files required for completing the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation. After completing SQL Server installation, you must run the DQSInstaller.exe file to complete the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation. For more information, see [Run DQSInstaller.exe to Complete Data Quality Server Installation](../../data-quality-services/install-windows/run-dqsinstaller-exe-to-complete-data-quality-server-installation.md). This also installs [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
||PolyBase|Installs PolyBase components.|  
||AdvancedAnalytics|Installs [SQL Server 2017 Machine Learning Services](https://docs.microsoft.com/sql/advanced-analytics/install/sql-machine-learning-services-windows-install) or [SQL Server 2016 R Services (In-Database)](https://docs.microsoft.com/sql/advanced-analytics/install/sql-r-services-windows-install).|  
||SQL_INST_MR |Applies to [SQL Server 2017 Machine Learning Services](https://docs.microsoft.com/sql/advanced-analytics/install/sql-machine-learning-services-windows-install). Pair with **AdvancedAnalytics** to install R Open and proprietary R packages.|  
||SQL_INST_MPY|Applies to [SQL Server 2017 Machine Learning Services](https://docs.microsoft.com/sql/advanced-analytics/install/sql-machine-learning-services-windows-install). Pair with **AdvancedAnalytics** to install Anaconda and proprietary Python packages.|  
|AS||Installs all [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components.|  
|RS||Installs all [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components.|  
|RS_SHP||Installs [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components for SharePoint.|  
|RS_SHPWFE||Installs [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-In for SharePoint products. |  
|DQC||Installs [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)].|  
|IS||Installs all [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components.|  
|MDS||Installs [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)].|  
|SQL_SHARED_MPY||Installs Python packages for [SQL Server 2017 Machine Learning Server (Standalone)](https://docs.microsoft.com/sql/advanced-analytics/install/sql-machine-learning-standalone-windows-install) |  
|SQL_SHARED_MR||Installs R packages for [SQL Server 2016 R Server (Standalone)](https://docs.microsoft.com/sql/advanced-analytics/install/sql-r-standalone-windows-install) or SQL Server 2017 Machine Learning Server (Standalone) |  
|Tools*||Installs client tools and SQL Server Books Online components.|  
||BC|Installs backward compatibility components.|  
||Conn|Installs connectivity components.|
||DREPLAY_CTLR|Installs Distributed Replay controller|  
||DREPLAY_CLT|Installs Distributed Replay client|  
||SNAC_SDK|Installs SDK for [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server Native Client|  
||SDK|Installs the software development kit.|  
||LocalDB**|Installs LocalDB, an execution mode of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] targeted to program developers.|  

*[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) is now in a stand-alone installer that is separate from the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installer. For details, see [Install SQL Server Management Studio from the command line](../../ssms/download-sql-server-management-studio-ssms.md).

**LocalDB is an option when installing any SKU of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. For more information, see [SQL Server 2016 Express LocalDB](../../database-engine/configure-windows/sql-server-2016-express-localdb.md). 
  
### Feature parameter examples:  
  
|Parameter and values|Description| 
|---------------|-----------------|  
|/FEATURES=SQLEngine|Installs the [!INCLUDE[ssDE](../../includes/ssde-md.md)] without replication and full-text.|  
|/FEATURES=SQLEngine,FullText|Installs the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and full-text.|  
|/FEATURES=SQL,Tools|Installs the complete [!INCLUDE[ssDE](../../includes/ssde-md.md)] and all tools.|  
|/FEATURES=BOL|Installs SQL Server Books Online components to view and manage help content.|  
|/FEATURES=SQLEngine,PolyBase|Installs the PolyBase engine.|  
  
##  <a name="RoleParameters"></a> Role Parameters  
 The setup role or /Role parameter is used to install a preconfigured selection of features. The SSAS roles install an SSAS instance in either an existing SharePoint farm, or a new un-configured farm. Two setup roles are provided to support each scenario. You can only choose one setup role to install at a time. If you choose a setup role, Setup installs the features and components that belong to the role. You cannot vary the features and components that are designated for that role. For more information about how to use the feature role parameter, see [Install Power Pivot from the Command Prompt](https://msdn.microsoft.com/7f1f2b28-c9f5-49ad-934b-02f2fa6b9328). 
  
 The AllFeatures_WithDefaults role is the default behavior for editions of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] and reduces the number of dialog boxes presented to the user. It can be specified from the command line when installing a SQL Server edition that is not [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. 
  
|Role|Description|Installs...|  
|----------|-----------------|---------------|  
|SPI_AS_ExistingFarm|Installs [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] as a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] named instance on an existing [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] farm or standalone server.|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] calculation engine, preconfigured for in-memory data storage and processing.<br /><br /> [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] solution packages<br /><br /> Installer program for the [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)]<br /><br /> SQL Server Books Online|  
|SPI_AS_NewFarm|Installs [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and [!INCLUDE[ssDE](../../includes/ssde-md.md)] as a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] named instance on a new, un-configured Office [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] farm or standalone server. SQL Server Setup will configure the farm during feature role installation.|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] calculation engine, preconfigured for in-memory data storage and processing.<br /><br /> [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] solution packages<br /><br /> SQL Server Books Online<br /><br /> [!INCLUDE[ssDE](../../includes/ssde-md.md)]<br /><br /> Configuration Tools<br /><br /> [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]|  
|AllFeatures_WithDefaults|Installs all features that are available with the current edition.<br /><br /> Adds the current user to the SQL Server **sysadmin** fixed server role.<br /><br /> On [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher and when the operating system is not a domain controller, the [!INCLUDE[ssDE](../../includes/ssde-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are defaulted to use the NTAUTHORITY\NETWORK SERVICE account, and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is defaulted to use the NTAUTHORITY\NETWORK SERVICE account.<br /><br /> This role is enabled by default in editions of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. For all other editions, this role is not enabled but can be specified through the UI or with command line parameters.|For editions of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], installs only those features available in the edition. For other editions, installs all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features.<br /><br /> The **AllFeatures_WithDefaults** parameter can be combined with other parameters which override the **AllFeatures_WithDefaults** parameter settings. For example, using the **AllFeatures_WithDefaults** parameter and the **/Features=RS** parameter overrides the command to install all features and only installs [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], but honors the **AllFeatures_WithDefaults** parameter to use the default service account for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].<br /><br /> When using the **AllFeatures_WithDefaults** parameter along with the **/ADDCURRENTUSERASSQLADMIN=FALSE** the provisioning dialog is not auto populated with the current user. Add **/AGTSVCACCOUNT** and **/AGTSVCPASSWORD** to specify a service account and password for the SQL Server Agent.|  
  
##  <a name="RollOwnership"></a> Controlling Failover Behavior using the /FAILOVERCLUSTERROLLOWNERSHIP Parameter  
To upgrade a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] failover cluster to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you must run the Setup on one failover cluster node at a time, starting with the passive nodes. Setup determines when to fail over to the upgraded node, depending on the total number of nodes in the failover cluster instance, and the number of nodes that have already been upgraded. When half of the nodes or more have already been upgraded, Setup by default will cause a failover to an upgraded node. 
 
To control the failover behavior of cluster nodes during the upgrade process, run the upgrade operation at the command prompt and use the /FAILOVERCLUSTERROLLOWNERSHIP parameter to control the failover behavior before the upgrade operation takes the node offline. Use of this parameter is as follows:  
  
-   /FAILOVERCLUSTERROLLOWNERSHIP=0 will not roll cluster ownership (move group) to upgraded nodes, and does not add this node to the list of possible owners of the SQL Server cluster at the end of upgrade. 
  
-   /FAILOVERCLUSTERROLLOWNERSHIP=1 will roll cluster ownership (move group) to upgraded nodes, and will add this node to the list of possible owners of the SQL Server cluster at the end of upgrade. 
  
-   /FAILOVERCLUSTERROLLOWNERSHIP=2 is the default setting. It will be used if this parameter is not specified. This setting indicates that SQL Server Setup will manage cluster ownership (move group) as needed. 
  
##  <a name="InstanceID"></a> Instance ID or InstanceID Configuration  
 The Instance ID or /InstanceID parameter is used for specifying where you can install the instance components and the registry path of the instance. The value of INSTANCEID is a string and should be unique. 
  
-   SQL Instance ID: `MSSQLxx.<INSTANCEID>`  
  
-   AS Instance ID: `MSASxx.<INSTANCEID>`  
  
-   RS Instance ID: `MSRSxx.<INSTANCEID>`  
  
The instance-aware components are installed to the following locations:  
  
`%Program Files%\Microsoft SQL Server\<SQLInstanceID>`  
  
`%Program Files%\Microsoft SQL Server\<ASInstanceID>`  
  
`%Program Files%\Microsoft SQL Server\<RSInstanceID>`  
  
> [!NOTE]
> If INSTANCEID is not specified on the command line, then by default Setup substitute \<INSTANCEID> with the \<INSTANCENAME>. 
  
## See Also  
 [Install SQL Server 2016 from the Installation Wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)   
 [SQL Server Failover Cluster Installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md)   
 [Install SQL Server 2016 Business Intelligence Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)     
  
