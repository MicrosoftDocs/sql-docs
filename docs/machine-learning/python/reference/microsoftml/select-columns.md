--- 
 
# required metadata 
title: "select_columns: Selects a set of columns and drops all others" 
description: "Selects a set of columns to retrain, dropping all others." 
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

# *microsoftml.select_columns*: Retains columns of a dataset





## Usage



```
microsoftml.select_columns(cols: [list, str], **kargs)
```





## Description

Selects a set of columns to retrain, dropping all others.


## Arguments


### cols

A character string or list of the names of the variables to keep.


### kargs

Additional arguments sent to compute engine.


## Returns

An object defining the transform.


## See also

[`concat`](concat.md),
[`drop_columns`](drop-columns.md).
