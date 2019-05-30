---
title: Generate code for data wrangling tasks
titleSuffix: Azure Data Studio
description: This article describes how to use the PROSE Code Accelerator in Azure Data Studio to automatically generate code for common data wrangling tasks.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 12/06/2018
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# Data Wrangling using PROSE Code Accelerator

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

PROSE Code Accelerator generates readable Python code for your data wrangling tasks. You can mix the generated code with your hand-written code in a seamless manner while working in a notebook within Azure Data Studio. This article provides an overview of how you can use the Code Accelerator.

 > [!NOTE]
 > Program Synthesis using Examples, aka PROSE, is a Microsoft technology that generates human-readable code using AI. It does so by analyzing a user's intent as well as data, generating several candidate programs, and picking the best program using ranking algorithms. To know more about the PROSE technology, visit the [PROSE homepage](https://microsoft.github.io/prose/).

The Code Accelerator comes pre-installed with Azure Data Studio. You can import it like any other Python package in the notebook. By convention, we import it as cx for short.

```python
import prose.codeaccelerator as cx
```

In the current release, the Code Accelerator can intelligently generate Python code for the following tasks:

- Reading data files to a Pandas or Pyspark dataframe.
- Fixing data types in a dataframe.
- Finding regular expressions representing patterns in a list of strings.

To get a general overview of Code Accelerator methods, see the [documentation](https://aka.ms/prose-codeaccelerator-overview).

## Reading data from a file to a dataframe

Often, reading files to a dataframe involves looking at the content of the file and determining the correct parameters to pass to a data-loading library. Depending on the complexity of the file, identifying the correct parameters may require several iterations.

PROSE Code Accelerator solves this problem by analyzing the structure of the data file and automatically generating code to load the file. In most cases, the generated code parses the data correctly. In a few cases, you might need to tweak the code to meet your needs.

Consider the following example:

 ```python
import prose.codeaccelerator as cx

# Call the ReadCsvBuilder builder to analyze the file content and generate code to load it
builder = cx.ReadCsvBuilder(r'C:/911.txt')

#Set target to pyspark if generating code to use pyspark library
#builder.Target = "pyspark"

#Get the code generated to fix the data types
builder.learn().code()
 ```

The previous code block prints the following python code to read a delimited file. Notice how PROSE automatically figures out the number of lines to skip, headers, quotechars, delimiters, etc.

 ```python
import pandas as pd

def read_file(file):
    names = ["lat",
             "lng",
             "desc",
             "zip",
             "title"]

    df = pd.read_csv(file,
        skiprows = 11,
        header = None,
        names = names,
        quotechar = "\"",
        delimiter = "|",
        index_col = False,
        dtype = str,
        na_values = [],
        keep_default_na = False,
        skipinitialspace = True)
    return df
 ```

Code Accelerator can generate code to load delimited, JSON, and fixed-width files to a dataframe. For reading fixed-width files, the `ReadFwfBuilder` optionally takes a human-readable schema file that it can parse to get the column positions. To learn more, see the [documentation](https://aka.ms/prose-codeaccelerator-docs).

## Fixing data types in a dataframe

It is common to have a pandas or pyspark dataframe with wrong data types. Often, this happens because of a few non-conforming values in a column. As a result, Integers are read as Float or Strings, and Dates are read as Strings. The effort required to manually fix the data types is proportional to the number of columns.

You can use the `DetectTypesBuilder` in these situations. It analyzes the data, and rather than fixing the data types in a black-box manner, it generates code for fixing the data types. The code serves as a starting point. You can review, use, or modify it as needed.

```python
import prose.codeaccelerator as cx

builder = cx.DetectTypesBuilder(df)

#Set target to pyspark if working with pyspark
#builder.Target = "pyspark"

#Get the code generated to fix the data types
builder.learn().code()
```

To learn more, see the [documentation](https://aka.ms/prose-codeaccelerator-fixtypes).

## Identifying patterns in Strings

Another common scenario is to detect patterns in a string column for the purpose of cleaning or grouping. For example, you may have a date column with dates in multiple different formats. In order to standardize the values, you might want to write conditional statements using regular expressions.


|   |Name                      |BirthDate      |
|---|:-------------------------|:--------------|
| 0 |Bertram du Plessis        |1995           |
| 1 |Naiara Moravcikova        |Unknown        |
| 2 |Jihoo Spel                |2014           |
| 3 |Viachaslau Gordan Hilario |22-Apr-67      |
| 4 |Maya de Villiers          |19-Mar-60      |

Depending on the volume and diversity in data, writing regular expressions for different patterns in the column can be a very time consuming task. The `FindPatternsBuilder` is a powerful code acceleration tool that solves the above problem by generating regular expressions for a list of Strings.

```python
import prose.codeaccelerator as cx

builder = cx.FindPatternsBuilder(df['BirthDate'])

#Set target to pyspark if working with pyspark
#builder.Target = "pyspark"

builder.learn().regexes
```

Here are the regular expressions generated by the `FindPatternsBuilder` for the above data.

```
^[0-9]{2}-[A-Z][a-z]+-[0-9]{2}$
^[0-9]{2}[\s][A-Z][a-z]+[\s][0-9]{4}$
^[0-9]{4}$
^Unknown$
```

Apart from generating Regular Expressions, `FindPatternsBuilder` can also generate code for clustering the values based on generated regexes. It can also assert that all the values in a column conform to the generated regular expressions. To learn more and see other useful scenarios, see the [documentation](https://aka.ms/prose-codeaccelerator-findpatterns).
