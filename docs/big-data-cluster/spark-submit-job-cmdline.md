---
title: "Submit Spark jobs: command line"
titleSuffix: SQL Server big data clusters
description: Submit Spark jobs on SQL Server big data cluster using command line tools.
author: dacoelho
ms.author: dacoelho
ms.reviewer: MikeRayMSFT
ms.date: 04/01/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# Submit Spark jobs using command-line tools

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article provides guidance on how to use command line tools to execute Spark jobs on SQL Server Big Data Clusters.

## Prerequisites

* [SQL Server 2019 big data tools](deploy-big-data-tools.md) configured and logged into the cluster:
  * **azdata** 
  * **curl** application to perform REST API calls to Livy

## Spark Jobs using __azdata__ or Livy

This article provides usage examples of command line patterns to submit Spark applications to SQL Server Big Data Clusters.

The Azure Data CLI [`azdata bdc spark` commands](../azdata/reference/reference-azdata-bdc-spark.md) surfaces all capabilities of SQL Server Big Data Clusters Spark on the command line. Whilst this guide focus on job submission, `azdata bdc spark` also support interactive modes for Python, Scala, SQL and R through the `azdata bdc spark session` command.

If direct integration to a REST API is desired, use standard Livy calls to submit jobs. We will use the `curl` command line tool in the Livy examples to execute the REST API call. For a detailed example on how to interact with the Spark Livy endpoint using Python code, see ["Using Spark from Livy end point"](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/spark/restful-api-access/accessing_spark_via_livy.ipynb) on Github.

## Simple ETL using SQL Server BDC Spark

