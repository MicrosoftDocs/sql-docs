---
title: "Lesson 1: Create a Project and Basic Package with SSIS"
description: "Lesson 1: Create a project and basic package with SSIS"
ms.reviewer: randolphwest
ms.date: 01/23/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: tutorial
---

# Lesson 1: Create a project and basic package with SQL Server Integration Services (SSIS)

[!INCLUDE [sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

In this lesson, you create a simple extract, transform, and load (ETL) package. The package extracts data from a single flat file source, transforms the data using two lookup transformations, and writes the transformed data to a copy of the `FactCurrencyRate` fact table in the [!INCLUDE [sssampledbdwobject-md](../includes/sssampledbdwobject-md.md)] sample database. As part of this lesson, you create new packages, add and configure data source and destination connections, and work with new control flow and data flow components.

Before creating a package, you need to understand the formatting used in both the source data and the destination. Then, you're ready to define the transformations necessary to map the source data to the destination.

## Prerequisites

This tutorial relies on Microsoft SQL Server Data Tools, a set of example packages, and a sample database.

> [!NOTE]  
> [!INCLUDE [article-uses-adventureworks](../includes/article-uses-adventureworks.md)]

To install the SQL Server Data Tools, see [Install SQL Server Data Tools (SSDT) for Visual Studio](../ssdt/download-sql-server-data-tools-ssdt.md).

To download all of the lesson packages for this tutorial:

1. Navigate to [Integration Services tutorial files](https://www.microsoft.com/download/details.aspx?id=56827).
1. Select the **Download** button.
1. Select the `Creating a Simple ETL Package.zip` file, then select **Next**.
1. After the file downloads, unzip its contents to a local directory.

## Look at the source data

For this tutorial, the source data is a set of historical currency data in a flat file named `SampleCurrencyData.txt`. The source data has the following four columns: the average rate of the currency, a currency key, a date key, and the end-of-day rate.

Here's an example of the source data in the `SampleCurrencyData.txt` file:

```output
1.00070049USD9/3/05 0:001.001201442
1.00020004USD9/4/05 0:001
1.00020004USD9/5/05 0:001.001201442
1.00020004USD9/6/05 0:001
1.00020004USD9/7/05 0:001.00070049
1.00070049USD9/8/05 0:000.99980004
1.00070049USD9/9/05 0:001.001502253
1.00070049USD9/10/05 0:000.99990001
1.00020004USD9/11/05 0:001.001101211
1.00020004USD9/12/05 0:000.99970009
```

When working with flat file source data, it's important to understand how the Flat File connection manager interprets the flat file data. If the flat file source is Unicode, the Flat File connection manager defines all columns as `[DT_WSTR]` with a default column width of `50`. If the flat file source is ANSI-encoded, the columns are defined as `[DT_STR]` with a default column width of `50`. You probably have to change these defaults to make the string column types more applicable for your data. You need to look at the data type of the destination, and then choose that type within the Flat File connection manager.

## Look at the destination data

The destination for the source data is a copy of the `FactCurrencyRate` fact table in [!INCLUDE [sssampledbdwobject-md](../includes/sssampledbdwobject-md.md)]. The `FactCurrencyRate` fact table has four columns, and has relationships to two dimension tables, as shown in the following table.

| Column name | Data type | Lookup table | Lookup column |
| --- | --- | --- | --- |
| `AverageRate` | **float** | None | None |
| `CurrencyKey` | **int** (FK) <sup>1</sup> | `DimCurrency` | `CurrencyKey` (PK) <sup>2</sup> |
| `DateKey` | **int** (FK) <sup>1</sup> | `DimDate` | `DateKey` (PK) <sup>2</sup> |
| `EndOfDayRate` | **float** | None | None |

<sup>1</sup> FK: Foreign key

<sup>2</sup> PK: Primary key

## Map the source data to the destination

Our analysis of the source and destination data formats indicates that lookups are necessary for the `CurrencyKey` and `DateKey` values. The transformations that perform these lookups get those values by using the alternate keys from the `DimCurrency` and `DimDate` dimension tables.

| Flat file column | Table name | Column name | Data type |
| --- | --- | --- | --- |
| `0` | `FactCurrencyRate` | `AverageRate` | **float** |
| `1` | `DimCurrency` | `CurrencyAlternateKey` | **nchar(3)** |
| `2` | `DimDate` | `FullDateAlternateKey` | **date** |
| `3` | `FactCurrencyRate` | `EndOfDayRate` | **float** |

## Lesson tasks

- [Lesson 1-1: Create a new Integration Services project](lesson-1-1-creating-a-new-integration-services-project.md)
- [Lesson 1-2: Add and configure a Flat File connection manager](lesson-1-2-adding-and-configuring-a-flat-file-connection-manager.md)
- [Lesson 1-3: Add and configure an OLE DB connection manager](lesson-1-3-adding-and-configuring-an-ole-db-connection-manager.md)
- [Lesson 1-4: Add a Data Flow task to the package](lesson-1-4-adding-a-data-flow-task-to-the-package.md)
- [Lesson 1-5: Add and configure the Flat File source](lesson-1-5-adding-and-configuring-the-flat-file-source.md)
- [Lesson 1-6: Add and configure the Lookup transformations](lesson-1-6-adding-and-configuring-the-lookup-transformations.md)
- [Lesson 1-7: Add and configure the OLE DB destination](lesson-1-7-adding-and-configuring-the-ole-db-destination.md)
- [Lesson 1-8: Annotate and format the Lesson 1 package](lesson-1-8-making-the-lesson-1-package-easier-to-understand.md)
- [Lesson 1-9: Test the Lesson 1 package](lesson-1-9-testing-the-lesson-1-tutorial-package.md)

## Next step

> [!div class="nextstepaction"]
> [Step 1: Create a new integration services project](../integration-services/lesson-1-1-creating-a-new-integration-services-project.md)
