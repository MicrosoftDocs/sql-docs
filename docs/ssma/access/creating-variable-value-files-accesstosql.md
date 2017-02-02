---
title: "Creating Variable Value Files (AccessToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
ms.assetid: 808595c3-8ef1-40bd-a93e-5cf237950e08
caps.latest.revision: 15
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# Creating Variable Value Files (AccessToSQL)
Variable Value File is an XML file comprising the parameter values of commands like, the source or destination server name that frequently change from one server migration to another. When a large number of database migrations occur, multiple variable files for storing the value of each source server will be created and referenced in a master script file with the **–v** switch at command line. This helps in maintaining static values in a few script files with the variable values in multiple variable files.  
  
> [!NOTE]  
> 1.  Variable names are prefixed and suffixed with a $ (dollar) symbol. If the variables are not assigned a value in the variable value file, you will encounter an error during the parsing of the script file resulting in stalling the console execution process.  
> 2.  The escape character for **$** is **$$**. If the value of a variable or static value of a parameter contains **$** (dollar) symbol, then **$$** must be specified to treat it as a character instead of a variable.  
> 3.  For maintainability purposes, variables can be declared inside `‘variable-group’` elements for logical separation of user defined variables.  Usage of this element is not mandatory.  
  
**Examples:**  
  
**Example 1:**  
  
```xml  
<!--Sample of variable value file commands-->  
  
<variables>  
  
  <variable-group name="ProjectSpecs">  
  
    <variable name="$type$" value="MyProject"/>  
  
    <variable name="$project_folder$" value=".\$project_name$"/>  
  
    <variable name="$project_name$" value="$type$ConsoleProject"/>  
  
    <variable name="$project_overwrite$" value="true"/>  
  
    <variable name="$project_type$" value="sql-server-2008"/>  
  
  </variable-group>  
  
</variables>  
```  
**Example 2:**  
  
```xml  
<!--Sample of variable value file commands-->  
  
<variables>  
  
  <variable-group name="SQLServerParams">  
  
    <variable-group name="SqlServerConnectionParams">  
  
      <variable name="$TargetServerName$" value="xxx"/>  
  
      <variable name="$TargetDB$" value="xxx"/>  
  
      <variable name="$TargetUserName$" value="xxx"/>  
  
      <variable name="$TargetPassword$" value="xxx"/>  
  
      <variable name="$TargetIsTrusted$" value="xxx"/>  
  
      <variable name="$TrustedConnection$" value="xxx"/>  
  
    </variable-group>  
  
    <variable-group name="SqlServerObjectParams">  
  
      <variable name="$ObjectName1$" value="TestTable1"/>  
  
      <variable name="$ObjectName2$" value="TestProc1"/>  
  
    </variable-group>  
  
  </variable-group>  
  
</variables>  
```  
  
## Variable Value File Validation  
The user can easily validate his/her variable value file against the schema definition file **ConsoleScriptVariablesSchema.xsd** available in the ‘Schemas’ folder.  
  
## Next Step  
The next step in operating the console is [Creating the Server Connection Files &#40;AccessToSQL&#41;](../../ssma/access/creating-the-server-connection-files-accesstosql.md)  
  
## See Also  
[Creating the Server Connection Files (Access)](http://msdn.microsoft.com/en-us/829153be-aa8e-4162-87e8-69882feecf19)  
  
