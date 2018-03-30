---
title: "MSReportServer_ConfigurationSetting Members | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
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
caps.latest.revision: 46
author: "douglaslM"
ms.author: "douglasl"
manager: "mblythe"
---
# MSReportServer_ConfigurationSetting Members
  The MSReportServer_ConfigurationSetting class contains the following properties and methods.  
  
## Public Properties  
  
|||  
|-|-|  
|[ConnectionPoolSize](../../2014/reporting-services/connectionpoolsize-property-wmi-msreportserver-configurationsetting.md)|Returns the connection pool size used by the report server to communicate with the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonAccount](../../2014/reporting-services/databaselogonaccount-property-wmi-msreportserver-configurationsetting.md)|Specifies the login account used by the report server to connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonTimeout](../../2014/reporting-services/databaselogontimeout-property-wmi-msreportserver-configurationsetting.md)|Specifies the number of seconds to wait before an attempt to log on to the report server database fails. Read-only.|  
|[DatabaseLogonType](../../2014/reporting-services/databaselogontype-property-wmi-msreportserver-configurationsetting.md)|Specifies whether the report server uses a Windows service account, a Windows user account, or a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] login to access the report server database. Read-only.|  
|[DatabaseName](../../2014/reporting-services/databasename-property-wmi-msreportserver-configurationsetting.md)|Specifies the name of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance that hosts the report server database.|  
|[DatabaseQueryTimeout](../../2014/reporting-services/databasequerytimeout-property-wmi-msreportserver-configurationsetting.md)|Specifies the number of seconds that must elapse before the command fails or times out. The report server is timing the process against the report server database, not a data source for the report.|  
|[DatabaseServerName](../../2014/reporting-services/databaseservername-property-wmi-msreportserver-configurationsetting.md)|Specifies the name of the server on which the report server database is installed.|  
|[InstallationID Property](../../2014/reporting-services/installationid-property-wmi-msreportserver-configurationsetting.md)|Returns a unique identifier for a specific report server instance.|  
|[InstanceName](../../2014/reporting-services/instancename-property-wmi-msreportserver-configurationsetting.md)|Specifies the name of a report server instance on a specific computer.|  
|[IsInitialized](../../2014/reporting-services/isinitialized-property-wmi-msreportserver-configurationsetting.md)|Indicates whether the report server instance is initialized. Read-only.|  
|[IsSharePointIntegrated](../../2014/reporting-services/issharepointintegrated-property-wmi.md)|Indicates whether the report server is configured for SharePoint integrated mode.|  
|[IsWebServiceEnabled](../../2014/reporting-services/iswebserviceenabled-property-wmi-msreportserver-configurationsetting.md)|Indicates whether the Report Server Web service is enabled. Read-only.|  
|[IsWindowsServiceEnabled](../../2014/reporting-services/iswindowsserviceenabled-property-wmi-msreportserver-configurationsetting.md)|Indicates whether the Report Server Windows service is enabled. Read-only.|  
|[MachineAccountIdentity Property &#40;WMI&#41;](../../2014/reporting-services/machineaccountidentity-property-wmi.md)|Gets the machine account identity of the computer that the report server is installed on.|  
|[PathName](../../2014/reporting-services/pathname-property-wmi-msreportserver-configurationsetting.md)|Specifies the installation path to a report server instance.|  
|[SecureConnectionLevel](../../2014/reporting-services/secureconnectionlevel-property-wmi-msreportserver-configurationsetting.md)|Returns the secure connection level specified in the RSReportServer.config file.|  
|[SenderEmailAddress](../../2014/reporting-services/senderemailaddress-property-wmi-msreportserver-configurationsetting.md)|Gets the address used to send e-mail from the report server. Read-only.|  
|[SendUsingSMTPServer](../../2014/reporting-services/sendusingsmtpserver-property-wmi-msreportserver-configurationsetting.md)|Specifies whether the SendUsing property in the e-mail configuration is set to TRUE.|  
|[SMTPServer](../../2014/reporting-services/smtpserver-property-wmi-msreportserver-configurationsetting.md)|Gets the SMTP server property from the RSReportServer.config file. Read-only.|  
|[UnattendedExecutionAccount](../../2014/reporting-services/unattendedexecutionaccount-property-wmi-msreportserver-configurationsetting.md)|Specifies the login user account that the report server impersonates when running reports unattended. Read-only.|  
|[Version](../../2014/reporting-services/version-property-wmi-msreportserver-configurationsetting.md)|Returns the version of the report server.|  
|[VirtualDirectoryReportManager Property &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/virtualdirectoryreportmanager-property-wmi-msreportserver-configurationsetting.md)|Returns the virtual directory for the report manager application|  
|[VirtualDirectoryReportServer Property &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/virtualdirectoryreportserver-property-wmi-msreportserver-configurationsetting.md)|Returns the Virtual directory for the report server web service application.|  
|[WindowsServiceIdentityActual](../../2014/reporting-services/windowsserviceidentityactual-property-wmi-msreportserver-configurationsetting.md)|Returns the identity that the Report Server Windows service is actually running under. Read-only.|  
|[WindowsServiceIdentityConfigured](../../2014/reporting-services/windowsserviceidentityconfigured-property.md)|Returns the identity that the Report Server Windows service was last configured to run under. Read-only.|  
  
## Public Methods  
  
|||  
|-|-|  
|[BackupEncryptionKey](../../2014/reporting-services/backupencryptionkey-method-wmi-msreportserver-configurationsetting.md)|Backs up the encryption key for the instance. The encryption key is stored encrypted with a password.|  
|[CreateSSLCertificateBinding Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/createsslcertificatebinding-method-wmi-msreportserver-configurationsetting.md)|Creates an SSL Certificate binding.|  
|[DeleteEncryptedInformation](../../2014/reporting-services/deleteencryptedinformation-method-wmi-msreportserver-configurationsetting.md)|Deletes the encrypted information from the report server database.|  
|[DeleteEncryptionKey](../../2014/reporting-services/deleteencryptionkey-method-wmi-msreportserver-configurationsetting.md)|Deletes the encryption keys from the report server database.|  
|[GenerateDatabaseCreationScript](../../2014/reporting-services/generatedatabasecreationscript-method-wmi-msreportserver-configurationsetting.md)|Generates an SQL script that can be used to create the report server database.|  
|[GenerateDatabaseRightsScript](../../2014/reporting-services/generatedatabaserightsscript-method-wmi-msreportserver-configurationsetting.md)|Generates an SQL script that can be used to grant a user permissions to the report server database.|  
|[GenerateDatabaseUpgradeScript](../../2014/reporting-services/generatedatabaseupgradescript-method-wmi-msreportserver-configurationsetting.md)|Generates an SQL script that can be used to upgrade a report server database.|  
|[GetAdminSiteUrl Method &#40;WMI&#41;](../../2014/reporting-services/getadminsiteurl-method-wmi.md)|Gets the absolute URL to the Central Administration Web site.|  
|[GetDatabaseVersionDisplayName](../../2014/reporting-services/getdatabaseversiondisplayname-method-wmi.md)|Gets the display name for a given report server database version string.|  
|[InitializeReportServer](../../2014/reporting-services/initializereportserver-method-wmi-msreportserver-configurationsetting.md)|Initializes the specified report server instance.|  
|[ListInstalledSharePointVersions Method &#40;WMI&#41;](../../2014/reporting-services/listinstalledsharepointversions-method-wmi.md)|Returns a set of tokens that represent the versions of [!INCLUDE[winSPServ](../includes/winspserv-md.md)] [!INCLUDE[offSPServ](../includes/offspserv-md.md)], [!INCLUDE[SPF2010](../includes/spf2010-md.md)], or [!INCLUDE[SPS2010](../includes/sps2010-md.md)] that are installed on the same computer as the report server.|  
|[ListIPAddresses Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/listipaddresses-method-wmi-msreportserver-configurationsetting.md)|Lists IP addresses for the computer.|  
|[ListReportServersInDatabase](../../2014/reporting-services/listreportserversindatabase-method-wmi-msreportserver-configurationsetting.md)|Returns a list of report server installations that are present in the report server database, regardless of whether those installations have access to secure information.|  
|[ListReservedURLs Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/listreservedurls-method-wmi-msreportserver-configurationsetting.md)|Lists URLs reserved for all applications on the report server.|  
|[ListSSLCertificateBindings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/listsslcertificatebindings-method-wmi-msreportserver-configurationsetting.md)|Lists SSL certificate bindings that exist in HTTP.SYS and those expected from rsreportserver.config.|  
|[ListSSLCertificates Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/listsslcertificates-method-wmi-msreportserver-configurationsetting.md)|Lists installed SSL certificates on the computer.|  
|[ReencryptSecureInformation](../../2014/reporting-services/reencryptsecureinformation-method-wmi-msreportserver-configurationsetting.md)|Generates a new encryption key and re-encrypts all secure information in the report server database using this new key.|  
|[RemoveSSLCertificateBindings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/removesslcertificatebindings-method-wmi-msreportserver-configurationsetting.md)|Remove an SSL Certificate binding.|  
|[RemoveUnattendedExecutionAccount](../../2014/reporting-services/removeunattendedexecutionaccount-method-wmi-msreportserver-configurationsetting.md)|Deletes the unattended execution account entry from the report server configuration.|  
|[RemoveURL Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/removeurl-method-wmi-msreportserver-configurationsetting.md)|Removes a URL reserved for the report server.|  
|[ReserveURL Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/reserveurl-method-wmi-msreportserver-configurationsetting.md)|Adds a URL reservation for a given application.|  
|[RestoreEncryptionKey](../../2014/reporting-services/restoreencryptionkey-method-wmi-msreportserver-configurationsetting.md)|Reapplies the specified encryption key to the report server database.|  
|[SetDatabaseConnection](../../2014/reporting-services/setdatabaseconnection-method-wmi-msreportserver-configurationsetting.md)|Sets the report server database connection to a particular report server database.|  
|[SetDatabaseLogonTimeout](../../2014/reporting-services/setdatabaselogontimeout-method-wmi-msreportserver-configurationsetting.md)|Specifies the default time-out value for report server database logon attempts.|  
|[SetDatabaseQueryTimeout](../../2014/reporting-services/setdatabasequerytimeout-method-wmi-msreportserver-configurationsetting.md)|Specifies the default time-out value for report server database connections.|  
|[SetEmailConfiguration](../../2014/reporting-services/setemailconfiguration-method-wmi-msreportserver-configurationsetting.md)|Configures the e-mail delivery extension used by the report server to send e-mail.|  
|[SetSecureConnectionLevel](../../2014/reporting-services/setsecureconnectionlevel-method-wmi-msreportserver-configurationsetting.md)|Sets the secure connection level of the report server.|  
|[SetServiceState](../../2014/reporting-services/setservicestate-method-wmi-msreportserver-configurationsetting.md)|Turns the Report Server Windows and Web services on and off.|  
|[SetUnattendedExecutionAccount](../../2014/reporting-services/setunattendedexecutionaccount-method-wmi-msreportserver-configurationsetting.md)|Specifies the account used to run reports unattended.|  
|[SetVirtualDirectory Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/setvirtualdirectory-method-wmi-msreportserver-configurationsetting.md)|Sets the virtual directory for an application.|  
|[SetWindowsServiceIdentity](../../2014/reporting-services/setwindowsserviceidentity-method-wmi-msreportserver-configurationsetting.md)|Makes the Report Server Windows service run as the specified Windows user, and grants this account sufficient file system permissions to allow the report server to operate.|  
  
## See Also  
 [MSReportServer_ConfigurationSetting Class](../../2014/reporting-services/msreportserver-configurationsetting-class.md)  
  
  