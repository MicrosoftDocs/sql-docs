---
title: Azure Synapse Pathway (Preview) assessment.
description: Perform a data warehouse code translation with Azure Synapse Pathway 
author: anshul82-ms
ms.author: anrampal
ms.prod: sql
ms.technology: "Azure Synapse Pathway"
ms.topic: tutorial 
ms.date: 02/16/2021
ms.custom: template-tutorial 
---

# Tutorial to perform your first code translation with Azure Synapse Pathway (Preview)

Azure Synapse Pathway v1.0 introduces support for translating schemas, tables, views, functions, etc. from **Netezza**, **Teradata**, **Snowflake**, and **Microsoft SQL Server** into T-SQL complaint code that automates migration to Azure Synapse Analytics.

For more information, see [Azure Synapse Pathway overview](azure-synapse-pathway-overview).

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Run your first translation of the SQL scripts in from your existing data warheouse to T-SQL scripts for Azure Synpase SQL 
> * Choose from one of the available sources
> * View the error and warnings about the objects that didn't get translated

## Prerequisites

To complete this tutorial, make sure you've installed [Azure Synapse Pathway](synapse-pathway-download.md). If you need an introduction, see [Azure Synapse Pathway overview](azure-synapse-pathway-overview.md).

## Run the translation

1. Launch the Azure Synapse Pathway MSI. 

1. Select from one of the available sources, the ones that will be added soon are grayed out.
1. In the Input directory folder, select browse and point the tool to the folder location of the **DDL** and **DML** scripts that need to be translated.
1. When translating the Netezza code to Azure Synapse Analytics, choose IBM Netezza in the Translation Type drop down.
  ![Azure Synapse assessment input.](./media/perform-assessment/assessment-input.png)

1. To select the output directory, select browse to specify the location where the output will be generated.
 ![Azure Synapse output directory.](./media/perform-assessment/output-directory.png)

1. Select **Translate** to start the translation

## View Results

1. The duration of the assessment depends on the number of databases added and the schema size of each database. Results are displayed for each database as soon as they're available.
 ![Azure Synapse assessment report.](./media/perform-assessment/assessment-report.png)

1. When you select view results, it will take you to the output directory specified in the previous step and you'll see the translated script file(s) based created based on your input directory structure.
1. It includes the project structure that can be easily committed to your Github repo.  
1. A results file will uploaded in the same output directory, which will have a list of errors and warnings.

## Next steps

[Learn how to save and load the assessment](tutorial-save-load-assessment.md)
