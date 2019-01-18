---
title: "Bulk Load Security Considerations (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLXML, XML Bulk Load"
  - "bulk load [SQLXML], security"
  - "security [SQLXML], XML Bulk Load"
  - "XML Bulk Load [SQLXML], security"
ms.assetid: 192fc6d4-ecbc-4a4d-a5cb-55e1f64af318
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Bulk Load Security Considerations (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The following are security guidelines for using XML Bulk Load:  
  
-   When you specify that the Bulk Load operation is to be performed as a transaction, you use the **TempFilePath** property to specify a folder in which to create the temporary files.  
  
     The Bulk Load process creates these temporary files with the following permissions:  
  
    -   Read/Write/Delete access is granted to the Bulk Load process.  
  
    -   Read permission is granted to all users, because the account under which Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will access these files is unknown. You can restrict the access to these temporary files by setting the appropriate permissions on the folder that contains them.  
  
-   XML Bulk Load does not itself have any permissions settings. It is assumed that the database is set up correctly and that the user context (that is, the login that Bulk Load is set use) has appropriate permissions set.  
  
-   In non-transactional mode, if an error occurs during the Bulk Load process, data may be left in a partially loaded state. Bulk Load will simply stop at the point where it is when this happens. Transactional mode can be used to alleviate this issue.  
  
-   When Bulk Load errors occur, they may include information about the database. For example, they may include the name of a table or column, or column type information. When you use Bulk Load, you should take care to catch errors from the Bulk Load process and return a generic error message, rather than exposing errors directly to users.  
  
-   Bulk Load sets no limit on the amount of data it works over. Bulk Load does not do any checking on the size of the data to be loaded. It is the responsibility of the user executing Bulk Load to ensure that there is enough memory to process the specified file, and that there is enough room in the database to store the data being loaded.  
  
-   Bulk Load does not make an attempt to use the data it is given as code. The data input is never executed in any fashion. Any code or commands in the input data are treated as normal data and will not be executed.  
  
-   Bulk Load may make formatting changes to the given data based on differences between the XML and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data models. For example, the format for specifying a time is different. Bulk Load will attempt to resolve these differences. As a result, some precision information may be lost.  
  
-   Bulk Load sets no limit on the amount of time it takes to process the data. Processing will continue until processing is complete or an error occurs.  
  
-   Bulk Load can create and delete temporary tables within the database, and needs permissions to do so. Permissions to these tables will be given to the same user who is connecting to the database for the Bulk Load process.  
  
-   Bulk Load can create and delete temporary files used during transactional mode processing, and needs permissions to do so. These files are created with the same permissions as the current user of the thread within which Bulk Load is running.  
  
-   If the user sets an error Log file for SQLXML to write errors into, then each time Bulk Load is executed the file will be overwritten with data from the last Bulk Load process.  
  
## See Also  
 [Performing Bulk Load of XML Data &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/performing-bulk-load-of-xml-data-sqlxml-4-0.md)  
  
  
