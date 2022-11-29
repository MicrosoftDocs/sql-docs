---
title: Import or view models with Machine Learning extension
description: Learn how to use Machine Learning extension for Azure Data Studio to import an ONNX model or view already imported models in your database.
author: rothja
ms.author: jroth
ms.date: 06/09/2020
ms.service: azure-data-studio
ms.topic: conceptual
---

# Import or view models with Machine Learning extension for Azure Data Studio (Preview)

Learn how to use the [Machine Learning extension for Azure Data Studio](machine-learning-extension.md) to import an ONNX model or view already imported models in your database.

> [!IMPORTANT]
> Import and view in a database with the Machine Learning extension currently only supports [Machine Learning Services in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/machine-learning-services-overview) and [Azure SQL Edge with ONNX](/azure/azure-sql-edge/onnx-overview).

## Prerequisites

- Install and configure [Machine Learning extension for Azure Data Studio](machine-learning-extension.md). You need to specify the [Python installation paths in the Extension Settings](machine-learning-extension.md#settings).

- The **onnxruntime**, **mlflow**, and **mlflow-dbstore** Python packages. If the packages are not already installed, the Machine Learning extension will prompt you to install them.

## View models

Follow the steps below to view ONNX models that are stored in your database.

1. Select **Import or view models**.

1. If you're asked to install **onnxruntime**, **mlflow**, and **mlflow-dbstore**, select **Yes**.

1. Select the **Models database** and **Models table** where your models are stored in.

This will show a list of your models. You can edit the model name and description, or delete a model from the list.

## Import a new model

Follow the steps below to import an ONNX model in your database.

1. Select **Import or view models**.

1. If you're asked to install **onnxruntime**, **mlflow**, and **mlflow-dbstore**, select **Yes**.

1. Select **Import models**.

1. Select the **Models database** you want to store the imported model in.

1. Select the **Models table** you want to store the imported model in. You can either choose an **Existing table** or create a **New table**. Select **Next**.

1. Select where your model is located and Select **Next**. You can use:
    - **File upload**. Choose this to use a model from a file. Select the model file under **Source files** and Select **Next**.
    - **Azure Machine Learning**. Choose this to use a model from Azure Machine Learning. First, **Sign in to Azure**. Then select your **Azure account**, **Azure subscription**, **Azure resource group**, and **Azure ML workspace**. Select the model you want to use and Select **Next**.

1. Enter the model **Name** and **Description** and Select **Deploy** to store the model in your database.

> [!NOTE]
> The Machine Learning extension is currently in preview. Therefore, the table schema where the models are stored might change in the future.

## Next steps

- [Machine Learning extension in Azure Data Studio](machine-learning-extension.md)
- [Manage packages in database](machine-learning-extension-manage-packages.md)
- [Make predictions](machine-learning-extension-predictions.md)
- [SQL machine learning documentation](../../machine-learning/index.yml)
- [Machine Learning Services in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/machine-learning-services-overview)
- [Machine learning and AI with ONNX in SQL Edge (Preview)](/azure/azure-sql-edge/onnx-overview)
