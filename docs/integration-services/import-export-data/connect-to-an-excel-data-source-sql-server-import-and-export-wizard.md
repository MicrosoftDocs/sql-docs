---
title: "Connect to an Excel Data Source (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "04/02/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 43fbaca0-36d8-4583-9056-af7010209b87
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Connect to an Excel Data Source (SQL Server Import and Export Wizard)
This article shows you how to connect to a **Microsoft Excel** data source from the **Choose a Data Source** or **Choose a Destination** page of the SQL Server Import and Export Wizard.

The following screen shot shows a sample connection to a Microsoft Excel workbook.

![Excel connection](../../integration-services/import-export-data/media/excel-connection.png) 

You may have to download and install additional files to connect to Excel files. For more info, see [Get the files you need to connect to Excel](../load-data-to-from-excel-with-ssis.md#files-you-need).

> [!IMPORTANT]
> For detailed info about connecting to Excel files, and about limitations and known issues for loading data from or to Excel files, see [Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md).

## Options to specify

> [!NOTE]
> The connection options for this data provider are the same whether Excel is your source or your destination. That is, the options you see are the same on both the **Choose a Data Source** and the **Choose a Destination** pages of the wizard.

**Excel file path**  
 Specify the path and file name for the Excel file. For example:
-   For a file on the local computer, **C:\\MyData.xlsx**.
-   For a file on a network share, **\\\\Sales\\Database\\Northwind.xlsx**.

Or, click **Browse**.  
  
 **Browse**  
 Locate the spreadsheet by using the **Open** dialog box.  

> [!NOTE]
> The wizard can't open a password-protected Excel file.

 **Excel version**  
Select the version of Excel that's used by the source or destination workbook.

**First row has column names**  
Indicate whether the first row of the data contains column names.
-   If the data doesn't contain column names but you enable this option, the wizard treats the first row of source data as the column names.
-   If the data contains column names but you disable this option, the wizard treats the row of column names as the first row of data.

If you specify that the data doesn't have column names, the wizard uses F1, F2, and so forth, as column headings.

## I don't see Excel in the list of data sources
If you don't see Excel in the list of data sources, are you running the 64-bit wizard? The providers for Excel and Access are typically 32-bit and aren't visible in the 64-bit wizard. Run the 32-bit wizard instead.

> [!NOTE]
> To use the 64-bit version of the SQL Server Import and Export Wizard, you have to install SQL Server. SQL Server Data Tools (SSDT) and SQL Server Management Studio (SSMS) are 32-bit applications and only install 32-bit files, including the 32-bit version of the wizard.

## See also
[Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)  
[Choose a Data Source](../../integration-services/import-export-data/choose-a-data-source-sql-server-import-and-export-wizard.md)  
[Choose a Destination](../../integration-services/import-export-data/choose-a-destination-sql-server-import-and-export-wizard.md)

