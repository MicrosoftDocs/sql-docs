---
title: "Lesson 1: Creating the Project and Basic Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 84d0b877-603f-4f8e-bb6b-671558ade5c2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 1: Creating the Project and Basic Package
  In this lesson, you will create a simple ETL package that extracts data from a single flat file source, transforms the data using two lookup transformation components, and writes that data to the **FactCurrency** fact table in **AdventureWorksDW2012**. As part of this lesson, you will learn how to create new packages, add and configure data source and destination connections, and work with new control flow and data flow components.  
  
> [!IMPORTANT]  
>  This tutorial requires the **AdventureWorksDW2012** sample database. For more information on installing and deploying **AdventureWorksDW2012**, see [Microsoft SQL Server Product Samples: Reporting Services](https://archive.codeplex.com/?p=msftrsprodsamples).  
  
## Understanding the Package Requirements  
 This tutorial requires Microsoft SQL Server Data Tools.  
  
 For more information on installing the SQL Server Data Tools see [SQL Server Data Tools Download](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-2017).  
  
 Before creating a package, you need a good understanding of the formatting used in both the source data and the destination. Once you understand both of these data formats, you will be ready to define the transformations necessary to map the source data to the destination.  
  
### Looking at the Source  
 For this tutorial, the source data is a set of historical currency data contained in the flat file, SampleCurrencyData.txt. The source data has the following four columns: the average rate of the currency, a currency key, a date key, and the end-of-day rate.  
  
 Here is an example of the source data contained in the SampleCurrencyData.txt file:  
  
 `1.00070049USD9/3/05 0:001.001201442`  
  
 `1.00020004USD9/4/05 0:001`  
  
 `1.00020004USD9/5/05 0:001.001201442`  
  
 `1.00020004USD9/6/05 0:001`  
  
 `1.00020004USD9/7/05 0:001.00070049`  
  
 `1.00070049USD9/8/05 0:000.99980004`  
  
 `1.00070049USD9/9/05 0:001.001502253`  
  
 `1.00070049USD9/10/05 0:000.99990001`  
  
 `1.00020004USD9/11/05 0:001.001101211`  
  
 `1.00020004USD9/12/05 0:000.99970009`  
  
 When working with flat file source data, it is important to understand how the Flat File connection manager interprets the flat file data. If the flat file source is Unicode, the Flat File connection manager defines all columns as [DT_WSTR] with a default column width of 50. If the flat file source is ANSI-encoded, the columns are defined as [DT_STR] with a column width of 50. You will probably have to change these defaults to make the string column types more appropriate for your data. To do this, you will need to look at the data type of the destination where the data will be written to and then choose the correct type within the Flat File connection manager.  
  
### Looking at the Destination  
 The ultimate destination for the source data is the **FactCurrency** fact table in **AdventureWorksDW**. The **FactCurrency** fact table has four columns, and has relationships to two dimension tables, as shown in the following table.  
  
|Column Name|Data Type|Lookup Table|Lookup Column|  
|-----------------|---------------|------------------|-------------------|  
|AverageRate|float|None|None|  
|CurrencyKey|int (FK)|DimCurrency|CurrencyKey (PK)|  
|DateKey|Int (FK)|DimDate|DateKey (PK)|  
|EndOfDayRate|float|None|None|  
  
### Mapping Source Data to be Compatible with the Destination  
 Analysis of the source and destination data formats indicates that lookups will be necessary for the **CurrencyKey** and **DateKey** values. The transformations that will perform these lookups will obtain the **CurrencyKey** and **DateKey** values by using the alternate keys from **DimCurrency** and **DimDate** dimension tables.  
  
|Flat File Column|Table Name|Column Name|Data Type|  
|----------------------|----------------|-----------------|---------------|  
|0|FactCurrency|AverageRate|float|  
|1|DimCurrency|CurrencyAlternateKey|nchar (3)|  
|2|DimDate|FullDateAlternateKey|date|  
|3|FactCurrency|EndOfDayRate|float|  
  
## Lesson Tasks  
 This lesson contains the following tasks:  
  
-   [Step 1: Creating a New Integration Services Project](lesson-1-1-creating-a-new-integration-services-project.md)  
  
-   [Step 2: Adding and Configuring a Flat File Connection Manager](lesson-1-2-adding-and-configuring-a-flat-file-connection-manager.md)  
  
-   [Step 3: Adding and Configuring an OLE DB Connection Manager](lesson-1-3-adding-and-configuring-an-ole-db-connection-manager.md)  
  
-   [Step 4: Adding a Data Flow Task to the Package](lesson-1-4-adding-a-data-flow-task-to-the-package.md)  
  
-   [Step 5: Adding and Configuring the Flat File Source](lesson-1-5-adding-and-configuring-the-flat-file-source.md)  
  
-   [Step 6: Adding and Configuring the Lookup Transformations](lesson-1-6-adding-and-configuring-the-lookup-transformations.md)  
  
-   [Step 7: Adding and Configuring the OLE DB Destination](lesson-1-7-adding-and-configuring-the-ole-db-destination.md)  
  
-   [Step 8: Making the Lesson 1 Package Easier to Understand](lesson-1-8-making-the-lesson-1-package-easier-to-understand.md)  
  
-   [Step 9: Testing the Lesson 1 Tutorial Package](lesson-1-9-testing-the-lesson-1-tutorial-package.md)  
  
## Start the Lesson  
 [Step 1: Creating a New Integration Services Project](lesson-1-1-creating-a-new-integration-services-project.md)  
  
  
