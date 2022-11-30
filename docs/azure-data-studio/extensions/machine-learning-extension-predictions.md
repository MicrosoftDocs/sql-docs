---
title: Make predictions with Machine Learning extension
description: Learn how to use Machine Learning extension for Azure Data Studio to make predictions with an ONNX model in your database.
author: rothja
ms.author: jroth
ms.date: 06/09/2020
ms.service: azure-data-studio
ms.topic: conceptual
---

# Make predictions with Machine Learning extension for Azure Data Studio (Preview)

Learn how to use the [Machine Learning extension for Azure Data Studio](machine-learning-extension.md) to make predictions with an ONNX model in your database. The extension will generate a T-SQL script using [PREDICT](../../t-sql/queries/predict-transact-sql.md) to make predictions on the dataset stored in your table with a model that is previously imported, resides in a local file, or from Azure Machine Learning.

> [!IMPORTANT]
> Make predictions with the Machine Learning extension currently only supports [Machine Learning Services in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/machine-learning-services-overview) and [Azure SQL Edge with ONNX](/azure/azure-sql-edge/onnx-overview).

## Prerequisites

- Install and configure [Machine Learning extension for Azure Data Studio](machine-learning-extension.md). You need to specify the [Python installation paths in the Extension Settings](machine-learning-extension.md#settings).

- The **onnxruntime**, **mlflow**, and **mlflow-dbstore** Python packages. If the packages are not already installed, the Machine Learning extension will prompt you to install them.

## Make predictions from ONNX model

Follow the steps below to use an ONNX model to make predictions.

1. Select **Make predictions**.

1. If you're asked to install **onnxruntime**, **mlflow**, and **mlflow-dbstore**, select **Yes**.

1. Choose where your model is located and select **Next**. You can use:
    - **Imported models**. Choose this to use a model that is already stored in your database. Choose the **Model database** and **Model table** where your model is located, select the model you want to use, and select **Next**.
    - **File upload**. Choose this to use a model from a file. Select the model file under **Source files** and select **Next**.
    - **Azure Machine Learning**. Choose this to use a model from Azure Machine Learning. First, **Sign in to Azure**. Then select your **Azure account**, **Azure subscription**, **Azure resource group**, and **Azure ML workspace**. Select the model you want to use and select **Next**.

1. Map the source data to your model.
    - Select the **Source database** and **Source table** containing the data set for which you want to apply the prediction.
    - Map the columns under **Model Input mapping** and **Model output**. The extension will automatically map columns that have the same name and data type.

1. Select **Predict**.

Azure Data Studio will create a new T-SQL query with the [PREDICT](../../t-sql/queries/predict-transact-sql.md), which you can use to make predictions on your data.

## Next steps

- [Machine Learning extension in Azure Data Studio](machine-learning-extension.md)
- [Manage packages in database](machine-learning-extension-manage-packages.md)
- [Import or view models](machine-learning-extension-import-view-models.md)
- [Notebooks in Azure Data Studio](../notebooks/notebooks-guidance.md)
- [SQL machine learning documentation](../../machine-learning/index.yml)
- [Machine Learning Services in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/machine-learning-services-overview)
- [Machine learning and AI with ONNX in SQL Edge (Preview)](/azure/azure-sql-edge/onnx-overview)