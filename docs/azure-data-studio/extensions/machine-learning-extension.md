---
title: Machine Learning extension
description: The Machine Learning extension for Azure Data Studio enables you to manage packages, import machine learning models, make predictions, and create notebooks to run experiments for your SQL databases.
author: rothja
ms.author: jroth
ms.date: 05/19/2020
ms.service: azure-data-studio
ms.topic: conceptual
---

# Machine Learning extension for Azure Data Studio (Preview)

The Machine Learning extension for [Azure Data Studio](../what-is-azure-data-studio.md) enables you to manage packages, import machine learning models, make predictions, and create notebooks to run experiments for your SQL databases. This extension is currently in preview.

## Prerequisites

The following prerequisites need to be installed on the computer you run Azure Data Studio.

- [Python 3](https://www.python.org/downloads/). Once you have installed Python, you need to specify the local path to a Python installation under [Extension Settings](#settings). If you have used a [Python kernel notebook](../notebooks/notebooks-python-kernel.md) in Azure Data Studio, the extension will use the path from the notebook by default.

- [Microsoft ODBC driver 17 for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md) for Windows, macOS, or Linux.

- [R 3.5](https://www.r-project.org/) (optional). Other version than 3.5 is currently not supported. Once you have installed R 3.5, you need to enable R and specify the local path to an R installation under [Extension Settings](#settings). This is only required if you want to manage R packages in your database.

### Trouble installing Python 3 from within ADS?

If you attempt to install Python 3 but get an error about TLS/SSL, add these two, optional components:

_sample error:_
```
$: ~/0.0.1/bin/python3 -m pip install --user "jupyter>=1.0.0" --extra-index-url https://prose-python-packages.azurewebsites.net
WARNING: pip is configured with locations that require TLS/SSL, however the ssl module in Python is not available.
Looking in indexes: https://pypi.org/simple, https://prose-python-packages.azurewebsites.net
Requirement already satisfied: jupyter
```

_install these:_

- [Homebrew](https://brew.sh) (optional). Install homebrew, then run `brew update` from the command line.

- *openssl* (optional). Next run `brew install openssl`.

## Install the extension

To install the Machine Learning extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select Extensions in the View menu.

1. Select the **Machine Learning** extension and view its details.

1. Select **Install**.

1. Select **Reload** to enable the extension. This is only required the first time you install an extension).

<a name="settings"></a>

## Extension settings

To change the settings for the Machine Learning extension, follow the steps below.

1. Open the extension manager in Azure Data Studio. You can either select the extensions icon or select Extensions in the View menu.

1. Find the **Machine Learning** extension under **enabled** extensions.

1. Select the **Manage** icon.

1. Select the **Extension Settings** icon.

The extensions settings look like this:

:::image type="content" source="media/machine-learning-extension/ml-extension-settings.png" alt-text="Machine Learning extension settings":::

### Enable Python

To use the Machine Learning extension as well as the Python package management in your database, follow the steps below.

> [!IMPORTANT]
> The Machine Learning extension requires Python to be enabled and configured to most functionality to work, even if you do not wish to use the Python package management in database functionality.

1. Ensure that **Machine Learning: Enable Python** is enabled. This setting is enabled by default.

1. Provide the path to your pre-existing Python installation under **Machine Learning: Python Path**. This can either be the full path to the Python executable or the folder the executable is in. If you have used a [Python kernel notebook](../notebooks/notebooks-python-kernel.md) in Azure Data Studio, the extension will use the path from the notebook by default.

### Enable R

To use the Machine Learning extension for R package management in your database, follow the steps below.

1. Ensure that **Machine Learning: Enable R** is enabled. This setting is disabled by default.

1. Provide the path to your pre-existing R installation under **Machine Learning: R Path**. This has to be the full path to the R executable. 

## Use the Machine Learning extension

To use the Machine Learning extension in Azure Data Studio, follow the steps below.

1. Open the **Connections** viewlet in Azure Data Studio.

1. Right-click on your server and select **Manage**.

1. Select **Machine Learning** in the left side menu under **General**.

Follow the links under **Next steps** to see how you can use the Machine Learning extension for manage packages, make predictions, and import models in your database.

## Next steps

- [Manage packages in database](machine-learning-extension-manage-packages.md)
- [Make predictions](machine-learning-extension-predictions.md)
- [Import or view models](machine-learning-extension-import-view-models.md)
- [Notebooks in Azure Data Studio](../notebooks/notebooks-guidance.md)
- [SQL machine learning documentation](../../machine-learning/index.yml)
- [Machine learning and AI with ONNX in SQL Edge (preview)](/azure/azure-sql-edge/onnx-overview)