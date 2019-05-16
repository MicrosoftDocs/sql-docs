---
title: "Steps in the SQL Server Import and Export Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "02/16/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 816fb1bd-7bb9-450d-ad65-e4c2d02eaff8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Steps in the SQL Server Import and Export Wizard

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


This topic describes the sequence of steps for importing and exporting data with the SQL Server Import and Export Wizard. It also contains links to the individual pages of documentation that describe each page or dialog box you see in the wizard.

This topic describes only the **steps** in the wizard. If you're looking for something else, see [Related tasks and content](#related).

## Steps for importing and exporting data  
 The following table lists the steps for importing and exporting data and the corresponding pages of the wizard. Depending on the options that you select in the wizard, you typically don't see all these pages.  

For a quick look at the several screens you see in a typical session, take a look at this simple end-to-end example on a single page - [Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md).

|Step|Wizard pages|  
|----------|------------------|  
|**Welcome**<br />You don't have to take any action on this page.|[Welcome to SQL Server Import and Export Wizard](../../integration-services/import-export-data/welcome-to-sql-server-import-and-export-wizard.md)|  
|**Pick the source** of the data.|[Choose a Data Source](../../integration-services/import-export-data/choose-a-data-source-sql-server-import-and-export-wizard.md)|  
|**Pick the destination** for the data.|[Choose a Destination](../../integration-services/import-export-data/choose-a-destination-sql-server-import-and-export-wizard.md)|  
|**Configure the destination**. (optional steps)<br /><br /> -   Create a new destination database.<br />-   If you're copying data to a text file, configure additional settings.|[Create Database](../../integration-services/import-export-data/create-database-sql-server-import-and-export-wizard.md)<br /><br />[Configure Flat File Destination](../../integration-services/import-export-data/configure-flat-file-destination-sql-server-import-and-export-wizard.md)|  
|**Specify what you want to copy.**|[Specify Table Copy or Query](../../integration-services/import-export-data/specify-table-copy-or-query-sql-server-import-and-export-wizard.md)<br /><br />[Select Source Tables and Views](../../integration-services/import-export-data/select-source-tables-and-views-sql-server-import-and-export-wizard.md)<br /><br />[Provide a Source Query](../../integration-services/import-export-data/provide-a-source-query-sql-server-import-and-export-wizard.md)|  
|**Configure the copy operation**. (optional steps)<br /><br /> -   Create a new destination table.<br />-   Decide what to do if the wizard doesn't know how to map data types between the source and destination that you selected.<br />-   Review column mappings between source and destination.<br />-   Handle issues with converting data types between source and destination.<br />-   Preview the data to be copied.|[Create Table SQL Statement](../../integration-services/import-export-data/create-table-sql-statement-sql-server-import-and-export-wizard.md)<br /><br />[Convert Types without Conversion Checking](../../integration-services/import-export-data/convert-types-without-conversion-checking-sql-server-import-and-export-wizard.md)<br /><br />[Column Mappings](../../integration-services/import-export-data/column-mappings-sql-server-import-and-export-wizard.md)<br /><br />[Review Data Type Mapping](../../integration-services/import-export-data/review-data-type-mapping-sql-server-import-and-export-wizard.md)<br /><br />[Column Conversion Details Dialog Box](../../integration-services/import-export-data/column-conversion-details-dialog-box-sql-server-import-and-export-wizard.md)<br /><br />[Preview Data Dialog Box](../../integration-services/import-export-data/preview-data-dialog-box-sql-server-import-and-export-wizard.md)|  
|**Copy the data.**<br /><br /> Optionally, save your settings as a SQL Server Integration Services (SSIS) package.|[Save and Run Package](../../integration-services/import-export-data/save-and-run-package-sql-server-import-and-export-wizard.md)<br /><br />[Save SSIS Package](../../integration-services/import-export-data/save-ssis-package-sql-server-import-and-export-wizard.md)<br /><br />[Complete the Wizard](../../integration-services/import-export-data/complete-the-wizard-sql-server-import-and-export-wizard.md)<br /><br />[Performing Operation](../../integration-services/import-export-data/performing-operation-sql-server-import-and-export-wizard.md)|  

> [!TIP]
> Tap the F1 key from any page or dialog box of the wizard to see documentation for the current page.

## <a name="related"></a> Related tasks and content  
Here are some other basic tasks.
-   **See a quick example of how the wizard works.**

    -   **If you prefer to see screen shots.** Take a look at this simple end-to-end example on a single page - [Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md).

    -   **If you prefer to watch a video.** Watch this four-minute video from YouTube that demonstrates the wizard and explains clearly and simply how to export data to Excel - [Using the SQL Server Import and Export Wizard to Export to Excel](https://go.microsoft.com/fwlink/?linkid=829049).

-   **Learn more about how the wizard works.**

    -   **Learn more about the wizard.** If you're looking for an overview of the wizard, see [Import and Export Data with the SQL Server Import and Export Wizard](../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md).

    -   **Learn how to connect to data sources and destinations.** If you're looking for info about how to connect to your data, select the page you want from the list here - [Connect to data sources with the SQL Server Import and Export Wizard](../../integration-services/import-export-data/connect-to-data-sources-with-the-sql-server-import-and-export-wizard.md). There's a separate page of documentation for each of several commonly used data sources. 

-   **Start the wizard.** If you're ready to run the wizard and just want to know how to start it, see [Start the SQL Server Import and Export Wizard](../../integration-services/import-export-data/start-the-sql-server-import-and-export-wizard.md).

-     **Get the wizard.** If you want to run the wizard, but you don't have [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on your computer, you can install the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard  by installing SQL Server Data Tools (SSDT). For more info, see [Download SQL Server Data Tools (SSDT)](https://msdn.microsoft.com/library/mt204009.aspx).


