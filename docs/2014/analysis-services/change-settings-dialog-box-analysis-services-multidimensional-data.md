---
title: "Change Settings Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.process.batchsettingsdialog.f1"
ms.assetid: 0041e042-d7ce-48f9-a690-a6dc65471ff3
author: minewiskan
ms.author: owend
manager: craigg
---
# Change Settings Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Change Settings** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to change the settings that govern the processing of objects listed in the **Process** dialog box. You can display the **Change Settings** dialog box by clicking **Change Settings** on the **Process** dialog box.  
  
> [!NOTE]  
>  Settings specified in this dialog box override default settings inherited from the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database for the objects listed in the **Process** dialog box.  
  
## Options  
 **Processing options**  
 Use this tab to modify settings related to the processing order, writeback table, and affected objects for the processing operation. The tab contains the following options:  
  
 **Parallel**  
 Click to process the objects in parallel.  
  
 **Maximum parallel tasks**  
 Select the maximum number of tasks to execute in parallel by the processing operation, or choose **Let the server decide** to allow [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to select an optimal number of parallel tasks.  
  
 **Sequential**  
 Click to process the objects sequentially.  
  
 **Transaction mode**  
 Choose the transaction mode that is used when objects are processed in sequential order:  
  
-   **One Transaction** processes all objects in the same transaction.  
  
-   **Separate Transactions** processes all objects, including dependent objects, in separate transactions.  
  
> [!NOTE]  
>  This option is enabled only if **Sequential** is selected.  
  
 **Writeback table option**  
 Choose the option used to manage a writeback table:  
  
-   **Create** creates a writeback table if it does not exist. An error occurs if a writeback table already exists.  
  
-   **Create always** creates a writeback table if it does not exist, or overwrites the existing writeback table if it does exist.  
  
-   **Use existing** uses the existing writeback table if it exists. An error occurs if a writeback table does not exist.  
  
 **Process affected objects**  
 Select to include and process objects that depend on objects included in the processing operation.  
  
 **Dimension key errors**  
 Use this tab to modify settings related to the error configuration for the processing operation. The tab contains the following options:  
  
 **Use default error configuration**  
 Select to use the default error configuration for objects in the processing operation.  
  
 **Use custom error configuration**  
 Select to define the error configuration for objects in the processing operation.  
  
 **Key error action**  
 Choose one of the following actions that occur when a new key is encountered during processing that cannot be looked up:  
  
-   **Convert to unknown** aggregates the information for the record into the unknown member.  
  
-   **Discard record** excludes the information for the record from being processed with the object.  
  
 **Ignore errors count**  
 Click to ignore any errors that occur when processing.  
  
 **Stop on error**  
 Click to stop processing when errors occur. This option enables the **Number of errors** and the **On error action** options.  
  
 **Number of errors**  
 Type the number of errors that are ignored before processing stops.  
  
 **On error action**  
 Choose one of the following actions to be taken when the number of errors exceeds the value in **Number of errors**:  
  
-   **Stop processing** ends the processing operation.  
  
-   **Stop logging** stops logging errors, but continues the processing operation.  
  
 **Key not found**  
 Specify one of the following actions to be taken when a key is not found when an object is processed:  
  
-   **Ignore error** ignores the error.  
  
-   **Report and continue** reports the error and continues the processing operation.  
  
-   **Report and stop** reports the error and stops the processing operation.  
  
 **Duplicate key**  
 Specify one of the following actions to be taken if a duplicate key is found when an object is processed:  
  
-   **Ignore error** ignores the error.  
  
-   **Report and continue** reports the error and continues the processing operation.  
  
-   **Report and stop** reports the error and stops the processing operation.  
  
 **Null key converted to unknown**  
 Specify one of the following actions to be taken when a null member key is added to the unknown member when an object is processed:  
  
-   **Ignore error** ignores the error.  
  
-   **Report and continue** reports the error and continues the processing operation.  
  
-   **Report and stop** reports the error and stops the processing operation.  
  
 **Null key not allowed**  
 Specify one of the following actions to be taken when a null key is found, but not allowed, when an object is processed:  
  
-   **Ignore error** ignores the error.  
  
-   **Report and continue** reports the error and continues the processing operation.  
  
-   **Report and stop** reports the error and stops the processing operation.  
  
 **Error log path**  
 Type the full path and file name for the error log file.  
  
 **Browse**  
 Click to open the **Open** dialog box and select the full path and file name for the error log file.  
  
 **Process affected objects**  
 Click to process the objects that have a dependency on the objects selected in the **Process** dialog box.  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Process Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](process-dialog-box-analysis-services-multidimensional-data.md)  
  
  
