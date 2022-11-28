---
title: "Install and remove Database Schema Conversion Toolkit (Oracle to Microsoft SQL)"
description: "Learn how to install and remove Database Schema Conversion Toolkit (Oracle to Microsoft SQL) extension."
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.date: "10/4/2021"
ms.service: azure-data-studio
ms.topic: conceptual
---

# Install and remove Database Schema Conversion Toolkit (Oracle to Microsoft SQL)

To use Database Schema Conversion Toolkit (Oracle to Microsoft SQL) features, you will need to [install the Azure Data Studio](../../../download-azure-data-studio.md) first.

Once Azure Data Studio is installed, select **Extensions** under the **View** menu and search for "Database Schema Conversion Toolkit" on the Extensions Marketplace. Select **Database Schema Conversion Toolkit (Oracle to Microsoft SQL)** and select the **Install** button in the search results list.

> [!NOTE]
> The **Database Schema Conversion Toolkit (Oracle to Microsoft SQL)** extension depends on **Extension for Oracle** and **SQL Database Projects** extensions. These extensions will be automatically installed.

## Upgrading Database Schema Conversion Toolkit (Oracle to Microsoft SQL)

Once available, latest version of the Database Schema Conversion Toolkit will be automatically installed, if extensions [Auto Update](../../add-extensions.md#updating-an-extension) option is enabled in Azure Data Studio.

## Removing Database Schema Conversion Toolkit (Oracle to Microsoft SQL)

When you have finished migrating databases, you might want to uninstall the Database Schema Conversion Toolkit extension.

To remove the Database Schema Conversion Toolkit extension, in Azure Data Studio, select **Extensions** under the **View** menu and search for "Database Schema Conversion Toolkit (Oracle to Microsoft SQL)" in the list of installed extensions. Select the extension and select **Uninstall** to remove the extension.

> [!NOTE]
> A restart of Azure Data Studio may be necessary to complete the removal process.

## Next steps

- [Convert Oracle database objects to Microsoft SQL](.\convert-oracle-database-objects-to-mssql.md)
- [Extend the functionality of Azure Data Studio - Add Azure Data Studio extensions](../../add-extensions.md#add-azure-data-studio-extensions)
- [Extend the functionality of Azure Data Studio - Uninstall an extension](../../add-extensions.md#uninstall-an-extension)
