--- 
 
# required metadata 
title: "dropColumns function (MicrosoftML) " 
description: " Specified columns to drop from the dataset. " 
keywords: "(MicrosoftML), dropColumns, transform" 
author: "dphansen"
ms.author: "davidph" 
manager: "cgronlun" 
ms.date: 07/15/2019
ms.topic: "reference" 
ms.prod: "mlserver" 
ms.service: "" 
ms.assetid: "" 
 
# optional metadata 
ROBOTS: "" 
audience: "" 
ms.devlang: "" 
ms.reviewer: "" 
ms.suite: "" 
ms.tgt_pltfrm: "" 
#ms.technology: "" 
ms.custom: "" 
 
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
--- 
 
 
 
 
 # dropColumns: Drops columns from the dataset 
 
 
Specified columns to drop from the dataset.
 
 
 ## Usage

```   
  dropColumns(vars, ...)
 
```
 
 ## Arguments

   
  
 ### `vars`
 A character vector or list of the names of the variables to drop. 
  
  
  
 ### ` ...`
 Additional arguments sent to compute engine. 
  
 
 
 ## Value
 
A `maml` object defining the transform.
 
 ## Author(s)
 
Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)

 
 
 
 
