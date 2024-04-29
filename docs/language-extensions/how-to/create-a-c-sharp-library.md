---
title: Create a .NET DLL from a C# project
description: Package your .NET project into a DLL file when using SQL Server Language Extensions to execute C# code.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: how-to
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# Create a .NET DLL from a C# project

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

Learn how to package your C# code into a dynamic link library (DLL) file, when using [SQL Server Language Extensions](../language-extensions-overview.md).

## Create a `.dll` file

To create a `.dll`, navigate to the folder containing your C# project and run this command, where `MyProject.csproj` is the name of the project file:

```cmd
dotnet publish MyProject.csproj
```

## Related content

- [How to call the .NET runtime in SQL Server Language Extensions](call-c-sharp-from-sql.md)
