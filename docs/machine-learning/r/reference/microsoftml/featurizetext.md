---
title: "stopwordsDefault function (MicrosoftML)"
description: "Text transforms that can be performed on data before training  a model."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), stopwordsDefault, featurizeText, stopwordsCustom, termDictionary, transform
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---





 # stopwordsDefault: Machine Learning Text Transform 
 

Text transforms that can be performed on data before training 
a model.


 ## Usage

```   
  stopwordsDefault()

  stopwordsCustom(dataFile = "")

  termDictionary(terms = "", dataFile = "", sort = "occurrence")

  featurizeText(vars, language = "English", stopwordsRemover = NULL,
    case = "lower", keepDiacritics = FALSE, keepPunctuations = TRUE,
    keepNumbers = TRUE, dictionary = NULL,
    wordFeatureExtractor = ngramCount(), charFeatureExtractor = NULL,
    vectorNormalizer = "l2", ...)

```

 ## Arguments



 ### `dataFile`
 character: \<string\>. Data file containing the terms (short form data). 



 ### `terms`
 An optional character vector of terms or categories. 



 ### `sort`
 Specifies how to order items when vectorized. Two orderings are supported:   
*   `"occurrence"`: items appear in the order encountered.    
*   `"value"`: items are sorted according to their default comparison.  For example, text sorting will be case sensitive (e.g., 'A' then 'Z'   then 'a').   




 ### `vars`
 A named list of character vectors of input variable names and the name of the output variable. Note that the input variables must be of the same type. For one-to-one mappings between input and output variables, a named character vector can be used. 



 ### `language`
 Specifies the language used in the data set. The following  values are supported:   
*   `"AutoDetect"`: for automatic language detection.    
*   `"English"`.    
*   `"French"`.    
*   `"German"`.    
*   `"Dutch"`.    
*   `"Italian"`.    
*   `"Spanish"`.    
*   `"Japanese"`.   




 ### `stopwordsRemover`
 Specifies the stopwords remover to use. There are three options supported:   
*   `NULL` No stopwords remover is used.   
*   `stopwordsDefault`: A precompiled language-specific list of stop words is used that includes the most common words from Microsoft Office.    
*   `stopwordsCustom`: A user-defined list of stopwords. It accepts   the following option: `dataFile`.   
 The default value is `NULL`. 



 ### `case`
 Text casing using the rules of the invariant culture. Takes the following values:   
*   `"lower"`.   
*   `"upper"`. 
*   `"none"`.   
  The default value is `"lower"`. 



 ### `keepDiacritics`
 `FALSE` to remove diacritical marks; `TRUE` to  retain diacritical marks. The default value is `FALSE`. 



 ### `keepPunctuations`
 `FALSE` to remove punctuation; `TRUE` to  retain punctuation. The default value is `TRUE`. 



 ### `keepNumbers`
 `FALSE` to remove numbers; `TRUE` to retain numbers. The default value is `TRUE`. 



 ### `dictionary`
 A `termDictionary` of allowlisted terms which accepts the following options:  
*   `terms`,   
*   `dataFile`, and  
*   `sort`.   
The default value is `NULL`.  Note that the stopwords list takes precedence over the dictionary allowlist as the stopwords are removed before the dictionary terms are allowlisted. 



 ### `wordFeatureExtractor`
 Specifies the word feature extraction arguments. There  are two different feature extraction mechanisms:   
*   [ngramCount](ngram.md): Count-based feature extraction (equivalent   to WordBag). It accepts the following options: `maxNumTerms` and `weighting`.    
*   [ngramHash](ngram.md): Hashing-based feature extraction (equivalent  to WordHashBag). It accepts the following options: `hashBits`,  `seed`, `ordered` and `invertHash`.   
 The default value is `ngramCount`. 



 ### `charFeatureExtractor`
 Specifies the char feature extraction arguments. There  are two different feature extraction mechanisms:   
