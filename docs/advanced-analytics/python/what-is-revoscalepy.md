---
title: "What is RevoScalePy | Microsoft Docs"
ms.custom: ""
ms.date: "04/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# What is RevoScalePy

**revoscalepy** (note: all lower case) is a new API provided by Microsoft R Server to support distributed computing, remote compute contexts, and high-performance algorithms for Python.

It is based on the **RevoScaleR** package for R, which was provided in Microsoft R Server and SQL Server R Services. It provides much the same features:

+ Multiple compute contexts
+ Functions equivalent to those in RevoScaleR for data transformation and visualization
+ Scalable, fast machine learning algorithms that provide distributed or parallel processing
+ Use of the Intel math libraries

## Versions and Supported Platforms

The **revoscalepy** library is available only when you install one of the following Microsoft products:

+ Machine Learning Services, in SQL Server vNext CTP 2.0

The **revoscalepy** library will also be included with Microsoft R Server later in 2017, after the SQL Server vNext release.

## API Reference

A summary of the included functions will be provided here.

## See Also

[Machine Learning Tutorials](../tutorials/machine-learning-services-tutorials.md)