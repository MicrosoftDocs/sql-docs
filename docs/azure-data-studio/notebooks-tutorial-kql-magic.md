---
title: Notebooks with KQL magic in Azure Data Studio
description: This tutorial shows how you can create and run KQL magic in Azure Data Studio.
author: markingmyname
ms.author: maghan
ms.reviewer: jukoesma
ms.topic: tutorial
ms.prod: sql
ms.technology: azure-data-studio
ms.custom: ""
ms.date: 04/22/2020
---

# Create and run a notebook with KQL magic

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This tutorial demonstrates how to create and run a notebook in Azure Data Studio using KQL magic.

## Prerequisites

- [Azure Data Studio](download-azure-data-studio.md)
- [Python installed](https://www.python.org/downloads/)

## Set up environment

1. Create a new notebook and change the **Kernel** to *Python 3*.

2. Select **Yes** to upgrade Python packages, which should install Kqlmagic bits.

   ![Yes](media/notebooks-tutorial-kql-magic/python-yes-upgrade.png)

3. Verify that Kqlmagic is installed

   ```python
   !pip list
   ```

   > [1Note]
   > If Kqlmagic is not listed, then run the command below.

   > ```python
   >      !pip install Kqlmagic --no-cache-dir --upgrade
   > ```

4. Environment variables

   Setup environment variables, including such that Kqlmagic can render the charts using plotly.

   ```python
   %env KQLMAGIC_NOTEBOOK_APP=AzureDataStudio
   %env KQLMAGIC_LOAD_MODE=silent
   %env KQLMAGIC_CONFIGURATION="show_query_time=False;plot_package='plotly';display_limit=100"
   %env KQLMAGIC_CONFIGURATION="show_init_banner=True;check_magic_version=False;show_what_new=False"
   ```

5. Setup Azure Data Explorer connection

   ```python
   %env KQLMAGIC_CONNECTION_STR=AzureDataExplorer://username='anyone@domain.com';cluster='help';database='Samples'
   ```

6. Load Kqlmagic

   ```python
   %reload_ext Kqlmagic
   ```

   - If this step fails, close the file and reopen.
   - If Samples@help is asking for password, leave it blank and press [Enter].

   > [!Note]
   > Test to ensure that Kqlmagic works by browsing the help documentation.

   > ```python
   > %kql --help "help"
   > ```

## Next steps

Learn more about notebooks:

- [How to use notebooks with SQL Server](notebooks-guidance.md)

- [How to manage notebooks in Azure Data Studio](notebooks-manage-sql-server.md)

- [Run a sample notebook using Spark](../big-data-cluster/notebooks-tutorial-spark.md)