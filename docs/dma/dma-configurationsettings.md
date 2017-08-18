---
title: "Configuration Settings (SQL Server Data Migration Assistant) | Microsoft Docs"
ms.custom: 
ms.date: "08/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-dma"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Assess"
ms.assetid: ""
caps.latest.revision: ""
author: "sabotta"
ms.author: "carlasab"
manager: "craigg"
---

# Configuration Settings for Data Migration Assistant

You can fine-tune certain behavior of Data Migration Assistant using configuration values in the dma.exe.config file. Here are few key configuration values.

You can find the dma.exe.config file for the Data Migration Assistant desktop application and the command line utility, in the following folders on your machine.

- Desktop Application

  %ProgramFiles%\\Microsoft Data Migration Assistant\\dma.exe.config

- Command Line Utility

  %ProgramFiles%\\Microsoft Data Migration Assistant\\dmacmd.exe.config 

Be sure to save a copy of the original config file before making any modification. After making changes, restart Data Migration Assistant for the new configuration values to take effect.

## Number of databases to assess in parallel

DMA assesses multiple databases in parallel. During assessment DMA extracts data-tier application (dacpac) to understand the database schema. This operation can time-out if several databases on the same server are assessed in parallel. 

Starting with Data Migration Assistant v2.0, you can controll this by setting the parallelDatabases configuration value. Default value is 8.

```
<advisorGroup>

<workflowSettings>

<assessment parallelDatabases="8" />

</workflowSettings>

</advisorGroup>
```




## Number of databases to migrate in parallel

Data Migration Assistant migrates multiple databases in parallel, before migrating logins. During migration, DMA will take a backup of the source database, optionally copy the backup, and then restore it on the target server. You may encounter timeout failures when several databases are selected for migration. 

Starting with Data Migration Assistant v2.0, if you experience this problem you can reduce the parallelDatabases configuration value. You can increase the value to reduce the overall migration time.

```
<advisorGroup>

<workflowSettings>

<migration parallelDatabases=”8″ />

</workflowSettings>

</advisorGroup>
```


## DacFX settings

During assessment, Data Migration Assistant extracts data-tier application (dacpac) to understand the database schema. This operation can fail with time-outs for extremely large databases, or if the server is under load. Starting with Data Migration v1.0, you can modify the following configuration values to avoid errors. 

> [!NOTE]
> The entire &lt;dacfx&gt; entry is commented by default. Remove the comments and then modify the value as needed.

- commandTimeout

   This sets the IDbCommand.CommandTimeout property in *seconds*. (Default=60)

- databaseLockTimeout

   This is equivalent to [SET LOCK\_TIMEOUT timeout\_period ](https://msdn.microsoft.com/library/ms189470.aspx) in *milliseconds*. (Default=5000)

- maxDataReaderDegreeOfParallelism

   Number of Sql connection pool connections to use. (Default=8)

```
<advisorGroup>

<advisorSettings>

<dacFx  commandTimeout="60" databaseLockTimeout="5000"
maxDataReaderDegreeOfParallelism="8"/>

</advisorSettings>

</advisorGroup>
```


## Stretch Database: recommendation threshold

With [SQL Server Stretch
Database](https://docs.microsoft.com/sql/sql-server/stretch-database/stretch-database), you can dynamically stretch warm and cold transactional data from Microsoft SQL Server 2016 to Azure. Stretch Database targets transactional databases with large amounts of cold data. The Stretch Database recommendation under Storage feature recommendation first identifies tables that it thinks will benefit from stretch, and then it identifies changes that need to be made to enable the table for stretch. 

Starting with Data Migration Assistant v2.0, you can control this threshold for a table to qualify for stretch could using the recommendedNumberOfRows configuration value. Default value is 100,000 rows. If you want to analyze the stretch capabilities for even smaller tables, then lower the value accordingly.

```
<advisorGroup>

<advisorSettings>

<stretchDBAdvisor  recommendedNumberOfRows="100000" />

</advisorSettings>

</advisorGroup>
```


## SQL connection timeout

You can control the [SQL connection
time-out](https://msdn.microsoft.com/library/system.data.sqlclient.sqlconnection.connectiontimeout(v=vs.110).aspx)
for source and target instances while running assessment or migration, by setting the connection timeout value to a specified number of seconds. The default value is 15 seconds.

```
<appSettings>

<add key="ConnectionTimeout" value="15" />

</appSettings>
```


## See Also

[Data Migration Assistant Download](https://www.microsoft.com/download/details.aspx?id=53595)
