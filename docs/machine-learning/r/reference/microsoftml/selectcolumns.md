---
title: "selectColumns function (MicrosoftML)"
description: "Selects a set of columns to retrain, dropping all others (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), selectColumns, transform
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
 
 
 # selectColumns: Selects a set of columns, dropping all others 
 
 
Selects a set of columns to retrain, dropping all others.
 
 
 ## Usage

```   
  selectColumns(vars, ...)
 
```
 
 ## Arguments

   
  
 ### `vars`
 Specifies character vector or list of the names of the variables to keep. 
  
  
  
 ### ` ...`
 Additional arguments sent to compute engine. 
  
 
 
 ## Value
 
A `maml` object defining the transform.
 
 ## Author(s)
 
Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)

 
 
 
 
