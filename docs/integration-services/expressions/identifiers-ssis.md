---
title: "Identifiers (SSIS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "regular identifiers [Integration Services]"
  - "variables [Integration Services], expressions"
  - "identifiers [Integration Services]"
  - "expressions [Integration Services], variables"
  - "lineage identifiers"
  - "columns [Integration Services], identifiers"
  - "names [Integration Services]"
  - "expressions [Integration Services], identifiers"
  - "qualified identifiers [Integration Services]"
ms.assetid: 56af984d-88b4-4db8-b6a2-6b07315a699e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Identifiers (SSIS)
  In expressions, identifiers are columns and variables that are available to the operation. Expressions can use regular and qualified identifiers.  
  
## Regular Identifiers  
 A regular identifier is an identifier that needs no additional qualifiers. For example, **MiddleName**, a column in the **Contacts** table of the **AdventureWorks** database, is a regular identifier.  
  
 The naming of regular identifiers must follow these rules:  
  
-   The first character of the name must be a letter as defined by the Unicode Standard 2.0, or an underscore (_).  
  
-   Subsequent characters can be letters or numbers as defined in the Unicode Standard 2.0, the underscore (_), \@, $, and # characters.  
  
> [!IMPORTANT]  
>  Embedded spaces and special characters, other than the ones listed, are not valid in regular identifiers. In order to use spaces and special characters, you must use a qualified identifier instead of a regular identifier.  
  
## Qualified Identifiers  
 A qualified identifier is an identifier that is delimited by brackets. An identifier may require a delimiter because the name of the identifier includes spaces, or because the identifier name does not begin with either a letter or the underscore character. For example, the column name **Middle Name** must be qualified by brackets and written as [Middle Name] in an expression.  
  
 If the name of the identifier includes spaces, or if the name is not a valid regular identifier name, the identifier must be qualified. The expression evaluator uses the opening and closing ([]) brackets to qualify identifiers. The brackets are put in the first and the last position of the string. For example, the identifier 5$> becomes [5$>]. Brackets can be used with column names, variable names, and function names.  
  
 If you build expressions using the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer dialog boxes, regular identifiers are automatically enclosed in brackets. However, brackets are required only if the name include invalid characters. For example, the column named **MiddleName** is valid without brackets.  
  
 You cannot reference column names that include brackets in expressions. For example, the column name **Column[1]** cannot be used in an expression. To use the column in an expression it must be renamed to a name without brackets.  
  
## Lineage Identifiers  
 Expressions can use lineage identifiers to refer to columns. The lineage identifiers are assigned automatically when you first create the package. You can view the lineage identifier for a column on the **Column Properties** tab of the **Advanced Editor** dialog box in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 If you refer to a column using its lineage identifier, the identifier must include the pound (#) character prefix. For example, a column with the lineage identifier 147 must be referenced as #147.  
  
 If an expression parses successfully, the expression evaluator replaces the lineage identifiers with column names in the dialog box.  
  
## Unique Column Names  
 Multiple components used in a package can expose columns with the same name. If these columns are used in expressions, they must be disambiguated before the expressions can be parsed successfully. The expression evaluator supports the dotted notation for identifying the source of the column. For example, two columns named **Age** become **FlatFileSource.Age** and **OLEDBSource.Age**, which indicates that their sources are FlatFileSource or OLEDBSource. The parser treats the fully qualified name as a single column name.  
  
 Source component names and column names are qualified separately. The following examples show valid use of brackets in a dotted notation:  
  
-   The source component name includes spaces.  
  
    ```  
    [MySo urce].Age  
    ```  
  
-   The first character in the column name is not valid.  
  
    ```  
    MySource.[#Age]  
    ```  
  
-   The source component and the column names contain invalid characters.  
  
    ```  
    [MySource?].[#Age]  
    ```  
  
> [!IMPORTANT]  
>  If both elements in dotted notation are enclosed in one pair of brackets, the expression evaluator interprets the pair as a single identifier, not a source-column combination.  
  
## Variables in Expressions  
 Variables, when referenced in expressions, must include the \@ prefix. For example, the **Counter** variable is referenced by using \@Counter. The \@ character is not part of the variable name; it only identifies the variable to the expression evaluator. If you build expressions by using the dialog boxes that [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides, the \@ character is automatically added to the variable name. It is not valid to include spaces between the \@ character and the variable name.  
  
 Variable names follow the same rules as those for other regular identifiers:  
  
-   The first character of the name must be a letter as defined by the Unicode Standard 2.0, or an underscore (_).  
  
-   Subsequent characters can be letters or numbers as defined in the Unicode Standard 2.0, the underscore (_), \@, $, and # characters.  
  
 If a variable name contains characters other than those listed, the variable must be enclosed in brackets. For example, variable names with spaces must be enclosed in brackets. The opening bracket follows the \@ character. For example, the **My Name** variable is referenced as \@[My Name]. It is not valid to include spaces between the variable name and the brackets.  
  
> [!NOTE]  
>  The names of user-defined and system variables are case-sensitive.  
  
## Unique Variable Names  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports custom variables and provides a set of system variables. By default, custom variables belong to the **User** namespace, and system variables belong to the **System** namespace. You can create additional namespaces for custom variables and update the namespace names to suit the needs of your application. The expression builder lists in-scope variables in all namespaces.  
  
 All variables have scope and belong to a namespace. A variable has package scope or the scope of a container or task in the package. The expression builder in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer lists only the in-scope variables. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
 Variables used in expressions must have unique names for the expression evaluator to evaluate the expression correctly. If a package uses multiple variables with the same name, their namespaces must be different. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides a namespace resolution operator, consisting of two colons (::), for qualifying a variable with its namespace. For example, the following expression uses two variables named **Count**; one belongs to the **User** namespace and one to the **MyNamespace** namespace.  
  
```  
@[User::Count] > @[MyNamespace::Count]  
```  
  
> [!IMPORTANT]  
>  You must enclose the combination of namespace and qualified variable name in brackets for the expression evaluator to recognize the variable.  
  
 If the value of **Count** in the **User** namespace is 10 and the value of **Count** in **MyNamespace** is 2, the expression evaluates to **true** because the expression evaluator recognizes two different variables.  
  
 If variable names are not unique, no error occurs. Instead, the expression evaluator uses only one instance of the variable to evaluate the expression and returns an incorrect result. For example, the following expression was intended to compare the values (10 and 2) for two separate **Count** variables but the expression evaluates to **false** because the expression evaluator uses the same instance of the **Count** variable two times.  
  
```  
@Count > @Count  
```  
  
## Related Content  
 Technical article, [SSIS Expression Cheat Sheet](https://go.microsoft.com/fwlink/?LinkId=746575), on pragmaticworks.com  
  
  
