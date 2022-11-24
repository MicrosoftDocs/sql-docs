---
title: "Submit Spark jobs: Command-line tools"
titleSuffix: SQL Server Big Data Clusters
description: Submit Spark jobs on SQL Server Big Data Clusters by using command-line tools.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: MikeRayMSFT
ms.date: 04/01/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# Submit Spark jobs by using command-line tools

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article provides guidance about how to use command-line tools to run Spark jobs on SQL Server Big Data Clusters.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Prerequisites

* [SQL Server 2019 big data tools](deploy-big-data-tools.md) configured and logged in to the cluster:
  * `azdata` 
  * A `curl` application to perform REST API calls to Livy

## Spark jobs that use azdata or Livy

This article provides examples of how to use command-line patterns to submit Spark applications to SQL Server Big Data Clusters.

The Azure Data CLI [`azdata bdc spark` commands](../azdata/reference/reference-azdata-bdc-spark.md) surface all capabilities of SQL Server Big Data Clusters Spark on the command line. This article focuses on job submission. But `azdata bdc spark` also supports interactive modes for Python, Scala, SQL, and R through the `azdata bdc spark session` command.

If you need direct integration with a REST API, use standard Livy calls to submit jobs. This article uses the `curl` command-line tool in the Livy examples to run the REST API call. For a detailed example that shows how to interact with the Spark Livy endpoint by using Python code, see [Use Spark from the Livy endpoint](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/spark/restful-api-access/accessing_spark_via_livy.ipynb) on GitHub.

## Simple ETL that uses Big Data Clusters Spark

This extract, transform, and load (ETL) application follows a common data engineering pattern. It loads tabular data from an Apache Hadoop Distributed File System (HDFS) landing zone path. It then uses a table format to write to an HDFS-processed zone path. 

