---
title: "Expression Builder | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.expressionbuilder.f1"
helpviewer_keywords: 
  - "Expression Builder dialog box"
ms.assetid: 4717ce33-bd4e-44bc-81e0-002de075b4d1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Expression Builder
  Use the **Expression Builder** dialog box to create and edit a property expression or write the expression that sets the value of a variable using a graphical user interface that lists variables and provides a built-in reference to the functions, type casts, and operators that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expression language includes.  
  
 A property expression is an expression that is assigned to a property. When the expression is evaluated, the property is dynamically updated to use the evaluation result of the expression. Likewise, an expression that is used in a variable enables the variable value to be updated with the evaluation result of the expression.  
  
 There are many ways to use expressions:  
  
-   **Tasks** Update the To line that a Send Mail task uses by inserting an e-mail address that is stored in a variable; or update the Subject line by concatenating a string such as "Sales for: " and the current date returned by the GETDATE function.  
  
-   **Variables** Set the value of a variable to the current month by using an expression like `DATEPART("mm",GETDATE())`; or set the value of a string by concatenating the string literal and the current date by using the expression `"Today's date is " + (DT_WSTR,30)(GETDATE())`.  
  
-   **Connection Managers** Set the code page of a Flat File connection manager by using a variable that contains a different code page identifier; or specify the number of rows in the data file to skip by entering a positive integer like 3 in the expression.  
  
 To learn more about property expressions and writing expressions, see [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md) and [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md).  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Variables**|Expand the **Variables** folder and drag variables to the **Expression** box.|  
|**Mathematical Functions**<br /><br /> **String Functions**<br /><br /> **Date/Time Functions**<br /><br /> **NULL Functions**<br /><br /> **Type Casts**<br /><br /> **Operators**|Expand the folders and drag functions, type casts, and operators to the **Expression** box.|  
|**Expression**|Edit or type an expression.`|  
|**Evaluated value**|Lists the evaluation result of the expression.|  
|**Evaluate Expression**|Click **Evaluate Expression** to view the evaluation result of the expression.|  
  
## See Also  
 [Expressions Page](../../integration-services/expressions/expressions-page.md)   
 [Property Expressions Editor](../../integration-services/expressions/property-expressions-editor.md)   
 [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md)   
 [System Variables](../../integration-services/system-variables.md)  
  
  
