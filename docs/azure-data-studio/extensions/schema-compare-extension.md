---
title: Schema Compare extension
description: Learn how to install and use the Azure Data Studio Schema Compare extension to easily compare two databases and selectively change one to match the other.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: "maghan"
ms.date: 5/24/2022
ms.service: azure-data-studio
ms.topic: conceptual
---

# Schema Compare extension

This article provides an overview of the Schema Compare extension for Azure Data Studio. The Schema Compare extension provides an easy-to-use way to compare two database definitions and apply the differences from the source to the target.  This includes active database connections, dacpac files, and [SQL projects](sql-database-project-extension.md).

It can be tedious to manually manage and synchronize different database versions. The Schema Compare extension simplifies the process of comparing databases and gives you full control when synchronizing them - you can selectively filter specific differences and categories of differences before applying any changes. The Schema Compare extension is a reliable tool that saves you time and code.


:::image type="content" source="media/schema-compare-extension/schema-compare-options.png" alt-text="Screenshot of the Azure Data Studio G U I, compare schema extension." lightbox="media/schema-compare-extension/schema-compare-options.png":::

## Features

The Schema Compare extension provides the following features: 

* Compare schemas between two dacpac files, databases, or SQL projects.
* View results as a set of actions to take against the target for it to match the source.
* Selectively exclude actions listed in results.
* Set options that control the scope of the comparison.
* Apply changes directly to the target or generate a script to apply changes at a later time. 
* Save the comparison. 

:::image type="content" source="media/schema-compare-extension/schema-compare.png" alt-text="Screenshot of the Azure Data Studio G U I, comparing schemas." lightbox="media/schema-compare-extension/schema-compare.png":::


## Install the extension

To install the Schema Compare extension, follow these steps: 

1. In Azure Data Studio, select the Extensions Icon to view available extensions.

    :::image type="content" source="media/schema-compare-extension/schema-compare-marketplace-search.png" alt-text="Screenshot of the Azure Data Studio GUI, marketplace search.":::

2. Search for the **Schema Compare** extension and select it to view its details. Select **Install** to add the extension.

3. Once installed, **Reload** to enable the extension in Azure Data Studio (only required when installing an extension for the first time).

## Compare schemas 

To compare schemas, open the Schema Compare dialog box. To do so, follow these steps: 

1. To open the Schema Compare dialog box, right-click a database in Object Explorer and select **Schema Compare**. The database you select is set as the **Source** database in the comparison.

    :::image type="content" source="media/schema-compare-extension/schema-compare-launch.png" alt-text="Screenshot of the Azure Data Studio G U I, schema compare launch.":::

2. Select one of the ellipses (...) to change the **Source** and **Target** of your Schema Compare and select OK.

    :::image type="content" source="media/schema-compare-extension/schema-compare-select-source-target.png" alt-text="Screenshot of the Azure Data Studio G U I, schema compare,  select source and target." lightbox="media/schema-compare-extension/schema-compare-select-source-target.png":::

3. To customize your comparison, select the **Options** button in the toolbar.

4. Select **Compare** to view the results of the comparison.

## Update an existing SQL project from a database

To update an existing SQL project from a database, follow these steps: 

1. Install both the **Schema Compare** and **[SQL Database Project](sql-database-project-extension.md)** extensions.

2. From a **Database dashboard** select the **Update Project from Database** option in the toolbar.

3. Select the existing SQL project and the desired file structure for new objects.

4. Choose **View changes in Schema Compare** to review the changes before applying them to the SQL project.

## Next steps

- [Learn more about Schema Compare in SSDT](../../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md)
- [Explore SQL database projects in Azure Data Studio](sql-database-project-extension.md)
