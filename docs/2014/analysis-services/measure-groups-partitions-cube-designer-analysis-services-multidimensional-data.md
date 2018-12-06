---
title: "Measure Groups (Partitions Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.partitions.partitionspane.measuregroupdetail.f1"
ms.assetid: 58e44b24-cfcd-4908-b445-d4374b961b98
author: minewiskan
ms.author: owend
manager: craigg
---
# Measure Groups (Partitions Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **Measure Groups** pane on the **Partitions** tab in Cube Designer to manage the partitions associated with each measure group in the cube.  
  
## Options  
 **Partitions**  
 Displays a grid containing the list of partitions that support the selected measure group. The grid contains the following columns:  
  
 **(Ordinal)**  
 Displays the ordinal position of the partition within the measure group.  
  
 Click to select the entire row for the partition.  
  
 **Partition Name**  
 Type the name of the selected partition.  
  
 **Source**  
 Type the table name (for table binding) or query (for query binding) that provides the fact table data for the selected partition.  
  
 Click the **...** button to display the **Partition Source** dialog box and define the source for the selected partition.  
  
 **Aggregation**  
 Displays the aggregation mode and the storage mode of the partition. The storage mode is displayed first: relational online analytical processing (ROLAP), multidimensional online analytical processing (MOLAP), or hybrid online analytical processing (HOLAP). The aggregation mode is displayed as a percentage of optimization requested, as a measure of space requested or used, or as the number of aggregations created. Click the **...** button to display the **Aggregation Design Wizard** and define the aggregation design for the specified partition.  
  
 **Description**  
 Type the optional description of the partition.  
  
 **New Partition...**  
 Click to display the **Partition Wizard** and create a new partition in the selected measure group.  
  
 **Storage settings...**  
 Click to display the **Storage Settings** dialog box and specify storage mode, proactive caching, and notification settings for the selected partition.  
  
> [!NOTE]  
>  This option is enabled only if any cell of a partition is selected in the **Partitions** grid of the selected measure group.  
  
 **Writeback settings...**  
 Click to display the **Enable/Disable Writeback** dialog box and specify writeback settings for the selected measure group.  
  
## Context Menu  
 The following options are available in the context menu displayed by right-clicking a row in the **Partitions** grid of a selected measure group:  
  
|Option|Definition|  
|------------|----------------|  
|**Add Business Intelligence**|Click to display the **Business Intelligence Wizard** and add business intelligence features to the cube. For more information about the **Business Intelligence Wizard**, see [Business Intelligence Wizard F1 Help](business-intelligence-wizard-f1-help.md).|  
|**New Partition**|Click to display the **Partition Wizard** and create a new partition in the selected measure group.|  
|**Rename Partition**|Select to rename the selected partition.|  
|**Delete**|Click to display the **Delete Objects** dialog box and delete the selected action.<br /><br /> Note: This option is disabled if a writeback partition is selected.|  
|**Design Aggregations**|Click to display the **Aggregation Design Wizard** and create an aggregation design for the selected partition.<br /><br /> Note: This option is disabled if a writeback partition is selected.|  
|**Storage Settings**|Click to display the **Storage Settings** dialog box and specify storage mode, proactive caching, and notification settings for the selected partition.|  
|**Writeback Settings**|Click to display the **Enable/Disable Writeback** dialog box and specify writeback settings for the measure group containing the selected partition.|  
|**Usage Based Optimization**|Click to display the **Usage-Based Optimization Wizard** and create an aggregation design based on existing usage patterns for the selected partition.<br /><br /> Note: This option is disabled if a writeback partition is selected.|  
|**Process**|Click to display the **Process** dialog box and process the selected partition.|  
|**Copy**|This option is disabled.|  
|**Paste**|This option is disabled.|  
|**Properties**|Select to display the **Properties** window in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] for the selected partition.|  
  
  
