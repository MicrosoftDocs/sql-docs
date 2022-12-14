---
title: "execute2D function (olapR)"
description: "Takes a Query object or an MDX string, and returns the result as a data frame."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (olapR), execute2D
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


# execute2D: olapR execute2D Methods 



Takes a Query object or an MDX string, and returns the result as a data frame.



## Usage

```   
  execute2D(olapCnn, query)
  execute2D(olapCnn, mdx)

```


## Arguments



### `olapCnn`
 Object of class "OlapConnection" returned by `OlapConnection()` 


### `query`
 Object of class "Query" returned by `Query()` 


### `mdx`
 String specifying a valid MDX query 




## Details

If a query is provided:
`execute2D` validates a query object (optional), generates an mdx query string from the query object, executes the mdx query across, and returns the result as a data frame.

If an MDX string is provided:
`execute2D` executes the mdx query, and returns the result as a data frame.



## Value

A data frame if the MDX command returned a result-set. 
`TRUE` and a warning if the query returned no data. 
An error if the query is invalid


## Notes

Multi-dimensional query results are flattened to 2D using a standard flattening algorithm.



## References

Creating a Demo OLAP Cube (the same as the one used in the examples):

- [Multidimensional Modeling (Adventure Works Tutorial)](/analysis-services/multidimensional-tutorial/multidimensional-modeling-adventure-works-tutorial)




## See also

[Query](Query.md), [OlapConnection](OlapConnection.md), [executeMD](ExecuteMD.md), [explore](Explore.md), data.frame


## Examples

 ```

  cnnstr <- "Data Source=localhost; Provider=MSOLAP;"
  olapCnn <- OlapConnection(cnnstr)

  qry <- Query()

  cube(qry) <- "[Analysis Services Tutorial]"
  columns(qry) <- c("[Measures].[Internet Sales Count]", "[Measures].[Internet Sales-Sales Amount]")
  rows(qry) <- c("[Product].[Product Line].[Product Line].MEMBERS") 
  pages(qry) <- c("[Sales Territory].[Sales Territory Region].[Sales Territory Region].MEMBERS")

  result1 <- execute2D(olapCnn, qry)

  mdx <- "SELECT {[Measures].[Internet Sales Count], [Measures].[Internet Sales-Sales Amount]} ON AXIS(0), {[Product].[Product Line].[Product Line].MEMBERS} ON AXIS(1), {[Sales Territory].[Sales Territory Region].[Sales Territory Region].MEMBERS} ON AXIS(2) FROM [Analysis Services Tutorial]"

  result2 <- execute2D(olapCnn, mdx)
```
