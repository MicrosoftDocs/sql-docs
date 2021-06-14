---
title: Parameterization of Notebooks in Azure Data Studio
description: This tutorial shows how you can create a parameterized notebook in ADS.
ms.topic: how-to
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: vasubhog
ms.author: vabhog
ms.reviewer: mikeray, alayu, maghan
ms.custom: ""
ms.date: 01/25/2021
---

# Create a Parameterized Notebook using Papermill

**Parameterization** is the ability to execute the same notebook with different parameters.

This article shows you how to create and run a parameterized notebook in Azure Data Studio using the python kernel.

> [!Note]
   > Currently parameterization can be used with Python, PySpark, PowerShell, and .Net Interactive Kernels.

## Prerequisites

- [Azure Data Studio](../download-azure-data-studio.md)
- [Python](https://www.python.org/downloads/)

## Install and set up Papermill in Azure Data Studio

The steps in this section all run within an Azure Data Studio notebook.

1. Create a new notebook and change the **Kernel** to *Python 3*.

   ![New Notebook](media/notebooks-kqlmagic/install-new-notebook.png)

2. You may be prompted to upgrade your Python packages when your packages need updating.

   ![Yes](media/notebooks-kqlmagic/install-python-yes.png)

3. Install Papermill:

   ```python
   import sys
   !{sys.executable} -m pip install papermill --no-cache-dir --upgrade
   ```

   Verify it's installed:

   ```python
   import sys
   !{sys.executable} -m pip list
   ```

   :::image type="content" source="media/notebooks-parameterization/install-list-papermill.png" alt-text="List":::

4. You can test if papermill is loaded properly by checking the version of papermill.

   ```python
   import papermill
   papermill
   ```

   :::image type="content" source="media/notebooks-parameterization/install-validation-papermill.png" alt-text="Validation":::

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

## How to execute Papermill notebook

Papermill can be executed two ways:

- Command Line Interface (CLI)
- Python API

### Parameterized CLI execution

To execute a notebook using the CLI, enter the papermill command in the terminal with the input notebook, location for output notebook, and options.

> [!Note]
   > Papermill Command Line Interface Documentation can be found [here](https://papermill.readthedocs.io/en/latest/usage-execute.html#execute-via-cli).

1. Execute Input Notebook with new parameters.

   ```shell
   papermill Input.ipynb Output.ipynb -p x 10 -p y 20
   ```

   This will execute the Input Notebook with new values for parameters **x** and **y**.

2. After Execution view the new Output Parameterized Notebook.
   You can note that there's a new cell labeled **# Injected-Parameters** containing the new parameter values passed in via CLI.

   :::image type="content" source="media/notebooks-parameterization/output-notebook.png" alt-text="Output Notebook":::

### Parameterized Python API execution

> [!Note]
   > Papermill Python API Documentation can be found [here](https://papermill.readthedocs.io/en/latest/usage-execute.html#execute-via-the-python-api).

1. Create a new notebook and change the **Kernel** to *Python 3*.
   ![New Notebook](media/notebooks-kqlmagic/install-new-notebook.png)

2. Add a new code cell and use papermill to use the execute method.

   ```python
   import papermill as pm

   pm.execute_notebook(
   '/Users/vasubhog/GitProjects/AzureDataStudio-Notebooks/Demo_Parameterization/Input.ipynb',
   '/Users/vasubhog/GitProjects/AzureDataStudio-Notebooks/Demo_Parameterization/Output.ipynb',
   parameters = dict(x = 10, y = 20)
   )
   ```

   ![Papermill Python API Execution](media/notebooks-parameterization/python-api-execute.png)

3. After Execution view new Output Parameterization Notebook.

   You can note that there's a new cell labeled **# Injected-Parameters** containing the new parameter values passed in via CLI.

   :::image type="content" source="media/notebooks-parameterization/output-notebook.png" alt-text="Output Notebook":::

## Next steps

Learn more about notebooks and Parameterization:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Papermill Parameterization Docs](https://papermill.readthedocs.io/en/latest/index.html)
- [URI Parameterization](./uri-parameterization.md)
- [Run with Parameters](./run-with-parameters.md)
