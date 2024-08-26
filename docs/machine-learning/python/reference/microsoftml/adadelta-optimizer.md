---
title: "adadelta_optimizer: adadelta_optimizer"
description: "Adaptive learning rate method."
author: VanMSFT
ms.author: vanto
ms.date: 07/15/2019
ms.service: sql
ms.subservice: "machine-learning-services"
ms.topic: "reference"
keywords:
  - optimizer
  - adadelta
ms.devlang: python
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
