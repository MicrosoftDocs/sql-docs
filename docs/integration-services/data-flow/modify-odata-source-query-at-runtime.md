---
title: "Provide an OData Source Query at Runtime | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: bcbba7f4-6e5d-46e6-a73a-3f17d3ff376a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Provide an OData Source Query at Runtime

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


 You can modify the OData Source query at runtime by adding an *expression* to the **[OData Source].[Query]** property of the Data Flow task.  
  
 The columns returned have to be the same columns that were returned at design time; otherwise, you get an error when the package is executed. Be sure to specify the same columns (in the same order) when using the $select query option. A safer alternative to using the $select option is to deselect the columns you don't want directly from the Source Component UI.  
  
 There are a few different ways of dynamically setting the query value at runtime. Here are some of the more common methods.  
  
## Provide the query as a parameter  
 The following procedure shows how to expose the query used by an OData Source component as a parameter of the package.  
  
1.  Right click on the **Data Flow task** and select the **Parameterize...** option.  
  
2.  In the **Parameterize** dialog, select **[\<Name of the OData Source Component>].[Query]** for **Property**.  
  
3.  Choose whether to **create new parameter** or **use an existing parameter**.  
  
4.  If you select **Create new parameter**:  
  
    1.  Enter **name** and **description** for the parameter.  
  
    2.  Specify default **value** for the parameter.  
  
    3.  Specify the **scope** (**package** or **project**) for the parameter.  
  
    4.  Specify whether the parameter is **required** or not  
  
5.  Click **OK** to close the dialog box.  
  
## Provide the query with an expression
 This method is useful when you want to dynamically construct the query string at runtime.
  
1.  Select the **Data Flow Task** that contains your **OData Source**.  
  
2.  In the **Properties** window, highlight the **Expressions** property.  
  
3.  Click the ... (ellipsis) button to bring up the **Property Expressions Editor**.  
  
4.  Select the **[OData Source].[Query]** property.  
  
5.  Click the ... (ellipsis) button for **Expression**.  
  
6.  Enter the **expression**.  
  
7.  Click **OK**.  
  
> [!NOTE]  
> When you use this approach, you have to ensure that the values you set are properly URL encoded. When receiving values from user input (for example, setting individual query option values from a parameter), you must ensure that the values are validated to avoid potential SQL injection-type attacks.  
  
  
