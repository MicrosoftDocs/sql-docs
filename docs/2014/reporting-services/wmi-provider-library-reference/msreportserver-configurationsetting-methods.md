---
title: "MSReportServer_ConfigurationSetting Methods | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "MSReportServer_ConfigurationSetting Methods"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "WMI provider [Reporting Services], MSReportServer_ConfigurationSetting class"
  - "MSReportServer_ConfigurationSetting class"
ms.assetid: a08c2476-5b8e-4792-94da-1360fe231c6e
author: markingmyname
ms.author: maghan
manager: craigg
---
# MSReportServer_ConfigurationSetting Methods
  The MSReportServer_ConfigurationSetting class of the Report Server WMI Provider provides the following public methods.  
  
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
|[ListInstalledSharePointVersions Method &#40;WMI&#41;](configurationsetting-method-listinstalledsharepointversions.md)|Returns a set of tokens that represent the versions of Microsoft [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] [!INCLUDE[offSPServ](../../includes/offspserv-md.md)], [!INCLUDE[SPF2010](../../includes/spf2010-md.md)], or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] that are installed on the same computer as the report server.|  
|[ListIPAddresses Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-listipaddresses.md)|Lists IP addresses for the computer.|  
|[ListReportServersInDatabase](configurationsetting-method-listreportserversindatabase.md)|Returns a list of report server installations that are present in the report server database, regardless of whether those installations have access to secure information.|  
|[ListReservedURLs Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-listreservedurls.md)|Lists URLs reserved for all applications on the report server.|  
|[ListSSLCertificateBindings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-listsslcertificatebindings.md)|Lists SSL certificate bindings that exist in HTTP.SYS and those expected from RSReportServer.config.|  
|[ListSSLCertificates Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-listsslcertificates.md)|Lists installed SSL certificates on the computer.|  
|[ReencryptSecureInformation](configurationsetting-method-reencryptsecureinformation.md)|Generates a new encryption key and re-encrypts all secure information in the report server database using this new key.|  
|[RemoveSSLCertificateBindings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-removesslcertificatebinding.md)|Remove an SSL Certificate binding.|  
|[RemoveUnattendedExecutionAccount](configurationsetting-method-removeunattendedexecutionaccount.md)|Deletes the unattended execution account entry from the report server configuration.|  
|[RemoveURL Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-removeurl.md)|Removes a URL reserved for the report server.|  
|[ReserveURL Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-reserveurl.md)|Adds a URL reservation for a given application.|  
|[RestoreEncryptionKey](configurationsetting-method-restoreencryptionkey.md)|Reapplies the specified encryption key to the report server database.|  
|[SetDatabaseConnection](configurationsetting-method-setdatabaseconnection.md)|Sets the report server database connection to a particular report server database.|  
|[SetDatabaseLogonTimeout](configurationsetting-method-setdatabaselogontimeout.md)|Specifies the default time-out value for report server database logon attempts.|  
|[SetDatabaseQueryTimeout](configurationsetting-method-setdatabasequerytimeout.md)|Specifies the default time-out value for report server database queries.|  
|[SetEmailConfiguration](configurationsetting-method-setemailconfiguration.md)|Configures the e-mail delivery extension used by the report server to send e-mail.|  
|[SetSecureConnectionLevel](configurationsetting-method-setsecureconnectionlevel.md)|Sets the secure connection level of the report server.|  
|[SetServiceState](configurationsetting-method-setservicestate.md)|Turns the Report Server service on and off.|  
|[SetUnattendedExecutionAccount](configurationsetting-method-setunattendedexecutionaccount.md)|Specifies the account used to run reports unattended.|  
|[SetVirtualDirectory Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-setvirtualdirectory.md)|Sets the virtual directory for an application.|  
|[SetWindowsServiceIdentity](configurationsetting-method-setwindowsserviceidentity.md)|Makes the Report Server service run as the specified Windows user, and grants this account sufficient file system permissions to allow the report server to operate.|  
  
## See Also  
 [MSReportServer_ConfigurationSetting Class](msreportserver-configurationsetting-class.md)  
  
  
