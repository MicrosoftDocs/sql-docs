---
title: "Connection Strings for JDBC and Microsoft JDBC Driver 4.0 for SQL Server (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7159c1bf-8f5d-4970-b9c4-b8fedffd4f29
caps.latest.revision: 12
author: BarbKess
---
# Connection Strings for JDBC and Microsoft JDBC Driver 4.0 for SQL Server (SQL Server PDW)
This topic describes how to connect to SQL Server PDW with JDBC by using Microsoft JDBC Driver 4.0 for SQL Server. You can use JDBC to write Java-based tools and applications that connect to SQL Server PDW.  
  
## Contents  
  
-   [Before You Begin](#BeforeBegin)  
  
-   [Connection String Keywords](#ConnectionString)  
  
-   [Supported Data Types](#DataTypes)  
  
## <a name="BeforeBegin"></a>Before You Begin  
**Software Requirements**  
  
-   Microsoft JDBC Driver 4.0 for SQL Server  
  
-   Java 1.6 Update 30 or later, or Java 1.7  
  
-   Windows, Suse Linux, or Redhat Linux  
  
**Learn About JDBC**  
  
Before developing applications that use JDBC, learn more about the Microsoft JDBC 4.0 Driver for SQL Server.  
  
-   For more information about the driver, see [Microsoft JDBC Driver for SQL Server](http://msdn.microsoft.com/en-us/data/aa937724.aspx) on MSDN.  
  
-   For the Books Online driver documentation, see [Microsoft SQL Server JDBC Driver 4.0](http://msdn.microsoft.com/en-us/library/dd631801(SQL.10).aspx) on MSDN.  
  
## <a name="ConnectionString"></a>Connection String Keywords  
The following table lists the supported connection string keywords for connecting to SQL Server PDW by using Microsoft JDBC 4.0 Driver for SQL Server. This is a subset of keywords supported for connections to SQL Server.  
  
For a full description of each string keyword, connection string creation information, and usage of the APIs, see [Setting the Connection Properties (SQL Server 2012)](http://msdn.microsoft.com/en-us/library/ms378988(v=sql.110).aspx) on MSDN.  
  
|Property|Type|Default|PDW Support|  
|------------|--------|-----------|---------------|  
|applicationName|String<br /><br />[<=128 char]|null|Yes|  
|databaseName, database|String<br /><br />[<=128 char]|null|Yes|  
|Encrypt|boolean<br /><br />[“true”&#124;”false”]|false|Yes|  
|failoverPartner|String|null|No|  
|hostNameInCertificate|String|null|No|  
|instanceName|String<br /><br />[<=128 char]|null|No|  
|lastUpdateCount|boolean<br /><br />[“true”&#124;”false”]|true|No|  
|lockTimeout|int|-1|No|  
|loginTimeout|Int [0…65535]|15|Yes (client side)|  
|packetSize|int [-1&#124;0&#124;512…32767]|8000|Yes|  
|Password|String<br /><br />[<=128 char]|null|Yes|  
|portNumber, port|Int [0…65535]|1433|Yes|  
|responseBuffering|String<br /><br />[“full”&#124;”adaptive”]|adaptive|Yes (client)|  
|sendStringParametersAsUnicode|boolean<br /><br />[“true”&#124;”false”]|true|Yes|  
|sendTimeAsDatetime|boolean<br /><br />[“true”&#124;”false”]|true|Yes|  
|serverName, server|String|null|Yes|  
|username, user|String<br /><br />[<=128 char]|null|Yes|  
|trustServerCertificate|boolean<br /><br />[“true”&#124;”false”]|false|Yes<br /><br />This property only affects the server SSL certificate validation if and only if the **encrypt** property is set to “true”.|  
|trustStore|String|null|Yes|  
|trustStorePassword|String|null|Yes|  
|workstationID|String<br /><br />[<=128 char]|<empty string>|Yes|  
|xopenStates|Boolean<br /><br />[“true”&#124;”false”]|false|No|  
  
## <a name="DataTypes"></a>Supported Data Types  
All of the data types that SQL Server PDW supports are also supported when connecting to SQL Server PDW by using the Microsoft JDBC Driver 4.0 for SQL Server. For a list of the supported data types for SQL Server PDW, see [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).  
  
