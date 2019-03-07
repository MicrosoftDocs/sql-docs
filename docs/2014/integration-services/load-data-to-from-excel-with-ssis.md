---
title: "Import from Excel or export to Excel with SSIS | Microsoft Docs"
description: "Learn how to import or export Excel data with SQL Server Integration Services (SSIS), along with prerequisites, known issues, and limitations."
ms.date: "04/10/2018"
ms.prod: sql-server-2014
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.custom: ""
ms.technology: integration-services
ms.topic: conceptual
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Import data from Excel or export data to Excel with SQL Server Integration Services (SSIS)

This article describes how to import data from Excel or export data to Excel with SQL Server Integration Services (SSIS). The article also describes prerequisites, limitations, and known issues.

You can import data from Excel or export data to Excel by creating an SSIS package and using the Excel Connection Manager and the Excel Source or the Excel Destination. You can also use the SQL Server Import and Export Wizard, which is built on SSIS.

This article contains the three sets of information you need to use Excel successfully from SSIS or to understand and troubleshoot common problems:
1.  [The files you need](#files-you-need).
2.  The information you have to provide when you load data from or to Excel.
    -   [Specify Excel](#specify-excel) as your data source.
    -   Provide the [Excel file name and path](#excel-file).
    -   Select the [Excel version](#excel-version).
    -   Specify whether the [first row contains column names](#first-row).
    -   Provide the [worksheet or range that contains the data](#sheets-ranges).
3.  Known issues and limitations.
    -   Issues with [data types](#issues-types).
    -   Issues with [importing](#issues-importing).
    -   Issues with [exporting](#issues-exporting).

## <a name="files-you-need"></a> Get the files you need to connect to Excel

Before you can import data from Excel or export data to Excel, you may have to download the connectivity components for Excel if they're not already installed. The connectivity components for Excel are not installed by default.

Download the latest version of the connectivity components for Excel here:
[Microsoft Access Database Engine 2016 Redistributable](https://www.microsoft.com/download/details.aspx?id=54920).
  
The latest version of the components can open files created by earlier versions of Excel.

Make sure that you download the Access Database Engine 2016 *Redistributable* and not the Microsoft Access 2016 *Runtime*.

If the computer already has a 32-bit version of Office, then you have to install the 32-bit version of the components. You also have to ensure that you run the SSIS package in 32-bit mode, or run the 32-bit version of the Import and Export Wizard.

If you have an Office 365 subscription, you may see an error message when you run the installer. The error indicates that you can't install the download side by side with Office click-to-run components. To bypass this error message, run the installation in quiet mode by opening a Command Prompt window and running the .EXE file that you downloaded with the `/quiet` switch. For example:

`C:\Users\<user name>\Downloads\AccessDatabaseEngine.exe /quiet`

If you have trouble installing the 2016 redistributable, install the 2010 redistributable instead from here: [Microsoft Access Database Engine 2010 Redistributable](https://www.microsoft.com/download/details.aspx?id=13255). (There is no redistributable for Excel 2013.)

## <a name="specify-excel"></a> Specify Excel

The first step is to indicate that you want to connect to Excel.

### In SSIS
In SSIS, create an Excel Connection Manager to connect to the Excel source or destination file. There are several ways to create the connection manager:

-   In the **Connection Managers** area, right-click and select **New connection**. In the **Add SSIS Connection Manager** dialog box, select **EXCEL** and then **Add**.
 
-   On the **SSIS** menu, select **New connection**. In the **Add SSIS Connection Manager** dialog box, select **EXCEL** and then **Add**.

-   Create the connection manager at the same time that you configure the **Excel Source** or the **Excel Destination** on the **Connection manager** page of the **Excel Source Editor** or of the **Excel Destination Editor**.

### In the SQL Server Import and Export Wizard
In the Import and Export Wizard, on the **Choose a Data Source** or **Choose a Destination** page, select **Microsoft Excel** in the **Data source** list.

If you don't see Excel in the list of data sources, make sure you're running the 32-bit wizard. The Excel connectivity components are typically 32-bit files and aren't visible in the 64-bit wizard.

## <a name="excel-file"></a> Excel file and file path

The first piece of info to provide is the path and file name for the Excel file. You provide this info in the **Excel Connection Manager Editor** in an SSIS package, or on the **Choose a Data Source** or **Choose a Destination** page of the Import and Export Wizard.

Enter the path and file name in the following format:

-   For a file on the local computer, **C:\\TestData.xlsx**.

-   For a file on a network share, **\\\\Sales\\Data\\TestData.xlsx**.

Or, click **Browse** to locate the spreadsheet by using the **Open** dialog box.  
  
> [!IMPORTANT]
> You can't connect to a password-protected Excel file.

## <a name="excel-version"></a> Excel version

The second piece of info to provide is the version of the Excel file. You provide this info in the **Excel Connection Manager Editor** in an SSIS package, or on the **Choose a Data Source** or **Choose a Destination** page of the Import and Export Wizard.

Select the version of Microsoft Excel that was used to create the file, or another compatible version. For example, if you had trouble installing the 2016 connectivity components, you can install the 2010 components and select **Microsoft Excel 2007-2010** in this list.

You may not be able to select newer Excel versions in the list if you only have older versions of the connectivity components installed. The **Excel version** list includes all the versions of Excel supported by SSIS. The presence of items in this list does not indicate that the required connectivity components are installed. For example, **Microsoft Excel 2016** appears in the list even if you have not installed the 2016 connectivity components.

## <a name="first-row"></a> First row has column names

If you're importing data from Excel, the next step is to indicate whether the first row of the data contains column names. You provide this info in the **Excel Connection Manager Editor** in an SSIS package, or on the **Choose a Data Source** page of the Import and Export Wizard.

-   If you disable this option because the source data doesn't contain column names, the wizard uses F1, F2, and so forth, as column headings.
-   If the data contains column names, but you disable this option, the wizard imports the column names as the first row of data.
-   If the data does not contain column names, but you enable this option, the wizard uses the first row of source data as the column names. In this case, the first row of source data is no longer included in the data itself.

If you're exporting data from Excel, and you enable this option, the first row of exported data includes the column names.

## <a name="sheets-ranges"></a> Worksheets and ranges

There are three types of Excel objects that you can use as the source or destination for your data: a worksheet, a named range, or an unnamed range of cells that you specify with its address.

-   **Worksheet.** To specify a worksheet, append the `$` character to the end of the sheet name and add delimiters around the string - for example, **[Sheet1$]**. Or, look for a name that ends with the `$` character in the list of existing tables and views.

-   **Named range.** To specify a named range, provide the range name - for example, **MyDataRange**. Or, look for a name that does not end with the `$` character in the list of existing tables and views.
    
-   **Unnamed range.** To specify a range of cells that you haven't named, append the $ character to the end of the sheet name, add the range specification, and add delimiters around the string - for example, **[Sheet1$A1:B4]**.

To select or specify the type of Excel object that you want to use as the source or destination for your data, do one of the following things:

### In SSIS

In SSIS, on the **Connection manager** page of the **Excel Source Editor** or of the **Excel Destination Editor**, do one of the following things:

-   To use a **worksheet** or a **named range**, select **Table or view** as the **Data access mode**. Then, in the **Name of the Excel sheet** list, select the worksheet or named range.

-   To use an **unnamed range** that you specify with its address, select **SQL command** as the **Data access mode**. Then, in the **SQL command text** field, enter a query like the following example:

    ```sql
    SELECT * FROM [Sheet1$A1:B5]
    ```

### In the SQL Server Import and Export Wizard
In the Import and Export Wizard, do one of the following things:

-   When you're **importing** from Excel, do one of the following things:

    -   To use a **worksheet** or a **named range**, on the **Specify table copy or query** page, select **Copy data from one or more tables or views**. Then, on the **Select Source Tables and Views** page, in the **Source** column, select the source worksheets and named ranges.

    -   To use an **unnamed range** that you specify with its address, on the **Specify table copy or query** page, select **Write a query to specify the data to transfer**. Then, on the **Provide a Source Query** page, provide a query similar to the following example:

        ```sql
        SELECT * FROM [Sheet1$A1:B5]
        ```

-   When you're **exporting** to Excel, do one of the following things:

    -   To use a **worksheet** or a **named range**, on the **Select Source Tables and Views** page, in the **Destination** column, select the destination worksheets and named ranges.

    -   To use an **unnamed range** that you specify with its address, on the **Select Source Tables and Views** page, in the **Destination** column, enter the range in the following format without delimiters: `Sheet1$A1:B5`. The wizard adds the delimiters.

After you select or enter the Excel objects to import or export, you can also do the following things on the **Select Source Tables and Views** page of the wizard:

-   Review column mappings between source and destination by selecting **Edit Mappings**.

-   Preview sample data to make sure it's what you expect by selecting **Preview**.

## <a name="issues-types"></a>Issues with data types

### Data types

The Excel driver recognizes only a limited set of data types. For example, all numeric columns are interpreted as doubles (DT_R8), and all string columns (other than memo columns) are interpreted as 255-character Unicode strings (DT_WSTR). SSIS maps the Excel data types as follows:

-   Numeric - double-precision float (DT_R8)

-   Currency - currency (DT_CY)

-   Boolean - Boolean (DT_BOOL)

-   Date/time - datetime (DT_DATE)

-   String - Unicode string, length 255 (DT_WSTR)

-   Memo - Unicode text stream (DT_NTEXT)

### Data type and length conversions

SSIS does not implicitly convert data types. As a result, you may have to use Derived Column or Data Conversion transformations to convert Excel data explicitly before loading it into a destination other than Excel, or to convert data from a source other than Excel before loading it into an Excel destination.

Here are some examples of the conversions that may be required:  
  
-   Conversion between Unicode Excel string columns and non-Unicode string columns with specific codepage.
  
-   Conversion between 255-character Excel string columns and string columns of different lengths.
  
-   Conversion between double-precision Excel numeric columns and numeric columns of other types.

> [!TIP]
> If you're using the Import and Export Wizard, and your data requires some of these conversions, the wizard configures the necessary conversions for you. As a result, even when you want to use an SSIS package, it may be useful to create the initial package by using the Import and Export Wizard. Let the wizard create and configure connection managers, sources, transformations, and destinations for you.

## <a name="issues-importing"></a> Issues with importing

### Empty rows

When you specify a worksheet or a named range as the source, the driver reads the *contiguous* block of cells starting with the first non-empty cell in the upper-left corner of the worksheet or range. As a result, your data doesn't have to start in row 1, but you can't have empty rows in the source data. For example, you can't have an empty row between the column headers and the data rows, or a title followed by empty rows at the top of the worksheet.

If there are empty rows above your data, you can't query the data as a worksheet. In Excel, you have to select your range of data and assign a name to the range, and then query the named range instead of the worksheet.

### Missing values

The Excel driver reads a certain number of rows (by default, eight rows) in the specified source to guess at the data type of each column. When a column appears to contain mixed data types, especially numeric data mixed with text data, the driver decides in favor of the majority data type, and returns null values for cells that contain data of the other type. (In a tie, the numeric type wins.) Most cell formatting options in the Excel worksheet do not seem to affect this data type determination.

You can modify this behavior of the Excel driver by specifying Import Mode to import all values as text. To specify Import Mode, add `IMEX=1` to the value of **Extended Properties** in the connection string of the Excel connection manager in the Properties window. 

### Truncated text

When the driver determines that an Excel column contains text data, the driver selects the data type (string or memo) based on the longest value that it samples. If the driver does not discover any values longer than 255 characters in the rows that it samples, it treats the column as a 255-character string column instead of a memo column. Therefore, values longer than 255 characters may be truncated.

To import data from a memo column without truncation, you have two options:

-   Make sure that the memo column in at least one of the sampled rows contains a value longer than 255 characters

-   Increase the number of rows sampled by the driver to include such a row. You can increase the number of rows sampled by increasing the value of **TypeGuessRows** under the following registry key:

| Redistributable components version | Registry key |
|---|---|
| Excel 2016 | HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Office\16.0\Access Connectivity Engine\Engines\Excel |
| Excel 2010 | HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Office\14.0\Access Connectivity Engine\Engines\Excel |
| | |

## <a name="issues-exporting"></a> Issues with exporting

### Create a new destination file

#### In SSIS

Create an Excel Connection Manager with the path and file name of the new Excel file that you want to create. Then, in the **Excel Destination Editor**, for **Name of the Excel sheet**, select **New** to create the destination worksheet. At this point, SSIS creates the new Excel file with the specified worksheet.

#### In the SQL Server Import and Export Wizard

On the **Choose a Destination** page, select **Browse**. In the **Open** dialog box, navigate to the folder where you want the new Excel file to be created, provide a name for the new file, and then select **Open**.

### Export to a large enough range

When you specify a range as the destination, an error occurs if the range has fewer *columns* than the source data. However, if the range that you specify has fewer *rows* than the source data, the wizard continues writing rows without error and extends the range definition to match the new number of rows.

### Export long text values

Before you can successfully save strings longer than 255 characters to an Excel column, the driver must recognize the data type of the destination column as **memo** and not **string**.

-   If an existing destination table already contains rows of data, then the first few rows that are sampled by the driver must contain at least one instance of a value longer than 255 characters in the memo column.

-   If a new destination table is created during package design or at run time or by the Import and Export Wizard, then the `CREATE TABLE` statement must use LONGTEXT (or one of its synonyms) as the data type of the destination memo column. In the wizard, check the `CREATE TABLE` statement and revise it, if necessary, by clicking **Edit SQL** next to the **Create destination table** option on the **Column Mappings** page.

## Related content

For more information about the components and procedures described in this article, see the following articles:

### About SSIS
[Excel Connection Manager](connection-manager/excel-connection-manager.md)  
[Excel Source](data-flow/excel-source.md)  
[Excel Destination](data-flow/excel-destination.md)  
[Loop through Excel Files and Tables by Using a Foreach Loop Container](control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md)  
[Working with Excel Files with the Script Task](extending-packages-scripting-task-examples/working-with-excel-files-with-the-script-task.md)

### About the SQL Server Import and Export Wizard
[Connect to an Excel Data Source](/sql/integration-services/import-export-data/connect-to-an-excel-data-source-sql-server-import-and-export-wizard)  
[Get started with this simple example of the Import and Export Wizard](/sql/integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard)

### Other articles
[Import data from Excel to SQL Server or Azure SQL Database](/sql/relational-databases/import-export/import-data-from-excel-to-sql)  

