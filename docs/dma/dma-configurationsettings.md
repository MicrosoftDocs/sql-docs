---
title: "Configure settings for Data Migration Assistant (SQL Server) | Microsoft Docs"
description: Learn how to configure settings for the Data Migration Assistant by updating values in the configuration file
ms.custom: ""
ms.date: "10/20/2018"
ms.prod: sql
ms.prod_service: "dma"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Assess"
ms.assetid: ""
author: pochiraju
ms.author: rajpo
manager: craigg
---

# Configure settings for Data Migration Assistant

You can fine-tune certain behavior of Data Migration Assistant by setting configuration values in the dma.exe.config file. This article describes key configuration values.

You can find the dma.exe.config file for the Data Migration Assistant desktop application and the command-line utility, in the following folders on your machine.

- Desktop Application

  %ProgramFiles%\\Microsoft Data Migration Assistant\\dma.exe.config

- Command-Line Utility

  %ProgramFiles%\\Microsoft Data Migration Assistant\\dmacmd.exe.config 

Be sure to save a copy of the original config file before making any modifications. After making changes, restart Data Migration Assistant for the new configuration values to take effect.

## Number of databases to assess in parallel

Data Migration Assistant assesses multiple databases in parallel. During assessment Data Migration Assistant extracts data-tier application (dacpac) to understand the database schema. This operation can time out if several databases on the same server are assessed in parallel. 

Starting with Data Migration Assistant v2.0, you can control this by setting the parallelDatabases configuration value. Default value is 8.

```
<advisorGroup>

<workflowSettings>

<assessment parallelDatabases="8" />

</workflowSettings>

</advisorGroup>
```




## Number of databases to migrate in parallel

Data Migration Assistant migrates multiple databases in parallel, before migrating logins. During migration, Data Migration Assistant will take a backup of the source database, optionally copy the backup, and then restore it on the target server. You may encounter timeout failures when several databases are selected for migration. 

Starting with Data Migration Assistant v2.0, if you experience this problem you can reduce the parallelDatabases configuration value. You can increase the value to reduce the overall migration time.

```
<advisorGroup>

<workflowSettings>

<migration parallelDatabases="8″ />

</workflowSettings>

</advisorGroup>
```


## DacFX settings

During assessment, Data Migration Assistant extracts data-tier application (dacpac) to understand the database schema. This operation can fail with time-outs for extremely large databases, or if the server is under load. Starting with Data Migration v1.0, you can modify the following configuration values to avoid errors. 

> [!NOTE]
> The entire &lt;dacfx&gt; entry is commented by default. Remove the comments and then modify the value as needed.

- commandTimeout

   This parameter sets the IDbCommand.CommandTimeout property in *seconds*. (Default=60)

- databaseLockTimeout

   This parameter is equivalent to [SET LOCK\_TIMEOUT timeout\_period](../t-sql/statements/set-lock-timeout-transact-sql.md) in *milliseconds*. (Default=5000)

- maxDataReaderDegreeOfParallelism

  This parameter sets the number of SQL connection pool connections to use. (Default=8)

```
<advisorGroup>

<advisorSettings>

<dacFx  commandTimeout="60" databaseLockTimeout="5000"
maxDataReaderDegreeOfParallelism="8"/>

</advisorSettings>

</advisorGroup>
```

## Stretch Database: Recommendation threshold

With [SQL Server Stretch Database](https://docs.microsoft.com/sql/sql-server/stretch-database/stretch-database), you can dynamically stretch warm and cold transactional data from Microsoft SQL Server 2016 to Azure. Stretch Database targets transactional databases with large amounts of cold data. The Stretch Database recommendation, under Storage feature recommendation, first identifies tables that it thinks will benefit from this feature, and then it identifies changes that need to be made to enable the table for this feature.

Starting with Data Migration Assistant v2.0, you can control this threshold for a table to qualify for the Stretch Database feature using the recommendedNumberOfRows configuration value. Default value is 100,000 rows. If you want to analyze the stretch capabilities for even smaller tables, then lower the value accordingly.

```
<advisorGroup>

<advisorSettings>

<stretchDBAdvisor  recommendedNumberOfRows="100000" />

</advisorSettings>

</advisorGroup>
```


## SQL connection timeout

You can control the [SQL connection time-out](https://msdn.microsoft.com/library/system.data.sqlclient.sqlconnection.connectiontimeout(v=vs.110).aspx)
for source and target instances while running an assessment or migration, by setting the connection timeout value to a specified number of seconds. The default value is 15 seconds.

```
<appSettings>

<add key="ConnectionTimeout" value="15" />

</appSettings>
```


## See also

[Data Migration Assistant Download](https://www.microsoft.com/download/details.aspx?id=53595)
