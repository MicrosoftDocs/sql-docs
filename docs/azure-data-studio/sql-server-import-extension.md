---
title: SQL Server Import Extension
titleSuffix: Azure Data Studio
description: Install and use the SQL Server Import extension (preview) for Azure Data Studio
ms.custom: "seodec18"
ms.date: "09/24/2018"
ms.reviewer: "alayu; sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "yualan"
ms.author: "alayu"
manager: craigg
---
# SQL Server Import extension (preview)

The SQL Server Import extension (preview) converts .txt and .csv files into a SQL table. This wizard utilizes a Microsoft Research framework known as [Program Synthesis using Examples (PROSE)](https://microsoft.github.io/prose/) to intelligently parse the file with minimal user input. It is a powerful framework for data wrangling, and it is the same technology that powers Flash Fill in Microsoft Excel

To learn more about the SSMS version of this feature, you can read [this article](https://docs.microsoft.com/sql/relational-databases/import-export/import-flat-file-wizard).


## Install the SQL Server Import extension

1. To open the extensions manager and access the available extensions, select the extensions icon, or select **Extensions** in the **View** menu.
2. Select an available extension to view its details.

   ![import extension manager](media/sql-server-import-extension/import-wizard-install.png)

1. Select the extension you want and **Install** it.
2. Select **Reload** to enable the extension (only required the first time you install an extension).

## Start Import Wizard

1. To start SQL Server Import, first make a connection to a server in the Servers tab.
2. After you make a connection, drill down to the target database that you want to import a file into a SQL table.
3. Right-click on the database and click **Import Wizard**.
    ![open import wizard](media/sql-server-import-extension/open-import-wizard.png)

## Importing a file
1. When you right-click to launch the wizard, the server and database are already auto-filled. If there are other active connections, you can select in the dropdown. 
    
    Select a file by clicking **Browse.** It should auto-fill the table name based on the file name, but you can also change it yourself.

    By default, the schema will be dbo but you can change it. Click **Next** to proceed.
    ![input file](media/sql-server-import-extension/import-wizard-input-file.png)
1. The wizard will generate a preview based on the first 50 rows. There is no additional action on this page other than verifying the data looks accurate. Click **Next** to proceed.
    ![open import wizard](media/sql-server-import-extension/import-wizard-preview-data.png)
2. On this page, you can make changes to column name, data type, whether it is a primary key, or to allow nulls. You can make as many changes as you like. Click **Import Data** to proceed.
    ![open import wizard](media/sql-server-import-extension/import-wizard-modify-columns.png)
3. This page gives a summary of the actions chosen. You can also see whether your table inserted successfully or not. 

    You can either click **Done, Previous** if you need to make changes, or **Import new file** to quickly import another file.
    ![open import wizard](media/sql-server-import-extension/import-wizard-summary.png)
1. Verify if your table successfully imported by refreshing your target database or running a SELECT query on the table name.

## Next steps
- To learn more about the Import Wizard, read the [blog post](https://cloudblogs.microsoft.com/sqlserver/2018/08/30/the-august-release-of-sql-operations-studio-is-now-available/).
- To learn more about PROSE, read the [documentation.](https://microsoft.github.io/prose/)
