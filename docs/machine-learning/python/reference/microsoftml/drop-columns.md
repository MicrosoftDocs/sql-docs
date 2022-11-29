--- 
 
# required metadata 
title: "drop_columns: Drops columns from the dataset" 
description: "Specified columns to drop from the dataset." 
keywords: "transform, schema" 
author: WilliamDAssafMSFT
ms.author: wiassaf 
ms.date: 07/15/2019
ms.topic: "reference" 
ms.service: sql
ms.subservice: "machine-learning-services" 
ms.assetid: "" 
 
# optional metadata 
ROBOTS: "" 
audience: "" 
ms.devlang: "Python" 
ms.reviewer: "" 
ms.suite: "" 
ms.tgt_pltfrm: "" 
ms.custom: "" 
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
 
---

# *microsoftml.drop_columns*: Drops columns from a dataset





## Usage



```
microsoftml.drop_columns(cols: [list, str], **kargs)
```





## Description

Specified columns to drop from the dataset.


## Arguments


### cols

A character string or list of the names of the variables to drop.


### kargs

Additional arguments sent to compute engine.


## Returns

An object defining the transform.


## See also

[`concat`](concat.md),
[`select_columns`](select-columns.md).
