---
title: "Converting Oracle Schemas (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Conversion Results"
ms.assetid: e021182d-31da-443d-b110-937f5db27272
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Converting Oracle Schemas (OracleToSQL)
After you have connected to Oracle, connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and set project and data mapping options, you can convert Oracle database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects.  
  
## The Conversion Process  
Converting database objects takes the object definitions from Oracle, converts them to similar [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects, and then loads this information into the SSMA metadata. It does not load the information into the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can then view the objects and their properties by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer.  
  
During the conversion, SSMA prints output messages to the Output pane and error messages to the Error List pane. Use the output and error information to determine whether you have to modify your Oracle databases or your conversion process to obtain the desired conversion results.  
  
## Setting Conversion Options  
Before converting objects, review the project conversion options in the **Project Settings** dialog box. By using this dialog box, you can set how SSMA converts functions and global variables. For more information, see [Project Settings &#40;Conversion&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-conversion-oracletosql.md).  
  
## Conversion Results  
The following table shows which Oracle objects are converted, and the resulting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects:  
  
|||  
|-|-|  
|Oracle Objects|Resulting SQL Server Objects|  
|Functions|If the function can be directly converted to [!INCLUDE[tsql](../../includes/tsql-md.md)], SSMA creates a function.<br /><br />In some cases, the function must be converted to a stored procedure. In this case, SSMA creates a stored procedure and a function that calls the stored procedure.|  
|Procedures|If the procedure can be directly converted to [!INCLUDE[tsql](../../includes/tsql-md.md)], SSMA creates a stored procedure.<br /><br />In some cases a stored procedure must be called in an autonomous transaction. In this case, SSMA creates two stored procedures: one that implements the procedure, and another that is used for calling the implementing stored procedure.|  
|Packages|SSMA creates a set of stored procedures and functions that are unified by similar object names.|  
|Sequences|SSMA creates sequence objects (SQL Server 2012 or SQL Server 2014) or emulates Oracle sequences.|  
|Tables with dependent objects such as indexes and triggers|SSMA creates tables with dependent objects.|  
|View with dependent objects, such as triggers|SSMA creates views with dependent objects.|  
|Materialized Views|**SSMA creates indexed views on SQL server with some exceptions. Conversion will fail if the materialized view includes one or more of the following constructs:**<br /><br />User-defined function<br /><br />Non deterministic field / function / expression in SELECT, WHERE or GROUP BY clauses<br /><br />Usage of Float column in SELECT*, WHERE or GROUP BY clauses  (special case of previous issue)<br /><br />Custom data type (incl. nested tables)<br /><br />COUNT(distinct &lt;field&gt;)<br /><br />FETCH<br /><br />OUTER joins (LEFT, RIGHT, or FULL)<br /><br />Subquery, other view<br /><br />OVER, RANK, LEAD, LOG<br /><br />MIN, MAX<br /><br />UNION, MINUS, INTERSECT<br /><br />HAVING|  
|Trigger|**SSMA creates triggers based on the following rules:**<br /><br />BEFORE triggers are converted to INSTEAD OF triggers.<br /><br />AFTER triggers are converted to AFTER triggers.<br /><br />INSTEAD OF triggers are converted to INSTEAD OF triggers. Multiple INSTEAD OF triggers defined on the same operation are combined into one trigger.<br /><br />Row-level triggers are emulated using cursors.<br /><br />Cascading triggers are converted into multiple individual triggers.|  
|Synonyms|**Synonyms are created for the following object types:**<br /><br />Tables and object tables<br /><br />Views and object views<br /><br />Stored procedures<br /><br />Functions<br /><br />**Synonyms for the following objects are resolved and replaced by direct object references:**<br /><br />Sequences<br /><br />Packages<br /><br />Java class schema objects<br /><br />User-defined object types<br /><br />Synonyms for another synonym cannot be migrated and will be marked as errors.<br /><br />Synonyms are not created for Materialized views.|  
|User Defined Types|**SSMA does not provide support for conversion of user defined types. User Defined Types, including its usage in PL/SQL programs are marked with special conversion errors guided by the following rules:**<br /><br />Table column of a user defined type is converted to VARCHAR(8000).<br /><br />Argument of user defined type to a stored procedure or function is converted to VARCHAR(8000).<br /><br />Variable of user defined type in PL/SQL block is converted to VARCHAR(8000).<br /><br />Object Table is converted to a Standard table.<br /><br />Object view is converted to a Standard view.|  
  
## Converting Oracle Database Objects  
To convert Oracle database objects, you first select the objects that you want to convert, and then have SSMA perform the conversion. To view output messages during the conversion, on the **View** menu, select **Output**.  
  
**To convert Oracle objects to SQL Server syntax**  
  
1.  In Oracle Metadata Explorer, expand the Oracle server, and then expand **Schemas**.  
  
2.  Select objects to convert:  
  
    -   To convert all schemas, select the check box next to **Schemas**.  
  
    -   To convert or omit a database, select the check box next to the schema name.  
  
    -   To convert or omit a category of objects, expand a schema, and then select or clear the check box next to the category.  
  
    -   To convert or omit individual objects, expand the category folder, and then select or clear the check box next to the object.  
  
3.  To convert all selected objects, right-click **Schemas** and select **Convert Schema**.  
  
    You can also convert individual objects or categories of objects by right-clicking the object or its parent folder, and then selecting **Convert Schema**.  
  
## Viewing Conversion Problems  
Some Oracle objects might not be converted. You can determine the conversion success rates by viewing the summary conversion report.  
  
**To view a summary report**  
  
1.  In Oracle Metadata Explorer, select **Schemas**.  
  
2.  In the right pane, select the **Report** tab.  
  
    This report shows the summary assessment report for all database objects that have been assessed or converted. You can also view a summary report for individual objects:  
  
    -   To view the report for an individual schema, select the schema in Oracle Metadata Explorer.  
  
    -   To view the report for an individual object, select the object in Oracle Metadata Explorer. Objects that have conversion problems have a red error icon.  
  
For objects that failed conversion, you can view the syntax that resulted in the conversion failure.  
  
**To view individual conversion problems**  
  
1.  In Oracle Metadata Explorer, expand **Schemas**.  
  
2.  Expand the schema that shows a red error icon.  
  
3.  Under the schema, expand a folder that has a red error icon.  
  
4.  Select the object that has a red error icon.  
  
5.  In the right pane, click the **Report** tab.  
  
6.  At the top of the **Report** tab is a drop-down list. If the list shows **Statistics**, change the selection to **Source**.  
  
    SSMA will display the source code and several buttons immediately above the code.  
  
7.  Click the **Next Problem** button. This is a red error icon with an arrow that points to the right.  
  
    SSMA will highlight the first problematic source code it finds in the current object.  
  
For each item that could not be converted, you have to determine what you want to do with that object:  
  
-   You can modify the source code for procedures on the **SQL** tab.  
  
-   You can modify the object in the Oracle database to remove or revise problematic code. To load the updated code into SSMA, you will have to update the metadata. For more information, see [Connecting to Oracle Database &#40;OracleToSQL&#41;](../../ssma/oracle/connecting-to-oracle-database-oracletosql.md).  
  
-   You can exclude the object from migration. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer and Oracle Metadata Explorer, clear the check box next to the item before loading the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and migrating data from Oracle.  
  
## Next Step  
The next step in the migration process is to [Load the converted objects into SQL Server](loading-converted-database-objects-into-sql-server-oracletosql.md).  
  
## See Also  
[Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)  
  
