---
title: "Merge Partitions in Analysis Services (SSAS - Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "partitions [Analysis Services], merging"
  - "merging partitions [Analysis Services]"
ms.assetid: b3857b9b-de43-4911-989d-d14da0196f89
author: minewiskan
ms.author: owend
manager: craigg
---
# Merge Partitions in Analysis Services (SSAS - Multidimensional)
  You can merge partitions in an existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database to consolidate fact data from multiple partitions of the same measure group.  
  
 [Common scenarios](#bkmk_Scenario)  
  
 [Requirements](#bkmk_prereq)  
  
 [Update the Partition Source after merging partitions](#bkmk_Where)  
  
 [Special considerations for partitions segmented by fact table or named query](#bkmk_fact)  
  
 [How to merge partitions using SSMS](#bkmk_partitionSSMS)  
  
 [How to merge partitions using XMLA](#bkmk_partitionsXMLA)  
  
##  <a name="bkmk_Scenario"></a> Common scenarios  
 The single most common configuration for partition use involves the separation of data across the dimension of time. The granularity of time associated with each partition varies depending on the business requirements specific to the project. For example, segmentation might be by years, with the most recent year divided by months, plus a separate partition for the active month. The active month partition regularly takes on new data.  
  
 When the active month is completed, that partition is merged back into the months in the year-to-date partition and the process continues. At the end of the year, a whole new year partition has been formed.  
  
 As this scenario demonstrates, merging partitions can become a routine task performed on a regular basis, providing a progressive approach for consolidating and organizing historical data.  
  
##  <a name="bkmk_prereq"></a> Requirements  
 Partitions can be merged only if they meet all of the following criteria:  
  
-   They have the same measure group.  
  
-   They have the same structure.  
  
-   They must be in a processed state.  
  
-   They have the same storage modes.  
  
-   They contain identical aggregation designs.  
  
-   They share the same string store compatibility level (applies to partitioned distinct count measure groups only).  
  
 If the target partition is empty (that is, it has an aggregation design but no aggregations), merge will drop the aggregations for the source partitions. You must run Process Index, Process Full, or Process Default on the partition to build the aggregations.  
  
 Remote partitions can be merged only with other remote partitions that are defined with the same remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
> [!NOTE]  
>  If you are using a combination of local and remote partitions, an alternative approach is to create new partitions that include the combined data, deleting the partitions you no longer use.  
  
 To create a partition that is a candidate for future merging, when you create the partition in the Partition Wizard, you can choose to copy the aggregation design from another of the cube's partitions. This ensures that these partitions have the same aggregation design. When they are merged, the aggregations of the source partition are combined with the aggregations in the target partition.  
  
##  <a name="bkmk_Where"></a> Update the Partition Source after merging partitions  
 Partitions are segmented by query, such as the WHERE clause of a SQL query used to process the data, or by a table or named query that provides data to the partition. The `Source` property on the partition indicates whether the partition is bound to a query or a table.  
  
 When you merge partitions, the contents of the partitions are consolidated, but the `Source` property is not updated to reflect the additional scope of the partition. This means if you subsequently reprocess a partition that retains its original `Source`, you will get incorrect data from that partition. The partition will erroneously aggregate data at the parent level. The following example illustrates this behavior.  
  
 **The Problem**  
  
 Suppose you have a cube containing information about three soft drink products. It has three partitions that use the same fact table. These partitions are segmented by product. Partition 1 contains data about [ColaFull], Partition 2 contains data about [ColaDecaf], and Partition 3 contains data about [ColaDiet]. If Partition 3 is merged into Partition 2, the data in the resulting partition (Partition 2) is correct and the cube data is accurate. However, when Partition 2 is processed, its content may be determined by the parent of the members at the product level. This parent, [SoftDrinks], also includes [ColaFull], the product in Partition 1. Processing Partition 2 loads the partition with data for all soft drinks, including [ColaFull]. The cube then contains duplicate data for [ColaFull] and returns incorrect data to end users.  
  
 **The Solution**  
  
 The solution is to update the `Source` property, adjusting either the WHERE clause or named query, or manually merging data from the underlying fact tables, to ensure that subsequent processing is accurate given the expanded scope of the partition.  
  
 In this example, after merging Partition 3 into Partition 2, you can provide a filter such as ("Product" = 'ColaDecaf' OR "Product" = 'ColaDiet') in the resulting Partition 2 to specify that only data about [ColaDecaf] and [ColaDiet] be extracted from the fact table, and the data pertaining to [ColaFull] be excluded. Alternatively, you can specify filters for Partition 2 and Partition 3 when they are created, and these filters will be combined during the merger process. In either case, after the partition is processed the cube does not contain duplicate data.  
  
 **The Conclusion**  
  
 After you merge partitions, always check the `Source` to verify the filter is correct for the merged data. If you started with a partition that included historical data for Q1, Q2, and Q3, and you now merge Q4, you must adjust the filter to include Q4. Otherwise, subsequent processing of the partition will yield erroneous results. It will not be correct for Q4.  
  
##  <a name="bkmk_fact"></a> Special considerations for partitions segmented by fact table or named query  
 In addition to queries, partitions can also be segmented by table or named query. If the source partition and target partition use the same fact table in a data source or data source view, the `Source` property is valid after merging partitions. It specifies the fact table data that is appropriate to the resulting partition. Because the facts that are required for the resulting partition are present in the fact table, no modification to the `Source` property is necessary.  
  
 Partitions using data from multiple fact tables or named queries require additional work. You must manually merge the facts from the fact table of the source partition into the fact table of the target partition.  
  
 Alternatively, you can change the source for the merged partition to a named query that returns the contents of two separate fact tables. If this manual step is not performed, the fact table does not contain complete information.  
  
 For the same reason, partitions obtaining segmented data from named queries also require updating. The combined partition must now have a named query that returns the combined result set that was previously obtained from the separate named queries.  
  
## Partition storage considerations: MOLAP  
 When MOLAP partitions are merged, the facts stored in the multidimensional structures of the partitions are also merged. This results in an internally complete and consistent partition. However, the facts stored in MOLAP partitions are copies of facts in the fact table. When the partition is subsequently processed, the facts in the multidimensional structure are deleted (only for full and refresh) and data is copied from the fact table as specified by the data source and filter for the partition. If the source partition uses a different fact table from the target partition, the fact table of the source partition must be manually merged with the fact table of the target partition to ensure that a complete set of data is available when the resulting partition is processed. This applies as well if the two partitions were based on different named queries.  
  
> [!IMPORTANT]  
>  A merged MOLAP partition with an incomplete fact table contains an internally merged copy of fact table data and operates correctly until it is processed.  
  
## Partition storage considerations: HOLAP and ROLAP Partitions  
 When HOLAP or ROLAP partitions that have different fact tables are merged, the fact tables are not automatically merged. Unless the fact tables are manually merged, only the fact table associated with the target partition is available to the resulting partition. Facts associated with the source partition are not available for drilldown in the resulting partition, and when the partition is processed, aggregations do not summarize data from the unavailable table.  
  
> [!IMPORTANT]  
>  A merged HOLAP or ROLAP partition with an incomplete fact table contains accurate aggregations, but incomplete facts. Queries that refer to missing facts return incorrect data. When the partition is processed, aggregations are computed only from available facts.  
  
 The absence of unavailable facts might not be noticed unless a user attempts to drill down to a fact in the unavailable table or executes a query that requires a fact from the unavailable table. Because aggregations are combined during the merge process, queries whose results are based only on aggregations return accurate data, whereas other queries may return inaccurate data. Even after the resulting partition is processed, the missing data from the unavailable fact table may not be noticed, especially if it represents only a small portion of the combined data.  
  
 Fact tables can be merged before or after merging the partitions. However, the aggregations will not accurately represent the underlying facts until both operations have been completed. It is recommended that you merge HOLAP or ROLAP partitions that access different fact tables when users are not connected to the cube that contains these partitions.  
  
##  <a name="bkmk_partitionSSMS"></a> How to merge partitions using SSMS  
  
> [!IMPORTANT]  
>  Before merging partitions, first copy the data filter information (often, the WHERE clause for filters based on SQL queries). Later, after the merge is completed, you should update the Partition Source property of the partition containing the accumulated fact data.  
  
1.  In Object Explorer, expand the **Measure Groups** node of the cube containing the partitions that you want to merge, expand **Partitions**, right-click the partition that is the target or destination of the merge operation. For example, if you are moving quarterly fact data to a partition that stores annual fact data, select the partition that contains the annual fact data.  
  
2.  Click **Merge Partitions** to open the **Merge Partition \<partition name>** dialog box.  
  
3.  Under **Source Partitions**, select the check box next to each source partition that you want to merge with the target partition, and click **OK**.  
  
    > [!NOTE]  
    >  Source partitions are immediately deleted after the source is merged into the target partition. Refresh the Partitions folder to update its contents after the merge is completed.  
  
4.  Right-click the partition containing the accumulated data and select **Properties**.  
  
5.  Open the `Source` property and modify the WHERE clause so that it includes the partition data you just merged. Recall that the `Source` property is not updated automatically. If you reprocess without first updating the `Source`, you might not get all of the expected data.  
  
##  <a name="bkmk_partitionsXMLA"></a> How to merge partitions using XMLA  
 See this topic for information, [Merging Partitions &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/merging-partitions-xmla.md).  
  
## See Also  
 [Processing Analysis Services Objects](processing-analysis-services-objects.md)   
 [Partitions &#40;Analysis Services - Multidimensional Data&#41;](../multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md)   
 [Create and Manage a Local Partition &#40;Analysis Services&#41;](create-and-manage-a-local-partition-analysis-services.md)   
 [Create and Manage a Remote Partition &#40;Analysis Services&#41;](create-and-manage-a-remote-partition-analysis-services.md)   
 [Set Partition Writeback](set-partition-writeback.md)   
 [Write-Enabled Partitions](../multidimensional-models-olap-logical-cube-objects/partitions-write-enabled-partitions.md)   
 [Configure String Storage for Dimensions and Partitions](configure-string-storage-for-dimensions-and-partitions.md)  
  
  
