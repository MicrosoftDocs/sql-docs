---
title: "Import Flat File to SQL"
description: Import Flat File Wizard is a simple way to copy data from a .csv or .txt file to a new database table. This article shows you how and when to use the wizard.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: "09/26/2017"
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.importflatfile.f1"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Import Flat File to SQL Wizard
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
> For content related to the Import and Export Wizard, see [SQL Server Import and Export Wizard](../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md).

Import Flat File Wizard is a simple way to copy data from a flat file (.csv, .txt) to a new table in your database.  The Import Flat File Wizard supports both comma-separated and fixed width format files. This overview describes the reasons for using this wizard, how to find this wizard, and a simple example to follow.

## Why would I use this wizard?
This wizard was created to improve the current import experience leveraging an intelligent framework known as Program Synthesis using Examples ([PROSE](https://microsoft.github.io/prose/)). For a user without specialized domain knowledge, importing data can often be a complex, error prone, and tedious task. This wizard streamlines the import process as simple as selecting an input file and unique table name, and the PROSE framework handles the rest.

PROSE analyzes data patterns in your input file to infer column names, types, delimiters, and more. This framework learns the structure of the file and does all of the hard work so users don't have to.

To further understand the user experience improvement of the Import Flat File Wizard, check out this video:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-new-Import-Flat-File-Wizard-in-SSMS-173/player?WT.mc_id=dataexposed-c9-niner]

## Prerequisites
This feature is available on SQL Server Management Studio (SSMS) v17.3 or later. Make sure you are using the latest version. You can find the latest version [here.](../../ssms/download-sql-server-management-studio-ssms.md)
 
## <a id="started"></a>Getting Started
To access the Import Flat File Wizard, follow these steps:

1. Open **SQL Server Management Studio**.
2. Connect to an instance of the SQL Server Database Engine or localhost.
3. Expand **Databases**, right-click a database (test in the example below), point to **Tasks**, and click **Import Flat File** above Import Data.

![Wizard menu](media/import-flat-file-wizard/import-flat-file-menu.png)

To learn more about the different functions of the wizard, refer to the following tutorial:

## Tutorial
For the purposes of this tutorial, feel free to use your own flat file. Otherwise, this tutorial is using the following CSV from Excel, which you are free to copy. If you use this CSV, title it **example.csv** and make sure to save it as a csv in an easy location such as your desktop.

![Wizard Excel](media/import-flat-file-wizard/import-flat-file-example.png)

Overview:
1. [Access Wizard](#step-1-access-wizard-and-intro-page)
2. [Specify Input File](#step-2-specify-input-file)
3. [Preview Data](#step-3-preview-data)
4. [Modify Columns](#step-4-modify-columns)
5. [Summary](#step-5-summary)
6. [Results](#step-6-results)

### Step 1: Access Wizard and Intro Page
Access the wizard as described [here](#started).

The first page of the wizard is the welcome page. If you do not want to see this page again, feel free to click **Do not show this starting page again.**

![Wizard Intro](media/import-flat-file-wizard/import-flat-file-intro.png)

### Step 2: Specify Input File
Click browse to select your input file. At default, the wizard searches for .csv and .txt files. PROSE will detect if the file is comma-separated or fixed-width format regardless of file extension.

The new table name should be unique, and the wizard does not allow you to move further if not.

![Wizard Specify](media/import-flat-file-wizard/import-flat-file-specify.png)

### Step 3: Preview Data
The wizard generates a preview that you can view for the first 50 rows. If there are any problems, click cancel, otherwise proceed to the next page.

![Wizard Preview](media/import-flat-file-wizard/import-flat-file-preview.png)

### Step 4: Modify Columns
The wizard identifies what it believes are the correct column names, data types, etc. Here is where you can edit the fields if they are incorrect (for example, data type should be a float instead of an int).

Columns where empty values are detected will have "Allow Nulls" checked. However if you expect nulls in a column and "Allow Nulls" is not checked, here is where you can update the table definition to allow nulls in one or all columns.

Proceed when ready.

![Wizard Modify](media/import-flat-file-wizard/import-flat-file-modify.png)

### Step 5: Summary
This is simply a summary page displaying your current configuration. If there are issues, you can go back to previous sections. Otherwise, clicking finish attempts the import process.

![Wizard Summary](media/import-flat-file-wizard/import-flat-file-summary.png)

### Step 6: Results
This page indicates whether the import was successful. If a green check mark appears, it was a success, otherwise you may need to review your configuration or input file for any errors.

![Wizard Results](media/import-flat-file-wizard/import-flat-file-results.png)

## Troubleshooting
The Import Flat File Wizard detects the data types based on the first 200 rows.  In scenarios where data further in the flat file does not conform to the automatically detected data types, an error will occur during import. The error message would be similar to the following:
```
Error inserting data into table. (Microsoft.SqlServer.Prose.Import)
The given value of type String from the data source cannot be converted to type nvarchar of the specified target column. (System.Data)
String or binary data would be truncated. (System.Data)
```
Tactics to alleviate this error:
- Expanding the data type size(s) in the [Modify Columns step](#step-4-modify-columns), such as the length of a nvarchar column, may compensate for variations in the data from the remainder of the flat file.
- Enabling error reporting in the [Modify Columns step](#step-4-modify-columns), especially by a smaller number, will reveal which row(s) in the flat file contain data that does not fit the selected data types. For example, in a flat file where the second row introduces an error, running the import with error reporting with a range of 1 provides a specific error message.  Examining the file directly at the location can provide more targeted changes to the data types based on the data in the identified rows.

![Error Reporting Results](media/import-flat-file-wizard/import-flat-file-error.png)

```
Error inserting data into table occured while inserting rows 1 - 2. (Microsoft.SqlServer.Prose.Import)
The given value of type String from the data source cannot be converted to type float of the specified target column. (System.Data)
Failed to convert parameter value from a String to a Double. (System.Data)
```


## Learn More

Learn more about the wizard.
 
- **Learn more about importing other sources.** If you are looking to import more than flat files, see [SQL Server Import and Export Wizard](../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md).
- **Learn more about connecting to flat file sources.** If you are looking for more information about connecting to flat file sources, see [Connect to a Flat File Data Source](../../integration-services/import-export-data/connect-to-a-flat-file-data-source-sql-server-import-and-export-wizard.md).
- **Learn more about PROSE.** If you are looking for an overview of the intelligent framework used by this wizard, see [PROSE SDK](https://microsoft.github.io/prose/).
