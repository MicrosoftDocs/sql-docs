---
title: "Report and Group Variables Collections References (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10404"
  - "sql12.rtp.rptdesigner.categorygroupproperties.variables.f1"
  - "10083"
  - "sql12.rtp.rptdesigner.seriesgroupproperties.variables.f1"
  - "sql12.rtp.rptdesigner.reportproperties.variables.f1"
  - "sql12.rtp.rptdesigner.groupproperties.variables.f1"
  - "10292"
  - "10412"
ms.assetid: 4be5b463-3ce2-483d-a3c6-dae752cb543e
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Report and Group Variables Collections References (Report Builder and SSRS)
  When you have a complex calculation that is used more than once in expressions in a report, you might want to create a variable. You can create a report variable or a group variable. Variable names must be unique in a report.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Report Variables  
 Use a report variable to hold a value for time-dependent calculations, such as currency rates or time stamps, or for a complex calculation that is referenced multiple times. By default, a report variable is calculated once and can be used in expressions throughout a report. Report variables are read-only by default. You can change the default to enable a report variable as read-write. The value in a report variable is preserved throughout a session, until the report is processed again.  
  
 To add a report variable, open the **ReportProperties** dialog box, click **Variables**, and provide a name and a value. Names are case-sensitive strings that begin with a letter and have no spaces. A name can include letters, numbers, or underscores (_).  
  
 To refer to the variable in an expression, use the global collection syntax, for example, `=Variables!CustomTimeStamp.Value`. On the design surface, the value appears in a text box as `<<Expr>>`.  
  
 You can use report variables in the following ways:  
  
-   **Read-only use** Set a value once to create a constant for the report session, for example, to create a time stamp.  
  
     Because expressions in text boxes are evaluated on-demand as a user pages through a report, dynamic values (for example, an expression that includes the `Now()` function, which returns the time of day) can return different values if you page forward and backward by using the **Back** button. By setting a the value of a report variable to the expression `=Now()`, and then adding the variable to your expression, you ensure the same value is used throughout report processing.  
  
-   **Read-write use** Set a value once and serialize the value within a report session. The read-write option for variables provides a better alternative than using a static variable in the Code block in the report definition.  
  
     When you clear the **Read-Only** option for a variable, the Writable property for the variable is set to `true`. To update the value from an expression, use the SetValue method, for example, `=Variables!MyVariable.SetValue("123")`.  
  
    > [!NOTE]  
    >  You cannot control when the report processor initializes a variable or evaluates an expression that updates a variable. The order of execution for variable initialization is undefined.  
  
 For more information about sessions, see [Previewing Reports in Report Builder](../report-builder/previewing-reports-in-report-builder.md).  
  
## Group Variables  
 Use a group variable to calculate a complex expression once in the scope of a group. A group variable is valid only in the scope of the group and its child groups.  
  
 For example, suppose a data region displays inventory data for items that are in different tax categories and you want to apply different tax rates for each category. You would group the data on Category and define a *Tax* variable on the parent group. Then you would define a group variable for *ItemTax* for each tax category and assign each of the different Category subgroups to the correct group variable. For example:  
  
-   For the parent group based on `[Category]`, define the variable *Tax* with a value `[Tax]`. Assume the category values are Food and Clothing.  
  
-   For the child group based on `[Subcategory]`, define the variable *ItemsTax* as `=Variables!Tax.Value * Sum(Fields!Price.Value)`. Assume the subcategory values for the category Food are Beverages and Bread. Assume the subcategory values for Clothing are Shirts and Hats.  
  
-   For a text box in a row in the child group, add the expression `=Variables!ItemsTax.Value`.  
  
     The text box displays the total tax for Beverages and Bread using the Food tax and for Shirts and Hats using the Clothing tax.  
  
 To add a group variable, open the **Tablix Group Properties** dialog box, click **Variables**, and provide a name and a value. The group variable is calculated once per unique group value.  
  
 To refer to the variable in an expression, use the global collection syntax, for example, `=Variables!GroupDescription.Value`. On the design surface, the value appears in a text box as `<<Expr>>`.  
  
## See Also  
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](built-in-collections-in-expressions-report-builder.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)  
  
  
