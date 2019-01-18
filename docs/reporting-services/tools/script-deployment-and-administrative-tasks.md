---
title: "Script Deployment and Administrative Tasks | Microsoft Docs"
ms.date: 05/30/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: tools


ms.topic: conceptual
helpviewer_keywords: 
  - "scripts [Reporting Services]"
  - "moving reports"
  - "report servers [Reporting Services], duplicating settings"
  - "deploying [Reporting Services], scripts"
  - "copying report server settings"
  - "administrative tasks [Reporting Services]"
  - "duplicating report server environment"
  - "migrating reports [Reporting Services]"
  - "scripts [Reporting Services], deployments"
  - "transferrng reports"
  - "reports [Reporting Services], migrating"
ms.assetid: d0416c9e-e3f9-456d-9870-2cfd2c49039b
author: markingmyname
ms.author: maghan
---

# Script Deployment and Administrative Tasks

  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports the use of scripts to automate routine installation, deployment, and administrative tasks. Deploying a report server is a multi-step process. You must use several tools and processes to configure a deployment; there is no single program or approach that can be used to automate all the tasks.  
  
 Not every step should be automated. In some cases, performing a step manually or through a graphical tool is the simplest and most effective approach. For example, if you want to deploy a large number of reports and models, it is better to copy the report server databases rather than write code that recreates report server environment.  
  
 Some steps require custom code. For example, configuring the URLs for the Web service and Report Manager can be automated, but only if you write custom code that makes calls into the Report Server Windows Management Instrumentation (WMI) provider. If you do not want to write code, you must use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to perform the step.  
  
 To run script that configures a report server, you must be a local administrator on the computer that you are configuring. For more information, see [Configure a Report Server for Remote Administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md).  
  
 This topic describes recommended approaches for automating specific steps. Several programs and programmatic interfaces are mentioned; descriptions of each one are provided later in this topic.  
  
## Deployment Tasks and How to Automate Them  
 The following table summarizes the installation and configuration tasks that are necessary for deploying a report server. You can use the table to match a specific task to an approach that allows you to automate or perform the task unattended.  
  
