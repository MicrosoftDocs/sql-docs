---
title: "Clear the Analysis Services Caches | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 6bf66fdd-6a03-4cea-b7e2-eb676ff276ff
author: minewiskan
ms.author: owend
manager: craigg
---
# Clear the Analysis Services Caches
  Analysis Services caches data to boost query performance. This topic provides recommendations for using the XMLA ClearCache command to clear caches that were created in response to an MDX query. The effects of running ClearCache vary depending on whether you are using a tabular or multidimensional model.  
  
 **When to clear the cache for multidimensional models**  
  
 For multidimensional databases, Analysis Services builds caches in the formula engine when evaluating a calculation, and in the storage engine for the results of dimension queries and measure group queries. Measure group queries occur when the formula engine needs measure data for a cell coordinate or subcube. Dimension queries occur when querying unnatural hierarchies and when applying autoexists.  
  
 Clearing the cache is recommended when conducting performance testing. By clearing the cache between test runs, you ensure that caching does not skew any test results that measure the impact of a query design change.  
  
 **When to clear the cache for tabular models**  
  
 Tabular models are generally stored in memory, where aggregations and other calculations are performed at the time a query is executed. As such, the ClearCache command has a limited effect on tabular models. For a tabular model, data may be added to the Analysis Services caches if MDX queries are run against it. In particular, DAX measures referenced by MDX and autoexists operations can cache results in the formula cache and dimension cache respectively. Note however, that unnatural hierarchies and measure group queries do not cache results in the storage engine. Additionally, it is important to recognize that DAX queries do not cache results in the formula and storage engine. To the extent that caches exist as a result of MDX queries, running ClearCache against a tabular model will invalidate any cached data from the system.  
  
 Running ClearCache will also clear in-memory caches in the xVelocity in-memory analytics engine (VertiPaq). The xVelocity engine maintains a small set of cached results. Running ClearCache will invalidate these caches in the xVelocity engine.  
  
 Finally, running ClearCache will also remove residual data that is left in memory when a tabular model is reconfigured for `DirectQuery` mode. This is particularly important if the model contains sensitive data that is subject to tight controls. In this case, running ClearCache is a precautionary action that you can take to ensure that sensitive data exists only where you expect it to be. Clearing the cache manually is necessary if you are using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to deploy the model and change the query mode. In contrast, using [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] to specify `DirectQuery` on the model and partitions will automatically clear the cache when you switch the model to use that query mode.  
  
 Compared with recommendations for clearing multidimensional model caches during performance testing, there is no broad recommendation for clearing tabular model caches. If you are not managing the deployment of a tabular model that contains sensitive data, there is no specific administrative task that calls for clearing the cache.  
  
## Clear the cache for Analysis Services models  
 To clear the cache, use XMLA and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can clear the cache at the database, cube, dimension or table, or measure group level. The following steps for clearing the cache at the database level apply to both multidimensional models and tabular models.  
  
> [!NOTE]  
>  Rigorous performance testing might require a more comprehensive approach to clearing the cache. For instructions on how to flush Analysis Services and file system caches, see the section on clearing caches in the [SQL Server 2008 R2 Analysis Services Operations Guide](https://go.microsoft.com/fwlink/?linkID=https://go.microsoft.com/fwlink/?LinkID=225539).  
  
 For both multidimensional and tabular models, clearing some of these caches can be a two-step process that consists of invalidating the cache when ClearCache executes, followed by emptying the cache when the next query is received. A reduction in memory consumption will be evident only after the cache is actually emptied.  
  
 Clearing the cache requires that you provide an object identifier to the `ClearCache` statement in an XMLA query. The first step in this topic explains how to get an object identifier.  
  
#### Step 1: Get the object identifier  
  
1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], right-click an object, select **Properties**, and copy the value from the ID property in the **Properties** pane. This approach works for the database, cube, dimension, or table.  
  
2.  To get the measure group ID, right-click the measure group and select **Script Measure Group As**. Choose either **Create** or **Alter**, and send the query to a window. The ID of the measure group will be visible in the object definition. Copy the ID of the object definition.  
  
#### Step 2: Run the query  
  
1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], right-click a database, point to **New Query**, and select **XMLA**.  
  
2.  Copy the following code example into the XMLA query window. Change `DatabaseID` to the ID of the database on the current connection.  
  
    ```  
    <ClearCache xmlns="https://schemas.microsoft.com/analysisservices/2003/engine">  
      <Object>  
        <DatabaseID> Adventure Works DW Multidimensional</DatabaseID>  
      </Object>  
    </ClearCache>  
  
    ```  
  
     Alternatively, you can specify a path of a child object, such as a measure group, to clear the cache for just that object.  
  
    ```  
    <ClearCache xmlns="https://schemas.microsoft.com/analysisservices/2003/engine">  
      <Object>  
        <DatabaseID>Adventure Works DW Multidimensional</DatabaseID>  
            <CubeID>Adventure Works</CubeID>  
            <MeasureGroupID>Fact Currency Rate</MeasureGroupID>  
      </Object>  
    </ClearCache>  
    ```  
  
3.  Press F5 to execute the query. You should see the following result:  
  
    ```  
    <return xmlns="urn:schemas-microsoft-com:xml-analysis">  
      <root xmlns="urn:schemas-microsoft-com:xml-analysis:empty" />  
    </return>  
    ```  
  
## See Also  
 [Script Administrative Tasks in Analysis Services](../script-administrative-tasks-in-analysis-services.md)   
 [Monitor an Analysis Services Instance](monitor-an-analysis-services-instance.md)  
  
  
