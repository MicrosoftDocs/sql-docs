---
title: "View Log Entries in the Log Events Window | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [Integration Services], viewing"
  - "Integration Services packages, logs"
  - "packages [Integration Services], logs"
ms.assetid: c02123c3-67fc-4370-ad14-91ed259f1873
author: janinezhang
ms.author: janinez
manager: craigg
---
# View Log Entries in the Log Events Window
  This procedure describes how to run a package and view the log entries it writes. You can view the log entries in real time. The log entries that are written to the **Log Events** window can also be copied and saved for further analysis.  
  
 It is not necessary to write the log entries to a log to write the entries to the **Log Events** window.  
  
### To view log entries  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  On the **SSIS** menu, click **Log Events**. You can optionally display the **Log Events** window by mapping the View.LogEvents command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
3.  On the **Debug** menu, click **Start Debugging**.  
  
     As the runtime encounters the events and custom messages that are enabled for logging, log entries for each event or message are written to the **Log Events** window.  
  
4.  On the **Debug** menu, click **Stop Debugging**.  
  
     The log entries remain available in the **Log Events** window until you rerun the package, run a different package, or close [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)].  
  
5.  View the log entries in the **Log Events** window.  
  
6.  Optionally, click the log entries to copy, right-click, and then click **Copy**.  
  
7.  Optionally, double-click a log entry, and in the **Log Entry** dialog box, view the details for a single log entry.  
  
8.  In the **Log Entry** dialog box, click the up and down arrows to display the previous or next log entry, and click the copy icon to copy the log entry.  
  
9. Open a text editor, paste, and then save the log entry to a text file.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Logging](performance/integration-services-ssis-logging.md)  
  
  
