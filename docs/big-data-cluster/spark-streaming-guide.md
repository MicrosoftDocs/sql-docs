---
title: SQL Server Big Data Clusters Spark Streaming guide
titleSuffix: SQL Server Big Data Clusters
description: This guide covers streaming use cases and how to stream by using SQL Server Big Data Clusters capabilities.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: guide
ms.metadata: seo-lt-2019
---

# SQL Server Big Data Clusters Spark Streaming guide

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This guide covers streaming use cases and how to implement them by using SQL Server Big Data Clusters Spark.

In this guide, you'll learn how to:

> [!div class="checklist"]
> * Load streaming libraries to use with PySpark and Scala Spark.
> * Implement three common streaming patterns by using SQL Server Big Data Clusters.

## Prerequisites

* A SQL Server Big Data Clusters deployment
* One of these options:
    * Apache Kafka cluster 2.0 or later
    * An Azure Event Hubs namespace and event hub

This guide assumes a good level of understanding about streaming technology concepts and architectures.
The following articles provide excellent conceptual baselines:

* [Data architecture guide - Real-time processing](/azure/architecture/data-guide/big-data/real-time-processing)
* [Use Azure Event Hubs from Apache Kafka applications](/azure/event-hubs/event-hubs-for-kafka-ecosystem-overview)
* [Data architecture guide - Choose a real-time message ingestion technology in Azure](/azure/architecture/data-guide/technology-choices/real-time-ingestion)

### Apache Kafka and Azure Event Hubs conceptual mapping

|Apache Kafka concept|Event Hubs concept|
|--------------------|------------------|
|Cluster|Namespace|
|Topic|Event hub|
|Partition|Partition|
|Consumer group|Consumer group|
|Offset|Offset|

### Reproducibility

