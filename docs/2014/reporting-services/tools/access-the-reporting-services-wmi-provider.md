---
title: "Access the Reporting Services WMI Provider | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "Reporting Services WMI Provider"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "WMI provider [Reporting Services]"
  - "programming [Reporting Services]"
ms.assetid: 22cfbeb8-4ea3-4182-8f54-3341c771e87b
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Access the Reporting Services WMI Provider
  The Reporting Services WMI provider exposes two WMI classes for administration of Native mode report server instances through scripting:  
  
> [!IMPORTANT]  
>  Starting with the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] release, the WMI provider is supported for only native mode report servers. SharePoint mode report servers can be managed with SharePoint Central Administration pages and PowerShell scripts.  
  
|Class|Namespace|Description|  
|-----------|---------------|-----------------|  
|MSReportServer_Instance|root\Microsoft\SqlServer\ReportServer\RS_*\<EncodedInstanceName>*\v11|Provides basic information required for a client to connect to an installed report server.|  
|MSReportServer_ConfigurationSetting|root\Microsoft\SqlServer\ReportServer\RS_*\<EncodedInstanceName>*\v11\Admin|Represents the installation and run-time parameters of a report server instance. These parameters are stored in the configuration file for the report server.<br /><br /> **\*\* Important \*\*** This class is only accessible with administrative privileges.|  
  
 An instance of each of the above classes is created for each report server instance. You can use any Microsoft or third party tools to access the WMI objects exposed by the report server, including WMI programming interfaces exposed by the .NET Framework itself. This topic describes how to access and use the WMI class instances with the PowerShell command [Get-WmiObject](https://technet.microsoft.com/library/dd315295.aspx).  
  
## Determine the Instance Name in the Namespace String  
 The instance name in the namespace path for the Reporting Services WMI classes is an encoding of the instance names that you specify when installing the named Reporting Services instances. Namely, special characters in the instance names are encoded. For example, an underline (_) is encoded as "_5f", so an instance name of "My_Instance" is encoded as "My_5fInstance" in the WMI namespace path.  
  
 To list the encoded instance names of your report server instances in the WMI namespace path, use the following PowerShell command:  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace root\Microsoft\SqlServer\ReportServer  -class __Namespace -ComputerName hostname | select Name  
```  
  
## Access the WMI Classes Using PowerShell  
 To access the WMI classes, run the following command:  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace <namespacename> -class <classname> -ComputerName <hostname>  
```  
  
 For example, to access the MSReportServer_ConfigurationSetting class on the default report server instance of the host myrshost, run the following command. The default report server instance must be installed on myrshost for this command to succeed.  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace "root\Microsoft\SqlServer\ReportServer\RS_MSSQLSERER\v11\Admin" -class MSReportServer_ConfigurationSetting -ComputerName myrshost  
```  
  
 This command syntax outputs all class property names and values. Note that all instances of the class MSReportServer_ConfigurationSetting is returned, even though you are accessing the class in the namespace of the default report server instance (RS_MSSQLSERVER). For example, if myrshost is installed with the default report server instance and a named report server instance called SHAREPOINT, this command will return two WMI objects and output the property names and values for both report server instances.  
  
 To return a specific class instance when multiple instances are returned, use the -Filter parameter to filter the results based on properties with unique values such as InstanceName. For example, to return only the WMI object for the default report server instance, use the following command:  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace "root\Microsoft\SqlServer\ReportServer\RS_MSSQLServer\v11\Admin" -class MSReportServer_ConfigurationSetting -ComputerName myrshost -filter "InstanceName='MSSQLSERVER'"  
```  
  
## Query the Available Methods and Properties  
 To see what methods and properties are available in one of the Reporting Services WMI classes, pipe the results from Get-WmiObject to the Get-Member command. For example:  
  
```  
PS C:\windows\system32> Get-WmiObject -namespace "root\Microsoft\SqlServer\ReportServer\RS_MSSQLServer\v11\Admin" -class MSReportServer_ConfigurationSetting -ComputerName myrshost | Get-Member  
```  
  
 For documentation on the properties and methods of the Reporting Services WMI classes, see ....  
  
## Use a WMI Method or Property  
 Once you have the WMI objects to the Reporting Services classes and know the available methods and properties, you can use these methods and properties. For example, if you have a named report server instance in SharePoint integrated mode called SHAREPOINT, use the following command sequence to retrieve the URL for the SharePoint Central Administration site:  
  
```  
PS C:\windows\system32> $rsconfig = Get-WmiObject -namespace "root\Microsoft\SqlServer\ReportServer\RS_MSSQLServer\v11\Admin" -class MSReportServer_ConfigurationSetting -ComputerName myrshost -filter "InstanceName='SHAREPOINT'"  
PS C:\windows\system32> $rsconfig.GetAdminSiteUrl()  
  
```  
  
## See Also  
 [Reporting Services WMI Provider Library Reference &#40;SSRS&#41;](../wmi-provider-library-reference/reporting-services-wmi-provider-library-reference-ssrs.md)   
 [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md)  
  
  
