---
title: Create a resource pool
description: Learn how you can create and use a resource pool for managing Python and R workloads in SQL Server Machine Learning Services. 
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 08/06/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Create a resource pool for SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Learn how you can create and use a resource pool for managing Python and R workloads in SQL Server Machine Learning Services. 

The process includes multiple steps:

1. Review status of any existing resource pools. It's important that you understand what services are using existing resources.
2. Modify server resource pools.
3. Create a new resource pool for external processes.
4. Create a classification function to identify external script requests.
5. Verify that the new external resource pool is capturing R or Python jobs from the specified clients or accounts.

<a name="bkmk_ReviewStatus"></a>

##  Review the status of existing resource pools
  
1.  Use a statement such as the following to check the resources assigned to the default pool for the server.
  
    ```sql
    SELECT * FROM sys.resource_governor_resource_pools WHERE name = 'default'
    ```

    **Sample results**

    |pool_id|name|min_cpu_percent|max_cpu_percent|min_memory_percent|max_memory_percent|cap_cpu_percent|min_iops_per_volume|max_iops_per_volume|
    |-|-|-|-|-|-|-|-|-|
    |2|default|0|100|0|100|100|0|0|

2.  Check the resources assigned to the default **external** resource pool.
  
    ```sql
    SELECT * FROM sys.resource_governor_external_resource_pools WHERE name = 'default'
    ```

    **Sample results**

    |external_pool_id|name|max_cpu_percent|max_memory_percent|max_processes|version|
    |-|-|-|-|-|-|
    |2|default|100|20|0|2|
 
3.  Under these server default settings, the external runtime will probably have insufficient resources to complete most tasks. To improve resources, you must modify the server resource usage as follows:
  
    -   Reduce the maximum computer memory that can be used by the database engine.
  
    -   Increase the maximum computer memory that can be used by the external process.

## Modify server resource usage

1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], run the following statement to limit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory usage to **60%** of the value in the 'max server memory' setting.

    ```sql
    ALTER RESOURCE POOL "default" WITH (max_memory_percent = 60);
    ```
  
2. Run the following statement to limit the use of memory by external processes to **40%** of total computer resources.
  
    ```sql
    ALTER EXTERNAL RESOURCE POOL "default" WITH (max_memory_percent = 40);
    ```
  
3.  To enforce these changes, you must reconfigure and restart Resource Governor as follows:
  
    ```sql
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ```
  
    > [!NOTE]
    >  These are just suggested settings to start with; you should evaluate your machine learning tasks in light of other server processes to determine the correct balance for your environment and workload.

## Create a user-defined external resource pool
  
1.  All changes to the configuration of Resource Governor are enforced across the server as a whole. The changes affect workloads that use the default pools for the server, as well as workloads that use the external pools.
  
     To provide more fine-grained control over which workloads should have precedence, you can create a new user-defined external resource pool. Define a classification function and assign it to the external resource pool. The **EXTERNAL** keyword is new.
  
     Create a new *user-defined external resource pool*. In the following example, the pool is named **ds_ep**.
  
    ```sql
    CREATE EXTERNAL RESOURCE POOL ds_ep WITH (max_memory_percent = 40);
    ```

2.  Create a workload group named `ds_wg` to use in managing session requests. For SQL queries you'll use the default pool; for all external process queries will use the `ds_ep` pool.
  
    ```sql
    CREATE WORKLOAD GROUP ds_wg WITH (importance = medium) USING "default", EXTERNAL "ds_ep";
    ```
  
     Requests are assigned to the default group whenever the request can't be classified, or if there's any other classification failure.

 
For more information, see [Resource Governor Workload Group](../../relational-databases/resource-governor/resource-governor-workload-group.md) and [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md).
  
## Create a classification function for machine learning
  
A classification function examines incoming tasks. It determines whether the task is one that can be run using the current resource pool. Tasks that do not meet the criteria of the classification function are assigned back to the server's default resource pool.
  
1. Begin by specifying that a classifier function should be used by Resource Governor to determine resource pools. You can assign a **null** as a placeholder for the classifier function.
  
    ```sql
    ALTER RESOURCE GOVERNOR WITH (classifier_function = NULL);
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ```
  
     For more information, see [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md).
  
2.  In the classifier function for each resource pool, define the type of statements or incoming requests that should be assigned to the resource pool.
  
     For example, the following function returns the name of the schema assigned to the user-defined external resource pool if the application that sent the request is either 'Microsoft R Host', 'RStudio', or 'Mashup'; otherwise it returns the default resource pool.
  
    ```sql
    USE master
    GO
    CREATE FUNCTION is_ds_apps()
    RETURNS sysname
    WITH schemabinding
    AS
    BEGIN
        IF program_name() in ('Microsoft R Host', 'RStudio', 'Mashup') RETURN 'ds_wg';
        RETURN 'default'
        END;
    GO
    ```
  
3.  When the function has been created, reconfigure the resource group to assign the new classifier function to the external resource group that you defined earlier.
  
    ```sql
    ALTER RESOURCE GOVERNOR WITH  (classifier_function = dbo.is_ds_apps);
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    GO
    ```

## Verify new resource pools and affinity

Check the server memory configuration and CPU for each of the workload groups. Verify the instance resource changes have been made, by reviewing:

+ the default pool for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] server
+ the default resource pool for external processes
+ the user-defined pool for external processes

1. Run the following statement to view all workload groups:

    ```sql
    SELECT * FROM sys.resource_governor_workload_groups;
    ```

    **Sample results**

    |group_id|name|importance|request_max_memory_grant_percent|request_max_cpu_time_sec|request_memory_grant_timeout_sec|max_dop|group_max_requests pool_id|pool_idd|external_pool_id|
    |-|-|-|-|-|-|-|-|-|-|
    |1|internal|Medium|25|0|0|0|0|1|2|
    |2|default|Medium|25|0|0|0|0|2|2|
    |256|ds_wg|Medium|25|0|0|0|0|2|256|
  
2.  Use the new catalog view, [sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md), to view all external resource pools.
  
    ```sql
    SELECT * FROM sys.resource_governor_external_resource_pools;
    ```

    **Sample results**
    
    |external_pool_id|name|max_cpu_percent|max_memory_percent|max_processes|version|
    |-|-|-|-|-|-|
    |2|default|100|20|0|2|
    |256|ds_ep|100|40|0|1|
  
     For more information, see [Resource Governor Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/resource-governor-catalog-views-transact-sql.md).
  
3.  Run the following statement to return information about the computer resources that are affinitized to the external resource pool, if applicable:
  
    ```sql
    SELECT * FROM sys.resource_governor_external_resource_pool_affinity;
    ```
  
     No information will be displayed because the pools were created with an affinity of AUTO. For more information, see [sys.dm_resource_governor_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pool-affinity-transact-sql.md).

## Next steps

For more information about managing server resources, see:

+ [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) 
+ [Resource Governor Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md)

For an overview of resource governance for machine learning, see:

+ [Manage Python and R workloads with Resource Governor in SQL Server Machine Learning Services](resource-governor.md)
