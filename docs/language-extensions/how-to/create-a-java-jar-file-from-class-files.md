---
title: Create a Java .jar file from class files
description: Package your class files into a .jar file when using SQL Server Language Extensions to execute Java code.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: how-to
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# Create a Java .jar file from class files

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

Learn how to package your class files into a `.jar` file, when using [SQL Server Language Extensions](../language-extensions-overview.md) to execute Java code. We recommend you package your files.

## Create a `.jar` file

To create a `.jar` from class files, navigate to the folder containing your class file and run this command:

```cmd
jar -cf <MyJar.jar> *.class
```

Make sure the path to `jar.exe` is part of the system path variable. Alternatively, specify the full path to **jar** which can be found under `/bin` in the JDK folder. For example:

```cmd
C:\Users\MyUser\Desktop\jdk1.8.0_201\bin\jar -cf <MyJar.jar> *.class
```

## Related content

- [How to call the Java runtime in SQL Server Language Extensions](call-java-from-sql.md)
