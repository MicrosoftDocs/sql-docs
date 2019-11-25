---
title: "Column Mappings (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.columnmapandtransform.f1"
ms.assetid: eadc54a6-f936-4ffc-91d7-fbfd2bdcab93
author: janinezhang
ms.author: janinez
manager: craigg
---
# Column Mappings (SQL Server Import and Export Wizard)
  Use the **Column Mappings** dialog box to edit transformation parameters.  
  
> [!NOTE]  
>  You do not have to copy all the columns in a table when you select the Table Copy option. Select **\<ignore>** in the **Destination** column of this dialog box for columns that you want to skip.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, as well as the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the SQL Server Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
 **Source**  
 Identifies the selected source table, view, or query.  
  
 **Destination**  
 Identifies the selected destination table, view, or query.  
  
 **Create destination table/file**  
 Specify whether to create the destination table if it does not already exist.  
  
 **Delete rows in destination table/file**  
 Specify whether to clear the data from an existing table before loading new data.  
  
 **Append rows to destination table/file**  
 Specify whether to append the new data to the data already present in an existing table.  
  
 **Edit SQL**  
 Use the default statement in the **Create Table SQL Statement** dialog box, or modify it for your purposes. If you modify this statement, you must also make associated changes to table mapping.  
  
 **Drop and re-create destination table**  
 Choose this option to overwrite the destination table. This option is only available when you use the wizard to create the destination table. The destination table is only dropped and re-created if you save the package that the wizard creates, and then run the package again.  
  
 **Enable identity insert**  
 Choose this option to allow existing identity values in the source data to be inserted into an identity column in the destination table. By default, the destination identity column does not allow this.  
  
 **Mappings**  
 Displays how each column in the data source maps to a column in the destination.  
  
 This list has the following columns:  
  
 **Source**  
 View each source column for which you can set transformation parameters.  
  
 **Destination**  
 Specify whether you want to ignore a column during the copy operation. You can copy only a subset of columns by selecting **\<ignore>** in this column for columns that you want to skip. Before you map columns, you must ignore all columns that will not be mapped.  
  
 **Type**  
 Select a data type for the column.  
  
 **Nullable**  
 Specify whether a column will allow a null value.  
  
 **Size**  
 Specify the number of characters in the column.  
  
 **Precision**  
 Specify the precision of displayed data, referring to the number of digits.  
  
 **Scale**  
 Specify the scale of displayed data, referring to the number of decimal places.  
  
  
