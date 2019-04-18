---
title: SandDance for Azure Data Studio
titleSuffix: Azure Data Studio
description: How to use SandDance in Azure Data Studio
ms.custom: "seodec18"
ms.date: "04/18/2019"
ms.prod: sql
ms.technology: azure-data-studio
ms.reviewer: "alayu; sstein"
ms.topic: conceptual
author: "yualan"
ms.author: "alayu"
manager: craigg
---
# SandDance for Azure Data Studio (Preview)
Azure Data Studio now offers a way to create quick visualizations for the .csv and .tsv files that you are working on. This includes local files or files on HDFS in your SQL Server 2019 Big Data Cluster. This extension is helpful when you are trying to have a quick look at the data and understand what's going on. We use a technology called SandDance from Microsoft Research, which can generate in-place visualizations of the data.

![sanddance-animation](https://user-images.githubusercontent.com/11507384/54236654-52d42800-44d1-11e9-859e-6c5d297a46d2.gif)

By using easy-to-understand views, SandDance helps you find insights about your data, which in turn help you tell stories supported by data, build cases based on evidence, test hypotheses, dig deeper into surface explanations, support decisions for purchases, or relate data into a wider, real world context.

SandDance uses unit visualizations, which apply a one-to-one mapping between rows in your database and marks on the screen.
Smooth animated transitions between views help you to maintain context as you interact with your data.

## Usage

Right-click on a local .csv or .tsv file and choose *View in SandDance*.

Right-click on a .csv or .tsv file in HDFS if you are connected to SQL Server 2019 Big Data Cluster and choose *View in SandDance*.

## Known Issues

Currently your data should have the first column as a unique identifier.

Currently we do not cap the row count that is visualized. However, memory consumption goes up proportionally to the number of rows, so we recommend that the data set or view is limited to around 100k rows.

See [known issues](https://microsoft.github.io/SandDance/#known-issues)

## Release Notes

### 1.0.0

Initial release of azdata-sanddance

## Next Steps
To learn more, [visit the GitHub repo.](https://github.com/Microsoft/SandDance)