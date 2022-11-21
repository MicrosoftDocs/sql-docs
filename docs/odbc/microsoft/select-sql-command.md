---
description: "SELECT - SQL Command"
title: "SELECT - SQL Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "select [ODBC]"
ms.assetid: 2149c3ca-3a71-446d-8d53-3d056e2f301a
author: David-Engel
ms.author: v-davidengel
---
# SELECT - SQL Command
Retrieves data from one or more tables.  
  
 The Visual FoxPro ODBC Driver supports the native Visual FoxPro language syntax for this command. For driver-specific information, see **Driver Remarks**.  
  
## Syntax  
  
```  
  
SELECT [ALL | DISTINCT]  
   [Alias.] Select_Item [AS Column_Name]  
   [, [Alias.] Select_Item [AS Column_Name] ...]   
FROM [DatabaseName!]Table [Local_Alias]  
   [, [DatabaseName!]Table [Local_Alias] ...]   
[WHERE JoinCondition [AND JoinCondition  
...]  
   [AND | OR FilterCondition [AND | OR FilterCondition ...]]]  
[GROUP BY GroupColumn [, GroupColumn ...]]  
[HAVING FilterCondition]  
[UNION [ALL] SELECTCommand]  
[ORDER BY Order_Item [ASC | DESC] [, Order_Item [ASC | DESC] ...]]  
```  
  
## Arguments  
  
