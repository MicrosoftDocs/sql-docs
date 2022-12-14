---
title: "Create, export Spark ML models: MLeap"
titleSuffix: SQL Server Big Data Clusters
description: Use PySpark to train and create machine learning models with Spark on SQL Server Big Data Clusters. Export with MLeap, and then score the model with Java in SQL Server.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf, hudequei
ms.date: 10/05/2021
ms.service: sql
ms.subservice: machine-learning-bdc
ms.topic: conceptual
ms.metadata: seo-lt-2019
---

# Create, export, and score Spark machine learning models on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

The following sample shows how to build a model with [Spark ML](https://spark.apache.org/docs/latest/ml-guide.html), export the model to [MLeap](http://mleap-docs.combust.ml/), and score the model in SQL Server with its [Java Language Extension](../language-extensions/language-extensions-overview.md). This is done in the context of a SQL Server big data cluster.

The following diagram illustrates the work performed in this sample:

![Train score export with spark](./media/spark-create-machine-learning-model/train-score-export-with-spark.png)

## Prerequisites

All files for this sample are located at [https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/spark/sparkml](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/spark/sparkml).

To run the sample, you must also have the following prerequisites:

- A [SQL Server big data cluster](deploy-get-started.md)

- [Big data tools](deploy-big-data-tools.md)
   - **kubectl**
   - **curl**
   - **Azure Data Studio**

## Model training with Spark ML

For this sample, census data (**AdultCensusIncome.csv**) is used to build a Spark ML pipeline model.

1. Use the [mleap_sql_test/setup.sh](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/spark/sparkml/mleap_sql_test/setup.sh) file to download the data set from internet and put it on HDFS in your SQL Server big data cluster. This enables it to be accessed by Spark.

1. Then download the sample notebook [train_score_export_ml_models_with_spark.ipynb](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/spark/sparkml/train_score_export_ml_models_with_spark.ipynb). From a PowerShell or bash command line, run the following command to download the notebook:

   ```PowerShell
   curl -o mssql_spark_connector.ipynb "https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/features/sql-big-data-cluster/spark/sparkml/train_score_export_ml_models_with_spark.ipynb"
   ```

   This notebook contains cells with the required commands for this section of the sample.

1. Open the notebook in Azure Data Studio, and run each code block. For more information about working with notebooks, see [How to use notebooks with SQL Server](../azure-data-studio/notebooks/notebooks-guidance.md).

The data is first read into Spark and split into training and testing data sets. Then the code trains a pipeline model with the training data. Finally, it exports the model to an MLeap bundle.

> [!TIP]
> You can also review or run the Python code associated with these steps outside of the notebook in the [mleap_sql_test/mleap_pyspark.py](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/spark/sparkml/mleap_sql_test/mleap_pyspark.py) file.

## Model scoring with SQL Server

Now that the Spark ML pipeline model is in a common serialization [MLeap bundle](https://github.com/combust/mleap-docs/blob/master/core-concepts/mleap-bundles.md) format, you can score the model in Java without the presence of Spark.

This sample uses the [Java Language Extension](../language-extensions/language-extensions-overview.md) in SQL Server. In order to score the model in SQL Server, you first need to build a Java application that can load the model into Java and score it. You can find the sample code for this Java application in the [mssql-mleap-app folder](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/spark/sparkml/mssql-mleap-app).

After building the sample, you can use Transact-SQL to call the Java application and score the model with a database table. This can be seen in thee [mleap_sql_test/mleap_sql_tests.py](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/spark/sparkml/mleap_sql_test/mleap_sql_tests.py) source file.

## Next steps

For more information about big data clusters, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md)
