---
title: Use sparklyr from RStudio
titleSuffix: SQL Server big data clusters
description: Connect to big data cluster using sparklyr from RStudio.
author: jejiang
ms.author: jejiang
ms.reviewer: jroth
ms.date: 02/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Use Sparklyr in SQL Server big data cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Sparklyr provides an R interface for Apache Spark. Sparklyr is the preffered way for R developers to use Spark. This article describes how to use sparklyr in a SQL Server 2019 big data cluster (preview) using RStudio.

## Prerequisites

- [Deploy a SQL Server 2019 big data cluster](quickstart-big-data-cluster-deploy.md).
- [Install RStudio](https://www.rstudio.com/)

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

``` r
copy_to(sc, iris)

iris_count <- dbGetQuery(sc, "SELECT COUNT(*) FROM iris")

iris_count
```

## Next steps

For more information about big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).