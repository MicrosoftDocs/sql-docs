---
title: Parameterization of Notebooks in Azure Data Studio using URI parameterization
description: This tutorial shows how you can create a parameterized notebook in ADS using URI parameterization.
ms.topic: how-to
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: vasubhog
ms.author: vabhog
ms.reviewer: mikeray, alayu, maghan
ms.custom: ""
ms.date: 01/25/2021
---

# Create a Parameterized Notebook using the Notebook URI

**Parameterization** is the ability to execute the same notebook with different parameters.

This article shows you how to create and run a parameterized notebook in Azure Data Studio using the python kernel.

> [!Note]
   > Currently parameterization can be used with Python, PySpark, PowerShell, and .Net Interactive Kernels.

## Prerequisites

- [Azure Data Studio](../download-azure-data-studio.md)
- [Python](https://www.python.org/downloads/)

## URI Parameterization

URI parameterization programmatically adds parameters to the query of the ADS URI to open the notebook in ADS with new parameters.

Azure Data Studio Notebook URI supports HTTPS/HTTP/FILE URI schema and follows the below format:
<pre>azuredatastudio://microsoft.notebook/open?url=</pre>

The format to pass in parameters with the ADS Notebook URI is as follows:
<pre>azuredatastudio://microsoft.notebook/open?url=LinkToNotebook<b>?x=1&y=2</b></pre>

In the URI query use **&** to indicate a new parameter to be injected.

## URI Parameterization Example

1. In this example, we'll parameterize a notebook that is stored on a GitHub repository [here](https://github.com/VasuBhog/PyCon/blob/main/Input.ipynb).

2. Below is the contents and structure of the notebook, must use a notebook that has a cell tagged with parameters.  

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

3. We can either use the search bar of any browser or a markdown cell to open up the notebook with new parameters.

    The format for the notebook URI with new values for x and y:
    `azuredatastudio://microsoft.notebook/open?url=https://raw.githubusercontent.com/VasuBhog/PyCon/main/Input.ipynb?x=10&y=20`

    :::image type="content" source="media/notebooks-parameterization/search-bar.png" alt-text="URI link in Search bar":::

    When opening the link from the web browser, you will be prompted to open the notebook in Azure Data Studio. Select open.

    :::image type="content" source="media/notebooks-parameterization/donwload-prompt.png" alt-text="Download Prompt":::

    You will then be prompted to download and open the notebook with new parameters.

4. Once you select `Yes` to open the notebook, view the new Output Parameterization Notebook and run all cells to see the new values.

   You can note that there's a new cell labeled **# Injected-Parameters** containing the new parameter values passed in.

   :::image type="content" source="media/notebooks-parameterization/output-notebook.png" alt-text="Output Notebook":::

## Next steps

Learn more about notebooks and Parameterization:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Papermill Parameterization](./papermill-parameterization.md)
- [Run with Parameters](./run-with-parameters.md)
