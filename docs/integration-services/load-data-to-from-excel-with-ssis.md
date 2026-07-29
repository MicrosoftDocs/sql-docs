---
title: "Import from Excel or Export to Excel with SSIS"
description: "Learn how to import or export Excel data with SQL Server Integration Services (SSIS), along with prerequisites, known issues, and limitations."
ms.reviewer: mathoma
ms.date: 09/08/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
---
# Import data from Excel or export data to Excel with SQL Server Integration Services (SSIS)

[!INCLUDE [sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

This article describes the connection information that you have to provide, and the settings that you have to configure, to import data from Excel or export data to Excel with SQL Server Integration Services (SSIS).

The following sections contain the information you need to use Excel successfully with SSIS, and to understand and troubleshoot common problems:

- The [tools](#tools) you can use.

- The [files](#files-you-need) you need.

- The connection information that you have to provide, and the settings that you have to configure, when you load data from or to Excel with SSIS.
   - [Specify Excel](#specify-excel) as your data source.
   - Provide the [Excel file name and path](#excel-file).
   - Select the [Excel version](#excel-version).
   - Specify whether the [first row contains column names](#first-row).
   - Provide the [worksheet or range that contains the data](#sheets-ranges).

- Known issues and limitations.
   - Issues with [data types](#issues-types).
   - Issues with [importing](#issues-importing).
   - Issues with [exporting](#issues-exporting).
   - Issues in [unattended or non‑interactive environments](#issues-non‑interactive-environments).

<a id="tools"></a>

## Tools you can use

You can import data from Excel or export data to Excel with SSIS by using one of the following tools:

- **SQL Server Integration Services (SSIS)**. Create an SSIS package that uses the Excel Source or the Excel Destination with the Excel Connection Manager. (This article doesn't describe how to create SSIS packages.)

- The **SQL Server Import and Export Wizard**, which is built on SSIS. For more info, see [Import and Export Data with the SQL Server Import and Export Wizard](import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md) and [Connect to an Excel Data Source (SQL Server Import and Export Wizard)](import-export-data/connect-to-an-excel-data-source-sql-server-import-and-export-wizard.md).

<a id="files-you-need"></a>

## Get the files you need to connect to Excel

Before you can import data from Excel or export data to Excel with SSIS, you might have to download the connectivity components for Excel if they're not already installed. The connectivity components for Excel aren't installed by default.

Use the table within [Unable to use the Access ODBC, OLEDB or DAO interfaces outside Office Click-to-Run](/office/troubleshoot/access/cannot-use-odbc-or-oledb) to understand if additional components are necessary for your environment.

**Note:** The Office System Drivers are only supported under certain scenarios, please refer to [Considerations for server-side Automation of Office](https://support.microsoft.com/topic/considerations-for-server-side-automation-of-office-48bcfe93-8a89-47f1-0bce-017433ad79e2) for specific guidance.

<a id="specify-excel"></a>

## Specify Excel as your data source

The first step is to indicate that you want to connect to Excel.

### In SSIS

In SSIS, create an Excel Connection Manager to connect to the Excel source or destination file. There are several ways to create the connection manager:

- In the **Connection Managers** area, right-click and select **New connection**. In the **Add SSIS Connection Manager** dialog box, select **EXCEL** and then **Add**.

- On the **SSIS** menu, select **New connection**. In the **Add SSIS Connection Manager** dialog box, select **EXCEL** and then **Add**.

- Create the connection manager at the same time that you configure the **Excel Source** or the **Excel Destination** on the **Connection manager** page of the **Excel Source Editor** or of the **Excel Destination Editor**.

### In the SQL Server Import and Export Wizard

In the Import and Export Wizard, on the **Choose a Data Source** or **Choose a Destination** page, select **Microsoft Excel** in the **Data source** list.

If you don't see Excel in the list of data sources, make sure you're running the 32-bit wizard. The Excel connectivity components are typically 32-bit files and aren't visible in the 64-bit wizard.

<a id="excel-file"></a>

## Excel file and file path

The first piece of info to provide is the path and file name for the Excel file. You provide this info in the **Excel Connection Manager Editor** in an SSIS package, or on the **Choose a Data Source** or **Choose a Destination** page of the Import and Export Wizard.

Enter the path and file name in the following format:

- For a file on the local computer, **C:\\TestData.xlsx**.

- For a file on a network share, **\\\\Sales\\Data\\TestData.xlsx**.

Or, select **Browse** to locate the spreadsheet by using the **Open** dialog box.

> [!IMPORTANT]  
> You can't connect to a password-protected Excel file.

<a id="excel-version"></a>

## Excel version

The second piece of info to provide is the version of the Excel file. You provide this info in the **Excel Connection Manager Editor** in an SSIS package, or on the **Choose a Data Source** or **Choose a Destination** page of the Import and Export Wizard.

Select the version of Microsoft Excel that was used to create the file, or another compatible version. For example, if you had trouble installing the 2016 connectivity components, you can install the 2010 components and select **Microsoft Excel 2007-2010** in this list.

You might not be able to select newer Excel versions in the list if you only have older versions of the connectivity components installed. The **Excel version** list includes all the versions of Excel supported by SSIS. The presence of items in this list doesn't indicate that the required connectivity components are installed. For example, **Microsoft Excel 2016** appears in the list even if you haven't installed the 2016 connectivity components.

> [!NOTE]  
> Starting with **SQL Server Management Studio 21** and **SQL Server 2025**, the Import and Export Wizard only supports a 64-bit environment. Microsoft.JET.OLEDB.4.0 only works in 32-bit environments.

To use the Import and Export Wizard for Excel files in a 64-bit environment, download the [Microsoft Access Database Engine 2016 Redistributable](https://www.microsoft.com/download/details.aspx?id=54920&msockid=27fcaab782ad62fb1b64b9a983c86387) to install the Microsoft.ACE.OLEDB.16.0 provider. Then select **Microsoft Excel 2016** as the *Excel version* in the **SQL Server Import and Export Wizard**, such as the following screenshot: 

![Screenshot of the Import and Export Excel Wizard in SSMS with Microsoft Excel 2016 selected.](media/load-data-to-from-excel-with-sql-server-integration-services-excel-version.png)

The Microsoft.ACE.OLEDB.16.0 provider supports Excel files created by Excel 97-2003 (`.xsl`), and Excel 2007-2010, 2016 (`.xlsx`). 

<a id="first-row"></a>

## First row has column names

If you're importing data from Excel, the next step is to indicate whether the first row of the data contains column names. You provide this info in the **Excel Connection Manager Editor** in an SSIS package, or on the **Choose a Data Source** page of the Import and Export Wizard.

- If you disable this option because the source data doesn't contain column names, the wizard uses F1, F2, and so forth, as column headings.
- If the data contains column names, but you disable this option, the wizard imports the column names as the first row of data.
- If the data doesn't contain column names, but you enable this option, the wizard uses the first row of source data as the column names. In this case, the first row of source data is no longer included in the data itself.

If you're exporting data from Excel, and you enable this option, the first row of exported data includes the column names.

<a id="sheets-ranges"></a>

## Worksheets and ranges

There are three types of Excel objects that you can use as the source or destination for your data: a worksheet, a named range, or an unnamed range of cells that you specify with its address.

- **Worksheet.** To specify a worksheet, append the `$` character to the end of the sheet name and add delimiters around the string - for example, **[Sheet1$]**. Or, look for a name that ends with the `$` character in the list of existing tables and views.

- **Named range.** To specify a named range, provide the range name - for example, **MyDataRange**. Or, look for a name that doesn't end with the `$` character in the list of existing tables and views.

- **Unnamed range.** To specify a range of cells that you haven't named, append the $ character to the end of the sheet name, add the range specification, and add delimiters around the string - for example, **[Sheet1$A1:B4]**.

To select or specify the type of Excel object that you want to use as the source or destination for your data, do one of the following things:

### In SSIS

In SSIS, on the **Connection manager** page of the **Excel Source Editor** or of the **Excel Destination Editor**, do one of the following things:

- To use a **worksheet** or a **named range**, select **Table or view** as the **Data access mode**. Then, in the **Name of the Excel sheet** list, select the worksheet or named range.

- To use an **unnamed range** that you specify with its address, select **SQL command** as the **Data access mode**. Then, in the **SQL command text** field, enter a query like the following example:

  ```sql
  SELECT * FROM [Sheet1$A1:B5]
  ```

### In the SQL Server Import and Export Wizard

In the Import and Export Wizard, do one of the following things:

- When you're **importing** from Excel, do one of the following things:

  - To use a **worksheet** or a **named range**, on the **Specify table copy or query** page, select **Copy data from one or more tables or views**. Then, on the **Select Source Tables and Views** page, in the **Source** column, select the source worksheets and named ranges.

  - To use an **unnamed range** that you specify with its address, on the **Specify table copy or query** page, select **Write a query to specify the data to transfer**. Then, on the **Provide a Source Query** page, provide a query similar to the following example:

    ```sql
    SELECT * FROM [Sheet1$A1:B5]
    ```

- When you're **exporting** to Excel, do one of the following things:

  - To use a **worksheet** or a **named range**, on the **Select Source Tables and Views** page, in the **Destination** column, select the destination worksheets and named ranges.

  - To use an **unnamed range** that you specify with its address, on the **Select Source Tables and Views** page, in the **Destination** column, enter the range in the following format without delimiters: `Sheet1$A1:B5`. The wizard adds the delimiters.

After you select or enter the Excel objects to import or export, you can also do the following things on the **Select Source Tables and Views** page of the wizard:

- Review column mappings between source and destination by selecting **Edit Mappings**.

- Preview sample data to make sure it's what you expect by selecting **Preview**.

<a id="issues-non‑interactive-environments"></a>
## Issues in Unattended or Non‑Interactive Environments

When SQL Server Integration Services (SSIS) packages use Excel Connection Managers and are executed in unattended or non‑interactive environments—such as SQL Agent Jobs, the SSIS Catalog, or other server‑side automation—they may fail with connection or provider‑related errors.

This occurs because Microsoft Office/Excel components (including ACE/Jet providers) are not supported for automation or use in service contexts. These components require an interactive desktop session and may behave unpredictably when invoked by background processes.

Microsoft Office has long provided official guidance that server-side or unattended automation of Office applications is unsupported, including Excel data providers.

### Recommended Alternatives

To ensure reliability in production and automated ETL pipelines, Microsoft recommends replacing Excel Connection Managers with one of the following supported, server‑safe alternatives:

- **Flat file sources (CSV/TXT)** - Ideal for structured tabular data and fully supported for unattended SSIS execution.

- **OLE DB or ODBC connections** - Use when the source data can be placed in a supported database or exposed via an OLE DB/ODBC provider.

- **OpenXML or ADO.NET–based approaches** - Suitable when Excel files must be consumed directly without relying on Office automation or ACE.

These options do not depend on Office binaries and provide predictable, reliable behavior in server environments.

### Further Reference

For detailed information on Office automation supportability, please see [Considerations for server-side Automation of Office](https://support.microsoft.com/en-us/topic/considerations-for-server-side-automation-of-office-48bcfe93-8a89-47f1-0bce-017433ad79e2).

<a id="issues-types"></a>
## Issues with data types

### Data types

The Excel driver recognizes only a limited set of data types. For example, all numeric columns are interpreted as doubles (DT_R8), and all string columns (other than memo columns) are interpreted as 255-character Unicode strings (DT_WSTR). SSIS maps the Excel data types as follows:

- Numeric - double-precision float (DT_R8)

- Currency - currency (DT_CY)

- Boolean - Boolean (DT_BOOL)

- Date/time - datetime (DT_DATE)

- String - Unicode string, length 255 (DT_WSTR)

- Memo - Unicode text stream (DT_NTEXT)

### Data type and length conversions

SSIS doesn't implicitly convert data types. As a result, you might have to use Derived Column or Data Conversion transformations to convert Excel data explicitly before loading it into a destination other than Excel, or to convert data from a source other than Excel before loading it into an Excel destination.

Here are some examples of the conversions that might be required:

- Conversion between Unicode Excel string columns and non-Unicode string columns with specific codepage.

- Conversion between 255-character Excel string columns and string columns of different lengths.

- Conversion between double-precision Excel numeric columns and numeric columns of other types.

> [!TIP]  
> If you're using the Import and Export Wizard, and your data requires some of these conversions, the wizard configures the necessary conversions for you. As a result, even when you want to use an SSIS package, it might be useful to create the initial package by using the Import and Export Wizard. Let the wizard create and configure connection managers, sources, transformations, and destinations for you.

<a id="issues-importing"></a>

## Issues with importing

### Empty rows

When you specify a worksheet or a named range as the source, the driver reads the *contiguous* block of cells starting with the first non-empty cell in the upper-left corner of the worksheet or range. As a result, your data doesn't have to start in row 1, but you can't have empty rows in the source data. For example, you can't have an empty row between the column headers and the data rows, or a title followed by empty rows at the top of the worksheet.

If there are empty rows above your data, you can't query the data as a worksheet. In Excel, you have to select your range of data and assign a name to the range, and then query the named range instead of the worksheet.

### Missing values

The Excel driver reads a certain number of rows (by default, eight rows) in the specified source to guess at the data type of each column. When a column appears to contain mixed data types, especially numeric data mixed with text data, the driver decides in favor of the majority data type, and returns null values for cells that contain data of the other type. (In a tie, the numeric type wins.) Most cell formatting options in the Excel worksheet don't seem to affect this data type determination.

You can modify this behavior of the Excel driver by specifying Import Mode to import all values as text. To specify Import Mode, add `IMEX=1` to the value of **Extended Properties** in the connection string of the Excel connection manager in the Properties window.

### Truncated text

When the driver determines that an Excel column contains text data, the driver selects the data type (string or memo) based on the longest value that it samples. If the driver doesn't discover any values longer than 255 characters in the rows that it samples, it treats the column as a 255-character string column instead of a memo column. Therefore, values longer than 255 characters might be truncated.

To import data from a memo column without truncation, you have two options:

- Make sure that the memo column in at least one of the sampled rows contains a value longer than 255 characters

- Increase the number of rows sampled by the driver to include such a row. You can increase the number of rows sampled by increasing the value of **TypeGuessRows** under the following registry key:

| Redistributable components version | Registry key |
|---|---|
| Excel 2016 | HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Office\16.0\Access Connectivity Engine\Engines\Excel |
| Excel 2010 | HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Office\14.0\Access Connectivity Engine\Engines\Excel |

<a id="issues-exporting"></a>

## Issues with exporting

### Create a new destination file

#### In SSIS

Create an Excel Connection Manager with the path and file name of the new Excel file that you want to create. Then, in the **Excel Destination Editor**, for **Name of the Excel sheet**, select **New** to create the destination worksheet. At this point, SSIS creates the new Excel file with the specified worksheet.

#### In the SQL Server Import and Export Wizard

On the **Choose a Destination** page, select **Browse**. In the **Open** dialog box, navigate to the folder where you want the new Excel file to be created, provide a name for the new file, and then select **Open**.

### Export to a large enough range

When you specify a range as the destination, an error occurs if the range has fewer *columns* than the source data. However, if the range that you specify has fewer *rows* than the source data, the wizard continues writing rows without error and extends the range definition to match the new number of rows.

### Export long text values

Before you can successfully save strings longer than 255 characters to an Excel column, the driver must recognize the data type of the destination column as **memo** and not **string**.

- If an existing destination table already contains rows of data, then the first few rows that are sampled by the driver must contain at least one instance of a value longer than 255 characters in the memo column.

## Related content

- [Excel Connection Manager](connection-manager/excel-connection-manager.md)
- [Excel Source](data-flow/excel-source.md)
- [Excel Destination](data-flow/excel-destination.md)
- [Loop through Excel Files and Tables with a Foreach Loop Container](control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md)
- [Working with Excel Files with the Script Task](extending-packages-scripting-task-examples/working-with-excel-files-with-the-script-task.md)
- [Connect to an Excel Data Source (SQL Server Import and Export Wizard)](import-export-data/connect-to-an-excel-data-source-sql-server-import-and-export-wizard.md)
- [Get started with this simple example of the Import and Export Wizard](import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md)
- [Import data from Excel to SQL Server or Azure SQL Database](../relational-databases/import-export/import-data-from-excel-to-sql.md)
