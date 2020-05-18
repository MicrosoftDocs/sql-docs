---
title: Machine Learning extension (preview)
titleSuffix: Azure Data Studio
description: Install and use the Machine Learning extension for Azure Data Studio
ms.date: "05/12/2020"
ms.reviewer: "sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Machine Learning extension (preview) for Azure Data Studio

The Machine Learning extension for [Azure Data Studio](what-is.md) gives you the ability to manage packages in your database, import models into your database and use these to make predictions, and create notebooks for experiments. This extension is currently in preview.

## Prerequisites

The following prerequisites need to be installed on the computer you run Azure Data Studio on.

- [Python 3](https://www.python.org/downloads/). Once you have installed Python, you need to specify the local path to a the Python installation under **Settings**. If you have used a [Python kernel notebook](notebooks-tutorial-python-kernel.md) in Azure Data Studio, the extension will use the path from the notebook by default.
- [Microsoft ODBC driver 17 for SQL Server](../connect/odbc/download-odbc-driver-for-sql-server.md) for Windows, macOS, or Linux.
- [R 3.5](https://www.r-project.org/) (optional). Once you have installed R, you need to enable R and specify the local path to a the R installation under Settings. This is only required if you want to manage R packages in your database.

## Install the Machine Learning extension

To install the Machine Learning extension in Azure Data Studio, follow the steps below.

1. Open Azure Data Studio.
1. Open the extensions manager. You can either select the extensions icon or select Extensions in the View menu.
1. Select the **Machine Learning** extension and view its details.
1. Click **Install**.
1. Click **Reload** to enable the extension. This is only required the first time you install an extension).
