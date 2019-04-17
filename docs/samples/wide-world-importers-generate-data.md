---
title: "WideWorldImporters generate data - SQL sample database | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2018"
ms.reviewer: ""
ms.prod: sql
ms.prod_service: sql
ms.technology: samples
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
---

# WideWorldImporters data generation
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
The released versions of the WideWorldImporters and WideWorldImportersDW databases have data from January 1, 2013, up to the day that the databases were generated.

When you use these sample databases, you might want to include more recent sample data.

## Data generation in WideWorldImporters

To generate sample data up to the current date:

1. If you haven't done so, install a clean version of the WideWorldImporters database. For installation instructions, see [Installation and configuration](wide-world-importers-oltp-install-configure.md).
2. Execute the following statement in the database:

    ```
        EXECUTE DataLoadSimulation.PopulateDataToCurrentDate
            @AverageNumberOfCustomerOrdersPerDay = 60,
            @SaturdayPercentageOfNormalWorkDay = 50,
            @SundayPercentageOfNormalWorkDay = 0,
            @IsSilentMode = 1,
            @AreDatesPrinted = 1;
    ```

    This statement adds sample sales and purchase data to the database, up to the current date. It displays the progress of the data generation by day. Data generation can take about 10 minutes for every year that needs data. Because of a random factor in the data generation, there are some differences in the data that's generated between runs.

    To increase or decrease the amount of data generated for orders per day, change the value for the parameter `@AverageNumberOfCustomerOrdersPerDay`. Use the parameters `@SaturdayPercentageOfNormalWorkDay` and `@SundayPercentageOfNormalWorkDay` to determine the order volume for weekend days.

## Import generated data in WideWorldImportersDW

To import sample data up to the current date in the WideWorldImportersDW OLAP database:

1. Execute the data generation logic in the WideWorldImporters OLTP database by using the steps in the preceding section.
2. If you haven't yet done so, install a clean version of the WideWorldImportersDW database. For installation instructions, see [Installation and configuration](wide-world-importers-oltp-install-configure.md).
3. Reseed the OLAP database by executing the following statement in the database:

    ```sql
    EXECUTE [Application].Configuration_ReseedETL
    ```

4. Run the *Daily ETL.ispac* SQL Server Integration Services package to import the data into the OLAP database. To learn how to run the ETL job, see [WideWorldImporters ETL workflow](wide-world-importers-perform-etl.md).

## Generate data in WideWorldImportersDW for performance testing

WideWorldImportersDW can arbitrarily increase data size for performance testing. For example, it can increase data size to use with clustered columnstore indexing.

One of the challenges is to keep the size of the download small enough to download easily, but large enough to demonstrate SQL Server performance features. For example, significant benefits for columnstore indexes are achieved only when you work with larger numbers of rows. 

You can use the `Application.Configuration_PopulateLargeSaleTable` procedure to increase the number of rows in the `Fact.Sale` table. The rows are inserted in the 2012 calendar year to avoid colliding with existing World Wide Importers data that begins January 1, 2013.

### Procedure details

#### Name

    Application.Configuration_PopulateLargeSaleTable

#### Parameters

  `@EstimatedRowsFor2012` **bigint** (with a default of 12000000)

#### Result

Approximately the required number of rows are inserted into the `Fact.Sale` table in the 2012 year. The procedure artificially limits the number of rows to 50,000 per day. You can change this limitation, but the limitation helps you avoid accidental overinflations of the table.

The procedure also applies clustered columnstore indexing if it hasn't already been applied.
