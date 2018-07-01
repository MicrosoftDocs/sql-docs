---
title: "Query the Parameters Used to Create a Mining Model | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Query the Parameters Used to Create a Mining Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The composition of a mining model is affected not only by the training cases, but by the parameters that were set when the model was created. Therefore, you might find it useful to retrieve the parameter settings of an existing model to better understand the behavior of the model. Retrieving parameters is also useful when documenting a particular version of that model.  
  
 To find the parameters that were used when the model was created, you create a query against one of the mining model schema rowsets. These schema rowsets are exposed as a set of system views that you can query easily by using Transact-SQL syntax. This procedure describes how to create a query that returns the parameters that were used to create the specified mining model.  
  
### To open a Query window for a schema rowset query  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that contains the model you want to query.  
  
2.  Right-click the instance name, select **New Query**, and then select **DMX**.  
  
    > [!NOTE]  
    >  You can also create a query against a data mining model by using the **MDX** template.  
  
3.  If the instance contains multiple databases, select the database that contains the model you want to query from the **Available Databases** list in the toolbar.  
  
### To return model parameters for an existing mining model  
  
1.  In the DMX query pane, type or paste the following text:  
  
    ```  
    SELECT MINING_PARAMETERS  
    FROM $system.DMSCHEMA_MINING_MODELS  
    WHERE MODEL_NAME = ''  
    ```  
  
2.  In Object Explorer, select the mining model you want, and then drag it into the DMX Query pane, between the single quotation marks.  
  
3.  Press F5, or click **Execute**.  
  
## Example  
 The following code returns a list of the parameters that were used to create the mining model that you build in the [Basic Data Mining Tutorial](http://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c). These parameters include the explicit values for any defaults used by the mining services available from providers on the server.  
  
```  
SELECT MINING_PARAMETERS   
FROM $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'TM Clustering'  
```  
  
 The code example returns the following parameters for the clustering model:  
  
 eExample Results:  
  
 MINING_PARAMETERS  
  
 CLUSTER_COUNT=10,CLUSTER_SEED=0,CLUSTERING_METHOD=1,MAXIMUM_INPUT_ATTRIBUTES=255,MAXIMUM_STATES=100,MINIMUM_SUPPORT=1,MODELLING_CARDINALITY=10,SAMPLE_SIZE=50000,STOPPING_TOLERANCE=10  
  
## See Also  
 [Data Mining Query Tasks and How-tos](../../analysis-services/data-mining/data-mining-query-tasks-and-how-tos.md)   
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)  
  
  
