---
title: "Tutorial: Regex string search in C#"
description: This tutorial shows you how to use SQL Server Language Extensions and run C# code that search a string with regular expressions (regex).
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: tutorial
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# Tutorial: Search for a string using regular expressions (regex) in C#

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

This tutorial shows you how to use [SQL Server Language Extensions](../language-extensions-overview.md) to create a C# class that receives two columns (ID and text) from SQL Server and a regular expression (regex) as an input parameter. The class returns two columns back to SQL Server (ID and text).

For a given text in the text column sent to the C# class, the code checks if the given regular expression is fulfilled, and returns that text together with the original ID.

This sample code uses a regular expression that checks if a text contains the word `C#` or `c#`.

## Prerequisites

- Database Engine instance on [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, with the extensibility framework and .NET programming extension [on Windows](../install/windows-c-sharp.md). For more information, see [What is SQL Server Language Extensions?](../language-extensions-overview.md). For more information about coding requirements, see [How to call the .NET runtime in SQL Server Language Extensions](../how-to/call-c-sharp-from-sql.md).

- SQL Server Management Studio or Azure Data Studio for executing T-SQL.

- .NET 6 or later SDK on Windows.

- The `dotnet-core-CSharp-lang-extension-windows-release.zip` file from the [Microsoft Extensibility SDK for C# for SQL Server](../how-to/extensibility-sdk-c-sharp-sql-server.md).

Command-line compilation using `dotnet build` is sufficient for this tutorial.

## Create sample data

First, create a new database and populate a `testdata` table with `ID` and `text` columns.

```sql
CREATE DATABASE csharptest
GO
USE csharptest
GO

CREATE TABLE testdata (
    [id] INT,
    [text] VARCHAR(100),
)
GO

INSERT INTO testdata(id, "text") VALUES (4, 'This sentence contains C#')
INSERT INTO testdata(id, "text") VALUES (1, 'This sentence does not')
INSERT INTO testdata(id, "text") VALUES (3, 'I love c#!')
INSERT INTO testdata(id, "text") VALUES (2, NULL)
GO
```

## Create the main class

In this step, create a class file called `RegexSample.cs` and copy the following C# code into that file.

This main class is importing the SDK, which means that the C# file downloaded in the first step needs to be discoverable from this class.

```csharp
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Microsoft.Data.Analysis;
using Microsoft.SqlServer.CSharpExtension.SDK;
using System.Text.RegularExpressions;

namespace UserExecutor
{
    /// <summary>
    /// This class extends the AbstractSqlServerExtensionExecutor and uses
    /// a regular expression that checks if a text contains the word "C#" or "c#"
    /// </summary>
    public class CSharpRegexExecutor: AbstractSqlServerExtensionExecutor
    {
        /// <summary>
        /// This method overrides the Execute method from AbstractSqlServerExtensionExecutor.
        /// </summary>
        /// <param name="input">
        /// A C# DataFrame contains the input dataset.
        /// </param>
        /// <param name="sqlParams">
        /// A Dictionary contains the parameters from SQL server with name as the key.
        /// </param>
        /// <returns>
        /// A C# DataFrame contains the output dataset.
        /// </returns>
        public override DataFrame Execute(DataFrame input, Dictionary<string, dynamic> sqlParams){
            // Drop NULL values and sort by id
            //
            input = input.DropNulls().OrderBy("id");

            // Create empty output DataFrame with two columns
            //
            DataFrame output = new DataFrame(new PrimitiveDataFrameColumn<int>("id", 0), new StringDataFrameColumn("text", 0));

            // Filter text containing specific substring using regex expression
            //
            DataFrameColumn texts = input.Columns["text"];
            for(int i = 0; i < texts.Length; ++i)
            {
                if(Regex.IsMatch((string)texts[i], sqlParams["@regexExpr"]))
                {
                    output.Append(input.Rows[i], true);
                }
            }

            // Modify the parameters
            //
            sqlParams["@rowsCount"]  = output.Rows.Count;
            sqlParams["@regexExpr"] = "Success!";

            // Return output dataset as a DataFrame
            //
            return output;
        }
    }
}
```

## Compile and create a DLL file

Package your classes and dependencies into a DLL. You can create a `.csproj` file called `RegexSample.csproj` and copy the following code into that file.

```csharp
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <EnableDynamicLoading>true</EnableDynamicLoading>
  </PropertyGroup>
  <PropertyGroup>
    <OutputPath>$(BinRoot)/$(Configuration)/</OutputPath>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.Data.Analysis" Version="0.4.0" />
  </ItemGroup>
  <ItemGroup>
    <Reference Include="Microsoft.SqlServer.CSharpExtension.SDK">
      <HintPath>[path]\Microsoft.SqlServer.CSharpExtension.dll</HintPath>
    </Reference>
  </ItemGroup>
</Project>
```

Go to the project folder and run `dotnet build`, which generates the following file:

`path\to\project\bin\Debug\RegexSample.dll`

For more information, see [Create a .NET DLL from a C# project](../how-to/create-a-c-sharp-library.md).

## Create external language

You need to create an external language in the database. The external language is a database scoped object, which means that external languages like C# need to be created for each database you want to use it in.

1. Create a `.zip` file containing the extension.

   As part of the SQL Server setup on Windows, the .NET extension `.zip` file is installed in this location: `<SQL Server install path>\MSSQL\Binn>\dotnet-core-CSharp-lang-extension.zip`. This zip file contains the `nativecsharpextension.dll`.

1. Create an external language `dotnet` from the `.zip` file:

   ```sql
   CREATE EXTERNAL LANGUAGE [dotnet]
   FROM (
       CONTENT = N'<path>\dotnet-core-CSharp-lang-extension.zip',
       FILE_NAME = 'nativecsharpextension.dll'
   );
   GO
   ```

## Set permissions

To execute .NET C# code, the user `SID S-1-15-2-1` (`<LocalMachineName>\ALL APPLICATION PACKAGES`) needs to be granted read permissions to the `\MSSQL` folder.

1. Right-click the folder and choose **Properties** > **Security**
1. Select **Edit**
1. Select **Add**
1. In **Select Users, Computer, Service Accounts, or Groups**:
   1. Select **Object Types** and make sure **Built-in security principles and Groups** is selected
   1. Select **Locations** to select the local computer name at the top of the list
   1. Enter `ALL APPLICATION PACKAGES`, check the name, and select **OK** to add. If the name doesn't resolve, revisit the **Locations** step. The system identifier (SID) is local to your machine.

For more information, see [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md).

## Create external libraries

Use [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md) to create an external library for your DLL files. SQL Server has access to the `.dll` files and you don't need to set any special permissions to the `classpath`.

Create an external library for the RegEx code.

```sql
CREATE EXTERNAL LIBRARY [regex.dll]
FROM (CONTENT = N'<path>\RegexSample.dll')
WITH (LANGUAGE = 'Dotnet');
GO
```

## <a id="call-method"></a> Call the C# class

Call the stored procedure `sp_execute_external_script` to invoke the C# code from SQL Server. In the script parameter, define which `libraryname;namespace.classname` you want to call. You can also define which `namespace.classname` you want to call without specifying the library name. The extension will find the first library that has the matched `namespace.classname`. In the following code, the class belongs to a namespace called `UserExecutor` and a class called `CSharpRegexExecutor`.

The code doesn't define which method to call. By default, the `Execute` method will be called. This means that you need to follow the SDK interface and implement an `Execute` method in your C# class, if you want to be able to call the class from SQL Server.

The stored procedure takes an input query (input dataset) and a regular expression and returns the rows that fulfilled the given regular expression. It uses a regular expression `[Cc]#` that checks if a text contains the word `C#` or `c#`.

```sql
DECLARE @rowsCount INT;
DECLARE @regexExpr VARCHAR(200);

SET @regexExpr = N'[Cc]#';

EXEC sp_execute_external_script @language = N'dotnet',
    @script = N'regex.dll;UserExecutor.CSharpRegexExecutor',
    @input_data_1 = N'SELECT * FROM testdata',
    @params = N'@regexExpr VARCHAR(200) OUTPUT, @rowsCount INT OUTPUT',
    @regexExpr = @regexExpr OUTPUT,
    @rowsCount = @rowsCount OUTPUT
WITH result sets((
            id INT,
            TEXT VARCHAR(100)
            ));

SELECT @rowsCount AS rowsCount, @regexExpr AS message;
```

### Results

After executing the call, you should get a result set with two of the rows.

:::image type="content" source="../media/c-sharp/c-sharp-sample-results.png" alt-text="Screenshot of results from C# sample.":::

## Related content

- [How to call the .NET runtime in SQL Server Language Extensions](../how-to/call-c-sharp-from-sql.md)
- [What is SQL Server Language Extensions?](../language-extensions-overview.md)
- [C# .NET and SQL Server supported data types](../how-to/c-sharp-to-sql-data-types.md)