This guide uses the producer application provided in [Quickstart: Data streaming with Event Hubs by using the Kafka protocol](/azure/event-hubs/event-hubs-quickstart-kafka-enabled-event-hubs). You can find sample applications in many programming languages at [Azure Event Hubs for Apache Kafka](https://github.com/Azure/azure-event-hubs-for-kafka) on GitHub. Use these applications to jump-start streaming scenarios.

> [!NOTE]
> One of the steps accomplished by the quickstart is that the Kafka streaming option is enabled when creating the Azure Event Hub. Confirm that the Kafka endpoint for the Azure Event Hub namespace is enabled.

The following modified `producer.py` code streams simulated sensor JSON data into the streaming engine by using a Kafka-compatible client. Notice that Azure Event Hubs is compatible with the Kafka protocol. Follow the [setup instructions](https://github.com/Azure/azure-event-hubs-for-kafka/tree/master/quickstart/python) in GitHub to make the sample work for you. 

All connection information is in the `conf` dictionary. Your setup might differ depending on your environment. Replace at least `bootstrap.servers` and `sasl.password`. These settings are the most relevant in the following code sample.

```python
#!/usr/bin/env python
#
# Copyright (c) Microsoft Corporation. All rights reserved.
# Copyright 2016 Confluent Inc.
# Licensed under the MIT License.
# Licensed under the Apache License, version 2.0.
#
# Original Confluent sample modified for use with Azure Event Hubs for Apache Kafka ecosystems.

from confluent_kafka import Producer
import sys
import random
import time
import json

sensors = ["Sensor 1", "Sensor 2", "Sensor 3"]

if __name__ == '__main__':
    if len(sys.argv) != 2:
        sys.stderr.write('Usage: %s <topic>\n' % sys.argv[0])
        sys.exit(1)
    topic = sys.argv[1]

    # Producer configuration
    # See https://github.com/edenhill/librdkafka/blob/master/CONFIGURATION.md
    # See https://github.com/edenhill/librdkafka/wiki/Using-SSL-with-librdkafka#prerequisites for SSL issues
    conf = {
        'bootstrap.servers': '',                     #replace!
        'security.protocol': 'SASL_SSL',
        'ssl.ca.location': '/usr/lib/ssl/certs/ca-certificates.crt',
        'sasl.mechanism': 'PLAIN',
        'sasl.username': '$ConnectionString',
        'sasl.password': '',                         #replace!
        'client.id': 'python-sample-producer'
    }

    # Create Producer instance
    p = Producer(**conf)

    def delivery_callback(err, msg):
        if err:
            sys.stderr.write('%% Message failed delivery: %s\n' % err)
        else:
            sys.stderr.write('%% Message delivered to %s [%d] @ %o\n' % (msg.topic(), msg.partition(), msg.offset()))

    # Simulate stream
    for i in range(0, 10000):
        try:
            payload = {
                'sensor': random.choice(sensors),
                'measure1': random.gauss(37, 7),
                'measure2': random.random(),
            }
            p.produce(topic, json.dumps(payload).encode('utf-8'), callback=delivery_callback)
            #p.produce(topic, str(i), callback=delivery_callback)
        except BufferError as e:
            sys.stderr.write('%% Local producer queue is full (%d messages awaiting delivery): try again\n' % len(p))
        p.poll(0)
        time.sleep(2)

    # Wait until all messages have been delivered
    sys.stderr.write('%% Waiting for %d deliveries\n' % len(p))
    p.flush()

```

Run the sample producer application by using the following command. Replace `<my-sample-topic>` with your environment information.

```bash
python producer.py <my-sample-topic>
```

## Streaming scenarios

|Streaming pattern             |Scenario description and implementation         |
|------------------------------|------------------------------------------------|
|Pull from Kafka or Event Hubs |Create a Spark Streaming job that pulls data continuously from the streaming engine, performing optional transformations and analytics logic.|
|Sink streaming data into Apache Hadoop Distributed File System (HDFS) |In general, this pattern correlates with the previous pattern. After the streaming pull and transformation logic, data can be written to many locations to achieve the desired data persistence requirement.|
|Push from Spark into Kafka or Event Hubs |After processing by Spark, data can be pushed back into the external streaming engine. This pattern is desirable in many scenarios, such as real-time product recommendations and micro-batch fraud and anomaly detection.|

## Sample Spark Streaming application

This sample application implements the three streaming patterns described in the previous section. The application:

1. Sets up configuration variables to connect to the streaming service.
1. Creates a Spark Streaming data frame to pull data.
1. Writes aggregated data locally to HDFS.
1. Writes aggregated data to a different topic in the streaming service.

Here's the complete `sample-spark-streaming-python.py` code:
```python
from pyspark import SparkContext, SparkConf
from pyspark.streaming import StreamingContext
from pyspark.sql import SparkSession
from pyspark.sql.types import *
from pyspark.sql.functions import *

# Sets up batch size to 15 seconds
duration_ms = 15000
# Changes Spark session into a structured streaming context
sc = SparkContext.getOrCreate()
ssc = StreamingContext(sc, duration_ms)
spark = SparkSession(sc)

# Connection information
bootstrap_servers = "" # Replace!
sasl = "" # Replace!
# Topic we will consume from
topic = "sample-topic"
# Topic we will write to
topic_to = "sample-topic-processed"

# Define the schema to speed up processing
jsonSchema = StructType([StructField("sensor", StringType(), True), StructField("measure1", DoubleType(), True), StructField("measure2", DoubleType(), True)])

streaming_input_df = (
    spark.readStream \
    .format("kafka") \
    .option("subscribe", topic) \
    .option("kafka.bootstrap.servers", bootstrap_servers) \
    .option("kafka.sasl.mechanism", "PLAIN") \
    .option("kafka.security.protocol", "SASL_SSL") \
    .option("kafka.sasl.jaas.config", sasl) \
    .option("kafka.request.timeout.ms", "60000") \
    .option("kafka.session.timeout.ms", "30000") \
    .option("failOnDataLoss", "true") \
    .option("startingOffsets", "latest") \
    .load()
)

def foreach_batch_function(df, epoch_id):
    # Transform and write batchDF
    if df.count() <= 0:
        None
    else:
        # Create a data frame to be written to HDFS
        sensor_df = df.selectExpr('CAST(value AS STRING)').select(from_json("value", jsonSchema).alias("value")).select("value.*")
        # root
        #  |-- sensor: string (nullable = true)
        #  |-- measure1: double (nullable = true)
        #  |-- measure2: double (nullable = true)
        sensor_df.persist()
        # Write to HDFS
        sensor_df.write.format('parquet').mode('append').saveAsTable('sensor_data')
        # Create a summarization data frame
        sensor_stats_df = (sensor_df.groupBy('sensor').agg({'measure1':'avg', 'measure2':'avg', 'sensor':'count'}).withColumn('ts', current_timestamp()).withColumnRenamed('avg(measure1)', 'measure1_avg').withColumnRenamed('avg(measure2)', 'measure2_avg').withColumnRenamed('avg(measure1)', 'measure1_avg').withColumnRenamed('count(sensor)', 'count_sensor'))
        # root
        # |-- sensor: string (nullable = true)
        # |-- measure2_avg: double (nullable = true)
        # |-- measure1_avg: double (nullable = true)
        # |-- count_sensor: long (nullable = false)
        # |-- ts: timestamp (nullable = false)
        sensor_stats_df.write.format('parquet').mode('append').saveAsTable('sensor_data_stats')
        # Group by and send metrics to an output Kafka topic
        sensor_stats_df.writeStream \
            .format("kafka") \
            .option("topic", topic_to) \ 
            .option("kafka.bootstrap.servers", bootstrap_servers) \
            .option("kafka.sasl.mechanism", "PLAIN") \
            .option("kafka.security.protocol", "SASL_SSL") \
            .option("kafka.sasl.jaas.config", sasl) \
            .save()
        # For example, you could write to SQL Server
        # df.write.format('com.microsoft.sqlserver.jdbc.spark').mode('append').option('url', url).option('dbtable', datapool_table).save()
        sensor_df.unpersist()


writer = streaming_input_df.writeStream.foreachBatch(foreach_batch_function).start().awaitTermination()

```

Create the following tables by using Spark SQL. The PySpark kernel in an Azure Data Studio notebook is one way to run Spark SQL interactively. In a new notebook in Azure Data Studio, connect to the Spark pool of your big data cluster. Choose the PySpark kernel, and execute the following: 

```python
%%sql
CREATE TABLE IF NOT EXISTS sensor_data (sensor string, measure1 double, measure2 double)
USING PARQUET;

CREATE TABLE IF NOT EXISTS sensor_data_stats (sensor string, measure2_avg double, measure1_avg double, count_sensor long, ts timestamp)
USING PARQUET;
```


### Copy the application to HDFS

```bash
azdata bdc hdfs cp --from-path sample-spark-streaming-python.py --to-path "hdfs:/apps/ETL-Pipelines/sample-spark-streaming-python.py"
```

### Configure Kafka libraries

Set up your Kafka client libraries with your application before you submit the jobs. Two libraries are required:

* [kafka-clients](https://mvnrepository.com/artifact/org.apache.kafka/kafka-clients) - This core library enables Kafka protocol support and connectivity.
* [spark-sql-kafka](https://mvnrepository.com/artifact/org.apache.spark/spark-sql-kafka-0-10) - This library enables the Spark SQL data frame functionality on Kafka streams.

Both libraries must:

* Target Scala 2.12 and Spark 3.1.2. This SQL Server Big Data Cluster requirement is for Cumulative Update  13 (CU13) or later.
* Be compatible with your Streaming server.

> [!CAUTION]
   > As a general rule, use the most recent compatible library. The code in this guide was tested by using Apache Kafka for Azure Event Hubs. The code is provided as-is, not as a statement of supportability. 
   > 
   > Apache Kafka offers bidirectional client compatibility by design. But library implementations vary across programming languages. Always refer to your Kafka platform documentation to correctly map compatibility.

#### Share library locations for jobs on HDFS

If multiple applications connect to the same Kafka cluster, or if your organization has a single versioned Kafka cluster, copy the appropriate library JAR files to a shared location on HDFS. Then all jobs should reference the same library files.

Copy the libraries to the common location:

```bash
azdata bdc hdfs cp --from-path kafka-clients-2.7.0.jar --to-path "hdfs:/apps/jars/kafka-clients-3.0.0.jar"
azdata bdc hdfs cp --from-path spark-sql-kafka-0-10_2.11-2.4.7.jar --to-path "hdfs:/apps/jars/spark-sql-kafka-0-10_2.12-3.1.2.jar"
```

#### Dynamically install the libraries

You can dynamically install packages when you submit a job by using the [package management features](spark-install-packages.md) of SQL Server Big Data Clusters. There's a job startup time penalty because of the recurrent downloads of the library files on each job submission.

### Submit the Spark Streaming job by using azdata

The following example uses the shared library JAR files on HDFS:

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/sample-spark-streaming-python.py \
-j '["/apps/jars/kafka-clients-3.0.0.jar","/apps/jars/spark-sql-kafka-0-10_2.12-3.1.2.jar"]' \
--config '{"spark.streaming.concurrentJobs":"3","spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyStreamingETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

This example uses dynamic package management to install the dependencies:

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/sample-spark-streaming-python.py \
--config '{"spark.jars.packages": "org.apache.kafka:kafka-clients:3.0.0,org.apache.spark:spark-sql-kafka-0-10_2.12:3.1.2","spark.streaming.concurrentJobs":"3","spark.sql.legacy.allowCreatingManagedTableUsingNonemptyLocation":"true"}' \
-n MyStreamingETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

## Next steps

To submit Spark jobs to SQL Server Big Data Clusters by using `azdata` or Livy endpoints, see [Submit Spark jobs by using command-line tools](spark-submit-job-command-line.md).

For more information about SQL Server Big Data Clusters and related scenarios, see [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
