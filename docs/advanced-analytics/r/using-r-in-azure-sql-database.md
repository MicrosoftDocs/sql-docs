---
title: "Using R in Azure SQL Database  | Microsoft Docs"
ms.custom: ""
ms.date: "11/09/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0a90c438-d78b-47be-ac05-479de64378b2
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
---
# Using R in Azure SQL Database

As of October 2017, Azure SQL Database supports execution of R code in-database using stored procedures, similar to R Services in SQL Server 2016.

This article provides an overview of the feature and describes known restrictions.

> [!NOTE]
> This release is an initial preview version and is intended for testing and exploration only. A production version will be released sometime in 2018. The exact release date and build will be based on user feedback. so we encourage you to try the feature and let us know what features are important. 
> 
> For information about the release schedule, see the [SQL Server blog](https://blogs.technet.microsoft.com/dataplatforminsider/) or the [Microsoft R Server blog](https://blogs.msdn.microsoft.com/rserver/).

## Features

The preview release provides the following functionality:

+ Call R using stored procedures for easy deployment of machine learning solutions.
+ Get scores from the model using any application that can connect to your cloud database.
+ Supports native scoring using the [PREDICT](../../t-sql/queries/predict-transact-sql.md) function, for fast scoring without use of the R runtime.

For restrictions specific to the preview version, see [Restrictions and known issues](#bkmk_restrictions).

### Components

The overall architecture is like that of SQL Server Machine R Services.

**Security**

+ The SQL Server Trusted Launchpad manages execution of R jobs and controls the lifetime of processes. 

**R packages**

+ The preview release includes Microsoft R Open 3.3.3, and Microsoft R Server version 9.2. The [RevoScaleR package](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) is preinstalled.

+ Some R packages have been removed or modified to reduce footprint in the Azure environment. For example, **mrsdeploy** is not included in Azure SQL Database.

**Performance**

+ Supports training and scoring from any model where data can fit in memory.  The amount of memory available depends on the edition of the database. 
+ Trivial parallelism is supported using the @parallel =1 argument, as well as streaming for R script execution 
+ In preview, limited to a single R script executed concurrently per database.

**Languages**

The roadmap for future release includes support for additional packages and for Python.

## Restrictions

This section lists some additional limitations that apply to the preview release.

### Upgrading R components and adding packages not supported

Azure SQL Database is a managed service, and customers are not expected to manage upgrades to the R components. For the preview release, please use the available installed packages.

Upgrades to R and other machine learning components will be released as they become available.

### Availability

The ability to run R and other machine learning scripts in Azure SQL Database is a preview feature in the West Central US region only. Expansion to other regions, such as West Europe, is planned for later releases.

Running R in Azure SQL Database requires sufficient storage and memory. Currently the following database service tiers and performance levels are supported:

+ Premium Service Tier: P1, P2, P4, P6, P11, P15 
+ Premium RS Service Tier: PRS1, PRS2, PRS4, PRS6 
+ Premium Elastic Pool: 125 eDTUs or greater 
+ Premium RS Elastic Pool: 125 eDTUs or greater 

### Resource management

This release does not support the ability to customize the R installation, or to monitor the use of R scripts.

For example, you cannot enable R script execution only on specific databases.

The DMV [sys.dm_db_resource_stats](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database), which is used to monitor CPU and memory usage of R scripts, is not available in the preview release.

### Other limitations

The following functionality is not supported: 

+ The MicrosoftML package is not available.
+ Package management features such as CREATE EXTERNAL LIBRARY are not supported.
+ You cannot use the Azure SQL database as a remote compute context when executing scripts from an R client. R scripts must be run by using the stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). Scripts called by the stored procedure cannot use other compute contexts.
+ You cannot execute calls to RevoScaleR functions that require parallel execution.
+ Loopback connections from R script to SQL Server are not supported. In other words, you cannot make external calls from your R script to another ODBC data source.

