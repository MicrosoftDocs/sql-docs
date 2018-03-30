---
title: "MSReportServer_ConfigurationSetting Properties | Microsoft Docs"
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
  - "MSReportServer_ConfigurationSetting Properties"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "WMI provider [Reporting Services], MSReportServer_ConfigurationSetting class"
  - "MSReportServer_ConfigurationSetting class"
ms.assetid: e75fe1e5-197b-4c65-859b-370821cad003
caps.latest.revision: 45
author: "douglaslM"
ms.author: "carlasab"
manager: "mblythe"
---
# MSReportServer_ConfigurationSetting Properties
  The MSReportServer_ConfigurationSetting class represents the installation and runtime parameters of a report server instance. These settings are stored in the RSReportServer.config configuration file.  
  
## Public Properties  
  
|||  
|-|-|  
|[ConnectionPoolSize](../../2014/reporting-services/connectionpoolsize-property-wmi-msreportserver-configurationsetting.md)|Returns the connection pool size used by the report server to communicate with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonAccount](../../2014/reporting-services/databaselogonaccount-property-wmi-msreportserver-configurationsetting.md)|Specifies the logon account used by the report server to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonTimeout](../../2014/reporting-services/databaselogontimeout-property-wmi-msreportserver-configurationsetting.md)|Specifies the number of seconds to wait before an attempt to log on to the report server database fails. Read-only.|  
|[DatabaseLogonType](../../2014/reporting-services/databaselogontype-property-wmi-msreportserver-configurationsetting.md)|Specifies whether the report server uses a Windows service account, a Windows user account, or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to access the report server database. Read-only.|  
|[DatabaseName](../../2014/reporting-services/databasename-property-wmi-msreportserver-configurationsetting.md)|Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database.|  
|[DatabaseQueryTimeout](../../2014/reporting-services/databasequerytimeout-property-wmi-msreportserver-configurationsetting.md)|Specifies the number of seconds that must elapse before the command fails or times out. The report server is timing the process against the SQL Server catalog, not a data source for the report.|  
|[DatabaseServerName](../../2014/reporting-services/databaseservername-property-wmi-msreportserver-configurationsetting.md)|Specifies the name of the server on which the report server database is installed.|  
|[InstallationID Property](../../2014/reporting-services/installationid-property-wmi-msreportserver-configurationsetting.md)|Returns a unique identifier for a specific report server instance.|  
|[InstanceName](../../2014/reporting-services/instancename-property-wmi-msreportserver-configurationsetting.md)|Specifies the name of a report server instance on a specific computer.|  
|[IsInitialized](../../2014/reporting-services/isinitialized-property-wmi-msreportserver-configurationsetting.md)|Indicates whether the report server instance is initialized.  Read-only.|  
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
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../2014/reporting-services/msreportserver-configurationsetting-members.md)  
  
  