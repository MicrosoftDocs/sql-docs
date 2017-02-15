---
title: "Data generation | Microsoft Docs"
ms.custom: ""
ms.date: "01/30/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.prod: "sql-non-specified"
ms.technology: 
  - " database-engine "
ms.topic: "article"
ms.assetid: f387273b-8b5f-4687-b033-09499ea2d68f
caps.latest.revision: 4
author: "BarbKess"
ms.author: "barbkess"
manager: "jhubbard"
robots: noindex,nofollow
---
# WideWorldImporters data generation
The released versions of the WideWorldImporters and WideWorldImportersDW databases contains data starting January 1st 2013, up to the day these databases were generated.

If the sample databases are used at a later date, for demonstration or illustration purposes, it may be beneficial to include more recent sample data in the database.

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

This statement adds sample sales and purchase data in the database, up to the current date. It outputs the progress of the data generation day-by-day. It will take rougly 10 minutes for every year that needs data. Note that there are some differences in the data generated between runs, since there is a random factor in the data generation.

To increase or decrease the amount of data generated, in terms of orders per day, change the value for the parameter `@AverageNumberOfCustomerOrdersPerDay`. The parameters `@SaturdayPercentageOfNormalWorkDay` and `@SundayPercentageOfNormalWorkDay` are used to determine the order volume for weekend days.

## Importing Generated Data in WideWorldImportersDW

To import sample data up to the current date in the OLAP database WideWorldImportersDW, follow these steps:

1. Execute the data generation logic in the WideWorldImporters OLTP database, using the steps above.
2. If you have not yet done so, install a clean version of the WideWorldImportersDW database. For installation instructions, **WideWorldImporters Installation and Configuration**.
3. Reseed the OLAP database by executing the following statement in the database:

```
    EXECUTE [Application].Configuration_ReseedETL
```

4. Run the SSIS package **Daily ETL.ispac** to import the data into the OLAP database. For instructions on how to run the ETL job, see **WideWorldImporters ETL Workflow**.

## Generating Data in WideWorldImportersDW for Performance Testing

WideWorldImportersDW has the capability to arbitrarily increase data size, for the purpose of performance testing, for example with clustered columnstore.

One of the challenges when shipping a sample like World Wide Importers is to keep the size of the download small enough to be distributable but large enough to be able to demonstrate SQL Server performance features. One area where this is a particular challenge is when working with columnstore indexes. Significant benefits come only when working with larger numbers of rows. 

The procedure `Application.Configuration_PopulateLargeSaleTable` can be used to greatly increase the number of rows in the `Fact.Sale` table. Note that the rows are inserted in the 2012 calendar year to avoid colliding with existing World Wide Importers data starting at 1st January 2013.

### Procedure Details

#### Name: 

    Application.Configuration_PopulateLargeSaleTable

#### Parameters:

  `@EstimatedRowsFor2012` **bigint** (with a default of 12000000)

#### Result:

Approximately the required number of rows are inserted into the `Fact.Sale` table in the 2012 year. The procedure artificially limits the number of rows per day to 50000. This could be changed but is there to avoid accidential overinflations of the table.

In addition, the procedure applies clustered columnstore indexing, if it has not been applied already.