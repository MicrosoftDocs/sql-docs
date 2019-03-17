---
title: "Configure String Storage for Dimensions and Partitions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 987f6cfc-da82-4b2e-96ef-a8af88339e5f
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure String Storage for Dimensions and Partitions
  You can reconfigure string storage to accommodate very large strings in dimension attributes or partitions that exceed the 4 GB file size limit for string stores. If your dimensions or partitions include string stores of this size, you can work around the file size constraint by changing the **StringStoresCompatibilityLevel** property at the dimension or partition level, for local as well as linked (local or remote) objects.  
  
 Note that you can increase string storage on just those objects that require additional capacity. In most multidimensional models, string data is associated with dimensions. However, partitions that contain distinct count measures on top of strings can also benefit from this setting. Because the setting is for strings, numeric data is unaffected.  
  
 Valid values for this property include the following:  
  
|Value|Description|  
|-----------|-----------------|  
|**1050**|Specifies the default string storage architecture, subject to a 4 GB maximum file size per store.|  
|**1100**|Specifies larger string storage, supports up to 4 billion unique strings per store.|  
  
> [!IMPORTANT]  
>  Changing the string storage settings of an object requires that you reprocess the object itself and any dependent object. Processing is required to complete the procedure.  
  
 This topic contains the following sections:  
  
-   [About String Stores](#bkmk_background)  
  
-   [Prerequisites](#bkmk_prereq)  
  
-   [Step 1: Set the StringStoreCompatiblityLevel Property in SQL Server Data Tools](#bkmk_step1)  
  
-   [Step 2: Process the Objects](#bkmk_step2)  
  
##  <a name="bkmk_background"></a> About String Stores  
 String storage configuration is optional, which means that even new databases that you create use the default string store architecture that is subject to the 4 GB maximum file size. Using the larger string storage architecture has a small but noticeable impact on performance. You should use it only if your string storage files are close to or at the maximum 4 GB limit.  
  
> [!NOTE]  
>  This setting does not apply to data mining models. Currently, it is still possible to encounter the GB file size limitation on models containing data mining structures.  
  
 In an Analysis Services multidimensional database, strings are stored separately from numeric data to allow for optimizations based on characteristics of the data. String data is typically found in dimension attributes that represent names or descriptions. It is also possible to have string data in distinct count measures. String data can also be used in keys.  
  
 You can identify a string store by its file extension (for example, asstore, .bstore, .ksstore, or .string files). By default, each of these files is subject to a maximum 4 GB limit. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], you can override the maximum file size by specifying an alternative storage mechanism that allows a string store to grow as needed.  
  
 In contrast with the default string storage architecture which limits the size of the physical file, larger string storage is based on a maximum number of strings. The maximum limit for larger string storage is 4 billion unique strings or 4 billion records, whichever occurs first. Larger string storage creates records of an even size, where each record is equal to a 64K page. If you have very long strings that do not fit in a single record, your effective limit will be less than 4 billion strings.  
  
##  <a name="bkmk_prereq"></a> Prerequisites  
 You must have a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or later version of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 Dimensions and partitions must use MOLAP storage.  
  
 The database compatibility level must be set to 1100. If you created or deployed a database using [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] and the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or later version of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the database compatibility level is already set to 1100. If you moved a database created in an earlier version of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to ssSQL11 or later, you must update the compatibility level. For databases that you are moving, but not redeploying, you can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to set the compatibility level. For more information, see [Set the Compatibility Level of a Multidimensional Database &#40;Analysis Services&#41;](compatibility-level-of-a-multidimensional-database-analysis-services.md).  
  
##  <a name="bkmk_step1"></a> Step 1: Set the StringStoreCompatiblityLevel Property in SQL Server Data Tools  
  
1.  Using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project that contains the dimensions or partitions you want to modify.  
  
2.  To change the string storage for dimensions, open Solution Explorer. Double-click the dimension for which you are modifying string storage.  
  
3.  In Dimension Designer, in the Attributes pane, make sure that the parent node of the dimension is selected (for example, if the dimension is Customers, select Customers and not one of the child attributes).  
  
4.  In the Properties pane, in the Advanced section, set **StringStoresCompatibilityLevel** to **1100**. Repeat for other dimensions that require larger storage, otherwise leave the remaining dimensions at the **1050** value.  
  
5.  For partitions, open a cube from Solution Explorer.  
  
6.  Click the Partitions tab.  
  
7.  Expand the partition, select the partition that requires extra storage capacity, and then modify the **StringStoresCompatibilityLevel** property.  
  
8.  Save the file.  
  
##  <a name="bkmk_step2"></a> Step 2: Process the Objects  
 The new storage architecture will be used after you process the objects. Processing objects also proves you have successfully resolved storage constraint problem because the error that previously reported a string store overflow condition should no longer occur.  
  
-   In Solution Explorer, right-click the dimension or you just modified and select **Process**.  
  
 You must use the Process Full option on each object that is using the new string store architecture. Before processing, be sure to run an impact analysis on the dimension to check whether dependent objects also require reprocessing.  
  
## See Also  
 [Tools and Approaches for Processing &#40;Analysis Services&#41;](tools-and-approaches-for-processing-analysis-services.md)   
 [Processing Options and Settings &#40;Analysis Services&#41;](processing-options-and-settings-analysis-services.md)   
 [Partition Storage Modes and Processing](../multidimensional-models-olap-logical-cube-objects/partitions-partition-storage-modes-and-processing.md)   
 [Dimension Storage](../multidimensional-models-olap-logical-dimension-objects/dimensions-storage.md)  
  
  
