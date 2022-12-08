---
title: "Query function (olapR)"
description: "Query constructs a Query object. Set functions are used to build and modify the Query axes and cube name."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (olapR), Query
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


# Query: olapR Query Construction 



`Query` constructs a "Query" object. Set functions are used to build and modify the Query axes and cube name.



## Usage

```   
  Query(validate = FALSE)

  cube(qry)
  cube(qry) <- cubeName

  columns(qry)
  columns(qry) <- axis

  rows(qry)
  rows(qry) <- axis

  pages(qry)
  pages(qry) <- axis

  chapters(qry)
  chapters(qry) <- axis

  axis(qry, n)
  axis(qry, n) <- axis

  slicers(qry)
  slicers(qry) <- axis

  compose(qry)

  is.Query(qry)

```


## Arguments



### `validate`
 A logical (TRUE, FALSE, NA) specifying whether the Query should be validated during execution 


### `qry`
 An object of class "Query" returned by `Query` 


### `cubeName`
 A string specifying the name of the cube to query 


### `axis`
 A vector of strings specifying an axis. See example below. 


### `n`
 An integer representing the axis number to be set. axis(qry, 1) == columns(qry), axis(qry, 2) == pages(qry), etc. 




## Details

`Query` is the constructor for the Query object. Set functions are used to specify what the Query should return. Queries are passed to the `Execute2D` and `ExecuteMD` functions. `compose` takes the Query object and generates an MDX string equivalent to the one that the Execute functions would generate and use.



## Value

`Query` returns an object of type "Query". 
`cube` returns a string. 
`columns` returns a vector of strings. 
`rows` returns a vector of strings. 
`pages` returns a vector of strings. 
`sections` returns a vector of strings. 
`axis` returns a vector of strings. 
`slicers` returns a vector of strings. 
`compose` returns a string. 
`is.Query` returns a boolean.


## Notes

- A Query object is not as powerful as pure MDX. If the Query API is not sufficient, try using an MDX Query string with one of the Execute functions.



## References
  See [execute2D](Execute2D.md) or [executeMD](ExecuteMD.md) for references.  


## See also

[execute2D](Execute2D.md), [executeMD](ExecuteMD.md), [OlapConnection](OlapConnection.md), [explore](Explore.md)


## Examples

 ```

  qry <- Query(validate = TRUE)

  cube(qry) <- "[Analysis Services Tutorial]"

  columns(qry) <- c("[Measures].[Internet Sales Count]", "[Measures].[Internet Sales-Sales Amount]")
  rows(qry) <- c("[Product].[Product Line].[Product Line].MEMBERS") 
  axis(qry, 3) <- c("[Date].[Calendar Quarter].MEMBERS")

  slicers(qry) <- c("[Sales Territory].[Sales Territories].[Sales Territory Region].[Northwest]")

  print(cube(qry)) #[Analysis Services Tutorial]
  print(axis(qry, 2)) #c("[Product].[Product Line].[Product Line].MEMBERS") 

  print(compose(qry))  #SELECT {[Measures].[Internet Sales Count], [Measures].[Internet Sales-Sales Amount]} ON AXIS(0), {[Product].[Product Line].[Product Line].MEMBERS} ON AXIS(1), {[Date].[Calendar Quarter].MEMBERS} ON AXIS(2) FROM [Analysis Services Tutorial] WHERE {[Sales Territory].[Sales Territories].[Sales Territory Region].[Northwest]}
```

