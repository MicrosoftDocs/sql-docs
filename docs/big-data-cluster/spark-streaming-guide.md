---
title: SQL Server Big Data Clusters Spark Streaming Guide
titleSuffix: SQL Server Big Data Clusters
description: This guide covers streaming use cases and how to implement it using SQL Server Big Data Clusters capabilities.
author: dacoelho 
ms.author: dacoelho
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 04/06/2021
ms.topic: guide
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server Big Data Clusters Spark Streaming Guide

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This guide covers streaming use cases and how to implement it using SQL Server Big Data Clusters Spark. 

In this guide, you'll learn how to:

> [!div class="checklist"]
> * Load streaming libraries to use with PySpark and Scala Spark.
> * Implement three common streaming patterns using SQL Server Big Data Clusters.

## Prerequisites

* SQL Server Big Data Clusters deployment
* One of these two options:
    * Apache Kafka cluster 2.0+
    * Azure Event Hubs - Namespace and Event Hub

This guide assumes good level of understanding of streaming technology concepts and architectures.
The following articles provide excellent conceptual baselines:

* [Data Architecture Guide - Real time processing](https://docs.microsoft.com/azure/architecture/data-guide/big-data/real-time-processing)
* [Use Azure Event Hubs from Apache Kafka applications](https://docs.microsoft.com/azure/event-hubs/event-hubs-for-kafka-ecosystem-overview)
* [Data Architecture Guide - Choosing a real-time message ingestion technology in Azure](https://docs.microsoft.com/azure/architecture/data-guide/technology-choices/real-time-ingestion)

### Apache Kafka and Azure Event Hub conceptual mapping

|Apache Kafka Concept|Event Hubs Concept|
|--------------------|------------------|
|Cluster|Namespace|
|Topic|Event Hub|
|Partition|Partition|
|Consumer Group|Consumer Group|
|Offset|Offset|

### Reproducibility

This guide leverages the producer application provided by the [Quickstart: Data streaming with Event Hubs using the Kafka protocol](https://docs.microsoft.com/azure/event-hubs/event-hubs-quickstart-kafka-enabled-event-hubs). Furthermore, there are sample applications in many programming languages on [Azure Event Hubs for Apache Kafka Github](https://github.com/Azure/azure-event-hubs-for-kafka) page to help you jumpstart streaming scenarios.

Here is a modified `producer.py` that streams simulated sensor JSON data into Streaming engine using a Kafka compatible client. It is important to notice that Azure Event Hubs is Kafka protocol compatible. Follow the setup instructions in the [Github repository](https://github.com/Azure/azure-event-hubs-for-kafka/tree/master/quickstart/python) to get the sample to work for you. The `conf` dictionary is where all connection information takes place and your mileage may vary depending on your environment. Make sure you replace at least `bootstrap.servers` and `sasl.password` as it is the most relevant configuration in the code sample bellow.

```python
#!/usr/bin/env python
#
# Copyright (c) Microsoft Corporation. All rights reserved.
# Copyright 2016 Confluent Inc.
# Licensed under the MIT License.
# Licensed under the Apache License, Version 2.0
#
# Original Confluent sample modified for use with Azure Event Hubs for Apache Kafka Ecosystems

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

Execute the sample producer application using the following command, replacing `<my-sample-topic>` with your environment information.

```bash
python producer.py <my-sample-topic>
```

## Streaming Scenarios

|Streaming Pattern             |Scenario Description and Implementation         |
|------------------------------|------------------------------------------------|
|Pull from Kafka or Event Hubs |Create a Spark streaming job that pulls data continuously from the streaming engine, performing optional transformations and analytics logic.|
|Sink streaming data into HDFS |In general this correlates with the previous pattern; after the streaming pull and transformation logic, data may be written to a multitude of data locations in order to achieve with the desired data persistence requirement.|
|Push from Spark into Kafka or Event Hubs |After processing by Spark, data may be pushed back into the external streaming engine. There are many scenarios where this is desired, such as real time product recommendation, micro batch fraud and anomaly detection, etc.|

## Sample Spark Streaming application

This sample application implements all three streaming patterns described in the previous section. The application goes about the following:

1. Sets up configuration variables for connecting to the streaming service
1. Creates a spark streaming data frame to pull data
1. Writes aggregated data locally to HDFS
1. Writes aggregated data to a different topic in the streaming service

Here is the complete `sample-spark-streaming-python.py` code:
```python
from pyspark import SparkContext, SparkConf
from pyspark.streaming import StreamingContext
from pyspark.sql import SparkSession
from pyspark.sql.types import *
from pyspark.sql.functions import *

# Sets up batch size to 15 seconds
duration_ms = 15000
# Changes Spark Session into a Structured Streaming context
sc = SparkContext.getOrCreate()
ssc = StreamingContext(sc, duration_ms)
spark = SparkSession(sc)

# Connection Information
bootstrap_servers = "" # Replace!
sasl = "" # Replace!
# Topic we will consume from
topic = "sample-topic"
# Topic we will write toa
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
        # Write to HDFS:
        sensor_df.write.format('parquet').mode('append').saveAsTable('sensor_data')
        # Create a summarization dataframe
        sensor_stats_df = (sensor_df.groupBy('sensor').agg({'measure1':'avg', 'measure2':'avg', 'sensor':'count'}).withColumn('ts', current_timestamp()).withColumnRenamed('avg(measure1)', 'measure1_avg').withColumnRenamed('avg(measure2)', 'measure2_avg').withColumnRenamed('avg(measure1)', 'measure1_avg').withColumnRenamed('count(sensor)', 'count_sensor'))
        # root
        # |-- sensor: string (nullable = true)
        # |-- measure2_avg: double (nullable = true)
        # |-- measure1_avg: double (nullable = true)
        # |-- count_sensor: long (nullable = false)
        # |-- ts: timestamp (nullable = false)
        sensor_stats_df.write.format('parquet').mode('append').saveAsTable('sensor_data_stats')
        # Group by and send metrics to an output kafka topic:
        sensor_stats_df.writeStream
            .format("kafka")
            .option("topic", topic_to)
            .option("kafka.bootstrap.servers", bootstrap_servers)
            .option("kafka.sasl.mechanism", "PLAIN")
            .option("kafka.security.protocol", "SASL_SSL")
            .option("kafka.sasl.jaas.config", sasl)
            .save()
        # For example, you could write to SQL Server
        # df.write.format('com.microsoft.sqlserver.jdbc.spark').mode('append').option('url', url).option('dbtable', datapool_table).save()
        sensor_df.unpersist()


writer = streaming_input_df.writeStream.foreachBatch(foreach_batch_function).start().awaitAnyTermination()

```

Here are the tables that need to be created using __Spark SQL__:
```sql
CREATE TABLE IF NOT EXISTS sensor_data (sensor string, measure1 double, measure2 double)
USING PARQUET;

CREATE TABLE IF NOT EXISTS sensor_data_stats (sensor string, measure2_avg double, measure1_avg double, count_sensor long, ts timestamp)
USING PARQUET;
```


### Copy the application to HDFS

```bash
azdata bdc hdfs cp --from-path sample-spark-streaming-python.py --to-path "hdfs:/apps/ETL-Pipelines/sample-spark-streaming-python.py"
```

### Configuring Kafka libraries

The most important step is setting up your Kafka client libraries to your application before submission.

There are __two required libraries__:

* [kafka-clients](https://mvnrepository.com/artifact/org.apache.kafka/kafka-clients) - Core library that enables Kafka protocol support and connectivity.
* [spark-sql-kafka](https://mvnrepository.com/artifact/org.apache.spark/spark-sql-kafka-0-10) - Enables the Spark SQL data frame functionality on Kafka streams.

Both libraries must comply with the following requirements:

* Target Scala 2.11 and Spark 2.4.7. This is a SQL Server BDC requirement as per CU9 or greater.
* Be compatible with your Streaming server.

> [!CAUTION]
   > As a general rule use the most recent compatible library. The code provided in this guide was tested using Apache Kafka for Azure Event Hubs and is provided as-is, not as a supportability statement. Apache Kafka offers Bidirectional Client Compatibility by design, but library implementations vary across programming languages. Always refer to your Kafka platform documentation to correctly map compatibility.

#### Shared library locations for jobs on HDFS

If multiple applications connect to the same Kafka cluster or your organization has a single versioned Kafka cluster, copy the appropriate library jar files to a shared location on HDFS. Then all jobs should reference the same library files.

Copy the libraries to the common location:

```bash
azdata bdc hdfs cp --from-path kafka-clients-2.7.0.jar --to-path "hdfs:/apps/jars/kafka-clients-2.7.0.jar"
azdata bdc hdfs cp --from-path spark-sql-kafka-0-10_2.11-2.4.7.jar --to-path "hdfs:/apps/jars/spark-sql-kafka-0-10_2.11-2.4.7.jar"
```

#### Dynamically install the libraries

It is possible to dynamically install the packages on job submission using the [package management features](spark-install-packages.md) of SQL Server Big Data Clusters. There is a job startup time penalty due to the recurrent downloads of the library files on each job submission.

### Submit the Spark streaming job using `azdata`

The first example uses the shared library jar files on HDFS:

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/sample-spark-streaming-python.py \
-j '["/apps/jars/kafka-clients-2.7.0.jar","/apps/jars/spark-sql-kafka-0-10_2.11-2.4.7.jar"]' \
--config '{"spark.streaming.concurrentJobs":"5"}' \
-n MyStreamingETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

This example uses the dynamic package management to install the dependencies:

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/sample-spark-streaming-python.py \
--config '{"spark.jars.packages": "org.apache.kafka:kafka-clients:2.7.0,org.apache.spark:spark-sql-kafka-0-10_2.11:2.4.7","spark.streaming.concurrentJobs":"5"}' \
-n MyStreamingETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

## Next steps

To learn how to submit Spark jobs to SQL Server big data cluster using azdata or Livy endpoints, see [Submit Spark jobs using command-line tools](spark-submit-job-cmdline.md).

For more information on SQL Server big data cluster and related scenarios, See [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
