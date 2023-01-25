---
title: "Configure settings for Data Migration Assistant"
description: Learn how to configure settings for the Data Migration Assistant by updating values in the configuration file
author: rajeshsetlem
ms.author: rajpo
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Data Migration Assistant, Assess"
---

# Configure settings for Data Migration Assistant

You can fine-tune certain behavior of Data Migration Assistant by setting configuration values in the dma.exe.config file. This article describes key configuration values.

You can find the dma.exe.config file for the Data Migration Assistant desktop application and the command-line utility, in the following folders on your machine.

- Desktop Application

  `%ProgramFiles%\Microsoft Data Migration Assistant\dma.exe.config`

- Command-Line Utility

  `%ProgramFiles%\Microsoft Data Migration Assistant\dmacmd.exe.config`

Be sure to save a copy of the original config file before making any modifications. After making changes, restart Data Migration Assistant for the new configuration values to take effect.

## Number of databases to assess in parallel

Data Migration Assistant assesses multiple databases in parallel. During assessment Data Migration Assistant extracts data-tier application (dacpac) to understand the database schema. This operation can time out if several databases on the same server are assessed in parallel.

Starting with Data Migration Assistant v2.0, you can control this by setting the parallelDatabases configuration value. Default value is 8.

```xml
<advisorGroup>
<workflowSettings>
<assessment parallelDatabases="8" />
</workflowSettings>
</advisorGroup>
```

## Number of databases to migrate in parallel

Data Migration Assistant migrates multiple databases in parallel, before migrating logins. During migration, Data Migration Assistant will take a backup of the source database, optionally copy the backup, and then restore it on the target server. You may encounter timeout failures when several databases are selected for migration.

Starting with Data Migration Assistant v2.0, if you experience this problem you can reduce the parallelDatabases configuration value. You can increase the value to reduce the overall migration time.

```xml
<advisorGroup>
<workflowSettings>
<migration parallelDatabases="8″ />
</workflowSettings>
</advisorGroup>
```

## DacFX settings

During assessment, Data Migration Assistant extracts data-tier application (dacpac) to understand the database schema. This operation can fail with time-outs for large databases, or if the server is under load. Starting with Data Migration v1.0, you can modify the following configuration values to avoid errors.

> [!NOTE]
> The entire `<dacfx>` entry is commented by default. Remove the comments and then modify the value as needed.

- commandTimeout

  This parameter sets the IDbCommand.CommandTimeout property in *seconds*. (Default=60)

- databaseLockTimeout

  This parameter is equivalent to [SET LOCK\_TIMEOUT timeout\_period](../t-sql/statements/set-lock-timeout-transact-sql.md) in *milliseconds*. (Default=5000)

- maxDataReaderDegreeOfParallelism

  This parameter sets the number of SQL connection pool connections to use. (Default=8)

```xml
<advisorGroup>
<advisorSettings>
<dacFx commandTimeout="60" databaseLockTimeout="5000" maxDataReaderDegreeOfParallelism="8"/>
</advisorSettings>
</advisorGroup>
```

## Stretch Database: Recommendation threshold

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../includes/ssnotedepfutureavoid-md.md)]

With [SQL Server Stretch Database](../sql-server/stretch-database/stretch-database.md), you can dynamically stretch warm and cold transactional data from Microsoft SQL Server 2016 to Azure. The Stretch Database recommendation is no longer available as an advisor option.

## SQL connection timeout

You can control the [SQL connection time-out](/dotnet/api/system.data.sqlclient.sqlconnection.connectiontimeout)
for source and target instances while running an assessment or migration, by setting the connection timeout value to a specified number of seconds. The default value is 15 seconds.

```xml
<appSettings>
<add key="ConnectionTimeout" value="15" />
</appSettings>
```

## Ignore error codes

Each rule has an error code in its title. If you don't need rules and want to ignore them, use the `ignoreErrorCodes` property. You can specify to ignore a single error or multiple errors. To ignore multiple errors, use a semicolon, for example, `ignoreErrorCodes="46010;71501"`. The default value is 71501, which is associated with unresolved references identified when an object references system objects such as procedures, views, etc.

```xml
<workflowSettings>
<assessment parallelDatabases="8" ignoreErrorCodes="71501" />
</workflowSettings>
```

## See also

- [Data Migration Assistant Download](https://www.microsoft.com/download/details.aspx?id=53595)
