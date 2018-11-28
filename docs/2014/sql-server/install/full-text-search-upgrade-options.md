---
title: "Full-Text Search Upgrade Options | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "Full-Text Search"
  - "Upgrade options, Full-Text Search"
ms.assetid: 16c9376b-5fbb-4495-a429-06a2493849c9
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Full-Text Search Upgrade Options
  Use the Full-Text Search Upgrade Options page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to select the full-text search upgrade option to use for the databases that you are upgrading at this time.  
  
 In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] each full-text index resides in a full-text catalog that belongs to a filegroup, has a physical path, and is treated as a database file. Now, a full-text catalog is a logical concept-a virtual object-that refers to a group of full-text indexes. Therefore, a new full-text catalog is not treated as a database file with a physical path. However, during upgrade of any full-text catalog that contains data files, a new filegroup is created on same disk. This maintains the old disk I/O behavior after upgrade. Any full-text index from that catalog is placed in the new filegroup if the root path exists. If the old full-text catalog path is invalid, the upgrade keeps the full-text index in the same filegroup as base table or, for a partitioned table, in the primary filegroup.  
  
## Options  
 When you upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], choose one of the following full-text upgrade options.  
  
 **Import**  
 Full-text catalogs are imported. Typically, import is significantly faster than rebuild. For example, when using only one CPU, import runs about 10 times faster than rebuild. However, a full-text catalog imported from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] does not use the new and enhanced word breakers, so you might want to rebuild your full-text catalogs eventually.  
  
> [!NOTE]  
>  Rebuild can run in multi-threaded mode, and if more than 10 CPUs are available, rebuild might run faster than import if you allow rebuild to use all of the CPUs.  
  
 If a full-text catalog is not available, the associated full-text indexes are rebuilt. This option is available for only [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] databases.  
  
 For information about the impact of importing full-text index, see "Considerations for Choosing a Full-Text Upgrade Option," later in this topic.  
  
 **Rebuild**  
 Full-text catalogs are rebuilt using the new and enhanced word breakers. Rebuilding indexes can take a lot of time, and a significant amount of CPU and memory might be required after the upgrade.  
  
 **Reset**  
 Full-text catalogs are reset. When upgrading from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], full-text catalog files are removed, but the metadata for full-text catalogs and full-text indexes is retained. After being upgraded, all full-text indexes are disabled for change tracking and crawls are not started automatically. The catalog will remain empty until you manually issue a full population, after the upgrade completes.  
  
 All of these upgrade options ensure that upgraded databases benefit fully from full-text performance enhancements.  
  
## Considerations for Choosing a Full-Text Upgrade Option  
 When choosing the upgrade option for your upgrade, consider the following:  
  
-   How do you use word breakers?  
  
     The full-text search service in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] includes word breakers and stemmers. These might change the results of full-text queries from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] for a specific text pattern or scenario. Therefore, how you use word breakers is important when choosing a suitable upgrade option:  
  
    -   If the word breakers of the full-text language you use did not change, or if recall accuracy is not critical to you, importing is suitable. Later, if you experience any recall issues, you can upgrade to the new word breakers simply by rebuilding your full-text catalogs.  
  
    -   If you care about recall accuracy and you use one of the word breakers that were added after [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], rebuilding is suitable.  
  
-   Were any full-text indexes built on integer full-text key columns?  
  
     Rebuilding performs internal optimizations that improve the query performance of the upgraded full-text index in some cases. Specifically, if you have full-text catalogs that contain full-text indexes for which the full-text key column of the base table is an integer data type, rebuilding achieves ideal performance of full-text queries after upgrade. In this case, we highly recommend you to use the **Rebuild** option.  
  
    > [!NOTE]  
    >  For full-text indexes in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], we recommend that the column serving as the full-text key be an integer data type. For more information, see [Improve the Performance of Full-Text Indexes](../../relational-databases/indexes/indexes.md).  
  
-   What is the priority for getting your server instance online?  
  
     Importing or rebuilding during upgrade takes a lot of CPU resources, which delays getting the rest of the server instance upgraded and online. If getting the server instance online as soon as possible is important and if you are willing to run a manual population after the upgrade, **Reset** is suitable.  
  
## Additional Resources  
  
