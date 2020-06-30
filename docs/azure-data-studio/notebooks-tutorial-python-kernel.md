---
title: Notebooks with Python Kernel in Azure Data Studio
description: This tutorial shows how you can create and run a Python notebook.
author: garyericson
ms.author: garye
ms.reviewer: mikeray, alayu, maghan
ms.topic: tutorial
ms.prod: azure-data-studio
ms.technology: 
ms.custom: ""
ms.date: 04/27/2020
---

# Create and run a Python notebook

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/_ss2019.md)]

This tutorial demonstrates how to create and run a notebook in Azure Data Studio using the Python kernel.

## Prerequisites

- [Azure Data Studio installed](download-azure-data-studio.md)

## New notebook

The following steps show how to create a notebook file in Azure Data Studio:

1. Open Azure Data Studio. If you're prompted to connect to a SQL Server, you may connect or click **Cancel**.

1. Select **New Notebook** in the **File** menu.

1. Select **Python 3** for the **Kernel**.

   :::image type="content" source="media/notebook-tutorial-python/set-kernel-and-attach-to-python.png" alt-text="Set Kernel":::

1. If you're prompted to configure Python, then in **Configure Python for Notebooks** select either:

   - **New Python installation** to install a new copy of Python for Azure Data Studio, or
   - **Use existing Python installation** to specify the path to an existing Python installation

## Run a notebook cell

You can create cells containing code or text. You can run a code cell in place and the results are shown in the notebook after the cell is finished running. Text cells allow you to intersperse formatted documentation with your code.

### Code

Add a new Python code cell by selecting the **+Code** command in the toolbar.

:::image type="content" source="media/notebook-tutorial-python/notebook-toolbar-python.png" alt-text="Notebook toolbar":::

This example does simple math.

```python
a = 1
b = 2
c = a/b
print(c)
```
Run the cell by clicking the play button to the left of the cell. The results appear below.

:::image type="content" source="media/notebook-tutorial-python/run-notebook-cell-python.png" alt-text="Run notebook cell":::

### Text

Add a new text cell by selecting the **+Text** command in the toolbar.

:::image type="content" source="media/notebook-tutorial-python/notebook-toolbar-python-text.png" alt-text="Notebook toolbar":::

The cell changes to edit mode and you can now type markdown and see the preview at the same time.

:::image type="content" source="media/notebook-tutorial-python/notebook-markdown-cell-python.png" alt-text="Markdown cell":::

Selecting outside the text cell shows just the markdown text.

:::image type="content" source="media/notebook-tutorial-python/notebook-markdown-preview-python.png" alt-text="Markdown text":::

## Next steps

Learn more about notebooks:

- [How to use notebooks with SQL Server](notebooks-guidance.md)

- [Create and run a SQL Server notebook](notebooks-tutorial-sql-kernel.md)

- [How to manage notebooks in Azure Data Studio](notebooks-manage-sql-server.md)
