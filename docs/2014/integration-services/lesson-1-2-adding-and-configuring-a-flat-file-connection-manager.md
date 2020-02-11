---
title: "Step 2: Adding and Configuring a Flat File Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 9a77dd32-d8c2-4961-ad37-2a971f9d6043
author: janinezhang
ms.author: janinez
manager: craigg
---
# Step 2: Adding and Configuring a Flat File Connection Manager
  In this task, you add a Flat File connection manager to the package that you just created. A Flat File connection manager enables a package to extract data from a flat file. Using the Flat File connection manager, you can specify the file name and location, the locale and code page, and the file format, including column delimiters, to apply when the package extracts data from the flat file. In addition, you can manually specify the data type for the individual columns, or use the **Suggest Column Types** dialog box to automatically map the columns of extracted data to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] data types.  
  
 You must create a new Flat File connection manager for each file format that you work with. Because this tutorial extracts data from multiple flat files that have exactly the same data format, you will need to add and configure only one Flat File connection manager for your package.  
  
 For this tutorial, you will configure the following properties in your Flat File connection manager:  
  
-   **Column names:** Because the flat file does not have column names, the Flat File connection manager creates default column names. These default names are not useful for identifying what each column represents. To make these default names more useful, you need to change the default names to names that match the fact table into which the flat file data is to be loaded.  
  
-   **Data mappings:** The data type mappings that you specify for the Flat File connection manager will be used by all flat file data source components that reference the connection manager. You can either manually map the data types by using the Flat File connection manager, or you can use the **Suggest Column Types** dialog box. In this tutorial, you will view the mappings suggested in the **Suggest Column Types** dialog box and then manually make the necessary mappings in the **Flat File Connection Manager Editor** dialog box.  
  
 The Flat File connection manager provides locale information about the data file. If your computer is not configured to use the regional option English (United States), you must set additional properties in the **Flat File Connection Manager Editor** dialog box.  
  
### To add a Flat File connection manager to the SSIS package  
  
1.  Right-click anywhere in the **Connection Managers** area, and then click **New Flat File Connection**.  
  
2.  In the **Flat File Connection Manager Editor** dialog box, for **Connection manager name**, type **Sample Flat File Source Data**.  
  
3.  Click **Browse**.  
  
4.  In the **Open** dialog box, locate the SampleCurrencyData.txt file on your machine.  
  
     The sample data is included with the [!INCLUDE[ssIS](../includes/ssis-md.md)] lesson packages. To download the sample data and the lesson packages, do the following.  
  
    1.  Navigate to [Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=275027)  
  
    2.  Click the **DOWNLOADS** tab.  
  
    3.  Click the  SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.  
  
5.  Clear the Column names in the first data row checkbox.  
  
### To set locale sensitive properties  
  
1.  In the **Flat File Connection Manager Editor** dialog box, click **General**.  
  
2.  Set **Locale** to English (United States) and **Code page** to 1252.  
  
### To rename columns in the Flat File connection manager  
  
1.  In the **Flat File Connection Manager Editor** dialog box, click **Advanced**.  
  
2.  In the property pane, make the following changes:  
  
    -   Change the **Column 0** name property to `AverageRate`.  
  
    -   Change the **Column 1** name property to `CurrencyID`.  
  
    -   Change the **Column 2** name property to `CurrencyDate`.  
  
    -   Change the **Column 3** name property to `EndOfDayRate`.  
  
    > [!NOTE]  
    >  By default, all four of the columns are initially set to a string data type [DT_STR] with an `OutputColumnWidth` of 50.  
  
### To remap column data types  
  
1.  In the **Flat File Connection Manager Editor** dialog box, click **Suggest Types**.  
  
     [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] automatically suggests the most appropriate data types based on the first 200 rows of data. You can also change these suggestion options to sample more or less data, to specify the default data type for integer or Boolean data, or to add spaces as padding to string columns.  
  
     For now, make no changes to the options in the **Suggest Column Types** dialog box, and click **OK** to have [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] suggest data types for columns. This returns you to the **Advanced** pane of the **Flat File Connection Manager Editor** dialog box, where you can view the column data types suggested by [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. (If you click **Cancel**, no suggestions are made to column metadata and the default string (DT_STR) data type is used.)  
  
     In this tutorial, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] suggests the data types shown in the second column of the following table for the data from the SampleCurrencyData.txt file. However, the data types that are required for the columns in the destination, which will be defined in a later step, are shown in the last column of the following table.  
  
    |Flat File Column|Suggested Type|Destination Column|Destination Type|  
    |----------------------|--------------------|------------------------|----------------------|  
    |AverageRate|float [DT_R4]|FactCurrency.AverageRate|float|  
    |CurrencyID|string [DT_STR]|DimCurrency.CurrencyAlternateKey|nchar(3)|  
    |CurrencyDate|date [DT_DATE]|DimDate.FullDateAlternateKey|date|  
    |EndOfDayRate|float [DT_R4]|FactCurrency.EndOfDayRate|float|  
  
     The data type suggested for the `CurrencyID` column is incompatible with the data type of the field in the destination table. Because the data type of `DimCurrency.CurrencyAlternateKey` is nchar (3), `CurrencyID` must be changed from string [DT_STR] to string [DT_WSTR]. Additionally, the field `DimDate.FullDateAlternateKey` is defined as a date data type; therefore, `CurrencyDate` needs to be changed from date [DT_Date] to database date [DT_DBDATE].  
  
2.  In the list, select the CurrencyID column and in the property pane, change the Data Type of column `CurrencyID` from string [DT_STR] to Unicode string [DT_WSTR].  
  
3.  In the property pane, change the data type of column `CurrencyDate` from date [DT_DATE] to database date [DT_DBDATE].  
  
4.  Click **OK**.  
  
## Next Task in Lesson  
 [Step 3: Adding and Configuring an OLE DB Connection Manager](lesson-1-3-adding-and-configuring-an-ole-db-connection-manager.md)  
  
## See Also  
 [Flat File Connection Manager](connection-manager/file-connection-manager.md)   
 [Integration Services Data Types](data-flow/integration-services-data-types.md)  
  
  
