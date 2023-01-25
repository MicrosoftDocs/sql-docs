---
title: "explore function (olapR)"
description: "Allows for exploration of cube metadata."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (olapR), explore
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---

# explore: olapR explore Method 



Allows for exploration of cube metadata



## Usage

```   
  explore(olapCnn, cube = NULL, dimension = NULL, hierarchy = NULL, level = NULL)

```


## Arguments



### `olapCnn`
 Object of class "OlapConnection" returned by `OlapConnection()` 


### `cube`
 A string specifying a cube name 


### `dimension`
 A string specifying a dimension name 


### `hierarchy`
 A string specifying a hierarchy name 


### `level`
 A string specifying a level name 




## Details

`explore` 



## Value

Prints cube metadata. Returns NULL.
An error is thrown if arguments are invalid.


## Notes

Arguments must be specified in order. For example: In order to explore hierarchies, a dimension and a cube must be specified.



## References
  See [execute2D](Execute2D.md) or [executeMD](ExecuteMD.md) for references.  


## See also

query, [OlapConnection](OlapConnection.md), [executeMD](ExecuteMD.md), [execute2D](Execute2D.md)


## Examples

 ```

  cnnstr <- "Data Source=localhost; Provider=MSOLAP;"
  ocs <- OlapConnection(cnnstr)

  #Exploring Cubes
  explore(ocs)
  #Analysis Services Tutorial
  #Internet Sales
  #Reseller Sales
  #Sales Summary
  #[1] TRUE

  #Exploring Dimensions
  explore(ocs, "Analysis Services Tutorial")
  #Customer
  #Date
  #Due Date
  #Employee
  #Internet Sales Order Details
  #Measures
  #Product
  #Promotion
  #Reseller
  #Reseller Geography
  #Sales Reason
  #Sales Territory
  #Ship Date
  #[1] TRUE

  #Exploring Hierarchies
  explore(ocs, "Analysis Services Tutorial", "Product")
  #Category
  #Class
  #Color
  #Days To Manufacture
  #Dealer Price
  #End Date
  #List Price
  #Model Name
  #Product Categories
  #Product Line
  #Product Model Lines
  #Product Name
  #Reorder Point
  #Safety Stock Level
  #Size
  #Size Range
  #Standard Cost
  #Start Date
  #Status
  #Style
  #Subcategory
  #Weight
  #[1] TRUE

  #Exploring Levels
  explore(ocs, "Analysis Services Tutorial", "Product", "Product Categories")
  #(All)
  #Category
  #Subcategory
  #Product Name
  #[1] TRUE

  #Exploring Members
  #NOTE: -> indicates that the following member is a child of the previous member
  explore(ocs, "Analysis Services Tutorial", "Product", "Product Categories", "Category")
  #Accessories
  #Bikes
  #Clothing
  #Components
  #Assembly Components
  #-> Assembly Components
  #--> Assembly Components
```

