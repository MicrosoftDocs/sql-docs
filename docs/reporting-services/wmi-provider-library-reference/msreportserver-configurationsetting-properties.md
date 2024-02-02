---
title: "MSReportServer_ConfigurationSetting properties"
description: "MSReportServer_ConfigurationSetting properties"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "WMI provider [Reporting Services], MSReportServer_ConfigurationSetting class"
  - "MSReportServer_ConfigurationSetting class"
apilocation: "reportingservices.mof"
apiname: "MSReportServer_ConfigurationSetting Properties"
apitype: MOFDef
---
# MSReportServer_ConfigurationSetting properties
  The *MSReportServer_ConfigurationSetting* class represents the installation and runtime parameters of a report server instance. These settings are stored in the `RSReportServer.config` configuration file.  
  
## Public properties  
  
|Property|Description|  
|-|-|  
|[ConnectionPoolSize](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-connectionpoolsize.md)|Returns the connection pool size used by the report server to communicate with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonAccount](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogonaccount.md)|Specifies the sign in account used by the report server to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database. Read-only.|  
|[DatabaseLogonTimeout](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogontimeout.md)|Specifies the number of seconds to wait before an attempt to sign in to the report server database fails. Read-only.|  
|[DatabaseLogonType](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogontype.md)|Specifies whether the report server uses a Windows service account, a Windows user account, or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign in to access the report server database. Read-only.|  
|[DatabaseName](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databasename.md)|Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database.|  
|[DatabaseQueryTimeout](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databasequerytimeout.md)|Specifies the number of seconds that must elapse before the command fails or times out. The report server is timing the process against the SQL Server catalog, not a data source for the report.|  
|[DatabaseServerName](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaseservername.md)|Specifies the name of the server on which the report server database is installed.|  
|[InstallationID Property](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-installationid.md)|Returns a unique identifier for a specific report server instance.|  
|[InstanceName](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-instancename.md)|Specifies the name of a report server instance on a specific computer.|  
|[IsInitialized](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-isinitialized.md)|Indicates whether the report server instance is initialized.  Read-only.|  
|[IsSharePointIntegrated](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-issharepointintegrated.md)|Indicates whether the report server is configured for SharePoint integrated mode.|  
|[IsWebServiceEnabled](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-iswebserviceenabled.md)|Indicates whether the Report Server Web service is enabled. Read-only.|  
|[IsWindowsServiceEnabled](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-iswindowsserviceenabled.md)|Indicates whether the Report Server Windows service is enabled. Read-only.|  
|[MachineAccountIdentity Property &#40;WMI&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-machineaccountidentity.md)|Gets the machine account identity of the computer that the report server is installed on.|  
|[PathName](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-pathname.md)|Specifies the installation path to a report server instance.|  
|[SecureConnectionLevel](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-secureconnectionlevel.md)|Returns the secure connection level specified in the RSReportServer.config file.|  
|[SenderEmailAddress](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-senderemailaddress.md)|Gets the address used to send email from the report server. Read-only.|  
|[SendUsingSMTPServer](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-sendusingsmtpserver.md)|Specifies whether the *SendUsing* property in the email configuration is set to **TRUE**.|  
|[SMTPServer](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-smtpserver.md)|Gets the *SMTPServer* property from the `RSReportServer.config` file. Read-only.|  
|[UnattendedExecutionAccount](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-unattendedexecutionaccount.md)|Specifies the sign in user account that the report server impersonates when running reports unattended. Read-only.|  
|[Version](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-version.md)|Returns the version of the report server.|  
|[VirtualDirectoryReportManager Property &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-virtualdirectoryreportmanager.md)|Returns the virtual directory for the report manager application.|  
|[VirtualDirectoryReportServer Property &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-virtualdirectoryreportserver.md)|Returns the Virtual directory for the report server web service application.|  
|[WindowsServiceIdentityActual](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-windowsserviceidentityactual.md)|Returns the identity that the Report Server Windows service is actually running under. Read-only.|  
|[WindowsServiceIdentityConfigured](../../reporting-services/wmi-provider-library-reference/windowsserviceidentityconfigured-property.md)|Returns the identity that the Report Server Windows service was last configured to run under. Read-only.|  
  
## Related content 
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  

  
