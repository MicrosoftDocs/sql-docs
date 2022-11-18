---
title: "Data Type Mapping for Oracle Publishers | Microsoft Docs"
description: Learn about default mappings of data types between Oracle and SQL Server when data is moved from the Oracle Publisher to the SQL Server Distributor.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], data type mapping"
  - "data types [SQL Server replication], Oracle publishing"
  - "mapping data types [SQL Server replication]"
ms.assetid: 6da0e4f4-f252-4b7e-ba60-d2e912aa278e
author: "MashaMSFT"
ms.author: "mathoma"
---
# Data Type Mapping for Oracle Publishers
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Oracle data types and [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types do not always match exactly. Where possible, the matching data type is selected automatically when publishing an Oracle table. In cases that a single data type mapping is not clear, alternative data type mappings are provided. For information about how to select alternative mappings, see the "Specifying Alternative Data Type Mappings" section later in this topic.  
  
 The following table shows how data types are mapped by default between Oracle and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] when data is moved from the Oracle Publisher to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor. The Alternatives column indicates whether alternative mappings are available.  
  
|Oracle data type|SQL Server data type|Alternatives|  
|----------------------|--------------------------|------------------|  
|BFILE|VARBINARY(MAX)|Yes|  
|BLOB|VARBINARY(MAX)|Yes|  
|CHAR([1-2000])|CHAR([1-2000])|Yes|  
|CLOB|VARCHAR(MAX)|Yes|  
|DATE|DATETIME|Yes|  
|FLOAT|FLOAT|No|  
|FLOAT([1-53])|FLOAT([1-53])|No|  
|FLOAT([54-126])|FLOAT|No|  
|INT|NUMERIC(38)|Yes|  
|INTERVAL|DATETIME|Yes|  
|LONG|VARCHAR(MAX)|Yes|  
|LONG RAW|IMAGE|Yes|  
|NCHAR([1-1000])|NCHAR([1-1000])|No|  
|NCLOB|NVARCHAR(MAX)|Yes|  
|NUMBER|FLOAT|Yes|  
|NUMBER([1-38])|NUMERIC([1-38])|No|  
|NUMBER([0-38],[1-38])|NUMERIC([0-38],[1-38])|Yes|  
|NVARCHAR2([1-2000])|NVARCHAR([1-2000])|No|  
|RAW([1-2000])|VARBINARY([1-2000])|No|  
|REAL|FLOAT|No|  
|ROWID|CHAR(18)|No|  
|TIMESTAMP|DATETIME|Yes|  
|TIMESTAMP(0-7)|DATETIME|Yes|  
|TIMESTAMP(8-9)|DATETIME|Yes|  
|TIMESTAMP(0-7) WITH TIME ZONE|VARCHAR(37)|Yes|  
|TIMESTAMP(8-9) WITH TIME ZONE|VARCHAR(37)|No|  
|TIMESTAMP(0-7) WITH LOCAL TIME ZONE|VARCHAR(37)|Yes|  
|TIMESTAMP(8-9) WITH LOCAL TIME ZONE|VARCHAR(37)|No|  
|UROWID|CHAR(18)|No|  
|VARCHAR2([1-4000])|VARCHAR([1-4000])|Yes|  
  
## Considerations for Data Type Mapping  
 Keep the following data type issues in mind when replicating data from an Oracle database.  
  
### Unsupported Data Types  
 The following data types are not supported; columns that have these types cannot be replicated:  
  
-   Object types  
  
-   XML types  
  
-   Varrays  
  
-   Nested tables  
  
-   Columns that use REF  
  
### The DATE Data Type  
 Dates in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] range from 1753 A.D. to 9999 A.D., whereas dates in Oracle range from 4712 B.C. to 4712 A.D. If a column of type DATE contains values that are out of range for SQL Server, select the alternative data type for the column, which is VARCHAR(19).  
  
### FLOAT and NUMBER Types  
 The scale and precision specified during the mapping of FLOAT and NUMBER data types depends upon the scale and precision specified for the column using the data type in the Oracle database. Precision is the number of digits in a number. Scale is the number of digits to the right of the decimal point in a number. For example, the number 123.45 has a precision of 5 and a scale of 2.  
  
 Oracle allows numbers to be defined with a scale greater than the precision, such as NUMBER(4,5), but [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] requires the precision to be equal to or greater than the scale. To ensure there is no data truncation, if the scale is greater than the precision at the Oracle Publisher, the precision is set equal to the scale when the data type is mapped: NUMBER(4,5) would be mapped as NUMERIC(5,5).  
  
> [!NOTE]  
>  If you do not specify a scale and precision for NUMBER, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] defaults to using the maximum scale (8) and precision (38). We recommend that you set a specific scale and precision in Oracle for better storage and performance when the data is replicated.  
  
### Large Object Types  
 Oracle supports up to 4 gigabytes (GB), whereas SQL Server supports up to 2 GB. Data replicated above 2 GB is truncated.  
  
 If an Oracle table includes a BFILE column, the data for the column is stored in the file system. The replication administrative user account must be granted access to the directory in which the data is stored using the following syntax:  
  
 `GRANT READ ON DIRECTORY <directory_name> TO <replication_administrative_user_schema>`  
  
 For more information about large objects types, see the section "Considerations for Large Objects" in [Design Considerations and Limitations for Oracle Publishers](../../../relational-databases/replication/non-sql/design-considerations-and-limitations-for-oracle-publishers.md).  
  
## Specifying Alternative Data Type Mappings  
 Typically, the default data type mapping is appropriate, but for many Oracle data types, you can select a data type mapping from a set of alternative mappings, rather than using the default. There are two ways to specify alternative mappings:  
  
-   Override the default on a per-article basis using stored procedures or the New Publication Wizard.  
  
-   Globally change the default for all future articles using stored procedures (defaults are not changed for existing articles).  
  
 To specify alternative data type mappings, see [Specify Data Type Mappings for an Oracle Publisher](../../../relational-databases/replication/publish/specify-data-type-mappings-for-an-oracle-publisher.md).  
  
## See Also  
 [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md)   
 [Design Considerations and Limitations for Oracle Publishers](../../../relational-databases/replication/non-sql/design-considerations-and-limitations-for-oracle-publishers.md)   
 [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md)  
  
  
