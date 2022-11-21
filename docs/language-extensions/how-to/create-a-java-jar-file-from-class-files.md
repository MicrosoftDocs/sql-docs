---
title: Create a Java jar file from class files
description: Package your class files into a jar file when using SQL Server Language Extensions to execute Java code. 
author: rothja
ms.author: jroth 
ms.date: 11/05/2019
ms.topic: how-to
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# Create a Java jar file from class files
[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

Learn how to package your class files into a jar file, when using [SQL Server Language Extensions](../language-extensions-overview.md) to execute Java code. We recommend you package your files.

## Create a jar file

To create a jar from class files, navigate to the folder containing your class file and run this command:

```cmd
jar -cf <MyJar.jar> *.class
```

Make sure the path to `jar.exe` is part of the system path variable. Alternatively, specify the full path to jar which can be found under `/bin` in the JDK folder. For example:

```cmd
C:\Users\MyUser\Desktop\jdk1.8.0_201\bin\jar -cf <MyJar.jar> *.class
```

## Next steps

+ [How to call the Java runtime in SQL Server Language Extensions](../how-to/call-java-from-sql.md)