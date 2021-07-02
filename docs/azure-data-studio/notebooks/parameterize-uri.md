---
title: Parameterization of notebooks in Azure Data Studio with URI parameterization
description: This tutorial shows how you can create a parameterized notebook in ADS with URI parameterization.
ms.topic: how-to
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: vasubhog
ms.author: vabhog
ms.reviewer: mikeray, alayu, maghan
ms.custom: ""
ms.date: 06/14/2021
---

# Create a Parameterized Notebook with the Notebook URI

**Parameterization** is the ability to execute the same notebook with different parameters.

This article shows you how to create and run a parameterized notebook in Azure Data Studio with the python kernel.

> [!Note]
> Currently parameterization can be used with Python, PySpark, PowerShell, and .Net Interactive Kernels.

## Prerequisites

- [Azure Data Studio](../download-azure-data-studio.md)
- [Python](https://www.python.org/downloads/)

## URI Parameterization

URI parameterization programmatically adds parameters to the query of the ADS URI to open the notebook in ADS with new parameters.

Azure Data Studio Notebook URI supports HTTPS/HTTP/FILE URI schema and follows the format:  

_azuredatastudio:\/\/microsoft.notebook\/open?url=_

The format to pass in parameters with the ADS Notebook URI is as follows:  

_azuredatastudio:\/\/microsoft.notebook\/open?url=LinkToNotebook_***?x=1&y=2***

In the URI query, use **&** to indicate a new parameter to be injected.

## URI Parameterization Example

**To open the below notebook example in Azure Data Studio, visit [GitHub](https://github.com/VasuBhog/sql-server-samples/blob/master/samples/applications/azure-data-studio/parameterization.ipynb) to then download and open in Azure Data Studio.**

Below is the contents and structure of the notebook, you must use a notebook that has a cell tagged with parameters.

   Tag a code cell in Azure Data Studio as **Parameters Cell**.

   :::image type="content" source="media/notebooks-parameterization/make-parameter-cell.png" alt-text="Parameter Cell Notebook":::

   Below is the contents of the notebook:

   ```python
   x = 2.0
   y = 5.0
   ```

   ```python
   addition = x + y
   multiply = x * y
   ```

   ```python
   print("Addition: " + str(addition))
   print("Multiplication: " + str(multiply))
   ```

1. We can either use the search bar of any browser or a markdown cell to open up the notebook URI link.

    Below is the notebook URI for parameterizing the notebook with new x and y values:

    _azuredatastudio://microsoft.notebook/open?url=https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/applications/azure-data-studio/parameterization.ipynb_**?x=10&y=20**

    :::image type="content" source="media/notebooks-parameterization/search-bar.png" alt-text="URI link in Search bar":::
    
    When opening the link from the web browser, you're prompted to open the notebook in Azure Data Studio. Select **Open Azure Data Studio**.
    
    :::image type="content" source="media/notebooks-parameterization/donwload-prompt.png" alt-text="Download Prompt":::

2. Then you're prompted to download and open the notebook with new parameters.

    Once you select **Yes**, view the new parameterized notebook and **run all cells** to see the new output.
   
    You can note that there's a new cell labeled **# Injected-Parameters** containing the new parameter values passed in.
    
    :::image type="content" source="media/notebooks-parameterization/output-notebook.png" alt-text="Output Notebook":::

## Next steps

Learn more about notebooks and Parameterization:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Papermill Parameterization](./parameterize-papermill.md)
- [Run with Parameters](./run-with-parameters.md)
