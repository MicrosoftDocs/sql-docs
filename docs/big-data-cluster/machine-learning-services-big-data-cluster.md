---
title: Use Machine Learning Services (Python and R)
description: 
author: dphansen
ms.author: davidph
ms.date: 10/21/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: machine-learning
---

# Use Machine Learning Services (Python and R) on a Big Data Cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

You can run Python and R scripts on the master instance of a [SQL Server Big Data Clusters](big-data-cluster-overview.md) with [Machine Learning Services](../advanced-analytics/index.yml).

## Enable Machine Learning Services

Machine Learning Services is installed by default on Big Data Clusters and does require separate installation. 

To enable Machine Learning Services, run this statement on the master instance:

```sql
EXEC sp_configure 'external scripts enabled', 1
RECONFIGURE WITH OVERRIDE
GO
```

## Enable on Always On Availability Groups

If you are using SQL Server Big Data Clusters with [Always On Availability Groups](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md), you need to perform a few extra steps to enable Machine Learning Services.

1. Connect to the master instance and run this statement:

    ```sql
    SELECT @@SERVERNAME
    ```

    Note down the server name. In this example, the server name is **master-2**.

1. On each replica on the Always On Availability Group in the Big Data Cluster, run these `kubectl` commands:

    ```
    kubectl -n bdc expose pod master-0 --port=1533 --name=mymaster-0 --type=LoadBalancer

    kubectl -n bdc expose pod master-1 --port=1533 --name=mymaster-1 --type=LoadBalancer

    kubectl -n bdc expose pod master-2 --port=1533 --name=mymaster-2 --type=LoadBalancer
    ```

    You should see an output similar to this:
    
    ```
    service/mymaster-0 exposed
    
    service/mymaster-1 exposed
    ```

1. Connect to each master replica endpoint and enable script execution.

    Run this statement:

    ```sql
    EXEC sp_configure 'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    GO
    ```

You are now ready to run Python and R scripts on the master instance of your Big Data Cluster.

## Next steps

+ [Quickstart: Create and run simple Python scripts with SQL Server Machine Learning Services](../advanced-analytics/tutorials/quickstart-python-create-script.md)
+ [Quickstart: Create and score a predictive model in Python with SQL Server Machine Learning Services](../advanced-analytics/tutorials/quickstart-python-train-score-model.md)
+ [Quickstart: Create and run simple R scripts with SQL Server Machine Learning Services](../advanced-analytics/tutorials/quickstart-r-create-script)
+ [Quickstart: Create and score a predictive model in R with SQL Server Machine Learning Services](../advanced-analytics/tutorials/quickstart-r-train-score-model.md)