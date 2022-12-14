---
title: "Key features in DW WideWorldImporters database"
description: Learn how the WideWorldImportersDW database showcases key features of SQL Server that are suitable for data warehousing and analytics. 
ms.service: sql
ms.subservice: "samples"
ms.date: 07/01/2020
ms.reviewer: ""
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest||>=aps-pdw-2016||=azuresqldb-mi-current"
ms.custom: "seo-lt-2019"
---
# WideWorldImportersDW use of SQL Server features and capabilities
[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]
WideWorldImportersDW is designed to showcase many of the key features of SQL Server that are suitable for data warehousing and analytics. The following is a list of SQL Server features and capabilities, and a description of how they are used in WideWorldImportersDW.

## PolyBase

[Applies to SQL Server (2016 and later)]

PolyBase is used to combine sales information from WideWorldImportersDW with a public data set about demographics to understand which cities might be of interest for further expansion of sales.

To enable the use of PolyBase in the sample database, make sure it is installed, and run the following stored procedure in the database:

```sql
EXECUTE [Application].[Configuration_ApplyPolyBase]
```

This will create an external table `dbo.CityPopulationStatistics` that references a public data set that contains population data for cities in the United States, hosted in Azure Blob Storage. You are encouraged to review the code in the stored procedure to understand the configuration process. If you want to host your own data in Azure Blob Storage and keep it secure from general public access, you will need to undertake additional configuration steps. The following query returns the data from that external data set:

```sql
SELECT
        CityID, StateProvinceCode, CityName,
        YearNumber, LatestRecordedPopulation
    FROM
        dbo.CityPopulationStatistics;
```

To understand which cities might be of interest for further expansion, the following query looks at the growth rate of cities, and returns the top 100 largest cities with significant growth, and where Wide World Importers does not have a sales presence. The query involves a join between the remote table `dbo.CityPopulationStatistics` and the local table `Dimension.City`, and a filter involving the local table `Fact.Sales`.

```sql
WITH PotentialCities
AS
(
    SELECT cps.CityName,
            cps.StateProvinceCode,
            MAX(cps.LatestRecordedPopulation) AS PopulationIn2016,
            (MAX(cps.LatestRecordedPopulation) - MIN(cps.LatestRecordedPopulation)) * 100.0
                / MIN(cps.LatestRecordedPopulation) AS GrowthRate
    FROM dbo.CityPopulationStatistics AS cps
    WHERE cps.LatestRecordedPopulation IS NOT NULL
    AND cps.LatestRecordedPopulation <> 0
    GROUP BY cps.CityName, cps.StateProvinceCode
),
InterestingCities
AS
(
    SELECT DISTINCT pc.CityName,
                    pc.StateProvinceCode,
                    pc.PopulationIn2016,
                    FLOOR(pc.GrowthRate) AS GrowthRate
    FROM PotentialCities AS pc
    INNER JOIN Dimension.City AS c
    ON pc.CityName = c.City
    WHERE GrowthRate > 2.0
    AND NOT EXISTS (SELECT 1 FROM Fact.Sale AS s WHERE s.[City Key] = c.[City Key])
)
SELECT TOP(100) CityName, StateProvinceCode, PopulationIn2016, GrowthRate
FROM InterestingCities
ORDER BY PopulationIn2016 DESC;
```

## Clustered Columnstore Indexes

(Full version of the sample)

Clustered Columnstore Indexes (CCI) are used with all the fact tables, to reduce storage footprint and improve query performance. With the use of CCI, the base storage for the fact tables uses column compression.

Nonclustered indexes are used on top of the clustered columnstore index, to facilitate primary key and foreign key constraints. These constraints were added out of an abundance of caution - the ETL process sources the data from the WideWorldImporters database, which has constraints to enforce integrity. Removing primary and foreign key constraints, and their supporting indexes, would reduce the storage footprint of the fact tables.

**Data size**

The sample database has limited data size, to make it easy to download and install the sample. However, to see the real performance benefits of columnstore indexes, you would want to use a larger data set.

You can run the following statement to increase the size of the `Fact.Sales` table by inserting another 12 million rows of sample data. These rows are all inserted for the year 2012, such that there is no interference with the ETL process.

```sql
    EXECUTE [Application].[Configuration_PopulateLargeSaleTable]
```

This statement will take around 5 minutes to run. To insert more than 12 million rows, pass the desired number of rows to insert as a parameter to this stored procedure.

To compare query performance with and without columnstore, you can drop and/or recreate the clustered columnstore index.

To drop the index:

```sql
 DROP INDEX [CCX_Fact_Order] ON [Fact].[Order]
```

To recreate:

```sql
CREATE CLUSTERED COLUMNSTORE INDEX [CCX_Fact_Order] ON [Fact].[Order]
```

## Partitioning

(Full version of the sample)

Data size in a Data Warehouse can grow very large. Therefore it is best practice to use partitioning to manage the storage of the large tables in the database.

All of the larger fact tables are partitioned by year. The only exception is `Fact.Stock Holdings`, which is not date-based and has limited data size compared with the other fact tables.

The partition function used for all partitioned tables is `PF_Date`, and the partition scheme being used is `PS_Date`.

## In-Memory OLTP

(Full version of the sample)

WideWorldImportersDW uses SCHEMA_ONLY memory-optimized tables for the staging tables. All `Integration.`*`_Staging` tables are SCHEMA_ONLY memory-optimized tables.

The advantage of SCHEMA_ONLY tables is that they are not logged, and do not require any disk access. This improves the performance of the ETL process. Since these tables are not logged, their contents are lost if there is a failure. However, the data source is still available, so the ETL process can simply be restarted if a failure occurs.
