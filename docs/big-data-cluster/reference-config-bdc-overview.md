---
title: SQL Server Big Data Clusters Configuration Properties
titleSuffix: SQL Server big data clusters
description: Reference article for configuration properties for Big Data Clusters
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 02/11/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---
# SQL Server Big Data Clusters Configuration Properties

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

Big Data Clusters configuration settings can be defined at the following scopes: `cluster`, `service`, and `resource`. The hierarchy of the settings follows in this order as well, from highest to lowest. BDC components will take the value of the setting defined at the lowest scope. If the setting is not defined at a given scope, it will inherit the value from its higher parent scope. Below is a list of available settings for each component of BDC at the various scopes. You can also view configurable settings for your BDC using azdata.

## BDC cluster-scope settings
You can configure the following settings at the cluster scope.

|Property|Options|
| --- | --- |
|`mssql.telemetry`|`customerfeedback = { true | false }` |
|`mssql.traceflag`|`traceflag<#> = <####>` |

## SQL service-scope settings
You can configure the following settings at the SQL service scope.

|Property|Options|
| --- | --- |
|`mssql.language`|`lcid = <language_identifier>` |

## Spark service-scope settings
Visit the [Apache Spark & Apache Hadoop configuration article](reference-config-spark-hadoop.md) to see all supported and unsupported settings.

## HDFS service-scope settings
Visit the [Apache Spark & Apache Hadoop configuration article](reference-config-spark-hadoop.md) to see all supported and unsupported settings.

## Gateway service-scope settings
No gateway service-scope settings configurable. Configure settings at the gateway resource-scope.

## App service-scope settings
None available

## Master Pool resource-scope settings
|Property|Options|
| --- | --- |
|`mssql.sqlagent`|`enabled = { true | false }` |
|`mssql.licensing`|`pid = { Enterprise | Developer }` |
<!-- |`mssql.collation`|`x = <language_identifier>` | -->

> [!NOTE]
> Changing the default collation for an instance of SQL Server is a complex operation. In addition to changing the `mssql.collation` setting, you may need to re-create your user databases and all objects in them. For instructions on how to do so, see [Set or Change the Server Collation](../relational-databases/collations/set-or-change-the-server-collation.md#changing-the-server-collation-in-sql-server)

## Storage Pool resource-scope settings
The storage pool consists of SQL, Spark, and HDFS components.

### Available SQL configurations
|Property|Options|
| --- | --- |
|`mssql.degreeOfParallelism`| |
|`mssql.minServerMemory`| |
|`mssql.maxServerMemory`| |
|`mssql.network.tlscert`| |
|`mssql.network.tlskey`| |
|`mssql.numberOfCpus`| |
|`mssql.storagePoolCacheSize`| |
|`mssql.storagePoolMaxCacheSize`| |
|`mssql.storagePoolCacheAutogrowth`| |
|`mssql.tempdb.autogrowthPerDataFile`| |
|`mssql.tempdb.autogrowthPerLogFile`| |
|`mssql.tempdb.dataFileSize`| |
|`mssql.tempdb.dataFileMaxSize`| |
|`mssql.tempdb.logFileMaxSize`| |
|`mssql.tempdb.numberOfDataFiles`| |
|`mssql.traceflag`|`traceflag<#> = <####>` |


### Available Apache Spark and Hadoop configurations
Visit the [Apache Spark & Apache Hadoop configuration article](reference-config-spark-hadoop.md) to see all supported and unsupported settings.

## Data Pool resource-scope settings
|Property|Options|
| --- | --- |
|`mssql.degreeOfParallelism`| |
|`mssql.minServerMemory`| |
|`mssql.maxServerMemory`| |
|`mssql.network.tlscert`| |
|`mssql.network.tlskey`| |
|`mssql.numberOfCpus`| |
|`mssql.tempdb.autogrowthPerDataFile`| |
|`mssql.tempdb.autogrowthPerLogFile`| |
|`mssql.tempdb.dataFileSize`| |
|`mssql.tempdb.dataFileMaxSize`| |
|`mssql.tempdb.logFileMaxSize`| |
|`mssql.tempdb.numberOfDataFiles`| |
|`mssql.traceflag`|`traceflag<#> = <####>` |

## Compute Pool resource-scope settings
|Property|Options|
| --- | --- |
|`mssql.degreeOfParallelism`| |
|`mssql.minServerMemory`| |
|`mssql.maxServerMemory`| |
|`mssql.network.tlscert`| |
|`mssql.network.tlskey`| |
|`mssql.numberOfCpus`| |
|`mssql.tempdb.autogrowthPerDataFile`| |
|`mssql.tempdb.autogrowthPerLogFile`| |
|`mssql.tempdb.dataFileSize`| |
|`mssql.tempdb.dataFileMaxSize`| |
|`mssql.tempdb.logFileMaxSize`| |
|`mssql.tempdb.numberOfDataFiles`| |
|`mssql.traceflag`|`traceflag<#> = <####>` |

## Spark Pool resource-scope settings
Visit the [Apache Spark & Apache Hadoop configuration article](reference-config-spark-hadoop.md) to see all supported and unsupported settings.

## Gateway resource-scope settings
Visit the [Apache Spark & Apache Hadoop configuration article](reference-config-spark-hadoop.md) to see all supported and unsupported settings.

## `Sparkhead` resource-scope settings
Visit the [Apache Spark & Apache Hadoop configuration article](reference-config-spark-hadoop.md) to see all supported and unsupported settings.

## Zookeeper resource-scope settings
Visit the [Apache Spark & Apache Hadoop configuration article](reference-config-spark-hadoop.md) to see all supported and unsupported settings.

## Namenode resource-scope settings
Visit the [Apache Spark & Apache Hadoop configuration article](reference-config-spark-hadoop.md) to see all supported and unsupported settings.

## App Proxy resource-scope settings
None available

## Next steps

[Configure SQL Server Big Data Clusters](configure-bdc-overview.md)