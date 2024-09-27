---
title: "Access the Reporting Services WMI provider"
description: Learn how to access the Reporting Services WMI provider. The WMI provider exposes two WMI classes for administration of Native mode report server instances through scripting.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "WMI provider [Reporting Services]"
  - "programming [Reporting Services]"
apilocation: "reportingservices.mof"
apiname: "Reporting Services WMI Provider"
---
# Access the Reporting Services WMI provider
  The Reporting Services WMI provider exposes two WMI classes for administration of Native mode report server instances through scripting:  
  
> [!IMPORTANT]  
>  Starting with the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] release, the WMI provider is supported for only native mode report servers. SharePoint mode report servers can be managed with SharePoint Central Administration pages and PowerShell scripts.  
  
|Class|Namespace|Description|  
|-----------|---------------|-----------------|  
|MSReportServer_Instance|root\Microsoft\SqlServer\ReportServer\RS_*\<EncodedInstanceName>*\v13|Provides basic information required for a client to connect to an installed report server.|  
|MSReportServer_ConfigurationSetting|root\Microsoft\SqlServer\ReportServer\RS_*\<EncodedInstanceName>*\v13\Admin|Represents the installation and run-time parameters of a report server instance. These parameters are stored in the configuration file for the report server.<br /><br /> **\*\* Important \*\*** This class is only accessible with administrative privileges.|  
  
 An instance of each of the above classes is created for each report server instance. You can use any Microsoft or non-Microsoft tools to access the WMI objects exposed by the report server, including WMI programming interfaces exposed by the .NET Framework itself. This article describes how to access and use the WMI class instances with the PowerShell command [Get-WmiObject](/previous-versions//dd315295(v=technet.10)).  
  
## Determine the instance name in the namespace string  
 The instance name in the namespace path for the Reporting Services WMI classes is an encoding of the instance names that you specify when installing the named Reporting Services instances. Namely, special characters in the instance names are encoded. For example, an underline (_) is encoded as `_5f`, so an instance name of `My_Instance` is encoded as `My_5fInstance` in the WMI namespace path.  
  
 To list the encoded instance names of your report server instances in the WMI namespace path, use the following PowerShell command:  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace root\Microsoft\SqlServer\ReportServer  -class __Namespace -ComputerName hostname | select Name  
```  
  
## Access the WMI classes by using PowerShell  
 To access the WMI classes, run the following command:  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace <namespacename> -class <classname> -ComputerName <hostname>  
```  
  
 For example, to access the MSReportServer_ConfigurationSetting class on the default report server instance of the host myrshost, run the following command. The default report server instance must be installed on myrshost for this command to succeed.  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace "root\Microsoft\SqlServer\ReportServer\RS_MSSQLSERER\v11\Admin" -class MSReportServer_ConfigurationSetting -ComputerName myrshost  
```  
  
 This command syntax outputs all class property names and values. All instances of the class MSReportServer_ConfigurationSetting are returned, even though you're accessing the class in the namespace of the default report server instance (RS_MSSQLSERVER). For example, myrshost might be installed with the default report server instance and a named report server instance called SHAREPOINT. This command returns two WMI objects and output the property names and values for both report server instances.  
  
 To return a specific class instance when multiple instances are returned, use the -Filter parameter to filter the results based on properties with unique values such as InstanceName. For example, to return only the WMI object for the default report server instance, use the following command:  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace "root\Microsoft\SqlServer\ReportServer\RS_MSSQLServer\v13\Admin" -class MSReportServer_ConfigurationSetting -ComputerName myrshost -filter "InstanceName='MSSQLSERVER'"  
```  
  
## Query the available methods and properties  
 To see what methods and properties are available in one of the Reporting Services WMI classes, pipe the results from Get-WmiObject to the Get-Member command. For example:  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace "root\Microsoft\SqlServer\ReportServer\RS_MSSQLServer\v13\Admin" -class MSReportServer_ConfigurationSetting -ComputerName myrshost | Get-Member  
```  
  
## Use a WMI method or property  
 Once you have the WMI objects to the Reporting Services classes and know the available methods and properties, you can use these methods and properties. For example, if you have a named report server instance in SharePoint integrated mode called SHAREPOINT, use the following command sequence to retrieve the URL for the SharePoint Central Administration site:  
  
```  
PS C:\windows\system32> $rsconfig = Get-WmiObject -namespace "root\Microsoft\SqlServer\ReportServer\RS_MSSQLServer\v13\Admin" -class MSReportServer_ConfigurationSetting -ComputerName myrshost -filter "InstanceName='SHAREPOINT'"  
PS C:\windows\system32> $rsconfig.GetAdminSiteUrl()  
  
```  
  
## Related content

- [Reporting Services WMI provider library reference &#40;SSRS&#41;](../../reporting-services/wmi-provider-library-reference/reporting-services-wmi-provider-library-reference-ssrs.md)
- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