|Task|Approach|  
|----------|--------------|  
|Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|You can run setup from the command line to perform an unattended installation.<br /><br /> You can use Setup to both install and configure a report server, but only if you specify the default configuration option and your system meets all the requirements for this installation type. If you cannot install the default configuration, you must perform a files-only installation.|  
|Configure the service account.|The service account is initially configured through Setup. To automate changes to the service account as a post-Setup task, you must write custom code that makes calls into the Report Server WMI provider. There are no command-prompt utilities or script templates for configuring the service account programmatically.<br /><br /> If coding requirements prevent you from automating this step, you can easily configure the account manually by running the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. For more information, see [Configure a Service Account &#40;SSRS Configuration Manager&#41;](https://msdn.microsoft.com/library/25000ad5-3f80-4210-8331-d4754dc217e0).|  
|Configure the Report Server Web service and Report Manager URLs.|You must write custom code that makes calls into the Report Server WMI provider. There are no command line utilities or script templates for configuring the URLs.<br /><br /> If you want to avoid writing code, you can configure the URLs manually by running the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. For more information, see [Configure a URL  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md).|  
|Create the report server database.|You must write custom code that makes calls into the Report Server WMI provider. There are no command-prompt utilities or script templates for creating the report server databases and RSExecRole.<br /><br /> If you want to avoid writing code, you can create the database manually by running the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. For more information, see [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).|  
|Configure the report server database connection.|If you are changing the connection string, account or password, or the authentication type, run the **rsconfig** utility to configure the connection. For more information, see [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md) and [rsconfig Utility &#40;SSRS&#41;](../../reporting-services/tools/rsconfig-utility-ssrs.md).<br /><br /> You cannot use rsconfig.exe to create or upgrade the database. The database and RSExecRole must already exist.|  
|Configure a scale-out deployment.|Choose from the following approaches to automate scale-out deployment:<br /><br /> -   Run the rskeymgmt.exe utility to join report server instances to an existing installation. For more information, see [Add and Remove Encryption Keys for Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/add-and-remove-encryption-keys-for-scale-out-deployment.md).<br />-   Write custom code that runs against the Report Server WMI provider.|  
|Backup encryption keys.|Choose from the following approaches to automate encryption key backup:<br /><br /> -   Run the rskeymgmt.exe utility to back up the keys. For more information, see [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).<br />-   Write custom code that runs against the Report Server WMI provider.|  
|Configure Report Server E-mail.|Write custom code that runs against the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] WMI provider. The provider supports a subset of the e-mail configuration settings.<br /><br /> Although the RSReportServer.config file includes all the settings, do not use the file in an automated manner. Specifically, do not use a batch file to copy the file to another report server. Each configuration file includes values that are specific to the current instance. Those values will not be valid on other report server instances.<br /><br /> For more information about the settings, see [Configure a Report Server for E-Mail Delivery (SSRS Configuration Manager)](https://msdn.microsoft.com/b838f970-d11a-4239-b164-8d11f4581d83).|  
|Configure the unattended execution account.|Choose from the following approaches to automate unattended processing account configuration:<br /><br /> -   Run the rsconfig.exe utility to configure the account. For more information, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).<br />-   Write custom code that makes calls into the Report Server WMI provider.|  
|Deploy existing content on another report server, including the folder hierarchy, role assignments, reports, subscriptions, schedules, data sources, and resources.|The best way to re-create an existing report server environment is to copy the report server database to a new report server instance.<br /><br /> An alternative approach is to write custom code that recreates existing report server content programmatically. However, be aware that subscriptions, report snapshots, and report history cannot be recreated programmatically.<br /><br /> Some deployments can benefit from using both techniques together (that is, restore a report server database, and then run custom code that modifies the report server database for a specific installation).<br /><br /> For a detailed example, see [Sample Reporting Services rs.exe Script to Copy Content between Report Servers](../../reporting-services/tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).<br /><br /> For more information about relocating a report server database, see [Moving the Report Server Databases to Another Computer &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md). For more information about creating report serer environment programmatically, see the section "Using Script to Migrate Report Server Content and Folders" in this topic.|  
  
## Tools and Technologies for Automating Server deployment  
 The following list summarizes the programs and interfaces that can be used to automate deployment and maintenance tasks:  
  
-   The Setup program can be run in unattended mode to install and sometimes configure report server components. You must use the Files-Only installation option to have Setup configure a report server instance.  
  
-   The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] WMI provider and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] command line utilities can be used for local and remote server configuration.  
  
     The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] WMI provider exposes classes, properties, and methods that allow you to configure all aspects of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation including specifying the service account, configuring URLs, creating and configuring the report server database, or configuring a report server for e-mail delivery. You must write custom code or script to use the WMI provider. For more information, see [Access the Reporting Services WMI Provider](../../reporting-services/tools/access-the-reporting-services-wmi-provider.md).  
  
     An alternative to writing code is to use the command line utilities (rsconfig.exe and rskeymgmt.exe). You can write batch files that run the utilities. You can use the utilities to automate some but not all configuration tasks.  
  
-   The report server script host tool (rs.exe) can run custom [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] code that you might write to re-create or move existing content from one report server to another. With this approach, you write script in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)], save it as an .rss file, and use rs.exe to run the script on the target report server. The script you write can call the SOAP interface to the Report Server Web service. Deployment scripts are written using this approach because it allows you to re-create a report server folder namespace and content, and re-create role-based security.  
  
-   The SQL Server 2012 release introduced PowerShell cmdlets for SharePoint integrated mode. You can use PowerShell to configure and administer the SharePoint integration.  For more information, see [PowerShell cmdlets for Reporting Services SharePoint Mode](../../reporting-services/report-server-sharepoint/powershell-cmdlets-for-reporting-services-sharepoint-mode.md).  
  
