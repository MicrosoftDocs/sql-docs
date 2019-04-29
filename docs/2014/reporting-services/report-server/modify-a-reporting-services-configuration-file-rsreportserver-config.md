---
title: "Modify a Reporting Services Configuration File (RSreportserver.config) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 958ef51f-2699-4cb2-a92e-3b4322e36a30
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Modify a Reporting Services Configuration File (RSreportserver.config)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores application settings in a set of configuration files. Setup creates the configuration files for each report server instance you install. Within each file, values are either set during installation or when you use tools and applications to configure a server for operation. In some cases, you must modify a file directly to add or configure advanced settings. Configuration settings are specified as either XML elements or attributes. If you understand XML and configuration files, you can use a text or code editor to modify user-definable settings.  
  
 Some configuration settings can be set only through a tool. Settings that contain encrypted values must be modified through the Reporting Services Configuration tool, the Setup program, or the **rsconfig** command line utility. You must be a member of the local Administrators group to run these tools.'  
  
> [!IMPORTANT]  
>  Use caution when modifying configuration files. If you modify a setting that is reserved for internal use, you may disable your installation. Generally, modifying configuration settings is not recommended unless you are trying to solve a specific problem. For more information about which settings are safe to change, see [RSReportServer Configuration File](rsreportserver-config-configuration-file.md) or [RSReportDesigner Configuration File](rsreportdesigner-configuration-file.md). For more information about configuration files, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] product documentation.  
  
 In this topic:  
  
-   [Reading and Using Configuration Values](#bkmk_read_values)  
  
-   [About Default Values](#bkmk_default_values)  
  
-   [Deleting Configuration Settings](#bkmk_delete_config_settings)  
  
-   [To Edit a Reporting Services Configuration File](#bkmk_edit_configuation_file)  
  
##  <a name="bkmk_read_values"></a> Reading and Using Configuration Values  
 A report server reads the configuration files when the service starts and whenever the configuration file is saved. New and revised values take effect in a new application domain after the current application domain expires. Whenever possible, requests that are still processing in the current application domain are allowed to complete. However, a few settings require an immediate application domain recycle operation. In this case, all requests that are in process are restarted in a new application domain.  
  
 If the report server detects an invalid value, the report server logs an error to the Windows application log and either fails to start or uses a default value, depending on the error:  
  
-   If the error is malformed XML, the report server will not start. If the report server is running when you introduce the error, the report server ignores the invalid configuration file until the report server restarts or the application domain is recycled. Once the error is detected, the report server will no longer start.  
  
-   If the error is an invalid configuration value, the server will use internal default values and log an error to the trace log files. In the few cases where internal default values are not available, the server will return the rsServerConfigurationError error if the invalid configuration setting is critical to server operations. Errors about missing or invalid critical settings are returned to the client application in an HTML error page and logged to the event log.  
  
 All configuration file changes, including successful changes, are recorded in the report server trace log file. Only errors are logged to the application event log.  
  
##  <a name="bkmk_default_values"></a> About Default Values  
 Most configuration settings have default values that are specified internally in the report server. The report server will use these values if a user-defined value is invalid or not specified. If a default value must be used due to an invalid configuration setting, an error is written to the trace log file.  
  
##  <a name="bkmk_delete_config_settings"></a> Deleting Configuration Settings  
 For configuration settings that have default values, removing the setting from the configuration file has no effect. Most configuration settings are actually defined and configured internally. If you delete an item from the configuration file, the internal copy is still available and uses the default value that is defined for it.  
  
##  <a name="bkmk_edit_configuation_file"></a> To Edit a Reporting Services Configuration File  
  
1.  Find the configuration file you want to edit:  
  
    -   **RSReportServer.config** is located in the following folder:  
  
        ```  
        C:\Program Files\Microsoft SQL Server\MSRS11.MSSQLSERVER\Reporting Services\ReportServer  
        ```  
  
    -   **RSReportServerServices.exe.config** is located in the following folder:  
  
        ```  
        C:\Program Files\Microsoft SQL Server\MSRS11.MSSQLSERVER\Reporting Services\ReportServer\bin  
        ```  
  
    -   **RSReportDesigner.config** is located in the following folder:  
  
        ```  
        C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\PrivateAssemblies  
        ```  
  
2.  Save a copy of the file in case you need to roll back your changes.  
  
3.  Open the original file in Notepad or a code editor. Do not use Textpad; it sets the file length before the file is saved, causing an invalid character error to be written to the trace log file.  
  
4.  Type the element or value that you want to add or use. Elements are case-sensitive. If you are adding an element, be sure to use the correct upper and lower case letters. Specific instructions for editing configuration files are available if you are customizing rendering extension, authentication extensions, or data processing extensions:  
  
    -   [Authentication with the Report Server](../security/authentication-with-the-report-server.md)  
  
    -   [Configure Report Manager to Pass Custom Authentication Cookies](../security/configure-the-web-portal-to-pass-custom-authentication-cookies.md)  
  
    -   [Customize Rendering Extension Parameters in RSReportServer.Config](../customize-rendering-extension-parameters-in-rsreportserver-config.md)  
  
5.  Save the file.  
  
6.  Check the trace log files to verify that errors did not occur. If you see error conditions, a setting or its value is specified incorrectly. Review the [RSReportServer Configuration File](rsreportserver-config-configuration-file.md) for valid values for any setting that is causing an error. For more information about how to view the trace log, see [Report Server Service Trace Log](report-server-service-trace-log.md).  
  
## See Also  
 [RSReportServer Configuration File](rsreportserver-config-configuration-file.md)   
 [ReportingServicesService Configuration File](reportingservicesservice-configuration-file.md)   
 [RSReportDesigner Configuration File](rsreportdesigner-configuration-file.md)   
 [Deploying a Data Processing Extension](../extensions/data-processing/deploying-a-data-processing-extension.md)   
 [Deploying a Delivery Extension](../extensions/delivery-extension/deploying-a-delivery-extension.md)   
 [Deploying a Rendering Extension](../extensions/rendering-extension/deploying-a-rendering-extension.md)   
 [How to: Deploy a Custom Report Item](../custom-report-items/how-to-deploy-a-custom-report-item.md)   
 [Reporting Services Configuration Files](reporting-services-configuration-files.md)  
  
  
