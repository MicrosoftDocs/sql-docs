---
title: "MSReportServer_ConfigurationSetting Methods | Microsoft Docs"
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
  - "MSReportServer_ConfigurationSetting Methods"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "WMI provider [Reporting Services], MSReportServer_ConfigurationSetting class"
  - "MSReportServer_ConfigurationSetting class"
ms.assetid: a08c2476-5b8e-4792-94da-1360fe231c6e
caps.latest.revision: 45
author: "douglaslM"
ms.author: "carlasab"
manager: "mblythe"
---
# MSReportServer_ConfigurationSetting Methods
  The MSReportServer_ConfigurationSetting class of the Report Server WMI Provider provides the following public methods.  
  
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
|[ListInstalledSharePointVersions Method &#40;WMI&#41;](../../2014/reporting-services/listinstalledsharepointversions-method-wmi.md)|Returns a set of tokens that represent the versions of Microsoft [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] [!INCLUDE[offSPServ](../../includes/offspserv-md.md)], [!INCLUDE[SPF2010](../../includes/spf2010-md.md)], or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] that are installed on the same computer as the report server.|  
|[ListIPAddresses Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/listipaddresses-method-wmi-msreportserver-configurationsetting.md)|Lists IP addresses for the computer.|  
|[ListReportServersInDatabase](../../2014/reporting-services/listreportserversindatabase-method-wmi-msreportserver-configurationsetting.md)|Returns a list of report server installations that are present in the report server database, regardless of whether those installations have access to secure information.|  
|[ListReservedURLs Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/listreservedurls-method-wmi-msreportserver-configurationsetting.md)|Lists URLs reserved for all applications on the report server.|  
|[ListSSLCertificateBindings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/listsslcertificatebindings-method-wmi-msreportserver-configurationsetting.md)|Lists SSL certificate bindings that exist in HTTP.SYS and those expected from RSReportServer.config.|  
|[ListSSLCertificates Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/listsslcertificates-method-wmi-msreportserver-configurationsetting.md)|Lists installed SSL certificates on the computer.|  
|[ReencryptSecureInformation](../../2014/reporting-services/reencryptsecureinformation-method-wmi-msreportserver-configurationsetting.md)|Generates a new encryption key and re-encrypts all secure information in the report server database using this new key.|  
|[RemoveSSLCertificateBindings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/removesslcertificatebindings-method-wmi-msreportserver-configurationsetting.md)|Remove an SSL Certificate binding.|  
|[RemoveUnattendedExecutionAccount](../../2014/reporting-services/removeunattendedexecutionaccount-method-wmi-msreportserver-configurationsetting.md)|Deletes the unattended execution account entry from the report server configuration.|  
|[RemoveURL Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/removeurl-method-wmi-msreportserver-configurationsetting.md)|Removes a URL reserved for the report server.|  
|[ReserveURL Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/reserveurl-method-wmi-msreportserver-configurationsetting.md)|Adds a URL reservation for a given application.|  
|[RestoreEncryptionKey](../../2014/reporting-services/restoreencryptionkey-method-wmi-msreportserver-configurationsetting.md)|Reapplies the specified encryption key to the report server database.|  
|[SetDatabaseConnection](../../2014/reporting-services/setdatabaseconnection-method-wmi-msreportserver-configurationsetting.md)|Sets the report server database connection to a particular report server database.|  
|[SetDatabaseLogonTimeout](../../2014/reporting-services/setdatabaselogontimeout-method-wmi-msreportserver-configurationsetting.md)|Specifies the default time-out value for report server database logon attempts.|  
|[SetDatabaseQueryTimeout](../../2014/reporting-services/setdatabasequerytimeout-method-wmi-msreportserver-configurationsetting.md)|Specifies the default time-out value for report server database queries.|  
|[SetEmailConfiguration](../../2014/reporting-services/setemailconfiguration-method-wmi-msreportserver-configurationsetting.md)|Configures the e-mail delivery extension used by the report server to send e-mail.|  
|[SetSecureConnectionLevel](../../2014/reporting-services/setsecureconnectionlevel-method-wmi-msreportserver-configurationsetting.md)|Sets the secure connection level of the report server.|  
|[SetServiceState](../../2014/reporting-services/setservicestate-method-wmi-msreportserver-configurationsetting.md)|Turns the Report Server service on and off.|  
|[SetUnattendedExecutionAccount](../../2014/reporting-services/setunattendedexecutionaccount-method-wmi-msreportserver-configurationsetting.md)|Specifies the account used to run reports unattended.|  
|[SetVirtualDirectory Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/setvirtualdirectory-method-wmi-msreportserver-configurationsetting.md)|Sets the virtual directory for an application.|  
|[SetWindowsServiceIdentity](../../2014/reporting-services/setwindowsserviceidentity-method-wmi-msreportserver-configurationsetting.md)|Makes the Report Server service run as the specified Windows user, and grants this account sufficient file system permissions to allow the report server to operate.|  
  
## See Also  
 [MSReportServer_ConfigurationSetting Class](../../2014/reporting-services/msreportserver-configurationsetting-class.md)  
  
  