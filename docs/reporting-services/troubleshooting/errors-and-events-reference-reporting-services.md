---
title: "Errors and events reference (Reporting Services)"
description: View the ID, type, category, source, and description for different report server events. These events include error, warning, and information types.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---

# Errors and events reference (Reporting Services)

This article provides information about errors and events for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] log files also contain error information. For more information about the types of log files that are available and how to view the logs, see [Reporting Services log files and sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md).  

## Cause and resolution for reporting services error messages  

Cause and resolution information is available for the errors most frequently searched for on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] web sites. For more information, see [Cause and resolution of Reporting Services errors](../../reporting-services/troubleshooting/cause-and-resolution-of-reporting-services-errors.md).  
  
## Report server events

The following report server events are recorded in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log.  
  
|Event ID|Type|Category|Source|Description|  
|--------------|----------|--------------|------------|-----------------|  
|106|Error|Scheduling|Report Server|SQL Server Agent must be running when you define a scheduled operation (for example, report subscription and delivery).|  
|[107](../../reporting-services/troubleshooting/report-server-windows-service-mssqlserver-107.md)|Error|Startup/Shutdown|Report Server<br /><br /> Scheduling and Delivery Processor|*\<Source>* can't connect to the report server database. For more information, see [Report Server Windows Service &#40;MSSQLServer&#41; 107](../../reporting-services/troubleshooting/report-server-windows-service-mssqlserver-107.md).|  
|108|Error|Extension|Report Server<br /><br /> web portal|*\<Source>* can't load a delivery, data processing, or rendering extension.<br /><br /> Most likely, this error is the result of an incomplete deployment or removal of an extension. For more information, see [Deploy a data processing extension](../../reporting-services/extensions/data-processing/deploying-a-data-processing-extension.md) and [Deploy a delivery extension](../../reporting-services/extensions/delivery-extension/deploying-a-delivery-extension.md).|  
|109|Information|Management|Report Server<br /><br /> web portal|A configuration file was modified. For more information, see [Reporting Services configuration files](../../reporting-services/report-server/reporting-services-configuration-files.md).|  
|110|Warning|Management|Report Server<br /><br /> web portal|A setting in one of the configuration files was modified such that it's no longer valid. A default value is used instead. For more information, see [Reporting Services configuration files](../../reporting-services/report-server/reporting-services-configuration-files.md).|  
|111|Error|Logging|Report Server<br /><br /> web portal|*\<Source>* can't create the trace log. For more information, see [Report server service trace log](../../reporting-services/report-server/report-server-service-trace-log.md).|  
|112|Warning|Security|Report Server|The report server detected a possible denial of service attack. For more information, see [Reporting Services security and protection](../../reporting-services/security/reporting-services-security-and-protection.md).|  
|113|Error|Logging|Report Server|The report server can't create a performance counter.|  
|114|Error|Startup/Shutdown|web portal|The web portal can't connect to the Report Server service.|  
|115|Warning|Scheduling|Scheduling and Delivery Processor|A scheduled task in the SQL Server Agent queue was modified or deleted.|  
|116|Error|Internal|Report Server<br /><br /> web portal<br /><br /> Scheduling and Delivery Processor|An internal error occurred.|  
|117|Error|Startup/Shutdown|Report Server|The report server database is an invalid version.|  
|118|Warning|Logging|Report Server<br /><br /> web portal|The trace log isn't at the expected directory location; a new trace log is created in the default directory. For more information, see [Report server service trace log](../../reporting-services/report-server/report-server-service-trace-log.md).|  
|119|Error|Activation|Report Server<br /><br /> Scheduling and Delivery Processor|*\<Source>* wasn't granted access to the contents of the report server database.|  
|120|Error|Activation|Report Server|The symmetric key can't be decrypted. Most likely, there was a change to the account that the service runs under. For more information, see [Configure and manage encryption keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md).|  
|121|Error|Startup/Shutdown|Report Server|Remote Procedure Call (RPC) Service failed to start.|  
|122|Warning|Delivery|Scheduling and Delivery Processor|Scheduling and Delivery Processor can't connect to the SMTP server that is used for e-mail delivery. For more information about SMTP server connections, see [E-mail settings - Reporting Services native mode (Configuration Manager)](../install-windows/e-mail-settings-reporting-services-native-mode-configuration-manager.md).|  
|123|Warning|Logging|Report Server<br /><br /> web portal|The report server failed to write to the trace log. For more information about trace logs, see [Report server service trace log](../../reporting-services/report-server/report-server-service-trace-log.md).|  
|124|Information|Activation|Report Server|The Report Server service was initialized. For more information, see [Initialize a report server &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-initialize-a-report-server.md).|  
|125|Information|Activation|Report Server|The key used for encrypting data was successfully extracted. For more information about keys, see [Configure and manage encryption keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md).|  
|126|Information|Activation|Report Server|The key used for encrypting data was successfully applied. For more information about keys, see [Configure and manage encryption keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md).|  
|127|Information|Activation|Report Server|Encrypted content was successfully removed from the report server database. For more information about deleting nonrecoverable encrypted data, see [Configure and manage encryption keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md).|  
|128|Error|Activation|Report Server|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components from different editions can't be used together.|  
|129|Error|Management|Report Server<br /><br /> Scheduling and Delivery Processor|An encrypted configuration file setting can't be decrypted.|  
|130|Error|Management|Report Server<br /><br /> Scheduling and Delivery Processor|*\<Source>* can't find the configuration file. The report server requires configuration files.|  
|131|Error|Security|Report Server<br /><br /> Scheduling and Delivery Processor|An encrypted user data value couldn't be decrypted.|  
|132|Error|Security|Report Server|A failure occurred during encryption of user data. The value can't be saved.|  
|133|Error|Management|Report Server<br /><br /> web portal<br /><br /> Scheduling and Delivery Processor|A configuration file failed to load. This error might occur if the XML isn't valid.|  
|134|Error|Management|Report Server|The report server failed to encrypt values for a setting in a configuration file.|  
  
## Related content

- [Monitor Reporting Services subscriptions](../../reporting-services/subscriptions/monitor-reporting-services-subscriptions.md)
- [Reporting Services log files and sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md)
