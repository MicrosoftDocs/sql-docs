---
title: "Examples of Advanced Integration Services Expressions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "functions [Integration Services]"
  - "operators [Integration Services]"
  - "expressions [Integration Services], examples"
  - "examples [Integration Services]"
ms.assetid: c7794ba3-0de5-466b-ae8a-9ddd27360049
author: janinezhang
ms.author: janinez
manager: craigg
---
# Examples of Advanced Integration Services Expressions
  This section provides examples of advanced expressions that combine multiple operators and functions. If an expression is used in a precedence constraint or the Conditional Split transformation, it must evaluate to a Boolean. That restriction, however, does not apply to expressions used in property expressions, variables, the Derived Column transformation, or the For Loop container.  
  
 The following examples use the **AdventureWorks** and the [!INCLUDE[ssSampleDBDWobject](../../includes/sssampledbdwobject-md.md)][!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases. Each example identifies the tables it uses.  
  
## Boolean Expressions  
  
-   This example uses the **Product** table. The expression evaluates the month entry in the **SellStartDate** column and returns TRUE if the month is June or later.  
  
    ```  
    DATEPART("mm",SellStartDate) > 6  
    ```  
  
-   This example uses the **Product** table. The expression evaluates the rounded result of dividing the **ListPrice** column by the **StandardCost** column, and returns TRUE if the result is greater than 1.5.  
  
    ```  
    ROUND(ListPrice / StandardCost,2) > 1.50  
    ```  
  
-   This example uses the **Product** table. The expression returns TRUE if all three operations evaluate to TRUE. If the **Size** column and the **BikeSize** variable have incompatible data types, the expression requires an explicit cast as shown the second example. The cast to DT_WSTR includes the length of the string.  
  
    ```  
    MakeFlag ==  TRUE && FinishedGoodsFlag == TRUE && Size != @BikeSize  
    MakeFlag ==  TRUE && FinishedGoodsFlag == TRUE  && Size != (DT_WSTR,10)@BikeSize  
    ```  
  
-   This example uses the **CurrencyRate** table. The expression compares values in tables and variables. It returns TRUE if entries in the **FromCurrencyCode** or **ToCurrencyCode** columns are equal to variable values and if the value in **AverageRate** is greater that the value in **EndOfDayRate**.  
  
    ```  
    (FromCurrencyCode == @FromCur || ToCurrencyCode == @ToCur) && AverageRate > EndOfDayRate  
    ```  
  
-   This example uses the **Currency** table. The expression returns TRUE if the first character in the **Name** column is not a or A.  
  
    ```  
    SUBSTRING(UPPER(Name),1,1) != "A"  
    ```  
  
     The following expression provides the same results, but it is more efficient because only one character is converted to uppercase.  
  
    ```  
    UPPER(SUBSTRING(Name,1,1)) != "A"  
    ```  
  
## Non-Boolean Expressions  
 Non-Boolean expressions are used in the Derived Column transformation, property expressions, and the For Loop container.  
  
-   This example uses the **Contact** table. The expression removes leading and trailing spaces from the **FirstName**, **MiddleName**, and **LastName** columns. It extracts the first letter of the **MiddleName** column if it is not null, concatenates the middle initial and the values in **FirstName** and **LastName**, and inserts appropriate spaces between values.  
  
    ```  
    TRIM(FirstName) + " " + (!ISNULL(MiddleName) ? SUBSTRING(MiddleName,1,1) + " " : "") + TRIM(LastName)  
    ```  
  
-   This example uses the **Contact** table. The expression validates entries in the **Salutation** column. It returns a **Salutation** entry or an empty string.  
  
    ```  
    (Salutation == "Sr." || Salutation == "Ms." || Salutation == "Sra." || Salutation == "Mr.") ? Salutation : ""  
    ```  
  
-   This example uses the **Product** table. The expression converts the first character in the **Color** column to uppercase and converts remaining characters to lowercase.  
  
    ```  
    UPPER(SUBSTRING(Color,1,1)) + LOWER(SUBSTRING(Color,2,15))  
    ```  
  
-   This example uses the **Product** table. The expression calculates the number of months a product has been sold and returns the string "Unknown" if either the **SellStartDate** or the **SellEndDate** column contains NULL.  
  
    ```  
    !(ISNULL(SellStartDate)) && !(ISNULL(SellEndDate)) ? (DT_WSTR,2)DATEDIFF("mm",SellStartDate,SellEndDate) : "Unknown"  
    ```  
  
-   This example uses the **Product** table. The expression calculates the markup on the **StandardCost** column and rounds the result to a precision of two. The result is presented as a percentage.  
  
    ```  
    ROUND(ListPrice / StandardCost,2) * 100  
    ```  
  
## Related Tasks  
 [Use an Expression in a Data Flow Component](https://msdn.microsoft.com/library/9181b998-d24a-41fb-bb3c-14eee34f910d)  
  
## Related Content  
 Technical article, [SSIS Expression Cheat Sheet](https://go.microsoft.com/fwlink/?LinkId=746575), on pragmaticworks.com  
  
  