*   [ngramCount](ngram.md): Count-based feature extraction (equivalent   to WordBag). It accepts the following options: `maxNumTerms` and `weighting`.    
*   [ngramHash](ngram.md): Hashing-based feature extraction (equivalent  to WordHashBag). It accepts the following options: `hashBits`,  `seed`, `ordered` and `invertHash`.   
 The default value is `NULL`. 



 ### `vectorNormalizer`
 Normalize vectors (rows) individually by rescaling them to unit norm. Takes one of the following values:   
*   `"none"`.    
*   `"l2"`.    
*   `"l1"`.    
*   `"linf"`. 
 The default value is `"l2"`. 



 ### ` ...`
 Additional arguments sent to the compute engine. 



 ## Details

The `featurizeText` transform produces a bag of counts of   
sequences of consecutive words, called n-grams, from a given corpus of text. 
There are two ways it can do this: 


build a dictionary of n-grams and use the ID in the dictionary as the index in the bag;

hash each n-gram and use the hash value as the index in the bag.


The purpose of hashing is to convert variable-length text documents into 
equal-length numeric feature vectors, to support dimensionality reduction 
and to make the lookup of feature weights faster.

The text transform is applied to text input columns. It offers language
detection, tokenization, stopwords removing, text normalization and feature
generation. It supports the following languages by default: English, French,
German, Dutch, Italian, Spanish and Japanese.

The n-grams are represented as count vectors, with vector slots
corresponding either to n-grams (created using `ngramCount`) or to 
their hashes (created using `ngramHash`). Embedding ngrams in a vector 
space allows their contents to be compared in an efficient manner.
The slot values in the vector can be weighted by the following factors:


term frequency - The number of occurrences of the slot in the text 

inverse document frequency - A ratio (the logarithm of 
inverse relative slot frequency) that measures the information a slot 
provides by determining how common or rare it is across the entire text.

term frequency-inverse document frequency - the product
term frequency and the inverse document frequency.




 ## Value

A `maml` object defining the transform.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## See also

[ngramCount](ngram.md), [ngramHash](ngram.md),
[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md), [rxNeuralNet](rxNeuralNet.md),
[rxOneClassSvm](rxOneClassSvm.md), [rxLogisticRegression](rxLogisticRegression.md).

 ## Examples

 ```

  trainReviews <- data.frame(review = c( 
          "This is great",
          "I hate it",
          "Love it",
          "Do not like it",
          "Really like it",
          "I hate it",
          "I like it a lot",
          "I kind of hate it",
          "I do like it",
          "I really hate it",
          "It is very good",
          "I hate it a bunch",
          "I love it a bunch",
          "I hate it",
          "I like it very much",
          "I hate it very much.",
          "I really do love it",
          "I really do hate it",
          "Love it!",
          "Hate it!",
          "I love it",
          "I hate it",
          "I love it",
          "I hate it",
          "I love it"),
       like = c(TRUE, FALSE, TRUE, FALSE, TRUE,
          FALSE, TRUE, FALSE, TRUE, FALSE, TRUE, FALSE, TRUE,
          FALSE, TRUE, FALSE, TRUE, FALSE, TRUE, FALSE, TRUE, 
          FALSE, TRUE, FALSE, TRUE), stringsAsFactors = FALSE
      )

      testReviews <- data.frame(review = c(
          "This is great",
          "I hate it",
          "Love it",
          "Really like it",
          "I hate it",
          "I like it a lot",
          "I love it",
          "I do like it",
          "I really hate it",
          "I love it"), stringsAsFactors = FALSE)


  outModel <- rxLogisticRegression(like ~ reviewTran, data = trainReviews,
      mlTransforms = list(featurizeText(vars = c(reviewTran = "review"),
      stopwordsRemover = stopwordsDefault(), keepPunctuations = FALSE)))
  # 'hate' and 'love' have non-zero weights
  summary(outModel)

  # Use the model to score
  scoreOutDF5 <- rxPredict(outModel, data = testReviews, 
      extraVarsToWrite = "review")
  scoreOutDF5
```



