---
description: "Lesson 1: Create a project and basic package with SSIS"
title: "Lesson 1: Create a project and basic package with SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: tutorial
ms.assetid: 84d0b877-603f-4f8e-bb6b-671558ade5c2
author: chugugrace
ms.author: chugu
---
# Lesson 1: Create a project and basic package with SSIS

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]



In this lesson, you create a simple ETL package that extracts data from a single flat file source, transforms the data using two lookup transformations, and writes the transformed data to a copy of the **FactCurrencyRate** fact table in the **AdventureWorksDW2012** sample database. As part of this lesson, you learn how to create new packages, add and configure data source and destination connections, and work with new control flow and data flow components.  
  
Before creating a package, you need to understand the formatting used in both the source data and the destination. Then, you be ready to define the transformations necessary to map the source data to the destination.  

## Prerequisites

This tutorial relies on Microsoft SQL Server Data Tools, a set of example packages, and a sample database.

* To install the SQL Server Data Tools, see [Download SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md).  
  
* To download all of the lesson packages for this tutorial:

    1.  Navigate to [Integration Services tutorial files](https://www.microsoft.com/download/details.aspx?id=56827).

    2.  Select the **DOWNLOAD** button.

    3.  Select the **Creating a Simple ETL Package.zip** file, then select **Next**.

    4.  After the file downloads, unzip its contents to a local directory.  

* To install and deploy the **AdventureWorksDW2012** sample database, see [Install and configure AdventureWorks sample database - SQL](../samples/adventureworks-install-configure.md).
  
## Look at the source data
For this tutorial, the source data is a set of historical currency data in a flat file named **SampleCurrencyData.txt**. The source data has the following four columns: the average rate of the currency, a currency key, a date key, and the end-of-day rate.  
  
Here is an example of the source data in the SampleCurrencyData.txt file:  
  
```
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
  
When working with flat file source data, it's important to understand how the Flat File connection manager interprets the flat file data. If the flat file source is Unicode, the Flat File connection manager defines all columns as [DT_WSTR] with a default column width of 50. If the flat file source is ANSI-encoded, the columns are defined as [DT_STR] with a default column width of 50. You probably have to change these defaults to make the string column types more applicable for your data. You need to look at the data type of the destination, and then choose that type within the Flat File connection manager.  
  
## Look at the destination data
The destination for the source data is a copy of the **FactCurrencyRate** fact table in **AdventureWorksDW**. The **FactCurrencyRate** fact table has four columns, and has relationships to two dimension tables, as shown in the following table.  
  
|Column Name|Data Type|Lookup Table|Lookup Column|  
|---------------|-------------|----------------|-----------------|  
|AverageRate|float|None|None|  
|CurrencyKey|int (FK)|DimCurrency|CurrencyKey (PK)|  
|DateKey|int (FK)|DimDate|DateKey (PK)|  
|EndOfDayRate|float|None|None|  
  
## Map the source data to the destination  
Our analysis of the source and destination data formats indicates that lookups are necessary for the **CurrencyKey** and **DateKey** values. The transformations that perform these lookups get those values by using the alternate keys from the **DimCurrency** and **DimDate** dimension tables.  
  
|flat file Column|Table Name|Column Name|Data Type|  
|--------------------|--------------|---------------|-------------|  
|0|FactCurrencyRate|AverageRate|float|  
|1|DimCurrency|CurrencyAlternateKey|nchar (3)|  
|2|DimDate|FullDateAlternateKey|date|  
|3|FactCurrencyRate|EndOfDayRate|float|  
  
## Lesson tasks  
This lesson contains the following tasks:  
  
-   [Step 1: Create a new Integration Services project](../integration-services/lesson-1-1-creating-a-new-integration-services-project.md)  
  
-   [Step 2: Add and configure a Flat File connection manager](../integration-services/lesson-1-2-adding-and-configuring-a-flat-file-connection-manager.md)  
  
-   [Step 3: Add and configure an OLE DB connection manager](../integration-services/lesson-1-3-adding-and-configuring-an-ole-db-connection-manager.md)  
  
-   [Step 4: Add a Data Flow task to the package](../integration-services/lesson-1-4-adding-a-data-flow-task-to-the-package.md)  
  
-   [Step 5: Add and configure the flat file source](../integration-services/lesson-1-5-adding-and-configuring-the-flat-file-source.md)  
  
-   [Step 6: Add and configure the lookup transformations](../integration-services/lesson-1-6-adding-and-configuring-the-lookup-transformations.md)  
  
-   [Step 7: Add and configure the OLE DB destination](../integration-services/lesson-1-7-adding-and-configuring-the-ole-db-destination.md)  
  
-   [Step 8: Annotate and format the Lesson 1 package](../integration-services/lesson-1-8-making-the-lesson-1-package-easier-to-understand.md)  
  
-   [Step 9: Test the Lesson 1 package](../integration-services/lesson-1-9-testing-the-lesson-1-tutorial-package.md)  
  
## Start the lesson  
[Step 1: Create a new integration services project](../integration-services/lesson-1-1-creating-a-new-integration-services-project.md)  
  
