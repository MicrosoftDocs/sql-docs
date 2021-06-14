---
title: Parameterization of Notebooks in Azure Data Studio using run with parameters action.
description: This tutorial shows how you can create a parameterized notebook in ADS using the run with parameters action.
ms.topic: how-to
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: vasubhog
ms.author: vasubhog
ms.reviewer: mikeray, alayu, maghan
ms.custom: ""
ms.date: 05/21/2021
---

# Create a Parameterized Notebook using Run with Parameters

**Parameterization** is the ability to execute the same notebook with different parameters.

This article shows you how to create and run a parameterized notebook in Azure Data Studio using the python kernel.

> [!Note]
   > Currently parameterization can be used with Python, PySpark, PowerShell, and .Net Interactive Kernels.

## Prerequisites

- [Azure Data Studio](../download-azure-data-studio.md)
- [Python](https://www.python.org/downloads/)

## Run with Parameters Action

The `Run with Parameters` notebook action enables users to quickly set new parameters for their notebook by allowing the user to input new parameters from the UI.

> [!Note]
   > The parameter cell has to be formatted with each new parameter on a new line.

## Set up a notebook for parameterization in Azure Data Studio

The steps in this section all run within an Azure Data Studio notebook.

1. Create a new notebook and change the **Kernel** to *Python 3*.

   ![New Notebook](media/notebooks-kqlmagic/install-new-notebook.png)

2. You may be prompted to upgrade your Python packages when your packages need updating.

   ![Yes](media/notebooks-kqlmagic/install-python-yes.png)

3. Verify the **Kernel** is set to *Python3*.

   ![Kernel change](media/notebooks-kqlmagic/change-kernel.png)

4. Create a New Code Cell and Tag as **Parameters Cell**.

   ```python
   x = 2.0
   y = 5.0
   ```

   :::image type="content" source="media/notebooks-parameterization/make-parameter-cell.png" alt-text="Parameter Cell Notebook":::

5. Add other cells to test different parameters.

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

6. Save notebook as **Input.ipynb**.
   :::image type="content" source="media/notebooks-parameterization/save-notebook.png" alt-text="Save Notebook":::

## How to Run the Notebook with Parameters

1. On the notebook toolbar, select the `Run with Parameters` action.

    :::image type="content" source="media/notebooks-parameterization/run-with-parameters.png" alt-text="Run with Parameters Action":::

2. Upon clicking the action, a new prompt will ask you to input new parameters for x and y.
   :::image type="content" source="media/notebooks-parameterization/first-parameter.png" alt-text="Input First Parameter":::

    :::image type="content" source="media/notebooks-parameterization/second-parameter.png" alt-text="Input New Parameters":::  

3. After, entering the new parameters view the new Output Parameterization Notebook and run all cells to see the new values.

   You can note that there's a new cell labeled **# Injected-Parameters** containing the new parameter values passed in.

   :::image type="content" source="media/notebooks-parameterization/output-notebook.png" alt-text="Output Notebook":::

## Next steps

Learn more about notebooks and parameterization:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Papermill Parameterization](./papermill-parameterization.md)
- [URI Parameterization](./uri-parameterization.md)
