---
title: "Create and Manage Tabular Model Partitions | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create and manage tabular model partitions
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]

  Partitions divide a table into logical parts. Each partition can then be processed (Refreshed) independent of other partitions. Partitions defined for a model during model authoring are duplicated in a deployed model. Once deployed, you can manage those partitions by using the **Partitions** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by using a script. Tasks provided in this topic describe how to create and manage partitions for a deployed model.  
  
  > [!NOTE]  
>  Partitions in tabular models created at the 1400 compatibility level are defined using an M query statement. To learn more, see [M Reference](https://msdn.microsoft.com/library/mt211003.aspx). 
>
  
## Tasks  
 To create and manage partitions for a deployed tabular model database, you will use the **Partitions** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. To view the **Partitions** dialog box, in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click on a table, and then click **Partitions**.  
  
###  <a name="bkmk_create_new"></a> To create a new partition  
  
1.  In the **Partitions** dialog box, click the **New** button.  
  
2.  In **Partition Name**, type a name for the partition. By default, the name of the default partition will be incrementally numbered for each new partition.  
  
3.  In **Query Statement**, type or paste a SQL or M query statement that defines the columns and any clauses you want to include in the partition into the query window.  
  
4.  To validate the statement, click **Check Syntax**.  
  
###  <a name="bkmk_copy"></a> To copy a partition  
  
1.  In the **Partitions** dialog box, in the **Partitions** list, select the partition you want to copy, and then click the **Copy** button.  
  
2.  In **Partition Name**, type a new name for the partition.  
  
3.  In **Query Statement**, edit the query statement.  
  
###  <a name="bkmk_merge"></a> To merge two or more partitions  
  
-   In the **Partitions** dialog box, in the **Partitions** list, use Ctrl+click to select the partitions you want to merge, and then click the **Merge** button.  
  
> [!IMPORTANT]  
>  Merging partitions does not update the partition metadata. You must edit the SQL or M statement for the resulting partition to make sure processing operations process all data in the merged partition.  
  
###  <a name="bkmk_delete"></a> To delete a partition  
  
-   In the **Partitions** dialog box, in the **Partitions** list, select the partition you want to delete, and then click the **Delete** button.  
  
## See also  
 [Tabular model partitions](../../analysis-services/tabular-models/tabular-model-partitions-ssas-tabular.md)   
 [Process tabular model partitions](../../analysis-services/tabular-models/process-tabular-model-partitions-ssas-tabular.md)  
  
  
