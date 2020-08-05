---
title: Use sparklyr from RStudio
titleSuffix: SQL Server big data clusters
description: Learn how to use sparklyr in a SQL Server 2019 Big Data Clusters to connect to Spark through the R interface.
author: jejiang
ms.author: jejiang
ms.reviewer: mikeray
ms.date: 06/22/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: machine-learning-bdc
---

# Use sparklyr in SQL Server big data cluster

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

Sparklyr provides an R interface for Apache Spark. Sparklyr is a popular way for R developers to use Spark. This article describes how to use sparklyr in a [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] using RStudio.

## Prerequisites

- [Deploy a SQL Server 2019 big data cluster](quickstart-big-data-cluster-deploy.md).

### Install RStudio Desktop

Install and configure **RStudio Desktop** with the following steps:

1. If you are running on a Windows client, [download and install R 3.4.4](https://cran.rstudio.com/bin/windows/base/old/3.4.4).

1. [Download and install RStudio Desktop](https://www.rstudio.com/products/rstudio/download/).

1. After installation completes, run the following commands inside of RStudio Desktop to install the required packages:

   ```RStudioDesktop
   install.packages("DBI", repos = "https://cran.microsoft.com/snapshot/2019-01-01")
   install.packages("dplyr", repos = "https://cran.microsoft.com/snapshot/2019-01-01")
   install.packages("sparklyr", repos = "https://cran.microsoft.com/snapshot/2019-01-01")
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

After connecting to Spark, you can run sparklyr. The following example performs a query on iris dataset using sparklyr:

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
