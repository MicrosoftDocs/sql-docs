---
title: Spark Library Management
titleSuffix: SQL Server Big Data Clusters
description: Spark Library Management
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 12/01/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# Spark library management

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article provides guidance on how to import and install packages for a Spark session through session and notebook configurations.

## Built-in tools

Scala Spark (Scala 2.12) and Hadoop base packages. 

PySpark (Python 3.8). Pandas, Sklearn, Numpy, and other data processing and machine learning packages.

MRO 3.5.2 packages. Sparklyr and SparkR for R Spark workloads.

## Install packages from a Maven repository onto the Spark cluster at runtime

Maven packages can be installed onto your Spark cluster using notebook cell configuration at the start of your spark session. Before starting a spark session in Azure Data Studio, run the following code:

```python
%%configure -f \
{"conf": {"spark.jars.packages": "com.microsoft.azure:azure-eventhubs-spark_2.12:2.3.1"}}
```

### Multiple packages and additional Spark configurations

In the following sample notebook cell, multiple packages are defined.

```python
%%configure -f \
{
    "conf": {
        "spark.jars.packages": "com.microsoft.azure:synapseml_2.12:0.9.4,com.microsoft.azure:azure-eventhubs-spark_2.12:2.3.1",
        "spark.jars.repositories":"https://mmlspark.azureedge.net/maven"
    }
}
```

## Install Python packages at PySpark at runtime

Session and Job level package management guarantees library consistency and isolation. The configuration is a Spark standard library configuration that can be applied on Livy sessions. __azdata spark__ support these configurations. The examples below are presented as __Azure Data Studio Notebooks__ configure cells that need to be run after attaching to a cluster with the PySpark kernel.

If the __"spark.pyspark.virtualenv.enabled" : "true"__ configuration is not set, the session will use the cluster default python and installed libraries.

### Session/Job configuration with requirements.txt

Specify the path to a requirements.txt file in HDFS to use as a reference for packages to install.

```python
%%configure -f \
{
    "conf": {
        "spark.pyspark.virtualenv.enabled" : "true",
        "spark.pyspark.virtualenv.python_version": "3.8",
        "spark.pyspark.virtualenv.requirements" : "hdfs://user/project-A/requirements.txt"
    }
}
```

### Session/Job configuration with different python versions

Create a conda virtualenv without a requirements file and dynamically add packages during the Spark session.

```python
%%configure -f \
{
    "conf": {
        "spark.pyspark.virtualenv.enabled" : "true",
        "spark.pyspark.virtualenv.python_version": "3.7"
    }
}
```

### Library installation

Execute the __sc.install_packages__ to install libraries dynamically in your session. Libraries will be installed into the driver and across all executor nodes.

 ```python
sc.install_packages("numpy==1.11.0")
import numpy as np
```

Is is also possible to install multiple libraries in the same command using an array.

 ```python
sc.install_packages(["numpy==1.11.0", "xgboost"])
import numpy as np
import xgboost as xgb
```

## Import .jar from HDFS for use at runtime
Import jar at runtime through Azure Data Studio notebook cell configuration.

```python
%%configure -f
{"conf": {"spark.jars": "/jar/mycodeJar.jar"}}
```

## Next steps

For more information on SQL Server big data cluster and related scenarios, See [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