> [!NOTE]  
>  A *subquery*, referred to in the following arguments, is a SELECT within a SELECT and must be enclosed in parentheses. You can have up to two subqueries at the same level (not nested) in the WHERE clause. (See that section of the arguments.) Subqueries can contain multiple join conditions.  
  
 [ALL &#124; DISTINCT]   [*Alias*.] *Select_Item* [AS *Column_Name*]    [, [*Alias*.] *Select_Item* [AS *Column_Name*] ...]  
 The SELECT clause specifies the fields, constants, and expressions that are displayed in the query results.  
  
 By default, ALL displays all the rows in the query results.  
  
 DISTINCT excludes duplicates of any rows from the query results.  
  
> [!NOTE]  
>  You can use DISTINCT only once per SELECT clause.  
  
 *Alias*. qualifies matching item names. Each item you specify with *Select_Item* generates one column of the query results. If two or more items have the same name, include the table alias and a period before the item name to prevent columns from being duplicated.  
  
 *Select_Item* specifies an item to be included in the query results. An item can be one of the following:  
  
-   The name of a field from a table in the FROM clause.  
  
-   A constant specifying that the same constant value is to appear in every row of the query results.  
  
-   An expression that can be the name of a user-defined function.  
  
 **User-Defined Functions with SELECT**  
  
 Although using user-defined functions in the SELECT clause has obvious benefits, you should also consider the following restrictions:  
  
-   The speed of operations performed with SELECT might be limited by the speed at which such user-defined functions are executed. High-volume manipulations involving user-defined functions might be better accomplished by using API and user-defined functions written in C or assembly language.  
  
-   The only reliable way to pass values to user-defined functions invoked from SELECT is by the argument list passed to the function when it is invoked.  
  
-   Even if you experiment and discover a supposedly forbidden manipulation that works correctly in a certain version of FoxPro, there is no guarantee it will continue to work in later versions.  
  
 Apart from these restrictions, user-defined functions are acceptable in the SELECT clause. However, remember that using SELECT might slow performance.  
  
 The following field functions are available for use with a select item that is a field or an expression involving a field:  
  
-   AVG(*Select_Item*)-Averages a column of numeric data.  
  
-   COUNT(*Select_Item*)-Counts the number of select items in a column. COUNT(*) counts the number of rows in the query output.  
  
-   MIN(*Select_Item*)-Determines the smallest value of *Select_Item* in a column.  
  
-   MAX(*Select_Item*)-Determines the largest value of *Select_Item* in a column.  
  
-   SUM(*Select_Item*)-Totals a column of numeric data.  
  
 You cannot nest field functions.  
  
 AS *Column_Name*  
 Specifies the heading for a column in the query output. This is useful when *Select_Item* is an expression or contains a field function and you want to give the column a meaningful name. *Column_Name* can be an expression but cannot contain characters (for example, spaces) that are not permitted in table field names.  
  
 FROM [*DatabaseName*!]*Table* [*Local_Alias*]   [, [*DatabaseName*!]*Table* [*Local_Alias*] ...]  
 Lists the tables that contain the data that the query retrieves. If no table is open, Visual FoxPro displays the **Open** dialog box so that you can specify the file location. After it has been opened, the table remains open after the query is complete.  
  
 *DatabaseName*! specifies the name of a database other than the one specified with the data source. You must include the name of the database that contains the table if the database is not specified with the data source. Include the exclamation point (!) delimiter after the database name and before the table name.  
  
 *Local_Alias* specifies a temporary name for the table named in *Table*. If you specify a local alias, you must use the local alias instead of the table name throughout the SELECT statement. The local alias does not affect the Visual FoxPro environment.  
  
 WHERE *JoinCondition* [AND *JoinCondition* ...]    [AND &#124; OR *FilterCondition* [AND &#124; OR *FilterCondition* ...]]  
 Tells Visual FoxPro to include only certain records in the query results. WHERE is required to retrieve data from multiple tables.  
  
 *JoinCondition* specifies fields that link the tables in the FROM clause. If you include more than one table in a query, you should specify a join condition for every table after the first.  
  
> [!IMPORTANT]  
>  Consider the following information when you create join conditions:  
  
-   If you include two tables in a query and do not specify a join condition, every record in the first table is joined to every record in the second table as long as the filter conditions are met. Such a query can produce lengthy results.  
  
-   Use caution when joining tables with empty fields because Visual FoxPro matches empty fields. For example, if you join on CUSTOMER.ZIP and INVOICE.ZIP and if CUSTOMER contains 100 empty zip codes and INVOICE contains 400 empty zip codes, the query output contains 40,000 extra records resulting from the empty fields. Use the **EMPTY( )** function to eliminate empty records from the query output.  
  
-   You must use the AND operator to connect multiple join conditions. Each join condition has the following form:  
  
     *FieldName1 Comparison FieldName2*  
  
     *FieldName1* is the name of a field from one table, *FieldName2* is the name of a field from another table, and *Comparison* is one of the operators described in the following table.  
  
|Operator|Comparison|  
|--------------|----------------|  
|=|Equal|  
|==|Exactly equal|  
|LIKE|SQL LIKE|  
|<>, !=, #|Not equal|  
|>|More than|  
|>=|More than or equal to|  
|<|Less than|  
|<=|Less than or equal to|  
  
 When you use the = operator with strings, it acts differently, depending on the setting of SET ANSI. When SET ANSI is set to OFF, Visual FoxPro treats string comparisons in a manner familiar to Xbase users. When SET ANSI is set to ON, Visual FoxPro follows ANSI standards for string comparisons. See [SET ANSI](../../odbc/microsoft/set-ansi-command.md) and [SET EXACT](../../odbc/microsoft/set-exact-command.md) for more information about how Visual FoxPro performs string comparisons.  
  
 *FilterCondition* specifies the criteria that records must meet to be included in the query results. You can include as many filter conditions in a query as you want, connecting them with the AND or OR operator. You can also use the NOT operator to reverse the value of a logical expression, or you can use **EMPTY( )** to check for an empty field. *FilterCondition* can take any of the forms in the following examples:  
  
 **Example 1** *FieldName1 Comparison FieldName2*  
  
 `customer.cust_id = orders.cust_id`  
  
 **Example 2** *FieldName Comparison Expression*  
  
 `payments.amount >= 1000`  
  
 **Example 3** *FieldName Comparison* ALL (*Subquery*)  
  
 `company < ALL ;`  
  
 `(SELECT company FROM customer WHERE country = "USA")`  
  
 When the filter condition includes ALL, the field must meet the comparison condition for all values generated by the subquery before its record is included in the query results.  
  
 **Example 4** *FieldName Comparison* ANY &#124; SOME (*Subquery*)  
  
 `company < ANY ;`  
  
 `(SELECT company FROM customer WHERE country = "USA")`  
  
 When the filter condition includes ANY or SOME, the field must meet the comparison condition for at least one of the values generated by the subquery.  
  
 The following example checks to see whether the values in the field are within a specified range of values:  
  
 **Example 5** *FieldName* [NOT] BETWEEN *Start_Range* AND *End_Range*  
  
 `customer.postalcode BETWEEN 90000 AND 99999`  
  
 The following example checks to see whether at least one row meets the criteria in the subquery. When the filter condition includes EXISTS, the filter condition evaluates to True (.T.) unless the subquery evaluates to the empty set.  
  
 **Example 6** [NOT] EXISTS (*Subquery*)  
  
 `EXISTS ;`  
  
 `(SELECT * FROM orders WHERE customer.postalcode =`  
  
 `orders.postalcode)`  
  
 **Example 7** *FieldName* [NOT] IN *Value_Set*  
  
 `customer.postalcode NOT IN ("98052","98072","98034")`  
  
 When the filter condition includes IN, the field must contain one of the values before its record is included in the query results.  
  
 **Example 8** *FieldName* [NOT] IN (*Subquery*)  
  
 `customer.cust_id IN ;`  
  
 `(SELECT orders.cust_id FROM orders WHERE orders.city="Seattle")`  
  
 Here the field must contain one of the values returned by the subquery before its record is included in the query results.  
  
 **Example 9** *FieldName* [NOT] LIKE *cExpression*  
  
 `customer.country NOT LIKE "USA"`  
  
 This filter condition searches for each field that matches *cExpression*. You can use the percent sign (%) and underscore ( _ ) wildcard characters as part of *cExpression*. The underscore represents a single unknown character in the string.  
  
 GROUP BY *GroupColumn* [, *GroupColumn* ...]  
 Groups rows in the query based on values in one or more columns. *GroupColumn* can be one of the following:  
  
-   The name of a regular table field.  
  
-   A field that includes an SQL field function.  
  
-   A numeric expression that indicates the location of the column in the result table. (The leftmost column number is 1.)  
  
 HAVING *FilterCondition*  
 Specifies a filter condition that groups must meet to be included in the query results. HAVING should be used with GROUP BY and can include as many filter conditions as you want, connected by the AND or OR operator. You can also use NOT to reverse the value of a logical expression.  
  
 *FilterCondition* cannot contain a subquery.  
  
 A HAVING clause without a GROUP BY clause behaves like a WHERE clause. You can use local aliases and field functions in the HAVING clause. Use a WHERE clause for faster performance if your HAVING clause contains no field functions.  
  
 [UNION [ALL] *SELECTCommand*]  
 Combines the final results of one SELECT with the final results of another SELECT. By default, UNION checks the combined results and eliminates duplicate rows. Use parentheses to combine multiple UNION clauses.  
  
 ALL prevents UNION from eliminating duplicate rows from the combined results.  
  
 UNION clauses follow these rules:  
  
-   You cannot use UNION to combine subqueries.  
  
-   Both SELECT commands must have the same number of columns in their query output.  
  
-   Each column in the query results of one SELECT must have the same data type and width as the corresponding column in the other SELECT.  
  
-   Only the final SELECT can have an ORDER BY clause, which must refer to output columns by number. If an ORDER BY clause is included, it affects the complete result.  
  
 You can also use the UNION clause to simulate an outer join.  
  
 When you join two tables in a query, only records with matching values in the joining fields are included in the output. If a record in the parent table does not have a corresponding record in the child table, the record in the parent table is not included in the output. An outer join lets you include all the records in the parent table in the output, together with the matching records in the child table. To create an outer join in Visual FoxPro, you must use a nested SELECT command, as in the following example:  
  
```  
SELECT customer.company, orders.order_id, orders.emp_id ;  
FROM customer, orders ;  
WHERE customer.cust_id = orders.cust_id ;  
UNION ;  
SELECT customer.company, 0, 0 ;  
FROM customer ;  
WHERE customer.cust_id NOT IN ;  
(SELECT orders.cust_id FROM orders)  
```  
  
> [!NOTE]  
>  Make sure that you include the space that immediately precedes each semicolon. Otherwise, you will receive an error.  
  
 The section of the command before the UNION clause selects records from both tables that have matching values. The customer companies that do not have associated invoices are not included. The section of the command after the UNION clause selects records in the customer table that do not have matching records in the orders table.  
  
 Regarding the second section of the command, note the following:  
  
-   The SELECT statement within the parentheses is processed first. This statement creates a selection of all customer numbers in the orders table.  
  
-   The WHERE clause finds all customer numbers in the customer table that are not in the orders table. Because the first section of the command provided all companies that had a customer number in the orders table, all companies in the customer table are now included in the query results.  
  
-   Because the structures of tables included in a UNION must be identical, there are two placeholders in the second SELECT statement to represent *orders.order_id* and *orders.emp_id* from the first SELECT statement.  
  
    > [!NOTE]  
    >  The placeholders must be the same type as the fields that they represent. If the field is a date type, the placeholder should be { / / }. If the field is a character field, the placeholder should be the empty string ("").  
  
 ORDER BY *Order_Item* [ASC &#124; DESC] [, *Order_Item* [ASC &#124; DESC] ...]  
 Sorts the query results based on the data in one or more columns. Each *Order_Item* must correspond to a column in the query results and can be one of the following:  
  
-   A field in a FROM table that is also a select item in the main SELECT clause (not in a subquery).  
  
-   A numeric expression that indicates the location of the column in the result table. (The leftmost column is number 1.)  
  
 ASC specifies an ascending order for query results, according to the order item or items, and is the default for ORDER BY.  
  
 DESC specifies a descending order for query results.  
  
 Query results appear unordered if you do not specify an order with ORDER BY.  
  
## Remarks  
 SELECT is an SQL command that is built into Visual FoxPro like any other Visual FoxPro command. When you use SELECT to pose a query, Visual FoxPro interprets the query and retrieves the specified data from the tables. You can create a SELECT query from within either the Command Prompt window or a Visual FoxPro program (as with any other Visual FoxPro command).  
  
> [!NOTE]  
>  SELECT does not respect the current filter condition specified with SET FILTER.  
  
## Driver Remarks  
 When your application sends the ODBC SQL statement SELECT to the data source, the Visual FoxPro ODBC Driver converts the command into the Visual FoxPro SELECT command without translation unless the command contains an ODBC escape sequence. Items enclosed in an ODBC escape sequence are converted to Visual FoxPro syntax. For more information about using ODBC escape sequences, see [Time and Date Functions](../../odbc/microsoft/time-and-date-functions-visual-foxpro-odbc-driver.md) and in the *Microsoft ODBC Programmer's Reference*, see [Escape Sequences in ODBC](../../odbc/reference/develop-app/escape-sequences-in-odbc.md).  
  
## See Also  
 [CREATE TABLE - SQL](../../odbc/microsoft/create-table-sql-command.md)   
 [INSERT - SQL](../../odbc/microsoft/insert-sql-command.md)   
 [SET ANSI](../../odbc/microsoft/set-ansi-command.md)   
 [SET EXACT](../../odbc/microsoft/set-exact-command.md)
