---
title: "Data Access Tracing with the ODBC Driver on Linux | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "data access tracing"
  - "tracing"
ms.assetid: 3149173a-588e-47a0-9f50-edb8e9adf5e8
caps.latest.revision: 13
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Data Access Tracing with the ODBC Driver on Linux
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Linux supports tracing of ODBC API call entry and exit.  
  
To trace your application behavior, first add the following line to the odbcinst.ini file:  
  
```  
Trace=Yes  
```  
  
Then start your application with strace. For example:  
  
```  
strace -t -f -o trace_out.txt executable  
```  
  
After you finish tracing your application, remove `Trace=Yes` from the odbcinst.ini file to avoid the performance penalty of tracing.  
  
Tracing applies to all applications that use the driver in odbcinst.ini. To not trace all applications (for example, to avoid disclosing sensitive per-user information), you can trace an individual application instance by providing it the location of a private odbcinst.ini, by using the ODBCSYSINI environment variable. For example:  
  
```  
$ ODBCSYSINI=/home/myappuser myapp  
```  
  
In this case, you can add **Trace=Yes** to the [ODBC Driver 11 for SQL Server] section of /home/myappuser/odbcinst.ini.  
  
## Determining Which odbc.ini File The Driver Is Using  
The Linux ODBC Driver does not know which odbc.ini is in use, or the path to the odbc.ini file. However, information about which odbc.ini file is in use is available, from the unixODBC tools odbc_config and odbcinst, and from the unixODBC Driver Manager documentation.  
  
For example, the following command prints (among other information) the location of system and user odbc.ini files that may contain, respectively, system and user DSNs:  
  
```  
$ odbcinst -j  
unixODBC 2.3.1  
DRIVERS............: /etc/odbcinst.ini  
SYSTEM DATA SOURCES: /etc/odbc.ini  
FILE DATA SOURCES..: /etc/ODBCDataSources  
USER DATA SOURCES..: /home/odbcuser/.odbc.ini  
SQLULEN Size.......: 8  
SQLLEN Size........: 8  
SQLSETPOSIROW Size.: 8  
```  
  
The [unixODBC documentation](http://www.unixodbc.org/doc/UserManual/) explains how the user vs. system DSN decision is made. Specifically:  
  
User DSN. These are your personal Data Sources. You are able to add new ones, remove existing ones, and configure existing ones. User DSN information is stored in a secret location where only you can access them. Keeping your User DSNs separate from other User DSNs allows you a great deal of flexibility and control over creating and working with data sources which are only important to you.  
  
System DSN. These are created by the System Administrator. They act very much like the User DSNs but with three important differences:  
  
-   Only the System Administrator can add, remove, and configure System DSNs.  
  
-   System DSNs will be used only if the DSN does not exist as a User DSN. In other words, your User DSN has precedence over the System DSN.  
  
-   Everyone shares the same list of System DSNs.  
  
