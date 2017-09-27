---
title: "Data Source Wizard Screen 4 | Microsoft Docs"
ms.custom: ""
ms.date: "09/27/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 76326eeb-1144-4b9f-85db-50524c655d30
caps.latest.revision: 22
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Data Source Wizard Screen 4

Specify the language to be used for SQL Server messages, the character set translation, and whether the ODBC driver for SQL Server should use regional settings. You can also control the logging of long-running queries and driver statistics settings.

## Options

### **Change the language of SQL Server system messages to**

Each instance of SQL Server can have multiple sets of system messages, with each set in a different language (for example, English, Spanish, French, and so on). If a data source is defined against a server that has multiple sets of system messages, you can specify which language you want to use for system messages. In the list, click the language. This option is unavailable if only one language is installed on the SQL Server.

### **Use strong encryption for data**

When selected, data that is passed through connections that are made using this DSN will be encrypted. Logins are encrypted by default, even if the check box is cleared.

### **Perform translation for character data**

When this check box is selected, the ODBC driver for SQL Server converts ANSI strings sent between the client computer and SQL Server by using Unicode. The ODBC driver sometimes converts between the SQL Server code page and Unicode on the client computer. This requires that the code page used by SQL Server be one of the code pages available on the client computer.

When this check box is cleared, no translation of extended characters in ANSI character strings is done when they are sent between the client application and the server. If the client computer is using an ANSI code page (ACP) different from the SQL Server code page, extended characters in ANSI character strings may be misinterpreted. If the client computer is using the same code page for its ACP that SQL Server is using, the extended characters are interpreted correctly.

### **Use regional settings when outputting currency, numbers, dates, and times**

Specifies that the driver use the regional settings of the client computer for formatting currency, numbers, dates, and times in character output strings. The driver uses the default regional setting for the Windows login account of the user connecting through the data source. Select this option for applications that only display data, not for applications that process data.

### **Save long running queries to the log file**

Specifies that the driver log any query that takes longer than the Long query time value. Long-running queries are logged to the specified file. To specify a log file, either type the full path and file name in the box, or click **Browse** to select a log file by navigating through existing file directories.

### **Long query time (milliseconds)**

Specifies a threshold value, in milliseconds, for long-running query logging. Any query that takes longer than this number of milliseconds to run is logged.

### **Log ODBC driver statistics to the log file**

Specifies that statistics be logged. Statistics are logged to the specified file. To specify a log file, either type the full path and file name in the box or click **Browse** to select a log file by navigating through existing file directories.

The statistics log is a tab-delimited file that can be analyzed in Microsoft Excel or any other application that supports tab-delimited files.
