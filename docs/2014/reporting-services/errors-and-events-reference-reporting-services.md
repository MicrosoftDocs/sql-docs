---
title: "Errors and Events Reference (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "messages [Reporting Services]"
  - "errors [Reporting Services]"
  - "Reporting Services, errors and events"
  - "troubleshooting [Reporting Services], errors"
  - "events [Reporting Services]"
ms.assetid: 818b4cc1-e65d-4f1a-bf7d-fe269e6dd739
caps.latest.revision: 40
author: "markingmyname"
ms.author: "maghan"
manager: "mblythe"
---
# Errors and Events Reference (Reporting Services)
  This topic provides information about errors and events for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] log files also contain error information. For more information about the types of log files that are available and how to view the logs, see [Reporting Services Log Files and Sources](../../2014/reporting-services/reporting-services-log-files-and-sources.md).  
  
## Cause and Resolution for Reporting Services Error Messages  
 Cause and resolution information is available for the errors most frequently searched for on the [!INCLUDE[msCoName](../includes/msconame-md.md)] Web sites. For more information, see [Cause and Resolution of Reporting Services Errors](../../2014/reporting-services/cause-and-resolution-of-reporting-services-errors.md).  
  
## Report Server Events  
 The following report server events are recorded in the [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows application log.  
  
|Event ID|Type|Category|Source|Description|  
|--------------|----------|--------------|------------|-----------------|  
|106|Error|Scheduling|Report Server|SQL Server Agent must be running when you define a scheduled operation (for example, report subscription and delivery).|  
|[107](../../2014/reporting-services/report-server-windows-service-mssqlserver-107.md)|Error|Startup/Shutdown|Report Server<br /><br /> Scheduling and Delivery Processor|*\<Source>* cannot connect to the report server database. For more information, see [Report Server Windows Service &#40;MSSQLServer&#41; 107](../../2014/reporting-services/report-server-windows-service-mssqlserver-107.md).|  
|108|Error|Extension|Report Server<br /><br /> Report Manager|*\<Source>* cannot load a delivery, data processing, or rendering extension.<br /><br /> Most likely, this is the result of an incomplete deployment or removal of an extension. For more information, see [Deploying a Data Processing Extension](../../2014/reporting-services/dev-guide/deploying-a-data-processing-extension.md) and [Deploying a Delivery Extension](../../2014/reporting-services/dev-guide/deploying-a-delivery-extension.md).|  
|109|Information|Management|Report Server<br /><br /> Report Manager|A configuration file has been modified. For more information, see [Reporting Services Configuration Files](../../2014/reporting-services/reporting-services-configuration-files.md).|  
|110|Warning|Management|Report Server<br /><br /> Report Manager|A setting in one of the configuration files has been modified such that it is no longer valid. A default value will be used instead. For more information, see [Reporting Services Configuration Files](../../2014/reporting-services/reporting-services-configuration-files.md).|  
|111|Error|Logging|Report Server<br /><br /> Report Manager|*\<Source>* cannot create the trace log. For more information, see [Report Server Service Trace Log](../../2014/reporting-services/report-server-service-trace-log.md).|  
|112|Warning|Security|Report Server|The report server has detected a possible denial of service attack. For more information, see [Reporting Services Security and Protection](../../2014/reporting-services/reporting-services-security-and-protection.md).|  
|113|Error|Logging|Report Server|The report server cannot create a performance counter.|  
|114|Error|Startup/Shutdown|Report Manager|Report Manager cannot connect to the Report Server service.|  
|115|Warning|Scheduling|Scheduling and Delivery Processor|A scheduled task in the SQL Server Agent queue has been modified or deleted.|  
|116|Error|Internal|Report Server<br /><br /> Report Manager<br /><br /> Scheduling and Delivery Processor|An internal error occurred.|  
|117|Error|Startup/Shutdown|Report Server|The report server database is an invalid version.|  
|118|Warning|Logging|Report Server<br /><br /> Report Manager|The trace log is not at the expected directory location; a new trace log will be created in the default directory. For more information, see [Report Server Service Trace Log](../../2014/reporting-services/report-server-service-trace-log.md).|  
|119|Error|Activation|Report Server<br /><br /> Scheduling and Delivery Processor|*\<Source>* has not been granted access to the contents of the report server database.|  
|120|Error|Activation|Report Server|The symmetric key cannot be decrypted. Most likely, the there has been a change to the account that the service runs as. For more information, see [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-and-manage-encryption-keys-ssrs-configuration-manager.md).|  
|121|Error|Startup/Shutdown|Report Server|Remote Procedure Call (RPC) Service failed to start.|  
|122|Warning|Delivery|Scheduling and Delivery Processor|Scheduling and Delivery Processor cannot connect to the SMTP server that is used for e-mail delivery. For more information about SMTP server connections, see [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md).|  
|123|Warning|Logging|Report Server<br /><br /> Report Manager|The report server failed to write to the trace log. For more information about trace logs, see [Report Server Service Trace Log](../../2014/reporting-services/report-server-service-trace-log.md).|  
|124|Information|Activation|Report Server|The Report Server service has been initialized. For more information, see [Initialize a Report Server &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/initialize-a-report-server-ssrs-configuration-manager.md).|  
|125|Information|Activation|Report Server|The key used for encrypting data was successfully extracted. For more information about keys, see [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-and-manage-encryption-keys-ssrs-configuration-manager.md).|  
|126|Information|Activation|Report Server|The key used for encrypting data was successfully applied. For more information about keys, see [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-and-manage-encryption-keys-ssrs-configuration-manager.md).|  
|127|Information|Activation|Report Server|Encrypted content was successfully removed from the report server database. For more information about deleting non-recoverable encrypted data, see [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-and-manage-encryption-keys-ssrs-configuration-manager.md).|  
|128|Error|Activation|Report Server|[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] components from different editions cannot be used together.|  
|129|Error|Management|Report Server<br /><br /> Scheduling and Delivery Processor|An encrypted configuration file setting in cannot be decrypted.|  
|130|Error|Management|Report Server<br /><br /> Scheduling and Delivery Processor|*\<Source>* cannot find the configuration file. Configuration files are required by the report server.|  
|131|Error|Security|Report Server<br /><br /> Scheduling and Delivery Processor|An encrypted user data value could not be decrypted.|  
|132|Error|Security|Report Server|A failure occurred during encryption of user data. The value cannot be saved.|  
|133|Error|Management|Report Server<br /><br /> Report Manager<br /><br /> Scheduling and Delivery Processor|A configuration file failed to load. This error may occur if the XML is not valid.|  
|134|Error|Management|Report Server|The report server failed to encrypt values for a setting in a configuration file.|  
  
## See Also  
 [Monitor Reporting Services Subscriptions](../../2014/reporting-services/monitor-reporting-services-subscriptions.md)   
 [Reporting Services Log Files and Sources](../../2014/reporting-services/reporting-services-log-files-and-sources.md)  
  
  