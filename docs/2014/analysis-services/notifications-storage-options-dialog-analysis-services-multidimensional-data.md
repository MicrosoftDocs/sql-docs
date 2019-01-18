---
title: "Notifications (Storage Options Dialog Box) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.partitiondesigner.partitionstoragesettings.setstorageoptions.notifications.f1"
ms.assetid: 5675cdbf-bfaa-4b6e-b716-31b8e9da72b4
author: minewiskan
ms.author: owend
manager: craigg
---
# Notifications (Storage Options Dialog Box) (Analysis Services - Multidimensional Data)
  Use the **Notifications** tab of the **Storage Options** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to set the notification method and related settings for a dimension, cube, measure group, or partition.  
  
> [!NOTE]  
>  You should be familiar with storage mode and proactive caching functionality before modifying these settings. For more information, see [Proactive Caching &#40;Partitions&#41;](multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Storage mode**|Selects the storage mode to use for the object.<br /><br /> **MOLAP**<br /> The object uses multidimensional OLAP (MOLAP) storage.<br /><br /> **HOLAP**<br /> The object uses hybrid OLAP (HOLAP) storage.<br /><br /> **ROLAP**<br /> The object uses relational OLAP (ROLAP) storage.|  
|**Enable proactive caching**|Enables proactive caching.<br /><br /> Note: If this option is not selected, all options except **Storage mode** are disabled.|  
|**SQL Server**|Uses a specialized trace mechanism on [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to identify changes to underlying tables for the object.|  
|**Specify tracking tables**|Specify the underlying tables to be tracked for the object, then type a list of tables delimited by semi-colon (;) characters or click the ellipsis button (**...**) to open the **Relational Objects** dialog box and choose the tables to be tracked. For more information, see [Relational Objects Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](relational-objects-dialog-box-analysis-services-multidimensional-data.md).<br /><br /> If this option is not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] tries to determine the list of underlying tables to be tracked for the object, if certain requirements are met. For more information about these requirements, see [Proactive Caching &#40;Partitions&#41;](multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).|  
|**Client initiated**|Select to use the XML for Analysis (XMLA) command, `NotifyTableChange`, to identify changes to underlying tables for the object. This option is typically selected if you plan to use a client-based notification process.|  
|**Specify tracking tables**|Select to specify the underlying tables to be tracked for the object, then type a list of tables delimited by semi-colon (;) characters or click the ellipsis button (**...**) to open the **Relational Objects** dialog box and choose the tables to be tracked. For more information, see [Relational Objects Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](relational-objects-dialog-box-analysis-services-multidimensional-data.md).<br /><br /> If this option is not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] tries to determine the list of underlying tables to be tracked for the object, if certain requirements are met. For more information about these requirements, see [Proactive Caching &#40;Partitions&#41;](multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).|  
|**Scheduled polling**|Uses a polling mechanism to identify changes by running a series of queries on the underlying tables for the object.|  
|**Polling interval**|Specifies the interval and units of time for the period that should pass before [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] executes the polling queries and processing queries defined in the polling grid.|  
|**Enable incremental updates**|Incrementally updates the MOLAP cache for an object based on a set of polling and processing queries designed to identify only additional data. If this option is selected, the polling query is associated with a table identifier in the data source view. The processing query is then used to compare the current value of the polling query with the stored value of the previously executed polling query to identify changes.<br /><br /> If this option is not selected, the MOLAP cache is fully updated. The polling query is used to identify that a change has occurred, and no processing query or table identifier is required.|  
|**Polling grid**|Contains the polling queries, processing queries, and table identifiers used by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to poll the data source and identify changes to underlying tables for the object. The grid contains the following columns:<br /><br /> **Polling query**: Type the singleton query executed at the polling interval to identify changes for the object, or click the ellipsis button (**...**) to open the **Create Polling Query** dialog box and define the singleton query. For more information, see [Create Polling Query Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](create-polling-query-dialog-box-analysis-services-multidimensional-data.md). If **Enable incremental updates** is selected, the polling query should return a value that identifies the last record added to the table identified in **Table**. If **Enable incremental updates** is not selected, the polling query should return a value that identifies the current number of records in the table.<br /><br /> **Processing query**: Type the query executed at the polling interval to retrieve new records from the table identified in **Table**, or click the ellipsis button (**...**) to open the **Create Processing Query** dialog box and define the query. For more information, see [Create Processing Query Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](create-processing-query-dialog-box-analysis-services-multidimensional-data.md). The query should be parameterized to accept two parameters-the previous value returned by the query in **Polling query** and the current value returned by the query in **Polling query**-that can be used to identify and extract only the records that have been added during the polling period. Note that this option is enabled only if **Enable incremental updates** is selected.<br /><br /> **Table**: Type the identifier of the table against which the query in **Polling query** tracks the last record, or click the ellipsis button (**...**) to open the **Find Table** dialog box and select the table. For more information, see [Find Table Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](find-table-dialog-box-analysis-services-multidimensional-data.md).|  
  
## See Also  
 [Storage Options Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](storage-options-dialog-box-analysis-services-multidimensional-data.md)  
  
  
