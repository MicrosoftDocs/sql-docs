---
title: "WideWorldImporters OLAP database catalog - SQL | Microsoft Docs"
ms.prod: sql
ms.prod_service: sql
ms.technology: samples
ms.custom: ""
ms.date: "08/04/2018"
ms.reviewer: ""
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest||>=aps-pdw-2016||=sqlallproducts-allversions||=azuresqldb-mi-current"
---
# WideWorldImportersDW database catalog
[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]
Explanations for the schemas, tables, and stored procedures in the WideWorldImportersDW database. 

The WideWorldImportersDW database is used for data warehousing and analytical processing. The transactional data about sales and purchases is generated in the WideWorldImporters database, and loaded into the WideWorldImportersDW database using a **daily ETL process**.

The data in WideWorldImportersDW thus mirrors the data in WideWorldImporters, but the tables are organized differently. While WideWorldImporters has a traditional normalized schema, WideWorldImportersDW uses the [star schema](https://wikipedia.org/wiki/Star_schema) approach for its table design. Besides the fact and dimension tables, the database includes a number of staging tables that are used in the ETL process.

## Schemas

The different types of tables are organized in three schemas.

|Schema|Description|
|-----------------------------|---------------------|
|Dimension|Dimension tables.|
|Fact|Fact tables.|  
|Integration|Staging tables and other objects needed for ETL.|  

## Tables

The dimension and fact tables are listed below. The tables in the Integration schema are used only for the ETL process, and are not listed.

### Dimension tables

WideWorldImportersDW has the following dimension tables. The description includes the relationship with the source tables in the WideWorldImporters database.

|Table|Source tables|
|-----------------------------|---------------------|
|City|`Application.Cities`, `Application.StateProvinces`, `Application.Countries`.|
|Customer|`Sales.Customers`, `Sales.BuyingGroups`, `Sales.CustomerCategories`.|
|Date|New table with information about dates, including financial year (based on November 1st start for financial year).|
|Employee|`Application.People`.|
|StockItem|`Warehouse.StockItems`, `Warehouse.Colors`, `Warehouse.PackageType`.|
|Supplier|`Purchasing.Suppliers`, `Purchasing.SupplierCategories`.|
|PaymentMethod|`Application.PaymentMethods`.|
|TransactionType|`Application.TransactionTypes`.|

### Fact tables

WideWorldImportersDW has the following fact tables. The description includes the relationship with the source tables in the WideWorldImporters database, as well as the classes of analytics/reporting queries each fact table is typically used with.

|Table|Source tables|Sample Analytics|
|-----------------------------|---------------------|---------------------|
|Order|`Sales.Orders` and `Sales.OrderLines`|Sales people, picker/packer productivity, and on time to pick orders. In addition, low stock situations leading to back orders.|
|Sale|`Sales.Invoices` and `Sales.InvoiceLines`|Sales dates, delivery dates, profitability over time, profitability by sales person.|
|Purchase|`Purchasing.PurchaseOrderLines`|Expected vs actual lead times|
|Transaction|`Sales.CustomerTransactions` and `Purchasing.SupplierTransactions`|Measuring issue dates vs finalization dates, and amounts.|
|Movement|`Warehouse.StockTransactions`|Movements over time.|
|Stock Holding|`Warehouse.StockItemHoldings`|On-hand stock levels and value.|

## Stored procedures

The stored procedures are used primarily for the ETL process and for configuration purposes.

Any extensions of the sample are encouraged to use the `Reports` schema for Reporting Services reports, and the `PowerBI` schema for Power-BI access.

### Application Schema

These procedures are used to configure the sample. They are used to apply enterprise edition features to the standard edition version of the sample, add PolyBase, and reseed ETL.

|Procedure|Purpose|
|-----------------------------|---------------------|
|Configuration_ApplyPartitionedColumnstoreIndexing|Applies both partitioning and columnstore indexes for fact tables.|
|Configuration_ConfigureForEnterpriseEdition|Applies partitioning, columnstore indexing and in-memory.|
|Configuration_EnableInMemory|Replaces the integration staging tables with SCHEMA_ONLY memory-optimized tables to improve ETL performance.|
|Configuration_ApplyPolyBase|Configures an external data source, file format, and table.|
|Configuration_PopulateLargeSaleTable|Applies enterprise edition changes, then populates a larger amount of data for the 2012 calendar year as additional history.|
|Configuration_ReseedETL|Removes existing data and restarts the ETL seeds. This allows for repopulating the OLAP database to match updated rows in the OLTP database.|

### Integration Schema

Procedures used in the ETL process fall in these categories:
- Helper procedures for the ETL package -  All Get* procedures.
- Procedures used by the ETL package for migrating staged data into the DW tables - All Migrate* procedures.
- `PopulateDateDimensionForYear` - Takes a year and ensures that all dates for that year are populated in the `Dimension.Date` table.

### Sequences Schema

Procedures to configure the sequences in the database.

|Procedure|Purpose|
|-----------------------------|---------------------|
|ReseedAllSequences|Calls the procedure `ReseedSequenceBeyondTableValue` for all sequences.|
|ReseedSequenceBeyondTableValue|Used to reposition the next sequence value beyond the value in any table that uses the same sequence. (Like a `DBCC CHECKIDENT` for identity columns equivalent for sequences but across potentially multiple tables.)|
