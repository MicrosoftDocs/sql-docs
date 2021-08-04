--- 
 
# required metadata 
title: "adadelta_optimizer: adadelta_optimizer" 
description: "Adaptive learning rate method." 
keywords: "optimizer, adadelta" 
author: "garyericson"
ms.author: "garye" 
manager: "cgronlun" 
ms.date: 07/15/2019
ms.topic: "reference" 
ms.prod: "sql"
ms.technology: "machine-learning-services" 
ms.service: "" 
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

# *microsoftml.adadelta_optimizer*: Adaptive learing rate method





## Usage



```
microsoftml.adadelta_optimizer(decay: numbers.Real = 0.95,
    cond: numbers.Real = 1e-06)
```





## Description

Adaptive learning rate method.


## Arguments


### decay

Decay rate (settings).


### cond

Condition constant (settings).


## See also

[`sgd_optimizer`](sgd-optimizer.md)
