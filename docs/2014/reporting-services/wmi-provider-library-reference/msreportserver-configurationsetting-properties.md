---
title: "MSReportServer_ConfigurationSetting Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
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
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# MSReportServer_ConfigurationSetting Properties
  The MSReportServer_ConfigurationSetting class represents the installation and runtime parameters of a report server instance. These settings are stored in the RSReportServer.config configuration file.  
  
## Public Properties  
  
|||  
|-|-|  
|[ConnectionPoolSize](configurationsetting-property-connectionpoolsize.md)|Returns the connection pool size used by the report server to communicate with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonAccount](configurationsetting-property-databaselogonaccount.md)|Specifies the logon account used by the report server to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonTimeout](configurationsetting-property-databaselogontimeout.md)|Specifies the number of seconds to wait before an attempt to log on to the report server database fails. Read-only.|  
|[DatabaseLogonType](configurationsetting-property-databaselogontype.md)|Specifies whether the report server uses a Windows service account, a Windows user account, or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to access the report server database. Read-only.|  
|[DatabaseName](configurationsetting-property-databasename.md)|Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database.|  
|[DatabaseQueryTimeout](configurationsetting-property-databasequerytimeout.md)|Specifies the number of seconds that must elapse before the command fails or times out. The report server is timing the process against the SQL Server catalog, not a data source for the report.|  
|[DatabaseServerName](configurationsetting-property-databaseservername.md)|Specifies the name of the server on which the report server database is installed.|  
|[InstallationID Property](configurationsetting-property-installationid.md)|Returns a unique identifier for a specific report server instance.|  
|[InstanceName](configurationsetting-property-instancename.md)|Specifies the name of a report server instance on a specific computer.|  
|[IsInitialized](configurationsetting-property-isinitialized.md)|Indicates whether the report server instance is initialized.  Read-only.|  
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
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
