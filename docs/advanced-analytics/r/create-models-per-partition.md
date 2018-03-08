---
title: "Create multiple models per partition (SQL Server Machine Learning Services) | Microsoft Docs"
ms.custom: "sqlseattle"
ms.date: "03/08/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "HeidiSteen"
ms.author: "heidist"
manager: "cgronlun"
ms.workload: "Inactive"
---
# Create multiple models based on partitions
[!INCLUDE[appliesto-ssvnex-xxxx-xxxx-xxx-md-winonly](../../includes/tsql-appliesto-ssvnext-xxxx-xxxx-xxx.md)]

**(Not for production workloads)**

SQL Server vNext CTP 1.4 adds new parameters to [sp_execute_external_script](../../sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to allow parallel processing of partitions:

+ input_data_1_partition_by_columns, specifying which columns to partition by
+ input_data_1_order_by_columns, specifying which columns to order by
 
With these parameters, you can perform parallel processing of multiple related models. Rather than train one very large model based on the entire dataset, you can create many small models, each processing data specific to a partition. Consider data partitioned by location, date, or product area -- you can now model, train, and score data in its partitioned state.

## Prerequisites
 
To obtain one model per partition, you must leverage parallelism. Your SQL query must execute on a parallel execution plan. Once the query plan is in place, you can train one model per partition, with minimal changes made to your original script (what is my original script?).

## R Example
 
This example uses the NYCTaxi data set, stored in five partitions, resulting in five individual models. 

For more information about the data set, see (LINK)
 
INSERT TRAINING SCRIPT HERE
 
In the same way, partition by can be used to for scoring. The below sample contains an R script that will score using the correct model for the partition it is currently processing.
 
INSERT SCORING SCRIPT HERE

