---
title: "gpu_math: gpu_math"
description: "NVidia CUDA implementation."
author: VanMSFT
ms.author: vanto
ms.date: 07/15/2019
ms.service: sql
ms.subservice: "machine-learning-services"
ms.topic: "reference"
keywords:
  - neural network
  - math
  - gpu
ms.devlang: python
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
