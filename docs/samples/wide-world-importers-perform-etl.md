---
title: "WideWorldImportersDW - ETL workflow | Microsoft Docs"
ms.prod: sql
ms.prod_service: sql
ms.technology: samples
ms.custom: ""
ms.date: "04/04/2018"
ms.reviewer: ""
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# WideWorldImportersDW ETL workflow
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
Use the *WWI_Integration* ETL package to migrate data from the WideWorldImporters database to the WideWorldImportersDW database as data changes. The package is run periodically (usually daily).

The package ensures high performance by using SQL Server Integration Services (SSIS) to orchestrate bulk T-SQL operations (instead of separate transformations in SSIS).

Dimensions are loaded first, and then Fact tables. You can rerun the package at any time after a failure.

The workflow is as follows:

 ![WideWorldImporters ETL workflow](media/wide-world-importers/wideworldimporters-etl-workflow.png)

The workflow starts with an expression task that determines the appropriate cutoff time. This time is the current time minus a few minutes. (This approach is more robust than requesting data right to the current time.) It then truncates any milliseconds from the time.

The main processing starts by populating the Date dimension table. It ensures that all dates for the current year have been populated in the table.

Next, a series of data flow tasks loads each dimension. Then, they load each fact.

## Prerequisites

- SQL Server 2016 (or later), with the WideWorldImporters and WideWorldImportersDW databases (in the same or in different instances of SQL Server)
- SQL Server Management Studio (SSMS)
- SQL Server 2016 Integration Services (SSIS)
  - Ensure that you create an SSIS catalog. To create an SSIS catalog, in SSMS Object Explorer, right-click **Integration Services**, and then select **Add Catalog**. Leave the default options. You are prompted to enable SQLCLR and provide a password.


## Download

For the latest release of the sample, see [wide-world-importers-release](http://go.microsoft.com/fwlink/?LinkID=800630). Download the *Daily ETL.ispac* SSIS package file.

For the source code to re-create the sample database, see [wide-world-importers](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/wide-world-importers/wwi-integration-etl).

## Install

1. Deploy the SSIS package:
   1. In Windows Explorer, open the *Daily ETL.ispac* package. This launches the Integration Services Deployment Wizard.
   2. Under **Select Source**, follow the defaults for Project Deployment, with the path pointing to the *Daily ETL.ispac* package.
   3. Under **Select Destination**, enter the name of the server that hosts the SSIS catalog.
   4. Select a path under the SSIS catalog, for example, in a new folder named *WideWorldImporters*.
   5. Select **Deploy** to finish the wizard.

2. Create a SQL Server Agent job for the ETL process:
   1. In SSMS, right-click **SQL Server Agent**, and then select **New** > **Job**.
   2. Enter a name, for example *WideWorldImporters ETL*.
   3. Add a **Job Step** of the type **SQL Server Integration Services Package**.
   4. Select the server that has the SSIS catalog, and then select the *Daily ETL* package.
   5. Under **Configuration** > **Connection Managers**, ensure that the connections to the source and target are configured correctly. The default is to connect to the local instance.
   6. Select **OK** to create the job.

3. Execute or schedule the job.
