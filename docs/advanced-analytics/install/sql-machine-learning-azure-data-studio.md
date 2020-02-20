---
title: Use Azure Data Studio notebooks
description: Learn how to run Python scripts in a notebooks in Azure Data Studio with SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 01/31/2020
ms.topic: conceptual
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Use Azure Data Studio notebooks with SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Learn how to run Python and R scripts in a notebooks in [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/what-is) with [SQL Server Machine Learning Services](../what-s-new-in-sql-server-machine-learning-services.md). Azure Data Studio is a cross-platform database tool for data professionals using the Microsoft family of on-premises and cloud data platforms on Windows, MacOS, and Linux.

## Prerequisites

- [Download and install Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio) on your workstation computer. Azure Data Studio is cross-platform, and runs on Windows, MacOS, and Linux.
- A server with SQL Server Machine Learning Services installed. You can run Machine Learning Services on Windows, Linux, or SQL Server Big Data Clusters:
    - [Install SQL Server Machine Learning Services on Windows](sql-machine-learning-services-windows-install.md) 
    - [Install SQL Server Machine Learning Services on Linux](../../linux/sql-server-linux-setup-machine-learning.md)
    - [Run Python and R scripts with Machine Learning Services on SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md).

## Create a SQL notebook

You can use Machine Learning Services in Azure Data Studio with a SQL notebook. To create a new notebook, follow these steps:

1. Click **new connection** to connect to your SQL Server.

1. Enter your **Connection Details** and click **Connect**.

1. Click **New Notebook** to create a new notebook. The notebook will by default use the **SQL kernel**.

> [!IMPORTANT]
> Machine Learning Services runs as part of SQL Server. Therefore, you need to use a SQL notebook and not a Python notebook.

## Example

## Next steps
