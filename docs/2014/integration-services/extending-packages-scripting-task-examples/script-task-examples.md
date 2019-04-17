---
title: "Script Task Examples | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Script task [Integration Services], examples"
  - "examples [Integration Services]"
  - "SSIS Script task, examples"
ms.assetid: b0dd77ee-ee11-4cd9-87aa-61dd67f2fe1c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Script Task Examples
  The Script task is a multi-purpose tool that you can use in a package to fill almost any requirement that is not met by the tasks included with [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. This topic lists Script task code samples that demonstrate some of the available functionality.  
  
> [!NOTE]  
>  If you want to create tasks that you can more easily reuse across multiple packages, consider using the code in these Script task samples as the starting point for custom tasks. For more information, see [Developing a Custom Task](../extending-packages-custom-objects/task/developing-a-custom-task.md).  
  
## In This Section  
  
### Example Topics  
 This section contains code examples that demonstrate various uses of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] classes that you can incorporate into an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Script task:  
  
 [Detecting an Empty Flat File with the Script Task](../extending-packages-scripting-task-examples/detecting-an-empty-flat-file-with-the-script-task.md)  
 Checks a flat file to determine whether it contains rows of data, and saves the result to a variable for use in control flow branching.  
  
 [Gathering a List for the ForEach Loop with the Script Task](../extending-packages-scripting-task-examples/gathering-a-list-for-the-foreach-loop-with-the-script-task.md)  
 Gathers a list of files that meet user-specified criteria, and populates a variable for later use by the Foreach from Variable Enumerator.  
  
 [Querying the Active Directory with the Script Task](../extending-packages-scripting-task-examples/querying-the-active-directory-with-the-script-task.md)  
 Retrieves user information from Active Directory based on the value of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] variable, by using classes in the System.DirectoryServices namespace.  
  
 [Monitoring Performance Counters with the Script Task](../extending-packages-scripting-task-examples/monitoring-performance-counters-with-the-script-task.md)  
 Creates a custom performance counter that can be used to track the execution progress of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package, by using classes in the System.Diagnostics namespace.  
  
 [Working with Images with the Script Task](../extending-packages-scripting-task-examples/working-with-images-with-the-script-task.md)  
 Compresses images into the JPEG format and creates thumbnail images from them, by using classes in the System.Drawing namespace.  
  
 [Finding Installed Printers with the Script Task](../extending-packages-scripting-task-examples/finding-installed-printers-with-the-script-task.md)  
 Locates installed printers that support a specific paper size, by using classes in the System.Drawing.Printing namespace.  
  
 [Sending an HTML Mail Message with the Script Task](../extending-packages-scripting-task-examples/sending-an-html-mail-message-with-the-script-task.md)  
 Sends a mail message in HTML format instead of plain text format.  
  
 [Working with Excel Files with the Script Task](../extending-packages-scripting-task-examples/working-with-excel-files-with-the-script-task.md)  
 Lists the worksheets in an Excel file and checks for the existence of a specific worksheet.  
  
 [Sending to a Remote Private Message Queue with the Script Task](../extending-packages-scripting-task-examples/sending-to-a-remote-private-message-queue-with-the-script-task.md)  
 Sends a message to a remote private message queue.  
  
### Other Examples  
 The following topics also contain code examples for use with the Script task:  
  
 [Using Variables in the Script Task](../extending-packages-scripting/task/using-variables-in-the-script-task.md)  
 Asks the user for confirmation of whether the package should continue to run, based on the value of a package variable that may exceed the limit specified in another variable.  
  
 [Connecting to Data Sources in the Script Task](../extending-packages-scripting/task/connecting-to-data-sources-in-the-script-task.md)  
 Retrieves a connection or connection information from connection managers defined in the package.  
  
 [Raising Events in the Script Task](../extending-packages-scripting/task/raising-events-in-the-script-task.md)  
 Raises an error, a warning, or an informational message based on the status of the Internet connection on the server.  
  
 [Logging in the Script Task](../extending-packages-scripting/task/logging-in-the-script-task.md)  
 Logs the number of items processed by the task to enabled log providers.  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
  
