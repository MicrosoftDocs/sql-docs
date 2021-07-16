---
title: Parameterize notebooks in Azure Data Studio with the Run with Parameters action
description: Learn how to create a parameterized notebook in Azure Data Studio by using the Run with Parameters action.
ms.topic: how-to
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: vasubhog
ms.author: vabhog
ms.reviewer: mikeray, alayu, maghan
ms.custom: ""
ms.date: 06/14/2021
---

# Create a parameterized notebook by using the Run with Parameters action

*Parameterization* is the ability to execute the same notebook by using different parameters.

This article shows you how to create and run a parameterized notebook in Azure Data Studio by using the Python kernel.

> [!NOTE]
> Currently, you can use parameterization with Python, PySpark, PowerShell, and .NET Interactive kernels.

## Prerequisites

- [Azure Data Studio](../download-azure-data-studio.md)
- [Python](https://www.python.org/downloads/)

## Run with Parameters action

When you use the Run with Parameters notebook action, the user can input new parameters in the UI to quickly set new parameters for your notebook. The user can then run the notebook with the new parameters.

> [!NOTE]
> It's important to format the parameter cell with each new parameter on a new line.

## Parameterization example

For an example of the notebook you'll create in the next section, save an [example notebook file](https://github.com/microsoft/sql-server-samples/blob/master/samples/applications/azure-data-studio/parameterization.ipynb), and then open the file in Azure Data Studio:

1. In [GitHub](https://github.com/microsoft/sql-server-samples/blob/master/samples/applications/azure-data-studio/parameterization.ipynb), select **Raw**.
1. Select Ctrl+S or right-click and save the file with the .ipynb extension.  
1. Open the file in Azure Data Studio.

## Set up a notebook for parameterization

Complete the following steps to create a notebook and try using different parameters. All the steps in this section run inside an Azure Data Studio notebook.

1. Create a new notebook. Change **Kernel** to **Python 3**:

    :::image type="content" source="media/notebooks-parameterization/install-new-notebook.png" alt-text="Screenshot that shows the New notebook menu option and setting the Kernel value to Python 3.":::

1. If you're prompted to upgrade your Python packages when your packages need updating, select **Yes**:

     :::image type="content" source="media/notebooks-parameterization/update-python-yes.png" alt-text="Screenshot that shows the dialog prompt to update Python packages.":::

1. Verify that **Kernel** is set to **Python 3**:

    :::image type="content" source="media/notebooks-parameterization/change-kernel.png" alt-text="Screenshot that shows the Kernel value to Python 3.":::

1. Make a new code cell. Select **Parameters** to tag the cell as a parameters cell.

    ```python
    x = 2.0
    y = 5.0
    ```

   :::image type="content" source="media/notebooks-parameterization/make-parameter-cell.png" alt-text="Screenshot that shows creating a new parameters cell with Parameters selected.":::

1. Add other cells to test different parameters:

    ```python
    addition = x + y
    multiply = x * y
    ```

    ```python
    print("Addition: " + str(addition))
    print("Multiplication: " + str(multiply))
    ```

    The output will look similar to this example:

    :::image type="content" source="media/notebooks-parameterization/test-cells.png" alt-text="Screenshot that shows the output of cells added to test new parameters.":::

1. Save the notebook as *Input.ipynb*:

    :::image type="content" source="media/notebooks-parameterization/save-notebook.png" alt-text="Screenshot that shows saving the notebook file.":::

## Run the notebook with parameters

1. On the notebook toolbar, select the Run with Parameters icon:

    :::image type="content" source="media/notebooks-parameterization/run-with-parameters.png" alt-text="Screenshot that shows the Run with Parameters icon selected on the toolbar.":::

1. A series of new dialogs prompt you to input new parameters for *x* and *y*:

    :::image type="content" source="media/notebooks-parameterization/first-parameter.png" alt-text="Screenshot that shows entering a new parameter for x.":::

    :::image type="content" source="media/notebooks-parameterization/second-parameter.png" alt-text="Screenshot that shows entering a new parameter for y.":::  

1. After you enter the new parameters, view the new parameterized notebook. Run all cells to see the new output. A new cell labeled `# Injected-Parameters` contains the new parameter values that were passed in:

    :::image type="content" source="media/notebooks-parameterization/output-notebook.png" alt-text="Screenshot that shows the output for new parameters.":::

## Next steps

Learn more about notebooks and parameterization:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Papermill parameterization](./parameterize-papermill.md)
- [URI parameterization](./parameterize-uri.md)
