---
title: "Sample JDBC Driver Applications | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: e136b87c-a138-45d6-8c3e-bcef94b7e483
author: MightyPen
ms.author: genemi
manager: craigg
---
# Sample JDBC Driver Applications

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample applications demonstrate various features of the JDBC driver. Additionally, they demonstrate good programming practices that you can follow when using the JDBC driver with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
All the sample applications are contained in *.java code files that can be compiled and run on your local computer, and they are located in various subfolders in the following location:  

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples  
```

The topics in this section describe how to configure and run the sample applications, and include a discussion of what the sample applications demonstrate.  
  
## In This Section  
  
| Topic                                                                                                        | Description                                                                                                                                                                                                                                                             |
| ------------------------------------------------------------------------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Connecting and Retrieving Data](../../connect/jdbc/connecting-and-retrieving-data.md)                       | These sample applications demonstrate how to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. They also demonstrate different ways in which to retrieve data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| [Working with Data Types &#40;JDBC&#41;](../../connect/jdbc/working-with-data-types-jdbc.md)                 | These sample applications demonstrate how to use the JDBC driver data type methods to work with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.                                                                                           |
| [Working with Result Sets](../../connect/jdbc/working-with-result-sets.md)                                   | These sample applications demonstrate how to use result sets to process data contained in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.                                                                                                         |
| [Working with Large Data](../../connect/jdbc/working-with-large-data.md)                                     | These sample applications demonstrate how to use adaptive buffering to retrieve large-value data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database without the overhead of server cursors.                                                      |
| [SQL data discovery and classification](../../connect/jdbc/data-discovery-classification-sample.md) | This sample application demonstrates how to retreive Data Discovery and Classification information contained in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database from a ResultSet object using JDBC Driver.                                      |
  
## See Also

[Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
