---
title: "Manually Process Data (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.datarefreshprogressdb.f1"
ms.assetid: 0918c04c-c1e6-45b4-acfa-96fa578e684b
author: minewiskan
ms.author: owend
manager: craigg
---
# Manually Process Data (SSAS Tabular)
  This topic describes how to manually process workspace data in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
 When you author a tabular model that uses external data, you can manually refresh the data by using the Process command. You can process a single table, all tables in the model, or one or more partitions. Whenever you process data, you may also need to recalculate data.  Processing data means getting the latest data from external sources. Recalculating means updating the result of any formula that uses the data.  
  
 Sections in this topic:  
  
-   [Manually Process Data](#bkmk_mahually_process)  
  
-   [Data Process Progress](#bkmk_data_process_progress)  
  
##  <a name="bkmk_mahually_process"></a> Manually Process Data  
  
#### To process data for a single table or all tables in a model  
  
1.  In the model designer, click the table you want to process.  
  
2.  Click on the **Model** menu, then click **Process**, and then click **Process** or **Process All**.  
  
#### To process data for all tables using the same connection  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click on the **Model** menu, and then click **Existing Connections**.  
  
2.  In the **Existing Connections** dialog box, select a connection, and then click **Process**.  
  
#### To process data for one or more partitions  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Model** menu, then point to **Process**, and then click **Process Partitions**.  
  
2.  In the **Process Partitions** dialog box, in **Mode**, select one of the following process modes:  
  
    |Mode|Description|  
    |----------|-----------------|  
    |**Process Default**|Detects the process state of a partition object, and performs processing necessary to deliver unprocessed or partially processed partition objects to a fully processed state. Data for empty tables and partitions is loaded; hierarchies, calculated columns, and relationships are built or rebuilt.|  
    |**Process Full**|Processes a partition object and all the objects that it contains. When Process Full is run for an object that has already been processed, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] drops all data in the object, and then processes the object. This kind of processing is required when a structural change has been made to an object.|  
    |**Process Data**|Load data into a partition or a table without rebuilding hierarchies or relationships or recalculating calculated columns and measures.|  
    |**Process Clear**|Removes all data from a partition.|  
    |**Process Add**|Incrementally update partition with new data.|  
  
3.  In the partitions list, select the partitions you want to process, and then click **OK**.  
  
##  <a name="bkmk_data_process_progress"></a> Data Process Progress  
 The **Data Process Progress** dialog box enables you to monitor the processing of data that you have imported into the model from an external source. To access this dialog box, click on the **Model** menu, and then click **Process Partitions**, **Process Table** or **Process All**.  
  
 **Status**  
 Indicates whether the process operation was successful or not.  
  
 **Details**  
 Lists the tables and views that were imported, the number of rows that were imported, and provides a link to a report of any issues.  
  
 **Stop Refresh**  
 Click to halt the process operation. This option is useful if the operation is taking too long, or if there are too many errors.  
  
## See Also  
 [Process Data &#40;SSAS Tabular&#41;](process-data-ssas-tabular.md)   
 [Troubleshoot Process Data &#40;SSAS Tabular&#41;](troubleshoot-process-data-ssas-tabular.md)  
  
  
