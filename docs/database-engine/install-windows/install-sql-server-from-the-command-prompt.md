---
title: "SQL Server installation - Command Prompt parameters"
ms.custom: ""
ms.date: 07/26/2019
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
  

## SQL Server setup control

The following values are used to describe the workflow the specified parameter applies to:
- **I**: Installing SQL Server and associated components.  
- **S**:  Syspreping an image for SQL Server installation. 
- **Up**: Upgrading SQL Server and associated components. 
- **Rp** : Reparing SQL Server and associated components. 
- **Rb** : Rebuilding the system databases. 
- **Un**: Uninstalling SQL Server and associated components. 
- **FC**: Failover cluster preparation and SQL Server installation to a failover cluster instance. 
- **All**: Used in all aforementioned workflows. 


|**Parameter** | **Values** | **Description**| 
| :---------   | :--------- |  :--------------|
| `/ACTION = ` <br /> **Required** <br /> Workflow: **All** | `Install`, `PrepareImage`, `CompleteImage`, `Upgrade`, `Repair`, `Rebuilddatabase`, `Uninstall`, `PrepareFailoverCluster`, `InstallFailoverCluster`, `CompleteFailoverCluster`, `AddNode`, `RemoveNode` | Determines the SQL Server workflow, such as installing SQL Server, preparing a Sysprep image, or preparing a failover cluster for a SQL Server installation.  |
Services. Required to acknowledge acceptance of R Open license terms.|.
| `/ENU` <br /> Optional <br /> Workflow: **I** | Blank  | Use this parameter to install the English version of SQL Server on a localized operating system when the installation media includes language packs for both English and the language corresponding to the operating system.|
| `/UpdateEnabled` <br /> Optional <br /> Workflow: **I** | Blank |  Specify whether SQL Server setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, SQL Server setup will include updates that are found. |
| `/UpdateSource =` <br /> Optional <br /> Workflow: **I**, **Up**, **Rp**, **Un**, **FC** | `MU`, or `<file path>` | Specify the location where SQL Server setup will obtain product updates. The valid values are "MU" to search Microsoft Update, a valid folder path, a relative path such as `.\MyUpdates` or a UNC share. By default, SQL Server setup will search Microsoft Update or a Windows Update Service through the Windows Server Update Services.|
| `/CONFIGURATIONFILE = ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies the [ConfigurationFile](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md) to use.|
| `/ERRORREPORTING` <br /> Optional <br /> Workflow: **I** | `0`, `1` | Prior to SQL Server 2016, manages errors sent to Microsoft. For information about SQL Server 2016 and greater, see [error reporting](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md) and the [SQL Server privacy supplement](../../sql-server/sql-server-privacy.md).| 
| `/FEATURES = ` <br /> **Required** <br /> Workflow: **I** | `SQL`, `SQLEngine`, `Replication`, `FullText`, `DQ`, `Polybase`, `AdvancedAnalytics`, `SQL_INST_MR`, `SQL_INST_MPY`, `AS`, `RS`, `RS_SHP`, `RS_SHPWFE`, `DQC`, `IS`, `MDS`, `SQL_SHARED_MPY`, `SQL_SHARED_MR`, `Tools`, `BC`, `Conn`, `DREPLAY_CTLR`, `DREPLAY_CLT`, `SNAC_SDK`, `SDK`, `LocalDB` | Selects the feature to modify. For a detailed list, see [Feature parameters](#a-namefeaturea-feature-parameters).  |
| `/ROLE = ` <br /> Optional <br /> Workflow: **I** | `SP_AS_ExistingFarm`, `SPI_AS_NewFarm`, `AllFeatures_WithDefaults` | Used to install a preconfigured selection of features. For more information, see [Role parameters](#a-nameroleparametersa-role-parameters). | 
| `/HELP`, `/?` <br /> | Blank | 
| `/INDICATEPROGRESS` <br /> Optional <br /> Workflow: **I** | Blank | Specifies that the verbose Setup log file is piped to the console.| 
| `/INSTALLSHAREDDIR = ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies a nondefault installation directory for 64-bit shared components. <br /> Default is `%Program Files%\Microsoft SQL Server`. <br /> Cannot be set to `%Program Files(x86)%\Microsoft SQL Server`. 
| `/INSTALLSHAREDWOWDIR = ` <br /> Optional <br /> Workflow: **I** | A fail path. Default is `%Program Files(x86)%\Microsoft SQL Server`  | Specifies a nondefault installation directory for 32-bit shared components. Supported only on a 64-bit system. Cannot be set to `%Program Files%\Microsoft SQL Server`. 
| `/INSTANCEDIR =` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies a nondefault installation directory for instance-specific components. | 
| `/INSTANCEID = ` <br /> Optional <br /> Workflow: **I** | A string value.  Specifies a nondefault value for an [InstanceID](#InstanceID).| 
| `/INSTANCENAME = ` <br /> **Required**| A string value. | Specifies a SQL Server instance name. | 
| `/PID = ` <br /> Optional <br /> Workflow: **I**| A string value. | Specifies the product key for the edition of SQL Server. If this parameter is not specified, Evaluation is used. |
| `Q`, `/QUIET` <br /> Optional| Blank | Specifies that Setup runs in a quiet mode without any user interface. This is used for unattended installations. The /Q parameter overrides the input of the /QS parameter.| 
| `/QS`, `/QUIETSIMPLE` <br /> Optional <br /> Workflow: **I** | Blank | Specifies that Setup runs and shows progress through the UI, but does not accept any input or show any error messages. | 
| `UIMODE = `<br /> Optional <br /> Workflow: **I** | `Normal`, `AutoAdvance` | Specifies whether to present only the minimum number of dialog boxes during setup. <br />                **/UIMode** can only be used with the **/ACTION=INSTALL** and **UPGRADE** parameters. Supported values:<br /><br /> **/UIMODE=Normal** is the default for non-Express editions and presents all setup dialog boxes for the selected features.<br /><br /> **/UIMODE=AutoAdvance** is the default for Express editions and skips nonessential dialog boxes.<br /><br /> Note that the **UIMode** setting cannot be used with the **/Q** or **/QS** parameters. | 
| `/SQMREPORTING = ` <br /> Optional <br /> Workflow: **I** | 0, 1 | Prior to SQL Server 2016, specifies feature usage reporting for SQL Server. To manage error reporting for SQL Server 2016 and greater, see [Configure diagnostic data collection](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md). | 
| `/HIDECONSOLE` <br /> Optional <br /> Workflow: **I** | Blank | Specifies that the console window is hidden or closed.| 



## Database engine control

|**Parameter** | **Values** | **Description** |
| :---------   | :--------- | :--------------|
| `/SUPPRESSPRIVACYSTATEMENTNOTICE` <br /> Optional <br /> Workflow: **I** | Blank | Only used for installation. Suppresses the privacy notice statement. By using this flag, you are agreeing with the [privacy notice](../../sql-server/sql-server-privacy.md).|
| `/IACCEPTSQLSERVERLICENSETERMS` <br /> Optional <br /> Workflow: **I** | Blank | Required only when the /Q or /QS parameter is specified for unattended installations. | Only used for installation. Required to acknowledge acceptance of the license terms. |
| `/ENABLERANU` <br /> Optional <br /> Workflow: **I** | Blank | Enables run-as credentials for SQL Server Express installations.| 
| `/INSTALLSQLDATADIR =` <br /> Optional | `<file path>` | Specifies the data directory for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\` | 
| `/SAPWD = ` <br /> **Required when /SECURITYMODE=SQL** | `<complex password>` | Specifies the password for the SQL Server **SA** account.
| `/SECURITYMODE = ` <br /> Optional <br /> Workflow: **I** | `SQL` | Specifies the security mode for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If this parameter is not supplied, then Windows-only authentication mode is supported.| 
| `/SQLBACKUPDIR = ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies the directory for backup files.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Backup`. | 
| `/SQLCOLLATION = ` <br /> Optional <br /> Workflow: **I** | `<collation>` | Specifies the collation settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> The default value is based on the locale of your Windows operating system. For more information, see [Collation Settings in Setup](https://msdn.microsoft.com/library/ms143508%28v=sql.105%29.aspx). | 
| `/ADDCURRENTUSERASSQLADMIN` <br /> Optional <br /> Workflow: **I** | `true`, `false` | Adds the current user to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **sysadmin** fixed server role. The /ADDCURRENTUSERASSQLADMIN parameter can be used when installing Express editions or when /Role=ALLFeatures_WithDefaults is used. For more information, see /ROLE below.<br /><br /> Use of /ADDCURRENTUSERASSQLADMIN is optional, but either /ADDCURRENTUSERASSQLADMIN or /SQLSYSADMINACCOUNTS is required. Default values:<br /><br /> **True** for editions of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> **False** for all other editions| 
| `/SQLSVCACCOUNT = ` <br /> **Required** <br /> Workflow: **I** | `<account name>` | Specifies the [startup](#Service-Account-Parameters) account for the SQL Server service. | 
| `/SQLSVCPASSWORD = ` <br /> **Required** | `<complex password>` | Specifies the password for SQLSVCACCOUNT.| 
| `/SQLSVCSTARTUPTYPE = `<br /> Optional <br /> Workflow: **I** | `automatic`, `disabled`, `manual` | Specifies the [startup](#Service-Account-Parameters) mode for the SQL Server service. | 
| `/SQLSYSADMINACCOUNTS = ` <br /> **Required** <br /> Workflow: **I** | `<account name>` | Use this parameter to provision logins to be members of the sysadmin role.<br /><br /> For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] editions other than [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], /SQLSYSADMINACCOUNTS is required. For editions of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], use of /SQLSYSADMINACCOUNTS is optional, but either /SQLSYSADMINACCOUNTS  or /ADDCURRENTUSERASSQLADMIN is required. | 
| `/SQLTEMPDBDIR = ` <br /> Optional <br /> Workflow: **I**| `<file path>` | Specifies the directories for tempdb data files. When specifying more than one directory, separate the directories with a blank space. If multiple directories are specified the tempdb data files will be spread across the directories in a round-robin fashion.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory)<br /><br /> **Note:** This parameter is added to RebuildDatabase scenario as well. | 
| `/SQLTEMPDBLOGDIR = ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies the directory for tempdb log file.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` (System Data Directory) | 
| `/SQLTEMPDBFILESIZE = ` <br /> Optional <br /> Workflow: **I** | Numeric value in MB. | Introduced in SQL Server 2016. Specifies the initial size of each tempdb data file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.<br/><br/>Min = (4 or 8 MB).<br/><br/>Max = 1024 MB (262,144 MB for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] | 
| `/SQLTEMPDBFILEGROWTH = ` <br /> Optional <br /> Workflow: **I** | Numeric value in MB. | Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024 .<br /><br /> Default value: 64. Allowed range: Min = 0, Max = 1024. |
| `/SQLTEMPDBLOGFILESIZE = `<br /> Optional <br /> Workflow: **I** | Numeric value from `4` to `1024` in MB. | Specifies the initial size of each tempdb log file.<br/><br/>Default = 4 MB for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], 8 MB for all other editions.]| 
| `/SQLTEMPDBLOGFILEGROWTH = `  <br /> Optional | Numeric value from `0` to `1024` in MB. | Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024.<br /><br /> Default value: 64. Allowed range: Min = 0, Max = 1024
| `/SQLTEMPDBFILECOUNT = ` <br /> Optional <br /> Workflow: **I** | Numeric value. | Specifies the number of tempdb data files to be added by setup. This value can be increased up to the number of cores. Default value:<br /><br /> 1  for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]<br /><br /> 8 or the number of cores, whichever is lower for all other editions<br /><br /> **Important** The primary database file for tempdb will still be tempdb.mdf. The additional tempdb files are named as tempdb_mssql_#.ndf where # represents a unique number for each additional tempdb database file created during setup. The purpose of this naming convention is to make them unique. Uninstalling an instance of SQL Server deletes the files with naming convention tempdb_mssql_#.ndf. Do not use tempdb_mssql_\*.ndf naming convention for user database files. |
| `/SQLUSERDBDIR = ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies the directory for the data files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data`|
| `/SQLSVCINSTANTFILEINIT = ` <br /> Optional <br /> Workflow: **I** | `true`, `false` | Enables instant file initialization for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account. For security and performance considerations, see [Database Instant File Initialization](../../relational-databases/databases/database-instant-file-initialization.md).<br /><br /> Default value: "False" | 
| `/SQLUSERDBLOGDIR = ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies the directory for the log files for user databases.<br /><br /> Default value: `<InstallSQLDataDir>\<SQLInstanceID>\MSSQL\Data` | 
| `/SQLMAXDOP = ` <br /> Optional <br /> Workflow: **I** | Numeric value. | Specifies the max degree of parallelism, which determines how many processors a single statement can utilize during the execution of a single statement. Only available starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]. <br /><br /> If ommitted on unattended (silent) installations, value will be default, which align withs the [max degree of parallelism guidelines](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#Guidelines). | 
| `/USESQLRECOMMENDEDMEMORYLIMITS` <br /> Optional <br /> Workflow: **I** | Blank | Specifies that the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will use calculated recommended values that align with the [server memory configuration guidelines](../../database-engine/configure-windows/server-memory-server-configuration-options.md#setting-the-memory-options-manually) for a standalone [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. <br /><br /> If /USESQLRECOMMENDEDMEMORYLIMITS, /SQLMINMEMORY, and /SQLMAXMEMORY are omitted on unattended (silent) installs, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will use the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory configuration. | 
| `/SQLMINMEMORY = ` <br /> Optional <br /> Workflow: **I** | Numeric value in MB | Specifies the Min Server Memory configuration. Only available starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)].<br /><br /> Default value: 0.<br /><br /> **Note:** This parameter cannot be used with /USESQLRECOMMENDEDMEMORYLIMITS. <br /><br /> If /USESQLRECOMMENDEDMEMORYLIMITS, /SQLMINMEMORY, and /SQLMAXMEMORY are omitted on unattended (silent) installs, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will use the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory configuration.|
| `/SQLMAXMEMORY = ` <br /> Optional <br /> Workflow: **I** | Numeric value in MB. |  Specifies the Max Server Memory configuration. Only available starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)].<br /><br /> Default value: calculated recommended value that aligns with the [server memory configuration guidelines](../../database-engine/configure-windows/server-memory-server-configuration-options.md#setting-the-memory-options-manually) for a standalone [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.<br /><br /> **Note:** This parameter cannot be used with /USESQLRECOMMENDEDMEMORYLIMITS. <br /><br /> If /USESQLRECOMMENDEDMEMORYLIMITS, /SQLMINMEMORY, and /SQLMAXMEMORY are omitted on unattended (silent) installs, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will use the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory configuration. | 






## Analysis services
| :---------   | :--------- | :--------------|
|**Parameter** | **Values** | **Description** |
| `/ASBACKUPDIR = ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] backup files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Backup`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Backup` |
| `/ASCOLLATION = ` <br /> Optional <br /> Workflow: **I** | A string value. | Specifies the collation setting for Analysis Services. Default value is `Latin1_General_CI_AS`. | 
| `/ASCONFIGDIR = ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configuration files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Config`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Config`
| `/ASDATADIR= ` <br /> Optional <br /> Workflow: **I** | `<file path>` | Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Data`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Data` | 
| `/ASLOGDIR = ` <br /> Optional  <br /> Workflow: **I**| `<file path>` | Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] log files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Log`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Log`|
| `/ASSERVERMODE = ` <br /> Optional <br /> Workflow: **I** | `MULTIDIMENSIONAL`, `POWERPIVOT`, `TABULAR` | Case-sensitive, and must be expressed in upper case. Specifies the server mode of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. For more information about valid values, see [Install Analysis Services](https://docs.microsoft.com/analysis-services/instances/install-windows/install-analysis-services). | 
| `/ASSVCACCOUNT = ` <br /> **Required** <br /> Workflow: **I** | `<account name>` | Specifies the [account](#Service-Account-Parameters) for the Analysis Services service. For more information, see [service accounts](#a-nameaccountsa-service-account-parameters). | 
| `/ASSVCPASSWORD = ` <br /> **Required** <br /> Workflow: **I** | `<complex password>`| Specifies the password for the Analysis Services service. | 
| `/ASSVCSTARTUPTYPE = ` <br /> Optional <br /> Workflow: **I** | `automatic`, `disabled`, `manual` | Specifies the [startup](#Service-Account-Parameters) mode for the Analysis Sergvices service. 
| `/ASSYSADMINACCOUNTS =` <br /> **Required** <br /> Workflow: **I** | `<account name>` | Specifies the administrator credentials for Analysis Services.|
| `/ASTEMPDIR = ` <br /> Optional  <br /> Workflow: **I** | `<file path>` | Specifies the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] temporary files. Default values:<br /><br /> For WOW mode on 64-bit: `%Program Files(x86)%\Microsoft SQL Server \<INSTANCEDIR>\<ASInstanceID>\OLAP\Temp`<br /><br /> For all other installations: `%Program Files%\Microsoft SQL Server\<INSTANCEDIR>\<ASInstanceID>\OLAP\Temp| 
| `/ASPROVIDERMSOLAP = ` <br /> Optional <br /> Workflow: **I** | `0`, `1` | Specifies whether the MSOLAP provider can run in-process. Default value: 1 = enabled. | 
| `/FARMACCOUNT = ` <br /> **Required for SPI_AS_NewFarm** <br /> Workflow: **I** | `<account name>` | Specifies a domain user account for running SharePoint Central Administration services and other essential services in a farm.<br /><br /> This parameter is used only for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that are installed through /ROLE = SPI_AS_NEWFARM.| 
| `/FARMPASSWORD = ` <br /> **Required for SPI_AS_NewFarm** <br /> Workflow: **I**  | `<complex password>` | Specifies a password for the farm account.| 
| `/PASSPHRASE = `<br /> **Required for SPI_AS_NewFarm** <br /> Workflow: **I** | Specifies a passphrase that is used to add additional application servers or Web front end servers to a SharePoint farm.<br /><br /> This parameter is used only for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that are installed through /ROLE = SPI_AS_NEWFARM. | 
| `/FARMADMINIPORT = ` <br /> **Required for SPI_AS_NewFarm** <br /> Workflow: **I**| Specifies a port used to connect to the SharePoint Central Administration web application.<br /><br /> This parameter is used only for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that are installed through /ROLE = SPI_AS_NEWFARM. |

## Integration Services
| :---------   | :--------- | :--------------|
|**Parameter** | **Values** | **Description** |
| `/ISSVCACCOUNT = ` <br /> **Required** | `<account name>` | Specifies the account for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].<br /><br /> Default value: `NT AUTHORITY\NETWORK SERVICE`. | 
| `/ISSVCPASSWORD = `<br /> **Required**  | `<complex password>` | Specifies the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] password. | 
| `/ISSVCStartupType = `| `automatic`, `disabled`, `manual` | Specifies the [startup](#Service-Account-Parameters) mode for the Integration Services service. | 

## Polybase

|**Parameter** | **Values** | **Description**|
| :---------   | :--------- | :--------------|
| `/PBENGSVCACCOUNT=` <br /> Optional | `<account name>` | Specifies the account for the Polybase engine service. Default is `NT Authority\NETWORK SERVICE`. | 
| `/PBDMSSVCPASSWORD = ` <br /> Optional| `<complex password>` | Specifies the password for the Polybase engine service account.| 
| `/PBENGSVCSTARTUPTYPE = ` <br /> Optional | Automatic (default), disabled, manual. | Specifies the [startup](#Service-Account-Parameters) mode for the PolyBase engine service. | 
| `/PBPORTRANGE = ` <br /> Optional | Numeric value - numeric value | Specifies a port range with at least 6 ports for PolyBase services. Example: `/PBPORTRANGE=16450-16460`| 
| `/PBSCALEOUT` <br /> Optional | `True`, `False` | Specifies if the SQL Server Database Engine instance will be used as a part of PolyBase Scale-out computational group. Use this option if you are configuring a PolyBase Scale-out computational group including the head node. | 

## Miscellaneous parameters

### SQL Server Agent

|**Parameter** | **Values** | **Description** |
| `/AGTSVCACCOUNT = ` <br /> **Required** | `<account name>` | Specifies the [account](#Service-Account-Parameters) for the SQL Server Agent service. |
| `/AGTSVCPASSWORD = ` <br /> **Required** | `<complex password>` | Specifies the password for the SQL Server agent service account. 
| `/AGTSVCSTARTUPTYPE = ` <br /> Optional | `automatic`, `disabled`, `manual` | Specifies the [startup](#Service-Account-Parameters) mode for the SQL Server Agent service. | 

### SQL Server Browser

|**Parameter** | **Values** | **Description**|
| :---------   | :--------- | :--------------|
| `/BROWSERSVCSTARTUPTYPE = ` <br /> Optional | `automatic`, `disabled`, `manual` |  Specifies the [startup](#Service-Account-Parameters) mode for SQL Server Browser service.| 

### Network configuration 

|**Parameter** | **Values** | **Description**|
| :---------   | :--------- | :--------------|
| `/NPENABLED = ` <br /> Optional | `0`, `1` | Specifies the state of the Named Pipes protocol for the SQL Server service. 0 disables the Named Pipes protocol, while 1 enables it. | 
| `/TCPENABLED = ` <br /> Optional | `0`, `1` | Specifies the state of the TCP protocol for the SQL Server service. 0 disables the TCP protocol, while 1 enables it. | 

## Reporting services
|**Parameter** | **Values** | **Description**|
| :---------   | :--------- | :--------------|
| `/RSINSTALLMODE = ` <br /> Optional | `SharePointFilesOnlyMode`, `DefaultNativeMode`, `FilesOnlyMode` | Specifies the Install mode for Reporting Services. <br /><br /> If the installation includes the SQL Server[!INCLUDE[ssDE](../../includes/ssde-md.md)], the default RSINSTALLMODE is DefaultNativeMode.<br /><br /> If the installation does not include the SQL Server[!INCLUDE[ssDE](../../includes/ssde-md.md)], the default RSINSTALLMODE is FilesOnlyMode.<br /><br /> If you choose DefaultNativeMode but the installation does not include the SQL Server[!INCLUDE[ssDE](../../includes/ssde-md.md)], the installation will automatically change the RSINSTALLMODE to FilesOnlyMode.| 
| `/RSSVCACCOUNT = ` <br /> **Required** | `<account name>` | Specifies the startup account for Reporting Services.| 
| `/RSSVCPASSWORD - ` <br /> **Required** | `<complex password>` | Specifies the password for the startup account for the Reporting Services service. | 
| `/RSSVCStartupType = ` <br /> Optional | `automatic`, `disabled`, `manual` | Specifies the [startup](#Service-Account-Parameters) mode for Reporting Services. | 


## Machine Learning Services

|**Parameter** | **Values** | **Description**|
| :---------   | :--------- | :--------------|
| `/IACCEPTPYTHONLICENSETERMS` <br /> **Required with /Q or /QS parameter** <br /> Workflow: **I** | Blank | Only used during a quiet installation that includes the Anaconda Python package. Required to acknowledge acceptance of Python license terms.|
| `/IACCEPTROPENLICENSETERMS` <br /> **Required with /Q or /QS parameter** <br /> Workflow: **I** | Blank | Only used during a quiet installation that includes R 
| `/MPYCACHEDIRECTORY` | Reserved for future use. | Use %TEMP% to store Python .CAB files for installation on a computer that does not have an internet connection.| 
| `/MRCACHEDIRECTORY = ` | `<file path>` | Use this parameter to specify the Cache directory for Microsoft R Open, SQL Server 2016 R Services, SQL Server 2016 R Server (Standalone), or R feature support in SQL Server 2017 Machine Learning Services or Machine Learning Server (Standalone). This setting is typically used when installing R components from the [command line on a computer without Internet access](https://docs.microsoft.com/sql/advanced-analytics/install/sql-ml-component-install-without-internet-access). | 
| `SQL_INSTA_JAVA` <br /> Optional | Blank | Starting with SQL Server 2019, specifies installing Java with Language Extensions. If /SQL_INST_JAVA is provided without the /SQLJAVADIR parameter, it's assumed you want to install the Zulu Open JRE that is provided by the installation media.| 
| `SQLJAVADIR = ` <br /> Optional | Starting with SQL Server 2019, specifies the path for the already-installed JRE or JDK. | 

## Filestream 

|**Parameter** | **Values** | **Description**|
| :---------   | :--------- | :--------------|
| `/FILESTREAMLEVEL = ` <br /> Optional | `0`, `1`, `2`, `3` |Specifies the access level for the FILESTREAM feature. Supported values:<br /><br /> 0 =Disable FILESTREAM support for this instance. (Default value)<br /><br /> 1=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access.<br /><br /> 2=Enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] and file I/O streaming access. (Not valid for cluster scenarios)<br /><br /> 3=Allow remote clients to have streaming access to FILESTREAM data.|  
| `/FILESTREAMSHARENAME = ` <br /> Optional <br /> **Required when FILESTREAMLEVEL is greater than 1.** | String value. | Specifies the name of the windows share in which the FILESTREAM data will be stored.|  
|SQL Server Full Text| 
| `/FTSVCACCOUNT = ` <br /> Optional | `<account name>` | Specifies the account for Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher. ServiceSID is used to help secure the communication between SQL Server and Full-text Filter Daemon. If the values are not provided, the Full-text Filter Launcher Service is disabled. You have to use SQL Server Control Manager to change the service account and enable full-text functionality.<br /><br /> Default value: Local Service Account. | 
| `/FTSVCPASSWORD = ` <br /> Optional | `<complex password>` | Specifies the password for the Full-Text filter launcher service.<br /><br /> This parameter is ignored in [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or higher.|


##  Service Account Parameters  
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

