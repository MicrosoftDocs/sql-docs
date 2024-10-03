---
title: "Modify a Reporting Services configuration file (RSreportserver.config)"
description: Learn how to modify configuration files in Reporting Services to add or configure advanced settings. Some settings are for internal use. Proceed with caution.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Modify a Reporting Services configuration file (RSreportserver.config)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores application settings in a set of configuration files. Setup creates the configuration files for each report server instance you install. Within each file, values are either set during installation or when you use tools and applications to configure a server for operation. In some cases, you must modify a file directly to add or configure advanced settings. Configuration settings are specified as either XML elements or attributes. If you understand XML and configuration files, you can use a text or code editor to modify user-definable settings.  
  
 Some configuration settings can be set only through a tool. Settings that contain encrypted values must be modified through the Reporting Services Configuration tool, the Setup program, or the `rsconfig` command line utility. You must be a member of the local Administrators group to run these tools.'  
  
> [!IMPORTANT]
>  Use caution when modifying configuration files. If you modify a setting that is reserved for internal use, you might disable your installation. Generally, modifying configuration settings isn't recommended unless you are trying to solve a specific problem. For more information about which settings are safe to change, see [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md) or [RSReportDesigner configuration file](../../reporting-services/report-server/rsreportdesigner-configuration-file.md). For more information about configuration files, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] product documentation.  
  
 In this article:  
  
-   [Read and use configuration values](#bkmk_read_values)  
  
-   [About default values](#bkmk_default_values)  
  
-   [Delete configuration settings](#bkmk_delete_config_settings)  
  
-   [Edit a Reporting Services configuration file](#bkmk_edit_configuation_file)  
  
##  <a name="bkmk_read_values"></a> Read and use configuration values  
 A report server reads the configuration files when the service starts and whenever the configuration file is saved. New and revised values take effect in a new application domain after the current application domain expires. Whenever possible, requests that are still processing in the current application domain are allowed to complete. However, a few settings require an immediate application domain recycle operation. In this case, all requests that are in process are restarted in a new application domain.  
  
 If the report server detects an invalid value, the report server logs an error to the Windows application log and either fails to start or uses a default value, depending on the error:  
  
-   If the error is malformed XML, the report server doesn't start. If the report server is running when you introduce the error, the report server ignores the invalid configuration file. It ignores the file until the report server restarts or the application domain is recycled. Once the error is detected, the report server no longer starts.  
  
-   If the error is an invalid configuration value, the server uses internal default values and logs an error to the trace log files. In the few cases where internal default values aren't available, the server returns the `rsServerConfigurationError` error if the invalid configuration setting is critical to server operations. Errors about missing or invalid critical settings are returned to the client application in an HTML error page and logged to the event log.  
  
 All configuration file changes, including successful changes, are recorded in the report server trace log file. Only errors are logged to the application event log.  
  
##  <a name="bkmk_default_values"></a> About default values  
 Most configuration settings have default values that are specified internally in the report server. The report server uses these values if a user-defined value is invalid or not specified. If a default value must be used due to an invalid configuration setting, an error is written to the trace log file.  
  
##  <a name="bkmk_delete_config_settings"></a> Delete configuration settings  
 For configuration settings that have default values, removing the setting from the configuration file has no effect. Most configuration settings are defined and configured internally. If you delete an item from the configuration file, the internal copy is still available and uses the default value that is defined for it.  
  
##  <a name="bkmk_edit_configuation_file"></a> Edit a Reporting Services configuration file  
  
1.  Find the configuration file you want to edit:  
  
    -   `RSReportServer.config` is located in the following folder:  
  
        ```  
        C:\Program Files\Microsoft SQL Server\MSRS13.MSSQLSERVER\Reporting Services\ReportServer  
        ```  
        
        [!INCLUDE[applies](../../includes/applies-md.md)] January 2017 Technical Preview of Power BI reports in SQL Server Reporting Services
        
        ```  
        C:\Program Files\Microsoft SQL Server Reporting Services\RSServer\ReportServer
        ```
  
    -   `RSReportServerServices.exe.config` is located in the following folder:  
    
        > [!NOTE] 
        > This isn't available with the January 2017 Technical Preview of Power BI reports in SQL Server Reporting Services.
  
        ```  
        C:\Program Files\Microsoft SQL Server\MSRS13.MSSQLSERVER\Reporting Services\ReportServer\bin  
        ```  
  
    -   `RSReportDesigner.config` is located in the following folder:  
  
        ```  
        C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\PrivateAssemblies  
        ```  
  
1.  Save a copy of the file in case you need to roll back your changes.  
  
1.  Open the original file in Notepad or a code editor. Don't use Textpad. It sets the file length before the file is saved, causing an invalid character error to be written to the trace log file.  
  
1.  Type the element or value that you want to add or use. Elements are case-sensitive. If you're adding an element, be sure to use the correct upper and lower case letters. Specific instructions for editing configuration files are available if you're customizing rendering extension, authentication extensions, or data processing extensions:  
  
    -   [Authentication with the report server](../../reporting-services/security/authentication-with-the-report-server.md)  
  
    -   [Configure the web portal to pass custom authentication cookies](../../reporting-services/security/configure-the-web-portal-to-pass-custom-authentication-cookies.md)
  
    -   [Customize rendering extension parameters in RSReportServer.Config](../../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)  
  
1.  Save the file.  
  
1.  Check the trace log files to verify that errors didn't occur. If you see error conditions, a setting or its value is specified incorrectly. Review the [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md) for valid values for any setting that is causing an error. For more information about how to view the trace log, see [Report server service trace log](../../reporting-services/report-server/report-server-service-trace-log.md).  
  
## Related content

- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
- [ReportingServicesService configuration file](../../reporting-services/report-server/reportingservicesservice-configuration-file.md)
- [RSReportDesigner configuration file](../../reporting-services/report-server/rsreportdesigner-configuration-file.md)
- [Deploy a data processing extension](../../reporting-services/extensions/data-processing/deploying-a-data-processing-extension.md)
- [Deploy a delivery extension](../../reporting-services/extensions/delivery-extension/deploying-a-delivery-extension.md)
- [Deploy a rendering extension](../../reporting-services/extensions/rendering-extension/deploying-a-rendering-extension.md)
- [Reporting Services configuration files](../../reporting-services/report-server/reporting-services-configuration-files.md)
