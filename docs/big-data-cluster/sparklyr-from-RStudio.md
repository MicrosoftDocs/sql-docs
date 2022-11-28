---
title: Use sparklyr from RStudio
titleSuffix: SQL Server Big Data Clusters
description: Learn how to use sparklyr in a SQL Server Big Data Cluster to connect to Spark through the R interface.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 10/05/2021
ms.service: sql
ms.subservice: machine-learning-bdc
ms.topic: conceptual
---

# Use sparklyr in SQL Server big data cluster

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Sparklyr provides an R interface for Apache Spark. Sparklyr is a popular way for R developers to use Spark. This article describes how to use sparklyr in a [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] using RStudio.

## Prerequisites

- [Deploy a SQL Server 2019 big data cluster](quickstart-big-data-cluster-deploy.md).

### Install R and RStudio Desktop

Install and configure **RStudio Desktop** with the following steps:

1. If you are running on a Windows client, [download and install R 3.6.3](https://cran.rstudio.com/bin/windows/base/old/3.6.3). Also, [download and install RTools 3.5](https://cran.r-project.org/bin/windows/Rtools/history.html). Make sure to configure RTools binary folder on your PATH environment variable.

    > [!WARNING]
    > R version 4.x and sparklyr versions other that the one specified below are verified not to work as of SQL Server Big Data Clusters CU13.

1. [Download and install RStudio Desktop](https://www.rstudio.com/products/rstudio/download/). Optionally, all samples work on the R shell.

1. After installation completes, run the following commands inside of RStudio Desktop or R shell to install the required packages. When asked, confirm to compile packages from source.

```r
install.packages("devtools")
devtools::install_github('rstudio/sparklyr', ref = 'v1.7.0', upgrade = 'always', repos = 'https://cran.microsoft.com/snapshot/2021-06-11/')
```

## Connect to Spark in a big data cluster

You can use sparklyr to connect from a client to the big data cluster using Livy and the HDFS/Spark gateway. 

In RStudio, create an R script and connect to Spark as in the following example:

> [!TIP]
> For the `<AZDATA_USERNAME>` and `<AZDATA_PASSWORD>` values, use the username and password you set during the big data cluster deployment.

[!INCLUDE [big-data-cluster-root-user](../includes/big-data-cluster-root-user.md)]

For the `<IP>` and `<PORT>` values, see the documentation on [connecting to a big data cluster](connect-to-big-data-cluster.md).

```r
library(sparklyr)
library(dplyr)
library(DBI)

#Specify the Knox username and password
config <- livy_config(user = "<AZDATA_USERNAME>", password = "<AZDATA_PASSWORD>")

httr::set_config(httr::config(ssl_verifypeer = 0L, ssl_verifyhost = 0L))

sc <- spark_connect(master = "https://<IP>:<PORT>/gateway/default/livy/v1",
                    method = "livy",
                    config = config)
```

## Run sparklyr queries

After connecting to Spark, you can run sparklyr. The following example performs a query on the `iris` dataset using sparklyr:

```r
iris_tbl <- copy_to(sc, iris)

iris_count <- dbGetQuery(sc, "SELECT COUNT(*) FROM iris")

iris_count
```

## Distributed R computations

One feature of sparklyr is the ability to [distribute R computations](https://spark.rstudio.com/guides/distributed-r/) with [spark_apply](https://spark.rstudio.com/guides/distributed-r/#apply-an-r-function-to-a-spark-object).

Because big data clusters use Livy connections, you must set `packages = FALSE` in the call to **spark_apply**. For more information, see the [Livy section](https://spark.rstudio.com/guides/distributed-r/#livy) of the sparklyr documentation on distributed R computations. With this setting, you can only use the R packages that are already installed on your Spark cluster in the R code passed to **spark_apply**. The following example demonstrates this functionality:

```r
iris_tbl %>% spark_apply(function(e) nrow(e), names = "nrow", group_by = "Species", packages = FALSE)
```

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
