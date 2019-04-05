---
title: Use sparklyr from RStudio
titleSuffix: SQL Server big data clusters
description: Connect to big data cluster using sparklyr from RStudio.
author: jejiang
ms.author: jejiang
ms.reviewer: jroth
ms.date: 04/05/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Use Sparklyr in SQL Server big data cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Sparklyr provides an R interface for Apache Spark. Sparklyr is the preferred way for R developers to use Spark. This article describes how to use sparklyr in a SQL Server 2019 big data cluster (preview) using RStudio.

## Prerequisites

- [Deploy a SQL Server 2019 big data cluster](quickstart-big-data-cluster-deploy.md).

### Install RStudio Desktop

Install and configure **RStudio Desktop** with the following steps:

1. If you are running on a Windows client, [download and install R 3.4.4](https://cran.rstudio.com/bin/windows/base/old/3.4.4).

1. [Download and install RStudio Desktop](https://www.rstudio.com/products/rstudio/download/).

1. After installation completes, run the following commands inside of RStudio Desktop to install the required packages:

   ```RStudio Desktop
   install.packages("DBI", repos = "https://cran.microsoft.com/snapshot/2019-01-01")
   install.packages("dplyr", repos = "https://cran.microsoft.com/snapshot/2019-01-01")
   install.packages("sparklyr", repos = "https://cran.microsoft.com/snapshot/2019-01-01")
   ```

## Connect to spark in SS19 Big Data cluster

In RStudio create a RScript and connect to the Spark as follows. Spark big data cluster connects through Livy, which can be reached with the [HDFS/Spark gateway](connect-to-big-data-cluster.md#hdfs). For authentication, use the username and password you set during the deployment.

```r
library(sparklyr)
library(dplyr)
library(DBI)

#Specify the Knox username and password
config <- livy_config(user = "***root***", password = "****")

httr::set_config(httr::config(ssl_verifypeer = 0L))

sc <- spark_connect(master = "https://<IP>:<PORT>/gateway/default/livy/v1",
                    method = "livy",
                    config = config)
```

## Run sparklyr queries

After connecting to Spark, you can run sparklyr. The following example performs a query on iris dataset using sparklyr:

```r
copy_to(sc, iris)

iris_count <- dbGetQuery(sc, "SELECT COUNT(*) FROM iris")

iris_count
```

## Distributed R computations

One feature of sparklyr is the ability to [distribute R computations](https://spark.rstudio.com/guides/distributed-r/) with [spark_apply](https://spark.rstudio.com/reference/spark_apply/).

Because big data clusters use Livy connections, you must set `packages = FALSE` in the call to **spark_apply**. For more information, see the [Livy section](https://spark.rstudio.com/guides/distributed-r/#livy) of the sparklyr documentation on distributed R computations. With this setting, you can only use the R packages that are already installed on your Spark cluster in the R code passed to **spark_apply**. The following example demonstrates this functionality:

```r
iris_tbl <- copy_to(sc, iris)

iris_tbl %>% spark_apply(function(e) nrow(e), names = "nrow", group_by = "Species", packages = FALSE)
```

## Next steps

For more information about big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).