This application exemplifies a common Data Engineering pattern, loading tabular data from a HDFS landing zone path and then writing using a table format to a HDFS processed zone path. The dataset used in this sample application can be downloaded [here](https://ailab.criteo.com/download-criteo-1tb-click-logs-dataset/).

### [PySpark](#tab/pyspark)

In this example we will use the following PySpark application saved as a python file named ```parquet_etl_sample.py``` in the local machine.

```python
from pyspark.sql import SparkSession

spark = SparkSession.builder.getOrCreate()

# Read clickstream_data from Storage Pool HDFS into a Spark data frame. Applies column renames.
df = spark.read.option("inferSchema", "true").csv('/securelake/landing/criteo/test.txt', sep='\t', 
    header=False).toDF("feat1","feat2","feat3","feat4","feat5","feat6","feat7","feat8",
    "feat9","feat10","feat11","feat12","feat13","catfeat1","catfeat2","catfeat3","catfeat4",
    "catfeat5","catfeat6","catfeat7","catfeat8","catfeat9","catfeat10","catfeat11","catfeat12",
    "catfeat13","catfeat14","catfeat15","catfeat16","catfeat17","catfeat18","catfeat19",
    "catfeat20","catfeat21","catfeat22","catfeat23","catfeat24","catfeat25","catfeat26")

# Prints the data frame inferred schema:
df.printSchema()

tot_rows = df.count()
print("Number of rows:", tot_rows)

# Drop the managed table
spark.sql("DROP TABLE dl_clickstream")

# Write data frame to HDFS managed table using optimized Delta Lake table format
df.write.format("parquet").mode("overwrite").saveAsTable("dl_clickstream")

print("Sample ETL pipeline completed")
```

#### Copy the PySpark application to HDFS

The application needs to be stored in HDFS so the cluster has access to it for execution. It is best practice to standardize and govern application locations within the cluster to streamline administration. In this example use case, all ETL pipeline applications would be stored on `hdfs:/apps/ETL-Pipelines` path. The sample application will be stored at `hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py`.

Run the following command to upload __`parquet_etl_sample.py`__ from the local development or staging machine to the HDFS cluster. 

```bash
azdata bdc hdfs cp --from-path parquet_etl_sample.py  --to-path "hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py"
```

#### Execute the PySpark application

Use the following command to submit the application to SQL Server BDC Spark for execution.

##### [azdata](#tab/pyspark/azdata)

This is the __`azdata`__ command that executes this application using commonly specified parameters. For complete parameter options for `azdata bdc spark batch create`, see [`azdata bdc spark`](../azdata/reference/reference-azdata-bdc-spark.md).

This application requires the `spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation` Spark config parameter, so the command is using the `--config` option. This was deliberate to exemplify how to pass configurations into the Spark session. You may use the `--config` option to specify multiple configuration parameters. This could also be achieved inside the application session setting the configuration in the SparkSession object.

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py \
--config '{"spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

##### [curl using Livy](#tab/pyspark/curl)

This is the __`curl`__ command that executes this application using Livy. Make sure to replace USER, PASSWORD and LIVY_ENDPOINT to reflect your environment.

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
---

### [Spark Scala](#tab/scala)

In this example we will use the following Spark application written in Scala Spark.

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

#### Bundle and Copy the Spark application to HDFS

The Spark documentation recommends creating an __assembly JAR__ (or bundle) containing your application and all of the dependencies. This is a required step in order to submit the application bundle to the cluster for execution. Setting up a complete Scala Spark development environment is beyond the scope of this guide, see the official [Spark documentation on creating self-contained applications](https://spark.apache.org/docs/latest/quick-start.html#self-contained-applications) for more information.

This example assumes that an application jar bundle named `parquet-etl-sample.jar` is compiled and available. Run the following command to upload the bundle from the local development or staging machine to the HDFS cluster.

```bash
azdata bdc hdfs cp --from-path parquet-etl-sample.jar  --to-path "hdfs:/apps/ETL-Pipelines/parquet-etl-sample.jar"
```

#### Execute the Spark Scala application

Use the following command to submit the application to SQL Server BDC Spark for execution.

##### [azdata](#tab/scala/azdata)

This is the __`azdata`__ command that executes this application using commonly specified parameters. For complete parameter options for `azdata bdc spark batch create`, see [`azdata bdc spark`](../azdata/reference/reference-azdata-bdc-spark.md).

This application requires the `spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation` Spark config parameter, so the command is using the `--config` option. This was deliberate to exemplify how to pass configurations into the Spark session. You may use the `--config` option to specify multiple configuration parameters. This could also be achieved inside the application session setting the configuration in the SparkSession object.

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/parquet-etl-sample.jar \
--class "ParquetETLSample" \
--config '{"spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyETLPipeline --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

##### [curl using Livy](#tab/scala/curl)

This is the __`curl`__ command that executes this application using Livy. Make sure to replace USER, PASSWORD and LIVY_ENDPOINT to reflect your environment.

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
---

### [Spark SQL](#tab/sql)

This example uses Spark SQL to perform the ingestion logic using tables and views to provide a SQL centric approach to ETL.

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

Run the following command to upload the __```parquet-etl-sample.sql```__ from the local development or staging machine to the HDFS cluster.

```bash
azdata bdc hdfs cp --from-path parquet-etl-sample.sql --to-path "hdfs:/apps/ETL-Pipelines/parquet-etl-sample.sql"
```

#### Execute the Spark SQL application

Use the following command to submit the application to SQL Server BDC Spark for execution.

##### [azdata](#tab/sql/azdata)

This is the __`azdata`__ command that executes this application using commonly specified parameters. For complete parameter options for `azdata bdc spark batch create`, see [`azdata bdc spark`](../azdata/reference/reference-azdata-bdc-spark.md).

Like the PySpark example, this application also requires the `spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation` Spark config parameter, so the command is using the `--config` option. This was deliberate to exemplify how to pass configurations into the Spark session. You may use the `--config` option to specify multiple configuration parameters. This could also be achieved inside the application session setting the configuration in the SparkSession object.

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/parquet_etl_sample.sql \
--config '{"spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyETLPipelineSQL --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

##### [curl using Livy](#tab/sql/curl)

This is the __`curl`__ command that executes this application using Livy. Make sure to replace USER, PASSWORD and LIVY_ENDPOINT to reflect your environment.

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
---
---

## Monitoring Spark jobs

The [`azdata bdc spark batch` commands](../azdata/reference/reference-azdata-bdc-spark.md) contains management actions for Spark batch jobs.

In order to __list all running jobs__, execute the following command.

This is the `azdata` command:

```bash
azdata bdc spark batch list -o table
```

This is the `curl` command using Livy.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches
```

In order to __get information__ for a Spark batch with the given ID execute the following command. The batch id is returned from 'spark batch create'.

This is the `azdata` command:

```bash
azdata bdc spark batch info --batch-id 0
```

This is the `curl` command using Livy.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>
```

In order to __get state information__ for a Spark batch with the given ID execute the following command.

This is the `azdata` command:

```bash
azdata bdc spark batch state --batch-id 0
```

This is the `curl` command using Livy.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>/state
```

In order to __get the logs__ for a Spark batch with the given ID execute the following command.

This is the `azdata` command:

```bash
azdata bdc spark batch log --batch-id 0
```

This is the `curl` command using Livy.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>/log
```

## Next steps

For more information on troubleshooting Spark code, see [Troubleshoot pyspark notebook](troubleshoot-pyspark-notebook.md).

A comprehensive set of Spark sample code is available on [SQL Server big data clusters Spark samples](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/spark) on Github.

For more information on SQL Server big data cluster and related scenarios, see [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
