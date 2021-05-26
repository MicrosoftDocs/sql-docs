---
title: Parameterization of Notebooks in Azure Data Studio using URI parameterization
description: This tutorial shows how you can create a parameterized notebook in ADS using URI parameterization.
ms.topic: how-to
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: vasubhog
ms.author: vasubhog
ms.reviewer: mikeray, alayu, maghan
ms.custom: ""
ms.date: 01/25/2021
---

# Create a Parameterized Notebook

**Parameterization** is the ability to execute the same notebook with different parameters.

This article shows you how to create and run a parameterized notebook in Azure Data Studio using the python kernel.

Note: Currently Parameterization can used for the following kernels - Python, PySpark, PowerShell, and .Net Interactive Kernels

## Prerequisites

- [Azure Data Studio](../download-azure-data-studio.md)
- [Python](https://www.python.org/downloads/)

## URI Parameterization

URI parameterization utilizes the Azure Data Studio URI to programmatically add parameters to the query of the URI to then open the notebook in ADS with new parameters.

Azure Data Studio Notebook URI Format: 
azuredatastudio://microsoft.notebook/open?url=

- Supports HTTPS/HTTP/FILE URI Schema

Format to pass in parameters follows user to 
azuredatastudio://microsoft.notebook/open?url=https://Hello.ipynb?x=1&y=2


## Store a Parameterized Notebook on Github

The steps in this section all run within an Azure Data Studio notebook.

1. Create a new notebook and change the **Kernel** to *Python 3*.

   ![New Notebook](media/notebooks-kqlmagic/install-new-notebook.png)

2. You may be prompted to upgrade your Python packages when your packages need updating.

   ![Yes](media/notebooks-kqlmagic/install-python-yes.png)

## Set up a parameterized notebook

1. Verify the **Kernel** is set to *Python3*.

   ![Kernel change](media/notebooks-kqlmagic/change-kernel.png)

2. Create a New Code Cell and Tag as **Parameters Cell**.

   ```python
   x = 2.0
   y = 5.0
   ```

   :::image type="content" source="media/notebooks-parameterization/make-parameter-cell.png" alt-text="Parameter Cell Notebook":::

3. Add other cells to test different parameters.

   ```python
   addition = x + y
   multiply = x * y
   ```

   ```python
   print("Addition: " + str(addition))
   print("Multiplication: " + str(multiply))
   ```

   Cells in Example Input Notebook:
   :::image type="content" source="media/notebooks-parameterization/test-cells.png" alt-text="Additional Input Notebook Cells":::

4. Save notebook as **Input.ipynb**.
   :::image type="content" source="media/notebooks-parameterization/save-notebook.png" alt-text="Save Notebook":::



## Next steps

Learn more about notebooks and Parameterization:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Papermill Parameterization Docs](https://papermill.readthedocs.io/en/latest/index.html)