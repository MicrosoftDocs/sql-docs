---
title: Machine Learning Services (Python, R)
titleSuffix: SQL Server Big Data Clusters
description: Learn how you can run Python and R scripts on the master instance of a SQL Server 2019 Big Data Clusters with Machine Learning Services.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf
ms.date: 09/09/2022
ms.service: sql
ms.subservice: machine-learning-bdc
ms.topic: conceptual
---

# Run Python and R scripts with Machine Learning Services on SQL Server 2019 Big Data Clusters

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

You can run Python and R scripts on the master instance of [SQL Server Big Data Clusters](big-data-cluster-overview.md) with [Machine Learning Services](../machine-learning/index.yml).

> [!NOTE]  
> You can also run Java code on the master instance of SQL Server Big Data Clusters with the [Java Language Extension](../language-extensions/java-overview.md). Following the steps below will also enable [SQL Server Language Extensions](../language-extensions/language-extensions-overview.md).

## Enable Machine Learning Services

Machine Learning Services is installed by default on SQL Server 2019 Big Data Clusters and does not require separate installation.

To enable Machine Learning Services, run this statement on the master instance:

```sql
EXEC sp_configure 'external scripts enabled', 1
RECONFIGURE WITH OVERRIDE
GO
```

You are now ready to run Python and R scripts on the master instance of Big Data Clusters. See the quickstarts under [Next steps](#next-steps) to run your first script.

> [!NOTE]  
>The configuration setting cannot be set on an availability group listener connection. If Big Data Clusters is deployed with high availability, the set `external scripts enabled` on each replica. See [Enable on cluster with high availability](#enable-on-cluster-with-high-availability).

## Enable on cluster with high availability

When you [Deploy SQL Server Big Data Cluster with high availability](deployment-high-availability.md), the deployment creates an availability group for the master instance. To enable Machine Learning Services, set `external scripts enabled` on each instance of the availability group. For a Big Data Cluster, you need to run `sp_configure` on each replica of the SQL Server master instance

The following section describes how to enable external scripts on each instance.

### Create an external load balancer for each instance

For each replica on the availability group, create a load balancer to allow you to connect to the instance.

`kubectl expose pod <pod-name> --port=<connection port number> --name=<load-balancer-name> --type=LoadBalancer -n <kubernetes namespace>`

The examples in this article use the following values:

- `<pod-name>`: `master-#`
- `<connection port number>`: `1533`
- `<load-balancer-name>`: `mymaster-#`
- `<kubernetes namespace>`: `mssql-cluster`

Update the following script for your environment, and run the commands:

```bash
kubectl expose pod master-0 --port=1533 --name=mymaster-0 --type=LoadBalancer -n mssql-cluster
kubectl expose pod master-1 --port=1533 --name=mymaster-1 --type=LoadBalancer -n mssql-cluster
kubectl expose pod master-2 --port=1533 --name=mymaster-2 --type=LoadBalancer -n mssql-cluster
```

`kubectl` returns the following output.

```bash
service/mymaster-0 exposed
service/mymaster-1 exposed
service/mymaster-2 exposed
```

Each load balancer is a master replica endpoint.

### Enable script execution on each replica

1. Get the IP address for the master replica endpoint.

   The following command returns the external IP address for the replica endpoint.

   `kubectl get services <load-balancer-name> -n <kubernetes namespace>`

   To get the external IP address for each replica in this scenario, run the following commands:

   ```bash
   kubectl get services mymaster-0 -n mssql-cluster
   kubectl get services mymaster-1 -n mssql-cluster
   kubectl get services mymaster-2 -n mssql-cluster
   ```

   > [!NOTE]  
   > It may take a little time before the external IP address is available. Run the preceding script periodically until each endpoint returns an external IP address.

1. Connect to the master replica endpoint and enable script execution.

    Run this statement:

    ```sql
    EXEC sp_configure 'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    GO
    ```

   For example, you can run the preceding command with `sqlcmd`. The following example connects to the master replica endpoint and enables script execution. Update the values in the script with for your environment.

   ```bash
   sqlcmd -S <IP address>,1533 -U <user name> -P <password> -Q "EXEC sp_configure 'external scripts enabled', 1; RECONFIGURE WITH OVERRIDE;"
   ```

   Repeat the step for each replica.

### Demonstration

The following image demonstrates this process.

:::image type="content" source="media/machine-learning-services/example-kube-enable-scripts.png" alt-text="A screenshot of the command prompt providing a demo of the steps necessary to enable external scripts." lightbox="media/machine-learning-services/example-kube-enable-scripts.png" ::: 

You are now ready to run Python and R scripts on the master instance of Big Data Clusters. See the quickstarts under [Next steps](#next-steps) to run your first script.

### Delete the master replica endpoints

On the Kubernetes cluster, delete the endpoint for each replica. The endpoint is exposed in Kubernetes as a load-balancing service.

The following command deletes load-balancing service.

`kubectl delete svc <load-balancer-name> -n mssql-cluster`

For the examples in this article, run the following commands.

```bash
kubectl delete svc mymaster-0 -n mssql-cluster
kubectl delete svc mymaster-1 -n mssql-cluster
kubectl delete svc mymaster-2 -n mssql-cluster
```

## SQL Server Big Data Clusters machine learning quickstarts

### Python quickstarts

 - [Run Python scripts](../machine-learning/tutorials/quickstart-python-create-script.md?view=sql-server-ver15&preserve-view=true)
 - [Data structures and objects](../machine-learning/tutorials/quickstart-python-data-structures.md?view=sql-server-ver15&preserve-view=true)
 - [Python functions](../machine-learning/tutorials/quickstart-python-functions.md?view=sql-server-ver15&preserve-view=true)
 - [Train and score a model](../machine-learning/tutorials/quickstart-python-train-score-model.md?view=sql-server-ver15&preserve-view=true)

### R quickstarts

 - [Run R scripts](../machine-learning/tutorials/quickstart-r-create-script.md?view=sql-server-ver15&preserve-view=true)
 - [Data types and objects](../machine-learning/tutorials/quickstart-r-data-types-and-objects.md?view=sql-server-ver15&preserve-view=true)
 - [R functions](../machine-learning/tutorials/quickstart-r-functions.md?view=sql-server-ver15&preserve-view=true)
 - [Train and score a model](../machine-learning/tutorials/quickstart-r-train-score-model.md?view=sql-server-ver15&preserve-view=true)

## SQL Server Big Data Clusters machine learning tutorials

### Python tutorial

#### Ski rental (linear regression)

 - [1 - Introduction](../machine-learning/tutorials/python-ski-rental-linear-regression.md?view=sql-server-ver15&preserve-view=true)
 - [2 - Prepare data](../machine-learning/tutorials/python-ski-rental-linear-regression-prepare-data.md?view=sql-server-ver15&preserve-view=true)
 - [3 - Train model](../machine-learning/tutorials/python-ski-rental-linear-regression-train-model.md?view=sql-server-ver15&preserve-view=true)
 - [4 - Deploy model](../machine-learning/tutorials/python-ski-rental-linear-regression-deploy-model.md?view=sql-server-ver15&preserve-view=true)

#### Categorize customers (k-means clustering)

 - [1 - Introduction](../machine-learning/tutorials/python-clustering-model.md?view=sql-server-ver15&preserve-view=true)
 - [2 - Prepare the data](../machine-learning/tutorials/python-clustering-model-prepare-data.md?view=sql-server-ver15&preserve-view=true)
 - [3 - Create the model](../machine-learning/tutorials/python-clustering-model-build.md?view=sql-server-ver15&preserve-view=true)
 - [4 - Deploy the model](../machine-learning/tutorials/python-clustering-model-deploy.md?view=sql-server-ver15&preserve-view=true)

#### NYC taxi tips (classification)

 - [1 - Introduction](../machine-learning/tutorials/python-taxi-classification-introduction.md?view=sql-server-ver15&preserve-view=true)
 - [2 - Data exploration](../machine-learning/tutorials/python-taxi-classification-explore-data.md?view=sql-server-ver15&preserve-view=true)
 - [3 - Feature engineering](../machine-learning/tutorials/python-taxi-classification-create-features.md?view=sql-server-ver15&preserve-view=true)
 - [4 - Train and deploy](../machine-learning/tutorials/python-taxi-classification-train-model.md?view=sql-server-ver15&preserve-view=true)
 - [5 - Predictions](../machine-learning/tutorials/python-taxi-classification-deploy-model.md?view=sql-server-ver15&preserve-view=true)

### R tutorials

#### Ski rental (decision tree)

 - [1 - Introduction](../machine-learning/tutorials/r-predictive-model-introduction.md?view=sql-server-ver15&preserve-view=true)
 - [2 - Prepare data](../machine-learning/tutorials/r-predictive-model-prepare-data.md?view=sql-server-ver15&preserve-view=true)
 - [3 - Train model](../machine-learning/tutorials/r-predictive-model-train.md?view=sql-server-ver15&preserve-view=true)
 - [4 - Deploy model](../machine-learning/tutorials/r-predictive-model-deploy.md?view=sql-server-ver15&preserve-view=true)

#### Categorize customers (k-means clustering)

 - [1 - Introduction](../machine-learning/tutorials/r-clustering-model-introduction.md?view=sql-server-ver15&preserve-view=true)
 - [2 - Prepare the data](../machine-learning/tutorials/r-clustering-model-prepare-data.md?view=sql-server-ver15&preserve-view=true)
 - [3 - Create the model](../machine-learning/tutorials/r-clustering-model-build.md?view=sql-server-ver15&preserve-view=true)
 - [4 - Deploy the model](../machine-learning/tutorials/r-clustering-model-deploy.md?view=sql-server-ver15&preserve-view=true)

#### NYC taxi tips (classification)

 - [1 - Introduction](../machine-learning/tutorials/r-taxi-classification-introduction.md?view=sql-server-ver15&preserve-view=true)
 - [2 - Data exploration](../machine-learning/tutorials/r-taxi-classification-explore-data.md?view=sql-server-ver15&preserve-view=true)
 - [3 - Feature engineering](../machine-learning/tutorials/r-taxi-classification-create-features.md?view=sql-server-ver15&preserve-view=true)
 - [4 - Train and deploy](../machine-learning/tutorials/r-taxi-classification-train-model.md?view=sql-server-ver15&preserve-view=true)
 - [5 - Predictions](../machine-learning/tutorials/r-taxi-classification-deploy-model.md?view=sql-server-ver15&preserve-view=true)

## SQL Server Big Data Clusters machine learning how-to guides

### Data exploration and modeling

 - [Plot Histogram in Python](../machine-learning/data-exploration/python-plot-histogram.md?view=sql-server-ver15&preserve-view=true)
 - [Import data into pandas dataframe](../machine-learning/data-exploration/python-dataframe-pandas.md?view=sql-server-ver15&preserve-view=true)
 - [Insert dataframe into SQL](../machine-learning/data-exploration/python-dataframe-sql-server.md?view=sql-server-ver15&preserve-view=true)

### Data type conversions

 - [Python to SQL](../machine-learning/python/python-libraries-and-data-types.md?view=sql-server-ver15&preserve-view=true)
 - [R to SQL](../machine-learning/r/r-libraries-and-data-types.md?view=sql-server-ver15&preserve-view=true)

### Deploy

 - [Operationalize using stored procedures](../machine-learning/tutorials/python-ski-rental-linear-regression-deploy-model.md?preserve-view=true&view=sql-server-ver15)
 - [Convert R code for SQL Server](../machine-learning/deploy/modify-r-python-code-to-run-in-sql-server.md?preserve-view=true&view=sql-server-ver15)

### Predictions

 - [Native scoring with PREDICT T-SQL](../machine-learning/predictions/native-scoring-predict-transact-sql.md?view=sql-server-ver15&preserve-view=true)

### Package management

#### Install new Python packages

 - [Get Python package information](../machine-learning/package-management/python-package-information.md?view=sql-server-ver15&preserve-view=true)
 - [Install with sqlmlutils](../machine-learning/package-management/install-additional-python-packages-on-sql-server.md?view=sql-server-ver15&preserve-view=true)

#### Install new R packages

 - [Get R package information](../machine-learning/package-management/r-package-information.md?view=sql-server-ver15&preserve-view=true)
 - [Install with sqlmlutils](../machine-learning/package-management/install-additional-r-packages-on-sql-server.md?view=sql-server-ver15&preserve-view=true)
 - [Create a miniCRAN repo](../machine-learning/package-management/create-a-local-package-repository-using-minicran.md?view=sql-server-ver15&preserve-view=true)
 - [Tips for using R packages](../machine-learning/package-management/tips-for-using-r-packages.md?view=sql-server-ver15&preserve-view=true)

### Monitor

 - [Monitor using SSMS reports](../machine-learning/administration/monitor-sql-server-machine-learning-services-using-custom-reports-management-studio.md?view=sql-server-ver15&preserve-view=true)
 - [Monitor using DMVs](../machine-learning/administration/monitor-sql-server-machine-learning-services-using-dynamic-management-views.md?view=sql-server-ver15&preserve-view=true)

 - [Monitor using extended events](../machine-learning/administration/extended-events.md?view=sql-server-ver15&preserve-view=true)
 - [Monitor PREDICT T-SQL](../machine-learning/administration/extended-events-predict-tsql.md?view=sql-server-ver15&preserve-view=true)

### Security

 - [Give users permission](../machine-learning/security/user-permission.md?view=sql-server-ver15&preserve-view=true)

## Spark Machine Learning

 - [Use Spark Machine Learning](spark-machine-learning.md)
 - [Data wrangling using PROSE Code Accelerator](use-prose-for-big-data-automation.md)
 - [Spark machine learning models with MLeap](spark-create-machine-learning-model.md)

## Next steps

 - [Run simple Python scripts](../machine-learning/tutorials/quickstart-python-create-script.md?toc=/sql/toc.json)
 - [Train and score a predictive model in Python](../machine-learning/tutorials/quickstart-python-train-score-model.md?toc=/sql/toc.json)
 - [Run simple R scripts](../machine-learning/tutorials/quickstart-r-create-script.md?toc=/sql/toc.json)
 - [Train and score a predictive model in R](../machine-learning/tutorials/quickstart-r-train-score-model.md?toc=/sql/toc.json)
