---
title: "MSReportServer_ConfigurationSetting Members | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "MSReportServer_ConfigurationSetting Members"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "WMI provider [Reporting Services], MSReportServer_ConfigurationSetting class"
  - "MSReportServer_ConfigurationSetting class"
ms.assetid: 5e21a53a-a2f8-4b35-a8d9-5483519e3857
author: markingmyname
ms.author: maghan
manager: kfile
---
# MSReportServer_ConfigurationSetting Members
  The MSReportServer_ConfigurationSetting class contains the following properties and methods.  
  
## Public Properties  
  
|||  
|-|-|  
|[ConnectionPoolSize](configurationsetting-property-connectionpoolsize.md)|Returns the connection pool size used by the report server to communicate with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonAccount](configurationsetting-property-databaselogonaccount.md)|Specifies the login account used by the report server to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonTimeout](configurationsetting-property-databaselogontimeout.md)|Specifies the number of seconds to wait before an attempt to log on to the report server database fails. Read-only.|  
|[DatabaseLogonType](configurationsetting-property-databaselogontype.md)|Specifies whether the report server uses a Windows service account, a Windows user account, or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to access the report server database. Read-only.|  
|[DatabaseName](configurationsetting-property-databasename.md)|Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database.|  
|[DatabaseQueryTimeout](configurationsetting-property-databasequerytimeout.md)|Specifies the number of seconds that must elapse before the command fails or times out. The report server is timing the process against the report server database, not a data source for the report.|  
|[DatabaseServerName](configurationsetting-property-databaseservername.md)|Specifies the name of the server on which the report server database is installed.|  
|[InstallationID Property](configurationsetting-property-installationid.md)|Returns a unique identifier for a specific report server instance.|  
|[InstanceName](configurationsetting-property-instancename.md)|Specifies the name of a report server instance on a specific computer.|  
|[IsInitialized](configurationsetting-property-isinitialized.md)|Indicates whether the report server instance is initialized. Read-only.|  
|[IsSharePointIntegrated](configurationsetting-property-issharepointintegrated.md)|Indicates whether the report server is configured for SharePoint integrated mode.|  
|[IsWebServiceEnabled](configurationsetting-property-iswebserviceenabled.md)|Indicates whether the Report Server Web service is enabled. Read-only.|  
|[IsWindowsServiceEnabled](configurationsetting-property-iswindowsserviceenabled.md)|Indicates whether the Report Server Windows service is enabled. Read-only.|  
|[MachineAccountIdentity Property &#40;WMI&#41;](configurationsetting-property-machineaccountidentity.md)|Gets the machine account identity of the computer that the report server is installed on.|  
|[PathName](configurationsetting-property-pathname.md)|Specifies the installation path to a report server instance.|  
|[SecureConnectionLevel](configurationsetting-property-secureconnectionlevel.md)|Returns the secure connection level specified in the RSReportServer.config file.|  
|[SenderEmailAddress](configurationsetting-property-senderemailaddress.md)|Gets the address used to send e-mail from the report server. Read-only.|  
|[SendUsingSMTPServer](configurationsetting-property-sendusingsmtpserver.md)|Specifies whether the SendUsing property in the e-mail configuration is set to TRUE.|  
|[SMTPServer](configurationsetting-property-smtpserver.md)|Gets the SMTP server property from the RSReportServer.config file. Read-only.|  
|[UnattendedExecutionAccount](configurationsetting-property-unattendedexecutionaccount.md)|Specifies the login user account that the report server impersonates when running reports unattended. Read-only.|  
|[Version](configurationsetting-property-version.md)|Returns the version of the report server.|  
|[VirtualDirectoryReportManager Property &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-property-virtualdirectoryreportmanager.md)|Returns the virtual directory for the report manager application|  
|[VirtualDirectoryReportServer Property &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-property-virtualdirectoryreportserver.md)|Returns the Virtual directory for the report server web service application.|  
|[WindowsServiceIdentityActual](configurationsetting-property-windowsserviceidentityactual.md)|Returns the identity that the Report Server Windows service is actually running under. Read-only.|  
|[WindowsServiceIdentityConfigured](windowsserviceidentityconfigured-property.md)|Returns the identity that the Report Server Windows service was last configured to run under. Read-only.|  
  
## Public Methods  
  
|||  
|-|-|  
|[BackupEncryptionKey](configurationsetting-method-backupencryptionkey.md)|Backs up the encryption key for the instance. The encryption key is stored encrypted with a password.|  
|[CreateSSLCertificateBinding Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-createsslcertificatebinding.md)|Creates an SSL Certificate binding.|  
|[DeleteEncryptedInformation](configurationsetting-method-deleteencryptedinformation.md)|Deletes the encrypted information from the report server database.|  
|[DeleteEncryptionKey](configurationsetting-method-deleteencryptionkey.md)|Deletes the encryption keys from the report server database.|  
|[GenerateDatabaseCreationScript](configurationsetting-method-generatedatabasecreationscript.md)|Generates an SQL script that can be used to create the report server database.|  
|[GenerateDatabaseRightsScript](configurationsetting-method-generatedatabaserightsscript.md)|Generates an SQL script that can be used to grant a user permissions to the report server database.|  
|[GenerateDatabaseUpgradeScript](configurationsetting-method-generatedatabaseupgradescript.md)|Generates an SQL script that can be used to upgrade a report server database.|  
|[GetAdminSiteUrl Method &#40;WMI&#41;](configurationsetting-method-getadminsiteurl.md)|Gets the absolute URL to the Central Administration Web site.|  
|[GetDatabaseVersionDisplayName](configurationsetting-method-getdatabaseversiondisplayname.md)|Gets the display name for a given report server database version string.|  
|[InitializeReportServer](configurationsetting-method-initializereportserver.md)|Initializes the specified report server instance.|  
|[ListInstalledSharePointVersions Method &#40;WMI&#41;](configurationsetting-method-listinstalledsharepointversions.md)|Returns a set of tokens that represent the versions of [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] [!INCLUDE[offSPServ](../../includes/offspserv-md.md)], [!INCLUDE[SPF2010](../../includes/spf2010-md.md)], or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] that are installed on the same computer as the report server.|  
|[ListIPAddresses Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-listipaddresses.md)|Lists IP addresses for the computer.|  
|[ListReportServersInDatabase](configurationsetting-method-listreportserversindatabase.md)|Returns a list of report server installations that are present in the report server database, regardless of whether those installations have access to secure information.|  
|[ListReservedURLs Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-listreservedurls.md)|Lists URLs reserved for all applications on the report server.|  
|[ListSSLCertificateBindings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-listsslcertificatebindings.md)|Lists SSL certificate bindings that exist in HTTP.SYS and those expected from rsreportserver.config.|  
|[ListSSLCertificates Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-listsslcertificates.md)|Lists installed SSL certificates on the computer.|  
|[ReencryptSecureInformation](configurationsetting-method-reencryptsecureinformation.md)|Generates a new encryption key and re-encrypts all secure information in the report server database using this new key.|  
|[RemoveSSLCertificateBindings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-removesslcertificatebinding.md)|Remove an SSL Certificate binding.|  
|[RemoveUnattendedExecutionAccount](configurationsetting-method-removeunattendedexecutionaccount.md)|Deletes the unattended execution account entry from the report server configuration.|  
|[RemoveURL Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-removeurl.md)|Removes a URL reserved for the report server.|  
|[ReserveURL Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-reserveurl.md)|Adds a URL reservation for a given application.|  
|[RestoreEncryptionKey](configurationsetting-method-restoreencryptionkey.md)|Reapplies the specified encryption key to the report server database.|  
|[SetDatabaseConnection](configurationsetting-method-setdatabaseconnection.md)|Sets the report server database connection to a particular report server database.|  
|[SetDatabaseLogonTimeout](configurationsetting-method-setdatabaselogontimeout.md)|Specifies the default time-out value for report server database logon attempts.|  
|[SetDatabaseQueryTimeout](configurationsetting-method-setdatabasequerytimeout.md)|Specifies the default time-out value for report server database connections.|  
|[SetEmailConfiguration](configurationsetting-method-setemailconfiguration.md)|Configures the e-mail delivery extension used by the report server to send e-mail.|  
|[SetSecureConnectionLevel](configurationsetting-method-setsecureconnectionlevel.md)|Sets the secure connection level of the report server.|  
|[SetServiceState](configurationsetting-method-setservicestate.md)|Turns the Report Server Windows and Web services on and off.|  
|[SetUnattendedExecutionAccount](configurationsetting-method-setunattendedexecutionaccount.md)|Specifies the account used to run reports unattended.|  
|[SetVirtualDirectory Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-setvirtualdirectory.md)|Sets the virtual directory for an application.|  
|[SetWindowsServiceIdentity](configurationsetting-method-setwindowsserviceidentity.md)|Makes the Report Server Windows service run as the specified Windows user, and grants this account sufficient file system permissions to allow the report server to operate.|  
  
## See Also  
 [MSReportServer_ConfigurationSetting Class](msreportserver-configurationsetting-class.md)  
  
  
