---
title: Spark Library Management
titleSuffix: SQL Server big data clusters
description: Spark Library Management
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 01/25/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# Spark library management

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article provides guidance on how to import and install packages for a Spark session through session and notebook configurations.

## Built-in tools
Spark and Hadoop base packages
Python 3.7 and Python 2.7
Pandas, Sklearn, Numpy, and other data processing packages.
R and MRO packages
Sparklyr

## Install packages from a Maven repository onto the Spark cluster at runtime
Maven packages can be installed onto your Spark cluster using notebook cell configuration at the start of your spark session. Before starting a spark session in Azure Data Studio, run the following code:

```
%%configure -f \
{"conf": {"spark.jars.packages": "com.microsoft.azure:azure-eventhubs-spark_2.11:2.3.1"}}
```

## Install Python packages at PySpark job-submission time
1. Specify the path to a requirements.txt file in HDFS to use as a reference for packages to install.
```
%%configure -f \
{"conf": {
    "spark.pyspark.virtualenv.enabled" : "true",
    "spark.pyspark.virtualenv.type" : "conda",
    "spark.pyspark.virtualenv.requirements" : "requirements.txt",
    "spark.pyspark.virtualenv.bin.path" : "/opt/mls/python/bin/conda"
    }, 
"files": ["hdfs://nmnode-0/tmp/requirements.txt"]
}
```
2. Create a conda virtualenv without a requirements file and dynamically add packages during the Spark session.
```
%%configure -f \
{"conf": {
    'spark.pyspark.virtualenv.enabled' : 'true',
    'spark.pyspark.virtualenv.type' : 'conda',
    'spark.pyspark.virtualenv.bin.path' : '/opt/mls/python/bin/conda',
    'spark.pyspark.virtualenv.python_version': '3.6'
 }
 ```

 ```python
sc.install_packages("numpy==1.11.0")
import numpy as np
```

## Import .jar from HDFS for use at runtime
Import jar at runtime through Azure Data Studio notebook cell configuration.

```
%%configure -f
{"conf": {"spark.jars": "/jar/mycodeJar.jar"}}
```

### Import .jar at runtime through Azure Data Studio notebook cell configuration
```
%%configure -f
{"conf": {"spark.jars": "/jar/mycodeJar.jar"}}
```

## Next steps

For more information on SQL Server big data cluster and related scenarios, See [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).