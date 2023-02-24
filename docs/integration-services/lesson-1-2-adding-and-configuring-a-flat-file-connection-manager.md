---
description: "Lesson 1-2: Add and configure a Flat File connection manager"
title: "Step 2: Add and configure a Flat File connection manager | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: tutorial
ms.assetid: 9a77dd32-d8c2-4961-ad37-2a971f9d6043
author: chugugrace
ms.author: chugu
---
# Lesson 1-2: Add and configure a Flat File connection manager

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]



In this task, you add a Flat File connection manager to the package that you just created. A Flat File connection manager enables a package to extract data from a flat file. Using the Flat File connection manager, you can specify the file name and location, the locale and code page, and the file format, including column delimiters, to apply when the package extracts data from the flat file. In addition, you can manually specify the data type for the individual columns, or use the **Suggest Column Types** dialog box to automatically map the columns of extracted data to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] data types.  
  
You must create a new Flat File connection manager for each file format you're working with. Because this tutorial extracts data from multiple flat files that all have the same data format, you only need to add and configure one Flat File connection manager for the example package.  
  
In this lesson, you configure the following properties in your Flat File connection manager:  
  
-   **Column names:** Since the flat file does not have column names, the Flat File connection manager creates default column names. These default names are not useful for identifying what each column represents. Change the default names to names that match the fact table into which the flat file data is to be loaded.  
  
-   **Data mappings:** The data type mappings that you specify for the Flat File connection manager are used by all flat file data source components that reference this connection manager. You can either manually map the data types by using the Flat File connection manager, or you can use the **Suggest Column Types** dialog box. In this task, you view the mappings suggested in the **Suggest Column Types** dialog box and then manually create the necessary mappings in the **Flat File Connection Manager Editor** dialog box.  
  
> [!NOTE]
> The Flat File connection manager provides locale information about the data file. If your computer is not configured to use the regional option **English (United States)**, you must set additional properties in the **Flat File Connection Manager Editor** dialog box.  
  
## Add a Flat File connection manager to the SSIS package  
  
1.  In the **Solution Explorer** pane, right-click on **Connection Managers** and select **New Connection Manager**.
1. In the **Add SSIS Connection Manager** dialog, select **FLATFILE**, then **Add**.
  
2.  In the **Flat File Connection Manager Editor** dialog box, for **Connection manager name**, enter **Sample Flat File Source Data**.  
  
3.  Select **Browse**.  
  
4.  In the **Open** dialog box, locate the **SampleCurrencyData.txt** file on your computer.  
  
5.  Clear the **Column names in the first data row** checkbox.  
  
### Set locale-sensitive properties  
  
1.  In the **Flat File Connection Manager Editor** dialog box, select **General**.  
  
2.  Set **Locale** to **English (United States)** and **Code page** to **1252**.  
  
### Rename columns in the Flat File connection manager  
  
1.  In the **Flat File Connection Manager Editor** dialog box, select **Advanced**.  
  
2.  In the property pane, make the following changes:  
  
    -   Change the **Column 0** name property to **AverageRate**.  
  
    -   Change the **Column 1** name property to **CurrencyID**.  
  
    -   Change the **Column 2** name property to **CurrencyDate**.  
  
    -   Change the **Column 3** name property to **EndOfDayRate**.  
  
### Remap column data types  
  
By default, all four of the columns are initially set to a string data type [DT_STR] with an **OutputColumnWidth** of 50.  

1.  In the **Flat File Connection Manager Editor** dialog box, select **Suggest Types**.  
  
    [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] automatically suggests appropriate data types based on the first 200 rows of data. You can also change these suggestion options to sample more or less data, to specify the default data type for integer or Boolean data, or to add spaces as padding to string columns.  
  
    For now, make no changes to the options in the **Suggest Column Types** dialog box, and select **OK** to have [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] suggest data types for columns. This action returns you to the **Advanced** pane of the **Flat File Connection Manager Editor** dialog box, where you can view the column data types suggested by [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. Alternately, if you select **Cancel**, no suggestions are made to column metadata and the default string (DT_STR) data type is used.  
  
    In this tutorial, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] suggests the data types shown in the second column of the following table for the data from the SampleCurrencyData.txt file. The fourth column provides the data types required for the columns in the destination, which are defined in a subsequent step.  
  
    |Flat File column|Suggested type|Destination column|Destination type|  
    |--------------------|------------------|----------------------|--------------------|  
    |AverageRate|float [DT_R4]|FactCurrencyRate.AverageRate|float|  
    |CurrencyID|string [DT_STR]|DimCurrency.CurrencyAlternateKey|nchar(3)|  
    |CurrencyDate|date [DT_DATE]|DimDate.FullDateAlternateKey|date|  
    |EndOfDayRate|float [DT_R4]|FactCurrencyRate.EndOfDayRate|float|  
  
    The data type suggested for the **CurrencyID** column is incompatible with the data type of the field in the destination table. Because the data type of `DimCurrency.CurrencyAlternateKey` is nchar(3), **CurrencyID** must be changed from string [DT_STR] to Unicode string [DT_WSTR]. In addition, the field `DimDate.FullDateAlternateKey` is defined as a date data type, so the type for **CurrencyDate** needs to be changed from date [DT_Date] to database date [DT_DBDATE].  
  
2.  In the list, select the **CurrencyID** column and in the property pane, change the Data Type of column **CurrencyID** from string [DT_STR] to Unicode string [DT_WSTR].  
  
3.  In the property pane, change the data type of column **CurrencyDate** from date [DT_DATE] to database date [DT_DBDATE].  
  
4.  Select **OK**.  
  
## Go to next task
[Step 3: Add and configure an OLE DB connection manager](../integration-services/lesson-1-3-adding-and-configuring-an-ole-db-connection-manager.md)  
  
## See also  
[Flat File connection manager](../integration-services/connection-manager/flat-file-connection-manager.md)  
[Integration Services data types](../integration-services/data-flow/integration-services-data-types.md)  
  
  
  
