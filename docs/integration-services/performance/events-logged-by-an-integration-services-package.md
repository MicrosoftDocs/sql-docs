---
title: "Events Logged by an Integration Services Package | Microsoft Docs"
ms.custom: supportability
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "package [Integration Services], events"
  - "events [Integration Services], package"
ms.assetid: 55a0951a-46f3-4f0f-9972-74cec9cc26b7
author: janinezhang
ms.author: janinez
manager: craigg
---
# Events Logged by an Integration Services Package

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package logs various event messages to the Windows Application event log. A package logs these messages when the package starts, when the package stops, and when certain problems occur.  
  
 This topic provides information about the common event messages that a package logs to the Application event log. By default, a package logs some of these messages even if you have not enabled logging on the package. However, there are other messages that the package will log only if you have enabled logging on the package. Regardless of whether the package logs these messages by default or because logging has been enabled, the Event Source for the messages is SQLISPackage.  
  
 For general information about how to run SSIS packages, see [Execution of Projects and Packages](../packages/run-integration-services-ssis-packages.md).  
  
 For information about how to troubleshoot running packages, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Messages about the Status of the Package  
 When you run an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package, the package typically logs various messages about the progress and status of the package. The following table lists those messages.  
  
> [!NOTE]  
>  The package will log the messages in the following table even if you have not enabled logging for the package.  
  
|Event ID|Symbolic Name|Text|Notes|  
|--------------|-------------------|----------|-----------|  
|12288|DTS_MSG_PACKAGESTART|Package "" started.|The package has started to run.|  
|12289|DTS_MSG_PACKAGESUCCESS|Package "" finished successfully.|The package successfully ran and is no longer running.|  
|12290|DTS_MSG_PACKAGECANCEL|Package "%1!s!" has been cancelled.|The package is no longer running because the package was canceled.|  
|12291|DTS_MSG_PACKAGEFAILURE|Package "" failed.|The package could not run successfully and stopped running.|  
  
 By default, in a new installation, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is configured not to log certain events that are related to the running of packages to the Application event log. This setting prevents too many event log entries when you use the Data Collector feature of the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. The events that are not logged are EventID 12288, "Package started," and EventID 12289, "Package finished successfully." To log these events to the Application event log, open the registry for editing. Then in the registry, locate the HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\SSIS node, and change the DWORD value of the LogPackageExecutionToEventLog setting from 0 to 1. However, in an upgrade insallation, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is configured to log these two events. To disable logging, change the value of the LogPackageExecutionToEventLog setting from 1 to 0.  
  
## Messages Associated with Package Logging  
 If you have enabled logging on the package, the Application event log is one of the destinations that is supported by the optional logging features in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
 When you have enabled logging on the package and the log location is the Application event log, the package logs entries that pertain to the following information:  
  
-   Messages about which stage the package is in while the package runs.  
  
-   Messages about particular events that occur while the package runs.  
  
### Messages about the Stages of Package Execution  
  
|Event ID|Symbolic Name|Text|Notes|  
|--------------|-------------------|----------|-----------|  
|12544|DTS_MSG_EVENTLOGENTRY|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|When you configure package logging to the Application event log, various messages use this generic format.|  
|12556|DTS_MSG_EVENTLOGENTRY_PACKAGESTART|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|The package started.|  
|12547|DTS_MSG_EVENTLOGENTRY_PREVALIDATE|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|Validation of the object is about to begin.|  
|12548|DTS_MSG_EVENTLOGENTRY_POSTVALIDATE|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|Validation of the object has finished.|  
|12552|DTS_MSG_EVENTLOGENTRY_PROGRESS|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|This generic message reports package progress.|  
|12546|DTS_MSG_EVENTLOGENTRY_POSTEXECUTE|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|The object has finished its work.|  
|12557|DTS_MSG_EVENTLOGENTRY_PACKAGEEND|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|The package has finished running.|  
  
### Messages about Events that Occur  
 The following table lists only some of the messages that are the result of events. For a more comprehensive list of error, warning, and informational messages that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses, see [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md).  
  
|Event ID|Symbolic Name|Text|Notes|  
|--------------|-------------------|----------|-----------|  
|12251|DTS_MSG_EVENTLOGENTRY_TASKFAILED|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|The task failed.|  
|12250|DTS_MSG_EVENTLOGENTRY_ERROR|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|This message reports an error that has occurred.|  
|12249|DTS_MSG_EVENTLOGENTRY_WARNING|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|This message reports a warning that has occurred.|  
|12258|DTS_MSG_EVENTLOGENTRY_INFORMATION|Event Name: %1%r Message: %9%r Operator: %2%r Source Name: %3%r Source ID: %4%r Execution ID: %5%r Start Time: %6%r End Time: %7%r Data Code: %8|This message reports informational that is not associated with an error or a warning.|  

## View Log Entries in the Log Events Window
  This procedure describes how to run a package and view the log entries it writes. You can view the log entries in real time. The log entries that are written to the **Log Events** window can also be copied and saved for further analysis.  
  
 It is not necessary to write the log entries to a log to write the entries to the **Log Events** window.  
  
### To view log entries  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  On the **SSIS** menu, click **Log Events**. You can optionally display the **Log Events** window by mapping the View.LogEvents command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
3.  On the **Debug** menu, click **Start Debugging**.  
  
     As the runtime encounters the events and custom messages that are enabled for logging, log entries for each event or message are written to the **Log Events** window.  
  
4.  On the **Debug** menu, click **Stop Debugging**.  
  
     The log entries remain available in the **Log Events** window until you rerun the package, run a different package, or close [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
5.  View the log entries in the **Log Events** window.  
  
6.  Optionally, click the log entries to copy, right-click, and then click **Copy**.  
  
7.  Optionally, double-click a log entry, and in the **Log Entry** dialog box, view the details for a single log entry.  
  
8.  In the **Log Entry** dialog box, click the up and down arrows to display the previous or next log entry, and click the copy icon to copy the log entry.  
  
9. Open a text editor, paste, and then save the log entry to a text file.
