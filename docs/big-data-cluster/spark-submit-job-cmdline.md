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

This section provides usage examples of command line patterns to submit Spark applications to SQL Server Big Data Clusters.

The Azure Data CLI [__```azdata bdc spark```__ commands](../azdata/reference/reference-azdata-bdc-spark.md) surfaces all capabilities of SQL Server Big Data Clusters Spark on the command line.

If direct integration to a REST API is desired, use standard Livy calls to submit jobs. We will use the ```curl``` command line tool in the Livy examples to execute the REST API call. For a detailed example on how to interact with the Spark Livy endpoint using Python code, see ["Using Spark from Livy end point"](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/spark/restful-api-access/accessing_spark_via_livy.ipynb) on Github.

## PySpark Application

In this example we will use the following PySpark application saved as a python file named ```parquet_etl_sample.py``` in the local machine. This application exemplifies a common Data Engineering pattern, loading tabular data from a HDFS landing zone path and then writing as a table format to a HDFS processed zone path. The dataset used in this sample application can be downloaded [here](https://ailab.criteo.com/download-criteo-1tb-click-logs-dataset/).

```python
from pyspark.sql import SparkSession

spark = SparkSession\
    .builder\
    .getOrCreate()

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

### Copy the PySpark Application to HDFS

The application needs to be stored in HDFS so the cluster has access to it for execution. It is best practice to standardize and govern the application locations within the cluster to streamline administration. In this example use case, all applications would be stored on ```hdfs:/apps/ETL-Pipelines``` path. The sample application will be stored at __```hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py```__.

Run the following command to upload __```parquet_etl_sample.py```__ from the local development or staging machine to the HDFS cluster. 

```bash
azdata bdc hdfs cp --from-path parquet_etl_sample.py  --to-path "hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py"
```

### Execute the PySpark Application

This is the __```azdata```__ command that executes this application using commonly specified parameters. For complete parameter options, check [__```azdata bdc spark batch create```__](../azdata/reference/reference-azdata-bdc-spark.md) reference documentation.

This application requires the ```spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation``` Spark config parameter, so the command is using the ```--config``` option. This was deliberate to exemplify how to pass configurations into the Spark session. You may use the ```--config``` option to specify multiple configuration parameters. This could also be achieved inside the application session setting the configuration in the SparkSession object.

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py \
--config '{"spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyETLPipeline --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

This is the __```curl```__ command that executes this application using Livy. Make sure you replace USER, PASSWORD and LIVY_ENDPOINT to reflect your environment.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches \
-H 'Content-Type: application/json; charset=utf-8' \
--data-binary @- << EOF
{
    "file": "/apps/ETL-Pipelines/parquet_etl_sample.py",
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

## Monitoring Spark Jobs

The [__```azdata bdc spark batch```__ commands](../azdata/reference/reference-azdata-bdc-spark.md) contains management actions for Spark Job batch management.

In order to __list all running jobs__, execute the following command.

This is the __```azdata```__ command:

```bash
azdata bdc spark batch list -o table
```

This is the __```curl```__ command using Livy.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches
```

In order to __get information__ for a Spark batch with the given ID execute the following command. The batch id is returned from 'spark batch create'.

This is the __```azdata```__ command:

```bash
azdata bdc spark batch info --batch-id 0
```

This is the __```curl```__ command using Livy.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>
```

In order to __get state information__ for a Spark batch with the given ID execute the following command.

This is the __```azdata```__ command:

```bash
azdata bdc spark batch state --batch-id 0
```

This is the __```curl```__ command using Livy.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>/state
```

In order to __get the logs__ for a Spark batch with the given ID execute the following command.

This is the __```azdata```__ command:

```bash
azdata bdc spark batch log --batch-id 0
```

This is the __```curl```__ command using Livy.

```bash
curl -k -u <USER>:<PASSWORD> -X POST <LIVY_ENDPOINT>/batches/<BATCH_ID>/log
```

## Next steps

For more information on troubleshooting Spark code, see [Troubleshoot pyspark notebook](troubleshoot-pyspark-notebook.md).

A comprehensive set of Spark sample code is available on ["SQL Server big data clusters Spark samples"](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/spark) on Github.

For more information on SQL Server big data cluster and related scenarios, see [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
