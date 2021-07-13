---
title: Parameterize notebooks in Azure Data Studio with Run with parameters action.
description: Learn how to create a parameterized notebook in Azure Data Studio by using the Run with parameters action.
ms.topic: how-to
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: vasubhog
ms.author: vabhog
ms.reviewer: mikeray, alayu, maghan
ms.custom: ""
ms.date: 06/14/2021
---

# Create a parameterized notebook by using Run with parameters action

*Parameterization* is the ability to execute the same notebook by using different parameters.

This article shows you how to create and run a parameterized notebook in Azure Data Studio with the Python kernel.

> [!NOTE]
> Currently, you can use parameterization with Python, PySpark, PowerShell, and .NET Interactive kernels.

## Prerequisites

- [Azure Data Studio](../download-azure-data-studio.md)
- [Python](https://www.python.org/downloads/)

## Run with parameters action

When you use the `run with parameters` notebook action, the user can input new parameters from the UI to quickly set new parameters for your notebook.

> [!NOTE]
> Format the parameter cell with each new parameter on a new line.

## Set up a notebook for parameterization in Azure Data Studio

To open the following notebook example in Azure Data Studio, [download the notebook from GitHub](https://github.com/VasuBhog/sql-server-samples/blob/master/samples/applications/azure-data-studio/parameterization.ipynb), and then open it in Azure Data Studio.

The steps in this section all run within an Azure Data Studio notebook.

1. Create a new notebook. Change **Kernel** to **Python 3**:

   ![New Notebook](media/notebooks-kqlmagic/install-new-notebook.png)

1. If you're prompted to upgrade your Python packages when your packages need updating, select **Yes**:

   ![Yes](media/notebooks-kqlmagic/install-python-yes.png)

1. Verify that **Kernel** is set to **Python3**:

   ![Kernel change](media/notebooks-kqlmagic/change-kernel.png)

1. Make a new code cell. Sselect **Parameters** to tag the cell as a parameters cell.

   ```python
   x = 2.0
   y = 5.0
   ```

   :::image type="content" source="media/notebooks-parameterization/make-parameter-cell.png" alt-text="Parameter Cell Notebook":::

1. Add other cells to test different parameters:

   ```python
   addition = x + y
   multiply = x * y
   ```

   ```python
   print("Addition: " + str(addition))
   print("Multiplication: " + str(multiply))
   ```

   In the example input notebook, the output will look similar to this example:

   :::image type="content" source="media/notebooks-parameterization/test-cells.png" alt-text="Additional Input Notebook Cells":::

1. Save the notebook as *Input.ipynb*:

   :::image type="content" source="media/notebooks-parameterization/save-notebook.png" alt-text="Save Notebook":::

## Run the notebook with parameters

1. On the notebook toolbar, select the `run with parameters` icon:

    :::image type="content" source="media/notebooks-parameterization/run-with-parameters.png" alt-text="Run with Parameters Action":::

1. A new dialog prompts you to input new parameters for *x* and *y*:

   :::image type="content" source="media/notebooks-parameterization/first-parameter.png" alt-text="Input First Parameter":::

   :::image type="content" source="media/notebooks-parameterization/second-parameter.png" alt-text="Input New Parameters":::  

1. After you enter the new parameters, view the new parameterized notebook. Run all cells to see the new output. A new cell labeled `# Injected-Parameters` contains the new parameter values that were passed in:

   :::image type="content" source="media/notebooks-parameterization/output-notebook.png" alt-text="Output Notebook":::

## Next steps

Learn more about notebooks and parameterization:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Papermill parameterization](./parameterize-papermill.md)
- [URI parameterization](./parameterize-uri.md)
