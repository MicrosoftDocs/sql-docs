---
title: "ETL workflow | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "samples"
ms.custom: ""
ms.date: "06/15/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 679e58fe-b062-4934-a94c-9bb916b0bcb0
caps.latest.revision: 5
author: "BarbKess"
ms.author: "barbkess"
manager: "jhubbard"
robots: noindex,nofollow
---
# WideWorldImportersDW ETL workflow
The ETL package WWI_Integration is used to migrate data from the WideWorldImporters database to the WideWorldImportersDW database as the data changes. The package is run periodically (most commonly daily).

## Overview

The design of the package uses SQL Server Integration Services (SSIS) to orchestrate bulk T-SQL operations (rather than as separate transformations within SSIS) to ensure high performance.

Dimensions are loaded first, followed by Fact tables. The package can be re-run at any time after a failure.

The workflow is as follows:

 ![WideWorldImporters ETL workflow](../../sample/world-wide-importers/media/wideworldimporters-etl-workflow.png)

It starts with an expression task that works out the appropriate cutoff time. This time is the current time less a few minutes. (This is more robust than requesting data right to the current time). It then truncates any milliseconds from the time.

The main processing starts by populating the Date dimension table. It ensures that all dates for the current year have been populated in the table.

After this, a series of data flow tasks loads each dimension, then each fact.

## Prerequisites

- SQL Server 2016 (or higher) with the databases WideWorldImporters and WideWorldImportersDW. These can be on the same or different instances of SQL Server.
- SQL Server Management Studio (SSMS)
- SQL Server 2016 Integration Services (SSIS).
  - Make sure you have created an SSIS Catalog. If not, right click **Integration Services** in SSMS Object Explorer, and choose **Add Catalog**. Follow the defaults. It will ask you to enable sqlclr and provide a password.


## Download

The latest release of the sample:

[wide-world-importers-release](http://go.microsoft.com/fwlink/?LinkID=800630)

Download the SSIS package file **Daily ETL.ispac**.

Source code to recreate the sample database is available from the following location.

[wide-world-importers](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/wide-world-importers/wwi-integration-etl)

## Install

1. Deploy the SSIS package.
   - Open the "Daily ETL.ispac" package from Windows Explorer. This will launch the Integration Services Deployment Wizard.
   - Under "Select Source" follow the default Project Deployment, with the path pointing to the "Daily ETL.ispac" package.
   - Under "Select Destination" enter the name of the server that hosts the SSIS catalog.
   - Select a path under the SSIS catalog, for example under a new folder "WideWorldImporters".
   - Finalize the wizard by clicking Deploy.

2. Create a SQL Server Agent job for the ETL process.
   - In SSMS, right-click "SQL Server Agent" and select New->Job.
   - Pick a name, for example "WideWorldImporters ETL".
   - Add a Job Step of type "SQL Server Integration Services Package".
   - Select the server with the SSIS catalog, and select the "Daily ETL" package.
   - Under Configuration->Connection Managers ensure the connections to the source and target are configured correctly. The default is to connect to the local instance.
   - Click OK to create the Job.

3. Execute or schedule the Job.