## Use Scripts to Migrate Report Server Content and Folders  
 You can write scripts that duplicate a report server environment on another report server instance. Deployment scripts are generally written in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] and then processed using the report server script host utility.  
  
 For a detailed example, see [Sample Reporting Services rs.exe Script to Copy Content between Report Servers](../../reporting-services/tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
 Use scripts to copy folders, shared data sources, resources, reports, role assignments, and settings from one server to another. You write a script for one report server instance, and then run it on another server to re-create the report server namespace. If you have multiple report servers in your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment, you can run the script on each server individually to configure all servers in the same way.  
  
 The following list describes the steps for migrating reports from one server to another.  
  
1.  Set your script variable to the URL of the source report server.  
  
2.  Use the <xref:ReportService2010.ReportingService2010.GetItemDefinition%2A> and <xref:ReportService2010.ReportingService2010.GetProperties%2A> methods to retrieve the report definition and the properties of the report.  
  
3.  Set the URL to point to the destination server.  
  
4.  Use <xref:ReportService2010.ReportingService2010.CreateCatalogItem%2A> method, passing the properties returned from <xref:ReportService2010.ReportingService2010.GetProperties%2A> and the report definition returned by <xref:ReportService2010.ReportingService2010.GetItemDefinition%2A>.  
  
 By using a combination of get and create methods, you can perform similar steps to migrate settings, folders, shared data sources, and resources. For more information about the methods available to you, see [Technical Reference &#40;SSRS&#41;](../../reporting-services/technical-reference-ssrs.md).  
  
> [!NOTE]  
>  Scripts run under the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows credentials of the user running the script unless credentials are explicitly set.  
  
 For more information about how to format and run a script file, see [Script with the rs.exe Utility and the Web Service](../../reporting-services/tools/script-with-the-rs-exe-utility-and-the-web-service.md).  
  
## Using Scripts to Set Server Properties  
 You can write scripts that set system properties on the report server. The following [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] .NET script shows one way to set properties. This example disables the RSClientPrint ActiveX control, but you can replace **EnableClientPrinting** and **False** with any valid property name and value. To view a complete list of server properties, see [Report Server System Properties](../../reporting-services/report-server-web-service/net-framework/reporting-services-properties-report-server-system-properties.md).  
  
 To use the script, save it to a file that has an .rss extension, and then use the rs.exe command prompt utility to run the file on the report server. The script is not compiled, so it is not necessary to have an installation of [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]. This example assumes that you have permissions on the local computer that hosts the report server. If you are not logged on under an account that has permissions, you must specify account information through additional command line arguments. For more information, see [RS.exe Utility &#40;SSRS&#41;](../../reporting-services/tools/rs-exe-utility-ssrs.md).  
  
> [!TIP]  
>  For a detailed example, see [Sample Reporting Services rs.exe Script to Copy Content between Report Servers](../../reporting-services/tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
```  
Public Sub Main()  
        Dim props(0) As [Property]  
        Dim setProp As New [Property]  
        setProp.Name = "EnableClientPrinting"  
        setProp.Value = "False"   
        props(0) = setProp  
        Try  
            rs.SetSystemProperties(props)  
        Catch ex As System.Web.Services.Protocols.SoapException  
            Console.Write(ex.Detail.InnerXml)  
        Catch e as Exception  
            Console.Write(e.Message)  
        End Try  
End Sub  
```

## Next steps

[GenerateDatabaseCreationScript Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-generatedatabasecreationscript.md)   
[GenerateDatabaseRightsScript Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-generatedatabaserightsscript.md)   
[GenerateDatabaseUpgradeScript Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-generatedatabaseupgradescript.md)   
[Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)   
[Install Reporting Services Native Mode Report Server](~/reporting-services/install-windows/install-reporting-services-native-mode-report-server.md)   
[Reporting Services Report Server &#40;Native Mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)   
[Report Server Command Prompt Utilities &#40;SSRS&#41;](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md)   
[Browser Support for Reporting Services and Power View](../../reporting-services/browser-support-for-reporting-services-and-power-view.md)   
[Reporting Services Tools](../../reporting-services/tools/reporting-services-tools.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
