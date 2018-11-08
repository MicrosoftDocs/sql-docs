---
title: "Batch Processing (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "batches [Analysis Services]"
ms.assetid: ba4dcf72-0667-41d0-816b-ab8ff9a7d9cb
author: minewiskan
ms.author: owend
manager: craigg
---
# Batch Processing (Analysis Services)
  In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can use the Batch command to send multiple processing commands to the server in a single request. Batch processing gives you a way to control which objects are to be processed, and in what order. Also, a batch can run as a series of stand-alone jobs, or as a transaction in which the failure of one process causes a rollback of the complete batch.  
  
 Batch processing maximizes data availability by consolidating and reducing the amount of time taken to commit changes. When you fully process a dimension, any partition using that dimension is marked as unprocessed. As a result, cubes that contain the unprocessed partitions are unavailable for browsing. You can address this with a batch processing job by processing the dimensions together with the affected partitions. Running the batch processing job as a transaction makes sure that all objects included in the transaction remain available for queries until all processing is completed. As the transaction commits the changes, locks are put on the affected objects, making the objects temporarily unavailable, but overall the amount of time used to commit the changes is less than if you processed objects individually.  
  
 The procedures in this topic show the steps for fully processing dimensions and partitions. Batch processing can also include other processing options, such as incremental processing. For these procedures to work correctly, you should use an existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that contains at least two dimensions and one partition.  
  
 This topic includes the following sections:  
  
 [Batch Processing in SQL Server Data Tools](#bkmk_ssdt)  
  
 [Batch Processing using XMLA in Management Studio](#bkmk_xmla)  
  
##  <a name="bkmk_ssdt"></a> Batch Processing in SQL Server Data Tools  
 Before objects can be processed in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], the project that contains the objects must be deployed. For more information, see [Deploy Analysis Services Projects &#40;SSDT&#41;](deploy-analysis-services-projects-ssdt.md).  
  
1.  Open [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
2.  Open a project that has been deployed.  
  
3.  In Solution Explorer, under the deployed project, expand the **Dimensions** folder.  
  
4.  Holding the Ctrl key, click each dimension listed in the **Dimensions** folder.  
  
5.  Right-click the selected dimensions, and then click **Process**.  
  
6.  Holding the Ctrl key, click each dimension listed in the **Object list**.  
  
7.  Right-click the selected dimensions and select **Process Full**.  
  
8.  To customize the batch process job, click **Change Settings.**  
  
9. Under **Processing options**, mark the following settings:  
  
    -   **Processing Order** is set to **Sequential**, and **Transaction mode** is set to **One Transaction**.  
  
    -   **Writeback Table Option** is set to **Use existing**.  
  
    -   Under **Affected Objects**, select the **Process affected objects** check box.  
  
10. Click the **Dimension key errors** tab. Verify that **Use default error configuration** is selected.  
  
11. Click **OK** to close the **Change Settings** screen.  
  
12. Click **Run** in the **Process Objects** screen to start the processing job.  
  
13. When the **Status** box shows **Process succeeded**, click **Close**.  
  
14. Click **Close** on the **Process Objects** screen.  
  
##  <a name="bkmk_xmla"></a> Batch Processing using XMLA in Management Studio  
 You can create an XMLA script that performs batch processing. Start by generating an XMLA script in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] for each object, and then combine them into a single XMLA Query that you run interactively or inside a scheduled task.  
  
 For step by step instructions, see **Example 2** in [Schedule SSAS Administrative Tasks with SQL Server Agent](../instances/schedule-ssas-administrative-tasks-with-sql-server-agent.md)  
  
## See Also  
 [Multidimensional Model Object Processing](processing-a-multidimensional-model-analysis-services.md)  
  
  
