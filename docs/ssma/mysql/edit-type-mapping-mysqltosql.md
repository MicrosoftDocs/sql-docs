---
title: "Edit Type Mapping (MySQLToSQL)"
description: "Edit Type Mapping (MySQLToSQL)"
author: cpichuka
ms.author: cpichuka
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Edit Type Mapping (MySQLToSQL)
The **Edit Type Mapping** dialog box lets you specify how types are mapped between the source and destination database objects.  
  
You can access this dialog box in several places:  
  
-   When you select a source database or database object, the **Type Mapping** tab appears to the right of the metadata explorer. Click **Add** to add a new type mapping, or click **Edit** to change an existing type mapping.  
  
-   On the **Tools** menu, select **Project Settings** or **Default Project Settings**. In the resulting dialog box, select **Type Mapping**. Click **Add** to add a new type mapping, or click **Edit** to change an existing type mapping.  
  
-   Table-specific type mappings override database and project type mappings. Database-specific mappings override project mappings.  
  
## Options  
  
##### Source type  
Select the source data type to map to a SQL Server data type.  
  
If the data type is of variable length, the following fields will appear under **Sourcetype**:  
  
##### From  
Specify the minimum length for this mapping. For example, for the **nchar** data type, you can enter 10 to specify that this mapping is for a range starting at **nchar(10).**  
  
##### To  
Specify the maximum length for this mapping. For example, for the **nchar** data type, you can enter 20 to specify that this mapping is for a range ending at **nchar(20).**  
  
##### Target type  
Select the SQL Server data type to which the source data type is mapped. When SSMA creates the table or in the SQL Server, the source data type will change to this data type.  
  
If the data type is of variable length, the following field will appear under **Target type**:  
  
##### Replace with  
Specify the target length for this mapping. For example, for the **nvarchar** data type, you can enter 20 to specify that the specified source data type should be mapped to **nvarchar (20).**  
  
