---
title: Manage packages with Machine Learning extension
description: Learn how to manage Python or R packages in your database with the [Machine Learning extension for Azure Data Studio.
author: rothja
ms.author: jroth
ms.date: 05/19/2020
ms.service: azure-data-studio
ms.topic: conceptual
---

# Manage packages in database with Machine Learning extension for Azure Data Studio (Preview)

Learn how to manage Python or R packages in your database with the [Machine Learning extension for Azure Data Studio](machine-learning-extension.md).

> [!IMPORTANT]
> Manage packages in database with the Machine Learning extension currently only supports [Machine Learning Services](../../machine-learning/sql-server-machine-learning-services.md) on SQL Server 2019.

## Prerequisites

- Install and configure [Machine Learning extension for Azure Data Studio](machine-learning-extension.md). You need to specify the [Python or R installation paths in the Extension Settings](machine-learning-extension.md#settings) for the package management to work.

- The **sqlmlutils** package. If the package is not already installed, the Machine Learning extension will prompt you to install this.

- For SQL Server on Linux, Windows authentication is not supported. Therefore, you need to use SQL authentication to connect to your SQL Server on Linux.

## Manage Python packages

You can install and uninstall Python packages with the Machine Learning extension.

### Install new Python package

Follow the steps below to install Python packages in your database.

1. Select **Manage packages in database**.

1. If you're asked to install **sqlmlutils**, select **Yes**.

1. Under the **Installed** tab, select **Python** under the **Package Type** and select your database under **Location**.

1. Select the **Add new** tab.

1. Enter the Python package you like to install in **Search Python packages** and select **Search**.

1. Verify the package is listed under **Package Name** and the desired version is listed under **Package Version**.

1. Select **Install**.

1. Select **Close** and verify that the package was installed successfully under **Tasks**.

### Uninstall a Python package

Follow the steps below to uninstall Python packages in your database.

> [!NOTE]
> You can only uninstall packages that have been installed in your database. It is not possible to uninstall a package that has been pre-installed with SQL Server Machine Learning Services.

1. Select **Manage packages in database**.

1. If you're asked to install **sqlmlutils**, select **Yes**.

1. Under the **Installed** tab, select **Python** under the **Package Type** and select your database under **Location**.

1. Select the package(s) you would like to uninstall.

1. Select **Uninstall selected packages**.

1. Select **Close** and verify that the package was installed successfully under **Tasks**.

## Manage R packages

You can install and uninstall R packages with the Machine Learning extension.

### Install new R package

Follow the steps below to install Python packages in your database.

1. Select **Manage packages in database**.

1. If asked to install **sqlmlutils**, select **Yes**.

1. Under the **Installed** tab, select **R** under the **Package Type** and select your database under **Location**.

1. Select the **Add new** tab.

1. Enter the R package you like to install in **Search R packages** and select **Search**.

1. Verify the package is listed under **Package Name** and the desired version is listed under **Package Version**.

1. Select **Install**.

1. Select **Close** and verify that the package was installed successfully under **Tasks**.

### Uninstall an R package

Follow the steps below to uninstall R packages in your database.

> [!NOTE]
> You can only uninstall packages that have been installed in your database. It is not possible to uninstall a package that has been pre-installed with SQL Server Machine Learning Services.

1. Select **Manage packages in database**.

1. If asked to install **sqlmlutils**, select **Yes**.

1. Under the **Installed** tab, select **R** under the **Package Type** and select your database under **Location**.

1. Select the package(s) you would like to uninstall.

1. Select **Uninstall selected packages**.

1. Select **Close** and verify that the package was installed successfully under **Tasks**.

## Next steps

- [Machine Learning extension in Azure Data Studio](machine-learning-extension.md)
- [Make predictions](machine-learning-extension-predictions.md)
- [Import or view models](machine-learning-extension-import-view-models.md)
- [Notebooks in Azure Data Studio](../notebooks/notebooks-guidance.md)
- [SQL machine learning documentation](../../machine-learning/index.yml)