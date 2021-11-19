--- 
 
# required metadata 
title: "gpu_math: gpu_math" 
description: "NVidia CUDA implementation." 
keywords: "neural network, math, gpu" 
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

# *microsoftml.gpu_math*: Acceleration with NVidia CUDA





## Usage



```
microsoftml.gpu_math(gpu_id: numbers.Real = -1,
    cu_dnn: bool = False, cu_dnn_algo: str = 'ImplicitPrecompGemm')
```





## Description

NVidia CUDA implementation.


## Arguments


### gpu_id

GPU device id (settings).


### cu_dnn

Use cuDNN on GPU (settings).


### cu_dnn_algo

cuDNN optimization options (settings).


## See also

[`avx_math`](avx-math.md),
[`clr_math`](clr-math.md),
[`mkl_math`](mkl-math.md),
[`sse_math`](sse-math.md)
