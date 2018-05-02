---
title: "WideWorldImporters generate data - SQL sample database | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2018"
ms.reviewer: ""
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.prod: sql
ms.prod_service: sql
ms.technology: samples
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
robots: noindex,nofollow
---
# WideWorldImporters data generation
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
The released versions of the WideWorldImporters and WideWorldImportersDW databases contains data starting January 1 2013, up to the day these databases were generated.

When using the sample databases, it might be beneficial to include more recent sample data.

## Data Generation in WideWorldImporters

To generate sample data up to the current date, follow these steps:

1. If you have not yet done so, install a clean version of the WideWorldImporters database. For installation instructions, **WideWorldImporters Installation and Configuration**.
2. Execute the following statement in the database:

```
    EXECUTE DataLoadSimulation.PopulateDataToCurrentDate
        @AverageNumberOfCustomerOrdersPerDay = 60,
        @SaturdayPercentageOfNormalWorkDay = 50,
        @SundayPercentageOfNormalWorkDay = 0,
        @IsSilentMode = 1,
        @AreDatesPrinted = 1;
```

This statement adds sample sales and purchase data in the database, up to the current date. It outputs the progress of the data generation day-by-day. It can take about 10 minutes for every year that needs data. There are some differences in the data generated between runs, since there is a random factor in the data generation.

To increase or decrease the amount of data generated, in terms of orders per day, change the value for the parameter `@AverageNumberOfCustomerOrdersPerDay`. The parameters `@SaturdayPercentageOfNormalWorkDay` and `@SundayPercentageOfNormalWorkDay` are used to determine the order volume for weekend days.

## Importing Generated Data in WideWorldImportersDW

To import sample data up to the current date in the OLAP database WideWorldImportersDW, follow these steps:

1. Execute the data generation logic in the WideWorldImporters OLTP database, using the steps above.
2. If you have not yet done so, install a clean version of the WideWorldImportersDW database. For installation instructions, **WideWorldImporters Installation and Configuration**.
3. Reseed the OLAP database by executing the following statement in the database:

    ```sql
    EXECUTE [Application].Configuration_ReseedETL
    ```

4. Run the SSIS package **Daily ETL.ispac** to import the data into the OLAP database. For instructions on how to run the ETL job, see **WideWorldImporters ETL Workflow**.

## Generating Data in WideWorldImportersDW for Performance Testing

WideWorldImportersDW has the capability to arbitrarily increase data size, for the purpose of performance testing, for example with clustered columnstore.

One of the challenges is to keep the size of the download small enough to download easily, but large enough to demonstrate SQL Server performance features. For example, significant benefits for columnstore indexes happen only when working with larger numbers of rows. 

The procedure `Application.Configuration_PopulateLargeSaleTable` can be used to greatly increase the number of rows in the `Fact.Sale` table. Note that the rows are inserted in the 2012 calendar year to avoid colliding with existing World Wide Importers data starting at January 1 2013.

### Procedure Details

#### Name: 

    Application.Configuration_PopulateLargeSaleTable

#### Parameters:

  `@EstimatedRowsFor2012` **bigint** (with a default of 12000000)

#### Result:

Approximately the required number of rows are inserted into the `Fact.Sale` table in the 2012 year. The procedure artificially limits the number of rows per day to 50000. This limitation could be changed, but is there to avoid accidental overinflations of the table.

In addition, the procedure applies clustered columnstore indexing, if it has not been applied already.
