---
title: "Define Semiadditive Behavior | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Define Semiadditive Behavior
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Semiadditive measures, which do not uniformly aggregate across all dimensions, are very common in many business scenarios. Every cube that is based on snapshots of balances over time exhibits this problem. You can find these snapshots in applications dealing with securities, account balances, budgeting, human resources, insurance policies and claims, and many other business domains.  
  
 Add semiadditive behavior to a cube to define an aggregation method for individual measures or members of the account type attribute. If the cube contains an account dimension, you can automatically set semiadditive behavior based on the account type.  
  
 To add semiadditive behavior, open a cube in Cube Designer and choose **Add Business Intelligence** from the Cube menu. In the Business Intelligence Wizard, select the **Define semiadditive behavior** option on the **Choose Enhancement** page. This wizard then guides you through the steps of identifying which measures have semiadditive behavior.  
  
 With the exception of LastChild which is available in the standard edition, semi-additive behaviors are only available in the business intelligence or enterprise editions.  
  
## Define Semiadditive Behavior  
 On the **Define Semiadditive Behavior** page of the wizard, you select how to define semiadditivity by selecting one of the following options:  
  
 **Turn off semiadditive behavior**  
 Removes semiadditive behavior from a cube in which semiadditive behavior was previously defined. This selection resets a measure to **SUM** if it is set to any of the following aggregation function types:  
  
-   By Account  
  
-   Average of Children  
  
-   First Child  
  
-   Last Child  
  
-   Last Nonempty Child  
  
-   First Nonempty Child  
  
-   None  
  
 This option does not change measures with a regular aggregation function: **Sum**, **Min**, **Max**, **Count**, or **Distinct****Count**.  
  
 **The wizard has detected the 'Account" account dimension, which contains semiadditive members. The server will aggregate members of this dimension according to the semiadditive behavior specified for each account type.**  
 Causes the system to set all measures from a measure group dimensioned by an Account type dimension to the By Account aggregation function and the server will aggregate members of the dimension according to the semiadditive behavior specified for each account type.  
  
> [!NOTE]  
>  This option is selected by default if the wizard detects an Account type dimension.  
  
 **Define semiadditive behavior for individual measures**  
 Selects the semiadditive behavior of each measure individually. The default setting is **SUM** (fully additive).  
  
> [!NOTE]  
>  This option is selected by default if the wizard does not detect an Account type dimension.  
  
 For each measure, you can select from the types of semiadditive functionality described in the following table.  
  
|Semiadditive function|Description|  
|---------------------------|-----------------|  
|Average of Children|The aggregation of a member is the average of its children.|  
|ByAccount|The system reads the semiadditive behavior specified for the account type.|  
|Count|The aggregation is a count of members.|  
|Distinct Count|The aggregation is a count of unique members.|  
|First Child|The member value is evaluated as the value of its first child along the time dimension.|  
|FirstNonEmpty|The member value is evaluated as the value of its first child along the time dimension that contains data.|  
|LastChild|The member value is evaluated as the value of its last child along the time dimension.|  
|LastNonEmpty|The member value is evaluated as the value of its last child along the time dimension that contains data.|  
|Max|The standard maximum aggregation function is applied.|  
|Min|The standard minimum aggregation function is applied.|  
|None|No aggregation is applied.|  
|Sum|The standard summation function is applied.|  
  
 Any existing semiadditive behavior is overwritten when you complete the wizard.  
  
  