Download the [sample application's dataset](https://ailab.criteo.com/download-criteo-1tb-click-logs-dataset/). Then create PySpark applications by using PySpark, Spark Scala, or Spark SQL. 

In the following sections, you'll find sample exercises for each solution. Select the tab for your platform. You'll run the application by using `azdata` or `curl`.

### [PySpark](#tab/pyspark)

This example uses the following PySpark application. It's saved as a Python file named *parquet_etl_sample.py* on the local machine.

```python
from pyspark.sql import SparkSession

spark = SparkSession.builder.getOrCreate()

# Read clickstream_data from storage pool HDFS into a Spark data frame. Applies column renames.
df = spark.read.option("inferSchema", "true").csv('/securelake/landing/criteo/test.txt', sep='\t', 
    header=False).toDF("feat1","feat2","feat3","feat4","feat5","feat6","feat7","feat8",
    "feat9","feat10","feat11","feat12","feat13","catfeat1","catfeat2","catfeat3","catfeat4",
    "catfeat5","catfeat6","catfeat7","catfeat8","catfeat9","catfeat10","catfeat11","catfeat12",
    "catfeat13","catfeat14","catfeat15","catfeat16","catfeat17","catfeat18","catfeat19",
    "catfeat20","catfeat21","catfeat22","catfeat23","catfeat24","catfeat25","catfeat26")

# Print the data frame inferred schema
df.printSchema()

tot_rows = df.count()
print("Number of rows:", tot_rows)

# Drop the managed table
spark.sql("DROP TABLE dl_clickstream")

# Write data frame to HDFS managed table by using optimized Delta Lake table format
df.write.format("parquet").mode("overwrite").saveAsTable("dl_clickstream")

print("Sample ETL pipeline completed")
```

#### Copy the PySpark application to HDFS

Store the application in HDFS so the cluster can access it for execution. As a best practice, standardize and govern application locations within the cluster to streamline administration. 

In this example use case, all ETL pipeline applications are stored on the *hdfs:/apps/ETL-Pipelines* path. The sample application is stored at *hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py*.

Run the following command to upload *parquet_etl_sample.py* from the local development or staging machine to the HDFS cluster. 

```bash
azdata bdc hdfs cp --from-path parquet_etl_sample.py  --to-path "hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py"
```

### [Spark Scala](#tab/scala)

This example uses a Spark application written in Scala Spark.

```scala
import org.apache.spark.sql.SparkSession

object ParquetETLSample {
    def main(args: Array[String]) {
        val spark = SparkSession.builder.getOrCreate()
        
        val df = spark.read.
            option("inferSchema", "true").
            option("header", "false").
            option("delimiter", "\t").
            csv("/securelake/landing/criteo/test.txt").
            toDF("feat1","feat2","feat3","feat4","feat5","feat6","feat7","feat8","feat9","feat10","feat11","feat12","feat13","catfeat1","catfeat2","catfeat3","catfeat4","catfeat5","catfeat6","catfeat7","catfeat8","catfeat9","catfeat10","catfeat11","catfeat12","catfeat13","catfeat14","catfeat15","catfeat16","catfeat17","catfeat18","catfeat19","catfeat20","catfeat21","catfeat22","catfeat23","catfeat24","catfeat25","catfeat26")
        
        val tot_rows = df.count()
        println(s"Number of rows: $tot_rows")

        spark.sql("DROP TABLE dl_clickstream")

        df.write.format("parquet").mode("overwrite").saveAsTable("dl_clickstream")

        println("Sample ETL pipeline completed")
        
        spark.stop()
    }
}
```

#### Bundle and copy the Spark application to HDFS

The Spark documentation recommends creating an _assembly JAR_ (or bundle) that contains your application and all of the dependencies. This step is required to submit the application bundle to the cluster for execution. 

Setting up a complete Scala Spark development environment is beyond the scope of this article. For more information, see the Spark documentation for [creating self-contained applications](https://spark.apache.org/docs/latest/quick-start.html#self-contained-applications).

This example assumes that an application JAR bundle named *parquet-etl-sample.jar* is compiled and available. Run the following command to upload the bundle from the local development or staging machine to the HDFS cluster.

```bash
azdata bdc hdfs cp --from-path parquet-etl-sample.jar  --to-path "hdfs:/apps/ETL-Pipelines/parquet-etl-sample.jar"
```

### [Spark SQL](#tab/sql)

This example uses Spark SQL for the ingestion logic. It uses tables and views to provide a SQL-centric approach to ETL.

```sql
DROP VIEW IF EXISTS etl_clickstream;

CREATE TEMPORARY VIEW etl_clickstream
USING CSV
OPTIONS (path "/securelake/landing/criteo/test.txt", header "false", delimiter "\t", mode "FAILFAST");

DROP TABLE IF EXISTS dl_clickstream;

CREATE TABLE dl_clickstream (
    feat1 integer,
    feat2 integer,
    feat3 integer,
    feat4 integer,
    feat5 integer,
    feat6 integer,
    feat7 integer,
    feat8 integer,
    feat9 integer,
    feat10 integer,
    feat11 integer,
    feat12 integer,
    feat13 integer,
    catfeat1 string,
    catfeat2 string,
    catfeat3 string,
    catfeat4 string,
    catfeat5 string,
    catfeat6 string,
    catfeat7 string,
    catfeat8 string,
    catfeat9 string,
    catfeat10 string,
    catfeat11 string,
    catfeat12 string,
    catfeat13 string,
    catfeat14 string,
    catfeat15 string,
    catfeat16 string,
    catfeat17 string,
    catfeat18 string,
    catfeat19 string,
    catfeat20 string,
    catfeat21 string,
    catfeat22 string,
    catfeat23 string,
    catfeat24 string,
    catfeat25 string,
    catfeat26 string
) 
USING PARQUET
AS SELECT * FROM etl_clickstream;
```

#### Copy the Spark SQL application to HDFS

Run the following command to upload the *parquet-etl-sample.sql* file from the local development or staging machine to the HDFS cluster.

```bash
azdata bdc hdfs cp --from-path parquet-etl-sample.sql --to-path "hdfs:/apps/ETL-Pipelines/parquet-etl-sample.sql"
```

---

#### Run the Spark application

Use the following command to submit the application to SQL Server Big Data Clusters Spark for execution.


# [PySpark and azdata](#tab/azdata/pyspark)

The `azdata` command runs the application by using commonly specified parameters. For complete parameter options for `azdata bdc spark batch create`, see [`azdata bdc spark`](../azdata/reference/reference-azdata-bdc-spark.md).

This application requires the `spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation` configuration parameter. So the command uses the `--config` option. This setup shows how to pass configurations into the Spark session. 

You can use the `--config` option to specify multiple configuration parameters. You could also specify them inside the application session by setting the configuration in the `SparkSession` object.

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py \
--config '{"spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m
```
> [!WARNING]
> The "name" or "n" parameter for batch name should be unique each time a new batch is created.

# [PySpark and curl, using Livy](#tab/curl/pyspark)

The `curl` command runs the application by using Livy. Replace `USER`, `PASSWORD`, and `LIVY_ENDPOINT` to reflect your environment.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches \
-H 'Content-Type: application/json; charset=utf-8' \
--data-binary @- << EOF
{
    "file": "/apps/ETL-Pipelines/parquet_etl_sample.py",
    "name": "MyETLPipelinePySpark",
    "numExecutors": 2,
    "executorCores": 2,
    "executorMemory": "1664m",
    "conf": {
        "spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"
    }
}
EOF
```
> [!WARNING]
> The "name" parameter should be unique each time a new batch is created.

# [Scala and azdata](#tab/azdata/scala)

The `azdata` command runs the application by using commonly specified parameters. For complete parameter options for `azdata bdc spark batch create`, see [`azdata bdc spark`](../azdata/reference/reference-azdata-bdc-spark.md).

The application requires the `spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation` configuration parameter. So the command uses the `--config` option. This setup shows how to pass configurations into the Spark session. 

You can use the `--config` option to specify multiple configuration parameters. You could also specify them inside the application session by setting the configuration in the `SparkSession` object.

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/parquet-etl-sample.jar \
--class "ParquetETLSample" \
--config '{"spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyETLPipeline --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

> [!WARNING]
> The "name" or "n" parameter for batch name should be unique each time a new batch is created.

# [Scala and curl, using Livy](#tab/curl/scala)

The `curl` command runs the application by using Livy. Replace `USER`, `PASSWORD`, and `LIVY_ENDPOINT` to reflect your environment.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches \
-H 'Content-Type: application/json; charset=utf-8' \
--data-binary @- << EOF
{
    "file": "/apps/ETL-Pipelines/parquet-etl-sample.jar",
    "class": "ParquetETLSample",
    "name": "MyETLPipeline",
    "numExecutors": 2,
    "executorCores": 2,
    "executorMemory": "1664m",
    "conf": {
        "spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"
    }
}
EOF
```
> [!WARNING]
> The "name" parameter for batch name should be unique each time a new batch is created.

# [SQL and azdata](#tab/azdata/sql)

The `azdata` command runs the application by using commonly specified parameters. For complete parameter options for `azdata bdc spark batch create`, see [`azdata bdc spark`](../azdata/reference/reference-azdata-bdc-spark.md).

Like the PySpark example, this application also requires the `spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation` configuration parameter. So the command uses the `--config` option. This setup shows how to pass configurations into the Spark session. 

You can use the `--config` option to specify multiple configuration parameters. You could also specify them inside the application session by setting the configuration in the `SparkSession` object.

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/parquet_etl_sample.sql \
--config '{"spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyETLPipelineSQL --executor-count 2 --executor-cores 2 --executor-memory 1664m
```
> [!WARNING]
> The "name" or "n" parameter for batch name should be unique each time a new batch is created.

# [SQL and curl, using Livy](#tab/curl/sql)

The `curl` command runs the application by using Livy. Replace `USER`, `PASSWORD`, and `LIVY_ENDPOINT` to reflect your environment.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches \
-H 'Content-Type: application/json; charset=utf-8' \
--data-binary @- << EOF
{
    "file": "/apps/ETL-Pipelines/parquet_etl_sample.sql",
    "name": "MyETLPipelineSQL",
    "numExecutors": 2,
    "executorCores": 2,
    "executorMemory": "1664m",
    "conf": {
        "spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"
    }
}
EOF
```
> [!WARNING]
> The "name" parameter should be unique each time a new batch is created.

---

## Monitor Spark jobs

The [`azdata bdc spark batch` commands](../azdata/reference/reference-azdata-bdc-spark.md) provide management actions for Spark batch jobs. 

To _list all running jobs_, run the following command.

* The `azdata` command:

    ```bash
    azdata bdc spark batch list -o table
    ```
    
* The `curl` command, using Livy:

    ```bash
    curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches
    ```
    
To _get information_ for a Spark batch with the given ID, run the following command. The `batch id` is returned from `spark batch create`.

* The `azdata` command:

    ```bash
    azdata bdc spark batch info --batch-id 0
    ```
    
* The `curl` command, using Livy:

    ```bash
    curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>
    ```
    
To _get state information_ for a Spark batch with the given ID, run the following command.

* The `azdata` command:

    ```bash
    azdata bdc spark batch state --batch-id 0
    ```
    
* The `curl` command, using Livy:

    ```bash
    curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>/state
    ```
    
To _get the logs_ for a Spark batch with the given ID, run the following command.

* The `azdata` command:

    ```bash
    azdata bdc spark batch log --batch-id 0
    ```
    
* The `curl` command, using Livy:

    ```bash
    curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>/log
    ```
    
## Next steps

For information about troubleshooting Spark code, see [Troubleshoot a PySpark notebook](troubleshoot-pyspark-notebook.md).

Comprehensive Spark sample code is available at [SQL Server Big Data Clusters Spark samples](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/spark) on GitHub.

For more information about SQL Server Big Data Clusters and related scenarios, see [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
