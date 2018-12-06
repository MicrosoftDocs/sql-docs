---
title: "ReportingServicesService Configuration File | Microsoft Docs"
ms.date: 03/15/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "traces [Reporting Services]"
  - "Report Server Windows service, ReportingServicesService configuration file"
  - "ReportingServicesService configuration file"
ms.assetid: 40f4a401-cb61-4c42-b1ec-01acdacdacd1
author: markingmyname
ms.author: maghan 
---
# ReportingServicesService Configuration File

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)]
  
The ReportingServicesService.exe.config file includes settings that configure tracing.  
  
## File Location  
 This file is located in the \Reporting Services\Report Server\Bin folder.  
  
## Editing Guidelines  
 You can modify this file to rename the log file or to increase or decrease trace levels. Do not modify any of the other settings. For instructions, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md). For more information about trace logs, see [Report Server Service Trace Log](../../reporting-services/report-server/report-server-service-trace-log.md).  
  
## Example Configuration  
 The following example shows the settings and default values found in the ReportingServicesService.exe.config file.  
  
```  
<configSections>  
      <section name="RStrace" type="Microsoft.ReportingServices.Diagnostics.RSTraceSectionHandler,Microsoft.ReportingServices.Diagnostics" />  
</configSections>  
\<system.diagnostics>  
      <switches>  
          <add name="DefaultTraceSwitch" value="3" />  
      </switches>  
\</system.diagnostics>  
<RStrace>  
      <add name="FileName" value="ReportServerService_" />  
      <add name="FileSizeLimitMb" value="32" />  
      <add name="KeepFilesForDays" value="14" />  
      <add name="Prefix" value="tid, time" />  
      <add name="TraceListeners" value="debugwindow, file" />  
      <add name="TraceFileMode" value="unique" />  
      <add name="Components" value="all" />  
</RStrace>  
<runtime>  
      <alwaysFlowImpersonationPolicy enabled="true"/>  
      <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">  
             <dependentAssembly>  
                    <assemblyIdentity name="Microsoft.ReportingServices.Interfaces"  
                        publicKeyToken="89845dcd8080cc91"  
                        culture="neutral" />  
                    <bindingRedirect oldVersion="8.0.242.0"  
                                     newVersion="10.0.0.0"/>  
                    <bindingRedirect oldVersion="9.0.242.0"  
                                     newVersion="10.0.0.0"/>  
             </dependentAssembly>  
      </assemblyBinding>  
      <gcServer enabled="true" />  
</runtime>  
```  
  
## Configuration Settings  
 The following table provides information about specific settings. Settings are presented in the order in which they appear in the configuration file.  
  
|Setting|Description|  
|-------------|-----------------|  
|**RStrace**|Specifies namespaces used for errors and tracing.|  
|**DefaultTraceSwitch**|Specifies the level of information that is reported to the ReportServerService trace log. Each level includes the information reported by all lower-numbered levels. Disabling tracing is not recommended. Valid values include:<br /><br /> 0= Disables tracing<br /><br /> 1= Exceptions and restarts<br /><br /> 2= Exceptions, restarts, warnings<br /><br /> 3= Exceptions, restarts, warnings, status messages (default)<br /><br /> 4= Verbose mode|  
|**FileName**|Specifies the first portion of the log file name. The value specified by **Prefix** completes the rest of the name. By default, the name is ReportServerService_.|  
|**FileSizeLimitMb**|Specifies an upper limit on trace log size. The file is measured in megabytes. Valid values are 0 to a maximum integer. The default value is 32.|  
|**KeepFilesForDays**|Specifies the number of days after which a trace log file will be deleted. Valid values are 0 to a maximum integer. The default value is 14.|  
|**Prefix**|Specifies a generated value that distinguishes one log instance from another. By default, timestamp values are appended to trace log file names. This value is set to " tid, time ". Do not modify this setting.|  
|**TraceListeners**|Specifies a target for outputting trace log content. You can specify multiple targets using a comma to separate each one. Valid values include:<br /><br /> DebugWindow (default)<br /><br /> File (default)<br /><br /> StdOut|  
|**TraceFileMode**|Specifies whether trace logs contain data for a 24-hour period. You should have one unique trace log for each component on each day. This value is set to "Unique (default)". Do not modify this value.|  
|**Components**|Specifies the components for which trace logs are created. The default value is **all**. Other valid values for this setting include the names of internal components. Do not modify this value.|  
|**Runtime**|Specifies configuration settings that support backward compatibility with the previous version. Runtime settings are used to redirect requests that target the previous version of Microsoft.ReportingServices.Interfaces to the new version.<br /><br /> All of the configuration settings in this section are described in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] product documentation. For more information, search for "Runtime Schema Settings" on the MSDN Web site or in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] documentation.|  
  
## See Also  
 [Reporting Services Configuration Files](../../reporting-services/report-server/reporting-services-configuration-files.md)   
 [Report Server Service Trace Log](../../reporting-services/report-server/report-server-service-trace-log.md)  
  
  
