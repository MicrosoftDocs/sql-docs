---
title: "Events Logged by the Integration Services Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "service [Integration Services], events"
  - "events [Integration Services], service"
  - "Integration Services service, events"
ms.assetid: d4122dcf-f16f-47a0-93a2-ffa3d0d4f9cf
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Events Logged by the Integration Services Service
  The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service logs various messages to the Windows Application event log. The service logs these messages when the service starts, when the service stops, and when certain problems occur.  
  
 This topic provides information about the common event messages that the service logs to the Application event log. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service logs all the messages described in this topic with an Event Source of SQLISService.  
  
 For general information about the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service, see [Integration Services Service &#40;SSIS Service&#41;](integration-services-service-ssis-service.md).  
  
## Messages about the Status of the Service  
 When you select [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] for installation, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is installed and started, and its Startup Type is set to Automatic.  
  
|Event ID|Symbolic Name|Text|Notes|  
|--------------|-------------------|----------|-----------|  
|256|DTS_MSG_SERVER_STARTING|Starting [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service.|The service is about to start.|  
|257|DTS_MSG_SERVER_STARTED|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service started.|The service started.|  
|260|DTS_MSG_SERVER_START_FAILED|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service failed to start.%nError: %1|The service was not able to start. This inability to start might be the result of a damaged installation or an inappropriate service account.|  
|258|DTS_MSG_SERVER_STOPPING|Stopping [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service.%n%nStop all running packages on exit: %1|The service is stopping, and if you configure the service to do this, will stop all running packages. You can set a true or false value in the configuration file that determines whether the service stops running packages when the service itself stops. The message for this event includes the value of this setting.|  
|259|DTS_MSG_SERVER_STOPPED|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service stopped.%nServer version %1|The service stopped.|  
  
## Messages about the Configuration File  
 Settings for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service are stored in an XML file that you can modify. For more information, see [Configuring the Integration Services Service &#40;SSIS Service&#41;](../configuring-the-integration-services-service-ssis-service.md).  
  
|Event ID|Symbolic Name|Text|Notes|  
|--------------|-------------------|----------|-----------|  
|274|DTS_MSG_SERVER_MISSING_CONFIG_REG|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service: %nRegistry setting specifying configuration file does not exist. %nAttempting to load default config file.|The Registry entry that contains the path of the configuration file does not exist or is empty.|  
|272|DTS_MSG_SERVER_MISSING_CONFIG|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service configuration file does not exist.%nLoading with default settings.|The configuration file itself does not exist at the specified location.|  
|273|DTS_MSG_SERVER_BAD_CONFIG|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service configuration file is incorrect.%nError reading config file: %1%n%nLoading server with default settings.|The configuration file could not be read or is not valid. This error might be the result of an XML syntax error in the file.|  
  
## Other Messages  
  
|Event ID|Symbolic Name|Text|Notes|  
|--------------|-------------------|----------|-----------|  
|336|DTS_MSG_SERVER_STOPPING_PACKAGE|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssIS](../../includes/ssis-md.md)] Service: stopping running package.%nPackage instance ID: %1%nPackage ID: %2%nPackage name: %3%nPackage description: %4%nPackage|The service is trying to stop a running package. You can monitor and stop running packages in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For information about how to manage packages in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], see [Package Management &#40;SSIS Service&#41;](package-management-ssis-service.md).|  
  
## Related Tasks  
 For information about how to view log entries, see [View Log Entries in the Log Events Window](../view-log-entries-in-the-log-events-window.md)  
  
## See Also  
 [Events Logged by an Integration Services Package](../performance/events-logged-by-an-integration-services-package.md)  
  
  
