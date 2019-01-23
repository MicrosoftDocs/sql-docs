---
title: "Add a multi-value parameter to a Report | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 12ad0e77-4c28-4bbb-ab11-473ae89ec9f1
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Add a multi-value parameter to a Report
  You can add a parameter to a report that allows the user to select more than one value for the parameter.  
  
 You can pass multiple parameter values to the report within the report URL. For a URL example includes a multi-value parameter, see [Pass a Report Parameter Within a URL](../pass-a-report-parameter-within-a-url.md).  
  
 For information on how to pass multiple parameter values to a stored procedure, see [Working With Multi-Select Parameters for SSRS Reports](https://go.microsoft.com/fwlink/?LinkId=321529) on mssqltips.com.  
  
### To add a multi-value parameter  
  
1.  In Report Builder, open the report that you want to add the multi-value parameter to.  
  
2.  Right-click the report dataset, and then click **Dataset Properties**  
  
3.  Add a variable to the dataset query by either editing the query text in the **Query** box, or by adding a filter by using the query designer. For more information, see [Build a Query in the Relational Query Designer &#40;Report Builder and SSRS&#41;](../report-data/build-a-query-in-the-relational-query-designer-report-builder-and-ssrs.md).  
  
    > [!IMPORTANT]  
    >  The query text must not include the DECLARE statement for the query variable.  
  
    > [!IMPORTANT]  
    >  The text for the query variable must include the `IN` operator, as shown in the following example.  
  
    ```  
    WHERE  
      Production.ProductInventory.ProductID IN (@ProductID)  
    ```  
  
    > [!IMPORTANT]  
    >  If you don't include the parentheses around the variable as shown above, the report fails to render and the "must declare the scalar variable" error is displayed.  
  
     A dataset parameter for an embedded dataset or a shared dataset is created automatically for the query variable. A report parameter is created automatically for the dataset parameter.  
  
4.  In the **Report Data** pane, expand the **Parameters** node, right-click the report parameter that was automatically created for the dataset parameter, and then click **Parameter Properties**.  
  
5.  In the **General** tab, select **Allow multiple values** to allow a user to select more than one value for the parameter.  
  
6.  (Optionally) In the **Available** values tab, specify a list of available values to display to the user.  
  
     An available values list limits the choices a user can make to only valid values for the parameter. For multiple values, the top of list begins with a **Select All** feature so the user can select or clear all values with a single click. If you choose to get the available values for the report parameter from a dataset query, be sure to select a dataset that does not contain the query variable that is associated with the same report parameter.  
  
     For more information, see [Add, Change, or Delete Available Values for a Report Parameter &#40;Report Builder and SSRS&#41;](add-change-or-delete-available-values-for-a-report-parameter.md).  
  
### To add a multi-value parameter  
  
1.  In Report Builder, open the report that you want to add the multi-value parameter to.  
  
2.  Right-click the report dataset, and then click **Dataset Properties**  
  
3.  Add a variable to the dataset query by either editing the query text in the **Query** box, or by adding a filter by using the query designer. For more information, see [Build a Query in the Relational Query Designer &#40;Report Builder and SSRS&#41;](../report-data/build-a-query-in-the-relational-query-designer-report-builder-and-ssrs.md).  
  
    > [!IMPORTANT]  
    >  The query text must not include the DECLARE statement for the query variable.  
  
    > [!IMPORTANT]  
    >  The text for the query variable must include the `IN` operator, as shown in the following example.  
  
    ```  
    WHERE  
      Production.ProductInventory.ProductID IN (@ProductID)  
    ```  
  
    > [!IMPORTANT]  
    >  If you don't include the parentheses around the variable as shown above, the report fails to render and the "must declare the scalar variable" error is displayed.  
  
     A dataset parameter for an embedded dataset or a shared dataset is created automatically for the query variable. A report parameter is created automatically for the dataset parameter.  
  
4.  In the **Report Data** pane, expand the **Parameters** node, right-click the report parameter that was automatically created for the dataset parameter, and then click **Parameter Properties**.  
  
5.  In the **General** tab, select **Allow multiple values** to allow a user to select more than one value for the parameter.  
  
6.  (Optionally) In the **Available** values tab, specify a list of available values to display to the user.  
  
     An available values list limits the choices a user can make to only valid values for the parameter. For multiple values, the top of list begins with a **Select All** feature so the user can select or clear all values with a single click. If you choose to get the available values for the report parameter from a dataset query, be sure to select a dataset that does not contain the query variable that is associated with the same report parameter.  
  
     For more information, see [Add, Change, or Delete Available Values for a Report Parameter &#40;Report Builder and SSRS&#41;](add-change-or-delete-available-values-for-a-report-parameter.md).  
  
## See Also  
 [Add Cascading Parameters to a Report &#40;Report Builder and SSRS&#41;](add-cascading-parameters-to-a-report-report-builder-and-ssrs.md)   
 [Add, Change, or Delete a Report Parameter &#40;Report Builder and SSRS&#41;](add-change-or-delete-a-report-parameter-report-builder-and-ssrs.md)  
  
  
