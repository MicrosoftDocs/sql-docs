---
title: Parameterize notebooks in Azure Data Studio with URI parameterization
description: Learn how to create a parameterized notebook in Azure Data Studio by using URI parameterization.
author: vasubhog
ms.author: vabhog
ms.reviewer: mikeray, alayu, maghan
ms.date: 06/14/2021
ms.service: azure-data-studio
ms.topic: how-to
---

# Create a parameterized notebook by using the notebook URI

*Parameterization* in Azure Data Studio is running the same notebook with a different set of parameters.

This article shows you how to create and run a parameterized notebook in Azure Data Studio by using the Python kernel.

> [!NOTE]
> Currently, you can use parameterization with Python, PySpark, PowerShell, and .NET Interactive kernels.

## Prerequisites

- [Azure Data Studio](../download-azure-data-studio.md)
- [Python](https://www.python.org/downloads/)

## URI parameterization

URI parameterization programmatically adds parameters to the query of the Azure Data Studio URI to open the notebook in Azure Data Studio with new parameters.

Azure Data Studio notebook URI supports HTTPS, HTTP, and FILE URI schema and uses this format:  

`azuredatastudio://microsoft.notebook/open?url=`

To pass in parameters with an Azure Data Studio notebook URI, use this format:  

`azuredatastudio://microsoft.notebook/open?url=LinkToNotebook?x=1&y=2`

In the URI query, use `&` to indicate a new parameter to be injected.

## Parameterization example

You can use an [example notebook file](https://github.com/microsoft/sql-server-samples/blob/master/samples/applications/azure-data-studio/parameterization.ipynb) to go through the steps in this article:

1. Go to the [notebook file in GitHub](https://github.com/microsoft/sql-server-samples/blob/master/samples/applications/azure-data-studio/parameterization.ipynb). Select **Raw**.
1. Select Ctrl+S or right-click, and then save the file with the .ipynb extension.  
1. Open the file in Azure Data Studio.

Here are the contents and structure of the notebook:

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

## Set up a notebook for parameterization

You can begin with the example notebook open in Azure Data Studio or complete the following steps to create a notebook. Then, try using different parameters. All the steps run inside an Azure Data Studio notebook.

1. The first code cell must be tagged with parameters. If you're using a new notebook file, make a new code cell. Select **Parameters** to tag the cell as a parameters cell.

   :::image type="content" source="media/notebooks-parameterization/make-parameter-cell.png" alt-text="Screenshot that shows creating a new parameters cell with Parameters selected.":::

1. You can use either the search bar of any browser or a Markdown cell to open the notebook URI link. Using the Azure Data Studio notebook URI (shown next), you can parameterize the example input notebook on GitHub.

   Copy the following notebook URI to parameterize the example input notebook on GitHub with new values for *x* and *y*. Paste the URI in a browser search bar:

   `azuredatastudio://microsoft.notebook/open?url=https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/applications/azure-data-studio/parameterization.ipynb?x=10&y=20`

   :::image type="content" source="media/notebooks-parameterization/search-bar.png" alt-text="Screenshot that shows the URI link in a browser search bar.":::

   When you open the link from the web browser, you're prompted to open the notebook in Azure Data Studio. Select **Open Azure Data Studio**.

   :::image type="content" source="media/notebooks-parameterization/download-prompt.png" alt-text="Screenshot that shows the download prompt.":::

1. You're prompted to download and open the notebook with new parameters.

   Select **Yes**, and then view the new parameterized notebook. On the toolbar, select **Run all** to run all cells and see the new output.

   A new cell labeled `# Injected-Parameters` contains the new parameter values that were passed in:
 
   :::image type="content" source="media/notebooks-parameterization/output-notebook.png" alt-text="Screenshot that shows the output for new parameters.":::

## Next steps

Learn more about notebooks and parameterization:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Papermill parameterization](./parameterize-papermill.md)
- [Run with Parameters](./run-with-parameters.md)
