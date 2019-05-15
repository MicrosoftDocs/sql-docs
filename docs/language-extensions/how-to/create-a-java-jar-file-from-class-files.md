---
title: Create a Java jar file from class files
titleSuffix: SQL Server Language Extensions
description: Learn how to create a Java jar file from class files 
author: dphansen
ms.author: davidph 
manager: cgronlun
ms.date: 05/15/2019
ms.topic: overview
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# What is SQL Server Language Extensions?
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

When executing a Java code from SQL Server, we recommend to package your class files into a jar file.

## Create a jar file

To create a jar from class files, navigate to the folder containing your class file and run this command:

```cmd
jar -cf <MyJar.jar> *.class
```

Make sure the path to **jar.exe** is part of the system path variable. Alternatively, specify the full path to jar which can be found under /bin in the JDK folder. For example:

```cmd
C:\Users\MyUser\Desktop\jdk1.8.0_201\bin\jar -cf <MyJar.jar> *.class
```