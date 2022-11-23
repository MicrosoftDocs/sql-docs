---
title: "executeMD function (olapR)"
description: "Takes a Query object or an MDX string, and returns the result as a multi-dimensional array."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (olapR), executeMD
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


# executeMD: olapR executeMD Methods 



Takes a Query object or an MDX string, and returns the result as a multi-dimensional array.



## Usage

```   
  executeMD(olapCnn, query)
  executeMD(olapCnn, mdx)

```


## Arguments



### `olapCnn`
 Object of class "OlapConnection" returned by `OlapConnection()` 


### `query`
 Object of class "Query" returned by `Query()` 


### `mdx`
 String specifying a valid MDX query 




## Details

If a Query is provided:
`executeMD` validates a Query object (optional), generates an mdx query string from the Query object, executes the mdx query across an XMLA connection, and returns the result  as a multi-dimensional array.

If an MDX string is provided:
`executeMD` executes the mdx query across an XMLA connection, and returns the result  as a multi-dimensional array.



## Value

Returns a multi-dimensional array.
Returns an error if the Query is invalid.


## Notes





## References

Creating a Demo OLAP Cube (the same as the one used in the examples): [Multidimensional Modeling (Adventure Works Tutorial)](/analysis-services/multidimensional-tutorial/multidimensional-modeling-adventure-works-tutorial)

## See also

[Query](Query.md), [OlapConnection](OlapConnection.md), [execute2D](Execute2D.md), [explore](Explore.md), array

## Examples

 ```

  cnnstr <- "Data Source=localhost; Provider=MSOLAP;"
  olapCnn <- OlapConnection(cnnstr)

  qry <- Query()

  cube(qry) <- "[Analysis Services Tutorial]"
  columns(qry) <- c("[Measures].[Internet Sales Count]", "[Measures].[Internet Sales-Sales Amount]")
  rows(qry) <- c("[Product].[Product Line].[Product Line].MEMBERS") 
  pages(qry) <- c("[Sales Territory].[Sales Territory Region].[Sales Territory Region].MEMBERS")

  result1 <- executeMD(olapCnn, qry)

  mdx <- "SELECT {[Measures].[Internet Sales Count], [Measures].[Internet Sales-Sales Amount]} ON AXIS(0), {[Product].[Product Line].[Product Line].MEMBERS} ON AXIS(1), {[Sales Territory].[Sales Territory Region].[Sales Territory Region].MEMBERS} ON AXIS(2) FROM [Analysis Services Tutorial]"

  result2 <- executeMD(olapCnn, mdx)
```
