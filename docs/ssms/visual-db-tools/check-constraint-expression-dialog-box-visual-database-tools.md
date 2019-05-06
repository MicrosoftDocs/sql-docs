---
title: "Check Constraint Expression Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.checkconstraintexpression"
ms.assetid: beb6ce43-3913-4d66-8826-8e885335b790
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Check Constraint Expression Dialog Box (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
When you attach a check constraint to a table or column, you must include an SQL expression. Type the check constraint expression in the box provided.  
  
## UIElement List  
Expression  
Enter the expression  
  
You can create a simple constraint expression to check data for a simple condition; or you can create a complex expression, using Boolean operators, to check data for several conditions. For example, suppose the authors table has a zip column where a 5-digit character string is required. This sample constraint expression guarantees that only 5-digit numbers are allowed:  
  
```  
zip LIKE '[0-9][0-9][0-9][0-9][0-9]'  
```  
  
Or suppose the sales table has a column called qty which requires a value greater than 0. This sample constraint guarantees that only positive values are allowed:  
  
```  
qty > 0  
```  
  
Or suppose the orders table limits the type of credit cards accepted for all credit card orders. This sample constraint guarantees that if the order is placed on a credit card, then only Visa, MasterCard, or American Express is accepted:  
  
```  
NOT (payment_method = 'credit card') OR  
   (card_type IN ('VISA', 'MASTERCARD', 'AMERICAN EXPRESS'))  
```  
  
## To define a constraint expression  
In the Check Constraints tab of the property pages, type an expression in the Constraint expression box using the following syntax:  
  
<pre>{constant | column_name | function | (subquery)}  
[{operator | AND | OR | NOT}  
{constant | column_name | function | (subquery)}...]</pre>  
  
The SQL syntax is made up of the following parameters:  
  
|Parameter|Description|  
|-------------|---------------|  
|constant|A literal value, such as numeric or character data. Character data must be enclosed within single quotation marks (').|  
|column_name|Specifies a column.|  
|function|A built-in function.|  
|operator|Arithmetic, bitwise, comparison, or string operators.|  
|AND|Use in Boolean expressions to connect two expressions. Results are returned when both expressions are true.<br /><br />When AND and OR are both used in a statement, AND is processed first. You can change the order of execution by using parentheses.|  
|OR|Use in Boolean expressions to connect two or more conditions. Results are returned when either condition is true.<br /><br />When AND and OR are both used in a statement, OR is evaluated after AND. You can change the order of execution by using parentheses.|  
|NOT|Negates any Boolean expression (which can include keywords, such as LIKE, NULL, BETWEEN, IN, and EXISTS).<br /><br />When more than one logical operator is used in a statement, NOT is processed first. You can change the order of execution by using parentheses.|  
  
## See Also  
[Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md)  
[Create Unique Constraints](../../relational-databases/tables/create-unique-constraints.md)  
  
