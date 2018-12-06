---
title: "Using Cube Writebacks (MDX) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Data Modification - Using Cube Writebacks
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  You update a cube by using the [UPDATE CUBE](../../../mdx/mdx-data-manipulation-update-cube.md) statement. This statement lets you update a tuple with a specific value. To effectively use the UPDATE CUBE statement to update a cube, you have to understand the syntax for the statement, the error conditions that can occur, and the affect that updates can have on a cube.  
  
## UPDATE CUBE Statement Syntax  
 The following syntax describes the UPDATE CUBE statement:  
  
```  
UPDATE [CUBE] <Cube_Name> SET <tuple>.VALUE = <value> [,<tuple>.VALUE = <value>...]  
 [ USE_EQUAL_ALLOCATION | USE_EQUAL_INCREMENT |  
  USE_WEIGHTED_ALLOCATION [BY <weight value_expression>] |  
  USE_WEIGHTED_INCREMENT [BY <weight value_expression>] ]   
```  
  
 If a full set of coordinates is not specified for the tuple, the unspecified coordinates will use the default member of the hierarchy. The tuple identified must reference a cell that is aggregated with the [Sum](../../../mdx/sum-mdx.md) function, and must not use a calculated member as one of the cell's coordinates.  
  
 You can think of the UPDATE CUBE statement as a subroutine that generates a series of individual writeback operations to atomic cells. All these individual writeback operations then roll up into the specified sum.  
  
> [!NOTE]  
>  When updated cells do not overlap, the **Update Isolation Level** connection string property can be used to enhance performance for UPDATE CUBE. For more information, see <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A>.  
  
## Example  
 You can test UPDATE CUBE using the Sales Targets measure group in the Adventure Works cube. This measure group consists of measures aggregated by SUM, which is a requirement for UPDATE CUBE.  
  
1.  Enable writeback for the Sales Targets measure group in the Adventure Works database. In Management Studio, right-click a measure group, point to **Write Back Options**, choose **Enable Writeback**.  
  
     You should see a new writeback table in the Writeback folder. The table name is WriteTable_Fact Sales Quota.  
  
2.  Open an MDX query window. Run the following select statement to view the original value:  
  
    ```  
    SELECT [Measures].[Sales Amount Quota] on 0 ,  
    [Employee].[Employee Department].[Title].&[Sales Representative].children on 1  
    FROM [Adventure Works]  
  
    ```  
  
     You should see sales amount quotas for each representative.  
  
3.  Run the update cube statement to write back a new value. In this example, we are setting the sales amount quota to 0. Because the new value is 0, do not specify an allocation method.  
  
    ```  
    UPDATE CUBE [Adventure Works]   
    SET ([Measures].[Sales Amount Quota], [Employee].[Employee Department].[Department].&[Sales]) = 0  
  
    ```  
  
4.  Re-run the SELECT statement. You should now see zero for the quotas.  
  
 The writeback value is constrained to the current session. To persist the value across users and sessions, process the writeback table. In Management Studio, right-click WriteTable_Fact Sales Quota and choose **Process**.  
  
 To specify an allocation method, the new value must be greater than zero. In this example, the new value for Sales Amount Quota is two million and the allocation method distributes the amount across all sales representatives.  
  
```  
UPDATE CUBE [Adventure Works]   
SET ([Measures].[Sales Amount Quota], [Employee].[Employee Department].[Department].&[Sales]) = 2000000   
USE_EQUAL_ALLOCATION  
```  
  
## Error Conditions  
 The following table describes both what can cause writebacks to fail and the result of those errors.  
  
|Error Condition|Result|  
|---------------------|------------|  
|Update includes members from the same dimension that do not exist with one another.|Update will fail. The cube space will not contain the referenced cell.|  
|Update includes a measure sourced to a measure of an unsigned type.|Update will fail. Increments require that the measure be able to take a negative value.|  
|Update includes a measure that aggregates other than sum.|An error is raised.|  
|Update was tried on a subcube.|An error is raised.|  
  
## Affect of Cube Changes  
 The following changes will not have an effect on a writeback:  
  
-   Processing of a cube, the cube's measure groups, or the cube's dimensions.  
  
-   Adding attributes to any dimension.  
  
-   Adding a new dimension.  
  
-   Deleting a dimension that does not include the writeback.  
  
-   Adding, modifying, or removing a hierarchy.  
  
-   Adding a new measure.  
  
 The following changes cannot be made without removing the writeback data:  
  
-   Deleting an attribute, or its attribute hierarchy, if the attribute is included in the writeback. This includes explicitly removing the attribute, or its attribute hierarchy, or removing the attribute's parent dimension.  
  
-   Deleting a measure included in the writeback.  
  
-   Adding an attribute without an **(All)** level to a dimension included in the writeback.  
  
-   Changing the dimension granularity for a dimension included in the writeback.  
  
## See Also  
 [Modifying Data &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-data-modification-modifying-data.md)  
  
  
