--- 
 
# required metadata 
title: "n_gram: n_gram" 
description: "Extracts NGrams from text and convert them to vector using dictionary." 
keywords: "N-Grams" 
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

# *microsoftml.n_gram*: Converts text into features using n-grams





## Usage



```
microsoftml.n_gram(ngram_length: numbers.Real = 1,
    skip_length: numbers.Real = 0, all_lengths: bool = True,
    max_num_terms: list = [10000000], weighting: str = 'Tf')
```





## Description

Extracts NGrams from text and convert them to vector using dictionary.


## Arguments


### ngram_length

Ngram length (settings).


### skip_length

Maximum number of tokens to skip when constructing an ngram (settings).


### all_lengths

Whether to include all ngram lengths up to NgramLength or only NgramLength (settings).


### max_num_terms

Maximum number of ngrams to store in the dictionary (settings).


### weighting

The weighting criteria (settings).


## See also

[n_gram_hash](n-gram-hash.md),
[featurize_text](featurize-text.md)
