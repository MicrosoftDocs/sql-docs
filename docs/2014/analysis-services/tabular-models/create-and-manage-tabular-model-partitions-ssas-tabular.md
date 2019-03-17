---
title: "Create and Manage Tabular Model Partitions (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: dab72cf0-95bc-4b63-95dc-505b5cd881c1
author: minewiskan
ms.author: owend
manager: craigg
---
# Create and Manage Tabular Model Partitions (SSAS Tabular)
  Partitions divide a table into logical parts. Each partition can then be processed (Refreshed) independent of other partitions. Partitions defined for a model during model authoring are duplicated in a deployed model. Once deployed, you can manage those partitions by using the **Partitions** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by using a script. Tasks provided in this topic describe how to create and manage partitions for a deployed model.  
  
 This topic includes the following tasks:  
  
-   [To create a new partition](#bkmk_create_new)  
  
-   [To copy a partition](#bkmk_copy)  
  
-   [To merge two or more partitions](#bkmk_merge)  
  
-   [To delete a partition](#bkmk_delete)  
  
## Tasks  
 To create and manage partitions for a deployed tabular model database, you will use the **Partitions** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. To view the **Partitions** dialog box, in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click on a table, and then click **Partitions**.  
  
###  <a name="bkmk_create_new"></a> To create a new partition  
  
1.  In the **Partitions** dialog box, click the **New** button.  
  
2.  In **Partition Name**, type a name for the partition. By default, the name of the default partition will be incrementally numbered for each new partition.  
  
3.  In **SQL Statement**, type or paste a SQL query statement that defines the columns and any clauses you want to include in the partition into the query window.  
  
4.  To validate the statement, click **Check Syntax**.  
  
###  <a name="bkmk_copy"></a> To copy a partition  
  
1.  In the **Partitions** dialog box, in the **Partitions** list, select the partition you want to copy, and then click the **Copy** button.  
  
2.  In **Partition Name**, type a new name for the partition.  
  
3.  In **SQL Statement**, edit the SQL query statement.  
  
###  <a name="bkmk_merge"></a> To merge two or more partitions  
  
-   In the **Partitions** dialog box, in the **Partitions** list, use Ctrl+click to select the partitions you want to merge, and then click the **Merge** button.  
  
> [!IMPORTANT]  
>  Merging partitions does not update the partition metadata. Administrators must alter the SQL Statement for the resulting partition to make sure processing operations process all data in the merged partition.  
  
###  <a name="bkmk_delete"></a> To delete a partition  
  
-   In the **Partitions** dialog box, in the **Partitions** list, select the partition you want to delete, and then click the **Delete** button.  
  
## See Also  
 [Tabular Model Partitions &#40;SSAS Tabular&#41;](partitions-ssas-tabular.md)   
 [Process Tabular Model Partitions &#40;SSAS Tabular&#41;](process-tabular-model-partitions-ssas-tabular.md)  
  
  
